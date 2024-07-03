import { Service } from './../models/service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/customer';

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

	getServices() {
		return this._http.get<Service[]>('services');
	}
}
