import { Component, Input, OnInit } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { Question } from 'src/app/_models/question';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css']
})
export class QuestionCardComponent implements OnInit {
  @Input()
  question!: Question;
  @Input()
  isCurrentUser: boolean = false;
  @Input()
  isUserAnswered: boolean = false;
  photo?: Photo;
  rep?: string;

  constructor(private questionService: QuestionService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.getPhoto(this.question.username);
    this.getRep(this.question.username);
  }

  getPhoto(username:string){
    this.questionService.getUserQuestionPhoto(username).subscribe(p => {
      this.photo = p;
    })
  }

  getRep(username:string){
    this.questionService.getUserQuestionRep(username).subscribe(p => {
      this.rep = p;
    })
  }

}
