using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.DAL
{
    public interface IBestillingRepository
    {
        Task<List<Rute>> VisAlleRuter();
        Task<List<Bestillinger>> index();
        Task<bool> Lagre(Bestillinger innBestilling);
        Task<bool> Slett(int id);
        Task<Bestillinger> HentEn(int id);
        Task<bool> Endre(Bestillinger endreBestilling);
        Rute FinnEnRute(RuteStopp reise);
        Task<List<Holdeplass>> HentHoldeplasser();

    }
}
