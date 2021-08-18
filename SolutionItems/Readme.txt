Analisa se a implementação cumpre com os atributos da action:
	- install-package Microsoft.AspNetCore.Mvc.Api.Analyzers

Instalar o automapper:
	- install-package AutoMapper.Extensions.Microsoft.DependencyInjection

	- adicionar no Startup: services.AddAutoMapper(typeof(Startup));
	
	- criar mapeamento do construtor de uma classe herdando de Profile
	
	- o "CreateMap<Supplier, SupplierViewModel>().ReverseMap()" faz o mapeamento em "duas vias"



Gerar o banco:
	- (migration já gerada) Update-Database 
	- se der o erro: "The term 'Update-database' is not recognized as the name of a cmdlet", instalar o pacote: "Microsoft.EntityFrameworkCore.Tools"
	
	
Envio de imagens grandes
	Anotações importantes: 
		- Ter a ropriedade no tipo iformfile, pq ele faz streaming (fatias) do arquivo.
		- [DisableRequestSizeLimit] desabilita o tamanho do request.
		- [RequestSizeLimit(40000000)] Define o tamanho máximo do request em bytes.
		- Numa api sem tratamento, o request com propriedade iformfile dá erro por conta do formato JSON, então
		pra resolver, é necessário criar um "CustomModelBinder". (Classe JsonWithFilesFormDataModelBinder)
			- usar o "MvcNewtonsoftJsonOptions" ao invéz do "MvcJsonOptions"
			- decorar a view model que tem a imagem grande com: [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "product")]
			- para chamar o método via postman, ver exemplo: "MinhaApiCore - Post - LargeImage"

JWT
	- O padrão JWT pode ser utilizado em aplicações de qualquer nível. É muito seguro e utilizado em inúmeras aplicações de alto risco (dados sensíveis ex. banking).
	- Apesar de ser possível ler o conteúdo, apenas com a chave de criptografia é possível manipular e criar um token compatível com a aplicação. 
	  Não devemos expor dados sensíveis no token devido a facilidade em ler seus dados.
	- Em ambientes de nuvem (ex. Azure) é possível salvar a chave nas configurações da App evitando a exposição em texto nos arquivos de configuração.
	- Site para validar o token: https://jwt.io/
    - Token muito grande = problema. Quanto menor a string das claims melhor;
	- Jti = Json Token Id
	- Nbf = Not valid before (UnixEpochDate)
	- Iat = Issued at (UnixEpochDate)
	- Sub = Subject (guid)


Uso obrigatório de HTTPS
	- Previnir um man in the middle usando um pineapple numa wifi aberta pra roubar informações;
	- Uso do HSTS (Conersa cliente e servidor somente em https)
	- UseHttpsRedirection pra forçar https caso tente chamar http
	

Cross-Origin Resource Sharing (CORS)
	- Documentação: https://developer.mozilla.org/pt-BR/docs/Web/HTTP/CORS
	- Mesmo com a politica de CORS implementada, um request funciona usando um postman, 
	  pq ela é um contrato com o navegador
	- [DisableCors] aplica o CORS e [EnableCors("Development")] relaxa o CORS. Caso *NÃO* tenha uma configuração global implementada.

Versionamento
	- intalar pacote: Microsoft.AspNetCore.Mvc.Versioning
	- instalar pacote: Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
	- [ApiVersion("1.0", Deprecated = true)] define a api na versão 1 como obsoleto

Swagger
	- instalar o pacote: Swashbuckle.AspNetCore
	- ver configurações específicas no arquivo SwaggerConfig
	- Os middlewares são executados em ordem de chamada, então, caso tenha SwaggerAuthorizedMiddleware deve estar por primeiro

Elmah
	- documentação: https://elmah.io/
	- precisa um usuário 
	- instalar o pacote: Elmah.Io.AspNetCore
	- instalar pacote: Elmah.Io.Extensions.Logging
	- O elmah só loga erros tratados. Lançar erro com o ex.Ship
	- Para resolver o tópico acima, criar um middleware para manusear os erros. "app.UseMiddleware<ExceptionMiddleware>();"
 

