import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import {
  HealthPairService,
  AuthenticationService,
  SearchService,
} from '../_services';
import { Insurance, Patient, Specialty } from '../models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
})
export class LandingPageComponent implements OnInit {
  chosenInsurance: string;
  speciality: string;
  warningText: string;

  specialties: Specialty[];
  insurances: Insurance[];
  currentPatient: Patient;

  loading = true;
  submitted = false;

  landingPageForm = this.builder.group({
    insurance: ['', Validators.required],
    specialty: ['', Validators.required],
  });
  constructor(
    private SearchService: SearchService,
    private router: Router,
    private builder: FormBuilder,
    private HealthPairService: HealthPairService,
    private authenticationService: AuthenticationService
  ) {
    this.currentPatient = this.authenticationService.CurrentPatientValue;
  }

  ngOnInit(): void {
    this.getAll();
    if (this.currentPatient) {
      this.landingPageForm.setValue({
        insurance: this.currentPatient.insuranceName,
        specialty: '',
      });
    }
  }

  get f() {
    return this.landingPageForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.landingPageForm.invalid) {
      return;
    }
    this.loading = true;
    this.SearchService.sharedIns = this.landingPageForm.get('insurance')?.value;
    this.SearchService.sharedSpec =
      this.landingPageForm.get('specialty')?.value;
    this.router.navigate(['/provider-selection']);
    this.loading = false;
  }

  getAll() {
    this.HealthPairService.getInsuranceAll().subscribe((insurances) => {
      this.insurances = insurances;
      if (this.currentPatient != null) {
        this.insurances = this.insurances.filter(
          (p) => p.insuranceName !== this.currentPatient.insuranceName
        );
      }
      this.insurances = this.insurances.sort((a, b) =>
        a.insuranceName > b.insuranceName ? 1 : -1
      );
    });
    this.HealthPairService.getSpecialtyAll().subscribe((specialties) => {
      this.specialties = specialties;
      this.specialties = this.specialties.sort((a, b) =>
        a.specialty > b.specialty ? 1 : -1
      );
    });
    setTimeout(() => {
      setTimeout(() => {}, 1000);
      if (
        this.insurances != undefined &&
        this.specialties != undefined &&
        this.insurances.length > 0 &&
        this.specialties.length > 0
      ) {
        this.loading = false;
      }
      if (this.loading == true) {
        this.warningText =
          'Load is taking longer than usual. There may be a problem connecting with the database.';
        console.log('retrying..');
        this.getAll();
      }
    }, 2000);
  }
}
