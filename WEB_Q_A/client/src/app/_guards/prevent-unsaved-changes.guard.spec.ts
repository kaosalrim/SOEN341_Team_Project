import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule } from 'ngx-toastr';

import { PreventUnsavedChangesGuard } from './prevent-unsaved-changes.guard';

describe('PreventUnsavedChangesGuard', () => {
  let guard: PreventUnsavedChangesGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule, ToastrModule.forRoot()]
    });
    guard = TestBed.inject(PreventUnsavedChangesGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
