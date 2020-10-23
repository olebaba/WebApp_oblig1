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
        Task<bool> Slett(int id);
        Task<Bestillinger> HentEn(int id);
        Task<bool> Endre(Bestillinger endreBestilling);
        List<RuteAvgang> FinnEnRuteAvgang(string[] holdeplasserOgDato);
        Task<List<Holdeplass>> HentAlleHoldeplasser();
        Task<bool> LoggInn(Bruker bruker);
        Task<Holdeplass> HentHoldeplass(int id);
        Task<bool> EndreHoldeplass(Holdeplass endreHoldeplass);
        Task<bool> LagreHoldeplass(Holdeplass innHS);
        Task<List<RuteStopp>> HentRuteStopp();
        Task<RuteStopp> EtRuteStopp(int id);
        RuteStopp NyttRuteStopp(string[] argumenter);
        RuteAvgang NyRuteAvgang(string[] argumenter);
        Task<bool> SlettRS(int id);
        Task<bool> EndreRS(RuteStopp endreRS);
        Task<bool> LagreRS(RuteStopp innRS);
        Task<bool> SlettHoldeplass(int id);
        Task<List<Rute>> AlleRuter();
        Task<Rute> EnRute(int id);
        Task<bool> LagreRute(string navn);
        Task<List<Priser>> HentPriser();
        Task<Priser> EnPris(int id);
        Task<bool> EndrePriser(Priser pris);
    }
}
