using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
	// Token: 0x02000014 RID: 20
	public class OwinRequest : IOwinRequest
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00002928 File Offset: 0x00000B28
		public OwinRequest()
		{
			IDictionary<string, object> environment = new Dictionary<string, object>(StringComparer.Ordinal);
			environment["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			environment["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			this.Environment = environment;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002977 File Offset: 0x00000B77
		public OwinRequest(IDictionary<string, object> environment)
		{
			if (environment == null)
			{
				throw new ArgumentNullException("environment");
			}
			this.Environment = environment;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002994 File Offset: 0x00000B94
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000299C File Offset: 0x00000B9C
		public virtual IDictionary<string, object> Environment { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000029A5 File Offset: 0x00000BA5
		public virtual IOwinContext Context
		{
			get
			{
				return new OwinContext(this.Environment);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000029B2 File Offset: 0x00000BB2
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000029BF File Offset: 0x00000BBF
		public virtual string Method
		{
			get
			{
				return this.Get<string>("owin.RequestMethod");
			}
			set
			{
				this.Set<string>("owin.RequestMethod", value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000029CE File Offset: 0x00000BCE
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000029DB File Offset: 0x00000BDB
		public virtual string Scheme
		{
			get
			{
				return this.Get<string>("owin.RequestScheme");
			}
			set
			{
				this.Set<string>("owin.RequestScheme", value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000029EA File Offset: 0x00000BEA
		public virtual bool IsSecure
		{
			get
			{
				return string.Equals(this.Scheme, "HTTPS", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000029FD File Offset: 0x00000BFD
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002A0A File Offset: 0x00000C0A
		public virtual HostString Host
		{
			get
			{
				return new HostString(OwinHelpers.GetHost(this));
			}
			set
			{
				OwinHelpers.SetHeader(this.RawHeaders, "Host", value.Value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002A23 File Offset: 0x00000C23
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00002A35 File Offset: 0x00000C35
		public virtual PathString PathBase
		{
			get
			{
				return new PathString(this.Get<string>("owin.RequestPathBase"));
			}
			set
			{
				this.Set<string>("owin.RequestPathBase", value.Value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002A4A File Offset: 0x00000C4A
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00002A5C File Offset: 0x00000C5C
		public virtual PathString Path
		{
			get
			{
				return new PathString(this.Get<string>("owin.RequestPath"));
			}
			set
			{
				this.Set<string>("owin.RequestPath", value.Value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002A71 File Offset: 0x00000C71
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002A83 File Offset: 0x00000C83
		public virtual QueryString QueryString
		{
			get
			{
				return new QueryString(this.Get<string>("owin.RequestQueryString"));
			}
			set
			{
				this.Set<string>("owin.RequestQueryString", value.Value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002A98 File Offset: 0x00000C98
		public virtual IReadableStringCollection Query
		{
			get
			{
				return new ReadableStringCollection(OwinHelpers.GetQuery(this));
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public virtual Uri Uri
		{
			get
			{
				return new Uri(string.Concat(new string[]
				{
					this.Scheme,
					Uri.SchemeDelimiter,
					this.Host.ToString(),
					this.PathBase.ToString(),
					this.Path.ToString(),
					this.QueryString.ToString()
				}));
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002B32 File Offset: 0x00000D32
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002B3F File Offset: 0x00000D3F
		public virtual string Protocol
		{
			get
			{
				return this.Get<string>("owin.RequestProtocol");
			}
			set
			{
				this.Set<string>("owin.RequestProtocol", value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002B4E File Offset: 0x00000D4E
		public virtual IHeaderDictionary Headers
		{
			get
			{
				return new HeaderDictionary(this.RawHeaders);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002B5B File Offset: 0x00000D5B
		private IDictionary<string, string[]> RawHeaders
		{
			get
			{
				return this.Get<IDictionary<string, string[]>>("owin.RequestHeaders");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002B68 File Offset: 0x00000D68
		public RequestCookieCollection Cookies
		{
			get
			{
				return new RequestCookieCollection(OwinHelpers.GetCookies(this));
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002B75 File Offset: 0x00000D75
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002B87 File Offset: 0x00000D87
		public virtual string ContentType
		{
			get
			{
				return OwinHelpers.GetHeader(this.RawHeaders, "Content-Type");
			}
			set
			{
				OwinHelpers.SetHeader(this.RawHeaders, "Content-Type", value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002B9A File Offset: 0x00000D9A
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00002BAC File Offset: 0x00000DAC
		public virtual string CacheControl
		{
			get
			{
				return OwinHelpers.GetHeader(this.RawHeaders, "Cache-Control");
			}
			set
			{
				OwinHelpers.SetHeader(this.RawHeaders, "Cache-Control", value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002BBF File Offset: 0x00000DBF
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002BD1 File Offset: 0x00000DD1
		public virtual string MediaType
		{
			get
			{
				return OwinHelpers.GetHeader(this.RawHeaders, "Media-Type");
			}
			set
			{
				OwinHelpers.SetHeader(this.RawHeaders, "Media-Type", value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002BE4 File Offset: 0x00000DE4
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public virtual string Accept
		{
			get
			{
				return OwinHelpers.GetHeader(this.RawHeaders, "Accept");
			}
			set
			{
				OwinHelpers.SetHeader(this.RawHeaders, "Accept", value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002C09 File Offset: 0x00000E09
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002C16 File Offset: 0x00000E16
		public virtual Stream Body
		{
			get
			{
				return this.Get<Stream>("owin.RequestBody");
			}
			set
			{
				this.Set<Stream>("owin.RequestBody", value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002C32 File Offset: 0x00000E32
		public virtual CancellationToken CallCancelled
		{
			get
			{
				return this.Get<CancellationToken>("owin.CallCancelled");
			}
			set
			{
				this.Set<CancellationToken>("owin.CallCancelled", value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002C41 File Offset: 0x00000E41
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002C4E File Offset: 0x00000E4E
		public virtual string LocalIpAddress
		{
			get
			{
				return this.Get<string>("server.LocalIpAddress");
			}
			set
			{
				this.Set<string>("server.LocalIpAddress", value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002C60 File Offset: 0x00000E60
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002C8C File Offset: 0x00000E8C
		public virtual int? LocalPort
		{
			get
			{
				int value;
				if (int.TryParse(this.LocalPortString, out value))
				{
					return new int?(value);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.LocalPortString = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				this.Environment.Remove("server.LocalPort");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002CCE File Offset: 0x00000ECE
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002CDB File Offset: 0x00000EDB
		private string LocalPortString
		{
			get
			{
				return this.Get<string>("server.LocalPort");
			}
			set
			{
				this.Set<string>("server.LocalPort", value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002CEA File Offset: 0x00000EEA
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public virtual string RemoteIpAddress
		{
			get
			{
				return this.Get<string>("server.RemoteIpAddress");
			}
			set
			{
				this.Set<string>("server.RemoteIpAddress", value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002D08 File Offset: 0x00000F08
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002D34 File Offset: 0x00000F34
		public virtual int? RemotePort
		{
			get
			{
				int value;
				if (int.TryParse(this.RemotePortString, out value))
				{
					return new int?(value);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.RemotePortString = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				this.Environment.Remove("server.RemotePort");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002D76 File Offset: 0x00000F76
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002D83 File Offset: 0x00000F83
		private string RemotePortString
		{
			get
			{
				return this.Get<string>("server.RemotePort");
			}
			set
			{
				this.Set<string>("server.RemotePort", value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002D92 File Offset: 0x00000F92
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00002D9F File Offset: 0x00000F9F
		public virtual IPrincipal User
		{
			get
			{
				return this.Get<IPrincipal>("server.User");
			}
			set
			{
				this.Set<IPrincipal>("server.User", value);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public async Task<IFormCollection> ReadFormAsync()
		{
			IFormCollection form = this.Get<IFormCollection>("Microsoft.Owin.Form#collection");
			if (form == null)
			{
				string text;
				using (StreamReader reader = new StreamReader(this.Body, Encoding.UTF8, true, 4096, true))
				{
					string text2 = await reader.ReadToEndAsync();
					text = text2;
				}
				StreamReader reader = null;
				form = OwinHelpers.GetForm(text);
				this.Set<IFormCollection>("Microsoft.Owin.Form#collection", form);
			}
			return form;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public virtual T Get<T>(string key)
		{
			object value;
			if (!this.Environment.TryGetValue(key, out value))
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002E21 File Offset: 0x00001021
		public virtual IOwinRequest Set<T>(string key, T value)
		{
			this.Environment[key] = value;
			return this;
		}
	}
}
