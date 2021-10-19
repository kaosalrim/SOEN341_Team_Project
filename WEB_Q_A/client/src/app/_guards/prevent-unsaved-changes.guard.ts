import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { QuestionEditComponent } from '../questions/question-edit/question-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: QuestionEditComponent): boolean {
      if (component.editForm?.dirty){
        return confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
      }
      return true;
    }
  
}
