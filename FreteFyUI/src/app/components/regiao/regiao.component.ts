import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Regiao } from '../../models/Regiao.model';
import { RegiaoService } from '../../services/regiao.service';
import { CommonModule } from '@angular/common';
import { HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-regiao',
  templateUrl: './regiao.component.html',
  styleUrls: ['./regiao.component.scss'],
  standalone: true, 
  imports: [
    CommonModule,
    RouterModule
  ] 
})

export class RegiaoComponent implements OnInit {
  regioes: Observable<Regiao[]> | undefined;

  constructor(
    private readonly regiaoService: RegiaoService, 
    private readonly router: RouterModule
  ) { }

  ngOnInit(): void {
    this.getAll();
    
  }

  getAll(): void {

    this.regiaoService.getAll().subscribe(regioes => {
      console.log(regioes);
      if(regioes.status == HttpStatusCode.Ok){
        this.regioes = of(regioes.obj);
        console.log(this.regioes);
      }
    });
  }

  changeStatus(regiao: Regiao): void {
    var message = '';
    if(regiao.status == 1){
      message = `Tem certeza que deseja INATIVAR a região "${regiao.nome}"?`;
    }
    else{
      message = `Tem certeza que deseja ATIVAR a região "${regiao.nome}"?`;
    }

    if (confirm(message)) {
      this.regiaoService.changeStatus(regiao).subscribe( regiao => {
        console.log(regiao);
          this.getAll();
        }
      );
    }
  }

  exportar(): void {
    this.regiaoService.exportRegioes().subscribe((response: BlobPart) => {
      const blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `Regioes.xlsx`;
      link.click();
    });
  }

}