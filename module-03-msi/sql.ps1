Install-Module -Name Az -AllowClobber

Connect-AzAccount

CLI:

az sql server ad-admin create --resource-group Pluralsight-06 --server-name azuresqlmsidemosrv --display-name admin_user --object-id 59e9adae-d50f-4a8a-b852-ac9d6a0ad832



{
  "id": "/subscriptions/19969c81-e8ff-4585-8c2f-3f196b588227/resourceGroups/Pluralsight-06/providers/Microsoft.Sql/servers/azuresqlmsidemosrv/administratorOperationResults/ActiveDirectory",
  "kind": null,
  "location": "East US",
  "login": "admin_user",
  "name": "ActiveDirectory",
  "resourceGroup": "Pluralsight-06",
  "sid": "b62914a2-3b54-480a-90fb-d221a946522a",
  "tenantId": "0ec02b79-d89f-48c4-9870-da4a7498d887",
  "type": "Microsoft.Sql/servers/administrators"
}