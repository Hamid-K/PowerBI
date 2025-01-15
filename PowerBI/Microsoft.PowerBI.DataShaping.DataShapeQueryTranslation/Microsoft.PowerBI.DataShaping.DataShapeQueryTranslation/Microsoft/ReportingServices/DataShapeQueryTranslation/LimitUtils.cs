using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200004B RID: 75
	internal static class LimitUtils
	{
		// Token: 0x06000310 RID: 784 RVA: 0x000091A8 File Offset: 0x000073A8
		internal static int GetPaddedCount(this TopLimitOperator limitOperator)
		{
			int num = limitOperator.Count.Value;
			if (!limitOperator.IsStrict.GetValueOrDefault<bool>())
			{
				num += 2;
			}
			return num;
		}
	}
}
