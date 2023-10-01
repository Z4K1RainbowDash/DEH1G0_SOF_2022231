import { FormControl} from "@angular/forms";
import {SearchTorrentModel} from "../DTOs/search-torrent-model";
import {Movies} from "../torrent-helpers/movies";
import {Series} from "../torrent-helpers/series";
import {Music} from "../torrent-helpers/music";
import {Games} from "../torrent-helpers/games";
import {Programs} from "../torrent-helpers/programs";
import {Books} from "../torrent-helpers/books";

export class TorrentCategoryFormGroups {
  mainCategoriesFormControl : FormControl;
  moviesCategoriesFormControl : FormControl;
  seriesCategoriesFormControl : FormControl;
  musicCategoriesFormControl : FormControl;
  gamesCategoriesFormControl : FormControl;
  programsCategoriesFormControl : FormControl;
  booksCategoriesFormControl : FormControl;
  private _subcategories : Map<string, FormControl>;

  constructor() {
    this.mainCategoriesFormControl = new FormControl([]);
    this.moviesCategoriesFormControl = new FormControl([]);
    this.seriesCategoriesFormControl = new FormControl([]);
    this.musicCategoriesFormControl = new FormControl([]);
    this.gamesCategoriesFormControl = new FormControl([]);
    this.programsCategoriesFormControl = new FormControl([]);
    this.booksCategoriesFormControl = new FormControl([]);

    this._subcategories = new Map<string, FormControl>([
      ['movies', this.moviesCategoriesFormControl],
      ['series', this.seriesCategoriesFormControl],
      ['music', this.musicCategoriesFormControl],
      ['games', this.gamesCategoriesFormControl],
      ['programs', this.programsCategoriesFormControl],
      ['books', this.booksCategoriesFormControl]
    ]);
  }

  createSearchTorrentModel(categories: Array<string> ,searchText: string): SearchTorrentModel
  {
    const searchTorrentModel = this.buildSearchTorrentModel()
    searchTorrentModel.SearchText = searchText
    this.setIsSelectedFields(searchTorrentModel, categories)

    return searchTorrentModel
  }
  private buildSearchTorrentModel()
  {
    const model: SearchTorrentModel = new SearchTorrentModel()

    model.Movies = this.buildMovies();
    model.Series = this.buildSeries();
    model.Music = this.buildMusic();
    model.Games = this.buildGames();
    model.Programs = this.buildPrograms();
    model.Books = this.buildBooks();

    return model
  }

  private buildMovies():Movies{
    const movies = new Movies()
    movies.SdHu = this.isSubcategorySelected('movies','SdHu');
    movies.SdEn = this.isSubcategorySelected('movies','SdEn');
    movies.DvdrHu = this.isSubcategorySelected('movies','DvdrHu');
    movies.DvdrEn = this.isSubcategorySelected('movies','DvdrEn');
    movies.Dvd9Hu = this.isSubcategorySelected('movies','Dvd9Hu');
    movies.Dvd9En = this.isSubcategorySelected('movies','Dvd9En');
    movies.HdHu = this.isSubcategorySelected('movies','HdHu');
    movies.HdEn = this.isSubcategorySelected('movies','HdEn');

    return movies
  }

  private buildSeries(): Series{

    const series= new Series();

    series.SdHu = this.isSubcategorySelected('series','SdHu');
    series.SdEn = this.isSubcategorySelected('series','SdEn');
    series.DvdrHu = this.isSubcategorySelected('series','DvdrHu');
    series.DvdrEn = this.isSubcategorySelected('series','DvdrEn');
    series.HdHu = this.isSubcategorySelected('series','HdHu');
    series.HdEn = this.isSubcategorySelected('series','HdEn');

    return series;
  }

  private buildMusic(){
    const music = new Music()

    music.Mp3Hu = this.isSubcategorySelected('music','Mp3Hu');
    music.Mp3En = this.isSubcategorySelected('music','Mp3En');
    music.LosslessHu = this.isSubcategorySelected('music','LosslessHu');
    music.LosslessEn = this.isSubcategorySelected('music','LosslessEn');
    music.Clip = this.isSubcategorySelected('music','Clip');

    return music

  }


  private  buildGames():Games{

    const games = new Games();

    games.Iso = this.isSubcategorySelected('games','Iso');
    games.Rip = this.isSubcategorySelected('games','Rip');
    games.Console = this.isSubcategorySelected('games','Console');

    return games;
  }

  private buildPrograms():Programs
  {
    const programs = new Programs();

    programs.Iso = this.isSubcategorySelected('programs','Iso');
    programs.Rip = this.isSubcategorySelected('programs','Rip');
    programs.Mobile = this.isSubcategorySelected('programs','Mobile');

    return programs;
  }

  private buildBooks()
  {
    const books = new Books();
    books.EBookHu = this.isSubcategorySelected('books','EBookHu');
    books.EBookEn = this.isSubcategorySelected('books','EBookEn');

    return books;
  }


  private setIsSelectedFields(searchTorrentModel: SearchTorrentModel, categories: Array<string>) {
    categories.forEach(
      (value) => this.setIsSelectedCategory(value, searchTorrentModel)
    )
  }

  private setIsSelectedCategory(category:string, searchTorrentModel: SearchTorrentModel)
  {
    // TODO rewrite
    switch(category) {
      case 'movies': {
        searchTorrentModel.Movies.IsSelected = true
        break;
      }

      case 'series': {
        searchTorrentModel.Series.IsSelected = true
        break;
      }

      case 'music': {
        searchTorrentModel.Music.IsSelected = true
        break;
      }

      case 'games': {
        searchTorrentModel.Games.IsSelected = true
        break;
      }

      case 'programs': {
        searchTorrentModel.Programs.IsSelected = true
        break;
      }

      case 'books': {
        searchTorrentModel.Books.IsSelected = true
        break;
      }

      default: {
        //TODO log;
        break;
      }
    }
  }

  public isSubcategorySelected(category: string, subcategory: string): boolean {
    return (this._subcategories.get(category)?.value.indexOf(subcategory) !== -1) || false;
  }
  public areAnySelectedSubcategoriesEmpty(): boolean {
    let result: boolean = false;
    const mainCategories: [string] = this.mainCategoriesFormControl.value;
    let index = 0;
    while (mainCategories.length !== index && !result) {
      let subcategoryForm = this._subcategories.get(mainCategories[index]);
      if (subcategoryForm?.value.length === 0) {
        result = true;
      }
      index++;
    }

    return result;
  }

  public isMainCategorySelected(category:string):boolean
  {
    let x:[string] = this.mainCategoriesFormControl.value
    return x.indexOf(category) !== -1
  }

}
