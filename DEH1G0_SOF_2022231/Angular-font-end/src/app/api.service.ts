import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {JwtHelperService} from '@auth0/angular-jwt'

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  jwtHelper: JwtHelperService
  router: Router
  readonly baseUrl: string = 'https://localhost:7235/api';

  constructor(router: Router, jwtHelper: JwtHelperService) {
    this.router = router
    this.jwtHelper = jwtHelper
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
    let token = localStorage.getItem('ncore-token');
    let expirationDate = localStorage.getItem('ncore-token-expiration');

    return token!== null && expirationDate!== null;
  }

  private dateCheck(): boolean {
    let result:boolean = true;
    let token = localStorage.getItem('ncore-token');
    if (this.jwtHelper.isTokenExpired(token)) {
      result = false;
    }

    return result;
  }



  private removeItemsFromLocalStore()
  {
    localStorage.removeItem('ncore-token');
    localStorage.removeItem('ncore-token-expiration');
  }


}


