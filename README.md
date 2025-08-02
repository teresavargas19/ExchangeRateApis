Exchange Rate Comparator

Una aplicación simple pero poderosa en .NET 9.0 que consulta múltiples APIs de tasas de cambio y te dice automáticamente cuál ofrece la mejor tasa. Ideal para elegir a quién le mandas tu dinero al extranjero.  

¿Qué hace esta API?
- Consulta varias APIs de tasas de cambio **en paralelo**
- Elige automáticamente la **mejor oferta disponible**
- Sigue funcionando aunque alguna API falle
- Resistente a errores, con **pruebas unitarias completas**
- Soporta respuestas en **JSON y XML**
- Registra errores y resultados para auditoría

Tecnologías y diseño
- .NET 9.0
- Arquitectura limpia (Clean Architecture)
- Principios SOLID
- Inyección de dependencias (`IApiStrategy`)
- Procesamiento paralelo con `Task.WhenAll`
- Pruebas unitarias con **xUnit + Moq**

Ejecutar proyecto 
git clone https://github.com/teresavargas19/ExchangeRateApis.git
cd ExchangeRateApis
docker-compose up --build

Pruebas
Cobertura total de los escenarios críticos:
- Selección de la mejor tasa entre múltiples APIs
- Falla total o parcial de proveedores
- Respuesta nula
- Empates de tasas
- Lógica desacoplada y fácil de testear

Ejecutar pruebas
dotnet test
