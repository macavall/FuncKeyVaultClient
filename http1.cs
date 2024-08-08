using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KeyVaultFunc56
{
    public class http1
    {
        private readonly ILogger<http1> _logger;

        public http1(ILogger<http1> logger)
        {
            _logger = logger;
        }

        [Function("http1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // ========================
            // ========================

            var client = new SecretClient(vaultUri: new Uri("https://macavallkv56.vault.azure.net/"), credential: new DefaultAzureCredential());

            KeyVaultSecret secret = client.GetSecret("mysecret"); // Result should be "mysecretvalue"

            // ========================
            // ========================

            return new OkObjectResult(secret);
        }
    }
}
