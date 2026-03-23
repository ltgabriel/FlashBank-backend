namespace Shared.Events
{
    // evento debitar - txt pide que debiten una cuenta
    public class DebitarEvent
    {
        public Guid TransaccionId { get; set; }
        public Guid CuentaId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }

    // evento deb completado - cta confirma que debito fue exitoso
    public class DebitoCompletadoEvent
    {
        public Guid TransaccionId { get; set; }
        public Guid CuentaId { get; set; }
        public decimal Monto { get; set; }
    }

    // evento debito fallido - cuenta avisa que debito fallo
    public class DebitoFallidoEvent
    {
        public Guid TransaccionId { get; set; }
        public Guid CuentaId { get; set; }
        public string? Motivo { get; set; }
    }
}