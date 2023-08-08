import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { ProviderSelectionComponent } from './provider-selection/provider-selection.component';
import { AppointmentDetailsComponent } from './appointment-details/appointment-details.component';
import { FacilityComponent } from './facility/facility.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { InsuranceComponent } from './insurance/insurance.component';
import { SpecialtyComponent } from './specialty/specialty.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { LogoutComponent } from './logout/logout.component';
import { AuthGuard } from './authentification';

const routes: Routes = [
  { path: '', redirectTo: '/landing-page', pathMatch: 'full' },
  { path: 'landing-page', component: LandingPageComponent },
  { path: 'provider-selection', component: ProviderSelectionComponent },
  {
    path: 'appointment-details/:id',
    component: AppointmentDetailsComponent,
    canActivate: [AuthGuard],
  },
  { path: 'facility', component: FacilityComponent, canActivate: [AuthGuard] },
  {
    path: 'insurance',
    component: InsuranceComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'specialty',
    component: SpecialtyComponent,
    canActivate: [AuthGuard],
  },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'appointment',
    component: AppointmentComponent,
    canActivate: [AuthGuard],
  },

  // otherwise redirect to home
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
