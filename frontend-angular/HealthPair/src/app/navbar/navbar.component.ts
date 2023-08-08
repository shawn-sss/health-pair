import { Component, OnInit } from '@angular/core';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from '../_services';
import { Router } from '@angular/router';
import { faHandHoldingMedical } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  faHandHoldingMedical = faHandHoldingMedical;
  navbarOpen = false;
  loggedin = false;
  faUserCircle = faUserCircle;
  currentPatient: any;
  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }
  login() {
    this.router.navigate(['/login']);
    if (this.currentPatient) {
      this.loggedin = true;
    }
  }
  logout() {
    this.loggedin = false;
    this.authenticationService.logout();
  }
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.authenticationService.CurrentPatient.subscribe(
      (x) => (this.currentPatient = x)
    );
  }
  ngOnInit() {
    if (!this.currentPatient) {
      this.login();
    }
  }
}
