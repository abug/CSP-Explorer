# CSP Explorer Sample

The CSP Explorer sample demonstrates CREST and Graph API authentication, gathering usage data, and gathering RateCard data for CSP subscriptions.

## Running the sample

In order to run the sample, modify the web.config with appropriate application id's and keys from partner center and your Azure Active Directory Tenant. Follow instructions [here](https://msdn.microsoft.com/en-us/library/partnercenter/mt267552.aspx) to enable API access for partner center. Follow instructions [here](http://blogs.msdn.com/b/dsadsi/archive/2013/11/26/how-to-create-a-service-principal-using-the-msol-cmdlets-for-use-with-the-waad-graph-api.aspx) for creating an AAD service principal with directory read permission to access the Graph API, or use the included [CreateCSPExplorerADSPN.ps1](CSP-Utilities\CreateCSPExplorerADSPN.ps1) powershell script. You will need to update the following information in the web.config on the CSP-Web project:

* reseller_tenant_name	=	AAD Domain Name of partner
* crest_app_id			=	App ID from partner center CREST API integration page
* crest_account_id		=	Account ID from partner center CREST API integration page
* crest_app_key			=	Key from partner center CREST API integration page
* graph_app_id			=	Client ID from AAD application configuration page
* graph_app_key			=	Key from AAD application configuration page

## Project Information

CSP Explorer uses a services layer to interact with various API's

* CSP-Foundation			->	This project contains base classes for making HTTP API calls. Additionally, a StructureMap dependency resolver class is included.
* CSP-CREST 				-> 	This project contains services and entities for interacting with CREST API.
* CSP-Graph					->	This project contains services and entities for interacting with Graph API.
* CSP-Web					->	This project is a MVC web front-end for interacting with CREST and Graph API's. Data is retrieved and * displayed using JSON.

## License
[MIT](LICENSE)