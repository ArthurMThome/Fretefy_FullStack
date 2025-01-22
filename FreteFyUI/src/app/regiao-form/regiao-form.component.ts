import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { RegiaoService } from '../services/regiao.service';
import { Regiao } from '../models/Regiao.model';
import { HttpStatusCode } from '@angular/common/http';
import { CidadeService } from '../services/cidade.service';
import { Cidade } from '../models/Cidade.model';

@Component({
  selector: 'app-regiao-form',
  templateUrl: './regiao-form.component.html',
  styleUrl: './regiao-form.component.scss',
  standalone: true, 
    imports: [
      CommonModule, 
      FormsModule, 
      ReactiveFormsModule
    ] 
})
export class RegiaoFormComponent {
  form: FormGroup;
  cidades: Cidade[] = [];
  regiao: Regiao | undefined;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private regiaoService: RegiaoService,
    private cidadeService: CidadeService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { 
    this.form = this.fb.group({
      id: [null],
      nome: ['', [Validators.required]],
      ativo: [true],
      cidades: this.fb.array([])
    });
  }

  ngOnInit(): void {
    const routeParams = this.activatedRoute.snapshot.paramMap;
    const regiaoId = routeParams.get('id');

    this.cidadeService.getAll().subscribe(result => {
      if(result.status == HttpStatusCode.Ok){
        this.cidades = result.obj;
      }
    });

    if (regiaoId) {
      this.isEditMode = true;
      this.regiaoService.getById(regiaoId).subscribe(result => {
        if(result.status == HttpStatusCode.Ok){
          this.regiao = result.obj;
          this.form.patchValue({
            id: this.regiao.id,
            nome: this.regiao.nome,
            status: this.regiao.status
          });
          console.log(this.regiao);
          this.setCidades(this.regiao.cidades);
        }
      });
    }
  }

  get cidadesFormArray(): FormArray {
    return this.form.get('cidades') as FormArray;
  }

  setCidades(cidades: Cidade[]): void {
    console.log(cidades);
    const cidadeControls = cidades.map(cidade => this.fb.group({
      id: [cidade.id],
      nome: [cidade.nome],
      uf: [cidade.uf]
    }));
    console.log(cidadeControls);
    this.cidadesFormArray.clear();
    cidadeControls.forEach(control => this.cidadesFormArray.push(control));
    console.log(this.cidadesFormArray);
  }

  addCidadeControl(): void {
    this.cidadesFormArray.push(this.fb.group({
      id: [null],
      nome: [''],
      uf: ['']
    }));
  }

  removeCidadeControl(index: number): void {
    this.cidadesFormArray.removeAt(index);
  }

  onSubmit(): void {
    if (this.form.valid) {
      const regiaoFormValue = this.form.value;
      
      const regiaoPayload: Regiao = {
        id: this.regiao?.id,
        nome: regiaoFormValue.nome,
        status: this.regiao?.status,
        cidades: regiaoFormValue.cidades.map((cidade: { id: string; nome: string; status: number; }) => {
          const matchingCity = this.cidades.find(c => c.id === cidade.id);
  
          if (matchingCity) {
            return {
              id: cidade.id,
              nome: matchingCity.nome,
              uf: matchingCity.uf
            };
          } else {
            console.error("Cidade com ID", cidade.id, "não encontrada");
            return {};
          }
        })
      };
  
      if (this.isEditMode) {
        this.regiaoService.updateRegiao(regiaoPayload)
        .subscribe(result => {
          if (result.status === HttpStatusCode.Ok) {
            console.log('Região atualizada com sucesso!');
            this.router.navigate(['/regiao']);
          } else {
            confirm('Erro ao cadastrar a região: ' + result.message)
          }
        });
      } else {
        this.regiaoService.createRegiao(regiaoPayload).subscribe(result => {
          if (result.status === HttpStatusCode.Ok) {
            console.log('Região atualizada com sucesso!');
            this.router.navigate(['/cidade']);
          } else {
            confirm('Erro ao cadastrar a região: ' + result.message)
          }
        });
      }
    }
  }
  
  cancelButton(){
    
    this.router.navigate(['/regiao']);
  }
}