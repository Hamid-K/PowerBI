using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001B RID: 27
	internal static class RetriableHostExceptionExtensions
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000058B3 File Offset: 0x00003AB3
		internal static bool IsRetriable(this Exception ex)
		{
			while (!(ex is IRetriableHostException))
			{
				ex = ex.InnerException;
				if (ex == null)
				{
					return false;
				}
			}
			return true;
		}
	}
}
