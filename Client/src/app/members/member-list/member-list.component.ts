import { Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
 
  // members: Member[] = [];
  members$: Observable<Member[]> | undefined;

  constructor(private memberService: MembersService) {  }
 
  ngOnInit(): void {    
    // this.loadMembers();
    this.members$ = this.memberService.getMembers();
  }

  // loadMembers(){
  //   this.memberService.getMembers().subscribe({
  //     next: members => this.members = members
  //   })
  // }

}
