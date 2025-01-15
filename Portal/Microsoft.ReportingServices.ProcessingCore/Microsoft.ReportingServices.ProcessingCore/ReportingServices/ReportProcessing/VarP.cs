using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000674 RID: 1652
	internal class VarP : VarBase
	{
		// Token: 0x06005AF4 RID: 23284 RVA: 0x00176118 File Offset: 0x00174318
		internal override object Result()
		{
			DataAggregate.DataTypeCode sumOfXType = this.m_sumOfXType;
			if (sumOfXType == DataAggregate.DataTypeCode.Null)
			{
				return null;
			}
			if (sumOfXType == DataAggregate.DataTypeCode.Double)
			{
				return (this.m_currentCount * (double)this.m_sumOfXSquared - (double)this.m_sumOfX * (double)this.m_sumOfX) / (this.m_currentCount * this.m_currentCount);
			}
			if (sumOfXType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return (this.m_currentCount * (decimal)this.m_sumOfXSquared - (decimal)this.m_sumOfX * (decimal)this.m_sumOfX) / (this.m_currentCount * this.m_currentCount);
		}
	}
}
