import { Component } from '@angular/core';
import { Customer } from './models/customer';
import { Service } from './models/service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent {
	title = 'frontend';

	customers: Customer[] = new Array(5);
	services: Service[] = new Array(5);

	selectedCustomer: Customer | null = null;

	selectCustomer(customer: any) {
		this.selectedCustomer = customer ?? { id: 0, name: '', email: '', phone: ''};
	}
}
