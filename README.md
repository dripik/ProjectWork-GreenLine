Come usare il programma:

(Per testare il funzionamento del sistema una parte deve utilizzare linux)

1: da terminale aperto nella root del progetto eseguire chmod +x one.sh per rendere lo script eseguibile 

2: nel terminale con il comando ./one.sh  lo script bash avvierà le istanze redis, docker di influxdb e postgresql.
Verrà creata su quest'ultima una tabella per la registrazione degli utenti del sito. 

3: si potrà ora procedere ad avviare l'api node dopo aver eseguito il comando npm i, ed aver cambiato la variabile fastifyip con l'ip della macchina su cui stiamo lanciando il servizio 

4: se avviato quest'ultimo non ci dovessero essere errori si può passare al GenBus dove andrà modificato nel file App il value delle variabili IpNode, IpRedis 

5: stessa cosa vale per il sito Angular7, prima il comando npm i per le dipendeze poi una volta avviato con il comando ng serve --o modificare la variabile readonly BaseURI = 'http://192.168.1.4:4000' presente in user.service.ts e farla puntare all'api node

6: una volta completata la registrazione dal form ed effettuato il login si potranno visualizzare i dati degli autobus generati in precedenza dal GenBus




Pre-requisiti software: Nodejs/npm, Angular CLI, docker.io, postgresql-client
