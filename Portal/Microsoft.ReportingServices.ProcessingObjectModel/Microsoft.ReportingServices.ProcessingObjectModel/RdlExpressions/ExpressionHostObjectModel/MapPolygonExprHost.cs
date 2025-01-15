using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000DF RID: 223
	public abstract class MapPolygonExprHost : MapSpatialElementExprHost
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00003C61 File Offset: 0x00001E61
		public virtual object UseCustomPolygonTemplateExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00003C64 File Offset: 0x00001E64
		public virtual object UseCustomPointTemplateExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000154 RID: 340
		public MapPolygonTemplateExprHost MapPolygonTemplateHost;

		// Token: 0x04000155 RID: 341
		public MapPointTemplateExprHost MapPointTemplateHost;
	}
}
