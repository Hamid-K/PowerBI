using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000084 RID: 132
	internal sealed class ExpressionTranslationException : Exception
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x000175A8 File Offset: 0x000157A8
		internal ExpressionTranslationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000175B1 File Offset: 0x000157B1
		internal ExpressionTranslationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
