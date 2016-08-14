#Paytime

Add PrivateSettings.config file with the following format

```
<appSettings>
  <!--Authentication and auth related settings-->
  <add key="ida:ClientId" value="XXXXXX" />
  <add key="ida:AADInstance" value="https://login.microsoftonline.com/" />
  <add key="ida:Domain" value="\[tenantname\].onmicrosoft.com" />
  <add key="ida:TenantId" value="XXXXX" />
  <add key="ida:PostLogoutRedirectUri" value="\[url\]" />
  <!--Reminder related settings-->
  <add key="MobileNumber" value="XXXXXX" />
  <add key="FromEmail" value="XXXX" />
  <add key="FromPerson" value="Paytime" />
  <add key="ToEmail" value="XXXXXX" />
  <add key="ToPerson" value="\[name\]" />
  <add key="EmailApiKey" value="\[SendGridApiKey\]" />
  <!--Reminder CRON related settings in 24hour format and 12hours less than Auckland time since its UTC-->
  <add key="paytime:ReminderHour" value="20" />
  <add key="paytime:ReminderMinute" value="00" />
</appSettings>
```
And add SecureDbConnection.config with the below settings

```
<connectionStrings>
  <add name="PaytimeDbContext" connectionString="\[connectionString\]" providerName="System.Data.SqlClient" />
  <add name="PaytimeAzureDbContext" connectionString="\[connectionString\]" />
</connectionStrings>
```