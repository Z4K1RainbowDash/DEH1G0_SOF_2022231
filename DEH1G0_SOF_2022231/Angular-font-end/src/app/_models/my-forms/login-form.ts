import {FormControl, Validators} from "@angular/forms";

export class LoginForm
{
    public usernameFormControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
    public passwordFormControl: FormControl = new FormControl('', [Validators.required, Validators.minLength(8)])

}
