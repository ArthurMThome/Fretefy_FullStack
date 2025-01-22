import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CidadeComponent } from './cidade/cidade.component';
import { RegiaoComponent } from './regiao/regiao.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'cidade', component: CidadeComponent },
  { path: 'regiao', component: RegiaoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }