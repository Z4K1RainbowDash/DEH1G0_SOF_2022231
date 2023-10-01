import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {HomeComponent} from "./home/home.component";
import {LoginComponent} from "./login/login.component";
import {LogoutComponent} from "./logout/logout.component";
import {RegisterComponent} from "./register/register.component";
import {ListUsersComponent} from "./list-users/list-users.component";
import {TorrentsComponent} from "./torrents/torrents.component";
import {ApiService} from "./api.service";


const routes: Routes = [

  { path: 'home', component: HomeComponent},
  { path: 'torrents', component: TorrentsComponent, canActivate: [ApiService]},
  { path: 'login', component: LoginComponent},
  { path: 'logout', component: LogoutComponent, canActivate: [ApiService]},
  { path: 'register', component: RegisterComponent},
  { path: 'list-users', component: ListUsersComponent, canActivate: [ApiService]},
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
