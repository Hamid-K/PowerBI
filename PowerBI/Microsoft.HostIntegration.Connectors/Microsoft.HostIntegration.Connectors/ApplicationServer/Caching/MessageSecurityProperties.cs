using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D0 RID: 208
	internal class MessageSecurityProperties : ConfigurationElement
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00015607 File Offset: 0x00013807
		internal MessageSecurityProperties()
		{
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00017D05 File Offset: 0x00015F05
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00017D17 File Offset: 0x00015F17
		[ConfigurationProperty("authorizationInfo")]
		public string AuthorizationInfo
		{
			get
			{
				return (string)base["authorizationInfo"];
			}
			set
			{
				base["authorizationInfo"] = value;
			}
		}

		// Token: 0x040003C5 RID: 965
		internal const string AUTHORIZATION_INFO = "authorizationInfo";
	}
}
