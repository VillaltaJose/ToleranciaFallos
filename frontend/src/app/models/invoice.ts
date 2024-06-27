export interface Invoice {
	Id: number;
	CustomerId: number;
	ServiceId: number;
	Amount: number;
	CreatedAt: Date | string;
}
