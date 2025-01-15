using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200072C RID: 1836
	internal class ODataCacheSettings
	{
		// Token: 0x0600369A RID: 13978 RVA: 0x000AE12C File Offset: 0x000AC32C
		public ODataCacheSettings()
		{
			this.OnCacheMissed = delegate
			{
			};
			this.credentialsCacheKeys = new Dictionary<ResourceCredentialCollection, string>();
			this.headersCacheKeys = new Dictionary<IValueReference, string>();
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000AE17A File Offset: 0x000AC37A
		// (set) Token: 0x0600369C RID: 13980 RVA: 0x000AE182 File Offset: 0x000AC382
		public Action OnCacheMissed { get; set; }

		// Token: 0x0600369D RID: 13981 RVA: 0x000AE18C File Offset: 0x000AC38C
		public string GetCacheKey(ResourceCredentialCollection credentials, Value headers, Uri uri, string other = null)
		{
			string credentialsKey = this.GetCredentialsKey(credentials);
			string headersKey = this.GetHeadersKey(headers);
			string absoluteUri = uri.AbsoluteUri;
			if (other != null)
			{
				return PersistentCacheKey.ODataFeed.Qualify(credentialsKey, headersKey, absoluteUri, other);
			}
			return PersistentCacheKey.ODataFeed.Qualify(credentialsKey, headersKey, absoluteUri);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000AE1D8 File Offset: 0x000AC3D8
		private string GetHeadersKey(Value headers)
		{
			if (headers.IsNull)
			{
				return string.Empty;
			}
			string text;
			if (!this.headersCacheKeys.TryGetValue(headers, out text))
			{
				text = headers.AsRecord.CreateCacheKey();
				this.headersCacheKeys.Add(headers, text);
			}
			return text;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000AE220 File Offset: 0x000AC420
		private string GetCredentialsKey(ResourceCredentialCollection credentials)
		{
			string hash;
			if (!this.credentialsCacheKeys.TryGetValue(credentials, out hash))
			{
				hash = credentials.GetHash();
				this.credentialsCacheKeys.Add(credentials, hash);
			}
			return hash;
		}

		// Token: 0x04001C0B RID: 7179
		private readonly Dictionary<ResourceCredentialCollection, string> credentialsCacheKeys;

		// Token: 0x04001C0C RID: 7180
		private readonly Dictionary<IValueReference, string> headersCacheKeys;
	}
}
