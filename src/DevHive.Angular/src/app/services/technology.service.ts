import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Guid} from 'guid-typescript';
import {Observable} from 'rxjs';
import {Technology} from 'src/models/technology';
import {AppConstants} from '../app-constants.module';

@Injectable({
  providedIn: 'root'
})
export class TechnologyService {
  constructor(private http: HttpClient) { }

  getTechnologyRequest(techId: Guid): Observable<object> {
    const options = {
      params: new HttpParams().set('Id', techId.toString())
    };
    return this.http.get(AppConstants.API_TECHNOLOGY_URL, options);
  }

  getAllTechnologiesWithSessionStorageRequest(): Observable<object> {
    const token = sessionStorage.getItem('UserCred') ?? '';
    return this.getAllTechnologiesRequest(token);
  }

  getAllTechnologiesRequest(authToken: string): Observable<object> {
    const options = {
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + authToken)
    };
    return this.http.get(AppConstants.API_TECHNOLOGY_URL + '/GetTechnologies', options);
  }

  getFullTechnologiesFromIncomplete(givenTechnologies: Technology[]): Promise<Technology[]> {
    if (givenTechnologies.length === 0) {
      return new Promise(resolve => resolve(givenTechnologies));
    }

    // This accepts language array with incomplete languages, meaning
    // languages that only have an id, but no name
    return new Promise(resolve => {
      const lastGuid = givenTechnologies[givenTechnologies.length - 1].id;

      // For each language, request his name and assign it
      for (const tech of givenTechnologies) {
        this.getTechnologyRequest(tech.id).subscribe(
          (result: object) => {
            // this only assigns the "name" property to the language,
            // because only the name is returned from the request
            Object.assign(tech, result);

            if (lastGuid === tech.id) {
              resolve(givenTechnologies);
            }
          }
        );
      }
    });
  }

}
