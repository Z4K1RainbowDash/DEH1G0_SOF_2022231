import { Books } from "../torrent-helpers/books"
import { Games } from "../torrent-helpers/games"
import { Movies } from "../torrent-helpers/movies"
import { Music } from "../torrent-helpers/music"
import { Programs } from "../torrent-helpers/programs"
import { Series } from "../torrent-helpers/series"

export class SearchTorrentModel
{
  public SearchText: string = ''
  public Movies: Movies = new Movies()
  public Series: Series = new Series()
  public Music: Music = new Music()
  public Games: Games = new Games()
  public Programs: Programs = new Programs()
  public Books: Books = new Books()

}
