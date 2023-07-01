import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CreatePatientComponent } from './create-patient.component';
import { ApiService } from '../api.service';
import { of } from 'rxjs';
import { Patient } from '../patient';

describe('CreatePatientComponent', () => {
  let component: CreatePatientComponent;
  let fixture: ComponentFixture<CreatePatientComponent>;
  let apiService: ApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatePatientComponent],
      imports: [FormsModule, HttpClientModule],
      providers: [ApiService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePatientComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiService);
    fixture.detectChanges();
  });

  it('should create the CreatePatientComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should call apiService.createPatient() when newPatient() is called', () => {
    const mockPatient: Patient = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      eMail: 'john@example.com',
      birthDate: new Date(),
    };

    spyOn(apiService, 'createPatient').and.returnValue(of(mockPatient));

    component.model = { ...mockPatient };
    component.newPatient();

    expect(apiService.createPatient).toHaveBeenCalledWith(component.model);
  });

  it('should reset the form after newPatient() is called', () => {
    const mockPatient: Patient = {
      id: 1,
      name: 'John',
      surname: 'Doe',
      telephone: '123456789',
      eMail: 'john@example.com',
      birthDate: new Date(),
    };

    spyOn(apiService, 'createPatient').and.returnValue(of(mockPatient));

    component.model = { ...mockPatient };
    component.newPatient();

    expect(component.model).toEqual({
      id: 1,
      name: '',
      surname: '',
      telephone: '',
      eMail: '',
      birthDate: new Date(),
    });
  });

  it('should set form errors when future date is entered', () => {
    const futureDate = new Date();
    futureDate.setDate(futureDate.getDate() + 1);

    const control = {
      setErrors: jasmine.createSpy('setErrors')
    };

    component.checkFutureDate({
      value: futureDate,
      control: control
    });

    expect(control.setErrors).toHaveBeenCalledWith({ futureDate: true });
  });

  it('should not set form errors when a valid date is entered', () => {
    const validDate = new Date();
    validDate.setFullYear(validDate.getFullYear() - 20);
  
    const control = {
      setErrors: jasmine.createSpy('setErrors')
    };

    component.checkFutureDate({
      value: validDate,
      control: control
    });

    expect(control.setErrors).toHaveBeenCalledWith(null);
  });
});
