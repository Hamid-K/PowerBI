using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000080 RID: 128
	internal class TenantIdResolver : TenantIdResolverBase
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override string Resolve(string explicitTenantId, TokenRequestContext context, string[] additionallyAllowedTenantIds)
		{
			bool disableTenantDiscovery = IdentityCompatSwitches.DisableTenantDiscovery;
			if (context.TenantId != explicitTenantId && context.TenantId != null && explicitTenantId != null)
			{
				if (disableTenantDiscovery || explicitTenantId == "adfs")
				{
					AzureIdentityEventSource.Singleton.TenantIdDiscoveredAndNotUsed(explicitTenantId, context.TenantId);
				}
				else
				{
					AzureIdentityEventSource.Singleton.TenantIdDiscoveredAndUsed(explicitTenantId, context.TenantId);
				}
			}
			string text;
			if (disableTenantDiscovery)
			{
				text = explicitTenantId;
			}
			else if (explicitTenantId == "adfs")
			{
				text = explicitTenantId;
			}
			else
			{
				text = context.TenantId ?? explicitTenantId;
			}
			string text2 = text;
			if (explicitTenantId != null && text2 != explicitTenantId && additionallyAllowedTenantIds != TenantIdResolverBase.AllTenants && Array.BinarySearch<string>(additionallyAllowedTenantIds, text2, StringComparer.OrdinalIgnoreCase) < 0)
			{
				throw new AuthenticationFailedException("The current credential is not configured to acquire tokens for tenant " + text2 + ". To enable acquiring tokens for this tenant add it to the AdditionallyAllowedTenants on the credential options, or add \"*\" to AdditionallyAllowedTenants to allow acquiring tokens for any tenant. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/multitenant/troubleshoot");
			}
			return text2;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		public override string[] ResolveAddionallyAllowedTenantIds(IList<string> additionallyAllowedTenants)
		{
			if (additionallyAllowedTenants == null || additionallyAllowedTenants.Count == 0)
			{
				return Array.Empty<string>();
			}
			if (additionallyAllowedTenants.Contains("*"))
			{
				return TenantIdResolverBase.AllTenants;
			}
			return additionallyAllowedTenants.OrderBy((string s) => s).ToArray<string>();
		}
	}
}
