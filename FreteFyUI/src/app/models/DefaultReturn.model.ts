import { HttpStatusCode } from "@angular/common/http";

export interface DefaultReturn<T> {
    status: HttpStatusCode;
    message: string;
    obj: T;
  }