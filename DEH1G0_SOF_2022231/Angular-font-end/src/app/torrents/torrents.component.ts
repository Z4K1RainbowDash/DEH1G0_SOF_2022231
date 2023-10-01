import {Component} from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";
import { FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {TorrentCategoryFormGroups} from "../_models/form-helpers/torrent-category-form-groups";
import {ErrorStateMatcher} from "@angular/material/core";
import {MyErrorStateMatcher} from "../_models/form-helpers/my-error-state-matcher";
import {SearchTorrentModel} from "../_models/DTOs/search-torrent-model";
import {TorrentModel} from "../_models/DTOs/torrent-model";
import {MatTableDataSource} from "@angular/material/table";
import {TorrentService} from "../torrent.service";
import {ErrorTextChecker} from "../_models/form-helpers/error-text-checker";



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
  public errorTextChecker: ErrorTextChecker


  // table
  dataSource : MatTableDataSource<TorrentModel>
  displayedColumns: string[]

  // torrent
  private readonly torrentService: TorrentService

  // basics
  private readonly snackBar: MatSnackBar


  constructor(snackBar: MatSnackBar, formBuilder: FormBuilder, torrentService: TorrentService) {
    this.matcher = new MyErrorStateMatcher()
    this.snackBar = snackBar
    this.categoryFormControl = new FormControl([]);
    this.searchTextFormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
    this.torrentCategoryFormGroups = new TorrentCategoryFormGroups();
    this.dataSource = new MatTableDataSource<TorrentModel>();
    this.displayedColumns = ['Name', 'Date', 'Size','Downloads', 'Seeders', 'Leechers', 'Action'];
    this.torrentService = torrentService
    this.errorTextChecker = new ErrorTextChecker()

    this.dataSource.data = [new TorrentModel('veryId','namexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx','n','2020-10-20 XX-yy-ZZ', '10Gb', '100','10','20'), new TorrentModel('veryId','namexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxzzzzzzzzzzzzzzzzzzzzz','n','2020-10-20', '10Gb', '100','10','20')]
  }

  handleDownloadButtonClick(torrentId: string, name: string): void {
    const replacedName = name.replaceAll(' ', '_')

    this.torrentService.downloadTorrentById(torrentId, replacedName)
      .subscribe(
        {
          next:(success) =>{

              this.downloadFile(success, name);
          },
          error:(error) => {
            console.log(error)
          }
        }
    );
  }

  private downloadFile(blob: Blob, name: string)
  {
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = name + '.torrent';
    link.click();
    window.URL.revokeObjectURL(url); // Cleanup and release the object URL
  }
  private searchTorrents(dto: SearchTorrentModel)
  {
    this.torrentService.getTorrentsByTorrentModelDTO(dto)
      .subscribe(
      {
        next: (success) =>{
          this.dataSource.data = success
        },
        error:(error) => {
          console.log(error)
        }
      }
    )
  }

  torrentsIsEmpty():boolean
  {
    return !this.dataSource.data || this.dataSource.data.length === 0;
  }
  sendUserSearchChoice():void
  {
    console.log(this.categoryFormControl.value)
    console.log(this.categoryFormControl.value.length)
      var newDto = this.torrentCategoryFormGroups.createSearchTorrentModel(this.categoryFormControl.value,this.searchTextFormControl.value);
      console.log(newDto)
      this.searchTorrents(newDto)
  }

  isSendValid(){
    return !this.areCategoriesEmpty() && this.isSearchFieldValid()
  }


  private areCategoriesEmpty():boolean {
    // TODO
    let isEmpty = this.torrentCategoryFormGroups.mainCategoriesFormControl.value.length === 0;
    if(isEmpty)
    {
      return true;
    }
    return this.torrentCategoryFormGroups.areAnySelectedSubcategoriesEmpty();
  }

  private isSearchFieldValid():boolean{
    let _isFormControlValid = !this.errorTextChecker.isFormControlinvalid(this.searchTextFormControl,'minlength')
    let _isFormControlNotEmpty = !this.errorTextChecker.isFormControlEmpty(this.searchTextFormControl);
    return _isFormControlValid && _isFormControlNotEmpty;
  }

  private hasAnyFormControlTrueValueFromFormGroup(formGroup: FormGroup<any>): boolean {
    console.log(formGroup)
    let hasTrueValue: boolean = false
    const keys = Object.keys(formGroup.controls);
    let index = 0;

    while (index < keys.length && !hasTrueValue) {
      const key = keys[index];
      const control = formGroup.get(key);
      if(control && control.value === true){
        hasTrueValue = true;
      }
      index++;
    }
    return hasTrueValue;
  }

  public isMainCategorySelected(category:string):boolean
  {
    let x:[string] = this.torrentCategoryFormGroups.mainCategoriesFormControl.value
    return x.indexOf(category) !== -1
  }
}
