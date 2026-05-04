import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { EquiposService } from '../../services/equipos.service';
import { Equipo } from '../../models/equipo';

@Component({
  selector: 'app-equipos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './equipos.component.html',
  styleUrl: './equipos.component.css'
})
export class EquiposComponent implements OnInit {
  equipos: Equipo[] = [];

  mensaje = '';
  tipoMensaje: 'info' | 'success' | 'error' = 'info';

  modoEdicion = false;
  equipoEditandoId: number | null = null;

  equipoForm: Equipo = {
    nombre: '',
    codigoInventario: '',
    tipoEquipo: '',
    marca: ''
  };

  constructor(private equiposService: EquiposService) {}

  ngOnInit(): void {
    this.cargarEquipos();
  }

  mostrarMensaje(texto: string, tipo: 'info' | 'success' | 'error'): void {
    this.mensaje = texto;
    this.tipoMensaje = tipo;
  }

  limpiarFormulario(): void {
    this.modoEdicion = false;
    this.equipoEditandoId = null;

    this.equipoForm = {
      nombre: '',
      codigoInventario: '',
      tipoEquipo: '',
      marca: ''
    };
  }

  cargarEquipos(): void {
    this.equiposService.getEquipos().subscribe({
      next: (data: Equipo[]) => {
        this.equipos = data;
      },
      error: () => {
        this.mostrarMensaje('Error al cargar equipos. Revisa que el backend esté encendido.', 'error');
      }
    });
  }

  guardarEquipo(): void {
    if (
      !this.equipoForm.nombre ||
      !this.equipoForm.codigoInventario ||
      !this.equipoForm.tipoEquipo ||
      !this.equipoForm.marca
    ) {
      this.mostrarMensaje('Completa todos los campos del equipo.', 'error');
      return;
    }

    if (this.modoEdicion && this.equipoEditandoId !== null) {
      this.equiposService.actualizarEquipo(this.equipoEditandoId, this.equipoForm).subscribe({
        next: () => {
          this.mostrarMensaje('Equipo actualizado correctamente.', 'success');
          this.limpiarFormulario();
          this.cargarEquipos();
        },
        error: (error: any) => {
          this.mostrarMensaje(error.error || 'Error al actualizar equipo.', 'error');
        }
      });

      return;
    }

    this.equiposService.crearEquipo(this.equipoForm).subscribe({
      next: () => {
        this.mostrarMensaje('Equipo registrado correctamente.', 'success');
        this.limpiarFormulario();
        this.cargarEquipos();
      },
      error: (error: any) => {
        this.mostrarMensaje(error.error || 'Error al guardar equipo.', 'error');
      }
    });
  }

  editarEquipo(equipo: Equipo): void {
    this.modoEdicion = true;
    this.equipoEditandoId = equipo.id ?? null;

    this.equipoForm = {
      nombre: equipo.nombre,
      codigoInventario: equipo.codigoInventario,
      tipoEquipo: equipo.tipoEquipo,
      marca: equipo.marca
    };

    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  eliminarEquipo(id: number): void {
    const confirmar = confirm(
      '¿Seguro que deseas eliminar este equipo?\n\nNo se puede eliminar si tiene un préstamo activo. Si el préstamo ya fue devuelto, sí se puede eliminar.'
    );

    if (!confirmar) {
      return;
    }

    this.equiposService.eliminarEquipo(id).subscribe({
      next: () => {
        this.mostrarMensaje('Equipo eliminado correctamente.', 'success');
        this.cargarEquipos();
      },
      error: (error: any) => {
        this.mostrarMensaje(
          error.error ||
          'No se puede eliminar este equipo porque tiene un préstamo activo. Primero debe devolverse o saldarse el préstamo.',
          'error'
        );
      }
    });
  }
}