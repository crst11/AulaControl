using AulaControl.Api.Data;
using AulaControl.Api.Models;

namespace AulaControl.Api.Seed;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Estudiantes.Any())
        {
            context.Estudiantes.AddRange(
                new Estudiante
                {
                    Nombre = "Cristian Pérez",
                    CodigoEstudiante = "2024001",
                    Correo = "cristian@universidad.edu",
                    Programa = "Ingeniería de Sistemas",
                    Telefono = "3001234567",
                    FechaRegistro = DateTime.Now
                },
                new Estudiante
                {
                    Nombre = "Laura Gómez",
                    CodigoEstudiante = "2024002",
                    Correo = "laura@universidad.edu",
                    Programa = "Ingeniería Industrial",
                    Telefono = "3002223344",
                    FechaRegistro = DateTime.Now
                },
                new Estudiante
                {
                    Nombre = "Andrés Martínez",
                    CodigoEstudiante = "2024003",
                    Correo = "andres@universidad.edu",
                    Programa = "Ingeniería Electrónica",
                    Telefono = "3015556677",
                    FechaRegistro = DateTime.Now
                },
                new Estudiante
                {
                    Nombre = "Sofía Ramírez",
                    CodigoEstudiante = "2024004",
                    Correo = "sofia@universidad.edu",
                    Programa = "Administración de Empresas",
                    Telefono = "3028889911",
                    FechaRegistro = DateTime.Now
                },
                new Estudiante
                {
                    Nombre = "Camilo Torres",
                    CodigoEstudiante = "2024005",
                    Correo = "camilo@universidad.edu",
                    Programa = "Ingeniería Mecatrónica",
                    Telefono = "3104445566",
                    FechaRegistro = DateTime.Now
                },
                new Estudiante
                {
                    Nombre = "Valentina Rojas",
                    CodigoEstudiante = "2024006",
                    Correo = "valentina@universidad.edu",
                    Programa = "Contaduría Pública",
                    Telefono = "3117778899",
                    FechaRegistro = DateTime.Now
                }
            );
        }

        if (!context.Equipos.Any())
        {
            context.Equipos.AddRange(
                new Equipo
                {
                    Nombre = "Kit Arduino Uno",
                    CodigoInventario = "ARD-001",
                    TipoEquipo = "Microcontrolador",
                    Marca = "Arduino",
                    Estado = "Disponible",
                    FechaRegistro = DateTime.Now
                },
                new Equipo
                {
                    Nombre = "Portátil Lenovo ThinkPad",
                    CodigoInventario = "PC-001",
                    TipoEquipo = "Computador portátil",
                    Marca = "Lenovo",
                    Estado = "Disponible",
                    FechaRegistro = DateTime.Now
                },
                new Equipo
                {
                    Nombre = "Proyector Epson X05",
                    CodigoInventario = "PROY-001",
                    TipoEquipo = "Proyector",
                    Marca = "Epson",
                    Estado = "Disponible",
                    FechaRegistro = DateTime.Now
                },
                new Equipo
                {
                    Nombre = "Tablet Samsung Galaxy Tab",
                    CodigoInventario = "TAB-001",
                    TipoEquipo = "Tablet",
                    Marca = "Samsung",
                    Estado = "Disponible",
                    FechaRegistro = DateTime.Now
                },
                new Equipo
                {
                    Nombre = "Multímetro Digital",
                    CodigoInventario = "MULT-001",
                    TipoEquipo = "Instrumento de medición",
                    Marca = "Uni-T",
                    Estado = "Disponible",
                    FechaRegistro = DateTime.Now
                },
                new Equipo
                {
                    Nombre = "Kit Sensores IoT",
                    CodigoInventario = "IOT-001",
                    TipoEquipo = "Kit de sensores",
                    Marca = "Genérico",
                    Estado = "Disponible",
                    FechaRegistro = DateTime.Now
                }
            );
        }

        context.SaveChanges();
    }
}