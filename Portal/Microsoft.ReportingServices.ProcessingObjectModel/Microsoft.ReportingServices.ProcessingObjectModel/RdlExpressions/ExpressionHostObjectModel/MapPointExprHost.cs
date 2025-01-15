using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000E1 RID: 225
	public abstract class MapPointExprHost : MapSpatialElementExprHost
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00003C77 File Offset: 0x00001E77
		public virtual object UseCustomPointTemplateExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000156 RID: 342
		public MapPointTemplateExprHost MapPointTemplateHost;
	}
}
