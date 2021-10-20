import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Question } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnInit {
  @Input()
  isUserSpecific: boolean = false;
  @Input()
  isUserAnswered: boolean = false;
  @Input()
  username: string = "";
  questions: Question[] = [];
  user?: User;

  constructor(private questionService: QuestionService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
    }

  ngOnInit(): void {
    if (this.isUserSpecific){
      if (this.isUserAnswered) {
        this.loadUserAnsweredQuestions(this.username);
      } else {
        this.loadUserQuestions(this.username);
      }      
    } else{
      this.loadQuestions();
    }    
  }

  loadQuestions(){
    this.questionService.getQuestions().subscribe(questions => {
      this.questions = questions;
    })
  }

  loadUserQuestions(username: string){
    this.questionService.getUserQuestions(username).subscribe(questions => {
      this.questions = questions;
    })
  }

  loadUserAnsweredQuestions(username: string){
    this.questionService.getUserQuestionsAnswered(username).subscribe(questions => {
      this.questions = questions;
    })
  }
}
