import { Component } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { AuthenticationService } from '@app/_services';
import { AccountMovementDto } from '../_models/accountMovementDto';
import { AccountMovementService } from '../_services/accountMovement.service';


@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    depositForm: FormGroup;
    withdrawForm: FormGroup;
    paymentForm: FormGroup;
    loading = false;
    submittedDeposit = false;
    submittedWithdraw = false;
    submittedPayment = false;
    returnUrl: string;
    error = '';
    accountBalance = '';
    name = '';
    clientId: number;
    accountMovements: AccountMovementDto[];

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private authenticationService: AuthenticationService,
        private accountMovementService: AccountMovementService
    ) {
    }

    ngOnInit() {
        this.clientId = this.authenticationService.currentUserValue.clientId;

        this.accountService.getAccountData(this.clientId).pipe(first()).subscribe(
            data => {
                this.accountBalance = data.accountBalance;
                this.name = data.name;
            });

        this.getAccountHistory();

        this.depositForm = this.formBuilder.group({
            depositAmount: ['', Validators.required],
        });

        this.withdrawForm = this.formBuilder.group({
            withdrawAmount: ['', Validators.required],
        });

        this.paymentForm = this.formBuilder.group({
            paymentAmount: ['', Validators.required],
        });
    }

    get fd() { return this.depositForm.controls; }
    get fw() { return this.withdrawForm.controls; }
    get fp() { return this.paymentForm.controls; }


    getAccountHistory() {
        this.accountMovementService.getAccountHistory(this.clientId).pipe(first()).subscribe(
            data => {
                this.accountMovements = data;
            }
        );
    }

    onSubmitDeposit() {
        this.submittedDeposit = true;

        if (this.depositForm.invalid) {
            return;
        }
        this.makeRequest(this.fd.depositAmount.value, "Deposit");
        this.loading = true;

    }

    onSubmitWithdraw() {
        this.submittedWithdraw = true;

        if (this.withdrawForm.invalid) {
            return;
        }

        this.makeRequest(this.fw.withdrawAmount.value, "Withdraw");
        this.loading = true;

    }
    onSubmitPayment() {
        this.submittedPayment = true;

        if (this.paymentForm.invalid) {
            return;
        }

        this.makeRequest(this.fp.paymentAmount.value, "Payment");
        this.loading = true;

    }

    clearForm() {
        this.submittedDeposit = false;
        this.submittedPayment = false,
        this.submittedWithdraw = false;
        this.depositForm.reset();
        this.withdrawForm.reset();
        this.paymentForm.reset();
    }
    makeRequest(value: number, operation: string) {


        this.accountService.accountOperation(this.clientId, value, operation)
            .pipe(first())
            .subscribe(
                data => {
                    if (data.success) {
                        this.accountBalance = data.accountBalance;
                        this.getAccountHistory()
                        this.clearForm();
                    }
                    else {
                        this.error = data.message;
                    }
                    this.loading = false;
                },
                error => {
                    this.error = error;
                    this.loading = false;
                });
    }
}