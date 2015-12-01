$spn = "https://CSP-Explorer"
$displayName = "CSP Explorer"
$password = "pass@word1"

$creds = Get-Credential
Connect-MsolService -Credential $creds

$servicePrincipal = New-MsolServicePrincipal -ServicePrincipalNames $spn -DisplayName $displayName -Type Password -Value $password
Add-MsolRoleMember -RoleObjectId "88d8e3e3-8f55-4a1e-953a-9b9898b8876b" -RoleMemberType ServicePrincipal -RoleMemberObjectId $servicePrincipal.ObjectId

Write-Host "App Key:`t`t$password"
Write-Host "App Key:`t$password"