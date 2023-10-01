export class LoginModel
{
  public readonly Username:string = ''
  public readonly Password:string = ''

  constructor(username:string, password:string) {
    this.Username = username
    this.Password = password
  }
}
