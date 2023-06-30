import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Doctor } from './doctor';
import { Patient } from './patient';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllPatients(): Observable<Patient[]> {
    const url = `${this.apiUrl}/patients/all`
    return this.http.get<Patient[]>(url)
      .pipe(
        tap(_ => console.log('fetched all patients')),
        catchError(this.handleError)
      );
  }

  getAllDoctors(): Observable<Doctor[]> {
    const url = `${this.apiUrl}/doctors/all`
    return this.http.get<Doctor[]>(url)
      .pipe(
        tap(_ => console.log('fetched all doctors')),
        catchError(this.handleError)
      );
  }

  getDoctorPatients(doctorId: number): Observable<Patient[]> {
    const url = `${this.apiUrl}/doctorsPatients/${doctorId}`
    return this.http.get<Patient[]>(url)
      .pipe(
        tap(_ => console.log(`fetched doctor id=${doctorId} patients`)),
        catchError(this.handleError)
      );
  }

  addPatientToDoctor(doctorId: number, patientId: number): Observable<any> {
    const url = `${this.apiUrl}/patients/addToDoctor/${doctorId}/${patientId}`
    return this.http.put(url, "",  { responseType: 'text' })
      .pipe(
        tap(_ => console.log(`patient id=${patientId} added to doctor id=${doctorId}`)),
        catchError(this.handleError)
      );
  }

  createDoctor(newDoctor: Doctor): Observable<Doctor> {
    const url = `${this.apiUrl}/createDoctor`;
    return this.http.post<Doctor>(url, newDoctor)
      .pipe(
        tap((addedDoctor: any) => console.log(`created new doctor: ${JSON.stringify(addedDoctor)}`)),
        catchError(this.handleError)
      )
  }

  createPatient(newPatient: Patient): Observable<Patient> {
    const url = `${this.apiUrl}/createPatient`;
    return this.http.post<Patient>(url, newPatient)
      .pipe(
        tap((addedPatient: any) => console.log(`created new patient: ${JSON.stringify(addedPatient)}`)),
        catchError(this.handleError)
      )
  }

  deletePatient(patientId: number): Observable<any> {
    const url = `${this.apiUrl}/deletePatient/${patientId}`
    return this.http.delete(url, { responseType: 'text' })
      .pipe(
        tap(_ => console.log(`patient id=${patientId} deleted`)),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse): Observable<any> {
    if (error.error instanceof ErrorEvent){
      console.error('An error occured:', error.error.message)
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body: ${JSON.stringify(error.error)}`
      );
    }

    return of({});
  }

  // private handleError<T>(operation = 'operation', result?: T) {
  //   return (error: any): Observable<T> => {
  //     console.error(error);
  //     return of(result as T);
  //   }
  // }
}
