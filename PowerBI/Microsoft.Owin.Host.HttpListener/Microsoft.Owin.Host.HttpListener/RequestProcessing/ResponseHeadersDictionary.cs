using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000016 RID: 22
	internal sealed class ResponseHeadersDictionary : HeadersDictionaryBase
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000066BD File Offset: 0x000048BD
		internal ResponseHeadersDictionary(HttpListenerResponse response)
		{
			this._response = response;
			this.Headers = this._response.Headers;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000066DD File Offset: 0x000048DD
		private bool HasContentLength
		{
			get
			{
				return this._response.ContentLength64 != 0L;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000066F0 File Offset: 0x000048F0
		private string[] ContentLength
		{
			get
			{
				return new string[] { this._response.ContentLength64.ToString(CultureInfo.InvariantCulture) };
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000671E File Offset: 0x0000491E
		public override ICollection<string> Keys
		{
			get
			{
				if (this.HasContentLength)
				{
					return base.Keys.Concat(new string[] { "Content-Length" }).ToList<string>();
				}
				return base.Keys;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000674D File Offset: 0x0000494D
		public override bool TryGetValue(string header, out string[] value)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (header.Equals("Content-Length", StringComparison.OrdinalIgnoreCase) && this.HasContentLength)
			{
				value = this.ContentLength;
				return true;
			}
			return base.TryGetValue(header, out value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006785 File Offset: 0x00004985
		protected override string[] Get(string header)
		{
			if (header.Equals("Content-Length", StringComparison.OrdinalIgnoreCase) && this.HasContentLength)
			{
				return this.ContentLength;
			}
			return base.Get(header);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000067AC File Offset: 0x000049AC
		protected override void Set(string header, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (header.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				this._response.ContentLength64 = long.Parse(value, CultureInfo.InvariantCulture);
				return;
			}
			if (header.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase) && value.Equals("chunked", StringComparison.OrdinalIgnoreCase))
			{
				this._response.SendChunked = true;
				return;
			}
			if (header.Equals("Connection", StringComparison.OrdinalIgnoreCase) && value.Equals("close", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = false;
				return;
			}
			if (header.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase) && value.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = true;
				return;
			}
			if (header.Equals("WWW-Authenticate", StringComparison.OrdinalIgnoreCase))
			{
				this._response.AddHeader("WWW-Authenticate", value);
				return;
			}
			base.Set(header, value);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006890 File Offset: 0x00004A90
		protected override void Append(string header, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (header.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				this._response.ContentLength64 = long.Parse(value, CultureInfo.InvariantCulture);
				return;
			}
			if (header.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase) && value.Equals("chunked", StringComparison.OrdinalIgnoreCase))
			{
				this._response.SendChunked = true;
				return;
			}
			if (header.Equals("Connection", StringComparison.OrdinalIgnoreCase) && value.Equals("close", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = false;
				return;
			}
			if (header.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase) && value.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = true;
				return;
			}
			if (!header.Equals("WWW-Authenticate", StringComparison.OrdinalIgnoreCase))
			{
				base.Append(header, value);
				return;
			}
			if (base.ContainsKey("WWW-Authenticate"))
			{
				string[] wwwAuthValues = this.Get("WWW-Authenticate");
				string[] newHeader = new string[wwwAuthValues.Length + 1];
				wwwAuthValues.CopyTo(newHeader, 0);
				newHeader[newHeader.Length - 1] = value;
				this._response.AddHeader("WWW-Authenticate", string.Join(", ", newHeader));
				return;
			}
			this._response.AddHeader("WWW-Authenticate", value);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000069C4 File Offset: 0x00004BC4
		public override bool Remove(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (!base.ContainsKey(header))
			{
				return false;
			}
			if (header.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				this._response.ContentLength64 = 0L;
			}
			else if (header.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
			{
				this._response.SendChunked = false;
			}
			else if (header.Equals("Connection", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = true;
			}
			else if (header.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = false;
			}
			else
			{
				if (!header.Equals("WWW-Authenticate", StringComparison.OrdinalIgnoreCase))
				{
					return base.Remove(header);
				}
				this._response.AddHeader("WWW-Authenticate", string.Empty);
			}
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006A8C File Offset: 0x00004C8C
		protected override void RemoveSilent(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (header.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				this._response.ContentLength64 = 0L;
				return;
			}
			if (header.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
			{
				this._response.SendChunked = false;
				return;
			}
			if (header.Equals("Connection", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = true;
				return;
			}
			if (header.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase))
			{
				this._response.KeepAlive = false;
				return;
			}
			if (header.Equals("WWW-Authenticate", StringComparison.OrdinalIgnoreCase))
			{
				this._response.AddHeader("WWW-Authenticate", string.Empty);
				return;
			}
			base.RemoveSilent(header);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006B3F File Offset: 0x00004D3F
		public override IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
		{
			if (this.HasContentLength)
			{
				yield return new KeyValuePair<string, string[]>("Content-Length", this.ContentLength);
			}
			int num;
			for (int i = 0; i < this.Headers.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, string[]>(this.Headers.GetKey(i), this.Headers.GetValues(i));
				num = i;
			}
			yield break;
		}

		// Token: 0x0400009F RID: 159
		private readonly HttpListenerResponse _response;
	}
}
