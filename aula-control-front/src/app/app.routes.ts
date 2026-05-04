import { Routes } from '@angular/router';

import { LayoutComponent } from './pages/layout/layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { EstudiantesComponent } from './pages/estudiantes/estudiantes.component';
import { EquiposComponent } from './pages/equipos/equipos.component';
import { PrestamosComponent } from './pages/prestamos/prestamos.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'estudiantes', component: EstudiantesComponent },
      { path: 'equipos', component: EquiposComponent },
      { path: 'prestamos', component: PrestamosComponent }
    ]
  }
];