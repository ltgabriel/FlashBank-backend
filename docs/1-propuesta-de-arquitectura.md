# Propuesta de arquitectura
# Comunicacion entre cuentas y transacciones 
Para evitar que los servicios esten acoplados, usaria rabbitMQ como message broker, asi se comunican de forma asincronica.

# evento que utilizo
- debitar -- (transaccion - cuentas): transaccion pide que debiten una cuenta
- debito completado--(cuenta - transacion): cuenta confirma que el debito fue exitoso.
- debitofallido..(cuenta - transacion):cuenta avisa que el debito fallo

# flujo norma de una transferencia 
1.-transaccion API recibe la solicitud, guarda la transaccion como "pendiente"
2.-publica el evento "debitar" en rabbitMQ
3.-cuenta API escucha, debita el saldo
4.- cuenta publica "debito completado"
5.-transaccion cambia al estado a "completado"

# que pasaria si cuenta/accounts API esta caida
los mensajes quedan en cola de rabbitMQ, cuando la api occount vuelve en servicio procesa todos los pendientes, el usuario no perderia la transacion  

# porque elegi rabbitMQ 
con rabbitMQ veo desacoplamiento, rendimientos automaticos y puedo escalar cuentas con varios consumidores 

tambien e pensado utilizar http directo pero si accounts falla las transacciones tambien fallarian 

Otra opcion seria kafka que de acuerdo a las docummentaciones es mas potente pero por la repides de la implementacion aun no lo implementaria por la urgencia reportada, tamvez me llevaria mas tiempo 
