using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000C7 RID: 199
	public abstract class MapDistanceScaleExprHost : MapDockableSubItemExprHost
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00003AEA File Offset: 0x00001CEA
		public virtual object ScaleColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00003AED File Offset: 0x00001CED
		public virtual object ScaleBorderColorExpr
		{
			get
			{
				return null;
			}
		}
	}
}
