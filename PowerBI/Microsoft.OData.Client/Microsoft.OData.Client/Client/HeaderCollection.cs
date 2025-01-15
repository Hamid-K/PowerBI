using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x02000062 RID: 98
	internal class HeaderCollection
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		internal HeaderCollection(IEnumerable<KeyValuePair<string, string>> headers)
			: this()
		{
			this.SetHeaders(headers);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000CD7B File Offset: 0x0000AF7B
		internal HeaderCollection(IODataResponseMessage responseMessage)
			: this()
		{
			if (responseMessage != null)
			{
				this.SetHeaders(responseMessage.Headers);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000CD94 File Offset: 0x0000AF94
		internal HeaderCollection(WebHeaderCollection headers)
			: this()
		{
			foreach (string text in headers.AllKeys)
			{
				this.SetHeader(text, headers[text]);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000CDCE File Offset: 0x0000AFCE
		internal HeaderCollection()
		{
			this.headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		internal IDictionary<string, string> UnderlyingDictionary
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000CDEE File Offset: 0x0000AFEE
		internal IEnumerable<string> HeaderNames
		{
			get
			{
				return this.headers.Keys;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000CDFB File Offset: 0x0000AFFB
		internal void SetDefaultHeaders()
		{
			this.SetUserAgent();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000CE03 File Offset: 0x0000B003
		internal bool TryGetHeader(string headerName, out string headerValue)
		{
			return this.headers.TryGetValue(headerName, out headerValue);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000CE14 File Offset: 0x0000B014
		internal string GetHeader(string headerName)
		{
			string text;
			if (!this.TryGetHeader(headerName, out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000CE2F File Offset: 0x0000B02F
		internal void SetHeader(string headerName, string headerValue)
		{
			if (headerValue == null)
			{
				this.headers.Remove(headerName);
				return;
			}
			this.headers[headerName] = headerValue;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000CE4F File Offset: 0x0000B04F
		internal void SetHeaders(IEnumerable<KeyValuePair<string, string>> headersToSet)
		{
			this.headers.SetRange(headersToSet);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000CE5D File Offset: 0x0000B05D
		internal IEnumerable<KeyValuePair<string, string>> AsEnumerable()
		{
			return this.headers.AsEnumerable<KeyValuePair<string, string>>();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000CE6C File Offset: 0x0000B06C
		internal void SetRequestVersion(Version requestVersion, Version maxProtocolVersion)
		{
			if (requestVersion != null)
			{
				if (requestVersion > maxProtocolVersion)
				{
					string text = Strings.Context_RequestVersionIsBiggerThanProtocolVersion(requestVersion.ToString(), maxProtocolVersion.ToString());
					throw Error.InvalidOperation(text);
				}
				if (requestVersion.Major > 0)
				{
					Version odataVersion = this.GetODataVersion();
					if (odataVersion == null || requestVersion > odataVersion)
					{
						this.SetHeader("OData-Version", requestVersion.ToString(2));
					}
				}
			}
			this.SetHeader("OData-MaxVersion", maxProtocolVersion.ToString(2));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000CEEA File Offset: 0x0000B0EA
		internal void SetHeaderIfUnset(string headerToSet, string headerValue)
		{
			if (this.GetHeader(headerToSet) == null)
			{
				this.SetHeader(headerToSet, headerValue);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000CF00 File Offset: 0x0000B100
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method and calls another instance method for non-silverlight scenearios.")]
		internal void SetUserAgent()
		{
			this.SetHeader("User-Agent", string.Format(CultureInfo.InvariantCulture, "Microsoft.OData.Client/{0}.{1}.{2}", new object[]
			{
				HeaderCollection.assemblyVersion.Major,
				HeaderCollection.assemblyVersion.Minor,
				HeaderCollection.assemblyVersion.Build
			}));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000CF63 File Offset: 0x0000B163
		internal HeaderCollection Copy()
		{
			return new HeaderCollection(this.headers);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000CF70 File Offset: 0x0000B170
		private Version GetODataVersion()
		{
			string text;
			if (!this.TryGetHeader("OData-Version", out text))
			{
				return null;
			}
			return Version.Parse(text);
		}

		// Token: 0x0400010F RID: 271
		private readonly IDictionary<string, string> headers;

		// Token: 0x04000110 RID: 272
		private static Version assemblyVersion = typeof(HeaderCollection).GetAssembly().GetName().Version;
	}
}
