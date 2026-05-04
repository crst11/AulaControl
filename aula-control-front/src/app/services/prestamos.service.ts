import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Prestamo } from '../models/prestamo';
import { API_URL } from './api.config';

@Injectable({
  providedIn: 'root'
})
export class PrestamosService {
  private url = `${API_URL}/Prestamos`;

  constructor(private http: HttpClient) {}

  getPrestamos(): Observable<Prestamo[]> {
    return this.http.get<Prestamo[]>(this.url);
  }

  getActivos(): Observable<Prestamo[]> {
    return this.http.get<Prestamo[]>(`${this.url}/activos`);
  }

  crearPrestamo(prestamo: Prestamo): Observable<Prestamo> {
    return this.http.post<Prestamo>(this.url, prestamo);
  }

  devolverPrestamo(id: number): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}/devolver`, {});
  }
}