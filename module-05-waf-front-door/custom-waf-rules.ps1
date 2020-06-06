az login

# Install the following module if needed
# Install-Module Az.Network -Force

# Create a geo-match custom rule
$var = New-AzApplicationGatewayFirewallMatchVariable -VariableName RemoteAddr

$condition = New-AzApplicationGatewayFirewallCondition -MatchVariable $var `
 -Operator IPMatch -MatchValue "<IP ADDRESS YOU LIKE TO BLOCK>" -NegationCondition $False

$rule = New-AzApplicationGatewayFirewallCustomRule -Name blockMyIp `
 -Priority 10 -RuleType MatchRule -MatchCondition $condition -Action Block

# Create a firewall policy
$wafPolicy = New-AzApplicationGatewayFirewallPolicy -Name "wafpolicyNew03" `
 -ResourceGroupName "rg-app-security-pluralsight" -Location "canadacentral" -CustomRule $rule

$a = Get-AzApplicationGateway -Name "app-gtw-ps-demo-03" `
 -ResourceGroupName "rg-app-security-pluralsight"

$a.FirewallPolicy = $wafPolicy

Set-AzApplicationGateway -ApplicationGateway $a