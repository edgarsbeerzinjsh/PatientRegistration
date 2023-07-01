import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import { Doctor } from './doctor';
import { Patient } from './patient';

describe('ApiService', () => {
  let service: ApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ApiService]
    });
    service = TestBed.inject(ApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all patients', () => {
    const dummyPatients: Patient[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        eMail: 'john@example.com',
        birthDate: new Date(),
        doctorsId: []
      },
    ];

    service.getAllPatients().subscribe(patients => {
      expect(patients).toEqual(dummyPatients);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/patients/all`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyPatients);
  });

  it('should retrieve all doctors', () => {
    const dummyDoctors: Doctor[] = [
      {
        id: 1,
        name: 'Doctor',
        surname: 'Strange',
        specialization: 'Neurology',
        telephone: '123456789',
        eMail: 'doctor@example.com'
      },
      // Add more dummy doctors as needed
    ];

    service.getAllDoctors().subscribe(doctors => {
      expect(doctors).toEqual(dummyDoctors);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/doctors/all`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyDoctors);
  });

  it('should retrieve doctor patients', () => {
    const doctorId = 1;
    const dummyPatients: Patient[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        eMail: 'john@example.com',
        birthDate: new Date(),
        doctorsId: [doctorId]
      },
    ];

    service.getDoctorPatients(doctorId).subscribe(patients => {
      expect(patients).toEqual(dummyPatients);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/doctorsPatients/${doctorId}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyPatients);
  });

  it('should add patient to doctor', () => {
    const doctorId = 1;
    const patientId = 1;

    service.addPatientToDoctor(doctorId, patientId).subscribe(() => {
      expect().nothing();
    });

    const req = httpMock.expectOne(`${service.apiUrl}/patients/addToDoctor/${doctorId}/${patientId}`);
    expect(req.request.method).toBe('PUT');
    req.flush({});
  });

  it('should create a new doctor', () => {
    const newDoctor: Doctor = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      specialization: 'Cardiology',
      telephone: '123456789',
      eMail: 'john@example.com'
    };

    service.createDoctor(newDoctor).subscribe((addedDoctor: Doctor) => {
      expect(addedDoctor).toEqual(newDoctor);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/createDoctor`);
    expect(req.request.method).toBe('POST');
    req.flush(newDoctor);
  });

  it('should create a new patient', () => {
    const newPatient: Patient = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      eMail: 'john@example.com',
      birthDate: new Date(),
      doctorsId: []
    };

    service.createPatient(newPatient).subscribe((addedPatient: Patient) => {
      expect(addedPatient).toEqual(newPatient);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/createPatient`);
    expect(req.request.method).toBe('POST');
    req.flush(newPatient);
  });

  it('should delete a doctor', () => {
    const doctorId = 1;

    service.deleteDoctor(doctorId).subscribe(() => {
      expect().nothing();
    });

    const req = httpMock.expectOne(`${service.apiUrl}/deleteDoctor/${doctorId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  it('should delete a patient', () => {
    const patientId = 1;

    service.deletePatient(patientId).subscribe(() => {
      expect().nothing();
    });

    const req = httpMock.expectOne(`${service.apiUrl}/deletePatient/${patientId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

});
