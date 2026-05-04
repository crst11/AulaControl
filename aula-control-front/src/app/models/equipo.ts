export interface Equipo {
  id?: number;
  nombre: string;
  codigoInventario: string;
  tipoEquipo: string;
  marca: string;
  estado?: string;
  fechaRegistro?: string;
}