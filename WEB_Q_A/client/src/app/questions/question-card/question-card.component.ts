import { Component, Input, OnInit } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { AccountService } from 'src/app/_services/account.service';

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

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }
}
