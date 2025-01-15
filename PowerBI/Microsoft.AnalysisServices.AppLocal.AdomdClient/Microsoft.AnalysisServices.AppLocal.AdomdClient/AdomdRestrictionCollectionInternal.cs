using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000068 RID: 104
	internal sealed class AdomdRestrictionCollectionInternal : XmlaPropertyCollectionBase
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0002311E File Offset: 0x0002131E
		internal AdomdRestrictionCollectionInternal(AdomdRestrictionCollection parentCollection)
		{
			this.parentCollection = parentCollection;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0002312D File Offset: 0x0002132D
		protected override Type ItemType
		{
			get
			{
				return typeof(AdomdRestriction);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00023139 File Offset: 0x00021339
		protected override IXmlaProperty CreateBasePropertyObject(IXmlaPropertyKey key, object propertyValue)
		{
			return new AdomdRestriction(key.Name, key.Namespace, propertyValue);
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0002314D File Offset: 0x0002134D
		protected override object Parent
		{
			get
			{
				return this.parentCollection;
			}
		}

		// Token: 0x04000462 RID: 1122
		private AdomdRestrictionCollection parentCollection;
	}
}
