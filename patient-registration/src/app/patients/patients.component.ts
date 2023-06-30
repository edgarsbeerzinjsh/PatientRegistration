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
  doctors: Doctor[] = []
  selectedDoctor: string = "AllPatients";
  addDoctor: string = "addDoctor";

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.getDoctors();
    this.PatientsForDoctor();
  }

  getPatients(): void {
    this.apiService.getAllPatients()
      .subscribe(p => this.patients = p);
  }

  getDoctors(): void {
    this.apiService.getAllDoctors()
      .subscribe(d => this.doctors = d);
  }

  getDoctorsPatients(doctorsId: number): void {
    this.apiService.getDoctorPatients(doctorsId)
      .subscribe(p => this.patients = p);
  }

  PatientsForDoctor() {
    if (this.selectedDoctor === "AllPatients") {
      this.getPatients()
    }
    else {
      this.getDoctorsPatients(parseInt(this.selectedDoctor))
    }
  }

  addPatientToDoctor(patientId: number) {
    var doctorId = parseInt(this.addDoctor)
    this.apiService.addPatientToDoctor(doctorId, patientId)
      .subscribe(() => {
        this.addDoctor = "addDoctor";
        this.PatientsForDoctor();
      });
  }

  deletePatient(patientId: number): void {
    this.patients = this.patients.filter(p => p.id !== patientId);
    this.apiService.deletePatient(patientId)
      .subscribe();
  }

  getDoctorNameById(doctorId: number): string {
    const doctor = this.doctors.find(d => d.id === doctorId);
    return doctor ? `${doctor.name} ${doctor.surname}` : '';
  }

  getAvailableDoctors(patient: any): any[] {
    return this.doctors.filter(d => !patient.doctorsId.includes(d.id));
  }

  hasAvaiableDoctors(patient: any): boolean {
    return this.getAvailableDoctors(patient).length > 0
  }

}
