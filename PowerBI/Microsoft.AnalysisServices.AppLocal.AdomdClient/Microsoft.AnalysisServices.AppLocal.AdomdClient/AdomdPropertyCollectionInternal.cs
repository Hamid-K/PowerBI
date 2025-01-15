using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000065 RID: 101
	internal sealed class AdomdPropertyCollectionInternal : XmlaPropertyCollectionBase
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x00022E10 File Offset: 0x00021010
		internal AdomdPropertyCollectionInternal(AdomdPropertyCollection parentCollection)
		{
			this.parentCollection = parentCollection;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00022E1F File Offset: 0x0002101F
		protected override Type ItemType
		{
			get
			{
				return typeof(AdomdProperty);
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00022E2B File Offset: 0x0002102B
		protected override IXmlaProperty CreateBasePropertyObject(IXmlaPropertyKey key, object propertyValue)
		{
			return new AdomdProperty(key.Name, key.Namespace, propertyValue);
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00022E3F File Offset: 0x0002103F
		protected override object Parent
		{
			get
			{
				return this.parentCollection;
			}
		}

		// Token: 0x0400045C RID: 1116
		private AdomdPropertyCollection parentCollection;
	}
}
