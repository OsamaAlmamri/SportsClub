import { TestBed } from '@angular/core/testing';

import { ServiceTimesService } from './service-times.service';

describe('ServiceTimesService', () => {
  let service: ServiceTimesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceTimesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
