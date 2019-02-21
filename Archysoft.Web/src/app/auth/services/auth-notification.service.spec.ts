import { TestBed } from '@angular/core/testing';

import { AuthNotificationService } from './auth-notification.service';

describe('AuthNotificationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuthNotificationService = TestBed.get(AuthNotificationService);
    expect(service).toBeTruthy();
  });
});
