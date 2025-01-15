using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000DE RID: 222
	public abstract class MapLineExprHost : MapSpatialElementExprHost
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00003C56 File Offset: 0x00001E56
		public virtual object UseCustomLineTemplateExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000153 RID: 339
		public MapLineTemplateExprHost MapLineTemplateHost;
	}
}
