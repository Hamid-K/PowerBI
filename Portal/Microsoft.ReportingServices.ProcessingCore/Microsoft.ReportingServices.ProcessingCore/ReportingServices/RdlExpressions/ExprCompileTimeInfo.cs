using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000560 RID: 1376
	internal sealed class ExprCompileTimeInfo
	{
		// Token: 0x06004A36 RID: 18998 RVA: 0x00139730 File Offset: 0x00137930
		public ExprCompileTimeInfo(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			this.ExpressionInfo = expression;
			this.OwnerObjectType = context.ObjectType;
			this.OwnerObjectName = context.ObjectName;
			this.OwnerPropertyName = context.PropertyName;
			this.NumErrors = 0;
			this.NumWarnings = 0;
		}

		// Token: 0x04002855 RID: 10325
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ExpressionInfo;

		// Token: 0x04002856 RID: 10326
		internal ObjectType OwnerObjectType;

		// Token: 0x04002857 RID: 10327
		internal string OwnerObjectName;

		// Token: 0x04002858 RID: 10328
		internal string OwnerPropertyName;

		// Token: 0x04002859 RID: 10329
		internal int NumErrors;

		// Token: 0x0400285A RID: 10330
		internal int NumWarnings;
	}
}
