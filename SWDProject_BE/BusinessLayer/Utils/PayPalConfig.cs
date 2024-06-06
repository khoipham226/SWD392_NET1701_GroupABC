using PayPalCheckoutSdk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Utils
{
    public static class PayPalConfig
    {
        public static PayPalHttpClient GetClient()
        {
            var environment = new SandboxEnvironment("Your-Client-Id", "Your-Client-Secret");
            return new PayPalHttpClient(environment);
        }
    }
}
