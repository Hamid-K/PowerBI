using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000246 RID: 582
	internal sealed class LimitWithAppliedToDataShape
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0004D2FF File Offset: 0x0004B4FF
		internal LimitWithAppliedToDataShape(Limit limit, DataShape appliedTo)
		{
			this.m_limit = limit;
			this.m_appliedTo = appliedTo;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004D315 File Offset: 0x0004B515
		public Limit Limit
		{
			get
			{
				return this.m_limit;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0004D31D File Offset: 0x0004B51D
		public DataShape AppliesTo
		{
			get
			{
				return this.m_appliedTo;
			}
		}

		// Token: 0x040008C4 RID: 2244
		private readonly DataShape m_appliedTo;

		// Token: 0x040008C5 RID: 2245
		private readonly Limit m_limit;
	}
}
