import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Equipo } from '../models/equipo';
import { API_URL } from './api.config';

@Injectable({
  providedIn: 'root'
})
export class EquiposService {
  private url = `${API_URL}/Equipos`;

  constructor(private http: HttpClient) {}

  getEquipos(): Observable<Equipo[]> {
    return this.http.get<Equipo[]>(this.url);
  }

  getDisponibles(): Observable<Equipo[]> {
    return this.http.get<Equipo[]>(`${this.url}/disponibles`);
  }

  crearEquipo(equipo: Equipo): Observable<Equipo> {
    return this.http.post<Equipo>(this.url, equipo);
  }

  actualizarEquipo(id: number, equipo: Equipo): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, equipo);
  }

  eliminarEquipo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}