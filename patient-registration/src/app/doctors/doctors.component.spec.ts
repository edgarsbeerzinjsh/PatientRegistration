import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { DoctorsComponent } from './doctors.component';
import { ApiService } from '../api.service';
import { of } from 'rxjs';
import { Doctor } from '../doctor';

describe('DoctorsComponent', () => {
  let component: DoctorsComponent;
  let fixture: ComponentFixture<DoctorsComponent>;
  let apiService: ApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DoctorsComponent],
      imports: [HttpClientModule],
      providers: [ApiService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorsComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiService);
  });

  it('should create the DoctorsComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch doctors on component initialization', () => {
    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology',
      },
      {
        id: 2,
        name: 'Jane',
        surname: 'Smith',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology',
      },
    ];

    spyOn(apiService, 'getAllDoctors').and.returnValue(of(mockDoctors));

    fixture.detectChanges();

    expect(apiService.getAllDoctors).toHaveBeenCalled();
    expect(component.doctors).toEqual(mockDoctors);
  });

  it('should delete a doctor on deleteDoctor() call', () => {
    const doctorIdToDelete = 1;
    const mockDoctors: Doctor[] = [
      {
        id: 1,
        name: 'John',
        surname: 'Doe',
        telephone: '123456789',
        email: 'john@example.com',
        specialization: 'Cardiology',
      },
      {
        id: 2,
        name: 'Jane',
        surname: 'Smith',
        telephone: '987654321',
        email: 'jane@example.com',
        specialization: 'Dermatology',
      },
    ];

    component.doctors = [...mockDoctors];

    spyOn(apiService, 'deleteDoctor').and.returnValue(of({}));

    component.deleteDoctor(doctorIdToDelete);

    expect(component.doctors.length).toBe(mockDoctors.length - 1);
    expect(apiService.deleteDoctor).toHaveBeenCalledWith(doctorIdToDelete);
  });
});
