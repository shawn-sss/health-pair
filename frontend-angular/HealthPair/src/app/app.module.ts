import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { ProviderSelectionComponent } from './provider-selection/provider-selection.component';
import { AppointmentDetailsComponent } from './appointment-details/appointment-details.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProviderComponent } from './provider/provider.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { FacilityComponent } from './facility/facility.component';
import { FacilityDetailsComponent } from './facility-details/facility-details.component';
import { InsuranceComponent } from './insurance/insurance.component';
import { InsuranceDetailsComponent } from './insurance-details/insurance-details.component';
import { AlertComponent } from './alert/alert.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { HttpClient } from '@angular/common/http';
import { AgmCoreModule } from '@agm/core';
import { google } from '@agm/core/services/google-maps-types';

import { JwtInterceptor, ErrorInterceptor } from './authentification';
import { SpecialtyComponent } from './specialty/specialty.component';
import { LogoutComponent } from './logout/logout.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ProviderSelectionComponent,
    AppointmentDetailsComponent,
    LandingPageComponent,
    ProviderComponent,
    NavbarComponent,
    RegisterComponent,
    LoginComponent,
    AppointmentComponent,
    FacilityComponent,
    FacilityDetailsComponent,
    InsuranceComponent,
    InsuranceDetailsComponent,
    AlertComponent,
    SpecialtyComponent,
    LogoutComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FontAwesomeModule,
    MDBBootstrapModule.forRoot(),
    BrowserAnimationsModule,
    BrowserModule,
    MaterialModule,
    HttpClientModule,
    //
    HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmDialogComponent],
})
export class AppModule {}
