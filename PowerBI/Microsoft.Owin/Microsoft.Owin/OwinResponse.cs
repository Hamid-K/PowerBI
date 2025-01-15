using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
	// Token: 0x02000015 RID: 21
	public class OwinResponse : IOwinResponse
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00002E38 File Offset: 0x00001038
		public OwinResponse()
		{
			IDictionary<string, object> environment = new Dictionary<string, object>(StringComparer.Ordinal);
			environment["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			environment["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			this.Environment = environment;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002E87 File Offset: 0x00001087
		public OwinResponse(IDictionary<string, object> environment)
		{
			if (environment == null)
			{
				throw new ArgumentNullException("environment");
			}
			this.Environment = environment;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002EA4 File Offset: 0x000010A4
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002EAC File Offset: 0x000010AC
		public virtual IDictionary<string, object> Environment { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002EB5 File Offset: 0x000010B5
		public virtual IOwinContext Context
		{
			get
			{
				return new OwinContext(this.Environment);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002EC2 File Offset: 0x000010C2
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002ED4 File Offset: 0x000010D4
		public virtual int StatusCode
		{
			get
			{
				return this.Get<int>("owin.ResponseStatusCode", 200);
			}
			set
			{
				this.Set<int>("owin.ResponseStatusCode", value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002EE3 File Offset: 0x000010E3
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00002EF0 File Offset: 0x000010F0
		public virtual string ReasonPhrase
		{
			get
			{
				return this.Get<string>("owin.ResponseReasonPhrase");
			}
			set
			{
				this.Set<string>("owin.ResponseReasonPhrase", value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002EFF File Offset: 0x000010FF
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00002F0C File Offset: 0x0000110C
		public virtual string Protocol
		{
			get
			{
				return this.Get<string>("owin.ResponseProtocol");
			}
			set
			{
				this.Set<string>("owin.ResponseProtocol", value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00002F1B File Offset: 0x0000111B
		public virtual IHeaderDictionary Headers
		{
			get
			{
				return new HeaderDictionary(this.RawHeaders);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002F28 File Offset: 0x00001128
		private IDictionary<string, string[]> RawHeaders
		{
			get
			{
				return this.Get<IDictionary<string, string[]>>("owin.ResponseHeaders");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002F35 File Offset: 0x00001135
		public virtual ResponseCookieCollection Cookies
		{
			get
			{
				return new ResponseCookieCollection(this.Headers);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002F44 File Offset: 0x00001144
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002F7C File Offset: 0x0000117C
		public virtual long? ContentLength
		{
			get
			{
				long value;
				if (long.TryParse(OwinHelpers.GetHeader(this.RawHeaders, "Content-Length"), out value))
				{
					return new long?(value);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					OwinHelpers.SetHeader(this.RawHeaders, "Content-Length", value.Value.ToString(CultureInfo.InvariantCulture));
					return;
				}
				this.RawHeaders.Remove("Content-Length");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002FC8 File Offset: 0x000011C8
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002FDA File Offset: 0x000011DA
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00003030 File Offset: 0x00001230
		public virtual DateTimeOffset? Expires
		{
			get
			{
				DateTimeOffset value;
				if (DateTimeOffset.TryParse(OwinHelpers.GetHeader(this.RawHeaders, "Expires"), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out value))
				{
					return new DateTimeOffset?(value);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					OwinHelpers.SetHeader(this.RawHeaders, "Expires", value.Value.ToString("r", CultureInfo.InvariantCulture));
					return;
				}
				this.RawHeaders.Remove("Expires");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003081 File Offset: 0x00001281
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00003093 File Offset: 0x00001293
		public virtual string ETag
		{
			get
			{
				return OwinHelpers.GetHeader(this.RawHeaders, "ETag");
			}
			set
			{
				OwinHelpers.SetHeader(this.RawHeaders, "ETag", value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000030A6 File Offset: 0x000012A6
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000030B3 File Offset: 0x000012B3
		public virtual Stream Body
		{
			get
			{
				return this.Get<Stream>("owin.ResponseBody");
			}
			set
			{
				this.Set<Stream>("owin.ResponseBody", value);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000030C4 File Offset: 0x000012C4
		public virtual void OnSendingHeaders(Action<object> callback, object state)
		{
			Action<Action<object>, object> onSendingHeaders = this.Get<Action<Action<object>, object>>("server.OnSendingHeaders");
			if (onSendingHeaders == null)
			{
				throw new NotSupportedException(Resources.Exception_MissingOnSendingHeaders);
			}
			onSendingHeaders(callback, state);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000030F3 File Offset: 0x000012F3
		public virtual void Redirect(string location)
		{
			this.StatusCode = 302;
			OwinHelpers.SetHeader(this.RawHeaders, "Location", location);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003111 File Offset: 0x00001311
		public virtual void Write(string text)
		{
			this.Write(Encoding.UTF8.GetBytes(text));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003124 File Offset: 0x00001324
		public virtual void Write(byte[] data)
		{
			this.Write(data, 0, (data == null) ? 0 : data.Length);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003137 File Offset: 0x00001337
		public virtual void Write(byte[] data, int offset, int count)
		{
			this.Body.Write(data, offset, count);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003147 File Offset: 0x00001347
		public virtual Task WriteAsync(string text)
		{
			return this.WriteAsync(text, CancellationToken.None);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003155 File Offset: 0x00001355
		public virtual Task WriteAsync(string text, CancellationToken token)
		{
			return this.WriteAsync(Encoding.UTF8.GetBytes(text), token);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003169 File Offset: 0x00001369
		public virtual Task WriteAsync(byte[] data)
		{
			return this.WriteAsync(data, CancellationToken.None);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003177 File Offset: 0x00001377
		public virtual Task WriteAsync(byte[] data, CancellationToken token)
		{
			return this.WriteAsync(data, 0, (data == null) ? 0 : data.Length, token);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000318B File Offset: 0x0000138B
		public virtual Task WriteAsync(byte[] data, int offset, int count, CancellationToken token)
		{
			return this.Body.WriteAsync(data, offset, count, token);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000031A0 File Offset: 0x000013A0
		public virtual T Get<T>(string key)
		{
			return this.Get<T>(key, default(T));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000031C0 File Offset: 0x000013C0
		private T Get<T>(string key, T fallback)
		{
			object value;
			if (!this.Environment.TryGetValue(key, out value))
			{
				return fallback;
			}
			return (T)((object)value);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000031E5 File Offset: 0x000013E5
		public virtual IOwinResponse Set<T>(string key, T value)
		{
			this.Environment[key] = value;
			return this;
		}
	}
}
