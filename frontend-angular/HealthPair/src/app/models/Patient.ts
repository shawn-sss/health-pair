export class Patient {
  patientId: number;
  patientFirstName: string;
  patientLastName: string;
  patientPassword: string;
  patientAddress1: string;
  patientCity: string;
  patientState: string;
  patientZipcode: number;
  patientBirthDay: Date;
  patientPhoneNumber: number;
  patientEmail: string;
  isAdmin: boolean;
  token: string;

  insuranceId: number;
  insuranceName: string;
}
