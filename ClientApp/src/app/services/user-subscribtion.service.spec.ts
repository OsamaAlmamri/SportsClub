import { TestBed } from '@angular/core/testing';

import { UserSubscribtionService } from './user-subscribtion.service';

describe('UserSubscribtionService', () => {
  let service: UserSubscribtionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserSubscribtionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
