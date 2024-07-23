import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserServiceService } from '../services/user-service.service';
import { UserStoreService } from '../services/user-store.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(
    private userService: UserServiceService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(8),
      ]),
    });
  }
  onLogin() {
    console.log(this.loginForm);
    const email = this.loginForm.controls['email'].value;
    const password = this.loginForm.controls['password'].value;
    this.userService.loginService({ email, password }).subscribe(
      (data) => {
        console.log(data);
        this.userService.storeToken(data.token);
        this.router.navigateByUrl('/dashboard');
      },
      (error) => {
        console.log(error);
        alert('Something went wrong!');
      }
    );
  }
}
