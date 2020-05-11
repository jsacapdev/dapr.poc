# Dapr Proof of Concept

Building out an application architecture using Dapr.

Run it up stand-alone:

`dapr run --app-id bankingapi --app-port 5000 dotnet run`

`dapr run --app-id bankingapi --app-port 5000 --port 37318 dotnet run`

Run it up in vscode.

With the simple sample we can see the rest interface, but also is the sidecar offering the pub/sub model.

`dapr publish -t deposit -p '{"id": "17", "amount": 15 }'`

`dapr publish -t withdraw -p '{"id": "17", "amount": 15 }'`

`dapr invoke --app-id bankingapi --method withdraw --payload '{"id": "17", "amount": 15 }'`
