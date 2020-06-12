# azure storage

`az storage account create -n patternsaksdevwe -g patterns-aks-dev-we --kind StorageV2 -l westeurope --debug`

`$connection = $(az storage account show-connection-string -g patterns-aks-dev-we --name patternsaksdevwe --query connectionString -o json | ConvertFrom-Json)`

`az storage table create -n daprpoc --connection-string $connection --debug`
