import { Component } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent {
	title = 'frontend';

	customers: any[] = new Array(5);
	services: any[] = new Array(5);

	selectedCustomer: any = null;

	selectCustomer(customer: any) {
		this.selectedCustomer = customer ?? 1;
	}
}
