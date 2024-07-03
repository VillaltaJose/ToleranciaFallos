import { Component, OnInit } from '@angular/core';
import { Customer } from './models/customer';
import { Service } from './models/service';
import { GeneralService } from './services/general.service';
import { Invoice } from './models/invoice';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
	title = 'frontend';

	customers: Customer[] = [];
	services: Service[] = [];

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

		const invoice: Invoice = {
			amount: service.price,
			customerId: this.selectedCustomer!.id,
			serviceId: service.id,
		};

		this._service.payInvoice(invoice)
		.subscribe(() => {
			this.selectCustomer(this.selectedCustomer!);
		});
	}
}
