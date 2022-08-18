namespace EventHub.Model
{
    public class CodersInRioData
    {
        public string Cidade { get; set; }
        public string SerialNumber { get; set; }
        public string TipoSensor { get; set; }
        public string ValorSensor { get; set; }
        public DateTime DataGravacao { get; set; }

        public override string ToString()
        {
            return $"Time: {DataGravacao:HH:mm:ss} | {TipoSensor}: {ValorSensor} | "
                 + $"City: {Cidade} | SerialNumber: {SerialNumber} ";
        }

    }
}