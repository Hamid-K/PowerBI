using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000029 RID: 41
	public sealed class TrustedResource
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006040 File Offset: 0x00004240
		public TrustedResource(string resourceValue, string[] urls)
		{
			if (resourceValue == null)
			{
				throw new ArgumentNullException("resourceValue");
			}
			if (urls == null)
			{
				throw new ArgumentNullException("urls");
			}
			this.resourceValue = resourceValue;
			this.urls = urls;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006072 File Offset: 0x00004272
		public string ResourceValue
		{
			get
			{
				return this.resourceValue;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000607A File Offset: 0x0000427A
		public string[] Urls
		{
			get
			{
				return this.urls;
			}
		}

		// Token: 0x04000100 RID: 256
		private readonly string resourceValue;

		// Token: 0x04000101 RID: 257
		private readonly string[] urls;
	}
}
