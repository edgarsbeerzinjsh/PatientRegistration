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
      .subscribe(patient => {
        alert(`You have added patient: ${JSON.stringify(patient)}`);
        this.model.name = "";
        this.model.surname = "";
        this.model.telephone = "";
        this.model.email = "";
        this.model.birthDate = new Date();
      });
  }

  getTodayDate(): string {
    const today = new Date();
    return today.toISOString().split('T')[0];
  }

  checkFutureDate(birthDate: any) {
    if (new Date(birthDate.value) > new Date()) {
      birthDate.control.setErrors({ futureDate: true});
    } else {
      birthDate.control.setErrors(null);
    }
  }
}
