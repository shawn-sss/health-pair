import { Component, OnInit } from '@angular/core';
import { HealthPairService, AuthenticationService } from '../_services';
import { Insurance, Patient } from '../models';

@Component({
  selector: 'app-insurance',
  templateUrl: './insurance.component.html',
  styleUrls: ['./insurance.component.css'],
})
export class InsuranceComponent implements OnInit {
  currentPatient: Patient;
  insurance: Insurance;
  myInsurances: Insurance[];
  createdMessage: string;
  highest: number;

  constructor(
    private HealthPairService: HealthPairService,
    private authenticationService: AuthenticationService
  ) {
    this.currentPatient = this.authenticationService.CurrentPatientValue;
    this.getHighestInsuranceId();
    this.myInsurances = [];
  }

  ngOnInit() {
    this.getHighestInsuranceId();
  }

  add(InsuranceName: string): void {
    let insurance = new Insurance();
    {
      insurance.insuranceName = InsuranceName;
    }
    this.HealthPairService.createInsurance(insurance).subscribe(
      (myInsurance) => {
        this.myInsurances.push(myInsurance);
      }
    );

    this.createdMessage = 'Your insurance has been created!';
  }

  delete(insurance: Insurance): void {
    this.myInsurances = this.myInsurances.filter((r) => r !== insurance);
    this.HealthPairService.deleteFacility(insurance.insuranceId).subscribe();
  }

  getHighestInsuranceId() {
    this.HealthPairService.getInsuranceAll().subscribe((insurances) => {
      (this.myInsurances = insurances),
        (this.highest =
          this.myInsurances[this.myInsurances.length - 1].insuranceId);
    });
  }
}
