using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200066D RID: 1645
	internal sealed class Count : DataAggregate
	{
		// Token: 0x06005ADF RID: 23263 RVA: 0x00175CD0 File Offset: 0x00173ED0
		internal override void Init()
		{
			this.m_currentTotal = 0;
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x00175CD9 File Offset: 0x00173ED9
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			if (DataAggregate.IsNull(DataAggregate.GetTypeCode(expressions[0])))
			{
				return;
			}
			this.m_currentTotal++;
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x00175D17 File Offset: 0x00173F17
		internal override object Result()
		{
			return this.m_currentTotal;
		}

		// Token: 0x04002F3C RID: 12092
		private int m_currentTotal;
	}
}
