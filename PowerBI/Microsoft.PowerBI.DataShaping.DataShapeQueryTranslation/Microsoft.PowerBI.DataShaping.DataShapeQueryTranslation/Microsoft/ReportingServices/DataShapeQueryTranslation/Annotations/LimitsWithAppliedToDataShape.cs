using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000245 RID: 581
	internal sealed class LimitsWithAppliedToDataShape
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0004D26D File Offset: 0x0004B46D
		public int Count
		{
			get
			{
				if (this.m_limitsWithAppliedToDataShapes != null)
				{
					return this.m_limitsWithAppliedToDataShapes.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700036D RID: 877
		public LimitWithAppliedToDataShape this[int index]
		{
			get
			{
				return this.m_limitsWithAppliedToDataShapes[index];
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004D292 File Offset: 0x0004B492
		public void AddLimitAppliedTo(Limit limit, DataShape appliedToDataShape)
		{
			if (this.m_limitsWithAppliedToDataShapes == null)
			{
				this.m_limitsWithAppliedToDataShapes = new List<LimitWithAppliedToDataShape>(2);
			}
			this.m_limitsWithAppliedToDataShapes.Add(new LimitWithAppliedToDataShape(limit, appliedToDataShape));
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004D2BC File Offset: 0x0004B4BC
		public bool HasDeclaredLimitAppliedTo(DataShape appliedToDataShape)
		{
			return this.m_limitsWithAppliedToDataShapes != null && this.m_limitsWithAppliedToDataShapes.Any((LimitWithAppliedToDataShape l) => l.AppliesTo == appliedToDataShape);
		}

		// Token: 0x040008C3 RID: 2243
		private List<LimitWithAppliedToDataShape> m_limitsWithAppliedToDataShapes;
	}
}
