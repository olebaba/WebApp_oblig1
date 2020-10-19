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
        
        private const string _loggetInn = "innlogget";
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
        public async Task HentHoldeplasserLoggetInn()
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

        /*
        [Fact]
        public async Task HentHoldeplasserIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentHoldeplasser()).ReturnsAsync(It.IsAny<List<Holdeplass>>());

            var bestillingController = new BestillingController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await bestillingController.HentHoldeplasser() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        */
        
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
        public async Task FeilInnlogging()
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
        public async Task FeilInputLoggInn()
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
    }
}
