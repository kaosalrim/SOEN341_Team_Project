import { Component, Input, OnInit } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { Photo } from 'src/app/_models/photo';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-answer-card',
  templateUrl: './answer-card.component.html',
  styleUrls: ['./answer-card.component.css']
})
export class AnswerCardComponent implements OnInit {
  @Input()
  answer!: Answer;
  photo?: Photo;
  rep?: string;

  constructor(private questionService: QuestionService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.getPhoto(this.answer.username);
    this.getRep(this.answer.username);
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
