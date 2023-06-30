import { Component } from '@angular/core';
import { Patient } from '../patient';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.css']
})
export class CreatePatientComponent {
  model: Patient = {
    name: "",
    surname: "",
    telephone: "",
    email: "",
    birthDate: new Date()
  }

  constructor(private apiService: ApiService) {}

  newPatient() {
    this.apiService.createPatient(this.model)
      .subscribe(patient => alert(`You have added patient: ${JSON.stringify(patient)}`));
  }

  getTodayDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = ('0' + (today.getMonth() + 1)).slice(-2);
    const day = ('0' + (today.getDate())).slice(-2);
    return `${year}-${month}-${day}`;
  }
}
