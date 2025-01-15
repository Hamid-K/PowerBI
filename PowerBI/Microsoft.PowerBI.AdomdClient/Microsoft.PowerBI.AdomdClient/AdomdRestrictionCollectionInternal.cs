using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000068 RID: 104
	internal sealed class AdomdRestrictionCollectionInternal : XmlaPropertyCollectionBase
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00022DEE File Offset: 0x00020FEE
		internal AdomdRestrictionCollectionInternal(AdomdRestrictionCollection parentCollection)
		{
			this.parentCollection = parentCollection;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00022DFD File Offset: 0x00020FFD
		protected override Type ItemType
		{
			get
			{
				return typeof(AdomdRestriction);
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00022E09 File Offset: 0x00021009
		protected override IXmlaProperty CreateBasePropertyObject(IXmlaPropertyKey key, object propertyValue)
		{
			return new AdomdRestriction(key.Name, key.Namespace, propertyValue);
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00022E1D File Offset: 0x0002101D
		protected override object Parent
		{
			get
			{
				return this.parentCollection;
			}
		}

		// Token: 0x04000455 RID: 1109
		private AdomdRestrictionCollection parentCollection;
	}
}
