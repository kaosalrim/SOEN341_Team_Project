import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Question } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-question-edit',
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent implements OnInit {
  @Input()
  questionId?: string;
  @ViewChild('editForm') editForm?: NgForm;
  question?: Question;
  user?: User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if(this.editForm?.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private questionService: QuestionService,
     private route: ActivatedRoute, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);      
   }

  ngOnInit(): void {
    this.loadQuestion();
  }

  loadQuestion() {
    this.questionService.getQuestion(this.route.snapshot.paramMap.get('id') || "-1").subscribe(question => this.question = question);
  }

  updateQuestion(){
    this.questionService.updateQuestion(this.question)?.subscribe(() => {
      this.toastr.success('Question updated successfully');
      this.editForm?.reset(this.question);
    })
  }
}
