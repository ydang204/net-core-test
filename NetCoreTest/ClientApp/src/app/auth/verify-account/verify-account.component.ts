import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService, AlertService } from '@app/_services';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { finalize, switchMap } from 'rxjs/operators';

const PrimaryWhite = '#ffffff';
const SecondaryGrey = '#cfcfcf';

@Component({
  selector: 'app-verify-account',
  templateUrl: './verify-account.component.html',
  styleUrls: ['./verify-account.component.css'],
})
export class VerifyAccountComponent implements OnInit {
  loading: boolean = false;
  isSuccess: boolean = false;
  public primaryColour = PrimaryWhite;
  public secondaryColour = SecondaryGrey;
  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.verifyAccount();
  }

  private verifyAccount() {
    this.loading = true;
    this.route.params
      .pipe(
        switchMap((params) => this.authService.verifyAccount(params.token)),
        finalize(() => (this.loading = false))
      )
      .subscribe(
        () => {
          this.isSuccess = true;
          this.loading = false;
          this.alertService.success('Verify account successfully!');
        },
        () => {
          this.loading = false;
          this.alertService.error('Verify account failed!');
        }
      );
  }
}
