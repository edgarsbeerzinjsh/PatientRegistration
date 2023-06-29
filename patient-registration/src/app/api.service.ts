import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../environments/environment';
import { Doctor } from './doctor';
import { Patient } from './patient';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  patients: Patient[] = [
    {
    name: "Testname",
    surname: "SurnameTest",
    telephone: "2345",
    email: "aa@aa",
    birthDate: new Date("1990-01-01"),
    id: 1,
    doctorsId: [1, 2]
    },
    {
    name: "Testname",
    surname: "SurnameTest",
    telephone: "2345",
    email: "aa@aa",
    birthDate: new Date("1990-01-01"),
    id: 1,
    doctorsId: [1, 2]
    },
  ];

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPatients(): Observable<Patient[]> {
    const url = `${this.apiUrl}/patients/all`
    return this.http.get<Patient[]>(url);
    
    //const patientsList = of(this.patients)
    //return patientsList;
  }
}
