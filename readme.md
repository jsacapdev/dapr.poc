# Dapr Proof of Concept

Building out an application architecture using Dapr.

Run it up stand-alone:

## banking api

so these examples show that when the application is running, dapr injects the sidecar. the api uses native parameters which is quite kool.

`dapr run --app-id bankingapi --app-port 5000 dotnet run`

`dapr run --app-id bankingapi --app-port 5000 --port 37318 dotnet run`

`dapr run --app-id bankingapi --app-port 5000 --port 3500 dotnet run`

## function api

this is also inject the sidecar into the function.so both are running using the side car. really kool. but in this case we are not using any native bindings. also notice that the function always go's through the sidecar.

`export DAPR_HTTP_PORT=37318`

`export DAPR_HTTP_PORT=3500`

`dapr run --app-id functionapi --app-port 7071 --port 37319 func start`

`dapr run --app-id functionapi --app-port 7071 --port 3500 func start`

Run it up in vscode.

With the simple sample we can see the rest interface, but also is the sidecar offering the pub/sub model.

## command line banking api

`dapr publish -t deposit -p '{"id": "17", "amount": 15 }'`

`dapr publish -t withdraw -p '{"id": "17", "amount": 15 }'`

`dapr invoke --app-id bankingapi --method withdraw --payload '{"id": "17", "amount": 15 }'`

## command line function api

`dapr invoke --app-id functionapi --method api/gatewaytrigger`
