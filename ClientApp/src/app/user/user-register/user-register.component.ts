import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, ValidationErrors,ValidatorFn } from '@angular/forms';
import { UserForRegister } from '../../model/user';
import { AlertifyService } from '../../services/alerify.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';




@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css'
})
export class UserRegisterComponent implements OnInit {
  
  registrationForm: FormGroup = this.fb.group({});
  user: UserForRegister = {} as UserForRegister;
  userSubmitted: boolean = false;
  constructor(private fb: FormBuilder,
              private authService: AuthService,
              private alertify: AlertifyService,
              private router: Router ) { }

  ngOnInit() {
    this.createRegistrationForm();
  }
  createRegistrationForm() {
    this.registrationForm =  this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(5)]],
        confirmPassword: ['', Validators.required]
    }, {validators: this.passwordMatchingValidator});
  }

  // passwordMatchingValidatior(fg: FormGroup): Validators {
  //   return fg.get('password').value === fg.get('confirmPassword').value ? null :
  //       {notmatched: true};
  // }
  passwordMatchingValidator(fg: FormGroup): ValidationErrors | null {
    const passwordControl = fg.get('password');
    const confirmPasswordControl = fg.get('confirmPassword');

    if (passwordControl && confirmPasswordControl) {
        const password = passwordControl.value;
        const confirmPassword = confirmPasswordControl.value;

        if (password !== confirmPassword) {
            return { notmatched: true };
        }
    }

    return null;
  }
  onSubmit() {
    console.log(this.registrationForm.value);
    this.userSubmitted = true;

    if (this.registrationForm.valid) {
        // this.user = Object.assign(this.user, this.registerationForm.value);
        this.authService.registerUser(this.userData()).subscribe(() =>
        {
            this.onReset();
            this.alertify.success('Congrats, you are successfully registered');
            this.router.navigate(['/']);
        });
    }
  }

  onReset() {
    this.userSubmitted = false;
    this.registrationForm.reset();
}


  userData(): UserForRegister {
    return this.user = {
        email: this.email.value,
        password: this.password.value
    };
  }




  get email() {
    return this.registrationForm.get('email') as FormControl;
  }
  get password() {
    return this.registrationForm.get('password') as FormControl;
  }
  get confirmPassword() {
    return this.registrationForm.get('confirmPassword') as FormControl;
}

}
