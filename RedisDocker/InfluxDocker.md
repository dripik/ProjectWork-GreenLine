comandi per collegarsi da terminal a influxdb su docker
sudo docker run --rm --name=influxdb -d -p 8086:8086 influxdb //avvio il container di docker esponendo la porta standar 8086 il nome dell'istance docker sarà influxdb
sudo docker exec -it influxdb influx  //vado ad eseguire il comando influx dal container appena creato, cioè mi collego ad influx attraverso docker e non dal host

sudo apt install influxdb-client //vado ad installare sul host la cli per influxdb
influx //mi collego alla porta esposta di docker da terminale host

sudo docker container ls --all //mostra tutti i container che una volta stoppati rimangono salvati se non si avviano con il tag per eliminarli una volta spenti


sudo docker start influxdb // avvio un container stoppato ma che non è stato rimosso


 1803  sudo docker run -p 8086:8086 -d --name=influo --net=influxdb influxdb
 1804  docker run -p 8888:8888       --net=influxdb       chronograf --influxdb-url=http://influxdb:8086
 1805  sudo docker run -p 8888:8888       --net=influxdb       chronograf --influxdb-url=http://influxdb:8086
 1806  docker ps
 1807  sudo docker ps
 1809  sudo docker run -d -p 8888:8888 --net=influxdb chronograf --influxdb-url=http://influo:8086


prove per collegare chronograf


comandi utili 
> docker --help
> docker container --help
> docker container ls --help
> docker run --help


