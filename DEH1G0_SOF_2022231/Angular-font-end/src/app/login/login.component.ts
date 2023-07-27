import { Component } from '@angular/core';
import {ErrorStateMatcher} from "@angular/material/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MyErrorStateMatcher} from "../_models/form-helpers/my-error-state-matcher";
import {LoginModel} from "../_models/DTOs/loginmodel";
import {TokenModel} from "../_models/tokenmodel";
import {ErrorTextChecker} from "../_models/form-helpers/error-text-checker";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public matcher: ErrorStateMatcher
  public readonly errorTextChecker : ErrorTextChecker
  public loginFormGroup: FormGroup
  private readonly http: HttpClient
  private readonly router: Router
  private readonly snackBar: MatSnackBar
  private readonly formBuilder: FormBuilder



  constructor(http: HttpClient, router: Router, snackBar: MatSnackBar, formBuilder: FormBuilder) {
    this.matcher = new MyErrorStateMatcher()
    this.http = http
    this.router = router
    this.snackBar = snackBar
    this.formBuilder = formBuilder
    this.loginFormGroup = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
    this.errorTextChecker = new ErrorTextChecker()
  }


  SubmitLogin() {

    var loginModel = new LoginModel(
      this.loginFormGroup.get('username')?.value,
      this.loginFormGroup.get('password')?.value
    )

    this.http.post<TokenModel>("https://localhost:7235/api/Auth", loginModel)
      .subscribe({
        next: (success) =>{
          localStorage.setItem('ncore-token', success.token)
          localStorage.setItem('ncore-token-expiration', success.expiration.toString())
          console.log(success)
          this.router.navigate(['/home'])
          },
        error: (error) =>
        {
          console.log(error)
          this.snackBar.open("An error happened, please try again.", "Close", {duration: 5000})
        }
      })
  }
}
