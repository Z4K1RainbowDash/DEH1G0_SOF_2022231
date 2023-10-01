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
  //private readonly torrentCategories: TorrentCategories


  constructor(snackBar: MatSnackBar, formBuilder: FormBuilder, torrentService: TorrentService) {
    this.matcher = new MyErrorStateMatcher()
    this.snackBar = snackBar
    this.categoryFormControl = new FormControl([]);
    this.searchTextFormControl = new FormControl('', [Validators.required, Validators.minLength(3)])
    this.torrentCategoryFormGroups = new TorrentCategoryFormGroups();
    this.dataSource = new MatTableDataSource<TorrentModel>();
    this.displayedColumns = ['Name', 'Date', 'Size','Downloads', 'Seeders', 'Leechers', 'Action'];
    this.torrentService = torrentService
  }

  handleDownloadButtonClick(torrentId: string, name: string): void {
    // Do something with the param
    console.log('torrentId & name:')
    console.log(torrentId + ' ' + name);

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

    if(this.sendCheck())
    {
      var newDto = this.torrentCategoryFormGroups.createSearchTorrentModel(this.categoryFormControl.value,this.searchTextFormControl.value);
      console.log(newDto)

      this.searchTorrents(newDto)
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
    // TODO
    return true;
  }

  private checkSearchField() {
    // TODO
    return true;
  }


  /*
  private sendSearchTorrentModelDto(dto: SearchTorrentModel) {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('ncore-token')
    })

    this.http.post<Array<TorrentModel>>('https://localhost:7235/api/Torrents/SearchTorrent', dto,{headers:headers})
      .subscribe(
        {
          next: (success) =>{
            console.log(success)
            this.dataSource.data = success
            console.log('torrentsIsEmpty:'+ this.torrentsIsEmpty())
            console.log('!torrentsIsEmpty:'+ !this.torrentsIsEmpty())
            console.log(this.dataSource)
            console.log(this.dataSource.data)

          },
          error:(error) => {
            console.log(error)
          }
        }
      )
  }
*/
}
