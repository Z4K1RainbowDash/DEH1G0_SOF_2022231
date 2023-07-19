import {Component} from '@angular/core';
import {ErrorStateMatcher} from '@angular/material/core';
import {RegisterModel} from "../_models/registermodel";
import {MyErrorStateMatcher} from "../_models/my-error-state-matcher";
import {RegisterForm} from "../_models/my-forms/register-form";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent{
  registerModel: RegisterModel
  matcher: ErrorStateMatcher
  registerForm: RegisterForm

  constructor() {
    this.registerModel = new RegisterModel()
    this.matcher = new MyErrorStateMatcher()
    this.registerForm = new RegisterForm()
  }

}

