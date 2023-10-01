import {AbstractControl, FormGroup} from "@angular/forms";

export class ErrorTextChecker{
  isFormGroupFieldEmpty(formGroup:FormGroup, formFieldName:string):boolean {

    const control = formGroup.get(formFieldName);
    const result = control ? control.hasError('required') : null;
    return result !== null ? result : true;
  }

  isFormGroupFieldValid(formGroup: FormGroup, formFieldName: string, validationError:string): boolean {
    const control = formGroup.get(formFieldName);
    return this.isFormControlinvalid(control, validationError)
  }

  isFormControlinvalid(formControl:AbstractControl<any,any> | null, validationError:string):boolean
  {
    let result: boolean;

    if(!formControl){
      result = false;
    }
    else {
      result = formControl.value && formControl.hasError(validationError)
    }
    return result;
  }

  isFormControlEmpty(formControl:AbstractControl<any,any> | null): boolean
  {
    const result = formControl ? formControl.hasError('required') : null;
    return result !== null ? result : true;
  }



}
