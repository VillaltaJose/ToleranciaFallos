import { Component, OnInit } from '@angular/core';
import { Customer } from './models/customer';
import { Service } from './models/service';
import { GeneralService } from './services/general.service';
import { Invoice } from './models/invoice';
import { finalize } from 'rxjs';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
	title = 'frontend';

	customers: Customer[] = [];
	services: Service[] = [];

	loadings = {
		payment: false,
		services: false,
	};

	error: string | null = null;
	selectedCustomer: Customer | null = null;

	constructor(
		private _service: GeneralService,
	) {

	}

	ngOnInit(): void {
		this.getCustomers();
	}

	getCustomers() {
		this._service.getCustomers()
		.subscribe(data => {
			console.log(data)
			this.customers = data;
		})
	}

	selectCustomer(customer: Customer) {
		this.selectedCustomer = customer ?? { id: 0, name: '', email: '', phone: ''};
		this.services = [];

		this._service.getServices(customer.id)
		.subscribe(data => {
			this.services = data;
		})
	}

	payInvoice(service: Service) {
		if (!this.selectedCustomer) return;

		this.loadings.payment = true;
		this.error = null;

		const invoice: Invoice = {
			amount: service.price,
			customerId: this.selectedCustomer!.id,
			serviceId: service.id,
		};

		this._service.payInvoice(invoice)
		.pipe(finalize(() => this.loadings.payment = false))
		.subscribe(() => {
			this.selectCustomer(this.selectedCustomer!);
		}, (error) => {
			console.log(error);
			this.error = error.body ?? JSON.stringify(error);
		});
	}
}
