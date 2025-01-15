using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F57 RID: 3927
	internal abstract class AdobeAnalyticsCubeObject
	{
		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x060067CA RID: 26570 RVA: 0x00165394 File Offset: 0x00163594
		// (set) Token: 0x060067CB RID: 26571 RVA: 0x0016539C File Offset: 0x0016359C
		public string Id { get; protected set; }

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x060067CC RID: 26572 RVA: 0x001653A5 File Offset: 0x001635A5
		// (set) Token: 0x060067CD RID: 26573 RVA: 0x001653AD File Offset: 0x001635AD
		public string Name { get; protected set; }

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x060067CE RID: 26574
		public abstract AdobeAnalyticsCubeObjectKind Kind { get; }

		// Token: 0x060067CF RID: 26575 RVA: 0x001653B6 File Offset: 0x001635B6
		protected AdobeAnalyticsCubeObject(string name, string id)
		{
			this.Name = name;
			this.Id = id;
		}
	}
}
