using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000BE RID: 190
	public abstract class MapBindingFieldPairExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00003A40 File Offset: 0x00001C40
		public virtual object FieldNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00003A43 File Offset: 0x00001C43
		public virtual object BindingExpressionExpr
		{
			get
			{
				return null;
			}
		}
	}
}
