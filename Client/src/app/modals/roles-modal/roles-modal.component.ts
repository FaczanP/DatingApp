import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrl: './roles-modal.component.css'
})
export class RolesModalComponent implements OnInit {
  // title = '';
  // list: any;
  // closeBtnName = '';
  username = '';
  availableRoles: any[] = [];
  selectedRoles: any[] = [];
  constructor(public bsModalRef: BsModalRef) {}
  
  ngOnInit(): void {
  }

  updateChecked(checkedValue: string){
    const index = this.selectedRoles.indexOf(checkedValue);
    index !== -1 ? this.selectedRoles.splice(index, 1) : this.selectedRoles.push(checkedValue);
  }

}
