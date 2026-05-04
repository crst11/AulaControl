import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Estudiante } from '../models/estudiante';
import { API_URL } from './api.config';

@Injectable({
  providedIn: 'root'
})
export class EstudiantesService {
  private url = `${API_URL}/Estudiantes`;

  constructor(private http: HttpClient) {}

  getEstudiantes(): Observable<Estudiante[]> {
    return this.http.get<Estudiante[]>(this.url);
  }

  crearEstudiante(estudiante: Estudiante): Observable<Estudiante> {
    return this.http.post<Estudiante>(this.url, estudiante);
  }

  actualizarEstudiante(id: number, estudiante: Estudiante): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, estudiante);
  }

  eliminarEstudiante(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}