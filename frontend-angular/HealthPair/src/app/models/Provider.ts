export class Provider {
  providerId: number;
  providerFirstName: string;
  providerLastName: string;
  providerPhoneNumber: number;

  facilityId: number;
  facilityAddress1: string;
  facilityName: string;
  facilityCity: string;
  facilityState: string;
  facilityPhoneNumber: number;

  specialtyId: number;
  specialty: string;

  insuranceIds: number[];

  distance: number = 0;
}
