import { Service } from './../models/service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/customer';
import { Invoice } from '../models/invoice';

@Injectable({
	providedIn: 'root',
})
export class GeneralService {
	constructor(
		private _http: HttpClient,
	) {}

	getCustomers() {
		return this._http.get<Customer[]>('customers');
	}

	getServices(customerId: number) {
		return this._http.get<Service[]>(`services?customerId=${customerId}`);
	}

	payInvoice(invoice: Invoice) {
		return this._http.post('invoices', invoice);
	}
}
