import { Component, OnInit } from '@angular/core';
import { Doctor } from '../doctor';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit  {
  doctors: Doctor[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(): void {
    this.apiService.getAllDoctors()
      .subscribe(d => this.doctors = d);
  }

  deleteDoctor(doctorId: number): void {
    this.doctors = this.doctors.filter(p => p.id !== doctorId);
    this.apiService.deleteDoctor(doctorId)
      .subscribe();
  }
}
