using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000671 RID: 1649
	internal sealed class StDev : Var
	{
		// Token: 0x06005AEE RID: 23278 RVA: 0x00175F34 File Offset: 0x00174134
		internal override object Result()
		{
			if (1U == this.m_currentCount)
			{
				return null;
			}
			DataAggregate.DataTypeCode sumOfXType = this.m_sumOfXType;
			if (sumOfXType == DataAggregate.DataTypeCode.Null)
			{
				return null;
			}
			if (sumOfXType == DataAggregate.DataTypeCode.Double)
			{
				return Math.Sqrt((double)base.Result());
			}
			if (sumOfXType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return Math.Sqrt(Convert.ToDouble((decimal)base.Result()));
		}
	}
}
