import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Answer } from '../_models/answer';
import { Question } from '../_models/question';
import { MemberService } from './member.service';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  baseUrl = environment.apiUrl;
  questions: Question[] = [];

  constructor(private http: HttpClient, private memberService: MemberService) { }

  getQuestion(id: string) {
    const question = this.questions.find(x => x.id === +id);
    if(question !== undefined) return of(question);

    return this.http.get<Question>(this.baseUrl + 'questions/'+ id);
  }

  getQuestions() {
    if (this.questions.length > 0) {
      return of(this.questions);
    }
    return this.http.get<Question[]>(this.baseUrl + 'questions/').pipe(
      map(questions => {
        this.questions = questions;
        return questions;
      })
    );
  }

  getUserQuestions(username: string){
    if (this.questions.length > 0) {
      const qs = this.questions.filter(x => x.username === username);
      if(qs.length > 0){
        return of(qs);
      }
    }
    return this.http.get<Question[]>(this.baseUrl + 'questions/user/' + username);
  }

  getUserQuestionsAnswered(username: string){
    return this.http.get<Question[]>(this.baseUrl + 'questions/user-answered/' + username);
  }

  createQuestion(question: Question){
      return this.http.post<Question>(this.baseUrl + "questions/" , question);
  }

  updateQuestion(question?: Question){
    if (question) {
      return this.http.put(this.baseUrl + "questions/" + question.id, question).pipe(map(() => {
        const index = this.questions.indexOf(question);
        if(index !== -1)
          this.questions[index] = question;
      }));
    }
    return null;
  }

  updateQuestionsAnswers(answer? : Answer){
    if(answer){
      const question = this.questions.find(x => x.id = answer.questionId);
      if(question !== undefined){
        const qIndex = this.questions.indexOf(question);
        const index = question.answers.indexOf(answer);
        if (index !== -1){
          question.answers[index] = answer;
          this.questions[qIndex] = question;
        }
      }
    }
  }
}
