# cosistencia de datos 
## el problema 

en microservicios una transacion puede tocar varios servicios ejm.
1 transactions guarda la transaccion
2 account debita el saldo 
si falta el paso 2 la transaccion queda como "completada" pero el saldo no se debito, aqui hay datos inconsistentes.

# Mi solucion 

usar saga pattern para que cada servicio haga los que tenga que hacer

*flujo normal*
1 transactions guarda como "pendiente"
2 publica el evento "debitar
3 accounts debita y responde "debito completado"
4  transactions cambia a "completado"

*si falla el debito*
1 transactions guarda como"pendiente"
2 publica "debitar"
3 accounts falla (ej saldo insuficiente)
4 accounts responde "debitoFallido"
5 transactions cambia a"failed"
6 el saldo no se toca

