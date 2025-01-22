import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { HttpStatusCode } from '@angular/common/http';
import { Cidade } from '../../models/Cidade.model';
import { CidadeService } from '../../services/cidade.service';

@Component({
  selector: 'app-cidade',
  templateUrl: './cidade.component.html',
  styleUrls: ['./cidade.component.scss'],
  standalone: true, 
  imports: [
    CommonModule,
    RouterModule
  ] 
})

export class CidadeComponent implements OnInit {
  cidades: Observable<Cidade[]> | undefined;

  constructor(
    private readonly cidadeService: CidadeService, 
    private readonly router: RouterModule
  ) { }

  ngOnInit(): void {
    this.getAll();
    
  }

  getAll(): void {

    this.cidadeService.getAll().subscribe(cidades => {
      console.log(cidades);
      if(cidades.status == HttpStatusCode.Ok){
        this.cidades = of(cidades.obj);
        console.log(this.cidades);
      }
    });
  }
}