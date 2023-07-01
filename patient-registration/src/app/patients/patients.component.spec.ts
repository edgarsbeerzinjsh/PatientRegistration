import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { PatientsComponent } from './patients.component';
import { ApiService } from '../api.service';
import { of } from 'rxjs';
import { Doctor } from '../doctor';
import { Patient } from '../patient';

describe('PatientsComponent', () => {
  let component: PatientsComponent;
  let fixture: ComponentFixture<PatientsComponent>;
  let apiService: ApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PatientsComponent],
      imports: [FormsModule, HttpClientModule],
      providers: [ApiService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientsComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiService);
    fixture.detectChanges();
  });

  it('should create the PatientsComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should call apiService.getAllPatients() and populate patients array when initialized', () => {
    const mockPatients: Patient[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        email: 'john@example.com',
        birthDate: new Date('1990-01-01'),
        doctorsId: [1, 2]
      },
      {
        id: 2,
        name: 'Jane',
        surname: 'Smith',
        telephone: '987654321',
        email: 'jane@example.com',
        birthDate: new Date('1995-02-02'),
        doctorsId: [1, 3]
      }
    ];

    spyOn(apiService, 'getAllPatients').and.returnValue(of(mockPatients));

    component.ngOnInit();

    expect(apiService.getAllPatients).toHaveBeenCalled();
    expect(component.patients).toEqual(mockPatients);
  });

  it('should call apiService.getAllDoctors() and populate doctors array when initialized', () => {
    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'Dr. John',
        surname: 'Smith',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology'
      },
      {
        id: 2,
        name: 'Dr. Jane',
        surname: 'Doe',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology'
      }
    ];

    spyOn(apiService, 'getAllDoctors').and.returnValue(of(mockDoctors));

    component.ngOnInit();

    expect(apiService.getAllDoctors).toHaveBeenCalled();
    expect(component.doctors).toEqual(mockDoctors);
  });

  it('should call apiService.getDoctorPatients() and populate patients array when getDoctorsPatients() is called', () => {
    const doctorId = 1;
    const mockPatients: Patient[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        email: 'john@example.com',
        birthDate: new Date('1990-01-01'),
        doctorsId: [1, 2]
      },
      {
        id: 2,
        name: 'Jane',
        surname: 'Smith',
        telephone: '987654321',
        email: 'jane@example.com',
        birthDate: new Date('1995-02-02'),
        doctorsId: [1, 3]
      }
    ];

    spyOn(apiService, 'getDoctorPatients').and.returnValue(of(mockPatients));

    component.getDoctorsPatients(doctorId);

    expect(apiService.getDoctorPatients).toHaveBeenCalledWith(doctorId);
    expect(component.patients).toEqual(mockPatients);
  });

  it('should call apiService.addPatientToDoctor(), reset addDoctor value, and call PatientsForDoctor() when addPatientToDoctor() is called', () => {
    const patientId = 1;
    const doctorId = "2";

    spyOn(apiService, 'addPatientToDoctor').and.returnValue(of({}));

    const patientsForDoctorSpy = spyOn(component, 'PatientsForDoctor');

    component.addDoctor = doctorId;
    component.addPatientToDoctor(patientId);

    expect(apiService.addPatientToDoctor).toHaveBeenCalledWith(parseInt(doctorId), patientId);
    expect(patientsForDoctorSpy).toHaveBeenCalled();
  });

  it('should remove the patient from the patients array when deletePatient() is called', () => {
    const patientId = 1;
    const mockPatients: Patient[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        email: 'john@example.com',
        birthDate: new Date('1990-01-01'),
        doctorsId: [1, 2]
      },
      {
        id: 2,
        name: 'Jane',
        surname: 'Smith',
        telephone: '987654321',
        email: 'jane@example.com',
        birthDate: new Date('1995-02-02'),
        doctorsId: [1, 3]
      }
    ];

    component.patients = [...mockPatients];

    spyOn(apiService, 'deletePatient').and.returnValue(of({}));

    component.deletePatient(patientId);

    expect(component.patients.length).toBe(1);
    expect(component.patients[0].id).toBe(2);
    expect(apiService.deletePatient).toHaveBeenCalledWith(patientId);
  });

  it('should return the doctor name by id when getDoctorNameById() is called', () => {
    const doctorId = 1;
    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'Dr. John',
        surname: 'Smith',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology'
      },
      {
        id: 2,
        name: 'Dr. Jane',
        surname: 'Doe',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology'
      }
    ];

    component.doctors = [...mockDoctors];

    const doctorName = component.getDoctorNameById(doctorId);

    expect(doctorName).toBe('Dr. John Smith');
  });

  it('should return the available doctors for a patient when getAvailableDoctors() is called', () => {
    const patient: Patient = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      email: 'john@example.com',
      birthDate: new Date('1990-01-01'),
      doctorsId: [1, 2]
    };

    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'Dr. John',
        surname: 'Smith',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology'
      },
      {
        id: 2,
        name: 'Dr. Jane',
        surname: 'Doe',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology'
      },
      {
        id: 3,
        name: 'Dr. Mark',
        surname: 'Johnson',
        telephone: '123456788',
        email: 'mark@example.com',
        specialization: 'Orthopedics'
      }
    ];

    component.doctors = [...mockDoctors];

    const availableDoctors = component.getAvailableDoctors(patient);

    expect(availableDoctors.length).toBe(1);
    expect(availableDoctors[0].id).toBe(3);
  });

  it('should return true if there are available doctors for a patient when hasAvailableDoctors() is called', () => {
    const patient: Patient = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      email: 'john@example.com',
      birthDate: new Date('1990-01-01'),
      doctorsId: [1, 2]
    };

    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'Dr. John',
        surname: 'Smith',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology'
      },
      {
        id: 2,
        name: 'Dr. Jane',
        surname: 'Doe',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology'
      },
      {
        id: 3,
        name: 'Dr. Mark',
        surname: 'Johnson',
        telephone: '123456788',
        email: 'mark@example.com',
        specialization: 'Orthopedics'
      }
    ];

    component.doctors = [...mockDoctors];

    const hasAvailableDoctors = component.hasAvaiableDoctors(patient);

    expect(hasAvailableDoctors).toBe(true);
  });

  it('should return false if there are no available doctors for a patient when hasAvailableDoctors() is called', () => {
    const patient: Patient = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      email: 'john@example.com',
      birthDate: new Date('1990-01-01'),
      doctorsId: [1, 2]
    };

    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'Dr. John',
        surname: 'Smith',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology'
      },
      {
        id: 2,
        name: 'Dr. Jane',
        surname: 'Doe',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology'
      }
    ];

    component.doctors = [...mockDoctors];

    const hasAvailableDoctors = component.hasAvaiableDoctors(patient);

    expect(hasAvailableDoctors).toBe(false);
  });
});
