using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000045 RID: 69
	public abstract class DataValueExprHost : ReportObjectModelProxy
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002ABB File Offset: 0x00000CBB
		public virtual object DataValueNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00002ABE File Offset: 0x00000CBE
		public virtual object DataValueValueExpr
		{
			get
			{
				return null;
			}
		}
	}
}
