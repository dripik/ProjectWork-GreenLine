Breve guida per settare il DB postgres su docker 

Si trova tutto al link : https://hackernoon.com/dont-install-postgres-docker-pull-postgres-bee20e200198

sudo docker run --rm   --name pg-docker -e POSTGRES_PASSWORD=XXXXXX -d -p 5432:5432  postgres

questo scarica l'immagine e espone il servizio alla porta 5432 se avessimo gia psql installato dobbiamo cambiare il primo numero che corrisponde al lato esterno a docker quello a cui ci colleghiamo

psql -h localhost -p 5472 -U postgres 

per collegarsi come utente postgres 


CREATE EXTENSION pgcrypto;

esempio table

CREATE TABLE utenti(

   ID SERIAL PRIMARY KEY   ,

   UserName           VARCHAR(50)    NOT NULL UNIQUE,

   Email           VARCHAR(50)    NOT NULL UNIQUE,

   FullName           VARCHAR(50)    NOT NULL,

   Password           TEXT    NOT NULL

);

esempio di query per verificare il contenuto della password cryptata
SELECT *
  FROM utenti

 WHERE email = 'xxxxx' 

   AND password = crypt('xxxxx', password);   