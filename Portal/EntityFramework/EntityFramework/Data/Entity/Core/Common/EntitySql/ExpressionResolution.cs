using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200064D RID: 1613
	internal abstract class ExpressionResolution
	{
		// Token: 0x06004DCF RID: 19919 RVA: 0x00117EC0 File Offset: 0x001160C0
		protected ExpressionResolution(ExpressionResolutionClass @class)
		{
			this.ExpressionClass = @class;
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06004DD0 RID: 19920
		internal abstract string ExpressionClassName { get; }

		// Token: 0x04001C26 RID: 7206
		internal readonly ExpressionResolutionClass ExpressionClass;
	}
}
