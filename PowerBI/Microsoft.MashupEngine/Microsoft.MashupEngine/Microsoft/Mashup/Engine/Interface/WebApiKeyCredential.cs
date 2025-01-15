using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200009D RID: 157
	public class WebApiKeyCredential : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00003C3F File Offset: 0x00001E3F
		public WebApiKeyCredential(string apiKeyValue)
		{
			this.apiKeyValue = apiKeyValue;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00003C4E File Offset: 0x00001E4E
		public string ApiKeyValue
		{
			get
			{
				return this.apiKeyValue;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003C56 File Offset: 0x00001E56
		public override int GetHashCode()
		{
			return this.ApiKeyValue.GetHashCode();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003C63 File Offset: 0x00001E63
		public override bool Equals(object other)
		{
			return this.Equals(other as WebApiKeyCredential);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00003C63 File Offset: 0x00001E63
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as WebApiKeyCredential);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00003C71 File Offset: 0x00001E71
		private bool Equals(WebApiKeyCredential other)
		{
			return other != null && this.ApiKeyValue.Equals(other.ApiKeyValue);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00003C89 File Offset: 0x00001E89
		public IEnumerable<string> GetCacheParts()
		{
			yield return this.ApiKeyValue;
			yield break;
		}

		// Token: 0x0400018E RID: 398
		private readonly string apiKeyValue;
	}
}
