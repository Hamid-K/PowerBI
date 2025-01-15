using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200000D RID: 13
	internal readonly struct ExpressionEvaluationDetails
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002422 File Offset: 0x00000622
		public bool TypeAlignmentInvalidated
		{
			get
			{
				return this._typeAlignmentInvalidated;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000242A File Offset: 0x0000062A
		public ExpressionEvaluationDetails(bool typeAlignmentInvalidated)
		{
			this._typeAlignmentInvalidated = typeAlignmentInvalidated;
		}

		// Token: 0x04000009 RID: 9
		private readonly bool _typeAlignmentInvalidated;
	}
}
