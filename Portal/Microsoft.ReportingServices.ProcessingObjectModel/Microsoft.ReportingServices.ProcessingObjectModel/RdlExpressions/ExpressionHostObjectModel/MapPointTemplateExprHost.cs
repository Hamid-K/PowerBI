using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000DD RID: 221
	public abstract class MapPointTemplateExprHost : MapSpatialElementTemplateExprHost
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00003C48 File Offset: 0x00001E48
		public virtual object SizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00003C4B File Offset: 0x00001E4B
		public virtual object LabelPlacementExpr
		{
			get
			{
				return null;
			}
		}
	}
}
