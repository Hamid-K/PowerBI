using System;
using System.Net;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000095 RID: 149
	internal sealed class HttpEnvironmentProxyCredentials : ICredentials
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000EA95 File Offset: 0x0000CC95
		public HttpEnvironmentProxyCredentials(Uri httpProxy, NetworkCredential httpCred, Uri httpsProxy, NetworkCredential httpsCred)
		{
			this._httpCred = httpCred;
			this._httpsCred = httpsCred;
			this._httpProxy = httpProxy;
			this._httpsProxy = httpsProxy;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000EABA File Offset: 0x0000CCBA
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			if (uri == null)
			{
				return null;
			}
			if (uri.Equals(this._httpProxy))
			{
				return this._httpCred;
			}
			if (!uri.Equals(this._httpsProxy))
			{
				return null;
			}
			return this._httpsCred;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public static HttpEnvironmentProxyCredentials TryCreate(Uri httpProxy, Uri httpsProxy)
		{
			NetworkCredential networkCredential = null;
			NetworkCredential networkCredential2 = null;
			if (httpProxy != null)
			{
				networkCredential = HttpEnvironmentProxyCredentials.GetCredentialsFromString(httpProxy.UserInfo);
			}
			if (httpsProxy != null)
			{
				networkCredential2 = HttpEnvironmentProxyCredentials.GetCredentialsFromString(httpsProxy.UserInfo);
			}
			if (networkCredential == null && networkCredential2 == null)
			{
				return null;
			}
			return new HttpEnvironmentProxyCredentials(httpProxy, networkCredential, httpsProxy, networkCredential2);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000EB40 File Offset: 0x0000CD40
		private static NetworkCredential GetCredentialsFromString(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			value = Uri.UnescapeDataString(value);
			string text = "";
			string text2 = null;
			int num = value.IndexOfOrdinal(':');
			if (num != -1)
			{
				text = value.Substring(num + 1);
				value = value.Substring(0, num);
			}
			num = value.IndexOfOrdinal('\\');
			if (num != -1)
			{
				text2 = value.Substring(0, num);
				value = value.Substring(num + 1);
			}
			return new NetworkCredential(value, text, text2);
		}

		// Token: 0x040001F3 RID: 499
		private readonly NetworkCredential _httpCred;

		// Token: 0x040001F4 RID: 500
		private readonly NetworkCredential _httpsCred;

		// Token: 0x040001F5 RID: 501
		private readonly Uri _httpProxy;

		// Token: 0x040001F6 RID: 502
		private readonly Uri _httpsProxy;
	}
}
