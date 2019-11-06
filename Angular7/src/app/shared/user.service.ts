import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import * as io from "socket.io-client";
import { Observable,of } from 'rxjs';

interface idBus {
  key: string,
  value: string
}
interface Location {
  IdVeicolo: number,
  StringaVeicolo: string,
  TimeStamp: string,
  Latitudine: number,
  Longitudine: number,
  Passeggeri: number,
  PorteAperte: boolean
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  socket: any;
  readonly BaseURI = 'http://192.168.1.9:4000';
  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.socket = io(this.BaseURI);

  }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    let body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/ApplicationUser/Registration', body);
  }

  login(formData) {
    return this.http.post(this.BaseURI + '/ApplicationUser/Login', formData);
  }

  getUserProfile() {
    return this.http.get(this.BaseURI + '/UserProfile');
  }

  getMapData() {
    return this.http.get<Location[]>(this.BaseURI + '/get');
  }

  getMapDataByID(Id: string) {
    let params = new HttpHeaders().set("IdBus", Id);
    return this.http.get<Location[]>(this.BaseURI + '/get', { headers: params });
  }

  getBusId() {
    return this.http.get<idBus[]>(this.BaseURI + '/get/idBUS');
  }
  ///////////////parte per socket.io
  listen(Eventname: string) {
    return new Observable<Location>((subscribe) => {
      this.socket.on(Eventname, (data) => {
        subscribe.next(data);
      })
    })
  }
  emit(Eventname: string, data: any) {
    this.socket.emit(Eventname, data);
  }






}
