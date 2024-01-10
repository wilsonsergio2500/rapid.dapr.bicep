param location string = resourceGroup().location
param tags object = {}

param containerAppsEnvironmentName string
param containerRegistryName string
param name string = ''
param serviceName string = 'push'
param managedIdentityName string = ''
param exists bool = false


module publisher '../core/host/container-app-upsert.bicep' = {
  name: '${serviceName}-container-app-module'
  params: {
    name: name
    location: location
    tags: union(tags, { 'azd-service-name': 'push' })
    containerAppsEnvironmentName: containerAppsEnvironmentName
    containerRegistryName: containerRegistryName
    containerCpuCoreCount: '1.0'
    containerMemory: '2.0Gi'
    daprEnabled: true
    containerName: serviceName
    daprAppId: serviceName
    targetPort: 80
    identityType: 'UserAssigned'
    identityName: managedIdentityName
    exists: exists
  }
}


output SERVICE_PUBLISHER_IMAGE_NAME string = publisher.outputs.imageName
output SERVICE_PUBLISHER_NAME string = publisher.outputs.name
