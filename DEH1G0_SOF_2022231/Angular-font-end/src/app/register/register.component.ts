import {Component} from '@angular/core';
import {ErrorStateMatcher} from '@angular/material/core';
import {RegisterModel} from "../_models/DTOs/registermodel";
import {MyErrorStateMatcher} from "../_models/form-helpers/my-error-state-matcher";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ErrorTextChecker} from "../_models/form-helpers/error-text-checker";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent{

  public matcher: ErrorStateMatcher
  public registerFormGroup: FormGroup
  public readonly errorTextChecker : ErrorTextChecker
  private readonly http: HttpClient
  private readonly router: Router
  private readonly snackBar: MatSnackBar
  private readonly formBuilder: FormBuilder

  constructor(http:HttpClient,router:Router, snackBar:MatSnackBar, formBuilder: FormBuilder) {
    this.matcher = new MyErrorStateMatcher()
    this.http = http
    this.router = router
    this.snackBar = snackBar
    this.formBuilder = formBuilder
    this.registerFormGroup = this.formBuilder.group({
      firstName : ['',[Validators.required, Validators.minLength(3)]],
      lastName : ['',[Validators.required, Validators.minLength(3)]],
      username : ['',[Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
    this.errorTextChecker = new ErrorTextChecker()
  }

  LogFunction() {


      var registerModel  = new RegisterModel(
        this.registerFormGroup.get('firstName')?.value,
        this.registerFormGroup.get('lastName')?.value,
        this.registerFormGroup.get('email')?.value,
        this.registerFormGroup.get('username')?.value,
        this.registerFormGroup.get('password')?.value,
        )


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
          console.log(error)
          this.snackBar.open("An error happened, please try again.", "Close", {duration: 5000})
        }
      });

  }
}

