#!/bin/bash
# 
echo "avvio di influx db su docker su porta 8086"
sudo docker run --rm --name=influxdb -d -p 8086:8086 influxdb
echo "avvio di postgresql su docker porta 5432 se già utilizzata cambiare porta"
sudo docker run --rm   --name pg-docker -e POSTGRES_PASSWORD=password! -d -p 5432:5432  postgres
echo "password utente postgres password!" 
sleep 5;
export PGPASSWORD='password!'; psql -p 5432 -U postgres -h localhost -c"CREATE EXTENSION pgcrypto;CREATE TABLE utenti(

   ID SERIAL PRIMARY KEY   ,

   UserName           VARCHAR(50)    NOT NULL UNIQUE,

   Email           VARCHAR(50)    NOT NULL UNIQUE,

   FullName           VARCHAR(50)    NOT NULL,

   Password           TEXT    NOT NULL

);"
echo "Creazione completata!! "
