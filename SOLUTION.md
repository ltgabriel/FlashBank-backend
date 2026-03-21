# Explicacion tecnica

# 1 PROPUESTA DE ARQUITECTURA
# Porque rabbitMQ pra la comunicacion de servicios 
Porque es faciil de correr con docker para desarrallo, la libreria para .NET funciona bien.
Los eventos que usaría:
- Debitar (transacciones - cuenta)
- debito_completado (cuenta - transacion)
- debitofallido (cuenta - transacion)

También averigué sobre Kafka, sería una buena opción, pero en mi caso no la propondría para una solución rápida. Si me dieran tiempo para investigar más de Kafka, sería buena opción para procesar millones de eventos por segundo.

# 2 OPTIMIZACION DE LECTURA
## Porque CQRS para optimizar lecturas
La tabla tiene millones de registros y pormas índices que ponga, las consultas van a seguir siendo pesadas.
separa la base de datos de la lectura y ezcritura nos permite.
- La base de escritura sigue normalizada para inserts rápidos
- La base de lectura tiene índices columnstore que son más rápidos

Se que hay consistencia eventual (los datos se ven con 1-2 seg de retraso), pero para un historial de transacciones no es grave

Otra opción era usar Redis cache, pero para una solucion rapida no seria opcion esto seria analizar ocn el equipo para implementar en una proxima oportunidad para una mejora extra

# 3 CONSISTENCIA DE DATOS
# Porque saga pattern para la consistencia 

si falta el debito, la transacion queda como "pendiente" pero el saldo no se debita

con saga:
1 transactions guarda como "pendiente"
2 publica "debitar
3 accounts debita y responde "debito completado" o "debito fallido "
4 ransactions cambia el estado segun corresponda

No use two-phase commit porque bloquea recursos y no escala bien en microservicios. En cambio saga es mas liviano y tolerante a fallos.