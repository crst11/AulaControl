export interface Prestamo {
  id?: number;
  estudianteId: number;
  equipoId: number;
  fechaPrestamo?: string;
  fechaDevolucionEstimada: string;
  fechaDevolucionReal?: string;
  estadoPrestamo?: string;
  observacion: string;
  estudiante?: any;
  equipo?: any;
}