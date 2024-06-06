import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AlertifyService } from '../../services/alerify.service';
import { Router } from '@angular/router';
import { UserForLogin } from '../../model/user';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    console.log(loginForm.value);
    //const token = this.authService.authUser(loginForm.value);
    this.authService.authUser(loginForm.value).subscribe(
      (response) => {
          const user = response as UserForLogin
          console.log(response);
          if (user) {
              localStorage.setItem('token', user.token);
              localStorage.setItem('email', user.email);
              this.alertify.success('Login Successful');
              this.router.navigate(['/']);
          }
      },
      (error: any) => {
          // Handle error
          console.error(error);
          // For example, display an error message
          this.alertify.error('Login Failed');
      }
  );
  }
}
