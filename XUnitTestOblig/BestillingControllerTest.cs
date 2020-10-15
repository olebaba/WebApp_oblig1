using KundeAppTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";
        

        private readonly Mock<IBestillingRepository> mockRep = new Mock<IBestillingRepository>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        
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
            var bestillingController = new BestillingController(mockRep.Object);

            var resultat = await bestillingController.Lagre(It.IsAny<Bestillinger>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestillingen er lagret", resultat.Value);
        }
        
        [Fact]
        public async Task HentAlleLoggetInn()
        {
            var kongsberg = new Holdeplass { Sted = "Kongsberg", Avgangstider = "0940, 1140" };
            var notodden = new Holdeplass { Sted = "Notodden", Avgangstider = "1015, 1215" };

            var holdListe = new List<Holdeplass>();
            holdListe.Add(kongsberg);
            holdListe.Add(notodden);

            mockRep.Setup(k => k.HentHoldeplasser()).ReturnsAsync(holdListe);
            var bestillingController = new BestillingController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            var resultat = await bestillingController.HentHoldeplasser() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Holdeplass>>((List<Holdeplass>)resultat.Value, holdListe);
        }

        [Fact]
        public async Task HentAlleIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentHoldeplasser()).ReturnsAsync(It.IsAny<List<Holdeplass>>());

            var bestillingController = new BestillingController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentHoldeplasser as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

    }
}
