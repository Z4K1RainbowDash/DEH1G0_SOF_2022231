import {Component} from '@angular/core';
import {ErrorStateMatcher} from '@angular/material/core';
import {RegisterModel} from "../_models/registermodel";
import {MyErrorStateMatcher} from "../_models/my-error-state-matcher";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {RegisterForm} from "../_models/my-forms/register-form";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent{

  matcher: ErrorStateMatcher
  registerForm: RegisterForm
  http: HttpClient
  router: Router
  snackBar: MatSnackBar

  constructor(http:HttpClient,router:Router, snackBar:MatSnackBar) {
    this.matcher = new MyErrorStateMatcher()
    this.registerForm = new RegisterForm()
    this.http = http
    this.router = router
    this.snackBar = snackBar
  }

  LogFunction() {
    var registerModel: RegisterModel = new RegisterModel()
    registerModel.email = this.registerForm.emailFormControl.getRawValue()
    registerModel.firstName = this.registerForm.firstNameFormControl.getRawValue()
    registerModel.lastName = this.registerForm.lastNameFormControl.getRawValue()
    registerModel.userName = this.registerForm.userNameFormControl.getRawValue()
    registerModel.password = this.registerForm.passwordFormControl.getRawValue()

    console.log(registerModel)

    this.http.put("https://localhost:7235/api/Auth", registerModel)
      .subscribe({
        next: (success) => {
          this.snackBar.open("Registration was successful!", "Close", {duration: 5000})
            .afterDismissed()
            .subscribe(() => {
              this.router.navigate(['/login'])
            })
        },
        error: (error) => {
          this.snackBar.open("An error happened, please try again.", "Close", {duration: 5000})
        }
      });

  }
}

