import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Answer } from 'src/app/_models/answer';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AnswerService } from 'src/app/_services/answer.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-answer-edit',
  templateUrl: './answer-edit.component.html',
  styleUrls: ['./answer-edit.component.css']
})
export class AnswerEditComponent implements OnInit {
  @Input()
  answerId?: string;
  @ViewChild('editForm') editForm?: NgForm;
  answer?: Answer;
  user?: User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm?.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private answerService: AnswerService, private questionService: QuestionService,
    private route: ActivatedRoute, private toastr: ToastrService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
     }

  ngOnInit(): void {
    this.loadAnswer();
  }

  loadAnswer() {
    this.answerService.getAnswer(this.route.snapshot.paramMap.get('id') || "-1").subscribe(answer => this.answer = answer);
  }

  updateAnswer(){
    this.answerService.updateAnswer(this.answer)?.subscribe(() => {
      this.toastr.success('Answer updated successfully');
      this.questionService.updateQuestionsAnswers(this.answer);
      this.editForm?.reset(this.answer);
    }, error => {
      
    })
  }

}
