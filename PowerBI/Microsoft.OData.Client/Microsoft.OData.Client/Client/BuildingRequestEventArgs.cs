using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000024 RID: 36
	public class BuildingRequestEventArgs : EventArgs
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00006E5F File Offset: 0x0000505F
		internal BuildingRequestEventArgs(string method, Uri requestUri, HeaderCollection headers, Descriptor descriptor, HttpStack httpStack)
		{
			this.Method = method;
			this.RequestUri = requestUri;
			this.HeaderCollection = headers ?? new HeaderCollection();
			this.ClientHttpStack = httpStack;
			this.Descriptor = descriptor;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006E95 File Offset: 0x00005095
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006E9D File Offset: 0x0000509D
		public string Method { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006EA6 File Offset: 0x000050A6
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006EAE File Offset: 0x000050AE
		public Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
			set
			{
				this.requestUri = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006EB7 File Offset: 0x000050B7
		public IDictionary<string, string> Headers
		{
			get
			{
				return this.HeaderCollection.UnderlyingDictionary;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006EC4 File Offset: 0x000050C4
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006ECC File Offset: 0x000050CC
		public Descriptor Descriptor { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006ED5 File Offset: 0x000050D5
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006EDD File Offset: 0x000050DD
		internal HttpStack ClientHttpStack { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006EE6 File Offset: 0x000050E6
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006EEE File Offset: 0x000050EE
		internal HeaderCollection HeaderCollection { get; private set; }

		// Token: 0x0600012C RID: 300 RVA: 0x00006EF7 File Offset: 0x000050F7
		internal BuildingRequestEventArgs Clone()
		{
			return new BuildingRequestEventArgs(this.Method, this.RequestUri, this.HeaderCollection, this.Descriptor, this.ClientHttpStack);
		}

		// Token: 0x0400005E RID: 94
		private Uri requestUri;
	}
}
