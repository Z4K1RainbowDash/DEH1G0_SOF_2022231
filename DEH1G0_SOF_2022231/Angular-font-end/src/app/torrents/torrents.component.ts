import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatSnackBar} from "@angular/material/snack-bar";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {TorrentCategoryFormGroups} from "../_models/form-helpers/torrent-category-form-groups";
import {ErrorStateMatcher} from "@angular/material/core";
import {MyErrorStateMatcher} from "../_models/form-helpers/my-error-state-matcher";


@Component({
  selector: 'app-torrents',
  templateUrl: './torrents.component.html',
  styleUrls: ['./torrents.component.scss']
})
export class TorrentsComponent {

  // form
  public categoryFormControl: FormControl
  public searchTextFormControl : FormControl
  public matcher: ErrorStateMatcher
  public torrentCategoryFormGroups: TorrentCategoryFormGroups // torrent subcategories form groups
  private readonly formBuilder: FormBuilder

  // basics
  private readonly http:HttpClient
  private readonly snackBar: MatSnackBar
  //private readonly torrentCategories: TorrentCategories


  constructor(http:HttpClient, snackBar: MatSnackBar, formBuilder: FormBuilder) {
    this.http = http
    this.matcher = new MyErrorStateMatcher()
    this.formBuilder = formBuilder
    this.snackBar = snackBar
    //this.torrentCategories = new TorrentCategories();
    this.categoryFormControl = new FormControl([]);
    this.searchTextFormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
    this.torrentCategoryFormGroups = new TorrentCategoryFormGroups(this.formBuilder);
  }

  sendUserSearchChoice():void
  {
    console.log(this.categoryFormControl.value)

    if(this.sendCheck())
    {
      var newDto = this.torrentCategoryFormGroups.createSearchTorrentModel(this.categoryFormControl.value,this.searchTextFormControl.value);
      console.log(newDto)

      this.sendDto()
    }
    else
    {
      // TODO: error snackBar
    }
  }

  sendCheck(){

    return this.checkCategories() && this.checkSearchField()

  }


  private checkCategories():boolean {
    return true;
  }

  private checkSearchField() {
    return true;
  }

  private createDto() {
    console.log('search text:' + this.searchTextFormControl.value)
  }

  private sendDto() {

  }
}
