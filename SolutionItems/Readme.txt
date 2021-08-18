Analisa se a implementa��o cumpre com os atributos da action:
	- install-package Microsoft.AspNetCore.Mvc.Api.Analyzers

Instalar o automapper:
	- install-package AutoMapper.Extensions.Microsoft.DependencyInjection

	- adicionar no Startup: services.AddAutoMapper(typeof(Startup));
	
	- criar mapeamento do construtor de uma classe herdando de Profile
	
	- o "CreateMap<Supplier, SupplierViewModel>().ReverseMap()" faz o mapeamento em "duas vias"



Gerar o banco:
	- (migration j� gerada) Update-Database 
	- se der o erro: "The term 'Update-database' is not recognized as the name of a cmdlet", instalar o pacote: "Microsoft.EntityFrameworkCore.Tools"
	
	
Envio de imagens grandes
	Anota��es importantes: 
		- Ter a ropriedade no tipo iformfile, pq ele faz streaming (fatias) do arquivo.
		- [DisableRequestSizeLimit] desabilita o tamanho do request.
		- [RequestSizeLimit(40000000)] Define o tamanho m�ximo do request em bytes.
		- Numa api sem tratamento, o request com propriedade iformfile d� erro por conta do formato JSON, ent�o
		pra resolver, � necess�rio criar um "CustomModelBinder". (Classe JsonWithFilesFormDataModelBinder)
			- usar o "MvcNewtonsoftJsonOptions" ao inv�z do "MvcJsonOptions"
			- decorar a view model que tem a imagem grande com: [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "product")]
			- para chamar o m�todo via postman, ver exemplo: "MinhaApiCore - Post - LargeImage"

JWT
	- O padr�o JWT pode ser utilizado em aplica��es de qualquer n�vel. � muito seguro e utilizado em in�meras aplica��es de alto risco (dados sens�veis ex. banking).
	- Apesar de ser poss�vel ler o conte�do, apenas com a chave de criptografia � poss�vel manipular e criar um token compat�vel com a aplica��o. 
	  N�o devemos expor dados sens�veis no token devido a facilidade em ler seus dados.
	- Em ambientes de nuvem (ex. Azure) � poss�vel salvar a chave nas configura��es da App evitando a exposi��o em texto nos arquivos de configura��o.
	- Site para validar o token: https://jwt.io/
    - Token muito grande = problema. Quanto menor a string das claims melhor;
	- Jti = Json Token Id
	- Nbf = Not valid before (UnixEpochDate)
	- Iat = Issued at (UnixEpochDate)
	- Sub = Subject (guid)


Uso obrigat�rio de HTTPS
	- Previnir um man in the middle usando um pineapple numa wifi aberta pra roubar informa��es;
	- Uso do HSTS (Conersa cliente e servidor somente em https)
	- UseHttpsRedirection pra for�ar https caso tente chamar http
	

Cross-Origin Resource Sharing (CORS)
	- Documenta��o: https://developer.mozilla.org/pt-BR/docs/Web/HTTP/CORS
	- Mesmo com a politica de CORS implementada, um request funciona usando um postman, 
	  pq ela � um contrato com o navegador
	- [DisableCors] aplica o CORS e [EnableCors("Development")] relaxa o CORS. Caso *N�O* tenha uma configura��o global implementada.

Versionamento
	- intalar pacote: Microsoft.AspNetCore.Mvc.Versioning
	- instalar pacote: Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
	- [ApiVersion("1.0", Deprecated = true)] define a api na vers�o 1 como obsoleto

Swagger
	- instalar o pacote: Swashbuckle.AspNetCore
	- ver configura��es espec�ficas no arquivo SwaggerConfig
	- Os middlewares s�o executados em ordem de chamada, ent�o, caso tenha SwaggerAuthorizedMiddleware deve estar por primeiro

Elmah
	- documenta��o: https://elmah.io/
	- precisa um usu�rio 
	- instalar o pacote: Elmah.Io.AspNetCore
	- instalar pacote: Elmah.Io.Extensions.Logging
	- O elmah s� loga erros tratados. Lan�ar erro com o ex.Ship
	- Para resolver o t�pico acima, criar um middleware para manusear os erros. "app.UseMiddleware<ExceptionMiddleware>();"
 

