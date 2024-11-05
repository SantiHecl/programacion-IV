using CuponProyecto.Interfaces;

namespace CuponProyecto.Services
{
    public class CuponesServices : ICuponesServices
    {
        public async Task<string> GenerarNroCupon()
        {
            Random random = new Random();
            return $"{random.Next(100, 1000)}-{random.Next(100, 1000)}-{random.Next(100, 1000)}";
        }

    }
}
