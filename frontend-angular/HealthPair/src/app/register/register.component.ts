import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import {
  AlertService,
  HealthPairService,
  AuthenticationService,
} from '../_services';
import { Insurance, Patient } from '../models';

@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  insurances: Insurance[];
  chosenInsurance: string;
  myInsurance: Insurance;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private HealthPairService: HealthPairService,
    private alertService: AlertService,
    private location: Location
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.CurrentPatientValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      PatientFirstName: ['', Validators.required],
      PatientLastName: ['', Validators.required],
      PatientInsurance: ['', Validators.required],
      PatientAddress1: ['', Validators.required],
      PatientCity: ['', Validators.required],
      PatientState: ['', Validators.required],
      PatientZipcode: ['', Validators.required],
      PatientBirthDay: ['', Validators.required],
      PatientPhoneNumber: ['', Validators.required],
      PatientEmail: ['', Validators.required],
      PatientPassword: ['', [Validators.required, Validators.minLength(6)]],
    });

    this.getAll();
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.registerForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    this.loading = true;
    this.chosenInsurance = this.registerForm.get('PatientInsurance')?.value;

    this.HealthPairService.searchInsurance(this.chosenInsurance).subscribe(
      (insurance) => {
        this.myInsurance = insurance[0];
        var myReturnObject: Patient = new Patient();

        {
          (myReturnObject.insuranceId = this.myInsurance.insuranceId),
            (myReturnObject.insuranceName = this.myInsurance.insuranceName),
            (myReturnObject.patientId = 0),
            (myReturnObject.patientFirstName =
              this.registerForm.value.PatientFirstName),
            (myReturnObject.patientLastName =
              this.registerForm.value.PatientLastName),
            (myReturnObject.patientAddress1 =
              this.registerForm.value.PatientAddress1),
            (myReturnObject.patientBirthDay =
              this.registerForm.value.PatientBirthDay),
            (myReturnObject.patientCity = this.registerForm.value.PatientCity),
            (myReturnObject.patientEmail =
              this.registerForm.value.PatientEmail),
            (myReturnObject.patientPassword =
              this.registerForm.value.PatientPassword),
            (myReturnObject.patientPhoneNumber =
              this.registerForm.value.PatientPhoneNumber),
            (myReturnObject.patientState =
              this.registerForm.value.PatientState),
            (myReturnObject.patientZipcode =
              this.registerForm.value.PatientZipcode);
        }
        this.HealthPairService.createPatient(myReturnObject)
          .pipe(first())
          .subscribe(
            (data) => {
              this.alertService.success('Registration successful', true);
              this.authenticationService
                .login(this.f.PatientEmail.value, this.f.PatientPassword.value)
                .pipe(first())
                .subscribe(
                  (data) => {
                    this.location.back();
                  },
                  (error) => {
                    this.alertService.error(error);
                    this.loading = false;
                  }
                );
            },
            (error) => {
              this.alertService.error(error);
              this.loading = false;
            }
          );
      }
    );
    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }
  }

  getAll() {
    this.HealthPairService.getInsuranceAll().subscribe((insurances) => {
      this.insurances = insurances;
      this.insurances = this.insurances.sort((a, b) =>
        a.insuranceName > b.insuranceName ? 1 : -1
      );
    });
  }
}
