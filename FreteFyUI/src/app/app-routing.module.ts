import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CidadeComponent } from './components/cidade/cidade.component';
import { RegiaoComponent } from './components/regiao/regiao.component';
import { RegiaoFormComponent } from './regiao-form/regiao-form.component';
import { CidadeFormComponent } from './components/cidade-form/cidade-form.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'cidade', component: CidadeComponent },
  { path: 'regiao', component: RegiaoComponent },
  { path: 'regiao/adicionar', component: RegiaoFormComponent },
  { path: 'regiao/editar/:id', component: RegiaoFormComponent },
  { path: 'cidade/adicionar', component: CidadeFormComponent },
  { path: 'cidade/editar/:id', component: CidadeFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { } 