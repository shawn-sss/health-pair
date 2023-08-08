import { Component, OnInit } from '@angular/core';
import { Patient, Appointment } from '../models';
import { AuthenticationService, HealthPairService } from '../_services';
import { NgModel } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../_services/dialog.service';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css'],
})
export class AppointmentComponent implements OnInit {
  currentPatient: Patient;
  appointments: Appointment[] = [];
  appointment: Appointment;

  constructor(
    public service: HealthPairService,
    private authenticationService: AuthenticationService,
    private dialog: MatDialog,
    private dialogService: DialogService
  ) {
    this.authenticationService.CurrentPatient.subscribe(
      (x) => (this.currentPatient = x)
    );
  }

  ngOnInit() {
    this.GetAppointmentByCurrentUser();
  }

  GetAppointmentByCurrentUser() {
    if (this.currentPatient) {
      return this.service
        .searchAppointment(this.currentPatient.patientFirstName)
        .subscribe((appointments) => {
          this.appointments = appointments;
          this.appointments = this.appointments.sort((a, b) =>
            a.appointmentDate > b.appointmentDate ? 1 : -1
          );
        });
    }
  }

  CancelAppointment(appointment: Appointment) {
    this.dialogService
      .openConfirmDialog('Are you sure you want to cancel this appointment?')
      .afterClosed()
      .subscribe((res) => {
        if (res) {
          this.appointments = this.appointments.filter(
            (r) => r !== appointment
          );
          return this.service
            .deleteAppointment(appointment.appointmentId)
            .subscribe();
        }
      });
  }
}
