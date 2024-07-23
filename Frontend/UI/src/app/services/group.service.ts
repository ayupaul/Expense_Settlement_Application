import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserServiceService } from './user-service.service';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  BackendUrl: string = 'http://localhost:5006/api/Group';
  constructor(
    private http: HttpClient,
    private userService: UserServiceService
  ) {}
  createGroup(groupData: any): Observable<any> {
    const userId = this.userService.getUserIdFromToken();
    return this.http.post(`${this.BackendUrl}/${userId}`, groupData);
  }
  getMyGroups():Observable<any>{
    const userId = this.userService.getUserIdFromToken();
    return this.http.get(`${this.BackendUrl}/getMyGroups/${userId}`);
  }
  getUserInGroup(groupId:any):Observable<any>{
    return this.http.get(`${this.BackendUrl}/getUsersInGroup/${groupId}`);
  }
  addUserToGroup(userId:any,groupId:any):Observable<any>{
    return this.http.post(`${this.BackendUrl}/addUserToGroup/${userId}`,groupId);
  }
}
