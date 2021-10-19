import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { QuestionListComponent } from './questions/question-list/question-list.component';
import { QuestionDetailComponent } from './questions/question-detail/question-detail.component';
import { TopicsComponent } from './topics/topics.component';
import { AboutComponent } from './about/about.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { QuestionCardComponent } from './questions/question-card/question-card.component';
import { NavSideBarComponent } from './nav/nav-side-bar/nav-side-bar.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { AnswerCardComponent } from './answers/answer-card/answer-card.component';
import { ProfileComponent } from './user/profile/profile.component';
import { RouteReuseStrategy } from '@angular/router';
import { AARouteReuseStrategy } from './_models/aaroutereusestrategy';
import { QuestionEditComponent } from './questions/question-edit/question-edit.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { AnswerEditComponent } from './answers/answer-edit/answer-edit.component';
import { AnswerCreateComponent } from './answers/answer-create/answer-create.component';
import { QuestionCreateComponent } from './questions/question-create/question-create.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    QuestionListComponent,
    QuestionDetailComponent,
    TopicsComponent,
    AboutComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    QuestionCardComponent,
    NavSideBarComponent,
    AnswerCardComponent,
    ProfileComponent,
    QuestionEditComponent,
    AnswerEditComponent,
    AnswerCreateComponent,
    QuestionCreateComponent,
    TextInputComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgxSpinnerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: RouteReuseStrategy, useClass: AARouteReuseStrategy }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
