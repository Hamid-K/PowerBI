using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200066A RID: 1642
	internal sealed class Avg : Sum
	{
		// Token: 0x06005AD3 RID: 23251 RVA: 0x001759BE File Offset: 0x00173BBE
		internal override void Init()
		{
			base.Init();
			this.m_currentCount = 0U;
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x001759D0 File Offset: 0x00173BD0
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			if (DataAggregate.IsNull(DataAggregate.GetTypeCode(expressions[0])))
			{
				return;
			}
			base.Update(expressions, iErrorContext);
			this.m_currentCount += 1U;
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x00175A24 File Offset: 0x00173C24
		internal override object Result()
		{
			DataAggregate.DataTypeCode currentTotalType = this.m_currentTotalType;
			if (currentTotalType == DataAggregate.DataTypeCode.Null)
			{
				return null;
			}
			if (currentTotalType == DataAggregate.DataTypeCode.Double)
			{
				return (double)this.m_currentTotal / this.m_currentCount;
			}
			if (currentTotalType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return (decimal)this.m_currentTotal / this.m_currentCount;
		}

		// Token: 0x04002F33 RID: 12083
		private uint m_currentCount;
	}
}
