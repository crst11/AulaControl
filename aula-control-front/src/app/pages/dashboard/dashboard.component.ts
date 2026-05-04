import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EstudiantesService } from '../../services/estudiantes.service';
import { EquiposService } from '../../services/equipos.service';
import { PrestamosService } from '../../services/prestamos.service';

import { Estudiante } from '../../models/estudiante';
import { Equipo } from '../../models/equipo';
import { Prestamo } from '../../models/prestamo';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  estudiantes: Estudiante[] = [];
  equipos: Equipo[] = [];
  prestamos: Prestamo[] = [];

  constructor(
    private estudiantesService: EstudiantesService,
    private equiposService: EquiposService,
    private prestamosService: PrestamosService
  ) {}

  ngOnInit(): void {
    this.cargarDatos();
  }

  cargarDatos(): void {
    this.estudiantesService.getEstudiantes().subscribe({
      next: data => this.estudiantes = data
    });

    this.equiposService.getEquipos().subscribe({
      next: data => this.equipos = data
    });

    this.prestamosService.getPrestamos().subscribe({
      next: data => this.prestamos = data
    });
  }

  contarDisponibles(): number {
    return this.equipos.filter(e => e.estado === 'Disponible').length;
  }

  contarPrestados(): number {
    return this.equipos.filter(e => e.estado === 'Prestado').length;
  }

  contarPrestamosActivos(): number {
    return this.prestamos.filter(p => p.estadoPrestamo === 'Activo').length;
  }
}