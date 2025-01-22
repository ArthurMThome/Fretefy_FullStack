import { environment } from "../../environments/environment";
import { HttpClient } from '@angular/common/http'
import { Observable } from "rxjs";
import { Regiao } from "../models/Regiao.model";
import { Injectable } from "@angular/core";
import { DefaultReturn } from "../models/DefaultReturn.model";

@Injectable({
    providedIn: 'root'
  })
export class RegiaoService {
  mainRoute = "/api/regiao";
  changeStatusRoute = "/changestatus";
  exportRoute = "/exportar";

    constructor(private readonly http: HttpClient){ }

  getAll(): Observable<DefaultReturn<Regiao[]>> {
  return this.http.get<DefaultReturn<Regiao[]>>(`${environment.baseUrl}${this.mainRoute}`);
  }

  changeStatus(regiao: Regiao): Observable<DefaultReturn<Regiao>> {
    return this.http.put<DefaultReturn<Regiao>>(`${environment.baseUrl}${this.mainRoute}${this.changeStatusRoute}`, regiao);
  }

  getById(id: string): Observable<DefaultReturn<Regiao>> {
    return this.http.get<DefaultReturn<Regiao>>(`${environment.baseUrl}${this.mainRoute}/${id}`);
  }
  
  updateRegiao(regiao: Regiao): Observable<DefaultReturn<Regiao>> {
    return this.http.put<DefaultReturn<Regiao>>(`${environment.baseUrl}${this.mainRoute}`, regiao);
  }

  createRegiao(regiao: Regiao): Observable<DefaultReturn<Regiao>> {
    return this.http.post<DefaultReturn<Regiao>>(`${environment.baseUrl}${this.mainRoute}`, regiao);
  }

  exportRegioes(): Observable<Blob> {
    return this.http.get(`${environment.baseUrl}${this.mainRoute}${this.exportRoute}`, { responseType: 'blob' });
  }
}