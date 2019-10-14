Come usare il programma:

(Per testare il funzionamento del sistema una parte deve utilizzare linux)

1: da terminale lanciare l'installazione di docker "sudo apt-get install docker.io"

2: installiamo ora il common client per collegarci a psql "sudo apt-get install postgresql-client"

3: avviando da terminale con il comando ./one.sh  lo script bash, si avvieranno le istanze redis, docker di influxdb e postgresql.
Verrà creata su quest'ultima una tabella per la registrazione degli utenti del sito. 

4: si potrà ora procedere ad avviare l'api node dopo aver eseguito il comando npm i, ed aver cambiato la variabile fastifyip con l'ip della macchina su cui stiamo lanciando il servizio 

5: se avviato quest'ultimo non ci dovessero essere errori si può passare al GenBus dove andrà modificato nel file App il value delle variabili IpNode, IpRedis

6: stessa cosa vale per il sito Angular7 una volta avviato con il comando ng serve --o modificare la variabile readonly BaseURI = 'http://192.168.1.4:4000' presente in user.service.ts e farla puntare all'api node

7: una volta completata la registrazione dal form ed effettuato il login si potranno visualizzare i dati degli autobus generati in precedenza dal GenBus
