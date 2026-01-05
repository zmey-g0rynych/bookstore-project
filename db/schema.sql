-- mybooks.books определение
 
-- Drop table
 
-- DROP TABLE mybooks.books;
 
CREATE TABLE mybooks.books (
id serial4 NOT NULL,
title varchar(100) NULL,
price numeric(10, 2) NULL,
description text NULL,
author_first_name varchar(100) NULL,
author_last_name varchar(100) NULL,
CONSTRAINT book_pkey PRIMARY KEY (id)
);

-- mybooks.purchases определение
 
-- Drop table
 
-- DROP TABLE mybooks.purchases;
 
CREATE TABLE mybooks.purchases (
id serial4 NOT NULL,
user_id int4 NULL,
book_id int4 NULL,
purchase_date timestamp DEFAULT CURRENT_TIMESTAMP NULL,
CONSTRAINT purchases_pkey PRIMARY KEY (id)
);
 
 
-- mybooks.purchases внешние включи
 
ALTER TABLE mybooks.purchases ADD CONSTRAINT purchases_book_id_fkey FOREIGN KEY (book_id) REFERENCES mybooks.books(id) ON DELETE CASCADE;
ALTER TABLE mybooks.purchases ADD CONSTRAINT purchases_user_id_fkey FOREIGN KEY (user_id) REFERENCES mybooks.users(id) ON DELETE CASCADE;

-- mybooks.users определение
 
-- Drop table
 
-- DROP TABLE mybooks.users;
 
CREATE TABLE mybooks.users (
id serial4 NOT NULL,
username varchar(100) NOT NULL,
"password" varchar(255) NOT NULL,
"role" varchar(20) NOT NULL,
balance numeric(10, 2) DEFAULT 0 NULL,
CONSTRAINT users_pkey PRIMARY KEY (id),
CONSTRAINT users_username_key UNIQUE (username)
);