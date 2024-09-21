import { Component, Input, input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
  // encapsulation: ViewEncapsulation.None
})
export class MemberCardComponent implements OnInit{

@Input() member: Member | undefined;

  ngOnInit(): void {
  }

}
