import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CidadeService } from '../../services/cidade.service';
import { Cidade } from '../../models/Cidade.model';
import { HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-cidade-form',
  templateUrl: './cidade-form.component.html',
  styleUrl: './cidade-form.component.scss',
  standalone: true, 
  imports: [
    CommonModule, 
    FormsModule, 
    ReactiveFormsModule
  ] 
})
export class CidadeFormComponent {
  form: FormGroup;
  cidade: Cidade | undefined;
  estados: string[] = [
    'AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 
    'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN', 
    'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO'
  ];

  constructor(
      private readonly cidadeService: CidadeService,
      private formBuilder: FormBuilder, 
      private router: Router,
      private activatedRoute: ActivatedRoute
    ) {
    this.form = this.formBuilder.group({
      nome: ['', Validators.required],
      uf: ['', Validators.required]
    });
  }

  ngOnInit() {
    const routeParams = this.activatedRoute.snapshot.paramMap;
    const cidadeId = routeParams.get('id');

    if (cidadeId) {
      this.cidadeService.getById(cidadeId)
        .subscribe(result => {
          if(result.status == HttpStatusCode.Ok){
            this.cidade = result.obj;
            this.form.patchValue(result.obj);
          }
        });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      if (this.cidade) {

        var newCidade = this.form.value;
        newCidade.id = this.cidade.id;

        this.cidadeService.updateCidade(newCidade)
          .subscribe(result => {
            if (result.status === HttpStatusCode.Ok) {
              console.log('Cidade atualizada com sucesso!');
              this.router.navigate(['/cidade']);
            } else {
              confirm('Erro ao cadastrar a cidade: ' + result.message)
            }
          });
      } else {
        this.cidadeService.createCidade(this.form.value).subscribe( result => {
          if(result.status == HttpStatusCode.Ok){
            console.log('Cidade cadastrada com sucesso!');
            this.router.navigate(['/cidade']);
          } else {
            confirm('Erro ao cadastrar a cidade: ' + result.message)
          }
          }
        );
      }
    }
  }

  cancelButton(){
    
    this.router.navigate(['/cidade']);
  }
}