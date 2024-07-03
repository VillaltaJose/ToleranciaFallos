import { Component, OnInit } from '@angular/core';
import { Customer } from './models/customer';
import { Service } from './models/service';
import { GeneralService } from './services/general.service';

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

	selectCustomer(customer: any) {
		this.selectedCustomer = customer ?? { id: 0, name: '', email: '', phone: ''};

		this._service.getServices()
		.subscribe(data => {
			this.services = data;
		})
	}
}
