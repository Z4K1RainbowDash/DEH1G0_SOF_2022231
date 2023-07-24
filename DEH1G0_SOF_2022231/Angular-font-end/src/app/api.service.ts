import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  router: Router

  constructor(router: Router) {
    this.router = router
  }

  public isLoggedIn(): boolean {
    return this.tokenCheck() && this.dateCheck()
  }

  public canActivate() : boolean {
    if (!this.isLoggedIn()) {
      this.router.navigate(['/login'])
      return false
    }
    return true
  }
  private tokenCheck():boolean
  {
    let token = localStorage.getItem('nikprog-practiceapi-token');
    let expirationDate = localStorage.getItem('nikprog-practiceapi-token-expiration');

    return token!== null && expirationDate!== null;
  }

  private dateCheck(): boolean {

    return true;
    /*
    const expirationDate = localStorage.getItem('nikprog-practiceapi-token-expiration');

    if(expirationDate !== null)
    {
    // Convert JWT expiration to a Date object
    const expirationTime = this.convertJwtExpirationToDate(expirationDate);

    if (!expirationTime) {
      // Invalid or missing expiration time, consider it as expired
      this.removeItemsFromLocalStore()
      return false;
    }

    const currentTime = new Date().getTime();

    // Check if the token has expired
    if (currentTime > expirationTime.getTime()) {
      this.removeItemsFromLocalStore()
      return false;
    }

    return true;
  }
    return false;

     */
  }

  private convertJwtExpirationToDate(expiration: string): Date | null {
    try {
      const expirationTimeInSeconds = parseInt(expiration, 10); // Assuming the expiration is in seconds
      const expirationTimeInMilliseconds = expirationTimeInSeconds * 1000; // Convert to milliseconds

      if (isNaN(expirationTimeInMilliseconds)) {
        throw new Error('Invalid expiration time');
      }

      return new Date(expirationTimeInMilliseconds);
    } catch (error) {
      console.error('Error converting JWT expiration to Date:', error);
      return null;
    }



  }

  private removeItemsFromLocalStore()
  {
    localStorage.removeItem('nikprog-practiceapi-token');
    localStorage.removeItem('nikprog-practiceapi-token-expiration');
  }


}


