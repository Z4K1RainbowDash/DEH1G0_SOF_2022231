import {DefaultUserInfo} from "./default-user-info";

export class UserModel
{
  public readonly defaultUserInfo:DefaultUserInfo
  public readonly emailConfirmed: boolean
  public readonly accessFailedCount:number
  public readonly role:string

  constructor(defaultUserInfo: DefaultUserInfo, emailConfirmed:boolean, accessFailedCount:number, role:string) {
    this.emailConfirmed = emailConfirmed
    this.defaultUserInfo = defaultUserInfo
    this.accessFailedCount = accessFailedCount
    this.role = role

  }


}
