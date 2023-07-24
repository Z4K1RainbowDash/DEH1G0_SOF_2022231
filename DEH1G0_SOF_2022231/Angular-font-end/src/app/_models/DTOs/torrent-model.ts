export class TorrentModel
{
  public readonly Id: string
  public readonly Name : string
  public readonly Image : string
  public readonly Date : string
  public readonly Size : string
  public readonly Downloads : string
  public readonly Seeders : string
  public readonly Leechers : string

  constructor(id:string, name:string, image:string, date: string, size:string, downloads:string,
              seeders: string, leechers:string) {
    this.Id = id
    this.Name = name
    this.Image = image
    this.Date = date
    this.Size = size
    this.Downloads = downloads
    this.Seeders = seeders
    this.Leechers = leechers
  }

}
