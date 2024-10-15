import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-imput',
  templateUrl: './text-imput.component.html',
  styleUrl: './text-imput.component.css'
})
export class TextImputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';

  constructor (@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

  get control(): FormControl{
    return this.ngControl.control as FormControl
  }

}
