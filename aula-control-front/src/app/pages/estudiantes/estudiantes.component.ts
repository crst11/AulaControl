import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { EstudiantesService } from '../../services/estudiantes.service';
import { Estudiante } from '../../models/estudiante';

@Component({
  selector: 'app-estudiantes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './estudiantes.component.html',
  styleUrl: './estudiantes.component.css'
})
export class EstudiantesComponent implements OnInit {
  estudiantes: Estudiante[] = [];

  mensaje = '';
  tipoMensaje: 'info' | 'success' | 'error' = 'info';

  modoEdicion = false;
  estudianteEditandoId: number | null = null;

  estudianteForm: Estudiante = {
    nombre: '',
    codigoEstudiante: '',
    correo: '',
    programa: '',
    telefono: ''
  };

  constructor(private estudiantesService: EstudiantesService) {}

  ngOnInit(): void {
    this.cargarEstudiantes();
  }

  mostrarMensaje(texto: string, tipo: 'info' | 'success' | 'error'): void {
    this.mensaje = texto;
    this.tipoMensaje = tipo;
  }

  limpiarFormulario(): void {
    this.modoEdicion = false;
    this.estudianteEditandoId = null;

    this.estudianteForm = {
      nombre: '',
      codigoEstudiante: '',
      correo: '',
      programa: '',
      telefono: ''
    };
  }

  cargarEstudiantes(): void {
    this.estudiantesService.getEstudiantes().subscribe({
      next: (data: Estudiante[]) => {
        this.estudiantes = data;
      },
      error: () => {
        this.mostrarMensaje('Error al cargar estudiantes. Revisa que el backend esté encendido.', 'error');
      }
    });
  }

  guardarEstudiante(): void {
    if (
      !this.estudianteForm.nombre ||
      !this.estudianteForm.codigoEstudiante ||
      !this.estudianteForm.correo ||
      !this.estudianteForm.programa ||
      !this.estudianteForm.telefono
    ) {
      this.mostrarMensaje('Completa todos los campos del estudiante.', 'error');
      return;
    }

    if (this.modoEdicion && this.estudianteEditandoId !== null) {
      this.estudiantesService.actualizarEstudiante(this.estudianteEditandoId, this.estudianteForm).subscribe({
        next: () => {
          this.mostrarMensaje('Estudiante actualizado correctamente.', 'success');
          this.limpiarFormulario();
          this.cargarEstudiantes();
        },
        error: (error: any) => {
          this.mostrarMensaje(error.error || 'Error al actualizar estudiante.', 'error');
        }
      });

      return;
    }

    this.estudiantesService.crearEstudiante(this.estudianteForm).subscribe({
      next: () => {
        this.mostrarMensaje('Estudiante registrado correctamente.', 'success');
        this.limpiarFormulario();
        this.cargarEstudiantes();
      },
      error: (error: any) => {
        this.mostrarMensaje(error.error || 'Error al guardar estudiante.', 'error');
      }
    });
  }

  editarEstudiante(estudiante: Estudiante): void {
    this.modoEdicion = true;
    this.estudianteEditandoId = estudiante.id ?? null;

    this.estudianteForm = {
      nombre: estudiante.nombre,
      codigoEstudiante: estudiante.codigoEstudiante,
      correo: estudiante.correo,
      programa: estudiante.programa,
      telefono: estudiante.telefono
    };

    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  eliminarEstudiante(id: number): void {
    const confirmar = confirm(
      '¿Seguro que deseas eliminar este estudiante?\n\nNo se puede eliminar si tiene un préstamo activo. Si el préstamo ya fue devuelto, sí se puede eliminar.'
    );

    if (!confirmar) {
      return;
    }

    this.estudiantesService.eliminarEstudiante(id).subscribe({
      next: () => {
        this.mostrarMensaje('Estudiante eliminado correctamente.', 'success');
        this.cargarEstudiantes();
      },
      error: (error: any) => {
        this.mostrarMensaje(
          error.error ||
          'No se puede eliminar este estudiante porque tiene un préstamo activo. Primero debe devolver o saldar el préstamo.',
          'error'
        );
      }
    });
  }
}