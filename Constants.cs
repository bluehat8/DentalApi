namespace DentalApi
{
    public class Constants
    {
        public enum DentalRole
        {
            admin = 0,
            cliente = 1,
            doctor = 2,
            asistente = 3
        }
        public enum DentalMessageStatusEnum
        {
            pendiente = 1,
            leido = 2,
            eliminadoPorRemitente = 3,
            eliminadoPorDestinatario = 4
        }

        public enum DentalSolicitudCitaStatus
        {
            pendiente = 1,
            aceptada = 2,
            cancelada = 4,
            rechazada = 5,
        }

        public enum DentalNotificationsStatusEnum
        {
            pendiente = 1,
            leido = 2
        }
    }
}
