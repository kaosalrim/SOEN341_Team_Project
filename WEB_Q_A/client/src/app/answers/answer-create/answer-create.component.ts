import {
  Component,
  HostListener,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Answer } from 'src/app/_models/answer';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AnswerService } from 'src/app/_services/answer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-answer-create',
  templateUrl: './answer-create.component.html',
  styleUrls: ['./answer-create.component.css'],
})
export class AnswerCreateComponent implements OnInit {
  @Input()
  questionId?: number;
  @ViewChild('editForm') editForm?: NgForm;
  answer: Answer = {} as Answer;
  user?: User;
  baseUrl = environment.apiUrl;
  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private accountService: AccountService,
    private answerService: AnswerService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
  }

  ngOnInit(): void {}

  createAnswer() {
    if (this.user) {
      this.answer.questionId = this.questionId!;
      this.answer.username = this.user.username;
      this.answerService.createAnswer(this.answer)?.subscribe(
        () => {
          this.toastr.success('Answer created successfully');
          //this.questionService.getQuestions(true);
          this.router.navigateByUrl('/questions', { skipLocationChange: true })
            .then(() => {
              this.router.navigate(['/questions/' + this.questionId]);
            });
        },
        (error) => {
          this.toastr.error(error);
        }
      );
    }
  }
}
