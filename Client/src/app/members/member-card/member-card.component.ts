import { Component, Input, input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { PresenceService } from '../../_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
  // encapsulation: ViewEncapsulation.None
})
export class MemberCardComponent implements OnInit{

@Input() member: Member | undefined;

constructor(private memberService: MembersService, private toastr: ToastrService,
  public presenceService: PresenceService
){

}

  ngOnInit(): void {
  }

  addLike(member: Member){
    this.memberService.addLike(member.userName).subscribe({
      next: () => this.toastr.success('You have liked ' + member.knownAs)
    })
  }

}
