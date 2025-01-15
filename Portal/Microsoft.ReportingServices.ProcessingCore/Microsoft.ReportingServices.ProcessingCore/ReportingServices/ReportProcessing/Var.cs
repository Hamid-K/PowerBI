using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000672 RID: 1650
	internal class Var : VarBase
	{
		// Token: 0x06005AF0 RID: 23280 RVA: 0x00175FB4 File Offset: 0x001741B4
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
				return (this.m_currentCount * (double)this.m_sumOfXSquared - (double)this.m_sumOfX * (double)this.m_sumOfX) / (this.m_currentCount * (this.m_currentCount - 1U));
			}
			if (sumOfXType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return (this.m_currentCount * (decimal)this.m_sumOfXSquared - (decimal)this.m_sumOfX * (decimal)this.m_sumOfX) / (this.m_currentCount * (this.m_currentCount - 1U));
		}
	}
}
