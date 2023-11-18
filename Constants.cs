namespace DentalApi
{
    public class Constants
    {
        public enum DentalMessageStatusEnum
        {
            pendiente = 1,
            leido = 2,
            eliminadoPorRemitente = 3,
            eliminadoPorDestinatario = 4
        }

        public enum DentalNotificationsStatusEnum
        {
            pendiente = 1,
            leido = 2
        }
    }
}
