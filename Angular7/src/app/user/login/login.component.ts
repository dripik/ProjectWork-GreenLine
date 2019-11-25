import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../shared/user.service';
import { Component, OnInit, NgModule } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})

export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  };
  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  // se il token esiste gia, vai alla homepage
  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }
// se non esiste il token, fai il login e controlla, routing su un'altra pagina.
  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/home');
      },
      err => {
        // tslint:disable-next-line: triple-equals
        if (err.status == 400) {
          this.toastr.error('Nome utente o password errati', 'Autenticazione fallita');
        } else {
          console.log(err);
        }
      }
    );
  }
}
