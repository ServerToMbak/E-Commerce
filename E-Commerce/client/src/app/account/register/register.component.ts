import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { map, of, switchMap, timer } from 'rxjs';
import { AccontService } from '../accont.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm : FormGroup;
  errors: string [];
  constructor(private fb:FormBuilder, private accountService:AccontService, private router:Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email:[null, [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
      password:[null, [Validators.required]]
    });
  }
  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe(resonse => {
      this.router.navigateByUrl('/shop')
    }, error => {
      this.errors = error.errors
    });
    console.log(this.registerForm.value);
  };


  validateEmailNotTaken(): AsyncValidatorFn{
    return control => {
      return timer(500).pipe(
          switchMap(() => {
            if (!control.value)
            {
              return of(null);
            }
            return this.accountService.checkEmailExixts(control.value).pipe(
              map(res => {
                return res ? {emailExists: true} : null;
              })
            );
          })
        );
    };
}

}


