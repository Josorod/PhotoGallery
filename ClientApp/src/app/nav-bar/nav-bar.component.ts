import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AlertifyService } from '../services/alerify.service';
import { UserForLogin } from '../model/user';


@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  loggedinUser: string = '';
  userString: string ='';

  constructor(private alertify: AlertifyService) { }

  ngOnInit() {
  }

  loggedin(): string {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      return user && user.email ? user.email : '';
    } else {
      return '';
    }
  }

  onLogout() {
    localStorage.removeItem('user');
    this.alertify.success('You are logged out!');
  }
}
