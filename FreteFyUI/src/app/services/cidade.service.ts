import { environment } from "../../environments/environment";
import { HttpClient } from '@angular/common/http'
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { DefaultReturn } from "../models/DefaultReturn.model";
import { Cidade } from "../models/Cidade.model";

@Injectable({
    providedIn: 'root'
  })
export class CidadeService {
  mainRoute = "/api/cidade";
  regiaoRoute = "/regiao";

  constructor(private readonly http: HttpClient){ }

  getAll(): Observable<DefaultReturn<Cidade[]>> {
  return this.http.get<DefaultReturn<Cidade[]>>(`${environment.baseUrl}${this.mainRoute}`);
  }

  createCidade(cidade: Cidade): Observable<DefaultReturn<Cidade>> {
    return this.http.post<DefaultReturn<Cidade>>(`${environment.baseUrl}${this.mainRoute}`, cidade);
  }

  getById(id: string): Observable<DefaultReturn<Cidade>> {
    return this.http.get<DefaultReturn<Cidade>>(`${environment.baseUrl}${this.mainRoute}/${id}`);
    }
    
  getByRegiaoId(regiaoId: string): Observable<DefaultReturn<Cidade[]>> {
    return this.http.get<DefaultReturn<Cidade[]>>(`${environment.baseUrl}${this.mainRoute}${this.regiaoRoute}/${regiaoId}`);
    }

  getByRegiaoNull(): Observable<DefaultReturn<Cidade[]>> {
    return this.http.get<DefaultReturn<Cidade[]>>(`${environment.baseUrl}${this.mainRoute}${this.regiaoRoute}`);
    }

  updateCidade(cidade: Cidade): Observable<DefaultReturn<Cidade>> {
    return this.http.put<DefaultReturn<Cidade>>(`${environment.baseUrl}${this.mainRoute}`, cidade);
    }
}