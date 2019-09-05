const Influx = require('influx');
const fastify = require('fastify')({
  logger: true,
  ignoreTrailingSlash: true  //cercare a cosa serve e se è ancora necessario
});
const os = require('os');
const fastifyport = 4000;
var contatore = 0;
const influx = new Influx.InfluxDB({
  host: 'localhost',
  database: 'express_3',
  schema: [
    {
      measurement: 'response_times',
      fields: {
        IdVeicolo: Influx.FieldType.INTEGER,
        StringaVeicolo: Influx.FieldType.STRING,
        TimeStamp: Influx.FieldType.STRING,
        Latitudine: Influx.FieldType.FLOAT,
        Longitudine: Influx.FieldType.FLOAT,
        Altitudine: Influx.FieldType.FLOAT,
        Passeggeri: Influx.FieldType.INTEGER,
        PorteAperte: Influx.FieldType.BOOLEAN
      },
      tags: [
        'host'
      ]
    }
  ]
});
influx.getDatabaseNames()
  .then(names => {
    if (!names.includes('express_3')) {
      return influx.createDatabase('express_3');
    }
  })
  .catch(err => {
    console.error(`Error creating Influx database!`);
  })
fastify.post('/', async (request, reply) => {
  dati = request.body[0];
  influx.writePoints([
    {
      measurement: 'response_times',
      tags: { host: os.hostname() },
      fields: {
        IdVeicolo: dati.idVehicle,
        StringaVeicolo: dati.description,
        TimeStamp: dati.timeDate,
        Latitudine: dati.latitude,
        Longitudine: dati.longitude,
        Altitudine: dati.altitude,
        Passeggeri: dati.passenger,
        PorteAperte: dati.theDoors
      },
    }
  ]).catch(err => {
    console.error(`Error saving data to InfluxDB! ${err.stack}`)
  })
  reply.send('Ok');
  console.log("// " + (++contatore));
  console.log(request.body);
});

//parte in get
fastify.get('/get',async (request, reply) => {
 await influx.query(`
    select * from response_times `)
    .then(result => {
      reply.code(204)
      reply.send(JSON.stringify(result))
    }).catch(err => {
      reply.status(500).send(err.stack)
    })
})

// Run the server!
const start = async () => {
  try {
      await fastify.listen(fastifyport)
      fastify.log.info(`server listening on ${fastify.server.address().port}`)
  } catch (err) {
      fastify.log.error(err)
      process.exit(1)
  }
}
start();
