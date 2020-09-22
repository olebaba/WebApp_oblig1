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
        Task<List<Bestilling>> index();
        Task<bool> Lagre(Bestilling innBestilling);
        Task<bool> Slett(int id);
        Task<Bestilling> HentEn(int id);
        Task<bool> Endre(Bestilling endreBestilling);

    }
}
