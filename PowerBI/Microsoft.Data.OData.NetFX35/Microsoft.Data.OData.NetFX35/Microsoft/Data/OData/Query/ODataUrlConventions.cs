using System;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000051 RID: 81
	public sealed class ODataUrlConventions
	{
		// Token: 0x0600021F RID: 543 RVA: 0x000081EC File Offset: 0x000063EC
		private ODataUrlConventions(UrlConvention urlConvention)
		{
			this.urlConvention = urlConvention;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000081FB File Offset: 0x000063FB
		public static ODataUrlConventions Default
		{
			get
			{
				return ODataUrlConventions.DefaultInstance;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008202 File Offset: 0x00006402
		public static ODataUrlConventions KeyAsSegment
		{
			get
			{
				return ODataUrlConventions.KeyAsSegmentInstance;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008209 File Offset: 0x00006409
		internal UrlConvention UrlConvention
		{
			get
			{
				return this.urlConvention;
			}
		}

		// Token: 0x04000084 RID: 132
		private static readonly ODataUrlConventions DefaultInstance = new ODataUrlConventions(UrlConvention.CreateWithExplicitValue(false));

		// Token: 0x04000085 RID: 133
		private static readonly ODataUrlConventions KeyAsSegmentInstance = new ODataUrlConventions(UrlConvention.CreateWithExplicitValue(true));

		// Token: 0x04000086 RID: 134
		private readonly UrlConvention urlConvention;
	}
}
