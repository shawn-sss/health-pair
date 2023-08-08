import { catchError, map, tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';

import {
  Appointment,
  Facility,
  Insurance,
  Patient,
  Provider,
  Specialty,
} from '../models';
import { AlertService } from '../_services/alert.service';

@Injectable({
  providedIn: 'root',
})
export class HealthPairService {
  private baseUrl = environment.healthPairApiBaseUrl;

  get defaultUserId() {
    return 0;
  }

  constructor(private http: HttpClient, private alertService: AlertService) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    }),
  };

  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~
  // ! ***** APPOINTMENT ********
  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~

  /**
   * Sends a requests for all appointments in the database.
   *
   * @example
   * Simply call the function:
   * getAppointmentAll()
   *
   * @returns An Observable with an action result, and all of the Appointments in the database
   */
  getAppointmentAll(): Observable<Appointment[]> {
    return this.http
      .get<Appointment[]>(`${this.baseUrl}api/Appointment`)
      .pipe(
        catchError(this.handleError<Appointment[]>(`getAppointmentAll`, []))
      );
  }

  /**
   * Sends a request to the server for a specific Appointment related to an ID
   *
   * @example
   * Simply call the function with an input ID:
   * getAppointmentById(1)
   *
   * @param {number} id The ID that you are searching for
   * @returns An Observable with an action result, and a single Appointment from the database
   */
  getAppointmentById(id: number): Observable<Appointment> {
    return this.http
      .get<Appointment>(`${this.baseUrl}api/Appointment/${id}`)
      .pipe(catchError(this.handleError<Appointment>(`getAppointmentById`)));
  }

  /**
   * Sends a request to the server for a specific Appointment related to an input string
   *
   * @example
   * Simply call the function with an input string:
   * searchAppointment('Name')
   *
   * @param {string} term The string that you are searching for
   * @returns An Observable with an action result from the server, and any Appointments from the database that contain your input string
   */
  searchAppointment(term: string): Observable<Appointment[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.http
      .get<Appointment[]>(`${this.baseUrl}api/Appointment?search=${term}`)
      .pipe(
        catchError(this.handleError<Appointment[]>('searchAppointment', []))
      );
  }

  /**
   * Sends a request to the server to create a Appointment
   *
   * @example
   * Call the function with an input Appointment object:
   * createAppointment(myObject)
   *
   * @param {Appointment} appointment The Appointment object that you are trying to add to the database
   * @returns An Observable with the action result from the server, and a copy of your created Appointment
   */
  createAppointment(appointment: Appointment): Observable<Appointment> {
    return this.http
      .post<Appointment>(`${this.baseUrl}api/Appointment`, appointment)
      .pipe(catchError(this.handleError<Appointment>(`createAppointment`)));
  }

  /**
   * Sends a request to the server to update a specific Appointment, it uses the ID of your input object.
   *
   * @example
   * Call the function with an input Appointment object:
   * updateAppointment(myObject)
   *
   * @param {Appointment} appointment The Object with the new values that you are trying to update
   * @returns An Observable with a action result from the server, and the updated Appointment from the database
   */
  updateAppointment(appointment: Appointment): Observable<any> {
    return this.http
      .put(
        `${this.baseUrl}api/Appointment/${appointment.appointmentId}`,
        appointment,
        this.httpOptions
      )
      .pipe(catchError(this.handleError<Appointment>('updateAppointment')));
  }

  /**
   * Sends a request to the server to delete a specific Appointment related to the input ID.
   *
   * @example
   * Simply call the function with an input id:
   * deleteAppointment(1)
   *
   * @param {number} id The ID that you are trying to delete
   * @returns An Observable with a action result from the server
   */
  deleteAppointment(id: number) {
    return this.http
      .delete(`${this.baseUrl}api/Appointment/${id}`)
      .pipe(
        catchError(this.handleError<Appointment[]>(`deleteAppointment`, []))
      );
  }

  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~
  // ! ******* FACILITY *********
  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~

  /**
   * Sends a requests for all facilities in the database.
   *
   * @example
   * Simply call the function:
   * getFacilityAll()
   *
   * @returns An Observable with an action result, and all of the Facilities in the database
   */
  getFacilityAll(): Observable<Facility[]> {
    return this.http
      .get<Facility[]>(`${this.baseUrl}api/Facility`)
      .pipe(catchError(this.handleError<Facility[]>(`getFacilityAll`, [])));
  }

  /**
   * Sends a request to the server for a specific Facility related to an ID
   *
   * @example
   * Simply call the function with an input ID:
   * getFacilityById(1)
   *
   * @param {number} id The ID that you are searching for
   * @returns An Observable with an action result, and a single Facility from the database
   */
  getFacilityById(id: number): Observable<Facility> {
    return this.http
      .get<Facility>(`${this.baseUrl}api/Facility/${id}`)
      .pipe(catchError(this.handleError<Facility>(`getFacilityById`)));
  }

  /**
   * Sends a request to the server for a specific Facility related to an input string
   *
   * @example
   * Simply call the function with an input string:
   * searchFacility('Name')
   *
   * @param {string} term The string that you are searching for
   * @returns An Observable with an action result from the server, and any Facilities from the database that contain your input string
   */
  searchFacility(term: string): Observable<Facility[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.http
      .get<Facility[]>(`${this.baseUrl}api/Facility?search=${term}`)
      .pipe(catchError(this.handleError<Facility[]>('searchFacility', [])));
  }

  /**
   * Sends a request to the server to create a Facility
   *
   * @example
   * Call the function with an input Facility object:
   * createFacility(myObject)
   *
   * @param {Facility} facility The Facility object that you are trying to add to the database
   * @returns An Observable with the action result from the server, and a copy of your created Facility
   */
  createFacility(facility: Facility): Observable<Facility> {
    return this.http
      .post<Facility>(`${this.baseUrl}api/Facility`, facility)
      .pipe(catchError(this.handleError<Facility>(`createFacility`)));
  }

  /**
   * Sends a request to the server to update a specific Facility, it uses the ID of your input object.
   *
   * @example
   * Call the function with an input Facility object:
   * updateFacility(myObject)
   *
   * @param {Facility} facility The Object with the new values that you are trying to update
   * @returns An Observable with a action result from the server, and the updated Facility from the database
   */
  updateFacility(facility: Facility): Observable<any> {
    return this.http
      .put(
        `${this.baseUrl}api/Facility/${facility.facilityId}`,
        facility,
        this.httpOptions
      )
      .pipe(catchError(this.handleError<Facility>('updateFacility')));
  }

  /**
   * Sends a request to the server to delete a specific Facility related to the input ID.
   *
   * @example
   * Simply call the function with an input id:
   * deleteFacility(1)
   *
   * @param {number} id The ID that you are trying to delete
   * @returns An Observable with a action result from the server
   */
  deleteFacility(id: number) {
    return this.http
      .delete(`${this.baseUrl}api/Facility/${id}`)
      .pipe(catchError(this.handleError<Facility[]>(`deleteFacility`, [])));
  }

  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~
  // ! ******* INSURANCE ********
  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~

  /**
   * Sends a requests for all insurances in the database.
   *
   * @example
   * Simply call the function:
   * getInsuranceAll()
   *
   * @returns An Observable with and action result, and all of the Insurances in the database
   */
  getInsuranceAll(): Observable<Insurance[]> {
    return this.http
      .get<Insurance[]>(`${this.baseUrl}api/Insurance`)
      .pipe(catchError(this.handleError<Insurance[]>(`getInsuranceAll`, [])));
  }

  /**
   * Sends a request to the server for a specific Insurance related to an ID
   *
   * @example
   * Simply call the function with an input ID:
   * getInsuranceById(1)
   *
   * @param {number} id The ID that you are searching for
   * @returns An Observable with an action result, and a single Insurance from the database
   */
  getInsuranceById(id: number): Observable<Insurance> {
    return this.http
      .get<Insurance>(`${this.baseUrl}api/Insurance/${id}`)
      .pipe(catchError(this.handleError<Insurance>(`getInsuranceById`)));
  }

  /**
   * Sends a request to the server for a specific Insurance related to an input string
   *
   * @example
   * Simply call the function with an input string:
   * searchInsurance('Name')
   *
   * @param {string} term The string that you are searching for
   * @returns An Observable with an action result from the server, and any Insurances from the database that contain your input string
   */
  searchInsurance(term: string): Observable<Insurance[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.http
      .get<Insurance[]>(`${this.baseUrl}api/Insurance?search=${term}`)
      .pipe(catchError(this.handleError<Insurance[]>('searchInsurance', [])));
  }

  /**
   * Sends a request to the server to create a Insurance
   *
   * @example
   * Call the function with an input Insurance object:
   * createInsurance(myObject)
   *
   * @param {Insurance} insurance The Insurance object that you are trying to add to the database
   * @returns An Observable with the action result from the server, and a copy of your created Insurance
   */
  createInsurance(insurance: Insurance): Observable<Insurance> {
    return this.http
      .post<Insurance>(`${this.baseUrl}api/Insurance`, insurance)
      .pipe(catchError(this.handleError<Insurance>(`createInsurance`)));
  }

  /**
   * Sends a request to the server to update a specific Insurance, it uses the ID of your input object.
   *
   * @example
   * Call the function with an input Insurance object:
   * updateInsurance(myObject)
   *
   * @param {Insurance} insurance The Object with the new values that you are trying to update
   * @returns An Observable with a action result from the server, and the updated Insurance from the database
   */
  updateInsurance(insurance: Insurance): Observable<any> {
    return this.http
      .put(
        `${this.baseUrl}api/Insurance/${insurance.insuranceId}`,
        insurance,
        this.httpOptions
      )
      .pipe(catchError(this.handleError<Insurance>('updateInsurance')));
  }

  /**
   * Sends a request to the server to delete a specific Insurance related to the input ID.
   *
   * @example
   * Simply call the function with an input id:
   * deleteInsurance(1)
   *
   * @param {number} id The ID that you are trying to delete
   * @returns An Observable with a action result from the server
   */
  deleteInsurance(id: number) {
    return this.http
      .delete(`${this.baseUrl}api/Insurance/${id}`)
      .pipe(catchError(this.handleError<Insurance[]>(`deleteInsurance`, [])));
  }

  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~
  // ! ******* PATIENT *********
  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~

  /**
   * Sends a requests for all patients in the database.
   *
   * @example
   * Simply call the function:
   * getPatientAll()
   *
   * @returns An Observable with and action result, and all of the Patients in the database
   */
  getPatientAll(): Observable<Patient[]> {
    return this.http
      .get<Patient[]>(`${this.baseUrl}api/Patient`)
      .pipe(catchError(this.handleError<Patient[]>(`getPatientAll`, [])));
  }

  /**
   * Sends a request to the server for a specific Patient related to an ID
   *
   * @example
   * Simply call the function with an input ID:
   * getPatientById(1)
   *
   * @param {number} id The ID that you are searching for
   * @returns An Observable with an action result, and a single Patient from the database
   */
  getPatientById(id: number): Observable<Patient> {
    return this.http
      .get<Patient>(`${this.baseUrl}api/Patient/${id}`)
      .pipe(catchError(this.handleError<Patient>(`getPatientById`)));
  }

  /**
   * Sends a request to the server for a specific Patient related to an input string
   *
   * @example
   * Simply call the function with an input string:
   * searchPatient('Name')
   *
   * @param {string} term The string that you are searching for
   * @returns An Observable with an action result from the server, and any Patients from the database that contain your input string
   */
  searchPatient(term: string): Observable<Patient[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.http
      .get<Patient[]>(`${this.baseUrl}api/Patient?search=${term}`)
      .pipe(catchError(this.handleError<Patient[]>('searchPatient', [])));
  }

  /**
   * Sends a request to the server to create a Patient
   *
   * @example
   * Call the function with an input Patient object:
   * createPatient(myObject)
   *
   * @param {Patient} patient The Patient object that you are trying to add to the database
   * @returns An Observable with the action result from the server, and a copy of your created Patient
   */
  createPatient(patient: Patient): Observable<Patient> {
    return this.http
      .post<Patient>(`${this.baseUrl}api/Patient`, patient, this.httpOptions)
      .pipe(catchError(this.handleError<Patient>(`createPatient`)));
  }

  /**
   * Sends a request to the server to update a specific Patient, it uses the ID of your input object.
   *
   * @example
   * Call the function with an input Patient object:
   * updatePatient(myObject)
   *
   * @param {Patient} patient The Object with the new values that you are trying to update
   * @returns An Observable with a action result from the server, and the updated Patient from the database
   */
  updatePatient(patient: Patient): Observable<any> {
    return this.http
      .put(
        `${this.baseUrl}api/Patient/${patient.patientId}`,
        patient,
        this.httpOptions
      )
      .pipe(catchError(this.handleError<Patient>('updatePatient')));
  }

  /**
   * Sends a request to the server to delete a specific Patient related to the input ID.
   *
   * @example
   * Simply call the function with an input id:
   * deletePatient(1)
   *
   * @param {number} id The ID that you are trying to delete
   * @returns An Observable with a action result from the server
   */
  deletePatient(id: number) {
    return this.http
      .delete(`${this.baseUrl}api/Patient/${id}`)
      .pipe(catchError(this.handleError<Patient[]>(`deletePatient`, [])));
  }

  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~
  // ! ******* PROVIDER *********
  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~

  /**
   * Sends a requests for all providers in the database.
   *
   * @example
   * Simply call the function:
   * getProviderAll()
   *
   * @returns An Observable with and action result, and all of the Providers in the database
   */
  getProviderAll(): Observable<Provider[]> {
    return this.http
      .get<Provider[]>(`${this.baseUrl}api/Provider`)
      .pipe(catchError(this.handleError<Provider[]>(`getProviderAll`, [])));
  }

  /**
   * Sends a request to the server for a specific Provider related to an ID
   *
   * @example
   * Simply call the function with an input ID:
   * getProviderById(1)
   *
   * @param {number} id The ID that you are searching for
   * @returns An Observable with an action result, and a single Provider from the database
   */
  getProviderById(id: number): Observable<Provider> {
    return this.http
      .get<Provider>(`${this.baseUrl}api/Provider/${id}`)
      .pipe(catchError(this.handleError<Provider>(`getProviderById`)));
  }

  /**
   * Sends a request to the server for a specific Provider related to an input string
   *
   * @example
   * Simply call the function with an input string:
   * searchProvider('Name')
   *
   * @param {string} term The string that you are searching for
   * @returns An Observable with an action result from the server, and any Providers from the database that contain your input string
   */
  searchProvider(term: string): Observable<Provider[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.http
      .get<Provider[]>(`${this.baseUrl}api/Provider?search=${term}`)
      .pipe(catchError(this.handleError<Provider[]>('searchProvider', [])));
  }

  /**
   * Sends a request to the server to create a Provider
   *
   * @example
   * Call the function with an input Provider object:
   * createProvider(myObject)
   *
   * @param {Provider} provider The Provider object that you are trying to add to the database
   * @returns An Observable with the action result from the server, and a copy of your created Provider
   */
  createProvider(provider: Provider): Observable<Provider> {
    return this.http
      .post<Provider>(`${this.baseUrl}api/Provider`, provider)
      .pipe(catchError(this.handleError<Provider>(`createProvider`)));
  }

  /**
   * Sends a request to the server to update a specific Provider, it uses the ID of your input object.
   *
   * @example
   * Call the function with an input Provider object:
   * updateProvider(myObject)
   *
   * @param {Provider} provider The Object with the new values that you are trying to update
   * @returns An Observable with a action result from the server, and the updated Provider from the database
   */
  updateProvider(provider: Provider): Observable<any> {
    return this.http
      .put(
        `${this.baseUrl}api/Provider/${provider.providerId}`,
        provider,
        this.httpOptions
      )
      .pipe(catchError(this.handleError<Provider>('updateProvider')));
  }

  /**
   * Sends a request to the server to delete a specific Provider related to the input ID.
   *
   * @example
   * Simply call the function with an input id:
   * deleteProvider(1)
   *
   * @param {number} id The ID that you are trying to delete
   * @returns An Observable with a action result from the server
   */
  deleteProvider(id: number) {
    return this.http
      .delete(`${this.baseUrl}api/Provider/${id}`)
      .pipe(catchError(this.handleError<Provider[]>(`deleteProvider`, [])));
  }

  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~
  // ! ******* SPECIALTY *********
  // ! ~~~~~~~~~~~~~~~~~~~~~~~~~~

  /**
   * Sends a requests for all specialties in the database.
   *
   * @example
   * Simply call the function:
   * getSpecialtyAll()
   *
   * @returns An Observable with and action result, and all of the Specialties in the database
   */
  getSpecialtyAll(): Observable<Specialty[]> {
    return this.http
      .get<Specialty[]>(`${this.baseUrl}api/Specialty`)
      .pipe(catchError(this.handleError<Specialty[]>(`getSpecialtyAll`, [])));
  }

  /**
   * Sends a request to the server for a specific Specialty related to an ID
   *
   * @example
   * Simply call the function with an input ID:
   * getSpecialtyById(1)
   *
   * @param {number} id The ID that you are searching for
   * @returns An Observable with an action result, and a single Specialty from the database
   */
  getSpecialtyById(id: number): Observable<Specialty> {
    return this.http
      .get<Specialty>(`${this.baseUrl}api/Specialty/${id}`)
      .pipe(catchError(this.handleError<Specialty>(`getSpecialtyById`)));
  }

  /**
   * Sends a request to the server for a specific Specialty related to an input string
   *
   * @example
   * Simply call the function with an input string:
   * searchSpecialty('Name')
   *
   * @param {string} term The string that you are searching for
   * @returns An Observable with an action result from the server, and any Specialties from the database that contain your input string
   */
  searchSpecialty(term: string): Observable<Specialty[]> {
    if (!term.trim()) {
      return of([]);
    }
    return this.http
      .get<Specialty[]>(`${this.baseUrl}api/Specialty?search=${term}`)
      .pipe(catchError(this.handleError<Specialty[]>('searchSpecialty', [])));
  }

  /**
   * Sends a request to the server to create a Specialty
   *
   * @example
   * Call the function with an input Specialty object:
   * createSpecialty(myObject)
   *
   * @param {Specialty} specialty The Specialty object that you are trying to add to the database
   * @returns An Observable with the action result from the server, and a copy of your created Specialty
   */
  createSpecialty(specialty: Specialty): Observable<Specialty> {
    return this.http
      .post<Specialty>(`${this.baseUrl}api/Specialty`, specialty)
      .pipe(catchError(this.handleError<Specialty>(`createSpecialty`)));
  }

  /**
   * Sends a request to the server to update a specific Specialty, it uses the ID of your input object.
   *
   * @example
   * Call the function with an input Specialty object:
   * updateSpecialty(myObject)
   *
   * @param {Specialty} specialty The Object with the new values that you are trying to update
   * @returns An Observable with a action result from the server, and the updated Specialty from the database
   */
  updateSpecialty(specialty: Specialty): Observable<any> {
    return this.http
      .put(
        `${this.baseUrl}api/Specialty/${specialty.specialtyId}`,
        specialty,
        this.httpOptions
      )
      .pipe(catchError(this.handleError<Specialty>('updateSpecialty')));
  }

  /**
   * Sends a request to the server to delete a specific Specialty related to the input ID.
   *
   * @example
   * Simply call the function with an input id:
   * deleteSpecialty(1)
   *
   * @param {number} id The ID that you are trying to delete
   * @returns An Observable with a action result from the server
   */
  deleteSpecialty(id: number) {
    return this.http
      .delete(`${this.baseUrl}api/Specialty/${id}`)
      .pipe(catchError(this.handleError<Specialty[]>(`deleteSpecialty`, [])));
  }

  /**
   * Catches errors that are piped into it, and gives them to the alert service as well as prints them to the console logs.
   *
   * @param {string} operation The method/operation that threw the error
   * @param {Observable<T>} result The Observable returned that threw the error. This can also include status codes from the server.
   * @returns A function that print the error to alert service and console, and then return an Observable with a action result from the server
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.alertService.error(operation + ' ' + error);
      console.error(operation + ' ' + error);
      return of(result as T);
    };
  }
}
