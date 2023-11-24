namespace DentalApi.HelperModels
{
    public class HelpherMethods
    {
        public static string? GetStatusText(int estado)
        {
            string? result = "";
            switch (estado)
            {
                case (Int32)Constants.DentalSolicitudCitaStatus.aceptada:
                    result = "Aceptada";
                    break;
                case (Int32)Constants.DentalSolicitudCitaStatus.cancelada:
                    result = "Cancelada";
                    break;
                case (Int32)Constants.DentalSolicitudCitaStatus.pendiente:
                    result = "Pendiente";
                    break;
                case (Int32)Constants.DentalSolicitudCitaStatus.rechazada:
                    result = "Rechazada";
                    break;
            }

            return result;
        }
    }
}
