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
  coordinate = [];
  coordinateinit = [];
  autobus = [];
  idbus;
  busDetails;

  constructor(private router: Router, private service: UserService, ) {  }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );
    this.service.listen('response').subscribe(res =>{
      let result = JSON.parse(res);
      let a = [];
      a.push(result[0].Longitudine,result[0].Latitudine);
      this.coordinate.push(a)
      console.log(this.coordinate)
    });
      //
      //let passeggeri_sum = 0;
      // this.cordinateinit.push(res[0].Longitudine, res[0].Latitudine)
      // res.forEach(element => {
      // let a = [];
      // a.push(res.Longitudine, res.Latitudine);
      // console.log(a)
      // this.coordinate.push(a);
      // console.log(this.coordinate)
      //   //passeggeri_sum += element.Passeggeri;
      // });
      // this.busDetails = res[0];
      //this.busDetails.Passeggeri = (Math.round(passeggeri_sum / res.length));  // Media passeggeri
      // console.log(this.busDetails);
 
    this.service.getBusId().subscribe(
      res => {
        res.forEach(element => {
          this.autobus.push(element.value);
        });
      },
      err => {
        console.log(err);
      },
    );
    this.service.getMapData().subscribe(
      res => {
        this.coordinateinit.push(res[0].Longitudine, res[0].Latitudine);
      },
      err => {
        console.log(err);
      },
    );

  }
  BusSelect(Id) {
    this.service.getMapDataByID(Id).subscribe(
      res => {
        this.coordinate = [];
        let passeggeri_sum = 0;
        // console.log(this.cordinateinit)
        // this.cordinateinit.push(res[0].Longitudine, res[0].Latitudine)
        res.forEach(element => {
          let a = [];
          a.push(element.Longitudine, element.Latitudine);
          this.coordinate.push(a);
          passeggeri_sum += element.Passeggeri;
        });
        this.busDetails = res[0];
        this.busDetails.Passeggeri = (Math.round(passeggeri_sum / res.length));  // Media passeggeri
        // console.log(this.busDetails);
      },
      err => {
        console.log(err);
      },
    );
  }
  BusSelectio(Id){
    this.service.emit('request',Id);
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
  
}
