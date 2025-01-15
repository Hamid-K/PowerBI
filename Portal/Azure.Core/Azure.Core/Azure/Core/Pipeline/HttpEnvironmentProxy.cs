using System;
using System.Collections.Generic;
using System.Net;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000094 RID: 148
	internal sealed class HttpEnvironmentProxy : IWebProxy
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		public static bool TryCreate(out IWebProxy proxy)
		{
			Uri uri = HttpEnvironmentProxy.GetUriFromString(Environment.GetEnvironmentVariable("http_proxy"));
			if (uri == null && Environment.GetEnvironmentVariable("GATEWAY_INTERFACE") == null)
			{
				uri = HttpEnvironmentProxy.GetUriFromString(Environment.GetEnvironmentVariable("HTTP_PROXY"));
			}
			Uri uri2 = HttpEnvironmentProxy.GetUriFromString(Environment.GetEnvironmentVariable("https_proxy")) ?? HttpEnvironmentProxy.GetUriFromString(Environment.GetEnvironmentVariable("HTTPS_PROXY"));
			if (uri == null || uri2 == null)
			{
				Uri uri3 = HttpEnvironmentProxy.GetUriFromString(Environment.GetEnvironmentVariable("all_proxy")) ?? HttpEnvironmentProxy.GetUriFromString(Environment.GetEnvironmentVariable("ALL_PROXY"));
				if (uri == null)
				{
					uri = uri3;
				}
				if (uri2 == null)
				{
					uri2 = uri3;
				}
			}
			if (uri == null && uri2 == null)
			{
				proxy = null;
				return false;
			}
			string text = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
			proxy = new HttpEnvironmentProxy(uri, uri2, text);
			return true;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		private HttpEnvironmentProxy(Uri httpProxy, Uri httpsProxy, string bypassList)
		{
			this._httpProxyUri = httpProxy;
			this._httpsProxyUri = httpsProxy;
			this._credentials = HttpEnvironmentProxyCredentials.TryCreate(httpProxy, httpsProxy);
			if (!string.IsNullOrWhiteSpace(bypassList))
			{
				string[] array = bypassList.Split(new char[] { ',' });
				List<string> list = new List<string>(array.Length);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i].Trim();
					if (text.Length > 0)
					{
						list.Add(text);
					}
				}
				if (list.Count > 0)
				{
					this._bypass = list.ToArray();
				}
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000E860 File Offset: 0x0000CA60
		private static Uri GetUriFromString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
			{
				value = value.Substring(7);
			}
			string text = null;
			string text2 = null;
			ushort num = 80;
			int num2 = value.LastIndexOf('@');
			if (num2 != -1)
			{
				string text3 = value.Substring(0, num2);
				try
				{
					text3 = Uri.UnescapeDataString(text3);
				}
				catch
				{
				}
				value = value.Substring(num2 + 1);
				num2 = text3.IndexOfOrdinal(':');
				if (num2 == -1)
				{
					text = text3;
				}
				else
				{
					text = text3.Substring(0, num2);
					text2 = text3.Substring(num2 + 1);
				}
			}
			int num3 = value.IndexOfOrdinal(']');
			num2 = value.LastIndexOf(':');
			string text4;
			if (num2 == -1 || (num3 != -1 && num2 < num3))
			{
				text4 = value;
			}
			else
			{
				text4 = value.Substring(0, num2);
				int num4 = num2 + 1;
				while (num4 < value.Length && char.IsDigit(value[num4]))
				{
					num4++;
				}
				if (!ushort.TryParse(value.Substring(num2 + 1, num4 - num2 - 1), out num))
				{
					return null;
				}
			}
			try
			{
				UriBuilder uriBuilder = new UriBuilder("http", text4, (int)num);
				if (text != null)
				{
					uriBuilder.UserName = Uri.EscapeDataString(text);
				}
				if (text2 != null)
				{
					uriBuilder.Password = Uri.EscapeDataString(text2);
				}
				return uriBuilder.Uri;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		private bool IsMatchInBypassList(Uri input)
		{
			if (this._bypass != null)
			{
				foreach (string text in this._bypass)
				{
					if (text[0] == '.')
					{
						if (text.Length - 1 == input.Host.Length && string.Compare(text, 1, input.Host, 0, input.Host.Length, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						if (input.Host.EndsWith(text, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					else if (string.Equals(text, input.Host, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000EA4B File Offset: 0x0000CC4B
		public Uri GetProxy(Uri uri)
		{
			if (!(uri.Scheme == Uri.UriSchemeHttp))
			{
				return this._httpsProxyUri;
			}
			return this._httpProxyUri;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		public bool IsBypassed(Uri uri)
		{
			return this.GetProxy(uri) == null || this.IsMatchInBypassList(uri);
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000EA86 File Offset: 0x0000CC86
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000EA8E File Offset: 0x0000CC8E
		public ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040001E6 RID: 486
		private const string EnvAllProxyUC = "ALL_PROXY";

		// Token: 0x040001E7 RID: 487
		private const string EnvAllProxyLC = "all_proxy";

		// Token: 0x040001E8 RID: 488
		private const string EnvHttpProxyLC = "http_proxy";

		// Token: 0x040001E9 RID: 489
		private const string EnvHttpProxyUC = "HTTP_PROXY";

		// Token: 0x040001EA RID: 490
		private const string EnvHttpsProxyLC = "https_proxy";

		// Token: 0x040001EB RID: 491
		private const string EnvHttpsProxyUC = "HTTPS_PROXY";

		// Token: 0x040001EC RID: 492
		private const string EnvNoProxyLC = "no_proxy";

		// Token: 0x040001ED RID: 493
		private const string EnvNoProxyUC = "NO_PROXY";

		// Token: 0x040001EE RID: 494
		private const string EnvCGI = "GATEWAY_INTERFACE";

		// Token: 0x040001EF RID: 495
		private readonly Uri _httpProxyUri;

		// Token: 0x040001F0 RID: 496
		private readonly Uri _httpsProxyUri;

		// Token: 0x040001F1 RID: 497
		private readonly string[] _bypass;

		// Token: 0x040001F2 RID: 498
		private readonly ICredentials _credentials;
	}
}
