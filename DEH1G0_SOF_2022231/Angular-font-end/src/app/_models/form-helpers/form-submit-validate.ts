
import { FormGroup } from '@angular/forms';

export class FormSubmitValidate {

  validateFormGroup(formGroup: FormGroup): boolean {
    let isValid = true;

    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);

      if (control) {

        if (control.value === null || control.value === '') {
          isValid = false;
        }

        if (control.errors !== null) {
          isValid = false;
        }

      } else {
        isValid = false;
      }
    });

    return isValid;
  }
}
