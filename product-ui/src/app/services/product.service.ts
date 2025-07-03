
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = 'https://localhost:7292/api/Product';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/GetAll`);
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/GetById/${id}`);
  }

  create(product: Product): Observable<{status: boolean; message: string; data: Product}> {
    //return this.http.post<Product>(this.apiUrl, product);
    return this.http.post<{status: boolean; message: string; data: Product}>(this.apiUrl, product);

  }

  update(id: number, product: Product): Observable<{status: boolean; message: string; data: number}> {
    return this.http.put<{status: boolean; message: string; data: number}>(`${this.apiUrl}/${id}`, product);
  }

  delete(id: number): Observable<{status: boolean; message: string; data: number}> {
    // return this.http.delete<void>(`${this.apiUrl}/${id}`);
    return this.http.delete<{ status: boolean; message: string; data: number }>(`${this.apiUrl}/${id}`);

  }
}
