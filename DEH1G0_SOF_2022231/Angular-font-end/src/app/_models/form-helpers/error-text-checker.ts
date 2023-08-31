import {FormGroup} from "@angular/forms";

export class ErrorTextChecker{
  isFormGroupFieldEmpty(formGroup:FormGroup, formFieldName:string):boolean {

    const control = formGroup.get(formFieldName);
    const result = control ? control.hasError('required') : null;
    return result !== null ? result : true;
  }

  isFormGroupFieldValid(formGroup: FormGroup, formFieldName: string, validationError:string): boolean {
    const control = formGroup.get(formFieldName);

    if (!control) {
      return false;
    }

    return control.value && control.hasError(validationError);
  }

}
