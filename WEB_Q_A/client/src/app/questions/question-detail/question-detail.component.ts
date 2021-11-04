import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { Question } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-question-detail',
  templateUrl: './question-detail.component.html',
  styleUrls: ['./question-detail.component.css']
})
export class QuestionDetailComponent implements OnInit {
  question: Question | undefined;
  user?: User;

  constructor(private questionService: QuestionService, private route: ActivatedRoute,
     public accountService: AccountService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
      }

  ngOnInit(): void {
    this.loadQuestion();  
  }

  loadQuestion(){
    this.questionService.getQuestion(this.route.snapshot.paramMap.get('id') || "-1") 
    .subscribe(question => this.question = question);    
  }
}
