import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as FormData from 'form-data';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { Post } from 'src/models/post';
import { AppConstants } from '../app-constants.module';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private _http: HttpClient, private _tokenService: TokenService)
  { }

  getDefaultPost(): Post {
    return new Post(Guid.createEmpty(), 'Gosho', 'Trapov', 'gosho_trapov', 'Your opinion on my idea?', new Date(), []);
  }

  /* Requests from session storage */

  createPostWithSessionStorageRequest(postMessage: string): Observable<object> {
    const userId = this._tokenService.getUserIdFromSessionStorageToken();
    const token = this._tokenService.getTokenFromSessionStorage();

    return this.createPostRequest(userId, token, postMessage);
  }

  putPostWithSessionStorageRequest(postId: Guid, newMessage: string): Observable<object> {
    const userId = this._tokenService.getUserIdFromSessionStorageToken();
    const token = this._tokenService.getTokenFromSessionStorage();

    return this.putPostRequest(userId, token, postId, newMessage);
  }

  deletePostWithSessionStorage(postId: Guid): Observable<object> {
    const token = this._tokenService.getTokenFromSessionStorage();

    return this.deletePostRequest(postId, token);
  }

  /* Post requests */

  createPostRequest(userId: Guid, authToken: string, postMessage: string): Observable<object> {
    const form = new FormData();
    form.append('message', postMessage);
    const options = {
      params: new HttpParams().set('UserId', userId.toString()),
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + authToken)
    };
    return this._http.post(AppConstants.API_POST_URL, form, options);
  }

  getPostRequest(id: Guid): Observable<object> {
    const options = {
      params: new HttpParams().set('Id', id.toString())
    };
    return this._http.get(AppConstants.API_POST_URL, options);
  }

  putPostRequest(userId: Guid, authToken: string, postId: Guid, newMessage: string): Observable<object> {
    const body = {
      postId: postId.toString(),
      newMessage: newMessage
    };
    const options = {
      params: new HttpParams().set('UserId', userId.toString()),
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + authToken)
    };
    return this._http.put(AppConstants.API_POST_URL, body, options);
  }

  deletePostRequest(postId: Guid, authToken: string): Observable<object> {
    const options = {
      params: new HttpParams().set('Id', postId.toString()),
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + authToken)
    };
    return this._http.delete(AppConstants.API_POST_URL, options);
  }
}