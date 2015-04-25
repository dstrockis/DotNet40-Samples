using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    // You would pull this configuration data from somewhere else in a real system.
    static class TenantSecurityConfig
    {
        static Dictionary<string, double> ClientSecurityRules = new Dictionary<string, double>
        {
            {"71798d80-f0d7-4824-bc48-89ffe231ce22", 2}, // Hospital B
            {"60ffe505-3ef1-47e2-8553-2bbe12dd494f", 2}, // Default Tenant
            {"0c70f9e5-098a-4684-a0f4-630d9aa29abf", 2}, // Hospital A
        };

        public static double GetTenantInactivityPeriod(string tenantId, double standard)
        {
            double temp;
            if (ClientSecurityRules.TryGetValue(tenantId, out temp))
                return temp;
            return standard;
        }
    }
}
