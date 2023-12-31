import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';
import { User } from '../Models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  user: User;

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
  }

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe((response) => {
      this.router.navigateByUrl('/members');
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/home');
  }
}
