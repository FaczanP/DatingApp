import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { User } from '../../_models/user';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../modals/roles-modal/roles-modal.component';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrl: './user-managment.component.css'
})
export class UserManagmentComponent implements OnInit {
  users: User[] = [];
  bsModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();
  availableRoles = [
    'Admin',
    'Moderator',
    'Member'
  ];

  constructor(private adminService: AdminService, private modalService: BsModalService, private http: HttpClient) {}
  
  ngOnInit(): void { 
    this.getUsersWithRoles();
   }

  getUsersWithRoles(){
    this.adminService.getUsersWithRoles().subscribe({
      next: users => this.users = users
    });
  }

  openRolesModal(user: User){
    const config = {
      class: 'modal-dialog-centered',
      initialState:{
        username: user.userName,
        availableRoles: this.availableRoles,
        selectedRoles: [...user.roles]
      }
    }
    // const initalState: ModalOptions = {
    //   initialState:{
    //     list: [
    //       'Do thing',
    //       'Another thing',
    //       'Something else'
    //     ],
    //     title: 'Test modals'
    //   }
    // }
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.onHide?.subscribe({
      next: () => {
        const selectedRoles = this.bsModalRef.content?.selectedRoles;
        if(!this.arrayEqual(selectedRoles!, user.roles)){
          this.adminService.updateUserRoles(user.userName, selectedRoles!).subscribe({
            next: roles => user.roles = roles
          });
        }
      }
    })
    // this.bsModalRef.content!.closeBtnName = 'Close';
  }

  private arrayEqual(arr1: any[], arr2: any[]){
    return JSON.stringify(arr1.sort()) === JSON.stringify(arr2.sort());
  }

}
