using NLog;
using NUnit.Framework;
using SubstrateNetApi;
using SubstrateNetApi.MetaDataModel.Values;
using System;
using System.Threading.Tasks;

namespace SubstrateNetApiTest
{
    public class GetStorageTests
    {

        private const string WebSocketUrl = "wss://boot.worldofmogwais.com";

        private SubstrateClient _substrateClient;

        [SetUp]
        public void Setup()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);

            // Apply config           
            LogManager.Configuration = config;

            _substrateClient = new SubstrateClient(new Uri(WebSocketUrl));
        }

        [TearDown]
        public void TearDown()
        {
            _substrateClient.Dispose();
        }

        [Test]
        public async Task BasicTestAsync()
        {
            await _substrateClient.ConnectAsync();

            var reqResult = await _substrateClient.GetStorageAsync("Sudo", "Key");
            Assert.AreEqual("AccountId", reqResult.GetType().Name);
            Assert.IsTrue(reqResult is AccountId);

            var accountId = (AccountId)reqResult;
            Assert.AreEqual("5GYZnHJ4dCtTDoQj4H5H9E727Ykv8NLWKtPAupEc3uJ89BGr", accountId.Address);

            await _substrateClient.CloseAsync();
        }

        [Test]
        public async Task ParameterizedTestAsync()
        {
            await _substrateClient.ConnectAsync();

            var request = await _substrateClient.GetStorageAsync("Dmog", "AllMogwaisArray", "0");
            Assert.AreEqual("Hash", request.GetType().Name);
            Assert.IsTrue(request is Hash);

            await _substrateClient.CloseAsync();
        }
    }
}