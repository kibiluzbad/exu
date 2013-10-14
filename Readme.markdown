Exu Service
===

Serviço para realização de cálculos de valores totais de rotas utilizando as api`s do Maplink


Api
---

Considerando que seu servidor esteja na posta 3030.

### CalculaRota

POST - http://localhost:3030

**Content-type**: _application/json_

**Entrada**

    {"Addresses":[{
	"City":"São Paulo",
	"State": "SP",
	"Name":"Av. Paulista",
	"Number ":"1000"},
	{"City":"São Paulo",
	"State":"SP",
	"Name":"Al. Lorena",
	"Number":"800"}],
	"Type":"FastestRoute"}
	
Type pode ser:
* FastestRoute
* LessTraffic

**Saída**

Status code: 200

    {"Time":{"Days":0,"Hours":0,"Minutes":3,"Seconds":0,"Milliseconds":0},
	"Distance":1.69,
	"FuelCost":0.45,
	"TotalCost":0.45}

