import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthNotificationComponent } from './auth-notification.component';

describe('AuthNotificationComponent', () => {
  let component: AuthNotificationComponent;
  let fixture: ComponentFixture<AuthNotificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthNotificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
