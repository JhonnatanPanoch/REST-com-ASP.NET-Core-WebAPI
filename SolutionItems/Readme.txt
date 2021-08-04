Instalar o automapper:
	
	install-package AutoMapper.Extensions.Microsoft.DependencyInjection

	adicionar no Startup: services.AddAutoMapper(typeof(Startup));
	
	criar mapeamento do construtor de uma classe herdando de Profile
	
	o "CreateMap<Supplier, SupplierViewModel>().ReverseMap()" faz o mapeamento em "duas vias"



Gerar o banco
	(migration já gerada) Update-Database 
	se der o erro: "The term 'Update-database' is not recognized as the name of a cmdlet", instalar o pacote: "Microsoft.EntityFrameworkCore.Tools"