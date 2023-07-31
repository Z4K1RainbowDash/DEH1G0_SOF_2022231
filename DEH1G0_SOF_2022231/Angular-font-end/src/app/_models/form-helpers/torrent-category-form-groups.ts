import {FormBuilder, FormGroup} from "@angular/forms";
import {SearchTorrentModel} from "../DTOs/search-torrent-model";
import {Movies} from "../torrent-helpers/movies";
import {Series} from "../torrent-helpers/series";
import {Musics} from "../torrent-helpers/musics";
import {Games} from "../torrent-helpers/games";
import {Programs} from "../torrent-helpers/programs";
import {Books} from "../torrent-helpers/books";

export class TorrentCategoryFormGroups {

  // Subcategory CHECKBOXES
  MoviesCheckboxes : FormGroup
  SeriesCheckboxes : FormGroup
  MusicsCheckboxes : FormGroup
  GamesCheckboxes : FormGroup
  ProgramsCheckboxes : FormGroup
  BooksCheckboxes : FormGroup

  private readonly formBuilder:FormBuilder


  constructor(formBuilder: FormBuilder) {
    this.formBuilder = formBuilder

    this.MoviesCheckboxes = this.formBuilder.group({
      SdHu: false,
      SdEn: false,
      DvdrHu: false,
      DvdrEn: false,
      Dvd9Hu: false,
      Dvd9En: false,
      HdHu: false,
      HdEn: false
    })

    this.SeriesCheckboxes = this.formBuilder.group({
      SdHu: false,
      SdEn: false,
      DvdrHu: false,
      DvdrEn: false,
      HdHu: false,
      HdEn: false
    })

    this.MusicsCheckboxes = this.formBuilder.group({
      Mp3Hu: false,
      Mp3En: false,
      LosslessHu: false,
      LosslessEn: false,
      Clip: false
    })

    this.GamesCheckboxes = this.formBuilder.group({
      Iso: false,
      Rip: false,
      Console: false
    })

    this.ProgramsCheckboxes = this.formBuilder.group({
      Iso: false,
      Rip: false,
      Mobile: false
    })

    this.BooksCheckboxes = this.formBuilder.group({
      EBookHu: false,
      EBookEn: false
    })

  }

  createSearchTorrentModel(categories: Array<string> ,searchText: string): SearchTorrentModel
  {


    const searchTorrentModel = this.buildSearchTorrentModel()

    searchTorrentModel.SearchText = searchText

    this.setIsSelectedFields(searchTorrentModel, categories, searchText)



    return searchTorrentModel
  }
  private buildSearchTorrentModel()
  {
    const model: SearchTorrentModel = new SearchTorrentModel()

    model.Movies = this.buildMovies();
    model.Series = this.buildSeries();
    model.Musics = this.buildMusics();
    model.Games = this.buildGames();
    model.Programs = this.buildPrograms();
    model.Books = this.buildBooks();

    return model
  }

  private buildMovies():Movies{
    const movies = new Movies()

    movies.SdHu = this.MoviesCheckboxes.get('SdHu')?.value;
    movies.SdEn = this.MoviesCheckboxes.get('SdEn')?.value;
    movies.DvdrHu = this.MoviesCheckboxes.get('DvdrHu')?.value;
    movies.DvdrEn = this.MoviesCheckboxes.get('DvdrEn')?.value;
    movies.Dvd9Hu = this.MoviesCheckboxes.get('Dvd9Hu')?.value;
    movies.Dvd9En = this.MoviesCheckboxes.get('Dvd9En')?.value;
    movies.HdHu = this.MoviesCheckboxes.get('HdHu')?.value;
    movies.HdEn = this.MoviesCheckboxes.get('HdEn')?.value;

    return movies
  }

  private buildSeries(): Series{

    const series= new Series();

    series.SdHu = this.SeriesCheckboxes.get('SdHu')?.value;
    series.SdEn = this.SeriesCheckboxes.get('SdEn')?.value;
    series.DvdrHu = this.SeriesCheckboxes.get('DvdrHu')?.value;
    series.DvdrEn = this.SeriesCheckboxes.get('DvdrEn')?.value;
    series.HdHu = this.SeriesCheckboxes.get('HdHu')?.value;
    series.HdEn = this.SeriesCheckboxes.get('HdEn')?.value;

    return series;
  }

  private buildMusics(){
    const musics = new Musics()

    musics.Mp3Hu = this.MusicsCheckboxes.get('Mp3Hu')?.value;
    musics.Mp3En = this.MusicsCheckboxes.get('Mp3En')?.value;
    musics.LosslessHu = this.MusicsCheckboxes.get('LosslessHu')?.value;
    musics.LosslessEn = this.MusicsCheckboxes.get('LosslessEn')?.value;
    musics.Clip = this.MusicsCheckboxes.get('Clip')?.value;

    return musics

  }


  private  buildGames():Games{

    const games = new Games();

    games.Iso = this.GamesCheckboxes.get('Iso')?.value;
    games.Rip = this.GamesCheckboxes.get('Rip')?.value;
    games.Console = this.GamesCheckboxes.get('Console')?.value;

    return games;
  }

  private buildPrograms():Programs
  {
    const programs = new Programs();

    programs.Iso = this.ProgramsCheckboxes.get('Iso')?.value;
    programs.Rip = this.ProgramsCheckboxes.get('Rip')?.value;
    programs.Mobile = this.ProgramsCheckboxes.get('Mobile')?.value;

    return programs;
  }

  private buildBooks()
  {
    const books = new Books();
    books.EBookHu = this.BooksCheckboxes.get('EBookHu')?.value;
    books.EBookEn = this.BooksCheckboxes.get('EBookEn')?.value;

    return books;
  }


  private setIsSelectedFields(searchTorrentModel: SearchTorrentModel, categories: Array<string>, searchText: string) {
    // TODO
  }
}
