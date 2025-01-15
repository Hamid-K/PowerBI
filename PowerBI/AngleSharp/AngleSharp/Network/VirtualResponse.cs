using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AngleSharp.Extensions;

namespace AngleSharp.Network
{
	// Token: 0x0200009D RID: 157
	public class VirtualResponse : IResponse, IDisposable
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0001E797 File Offset: 0x0001C997
		private VirtualResponse()
		{
			this._address = Url.Create("http://localhost/");
			this._status = HttpStatusCode.OK;
			this._headers = new Dictionary<string, string>();
			this._content = Stream.Null;
			this._dispose = false;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		public static IResponse Create(Action<VirtualResponse> request)
		{
			VirtualResponse virtualResponse = new VirtualResponse();
			request(virtualResponse);
			return virtualResponse;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001E7F3 File Offset: 0x0001C9F3
		Url IResponse.Address
		{
			get
			{
				return this._address;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001E7FB File Offset: 0x0001C9FB
		Stream IResponse.Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001E803 File Offset: 0x0001CA03
		IDictionary<string, string> IResponse.Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001E80B File Offset: 0x0001CA0B
		HttpStatusCode IResponse.StatusCode
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001E813 File Offset: 0x0001CA13
		public VirtualResponse Address(Url url)
		{
			this._address = url;
			return this;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001E81D File Offset: 0x0001CA1D
		public VirtualResponse Address(string address)
		{
			return this.Address(Url.Create(address ?? string.Empty));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001E834 File Offset: 0x0001CA34
		public VirtualResponse Address(Uri url)
		{
			return this.Address(Url.Convert(url));
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001E842 File Offset: 0x0001CA42
		public VirtualResponse Status(HttpStatusCode code)
		{
			this._status = code;
			return this;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001E84C File Offset: 0x0001CA4C
		public VirtualResponse Status(int code)
		{
			return this.Status((HttpStatusCode)code);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001E855 File Offset: 0x0001CA55
		public VirtualResponse Header(string name, string value)
		{
			this._headers[name] = value;
			return this;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001E868 File Offset: 0x0001CA68
		public VirtualResponse Headers(object obj)
		{
			Dictionary<string, string> dictionary = obj.ToDictionary();
			return this.Headers(dictionary);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001E884 File Offset: 0x0001CA84
		public VirtualResponse Headers(IDictionary<string, string> headers)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				this.Header(keyValuePair.Key, keyValuePair.Value);
			}
			return this;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001E8DC File Offset: 0x0001CADC
		public VirtualResponse Content(string text)
		{
			this.Release();
			byte[] bytes = TextEncoding.Utf8.GetBytes(text);
			this._content = new MemoryStream(bytes);
			this._dispose = true;
			return this;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001E90F File Offset: 0x0001CB0F
		public VirtualResponse Content(Stream stream, bool shouldDispose = false)
		{
			this.Release();
			this._content = stream;
			this._dispose = shouldDispose;
			return this;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001E926 File Offset: 0x0001CB26
		private void Release()
		{
			if (this._dispose)
			{
				Stream content = this._content;
				if (content != null)
				{
					content.Dispose();
				}
			}
			this._dispose = false;
			this._content = null;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001E94F File Offset: 0x0001CB4F
		void IDisposable.Dispose()
		{
			this.Release();
		}

		// Token: 0x040003B8 RID: 952
		private Url _address;

		// Token: 0x040003B9 RID: 953
		private HttpStatusCode _status;

		// Token: 0x040003BA RID: 954
		private Dictionary<string, string> _headers;

		// Token: 0x040003BB RID: 955
		private Stream _content;

		// Token: 0x040003BC RID: 956
		private bool _dispose;
	}
}
