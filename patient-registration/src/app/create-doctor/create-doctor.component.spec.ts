import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateDoctorComponent } from './create-doctor.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../api.service';
import { of } from 'rxjs';
import { Doctor } from '../doctor';

describe('CreateDoctorComponent', () => {
  let component: CreateDoctorComponent;
  let fixture: ComponentFixture<CreateDoctorComponent>;
  let apiService: ApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateDoctorComponent],
      imports: [FormsModule, HttpClientModule],
      providers: [ApiService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateDoctorComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiService);
    fixture.detectChanges();
  });

  it('should create the CreateDoctorComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should call apiService.createDoctor() when newDoctor() is called', () => {
    const mockDoctor: Doctor = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      eMail: 'john@example.com',
      specialization: 'Cardiology'
    };

    spyOn(apiService, 'createDoctor').and.returnValue(of(mockDoctor));

    component.model = { ...mockDoctor };
    component.newDoctor();

    expect(apiService.createDoctor).toHaveBeenCalledWith(component.model);
  });

  it('should reset the form after newDoctor() is called', () => {
    const mockDoctor: Doctor = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      eMail: 'john@example.com',
      specialization: 'Cardiology'
    };

    spyOn(apiService, 'createDoctor').and.returnValue(of(mockDoctor));

    component.model = { ...mockDoctor };
    component.newDoctor();

    expect(component.model).toEqual({
      id: 1,
      name: '',
      surname: '',
      telephone: '',
      eMail: '',
      specialization: ''
    });
  });
});
