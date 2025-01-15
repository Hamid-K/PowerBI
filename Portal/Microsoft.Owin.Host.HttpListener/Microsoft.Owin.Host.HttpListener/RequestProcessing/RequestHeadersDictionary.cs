using System;
using System.Globalization;
using System.Net;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000015 RID: 21
	internal sealed class RequestHeadersDictionary : HeadersDictionaryBase
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000660E File Offset: 0x0000480E
		internal RequestHeadersDictionary(HttpListenerRequest request)
		{
			this._request = request;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000661D File Offset: 0x0000481D
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000662F File Offset: 0x0000482F
		protected override WebHeaderCollection Headers
		{
			get
			{
				return (WebHeaderCollection)this._request.Headers;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006638 File Offset: 0x00004838
		public override bool TryGetValue(string key, out string[] value)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				value = null;
				return false;
			}
			if (key.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				long contentLength = this._request.ContentLength64;
				if (contentLength >= 0L)
				{
					value = new string[] { contentLength.ToString(CultureInfo.InvariantCulture) };
					return true;
				}
			}
			else if (key.Equals("Host", StringComparison.OrdinalIgnoreCase))
			{
				string host = this._request.UserHostName;
				if (host != null)
				{
					value = new string[] { host };
					return true;
				}
			}
			return base.TryGetValue(key, out value);
		}

		// Token: 0x0400009E RID: 158
		private readonly HttpListenerRequest _request;
	}
}
