import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Pagination } from 'src/app/_models/pagination';
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
  //questions$!: Observable<Question[]>;
  user?: User;
  questions: Question[] = [];
  pagination: Pagination = {} as Pagination;
  pageNumber = 1;
  pageSize = 5;
  rotate = false;
  maxSize = 5;

  constructor(private questionService: QuestionService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
    }

  ngOnInit(): void {
    this.load();    
  }

  private load() {
    if (this.isUserSpecific) {
      if (this.isUserAnswered) {
        this.loadUserAnsweredQuestions(this.username);
      } else {
        this.loadUserQuestions(this.username);
      }
    } else {
      this.loadQuestions();
    }
  }

  loadQuestions(){
    this.questionService.getQuestions(this.pageNumber, this.pageSize).subscribe(response => {
      this.questions = response.result;
      this.pagination = response.pagination;
    });
  }

  loadUserQuestions(username: string){
    this.questionService.getUserQuestions(username,this.pageNumber, this.pageSize).subscribe(response => {
      this.questions = response.result;
      this.pagination = response.pagination;
    });
  }

  loadUserAnsweredQuestions(username: string){
    this.questionService.getUserQuestionsAnswered(username,this.pageNumber, this.pageSize).subscribe(response => {
      this.questions = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.load();
  }
}
