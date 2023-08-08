import { Component, OnInit } from '@angular/core';
import { HealthPairService, AuthenticationService } from '../_services';
import { Specialty, Patient } from '../models';

@Component({
  selector: 'app-specialty',
  templateUrl: './specialty.component.html',
  styleUrls: ['./specialty.component.css'],
})
export class SpecialtyComponent implements OnInit {
  currentPatient: Patient;
  specialtyName: Specialty;
  mySpecialties: Specialty[];
  createdMessage: string;
  highest: number;

  constructor(
    private HealthPairService: HealthPairService,
    private authenticationService: AuthenticationService
  ) {
    this.currentPatient = this.authenticationService.CurrentPatientValue;
    this.getHighestSpecialtyId();
    this.mySpecialties = [];
  }

  ngOnInit() {
    this.getHighestSpecialtyId();
  }

  AddSpecialty(specialtyName: string) {
    let newSpecialty = new Specialty();
    {
      newSpecialty.specialty = specialtyName;
    }
    this.HealthPairService.createSpecialty(newSpecialty).subscribe(
      (mySpecialty) => {
        this.mySpecialties.push(mySpecialty);
      }
    );

    this.createdMessage = 'Your specialty has been created!';
  }

  delete(specialty: Specialty) {
    this.mySpecialties = this.mySpecialties.filter((r) => r !== specialty);
    this.HealthPairService.deleteFacility(specialty.specialtyId).subscribe();
  }
  getHighestSpecialtyId() {
    this.HealthPairService.getSpecialtyAll().subscribe((specialties) => {
      (this.mySpecialties = specialties),
        (this.highest =
          this.mySpecialties[this.mySpecialties.length - 1].specialtyId);
    });
  }
}
