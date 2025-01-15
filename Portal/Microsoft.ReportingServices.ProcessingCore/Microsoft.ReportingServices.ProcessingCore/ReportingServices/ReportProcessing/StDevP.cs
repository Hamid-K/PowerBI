using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000673 RID: 1651
	internal sealed class StDevP : VarP
	{
		// Token: 0x06005AF2 RID: 23282 RVA: 0x001760A4 File Offset: 0x001742A4
		internal override object Result()
		{
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
