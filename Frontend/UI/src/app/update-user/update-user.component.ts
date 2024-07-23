import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../services/user-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css'],
})
export class UpdateUserComponent implements OnInit {
  userDetail: any;
  userId: any;
  userForm!: FormGroup;

  constructor(
    private userService: UserServiceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.userForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      userName: new FormControl('', [Validators.required]),
      amount: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((param) => {
      this.userId = param['id'];
    });

    this.userService.getUserIdForAdmin(this.userId).subscribe(
      (res) => {
        this.userDetail = res;
        this.initializeForm(this.userDetail);
      },
      (err) => {
        alert('Something went wrong');
      }
    );
  }

  initializeForm(userDetail: any) {
    this.userForm.patchValue({
      email: userDetail.email,
      userName: userDetail.userName,
      amount: userDetail.amount,
    });
  }

  onUpdate() {
    const user = this.userForm.value;
    this.userService.updateUser(user, this.userId).subscribe(
      (res) => {
        this.router.navigateByUrl('/dashboard');
      },
      (err) => {
        alert('Something went wrong!');
      }
    );
  }
}
