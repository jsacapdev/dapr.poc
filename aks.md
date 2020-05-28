# setting up the aks cluster

## resources are from here

https://github.com/Azure/azure-quickstart-templates/tree/master/101-aks-advanced-networking

## set up the rg and sp

az group create -n patterns-aks-dev-we -l westeurope --debug

az ad sp create-for-rbac -n aks_spn --skip-assignment -o json --debug

{
  "appId": "10026802-b3a8-47b6-aee2-aa1e38f50550",
  "displayName": "aks_spn",
  "name": "http://aks_spn",
  "password": "03e48803-928c-4203-88f5-f5aa0c3ad797",
  "tenant": "b2e6ad1a-f3e1-466e-8d89-0ba2f6a26c60"
}

az ad sp show --id 10026802-b3a8-47b6-aee2-aa1e38f50550 -o json --debug

az ad sp show --id 10026802-b3a8-47b6-aee2-aa1e38f50550 -o json --query objectId | ConvertFrom-Json

## run the pre-req

az group deployment create --name $(New-Guid) --resource-group patterns-aks-dev-we --template-file ./prereq.azuredeploy.json --parameters "@prereq.azuredeploy.parameters.json" --debug

az group deployment create --name $(New-Guid) --resource-group patterns-aks-dev-we --template-file ./azuredeploy.json --parameters "resourceName=patternsaksdevwe" "dnsPrefix=patternsaksdevwe" "existingServicePrincipalObjectId=3f1217ca-6477-4b60-bb76-c23ff6015b33" "existingServicePrincipalClientId=10026802-b3a8-47b6-aee2-aa1e38f50550" "existingServicePrincipalClientSecret=03e48803-928c-4203-88f5-f5aa0c3ad797" "existingVirtualNetworkName=vnet" "existingVirtualNetworkResourceGroup=patterns-aks-dev-we" "existingSubnetName=Subnet" "kubernetesVersion=1.16.9" --debug
