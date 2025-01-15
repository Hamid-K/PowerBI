using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000081 RID: 129
	internal abstract class TenantIdResolverBase
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000D70F File Offset: 0x0000B90F
		public static TenantIdResolver Default
		{
			get
			{
				return new TenantIdResolver();
			}
		}

		// Token: 0x06000457 RID: 1111
		public abstract string Resolve(string explicitTenantId, TokenRequestContext context, string[] additionallyAllowedTenantIds);

		// Token: 0x06000458 RID: 1112
		public abstract string[] ResolveAddionallyAllowedTenantIds(IList<string> additionallyAllowedTenants);

		// Token: 0x04000274 RID: 628
		public static readonly string[] AllTenants = new string[] { "*" };
	}
}
