import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Answer } from 'src/app/_models/answer';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { UserVotes } from 'src/app/_models/UserVotes';
import { AccountService } from 'src/app/_services/account.service';
import { AnswerService } from 'src/app/_services/answer.service';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-answer-card',
  templateUrl: './answer-card.component.html',
  styleUrls: ['./answer-card.component.css']
})
export class AnswerCardComponent implements OnInit {
  @Input()
  answer!: Answer;
  user?: User;
  member?: Member;

  constructor(public accountService: AccountService,
     private toastrService: ToastrService,
     private memberService: MemberService,
     private answerService: AnswerService) { 
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user), () => {}, () => {
        if(this.user){
          this.memberService.getMember(this.user.username).subscribe(member => {
            this.member = member;
          })
        }
      });
  }

  ngOnInit(): void {
  }

  rankUp(){
    if(this.member){
      const vote = this.member.userVotes.find(x => x.answerId === this.answer.id);
      if(vote === undefined){
        this.answer.rank += 1;
        this.answerService.updateAnswerRank(true, this.user!.username, this.answer)?.subscribe(() => {
          this.toastrService.success('Upvoted');
        }, error => {
          this.toastrService.error('Something went wrong ' + error);
        }, () => {
          this.memberService.getMember(this.user!.username, true).subscribe(member => {
            this.member = member;
          })
        })
      }else {
        this.toastrService.error('You already upvoted for this answer.');
      }
    } else {
      this.toastrService.error('You must login or register to use this function.');
    }    
  }

  rankDown(){
    if(this.member){
      const vote = this.member.userVotes.find(x => x.answerId === this.answer.id);
      if(vote !== undefined){
        this.answer.rank -= 1;
        this.answerService.updateAnswerRank(false, this.user!.username, this.answer)?.subscribe(() => {
          this.toastrService.success('Downvoted');
        }, error => {
          this.toastrService.error('Something went wrong ' + error);
        }, () => {
          this.memberService.getMember(this.user!.username, true).subscribe(member => {
            this.member = member;
          })
        })
      }else {
        this.toastrService.error('You never upvoted this answer.');
      }
    } else {
      this.toastrService.error('You must login or register to use this function.');
    }  
  }

  acceptAnswer(){
    if(this.member){
      
    } else {
      this.toastrService.error('You must login or register to use this function.');
    }  
  }

  unAcceptAnswer(){
    if(this.member){
      
    } else {
      this.toastrService.error('You must login or register to use this function.');
    }  
  }
}
