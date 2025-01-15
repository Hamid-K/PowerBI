using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000065 RID: 101
	internal sealed class AdomdPropertyCollectionInternal : XmlaPropertyCollectionBase
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x00022AE0 File Offset: 0x00020CE0
		internal AdomdPropertyCollectionInternal(AdomdPropertyCollection parentCollection)
		{
			this.parentCollection = parentCollection;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00022AEF File Offset: 0x00020CEF
		protected override Type ItemType
		{
			get
			{
				return typeof(AdomdProperty);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00022AFB File Offset: 0x00020CFB
		protected override IXmlaProperty CreateBasePropertyObject(IXmlaPropertyKey key, object propertyValue)
		{
			return new AdomdProperty(key.Name, key.Namespace, propertyValue);
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00022B0F File Offset: 0x00020D0F
		protected override object Parent
		{
			get
			{
				return this.parentCollection;
			}
		}

		// Token: 0x0400044F RID: 1103
		private AdomdPropertyCollection parentCollection;
	}
}
