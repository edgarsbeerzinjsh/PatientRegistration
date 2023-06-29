import { Component, OnInit } from '@angular/core';
import { Doctor } from '../doctor';
import { Patient } from '../patient';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  patients: Patient[] = []
  selectedDoctor?: string;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.getPatients();
  }

  getPatients(): void {
    this.apiService.getPatients()
      .subscribe(p => this.patients = p);
  }

  PatientsForDoctor() {
    console.log("Patients for: ", this.selectedDoctor);
  }

}
