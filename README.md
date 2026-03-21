# Prueba tecnica: Sistema de Core Bancario "FlashBank"
Propuesta para arreglar los problema de rendimineto y acoplamiento.

# Contexto
FlashBank es un neobanco que necesita rediseñar su infraestructura. Actualmente es un monolito con problemas de acoplamiento y rendimiento

# problema actual identificado
El endpoint `GET /accounts/{id}/history` está causando picos de CPU del 95% en la base de datos. La tabla Transactions tiene cientos de millones de registros y cada vez que un usuario abre la app se hace un SELECT pesado.


**# RESPUESTAS A LOS REQUERIMIENTOS* 

# 1. Propuesta de Arquitectura
**COmo se comunican el servicio de Cuentas y Transacciones para evitar acoplamiento**

Uso RabbitMQ para que los servicios no esten tan pegados. para la explicaion en la ruta del detalle
[Ver detalle](docs/1-propuesta-de-arquitectura.md)

# 2. Optimizacion de Lectura (Pain Point)
**Solución para la base de datos saturada**

Separo la BD de lectura de la de escritura CQRS
[ver_detalle](docs/2-optimizacion-lectura.md)

### 3. Consistencia de Datos
**Mantener consistencia cuando falla una transaccion**

Uso Saga Pattern para que si algo falla, se pueda revertir
[ver_detalle](docs/3-consistencia-de-datos.md)


# Explicación Técnica:

Ver [SOLUTION.md](SOLUTION.md) para la justificacion de:
- Por qué RabbitMQ
- Por qué CQRS 
- Por qué Saga 

## Tecnologias propuestas

- .NET 8
- RabbitMQ
- SQL Server
- Entity Framework Core

## Documentos
**Estos son lo documentos realizados en respuestas a la solucion requerida*

- [Arquitectura](docs/1-propuesta-de-arquitectura.md)
- [Optimizacion lectura](docs/2-optimizacion-lectura.md)
- [Consistencia-de-datos](docs/3-consistencia-de-datos.md)
- [Explicacion tecnicas](SOLUTION.md)