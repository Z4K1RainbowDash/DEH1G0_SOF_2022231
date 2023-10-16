export class SelectedTorrent {

  TorrentId: string;
  TorrentName: string;

  constructor(id: string, name:string) {
    this.TorrentId = id;
    this.TorrentName = name;
  }
}
