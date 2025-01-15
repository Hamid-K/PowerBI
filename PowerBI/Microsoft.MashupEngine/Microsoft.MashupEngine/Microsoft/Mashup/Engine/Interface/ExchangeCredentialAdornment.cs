using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000096 RID: 150
	public sealed class ExchangeCredentialAdornment : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000020FD File Offset: 0x000002FD
		public ExchangeCredentialAdornment()
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00003674 File Offset: 0x00001874
		public ExchangeCredentialAdornment(string ewsUrl, string ewsSupportedSchema, string emailAddress)
		{
			this.ewsUrl = ewsUrl;
			this.ewsSupportedSchema = ewsSupportedSchema;
			this.emailAddress = emailAddress;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00003691 File Offset: 0x00001891
		public string EwsUrl
		{
			get
			{
				return this.ewsUrl;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00003699 File Offset: 0x00001899
		public string EwsSupportedSchema
		{
			get
			{
				return this.ewsSupportedSchema;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000036A1 File Offset: 0x000018A1
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000036A9 File Offset: 0x000018A9
		public override int GetHashCode()
		{
			return this.EwsUrl.GetHashCode() ^ this.EwsSupportedSchema.GetHashCode() ^ this.EmailAddress.GetHashCode();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000036CE File Offset: 0x000018CE
		public override bool Equals(object other)
		{
			return this.Equals(other as ExchangeCredentialAdornment);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000036CE File Offset: 0x000018CE
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as ExchangeCredentialAdornment);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000036DC File Offset: 0x000018DC
		private bool Equals(ExchangeCredentialAdornment other)
		{
			return other != null && this.ewsSupportedSchema == other.ewsSupportedSchema && this.ewsUrl == other.ewsUrl && this.emailAddress == other.emailAddress;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000371A File Offset: 0x0000191A
		public IEnumerable<string> GetCacheParts()
		{
			yield return this.ewsUrl;
			yield return this.ewsSupportedSchema;
			yield return this.emailAddress;
			yield break;
		}

		// Token: 0x0400017A RID: 378
		private readonly string ewsUrl;

		// Token: 0x0400017B RID: 379
		private readonly string ewsSupportedSchema;

		// Token: 0x0400017C RID: 380
		private readonly string emailAddress;
	}
}
