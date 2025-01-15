using System;

namespace Microsoft.OData
{
	// Token: 0x02000022 RID: 34
	public sealed class ODataSimplifiedOptions
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004650 File Offset: 0x00002850
		public ODataSimplifiedOptions()
		{
			this.EnableParsingKeyAsSegmentUrl = true;
			this.EnableReadingKeyAsSegment = false;
			this.EnableReadingODataAnnotationWithoutPrefix = false;
			this.EnableWritingKeyAsSegment = false;
			this.EnableWritingODataAnnotationWithoutPrefix = false;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000467B File Offset: 0x0000287B
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004683 File Offset: 0x00002883
		public bool EnableParsingKeyAsSegmentUrl { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000468C File Offset: 0x0000288C
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004694 File Offset: 0x00002894
		public bool EnableReadingKeyAsSegment { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000469D File Offset: 0x0000289D
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000046A5 File Offset: 0x000028A5
		public bool EnableReadingODataAnnotationWithoutPrefix { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000046AE File Offset: 0x000028AE
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000046B6 File Offset: 0x000028B6
		public bool EnableWritingKeyAsSegment { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000046BF File Offset: 0x000028BF
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000046C7 File Offset: 0x000028C7
		public bool EnableWritingODataAnnotationWithoutPrefix { get; set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x000046D0 File Offset: 0x000028D0
		public ODataSimplifiedOptions Clone()
		{
			ODataSimplifiedOptions odataSimplifiedOptions = new ODataSimplifiedOptions();
			odataSimplifiedOptions.CopyFrom(this);
			return odataSimplifiedOptions;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000046EB File Offset: 0x000028EB
		internal static ODataSimplifiedOptions GetODataSimplifiedOptions(IServiceProvider container)
		{
			if (container == null)
			{
				return ODataSimplifiedOptions.DefaultOptions;
			}
			return container.GetRequiredService<ODataSimplifiedOptions>();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000046FC File Offset: 0x000028FC
		private void CopyFrom(ODataSimplifiedOptions other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSimplifiedOptions>(other, "other");
			this.EnableParsingKeyAsSegmentUrl = other.EnableParsingKeyAsSegmentUrl;
			this.EnableReadingKeyAsSegment = other.EnableReadingKeyAsSegment;
			this.EnableReadingODataAnnotationWithoutPrefix = other.EnableReadingODataAnnotationWithoutPrefix;
			this.EnableWritingKeyAsSegment = other.EnableWritingKeyAsSegment;
			this.EnableWritingODataAnnotationWithoutPrefix = other.EnableWritingODataAnnotationWithoutPrefix;
		}

		// Token: 0x0400008F RID: 143
		private static readonly ODataSimplifiedOptions DefaultOptions = new ODataSimplifiedOptions();
	}
}
