import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

import { Patient } from '../models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private CurrentPatientSubject: BehaviorSubject<Patient>;
  public CurrentPatient: Observable<Patient>;
  private baseUrl = environment.healthPairApiBaseUrl;

  constructor(private http: HttpClient) {
    this.CurrentPatientSubject = new BehaviorSubject<Patient>(
      JSON.parse(localStorage.getItem('CurrentPatient'))
    );
    this.CurrentPatient = this.CurrentPatientSubject.asObservable();
  }

  public get CurrentPatientValue(): any {
    return this.CurrentPatientSubject.value;
  }

  login(email, password) {
    return this.http
      .post<any>(`${this.baseUrl}api/patient/authenticate`, { email, password })
      .pipe(
        map((patient) => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('CurrentPatient', JSON.stringify(patient));
          this.CurrentPatientSubject.next(patient);
          return patient;
        })
      );
  }

  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('CurrentPatient');
    this.CurrentPatientSubject.next(null);
  }
}
