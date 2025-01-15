using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000D4 RID: 212
	public abstract class MapMarkerExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00003BC3 File Offset: 0x00001DC3
		public virtual object MapMarkerStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400014B RID: 331
		public MapMarkerImageExprHost MapMarkerImageHost;
	}
}
