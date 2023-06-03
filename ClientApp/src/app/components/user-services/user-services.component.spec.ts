import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserServicesComponent } from './user-services.component';

describe('UserServicesComponent', () => {
  let component: UserServicesComponent;
  let fixture: ComponentFixture<UserServicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserServicesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
