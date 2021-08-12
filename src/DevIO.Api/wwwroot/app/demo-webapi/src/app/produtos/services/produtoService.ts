import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

import { Observable } from 'rxjs';
import { catchError, map } from "rxjs/operators";
import { Fornecedor } from '../models/Fornecedor';
import { Product } from '../models/Product';
import { BaseService } from 'src/app/base/baseService';

@Injectable()
export class ProdutoService extends BaseService {
    constructor(private http: HttpClient) { super() }

    obterTodos(): Observable<Product[]> {
        return this.http
            .get<Product[]>(this.UrlServiceV1 + "products", super.ObterAuthHeaderJson())
            .pipe(
                catchError(this.serviceError));
    }

    registrarProdutoAlternativo(produto: FormData): Observable<Product> {

        return this.http
            .post(this.UrlServiceV1 + 'products', produto, super.ObterHeaderFormData())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );
    }

    registrarProduto(produto: Product): Observable<Product> {

        return this.http
            .post(this.UrlServiceV1 + 'products', produto, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );
    }

    obterFornecedores(): Observable<Fornecedor[]> {
        return this.http
            .get<Fornecedor[]>(this.UrlServiceV1 + 'suppliers', super.ObterAuthHeaderJson())
            .pipe(
                catchError(super.serviceError)
            );
    }
}