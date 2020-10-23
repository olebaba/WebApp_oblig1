using Castle.Core.Logging;
using KundeAppTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using oblig1_1.Controllers;
using oblig1_1.DAL;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOblig
{
    public class BestillingControllerTest
    {
        
        private const string _loggetInn = "innlogget";
        private const string _ikkeLoggetInn = "";
        

        private readonly Mock<IBestillingRepository> mockRep = new Mock<IBestillingRepository>();
        private readonly Mock<ILogger<AdminController>> mockLog = new Mock<ILogger<AdminController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        /*
        [Fact]
        public async Task Lagre()
        {
            var nyKunde = new Kunde
            {
                KID = 1,
                Navn = "Simon",
                Prisklasse = "voksen",
                Mobilnummer = "90907070"
            };

            var innBestilling = new Bestillinger
            {
                ID = 1,
                Pris = 150,
                Kunde = nyKunde
            };

            mockRep.Setup(k => k.Lagre(It.IsAny<Bestillinger>())).ReturnsAsync(true);
            var bestillingController = new BestillingController(mockRep.Object, mockLog.Object);

            var resultat = await bestillingController.Lagre(It.IsAny<Bestillinger>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestillingen er lagret", resultat.Value);
        }
        */
        [Fact]
        public async Task HentHoldeplasserLoggetInn()
        {
            var kongsberg = new Holdeplass { Sted = "Kongsberg", Sone = 2 };
            var notodden = new Holdeplass { Sted = "Notodden", Sone = 3 };

            var holdListe = new List<Holdeplass>();
            holdListe.Add(kongsberg);
            holdListe.Add(notodden);

            mockRep.Setup(k => k.HentAlleHoldeplasser()).ReturnsAsync(holdListe);
            var bestillingController = new AdminController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            var resultat = await bestillingController.AdminHentHoldeplasser() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Holdeplass>>((List<Holdeplass>)resultat.Value, holdListe);
        }

        
        [Fact]
        public async Task HentHoldeplasserIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentAlleHoldeplasser()).ReturnsAsync(It.IsAny<List<Holdeplass>>());

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.AdminHentHoldeplasser() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentPriserLoggetInn()
        {
            var pris1 = new Priser { Prisklasse = "Voksen", Pris1Sone = 40, Pris2Sone = 60, Pris3Sone = 70, Pris4Sone = 80 };
            var pris2 = new Priser { Prisklasse = "Barn", Pris1Sone = 20, Pris2Sone = 40, Pris3Sone = 50, Pris4Sone = 60 };

            var prisListe = new List<Priser>();
            prisListe.Add(pris1);
            prisListe.Add(pris2);

            mockRep.Setup(k => k.HentPriser()).ReturnsAsync(prisListe);
            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentPriser() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Priser>>((List<Priser>)resultat.Value, prisListe);
        }


        [Fact]
        public async Task HentPriserIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentPriser()).ReturnsAsync(It.IsAny<List<Priser>>());

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentPriser() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task GodkjentInnlogging() {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task InnloggingIkkeGodkjent()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task InnloggingFeilInput()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            bestillingController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            bestillingController.LoggUt();

            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }

        [Fact]
        public async Task SlettHoldeplassLoggetInn()
        {
            mockRep.Setup(k => k.SlettHoldeplass(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettHoldeplass(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Sletting utført", resultat.Value);
        }

        [Fact]
        public async Task SlettHoldeplassLoggetInnFeil()
        {
            mockRep.Setup(k => k.SlettHoldeplass(It.IsAny<int>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettHoldeplass(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke slette", resultat.Value);
        }

        [Fact]
        public async Task SlettHoldeplassIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettHoldeplass(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettHoldeplass(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndrePriserLoggetInn()
        {
            mockRep.Setup(k => k.EndrePriser(It.IsAny<Priser>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndrePriser(It.IsAny<Priser>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Priser endret", resultat.Value);
        }

        [Fact]
        public async Task EndrePriserLoggetInnFeil()
        {
            mockRep.Setup(k => k.EndrePriser(It.IsAny<Priser>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndrePriser(It.IsAny<Priser>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av prisene kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task FeilInputEndrePriserLoggetInn()
        {
            mockRep.Setup(k => k.EndrePriser(It.IsAny<Priser>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            bestillingController.ModelState.AddModelError("Pris1Sone", "Feil i inputvalidering");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndrePriser(It.IsAny<Priser>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndrePriserIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndrePriser(It.IsAny<Priser>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndrePriser(It.IsAny<Priser>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettRSLoggetInn()
        {
            mockRep.Setup(k => k.SlettRS(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettRS(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Sletting utført", resultat.Value);
        }

        [Fact]
        public async Task SlettRSLoggetInnFeil()
        {
            mockRep.Setup(k => k.SlettRS(It.IsAny<int>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettRS(It.IsAny<int>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunne ikke slette", resultat.Value);
        }

        [Fact]
        public async Task SlettRSIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettRS(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettRS(It.IsAny<int>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        /*
        [Fact]
        public async Task SlettRuteLoggetInn()
        {
            mockRep.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.SlettRute(It.IsAny<int>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Holdeplass lagret", resultat.Value);
        }
        */
        [Fact]
        public async Task LagreHoldeplassLoggetInnFeil()
        {
            mockRep.Setup(k => k.LagreHoldeplass(It.IsAny<Holdeplass>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreHoldeplass(It.IsAny<Holdeplass>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Holdeplass kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreHoldeplassFeilInput()
        {
            var holdeplass1 = new Holdeplass { ID = 1, Sted = "", Sone = 1 };
            mockRep.Setup(k => k.LagreHoldeplass(holdeplass1)).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);
            bestillingController.ModelState.AddModelError("Sted", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreHoldeplass(holdeplass1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreHoldeplassIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagreHoldeplass(It.IsAny<Holdeplass>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreHoldeplass(It.IsAny<Holdeplass>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreHoldeplassLoggetInn()
        {
            mockRep.Setup(k => k.EndreHoldeplass(It.IsAny<Holdeplass>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreHoldeplass(It.IsAny<Holdeplass>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Holdeplass endret", resultat.Value);
        }

        [Fact]
        public async Task EndreHoldeplassLoggetInnFeil()
        {
            mockRep.Setup(k => k.EndreHoldeplass(It.IsAny<Holdeplass>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreHoldeplass(It.IsAny<Holdeplass>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av holdeplassen kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreHoldeplassFeilInput()
        {
            var holdeplass1 = new Holdeplass { ID = 1, Sted = "", Sone = 1 };
            mockRep.Setup(k => k.EndreHoldeplass(It.IsAny<Holdeplass>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);
            bestillingController.ModelState.AddModelError("Sted", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreHoldeplass(holdeplass1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreHoldeplassIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreHoldeplass(It.IsAny<Holdeplass>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreHoldeplass(It.IsAny<Holdeplass>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LagreRSLoggetInn()
        {
            mockRep.Setup(k => k.LagreRS(It.IsAny<RuteStopp>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRS(It.IsAny<RuteStopp>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rutestopp lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreRSLoggetInnFeil()
        {
            mockRep.Setup(k => k.LagreRS(It.IsAny<RuteStopp>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRS(It.IsAny<RuteStopp>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Lagring av RuteStopp kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task LagreRSLoggetInnFeilInput()
        {
            var oslo = new Holdeplass { Sted = "", Sone = 1 };
            var OsloStavanger = new Rute { Navn = "Oslo-Stavanger" };
            var RuteOsloStavangerStoppOslo = new RuteStopp { Holdeplass = oslo, Rute = OsloStavanger, StoppTid = TimeSpan.FromMinutes(0) };
            mockRep.Setup(k => k.LagreRS(RuteOsloStavangerStoppOslo)).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);
            bestillingController.ModelState.AddModelError("Holdeplass", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRS(RuteOsloStavangerStoppOslo) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreRSIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagreRS(It.IsAny<RuteStopp>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRS(It.IsAny<RuteStopp>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreRSLoggetInn()
        {
            mockRep.Setup(k => k.EndreRS(It.IsAny<RuteStopp>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreRS(It.IsAny<RuteStopp>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rutestopp endret", resultat.Value);
        }

        [Fact]
        public async Task EndreRSLoggetInnFeil()
        {
            mockRep.Setup(k => k.EndreRS(It.IsAny<RuteStopp>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreRS(It.IsAny<RuteStopp>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av RuteStopp kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreRSLoggetInnFeilInput()
        {
            var oslo = new Holdeplass { Sted = "", Sone = 1 };
            var OsloStavanger = new Rute { Navn = "Oslo-Stavanger" };
            var RuteOsloStavangerStoppOslo = new RuteStopp { Holdeplass = oslo, Rute = OsloStavanger, StoppTid = TimeSpan.FromMinutes(0) };
            mockRep.Setup(k => k.EndreRS(RuteOsloStavangerStoppOslo)).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);
            bestillingController.ModelState.AddModelError("Holdeplass", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreRS(It.IsAny<RuteStopp>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreRSIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreRS(It.IsAny<RuteStopp>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.EndreRS(It.IsAny<RuteStopp>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteLoggetInn()
        {
            mockRep.Setup(k => k.LagreRute(It.IsAny<String>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRute(It.IsAny<String>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteLoggetInnFeil()
        {
            mockRep.Setup(k => k.LagreRute(It.IsAny<String>())).ReturnsAsync(false);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRute(It.IsAny<String>()) as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Lagring av Rute kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteFeilInput()
        {
            var OsloStavanger = new Rute { Navn = "" };
            mockRep.Setup(k => k.LagreRute(It.IsAny<String>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);
            bestillingController.ModelState.AddModelError("Navn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRute(It.IsAny<String>()) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagreRute(It.IsAny<String>())).ReturnsAsync(true);

            var bestillingController = new AdminController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.LagreRute(It.IsAny<String>()) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
    }
}
