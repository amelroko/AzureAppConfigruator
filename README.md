# Using Azure App Configuration with .Net Framework
# POC app


## Install the following NuGet packages:

1.	Microsoft.Configuration.ConfigurationBuilders.AzureAppConfiguration version 1.0.0 or later
2.	Microsoft.Configuration.ConfigurationBuilders.Environment version 2.0.0 or later
3.	System.Configuration.ConfigurationManager version 4.6.0 or later

## Setup our app.config
To instantiate the app config in our app, we need to add the following sections
```sh
<configSections>
    <section name="configBuilders" type="System.Configuration.ConfigurationBuildersSection, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" restartOnExternalChanges="false" requirePermission="false" />
</configSections>

<configBuilders>
    <builders>
        <add name="MyConfigStore" mode="Greedy" connectionString="${ConnectionString}" type="Microsoft.Configuration.ConfigurationBuilders.AzureAppConfigurationBuilder, Microsoft.Configuration.ConfigurationBuilders.AzureAppConfiguration" />
        <add name="Environment" mode="Greedy" type="Microsoft.Configuration.ConfigurationBuilders.EnvironmentConfigBuilder, Microsoft.Configuration.ConfigurationBuilders.Environment" />
    </builders>
</configBuilders>

<appSettings configBuilders="Environment,MyConfigStore"> // here, we can put as many settings as we want to load; we define these in configBuilders above
</appSettings>
```
The connection string is the endpoint that we can find in Access settings when we create our configuration in Azure:
SLIKA1

## How does this work

On the app load our application will pull the latest appsettings from Azure and cache them. While adding a new key/value, we can choose whether to add a simple key/value pair or a key vault reference. Key vault reference can be helpful for sensitive data that we store in the key vault, but it is easily accessible from our configuration. Live value updates are not supported out of the box, but Azure provides a mechanism that we can use to implement this by using Azure Event Grid. There is an event that we can set up called Microsoft.AppConfiguration.KeyValueModified will raise an event when we change any value in our configuration. Furthermore, we can consume these events and modify any changeable configurations. More on this link: https://learn.microsoft.com/en-us/azure/azure-app-configuration/concept-app-configuration-event?tabs=cloud-event-schema

## How to create different configurations for different environments

Besides the obvious solution of creating a new app configuration for each environment, we can have that as part of one configuration. This can be achieved by using labels for our key value pairs. We can label specific configurations as DEV configurations, and by adding the labelFilter="DEV" property to our config builder, we can only pull configurations that are labeled as DEV.

## Configuration import

Azure allows us to import configurations from JSON files, which can be useful in our case.

