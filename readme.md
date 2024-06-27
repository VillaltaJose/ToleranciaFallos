# Tolerancia a fallos
## Mecanismos implementados
### Load Balancer
El balanceo de carga distribuye las solicitudes entrantes entre múltiples servidores backend, asegurando que ningún servidor se sobrecargue. Esto mejora la disponibilidad y escalabilidad del sistema, permitiendo que si un servidor falla, las solicitudes se redirijan automáticamente a otros servidores activos, garantizando un servicio continuo.

### Retry Logic
La lógica de reintento permite que las aplicaciones manejen fallos temporales de conexión o errores en transacciones de base de datos. Al reintentar automáticamente la operación fallida después de un breve retraso, se mejora la resiliencia del sistema frente a fallos transitorios, reduciendo la probabilidad de fallos completos en las transacciones y mejorando la robustez de las operaciones críticas de backend.

### Taks Queues
Las colas de tareas permiten gestionar y procesar de forma asíncrona tareas como el envío de correos electrónicos. Esto desacopla la operación de envío de correos del flujo principal de la aplicación, mejorando la eficiencia y la tolerancia a fallos. Si hay un fallo en el envío de un correo, la tarea puede ser reintentada o reprogramada, asegurando que los correos eventualmente se envíen sin bloquear otras operaciones del sistema.

## Arquitectura

## Herramientas