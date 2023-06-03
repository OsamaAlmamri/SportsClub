import { TestBed } from '@angular/core/testing';

import { ServiceTypesService } from './service-types.service';

describe('ServiceTypesService', () => {
  let service: ServiceTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
