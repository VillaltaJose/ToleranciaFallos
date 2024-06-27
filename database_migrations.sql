create table customers (
    id serial primary key,
    name varchar(255) not null,
    email varchar(255) not null
);

create table services (
    id serial primary key,
    name varchar(255) not null,
    price numeric(10, 2) not null
);

create table invoices (
    id serial primary key,
    customer_id integer not null,
    service_id integer not null,
    amount numeric(10, 2) not null,
    created_at timestamp not null default now(),
    foreign key (customer_id) references customers(id),
    foreign key (service_id) references services(id)
);