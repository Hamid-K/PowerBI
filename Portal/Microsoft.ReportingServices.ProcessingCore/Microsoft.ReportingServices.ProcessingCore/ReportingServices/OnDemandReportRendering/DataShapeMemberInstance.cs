using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200009A RID: 154
	internal class DataShapeMemberInstance : BaseInstance
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0002714C File Offset: 0x0002534C
		internal DataShapeMemberInstance(DataShape ownerDataShape, DataShapeMember dataShapeMember)
			: base(dataShapeMember.ReportScope)
		{
			this.m_ownerDataShape = ownerDataShape;
			this.m_dataShapeMember = dataShapeMember;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00027168 File Offset: 0x00025368
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000268 RID: 616
		protected DataShape m_ownerDataShape;

		// Token: 0x04000269 RID: 617
		protected DataShapeMember m_dataShapeMember;
	}
}
