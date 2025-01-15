using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200001E RID: 30
	internal sealed class NoArgumentValidator : FixedNumberArgumentValidator
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002FB8 File Offset: 0x000011B8
		public NoArgumentValidator(string functionName)
			: base(functionName, 0)
		{
		}
	}
}
