# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [0.0.5] - 09/10/2019
### Deleted
- useless folder
### Added
- minor fix on this statment for influx : 
Tags are optional. You don’t need to have tags in your data structure, but it’s generally a good idea to make use of them because, unlike fields, tags are indexed. This means that queries on tags are faster and that tags are ideal for storing commonly-queried metadata.
- querying on tags for faster response


## [0.0.4] - 28/09/2019
### Added
- Angular project login/registration 
- bash script for linux docker container
- Api for Angular project on Nodeserver/server.js

## [0.0.3] - 18/09/2019
### Added
- Passati da express a fastify 
- aggiunti vari .md per avviare container docker
- modificato API per registrare dati utente su db postgres
- aggiunta di variabili per impostare ip e porta per l'API
- modificato funzioni su API per farle funzionare come promesse 


## [0.0.2] - 1/08/2019
### Added
- Sviluppo iniziale per cloud AWS

## [0.0.1] - 22/05/2019
### Added
- File README con consigli per avviare il programma
- CHANGELOG per tenere traccia degli avanzamenti e delle funzionalità
- Aggiunto autoavvio di influxDB con note sul README



