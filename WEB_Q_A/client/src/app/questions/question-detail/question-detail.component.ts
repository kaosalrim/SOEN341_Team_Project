import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { Photo } from 'src/app/_models/photo';
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
  photo?: Photo;
  rep?: string;
  user?: User;

  constructor(private questionService: QuestionService, private route: ActivatedRoute,
     private accountService: AccountService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
      }

  ngOnInit(): void {
    this.loadQuestion();
    //this.question?.answers?.length    
  }

  loadQuestion(){
    this.questionService.getQuestion(this.route.snapshot.paramMap.get('id') || "-1") 
    .subscribe(question => this.question = question, () => {}, () => {
      this.getPhoto();
      this.getRep();
    } );    
  }

  getPhoto(){
    this.questionService.getUserQuestionPhoto(this.question!.username).subscribe(p => {
      this.photo = p;
    })
  }

  getRep(){
    this.questionService.getUserQuestionRep(this.question!.username).subscribe(p => {
      this.rep = p;
    })
  }

}
