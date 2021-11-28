import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Answer } from '../_models/answer';

@Injectable({
  providedIn: 'root'
})
export class AnswerService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createAnswer(answer?: Answer){
    if (answer) {
      return this.http.post(this.baseUrl + "answers/", answer);
    }
    return null;
  }

  getAnswer(id: string) {
    return this.http.get<Answer>(this.baseUrl + 'answers/'+ id);
  }

  updateAnswer(answer?: Answer){
    if (answer) {
      return this.http.put(this.baseUrl + "answers/" + answer.id, answer);
    }
    return null;
  }

  updateBestAnswer(answer?: Answer){
    if (answer) {
      return this.http.put(this.baseUrl + "answers/updatebestanswer/" + answer.id, answer);
    }
    return null;
  }

  updateAnswerRank(upvote: boolean, username: string, answer?: Answer){
    if (answer) {
      return this.http.put(this.baseUrl + "answers/updaterank/" + answer.id + "/" + upvote + "/" + username, answer);
    }
    return null;
  }
}
