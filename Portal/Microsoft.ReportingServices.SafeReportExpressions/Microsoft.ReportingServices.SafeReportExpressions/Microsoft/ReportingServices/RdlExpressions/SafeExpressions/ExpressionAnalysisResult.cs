using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200000C RID: 12
	internal readonly struct ExpressionAnalysisResult
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002411 File Offset: 0x00000611
		public bool ContainsObjectReferences
		{
			get
			{
				return this._containsObjectReferences;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002419 File Offset: 0x00000619
		public ExpressionAnalysisResult(bool containsObjectReferences)
		{
			this._containsObjectReferences = containsObjectReferences;
		}

		// Token: 0x04000008 RID: 8
		private readonly bool _containsObjectReferences;
	}
}
