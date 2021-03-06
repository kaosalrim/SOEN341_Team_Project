import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'The WEB Q&A';
  users: any;

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    //get the user from local storage if any
    const userJson = localStorage.getItem('user');
    const user: User = userJson !== null ? JSON.parse(userJson) : null;
    this.accountService.setCurrentUser(user);
  }
}
