import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';

import {
  HealthPairService,
  AuthenticationService,
  DialogService,
  AlertService,
} from '../_services';
import { Patient, Provider, Appointment } from '../models';

import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-appointment-details',
  templateUrl: './appointment-details.component.html',
  styleUrls: ['./appointment-details.component.css'],
})
export class AppointmentDetailsComponent implements OnInit {
  currentProvider: Provider;
  currentPatient: Patient;
  appointmentForm: FormGroup;
  loading = false;
  submitted = false;
  responseText: string;

  title = 'test';
  date = new FormControl(new Date());
  time = { hour: 12, minute: 0, second: 0 };
  meridian = true;
  toggleMeridian() {
    this.meridian = !this.meridian;
  }

  constructor(
    private HealthPairService: HealthPairService,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private dialog: MatDialog,
    private dialogService: DialogService,
    private alertService: AlertService
  ) {
    this.currentPatient = this.authenticationService.CurrentPatientValue;
  }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');

    this.HealthPairService.getProviderById(id).subscribe((prov) => {
      this.currentProvider = prov;
      if (this.currentProvider == undefined) {
        this.router.navigateByUrl('');
      }
    });
  }

  onSubmit(date: Date, time: any) {
    this.dialogService
      .openConfirmDialog('Are you sure you want to schedule this appointment?')
      .afterClosed()
      .subscribe((res) => {
        if (res) {
          console.log(time.hour);
          if (time.hour > 17 || time.hour < 7) {
            this.alertService.error(
              'The office is only open between 7am and 6pm!'
            );
            return;
          }
          if (date < new Date()) {
            this.alertService.error('That date is in the past!');
            return;
          }
          if (!this.checkDate(date)) {
            this.alertService.error('That date is too far in the future!');
            return;
          }

          var craftedDate = new Date();
          craftedDate.setFullYear(date.getFullYear());
          craftedDate.setMonth(date.getMonth());
          craftedDate.setDate(date.getDate());
          craftedDate.setHours(time.hour - 5);
          craftedDate.setMinutes(time.minute);
          craftedDate.setSeconds(time.second);

          var myAppointment = new Appointment();
          {
            myAppointment.appointmentId = 0;
            myAppointment.appointmentDate = craftedDate;
            myAppointment.patientId = this.currentPatient.patientId;
            myAppointment.providerId = this.currentProvider.providerId;
          }
          this.HealthPairService.createAppointment(myAppointment).subscribe(
            (variable) => {
              console.log(variable);
              this.responseText =
                'Appointment Successfully Created! Redirecting...';
              setTimeout(() => {
                this.router.navigateByUrl('/appointment');
              }, 1000);
            }
          );
        }
      });
  }

  goBack(): void {
    this.location.back();
  }

  checkDate(myDate: Date): boolean {
    var checker = new Date();
    if (checker < this.sixMonthsPrior(myDate)) {
      return false;
    }
    return true;
  }

  sixMonthsPrior(date) {
    var d = new Date(date);
    var m = d.getMonth();
    d.setMonth(d.getMonth() - 6);

    var diff = (m + 12 - d.getMonth()) % 12;
    if (diff < 6) {
      d.setDate(0);
    }

    return d;
  }
}
