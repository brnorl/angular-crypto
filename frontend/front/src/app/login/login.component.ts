import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModel } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private http: HttpClient, private router: Router) { }


  async submit(login: any) {
    let email = login.form.value.eMail;
    let password = login.form.value.password;
    let request = {
      email: email,
      password: password
    };
    let loggedUser = await this.http.post('https://localhost:5001/User/Login', request).toPromise();

    if (loggedUser == null) {
      login.form.errors = "Account is Invalid";
    }
    if (loggedUser != null) {
      this.router.navigate(['crypto']);
    }
  }

}
