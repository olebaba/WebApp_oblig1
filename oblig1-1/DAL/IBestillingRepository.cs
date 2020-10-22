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
        Task<List<Bestillinger>> Index();
        Task<bool> Lagre(Bestillinger innBestilling);
        Task<bool> Slett(int id);
        Task<Bestillinger> HentEn(int id);
        Task<bool> Endre(Bestillinger endreBestilling);
        RuteAvgang FinnEnRuteAvgang(RuteAvgang reise);
        Task<List<Holdeplass>> HentAlleHoldeplasser();
        Task<List<Holdeplass>> VisHoldeplasserIRute(int id);
        Rute FinnRute(Holdeplass holdeplass);
        Task<List<Priser>> HentPriser();
        Task<bool> EndrePriser(Priser pris);
        Task<bool> LoggInn(Bruker bruker);
        Task<bool> SlettHoldeplass(int id);
        Task<bool> SlettRute(int id);

    }
}
