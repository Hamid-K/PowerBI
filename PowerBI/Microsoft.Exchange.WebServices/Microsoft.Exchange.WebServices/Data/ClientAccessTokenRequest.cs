using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000042 RID: 66
	public class ClientAccessTokenRequest : ComplexProperty
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000BE37 File Offset: 0x0000AE37
		public ClientAccessTokenRequest(string id, ClientAccessTokenType tokenType)
			: this(id, tokenType, null)
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BE42 File Offset: 0x0000AE42
		public ClientAccessTokenRequest(string id, ClientAccessTokenType tokenType, string scope)
		{
			this.id = id;
			this.tokenType = tokenType;
			this.scope = scope;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000BE5F File Offset: 0x0000AE5F
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000BE67 File Offset: 0x0000AE67
		public ClientAccessTokenType TokenType
		{
			get
			{
				return this.tokenType;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000BE6F File Offset: 0x0000AE6F
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x0400014E RID: 334
		private readonly string id;

		// Token: 0x0400014F RID: 335
		private readonly ClientAccessTokenType tokenType;

		// Token: 0x04000150 RID: 336
		private readonly string scope;
	}
}
