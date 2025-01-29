using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace PersonalExpenseTrackerSystem.Services
{
    public class KeyVaultManagement
    {
        private readonly IConfiguration _config;

        public KeyVaultManagement(IConfiguration config)
        {
            _config = config;
        }

        public SecretClient SecretClient
        {
            get
            {
                return new SecretClient(
                                 new Uri($"{this._config["KeyVault:VaultUri"]}"),
                                 new DefaultAzureCredential());
            }
        }
    }
}
