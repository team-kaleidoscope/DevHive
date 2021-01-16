import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  private _title = 'Register';
  public registerUserFormGroup: FormGroup;

  constructor(private _titleService: Title, private _fb: FormBuilder, private _router: Router, private _userService: UserService) {
    this._titleService.setTitle(this._title);
  }

  ngOnInit(): void {
    this.registerUserFormGroup = this._fb.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(3)
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(3)
      ]),
      username: new FormControl('', [
        Validators.required,
        Validators.minLength(3)
      ]),
      email: new FormControl('', [
        Validators.required,
        Validators.email,
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.pattern('.*[0-9].*') // Check if password contains atleast one number
      ]),
    });

    this.registerUserFormGroup.valueChanges.subscribe(console.log);
  }

  onSubmit(): void {
    this._userService.registerUser(this.registerUserFormGroup);
    this._router.navigate(['/']);
  }

  onRedirectRegister(): void {
    this._router.navigate(['/login']);
  }

  get firstName(): AbstractControl | null  {
    return this.registerUserFormGroup.get('firstName');
  }

  get lastName(): AbstractControl | null  {
    return this.registerUserFormGroup.get('lastName');
  }

  get username(): AbstractControl | null  {
    return this.registerUserFormGroup.get('username');
  }

  get email(): AbstractControl | null {
    return this.registerUserFormGroup.get('email');
  }

  get password(): AbstractControl | null  {
    return this.registerUserFormGroup.get('password');
  }
}
