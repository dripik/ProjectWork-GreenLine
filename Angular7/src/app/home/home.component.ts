import { UserService } from './../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})

export class HomeComponent implements OnInit {
  userDetails;
  cordinate = []
  cordinateinit = []
  autobus: string[] = ["autobus1", "autobus2", "autobus3"]



  constructor(private router: Router, private service: UserService, ) { }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );
    this.service.getMapData().subscribe(
      res => {
        this.cordinateinit.push(res[0].Longitudine, res[0].Latitudine)
        res.forEach(element => {
          let a = []
          a.push(element.Longitudine, element.Latitudine)
          this.cordinate.push(a)
        });
      },
      err => {
        console.log(err);
      },

    )
  }


  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
