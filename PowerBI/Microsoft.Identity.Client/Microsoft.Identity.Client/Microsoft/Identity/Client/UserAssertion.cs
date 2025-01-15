using System;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000180 RID: 384
	public sealed class UserAssertion
	{
		// Token: 0x06001274 RID: 4724 RVA: 0x0003EDA0 File Offset: 0x0003CFA0
		public UserAssertion(string jwtBearerToken)
			: this(jwtBearerToken, "urn:ietf:params:oauth:grant-type:jwt-bearer")
		{
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0003EDB0 File Offset: 0x0003CFB0
		public UserAssertion(string assertion, string assertionType)
		{
			if (string.IsNullOrWhiteSpace(assertion))
			{
				throw new ArgumentNullException("assertion");
			}
			if (string.IsNullOrWhiteSpace(assertionType))
			{
				throw new ArgumentNullException("assertionType");
			}
			ICryptographyManager cryptographyManager = PlatformProxyFactory.CreatePlatformProxy(null).CryptographyManager;
			this.AssertionType = assertionType;
			this.Assertion = assertion;
			this.AssertionHash = cryptographyManager.CreateBase64UrlEncodedSha256Hash(this.Assertion);
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0003EE15 File Offset: 0x0003D015
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0003EE1D File Offset: 0x0003D01D
		public string Assertion { get; private set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0003EE26 File Offset: 0x0003D026
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0003EE2E File Offset: 0x0003D02E
		public string AssertionType { get; private set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0003EE37 File Offset: 0x0003D037
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0003EE3F File Offset: 0x0003D03F
		internal string AssertionHash { get; set; }
	}
}
