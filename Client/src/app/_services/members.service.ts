import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Member } from '../_models/member'
import { environment } from '../../environments/environment.development';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  

  constructor(private http: HttpClient) { }

  getMembers() {
    if(this.members.length > 0) return of(this.members)
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map( members => {
        this.members = members;
        return members;
      })
    )  //, this.getHttpOptions())
  }

  getMember(username: string) {
    const member = this.members.find(x => x.userName == username);
    return this.http.get<Member>(this.baseUrl + 'users/' + username) //, this.getHttpOptions())

  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member};
      })
    );
  }

  setMainPhoto(photoId: number){
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

  // getHttpOptions() {
  //   const userString = localStorage.getItem('user');
  //   if (!userString) return;
  //   const user = JSON.parse(userString);
  //   const header = {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token
  //     })
  //   }
  //   return header
  // }
}
