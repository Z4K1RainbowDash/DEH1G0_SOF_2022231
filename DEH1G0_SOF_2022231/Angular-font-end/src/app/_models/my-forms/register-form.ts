import {FormControl, Validators} from "@angular/forms";

export class RegisterForm
{
  public firstNameFormControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
  public lastNameFormControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
  public emailFormControl: FormControl = new FormControl('', [Validators.required, Validators.email])
  public userNameFormControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
  public passwordFormControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(8)])

}
