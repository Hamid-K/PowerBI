using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x0200002B RID: 43
	public class DataServiceClientRequestMessageArgs
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00007F94 File Offset: 0x00006194
		public DataServiceClientRequestMessageArgs(string method, Uri requestUri, bool useDefaultCredentials, bool usePostTunneling, IDictionary<string, string> headers)
		{
			this.Headers = headers;
			this.Method = method;
			this.RequestUri = requestUri;
			this.UsePostTunneling = usePostTunneling;
			this.UseDefaultCredentials = useDefaultCredentials;
			this.actualMethod = this.Method;
			if (this.UsePostTunneling && this.Headers.ContainsKey("X-HTTP-Method"))
			{
				this.actualMethod = "POST";
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007FFD File Offset: 0x000061FD
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00008005 File Offset: 0x00006205
		public string Method { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000800E File Offset: 0x0000620E
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00008016 File Offset: 0x00006216
		public Uri RequestUri { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000801F File Offset: 0x0000621F
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00008027 File Offset: 0x00006227
		public bool UsePostTunneling { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008030 File Offset: 0x00006230
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00008038 File Offset: 0x00006238
		public IDictionary<string, string> Headers { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008041 File Offset: 0x00006241
		public string ActualMethod
		{
			get
			{
				return this.actualMethod;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00008049 File Offset: 0x00006249
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00008051 File Offset: 0x00006251
		public bool UseDefaultCredentials { get; private set; }

		// Token: 0x04000079 RID: 121
		private readonly string actualMethod;
	}
}
