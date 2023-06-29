import { Component } from '@angular/core';
import { Doctor } from '../doctor';
import { Patient } from '../patient';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent {
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

  selectedDoctor?: string;

  PatientsForDoctor() {
    console.log("Patients for: ", this.selectedDoctor);
  }

}
