using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000173 RID: 371
	public abstract class SecurityToken : ISafeLogSecurityArtifact
	{
		// Token: 0x060010AD RID: 4269 RVA: 0x00040B6D File Offset: 0x0003ED6D
		internal virtual IEnumerable<Claim> CreateClaims(string issuer)
		{
			return new List<Claim>();
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00040B74 File Offset: 0x0003ED74
		public virtual string UnsafeToString()
		{
			return this.ToString();
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060010AF RID: 4271
		public abstract string Id { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060010B0 RID: 4272
		public abstract string Issuer { get; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060010B1 RID: 4273
		public abstract SecurityKey SecurityKey { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060010B2 RID: 4274
		// (set) Token: 0x060010B3 RID: 4275
		public abstract SecurityKey SigningKey { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060010B4 RID: 4276
		public abstract DateTime ValidFrom { get; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060010B5 RID: 4277
		public abstract DateTime ValidTo { get; }
	}
}
