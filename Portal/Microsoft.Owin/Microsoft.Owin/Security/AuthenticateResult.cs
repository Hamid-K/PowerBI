using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200001D RID: 29
	public class AuthenticateResult
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00003D40 File Offset: 0x00001F40
		public AuthenticateResult(IIdentity identity, AuthenticationProperties properties, AuthenticationDescription description)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			if (identity != null)
			{
				this.Identity = (identity as ClaimsIdentity) ?? new ClaimsIdentity(identity);
			}
			this.Properties = properties;
			this.Description = description;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003D96 File Offset: 0x00001F96
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00003D9E File Offset: 0x00001F9E
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003DA7 File Offset: 0x00001FA7
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00003DAF File Offset: 0x00001FAF
		public AuthenticationProperties Properties { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00003DB8 File Offset: 0x00001FB8
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public AuthenticationDescription Description { get; private set; }
	}
}
