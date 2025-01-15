using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F58 RID: 3928
	internal class AdobeAnalyticsDimension : AdobeAnalyticsCubeObject
	{
		// Token: 0x060067D0 RID: 26576 RVA: 0x001653CC File Offset: 0x001635CC
		protected AdobeAnalyticsDimension(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x060067D1 RID: 26577 RVA: 0x00002139 File Offset: 0x00000339
		public override AdobeAnalyticsCubeObjectKind Kind
		{
			get
			{
				return AdobeAnalyticsCubeObjectKind.Dimension;
			}
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x001653D6 File Offset: 0x001635D6
		public static AdobeAnalyticsDimension New(string name, string id)
		{
			return new AdobeAnalyticsDimension(name, id);
		}
	}
}
