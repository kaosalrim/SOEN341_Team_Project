import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Question } from '../_models/question';
import { MemberService } from './member.service';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Question[]> = new PaginatedResult<Question[]>();

  constructor(private http: HttpClient, private memberService: MemberService) {}

  getQuestion(id: string) {
    return this.http.get<Question>(this.baseUrl + 'questions/' + id);
  }

  getQuestions(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== undefined && itemsPerPage !== undefined) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http
      .get<Question[]>(this.baseUrl + 'questions/', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          this.paginatedResult.result = response.body!;
          if (response.headers.get('Pagination') !== null){
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
          }
          return this.paginatedResult;
        })
      );
  }

  getUserQuestions(username: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== undefined && itemsPerPage !== undefined) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Question[]>(
      this.baseUrl + 'questions/user/' + username, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          this.paginatedResult.result = response.body!;
          if (response.headers.get('Pagination') !== null){
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
          }
          return this.paginatedResult;
        })
      );    
  }

  getUserQuestionsAnswered(username: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== undefined && itemsPerPage !== undefined) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Question[]>(
      this.baseUrl + 'questions/user-answered/' + username, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          this.paginatedResult.result = response.body!;
          if (response.headers.get('Pagination') !== null){
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
          }
          return this.paginatedResult;
        })
      ); 
  }

  createQuestion(question: Question) {
    return this.http.post<Question>(this.baseUrl + 'questions/', question);
  }

  updateQuestion(question?: Question) {
    if (question) {
      return this.http.put(this.baseUrl + 'questions/' + question.id, question);
    }
    return null;
  }
}
