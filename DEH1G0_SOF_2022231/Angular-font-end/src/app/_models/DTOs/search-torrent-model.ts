import { Books } from "../torrent-helpers/books"
import { Games } from "../torrent-helpers/games"
import { Movies } from "../torrent-helpers/movies"
import { Musics } from "../torrent-helpers/musics"
import { Programs } from "../torrent-helpers/programs"
import { Series } from "../torrent-helpers/series"

export class SearchTorrentModel
{
  public SearchText: string = ''
  public Movies: Movies = new Movies()
  public Series: Series = new Series()
  public Musics: Musics = new Musics()
  public Games: Games = new Games()
  public Programs: Programs = new Programs()
  public Books: Books = new Books()

}
