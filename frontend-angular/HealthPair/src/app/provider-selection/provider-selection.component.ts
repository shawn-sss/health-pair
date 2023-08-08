import { Component, OnInit } from '@angular/core';
import {
  HealthPairService,
  AuthenticationService,
  SearchService,
  UserLocationService,
} from '../_services';
import { Provider, Patient } from '../models';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-provider-selection',
  templateUrl: './provider-selection.component.html',
  styleUrls: ['./provider-selection.component.css'],
})
export class ProviderSelectionComponent implements OnInit {
  imgurl = 'https://cdn4.iconfinder.com/data/icons/linecon/512/photo-512.png';

  initialProviders: Provider[];
  finalProviders: Provider[];
  currentPatient: Patient;

  yourLocation: string;
  destinationLocation: string;
  finalDistance: string;

  stringIns: string;
  currentProviderCount: number = 0;

  constructor(
    private APIService: HealthPairService,
    public SearchService: SearchService,
    private locationService: UserLocationService,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private authenticationService: AuthenticationService
  ) {
    this.currentPatient = this.authenticationService.CurrentPatientValue;
  }
  ngOnInit(): void {
    this.initialProviders = [];
    this.finalProviders = [];
    if (this.SearchService.sharedIns) {
      this.getInsuranceByName().then((myID) => this.getAll(myID));
    }
    if (this.SearchService.sharedIns == undefined) {
      this.router.navigateByUrl('/landing-page');
    }
  }

  getInsuranceByName() {
    return this.APIService.searchInsurance(this.SearchService.sharedIns)
      .toPromise()
      .then((insurance) => {
        return insurance[0].insuranceId;
      });
  }

  getAll(id: number) {
    this.APIService.getProviderAll().subscribe(async (providers) => {
      this.initialProviders = providers;
      for (var i: number = 0; i < this.initialProviders.length; i++) {
        if (
          this.initialProviders[i].insuranceIds.includes(id) &&
          this.initialProviders[i].specialty.includes(
            this.SearchService.sharedSpec
          )
        ) {
          this.initialProviders[i].distance = 0;
          this.finalProviders.push(this.initialProviders[i]);
        }
      }
      for (var q: number = 0; q < this.finalProviders.length; q++) {
        this.setDistance(q);
      }
    });
  }

  async setDistance(q: number) {
    this.finalProviders[q].distance = await Promise.resolve(
      this.calculateDistance(this.finalProviders[q])
    );
    this.finalProviders = this.finalProviders.sort((a, b) =>
      a.distance > b.distance ? 1 : -1
    );
  }

  getCurrentLocation() {
    this.yourLocation = 'Your Location: Calculating...';
    return this.locationService.getMyPosition().then((pos) => {
      const myReturn = [pos.lng, pos.lat];
      this.yourLocation =
        'Your Location: Longitude: ' +
        myReturn[0] +
        ' Latitude: ' +
        myReturn[1];
      return myReturn;
    });
  }

  getTargetLocation(address: string, city: string, state: string) {
    this.destinationLocation = 'Destination: Calculating...';
    return this.locationService
      .getLocationCoords(address, city, state)
      .toPromise()
      .then((coords) => {
        const myReturn = [
          coords.results[0].geometry.location.lng,
          coords.results[0].geometry.location.lat,
        ];
        this.destinationLocation =
          'Destination: Longitude: ' +
          myReturn[0] +
          ' Latitude: ' +
          myReturn[1];
        return myReturn;
      });
  }

  calculateDistance(provider: Provider) {
    const promises = [
      this.getCurrentLocation(),
      this.getTargetLocation(
        provider.facilityAddress1,
        provider.facilityCity,
        provider.facilityState
      ),
    ];
    this.finalDistance = 'Distance: Calculating...';
    return Promise.all(promises).then((locations) => {
      const lon1 = locations[0][0];
      const lat1 = locations[0][1];
      const lon2 = locations[1][0];
      const lat2 = locations[1][1];
      const p = 0.017453292519943295; // Math.PI / 180
      const c = Math.cos;
      const a =
        0.5 -
        c((lat2 - lat1) * p) / 2 +
        (c(lat1 * p) * c(lat2 * p) * (1 - c((lon2 - lon1) * p))) / 2;
      this.finalDistance =
        'Distance: ' + Math.round(12742 * Math.asin(Math.sqrt(a))) + ' KMs.';
      return Math.round(12742 * Math.asin(Math.sqrt(a))); // 2 * R; R = 6371 km
    });
  }

  goBack(): void {
    this.location.back();
  }
}
