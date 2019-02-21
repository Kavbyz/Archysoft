import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PublicGuard, ProtectedGuard } from 'ngx-auth';

const routes: Routes = [
  {path:'', pathMatch:'full', redirectTo:'employees'},
  {path:'employees', canActivate:[ProtectedGuard], loadChildren:'./employees/employees.module#EmployeesModule'},
  {path:'auth', canActivate:[ProtectedGuard], loadChildren:'./auth/auth.module#AuthModule'},
  {path:'**', pathMatch:'full', redirectTo:''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
