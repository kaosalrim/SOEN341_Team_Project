import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { TopicsComponent } from './topics/topics.component';
import { QuestionDetailComponent } from './questions/question-detail/question-detail.component';
import { QuestionListComponent } from './questions/question-list/question-list.component';
import { AboutComponent } from './about/about.component';
import { AuthGuard } from './_guards/auth.guard';

//create routes to separate components
const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [{ path: 'about', component: AboutComponent }],
  },
  { path: 'questions', component: QuestionListComponent },
  { path: 'questions/:id', component: QuestionDetailComponent },
  { path: 'topics', component: TopicsComponent },
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
