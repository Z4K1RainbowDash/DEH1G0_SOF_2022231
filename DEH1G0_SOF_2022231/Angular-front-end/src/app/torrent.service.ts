import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {SearchTorrentModel} from "./_models/DTOs/search-torrent-model";
import {TorrentModel} from "./_models/DTOs/torrent-model";
import {Observable} from "rxjs";
import {ApiService} from "./api.service";
import {SelectedTorrent} from "./_models/DTOs/selected-torrent";

@Injectable({
  providedIn: 'root'
})
export class TorrentService {
  private apiService: ApiService
  private http: HttpClient;
  constructor(http: HttpClient, apiService: ApiService) {
    this.http = http;
    this.apiService = apiService;
  }

  downloadTorrentById(torrentId: string, name: string) :Observable<Blob> {
    const url = `${this.apiService.baseUrl}/Torrents/DownloadTorrent`;
    const dto = new SelectedTorrent(torrentId, name);

    const headers = new HttpHeaders({
      'Content-Type': 'application/octet-stream'
    });

    return this.http.post(url,dto, {
      responseType: 'blob',
      headers: headers})
    }


  getTorrentsByTorrentModelDTO(dto: SearchTorrentModel): Observable<TorrentModel[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const url = `${this.apiService.baseUrl}/Torrents/SearchTorrent`;

    return this.http.post<TorrentModel[]>(url, dto, { headers: headers });
  }

}
