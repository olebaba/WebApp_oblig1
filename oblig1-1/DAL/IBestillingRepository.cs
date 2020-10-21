using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.DAL
{
    public interface IBestillingRepository
    {
        Task<List<RuteAvgang>> VisAlleRuteAvganger();
        Task<List<Bestillinger>> index();
        Task<bool> Lagre(Bestillinger innBestilling);
        Task<bool> Slett(int id);
        Task<Bestillinger> HentEn(int id);
        Task<bool> Endre(Bestillinger endreBestilling);
        public List<RuteAvgang> FinnEnRuteAvgang(Holdeplass fra, Holdeplass til, DateTime dato);
        Task<List<Holdeplass>> HentHoldeplasser();
        Task<List<Holdeplass>> VisHoldeplasserIRute(int id);
        Rute FinnRute(Holdeplass holdeplass);

    }
}
