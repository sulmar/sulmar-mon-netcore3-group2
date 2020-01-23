
# .NET Core

## Struktura projektu

| Projekt  | Technologia  |
|---|---|
|  Resources | WebApi  |
|  Radio | Middleware  |
|  Navigation  | OWIN  |
|  Messaging | Signal-R  |
|  Tracking | gRPC  |


## Przydatne komendy CLI
- ``` dotnet --list-sdks ``` - wyświetlenie listy zainstalowanych SDK
- ``` dotnet new globaljson ``` - utworzenie pliku global.json
- ``` dotnet new globaljson --sdk-version {version} ``` - utworzenie pliku global.json i ustawienie wersji SDK
- ``` dotnet new --list ``` - wyświetlenie listy dostępnych szablonów
- ``` dotnet new {template} ``` - utworzenie nowego projektu na podstawie wybranego szablonu
- ``` dotnet new {template} -o {output} ``` - utworzenie nowego projektu w podanym katalogu
- ``` dotnet restore ``` - pobranie bibliotek nuget na podstawie pliku projektu
- ``` dotnet build ``` - kompilacja projektu
- ``` dotnet run ``` - uruchomienie projektu
- ``` dotnet run {app.dll}``` - uruchomienie aplikacji
- ``` dotnet test ``` - uruchomienie testów jednostkowych
- ``` dotnet run watch``` - uruchomienie projektu w trybie śledzenia zmian
- ``` dotnet test ``` - uruchomienie testów jednostkowych w trybie śledzenia zmian
- ``` dotnet add {project.csproj} reference {library.csproj} ``` - dodanie odwołania do biblioteki
- ``` dotnet remove {project.csproj} reference {library.csproj} ``` - usunięcie odwołania do biblioteki
- ``` dotnet new sln ``` - utworzenie nowego rozwiązania
- ``` dotnet sln {solution.sln} add {project.csproj}``` - dodanie projektu do rozwiązania
- ``` dotnet sln {solution.sln} remove {project.csproj}``` - usunięcie projektu z rozwiązania
- ``` dotnet publish -c Release -r {platform}``` - publikacja aplikacji
- ``` dotnet publish -c Release -r win10-x64``` - publikacja aplikacji dla Windows
- ``` dotnet publish -c Release -r linux-x64``` - publikacja aplikacji dla Linux
- ``` dotnet publish -c Release -r osx-x64``` - publikacja aplikacji dla MacOS
- ``` dotnet add package {package-name} ``` - dodanie pakietu nuget do projektu
- ``` dotnet remove package {package-name} ``` - usunięcie pakietu nuget do projektu

## Protokół HTTP 

request:
~~~
  GET /customers/index.html HTTP/1.1
  host: www.sulmar.pl
  accept: text/html
  {blank-line}
~~~

response:
~~~
  200 OK
  content-type: text/html
  
  <html>...</html>
~~~

request:
~~~
GET api/customers HTTP/1.1
  host: www.sulmar.pl
  accept: application/json
  {blank-line}
~~~

response:
~~~  200 OK
  content-type: application/json
  
  {json}
~~~


request:
~~~ 
 POST api/customers HTTP/1.1
  host: www.sulmar.pl
  content-type: application/xml
  <xml><customer>...</customer></xml>
  {blank-line}
~~~

response:
~~~
201 Created
~~~


## Konfiguracja

### Konfiguracja żródła 

~~~ csharp
 public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddXmlFile("appsettings.xml", optional: true,  reloadOnChange: true);
                    config.AddJsonFile("appsettings.json", optional: false,  reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true,  reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
              }
~~~

### Wstrzykiwanie opcji 

~~~ csharp
public class VehicleOptions
{
    public int Quantity { get; set; }
}
~~~

- Plik konfiguracyjny appsettings.json

~~~ json
{
  "VehicleOptions": {
    "Quantity": 40
  },
  
  ~~~
  
#### Konfiguracja z użyciem interfejsu IOptions<T>

- Instalacja biblioteki

~~~ bash
 dotnet add package Microsoft.Extensions.Options
~~~

- Wstrzykiwanie opcji

~~~ csharp

public class FakeVehicleService
{
   private readonly VehicleOptions options;

    public FakeCustomersService(IOptions<VehicleOptions> options)
    {
        this.options = options.Value;
    }
}
       
~~~

~~~ csharp
        
      public void ConfigureServices(IServiceCollection services)
      {
          services.Configure<VehicleOptions>(Configuration.GetSection("VehicleOptions"));
      }
    }
~~~





#### Konfiguracja bez interfejsu IOptions<T>
  
~~~ csharp
  public void ConfigureServices(IServiceCollection services)
        {
            var vehicleOptions = new VehicleOptions();
            Configuration.GetSection("VehicleOptions").Bind(vehicleOptions);
            services.AddSingleton(vehicleOptions);

            services.Configure<VehicleOptions>(Configuration.GetSection("VehicleOptions"));
        }

~~~

~~~ csharp
public class FakeVehicleService
{
   private readonly VehicleOptions options;

    public FakeCustomersService(VehicleOptions options)
    {
        this.options = options;
    }
}
~~~

