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
	- 


