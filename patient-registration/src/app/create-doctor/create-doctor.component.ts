import { Component } from '@angular/core';
import { Doctor } from '../doctor';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-create-doctor',
  templateUrl: './create-doctor.component.html',
  styleUrls: ['./create-doctor.component.css']
})
export class CreateDoctorComponent {
  model: Doctor = {
    name: "",
    surname: "",
    telephone: "",
    email: "",
    specialization: ""
  }

  constructor(private apiService: ApiService) {}

  newDoctor() {
    this.apiService.createDoctor(this.model)
      .subscribe(doctor => alert(`You have added doctor: ${JSON.stringify(doctor)}`));
  }
}
