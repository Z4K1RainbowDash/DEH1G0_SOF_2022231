import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BasicUserInfos} from "../_models/DTOs/basic-user-infos";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss']
})
export class ListUsersComponent implements OnInit{
  displayedColumns: string[]
  private readonly http: HttpClient;
  private tableDatas: Array<BasicUserInfos>
  dataSource : MatTableDataSource<BasicUserInfos>


  constructor(http:HttpClient) {
    this.http = http;
    this.displayedColumns = ['Username', 'Email', 'Roles','Actions'];
    this.tableDatas = [];
    this.dataSource = new MatTableDataSource<BasicUserInfos>();
  }

  ngOnInit() {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    this.http.get<Array<BasicUserInfos>>(
      'https://localhost:7235/api/Home/ListUsers',{headers:headers})
      .subscribe({
        next:(success) =>
        {
          console.log(success);
          this.tableDatas = success;
          this.dataSource = new MatTableDataSource(this.tableDatas)
        },
        error:(error) =>
        {
          console.log(error)
        }
      })
  }

}
