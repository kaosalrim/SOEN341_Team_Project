import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { TopicsComponent } from './topics/topics.component';
import { QuestionDetailComponent } from './questions/question-detail/question-detail.component';
import { QuestionListComponent } from './questions/question-list/question-list.component';
import { AboutComponent } from './about/about.component';
import { AuthGuard } from './_guards/auth.guard';
import { ProfileComponent } from './user/profile/profile.component';
import { QuestionEditComponent } from './questions/question-edit/question-edit.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { AnswerEditComponent } from './answers/answer-edit/answer-edit.component';
import { QuestionCreateComponent } from './questions/question-create/question-create.component';
import { HomeComponent } from './home/home.component';

//create routes to separate components
const routes: Routes = [
  { path: '', component: QuestionListComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'users/:username', component: ProfileComponent },
      { path: 'question/edit/:id', component: QuestionEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      { path: 'answer/edit/:id', component: AnswerEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      { path: 'question/create', component: QuestionCreateComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    ],
  },
  { path: 'questions', component: QuestionListComponent },
  { path: 'questions/:id', component: QuestionDetailComponent},
  { path: 'topics', component: TopicsComponent },
  { path: 'register', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
