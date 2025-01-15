using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Owin.Security;

namespace Microsoft.Owin
{
	// Token: 0x02000012 RID: 18
	public class OwinContext : IOwinContext
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000027D0 File Offset: 0x000009D0
		public OwinContext()
		{
			IDictionary<string, object> environment = new Dictionary<string, object>(StringComparer.Ordinal);
			environment["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			environment["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			this.Environment = environment;
			this.Request = new OwinRequest(environment);
			this.Response = new OwinResponse(environment);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002837 File Offset: 0x00000A37
		public OwinContext(IDictionary<string, object> environment)
		{
			if (environment == null)
			{
				throw new ArgumentNullException("environment");
			}
			this.Environment = environment;
			this.Request = new OwinRequest(environment);
			this.Response = new OwinResponse(environment);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002874 File Offset: 0x00000A74
		public virtual IOwinRequest Request { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002885 File Offset: 0x00000A85
		public virtual IOwinResponse Response { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000288E File Offset: 0x00000A8E
		public IAuthenticationManager Authentication
		{
			get
			{
				return new AuthenticationManager(this);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002896 File Offset: 0x00000A96
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000289E File Offset: 0x00000A9E
		public virtual IDictionary<string, object> Environment { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000028A7 File Offset: 0x00000AA7
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000028B4 File Offset: 0x00000AB4
		public virtual TextWriter TraceOutput
		{
			get
			{
				return this.Get<TextWriter>("host.TraceOutput");
			}
			set
			{
				this.Set<TextWriter>("host.TraceOutput", value);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000028C4 File Offset: 0x00000AC4
		public virtual T Get<T>(string key)
		{
			object value;
			if (!this.Environment.TryGetValue(key, out value))
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000028F1 File Offset: 0x00000AF1
		public virtual IOwinContext Set<T>(string key, T value)
		{
			this.Environment[key] = value;
			return this;
		}
	}
}
