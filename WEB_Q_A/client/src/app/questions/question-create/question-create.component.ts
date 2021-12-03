import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Question } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';
import { environment } from 'src/environments/environment';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-question-create',
  templateUrl: './question-create.component.html',
  styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  question: Question = {} as Question;
  user?: User;
  baseUrl = environment.apiUrl;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm?.dirty){
      $event.returnValue = true;
    }
  }
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      ['bold'],
      ['insertVideo', 'insertImage', 'backgroundColor']
      ]
  };

  constructor(private accountService: AccountService,
    private questionService: QuestionService,
    private toastr: ToastrService,
    private router: Router) {
      this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
     }

  ngOnInit(): void {
  }

  createQuestion() {
    if (this.user) {
      this.question.username = this.user.username;
      this.questionService.createQuestion(this.question)?.subscribe(question => {
        this.toastr.success('Question created successfully');
        this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() => {
            this.router.navigate(['/questions/'+ question.id]);
          });
          //this.questionService.addNewQuestion(question); 
      }, error => {
        this.toastr.error(error);
      });
    }
  }
}
