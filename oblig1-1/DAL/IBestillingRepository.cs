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
        RuteAvgang FinnEnRuteAvgang(RuteAvgang reise);
        Task<List<Holdeplass>> HentHoldeplasser();
        Task<bool> LoggInn(Bruker bruker);
        Task<Holdeplass> HentHoldeplass(int id);
        Task<bool> EndreHoldeplass(Holdeplass endreHoldeplass);
        Task<List<RuteStopp>> HentRuteStopp();
        Task<RuteStopp> EtRuteStopp(int id);
        Task<bool> SlettRS(int id);
        Task<bool> EndreRS(RuteStopp endreRS);

    }
}
