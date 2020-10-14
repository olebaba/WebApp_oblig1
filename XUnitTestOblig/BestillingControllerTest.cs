using Microsoft.AspNetCore.Mvc;
using Moq;
using oblig1_1.Controllers;
using oblig1_1.DAL;
using oblig1_1.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOblig
{
    public class BestillingControllerTest
    {
        
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

            var mock = new Mock<IBestillingRepository>();
            mock.Setup(k => k.Lagre(innBestilling)).ReturnsAsync(true);
            var bestilliingController = new BestillingController(mock.Object);

            bool resultat = await bestilliingController.Lagre(innBestilling);

            Assert.True(resultat);
        }
        

       
    }
}
