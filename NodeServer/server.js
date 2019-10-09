const Influx = require('influx');
const pg = require('pg');
const connectionStr = {
  user: 'postgres',
  host: '192.168.1.155',
  database: 'postgres',
  password: 'password!',
  port: 5432
};
const fastifyport = 4000;
const fastifyip = '192.168.1.155';
const fastify = require('fastify')({
  logger: true,
  ignoreTrailingSlash: true
});
fastify
  .register(require('fastify-cors'), {})
  .register(require('fastify-jwt'), { secret: 'supersecret' })

const os = require('os');
var contatore = 0;
const influx = new Influx.InfluxDB({
  host: '192.168.1.155',
  database: 'express_3',
  schema: [
    {
      measurement: 'response_times',
      fields: {
        Host: Influx.FieldType.STRING,
        StringaVeicolo: Influx.FieldType.STRING,
        TimeStamp: Influx.FieldType.STRING,
        Latitudine: Influx.FieldType.FLOAT,
        Longitudine: Influx.FieldType.FLOAT,
        Passeggeri: Influx.FieldType.INTEGER,
        PorteAperte: Influx.FieldType.BOOLEAN
      },
      tags: [
        'IdVeicolo'
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
  dati = JSON.parse(request.body);
  await influx.writePoints([
    {
      measurement: 'response_times',
      tags: { IdVeicolo: dati.IdVeicolo },
      fields: {
        Host: os.hostname(),
        StringaVeicolo: dati.StringaVeicolo,
        TimeStamp: dati.TimeStamp,
        Latitudine: dati.Latitudine,
        Longitudine: dati.Longitudine,
        Passeggeri: dati.Passeggeri,
        PorteAperte: dati.PorteAperte
      },
    }
  ]).catch(err => {
    reply.status(500)
    console.error(`Error saving data to InfluxDB! ${err.stack}`)
  }).then(() =>
    reply.status(204),
    console.log("// " + (++contatore)),
    console.log(request.body))
  
});

//parte in get
fastify.get('/get', (request, reply) => {
  const BusId = request.headers.idbus;
  if (typeof BusId !== 'undefined') {
    let id = parseInt(BusId);
    influx.query(`
    select * from response_times where IdVeicolo = '${id}'`)
      .then(result => {
        reply.status(200).send(JSON.stringify(result))
      }).catch(err => {
        reply.status(500).send(err.stack)
      })
  } else {
    influx.query(`
  select * from response_times limit 1 `)         //limit 1 per chiamata ngOnInit() ricavo solo cordinate per generare mappa
      .then(result => {
        reply.status(200).send(JSON.stringify(result))
      }).catch(err => {
        reply.status(500).send(err.stack)
      })
  }
});
//parte in get per id BUS
fastify.get('/get/idBUS', (request, reply) => {
  influx.query(`
  SHOW TAG VALUES WITH KEY = IdVeicolo `)
    .then(result => {
      reply.status(200).send(JSON.stringify(result))
    }).catch(err => {
      reply.status(500).send(err.stack)
    })
})
//////////////////////////////////////////////////////// parte per sito web che gestite le richieste di login e registrazione
fastify.post('/ApplicationUser/Registration', (request, reply) => {
  const client = new pg.Client(connectionStr);
  var dati = request.body;
  client.connect()
    .then(() => console.log('client has connect'));
  client.query(`INSERT INTO utenti (UserName, FullName, Email, Password) VALUES ('${dati.UserName}','${dati.FullName}','${dati.Email}',crypt('${dati.Password}', gen_salt('bf')))`)
    .then(() => {
      client.end()
      console.log('client close without error')
      reply.status(200).send({ succeeded: true });
    }).catch(err => {
      client.end()
      console.log('client close with : ' + err)
      reply.status(500).send(err)
    })
});
fastify.post('/ApplicationUser/Login', (request, reply) => {
  const client = new pg.Client(connectionStr);
  var dati = request.body;
  client.connect()
    .then(() => console.log('client has connect'));
  client.query(`SELECT (Id) FROM utenti WHERE UserName = ('${dati.UserName}') AND Password = crypt ('${dati.Password}', Password)`)
    .then(result => {
      client.end()
      console.log('client close without error')
      if (result.rows.length == 1) {
        var id = { UserID: result.rows[0].id }    //controllo che sia l`id esatto
        const token = fastify.jwt.sign({ id })
        reply.status(200).send({ result, token })
      } else {
        reply.status(400).send({ msg: "Username o password errati" })
      }
    }).catch(err => {
      client.end()
      console.log('client close with : ' + err)
      reply.status(500).send(err)
    })
})
fastify.get('/UserProfile', (request, reply) => {
  const bearerHeader = request.headers.authorization
  if (typeof bearerHeader !== 'undefined') {
    const token = bearerHeader.split(' ');
    const decoded = fastify.jwt.decode(token[1])
    //console.log(decoded.id);
    const client = new pg.Client(connectionStr);
    client.connect()
      .then(() => console.log('client has connect'));
    client.query(`SELECT username,email,fullname FROM utenti WHERE Id = ('${decoded.id.UserID}')`)
      .then(result => {
        client.end()
        //console.log(result.rows[0])
        reply.code(200).send(result.rows[0])
      }).catch(err => {
        client.end()
        console.log('client close with : ' + err)
        reply.status(500).send(err)
      })
  }
})
// Run the server!
const start = async () => {
  try {
    await fastify.listen(fastifyport, fastifyip)
    fastify.log.info(`server listening on ${fastify.server.address().port}`)
  } catch (err) {
    fastify.log.error(err)
    process.exit(1)
  }
}
start();
