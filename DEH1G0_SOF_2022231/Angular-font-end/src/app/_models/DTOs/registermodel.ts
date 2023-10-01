export class RegisterModel{
  public readonly FirstName: string
  public readonly LastName: string
  public readonly Email: string
  public readonly Username: string
  public readonly Password: string

  constructor(firstName:string,lastName:string, email:string, username: string, password:string) {
    this.FirstName = firstName
    this.LastName = lastName
    this.Email = email
    this.Username = username
    this.Password = password
  }
}
