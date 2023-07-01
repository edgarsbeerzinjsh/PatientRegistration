import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationComponent } from './navigation.component';

describe('NavigationComponent', () => {
  let component: NavigationComponent;
  let fixture: ComponentFixture<NavigationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavigationComponent]
    });
    fixture = TestBed.createComponent(NavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the NavigationComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should render navigation links', () => {
    const compiled = fixture.nativeElement;
    const links = compiled.querySelectorAll('a');
    expect(links.length).toBe(4);
    expect(links[0].textContent).toContain('Patient List');
    expect(links[1].textContent).toContain('Doctor List');
    expect(links[2].textContent).toContain('Add doctor');
    expect(links[3].textContent).toContain('Add patient');
  });
});
