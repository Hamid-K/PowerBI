using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000CC RID: 204
	public abstract class MapBucketExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00003B59 File Offset: 0x00001D59
		public virtual object StartValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00003B5C File Offset: 0x00001D5C
		public virtual object EndValueExpr
		{
			get
			{
				return null;
			}
		}
	}
}
