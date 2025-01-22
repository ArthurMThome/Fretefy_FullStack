import { Cidade } from "./Cidade.model";

export interface Regiao {
    id: string | undefined;
    nome: string;
    status: number | undefined;
    cidades: Cidade[];
}