import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { EstudiantesService } from '../../services/estudiantes.service';
import { EquiposService } from '../../services/equipos.service';
import { PrestamosService } from '../../services/prestamos.service';

import { Estudiante } from '../../models/estudiante';
import { Equipo } from '../../models/equipo';
import { Prestamo } from '../../models/prestamo';

@Component({
  selector: 'app-prestamos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './prestamos.component.html',
  styleUrl: './prestamos.component.css'
})
export class PrestamosComponent implements OnInit {
  estudiantes: Estudiante[] = [];
  equipos: Equipo[] = [];
  equiposDisponibles: Equipo[] = [];
  prestamos: Prestamo[] = [];

  mensaje = '';
  tipoMensaje: 'info' | 'success' | 'error' = 'info';

  nuevoPrestamo: Prestamo = {
    estudianteId: 0,
    equipoId: 0,
    fechaDevolucionEstimada: '',
    observacion: ''
  };

  constructor(
    private estudiantesService: EstudiantesService,
    private equiposService: EquiposService,
    private prestamosService: PrestamosService
  ) {}

  ngOnInit(): void {
    this.cargarTodo();
  }

  mostrarMensaje(texto: string, tipo: 'info' | 'success' | 'error'): void {
    this.mensaje = texto;
    this.tipoMensaje = tipo;
  }

  cargarTodo(): void {
    this.estudiantesService.getEstudiantes().subscribe({
      next: data => this.estudiantes = data
    });

    this.equiposService.getEquipos().subscribe({
      next: data => this.equipos = data
    });

    this.equiposService.getDisponibles().subscribe({
      next: data => this.equiposDisponibles = data
    });

    this.prestamosService.getPrestamos().subscribe({
      next: data => this.prestamos = data,
      error: () => this.mostrarMensaje('Error al cargar préstamos.', 'error')
    });
  }

  guardarPrestamo(): void {
    if (this.nuevoPrestamo.estudianteId === 0) {
      this.mostrarMensaje('Selecciona un estudiante.', 'error');
      return;
    }

    if (this.nuevoPrestamo.equipoId === 0) {
      this.mostrarMensaje('Selecciona un equipo disponible.', 'error');
      return;
    }

    if (!this.nuevoPrestamo.fechaDevolucionEstimada) {
      this.mostrarMensaje('Selecciona la fecha de devolución estimada.', 'error');
      return;
    }

    this.prestamosService.crearPrestamo(this.nuevoPrestamo).subscribe({
      next: () => {
        this.mostrarMensaje('Préstamo registrado correctamente.', 'success');

        this.nuevoPrestamo = {
          estudianteId: 0,
          equipoId: 0,
          fechaDevolucionEstimada: '',
          observacion: ''
        };

        this.cargarTodo();
      },
      error: error => {
        this.mostrarMensaje(error.error || 'Error al registrar préstamo.', 'error');
      }
    });
  }

  devolverPrestamo(id: number): void {
    this.prestamosService.devolverPrestamo(id).subscribe({
      next: () => {
        this.mostrarMensaje('Préstamo devuelto correctamente. El equipo vuelve a estar disponible.', 'success');
        this.cargarTodo();
      },
      error: () => {
        this.mostrarMensaje('Error al devolver préstamo.', 'error');
      }
    });
  }

  obtenerNombreEstudiante(prestamo: Prestamo): string {
    if (prestamo.estudiante?.nombre) {
      return prestamo.estudiante.nombre;
    }

    const estudiante = this.estudiantes.find(e => e.id === Number(prestamo.estudianteId));
    return estudiante ? estudiante.nombre : 'Sin estudiante';
  }

  obtenerNombreEquipo(prestamo: Prestamo): string {
    if (prestamo.equipo?.nombre) {
      return prestamo.equipo.nombre;
    }

    const equipo = this.equipos.find(e => e.id === Number(prestamo.equipoId));
    return equipo ? equipo.nombre : 'Sin equipo';
  }
}