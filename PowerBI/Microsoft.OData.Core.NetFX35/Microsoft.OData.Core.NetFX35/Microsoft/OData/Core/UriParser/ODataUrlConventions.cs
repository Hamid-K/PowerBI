using System;
using Microsoft.OData.Core.Evaluation;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001F4 RID: 500
	public sealed class ODataUrlConventions
	{
		// Token: 0x0600125D RID: 4701 RVA: 0x000422A6 File Offset: 0x000404A6
		private ODataUrlConventions(UrlConvention urlConvention)
		{
			this.urlConvention = urlConvention;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000422B5 File Offset: 0x000404B5
		public static ODataUrlConventions Default
		{
			get
			{
				return ODataUrlConventions.DefaultInstance;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000422BC File Offset: 0x000404BC
		public static ODataUrlConventions KeyAsSegment
		{
			get
			{
				return ODataUrlConventions.KeyAsSegmentInstance;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x000422C3 File Offset: 0x000404C3
		public static ODataUrlConventions ODataSimplified
		{
			get
			{
				return ODataUrlConventions.ODataSimplifiedInstance;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x000422CA File Offset: 0x000404CA
		internal UrlConvention UrlConvention
		{
			get
			{
				return this.urlConvention;
			}
		}

		// Token: 0x040007E5 RID: 2021
		private static readonly ODataUrlConventions DefaultInstance = new ODataUrlConventions(UrlConvention.CreateWithExplicitValue(false));

		// Token: 0x040007E6 RID: 2022
		private static readonly ODataUrlConventions KeyAsSegmentInstance = new ODataUrlConventions(UrlConvention.CreateWithExplicitValue(true));

		// Token: 0x040007E7 RID: 2023
		private static readonly ODataUrlConventions ODataSimplifiedInstance = new ODataUrlConventions(UrlConvention.CreateODataSimplifiedConvention());

		// Token: 0x040007E8 RID: 2024
		private readonly UrlConvention urlConvention;
	}
}
