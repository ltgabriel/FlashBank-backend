# OPtmizacion de lectura 
# el problema
la tabla de transactions tiene ciento de millones de registros, cada vez que un usuario abre la app se ase un select pesado para mostrar el historial, esto deja el CPU al 95%

# MI SOLUCION
aplico CQRS (separo la base de datos de lectura de la de escritura)
    *base de escritura*
        - sigue siendo la tabla de transactions normalizada
        - optimizada para insert rapidos
    *base de lectura*
        - una tabla nueva transactionsRead
        - desnormalizo (solo los campos que se consulta)
        - los indices para que las consultas sean mas rapidas 
    *sincronizacion*
    un worker cada 1 a 2 segundos copia los datos de la base de escritura a la base de lectura.

# como queda la tabla lectura
La tabla de lectura tiene los campos que se usan: Id, accoundId amount,type,status, createdat, descripcion. le pongo un indice accountid y createdat para que las consultas sean rapidas.

Con esto las consultas GET ya no tocan la tabla gigante y el consumo de CPU reportado deberia bajar.
