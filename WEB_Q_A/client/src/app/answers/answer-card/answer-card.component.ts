import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Answer } from 'src/app/_models/answer';
import { Member } from 'src/app/_models/member';
import { Question } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AnswerService } from 'src/app/_services/answer.service';
import { MemberService } from 'src/app/_services/member.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-answer-card',
  templateUrl: './answer-card.component.html',
  styleUrls: ['./answer-card.component.css'],
})
export class AnswerCardComponent implements OnInit {
  @Input()
  answer!: Answer;
  user?: User;
  member?: Member;

  constructor(
    public accountService: AccountService,
    private toastrService: ToastrService,
    private memberService: MemberService,
    private answerService: AnswerService,
    private questionService: QuestionService,
    private router: Router
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(
      (user) => (this.user = user),
      () => {},
      () => {
        if (this.user) {
          this.memberService
            .getMember(this.user.username)
            .subscribe((member) => {
              this.member = member;
            });
        }
      }
    );
  }

  ngOnInit(): void {  }

  rankUp() {
    if (this.member) {
      const vote = this.member.userVotes.find(
        (x) => x.answerId === this.answer.id
      );
      if (vote === undefined) {
        this.answer.rank += 1;
        this.answerService
          .updateAnswerRank(true, this.user!.username, this.answer)
          ?.subscribe(
            () => {
              this.toastrService.success('Upvoted');
            },
            (error) => {
              this.toastrService.error('Something went wrong ' + error);
            },
            () => {
              this.memberService
                .getMember(this.user!.username)
                .subscribe((member) => {
                  this.member = member;
                });
            }
          );
      } else {
        this.toastrService.error('You already upvoted for this answer.');
      }
    } else {
      this.toastrService.error(
        'You must login or register to use this function.'
      );
    }
  }

  rankDown() {
    if (this.member) {
      const vote = this.member.userVotes.find(
        (x) => x.answerId === this.answer.id
      );
      if (vote !== undefined) {
        this.answer.rank -= 1;
        this.answerService
          .updateAnswerRank(false, this.user!.username, this.answer)
          ?.subscribe(
            () => {
              this.toastrService.success('Downvoted');
            },
            (error) => {
              this.toastrService.error('Something went wrong ' + error);
            },
            () => {
              this.memberService
                .getMember(this.user!.username)
                .subscribe((member) => {
                  this.member = member;
                });
            }
          );
      } else {
        this.toastrService.error('You never upvoted this answer.');
      }
    } else {
      this.toastrService.error(
        'You must login or register to use this function.'
      );
    }
  }

  acceptAnswer() {
    if (this.member) {
      var question: Question = {} as Question;
      this.questionService
        .getQuestion(this.answer.questionId.toString())
        .subscribe(
          (q) => {
            question = q;
          },
          (error) => {
            this.toastrService.error('Something went wrong ' + error);
          },
          () => {
            if (question.username === this.member!.username) {
              var answers = question.answers.filter(
                (x) => x.id !== this.answer.id
              );
              answers.forEach((ans) => {
                ans.isBestAnswer = false;
              });
              this.answer.isBestAnswer = true;
              this.answerService
                .updateBestAnswer(this.answer)
                ?.subscribe(() => {
                  this.toastrService.success('Best answer accepted.');
                  this.router
                    .navigateByUrl('/', { skipLocationChange: true })
                    .then(() => {
                      this.router.navigate(['/questions/' + this.answer.questionId]);
                    });
                });
            } else {
              this.toastrService.error(
                'You are not the owner of this question.'
              );
            }
          }
        );
    } else {
      this.toastrService.error(
        'You must login or register to use this function.'
      );
    }
  }

  unAcceptAnswer() {
    if (this.member) {
      var question: Question = {} as Question;
      this.questionService
        .getQuestion(this.answer.questionId.toString())
        .subscribe(
          (q) => {
            question = q;
          },
          (error) => {
            this.toastrService.error('Something went wrong ' + error);
          },
          () => {
            if (question.username === this.member!.username) {
              this.answer.isBestAnswer = false;
              this.answerService
                .updateBestAnswer(this.answer)
                ?.subscribe(() => {
                  this.toastrService.success('Best answer removed.');
                });
            } else {
              this.toastrService.error(
                'You are not the owner of this question.'
              );
            }
          }
        );
    } else {
      this.toastrService.error(
        'You must login or register to use this function.'
      );
    }
  }
}
