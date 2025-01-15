using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200066F RID: 1647
	internal sealed class CountRows : DataAggregate
	{
		// Token: 0x06005AE7 RID: 23271 RVA: 0x00175DDA File Offset: 0x00173FDA
		internal override void Init()
		{
			this.m_currentTotal = 0;
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x00175DE3 File Offset: 0x00173FE3
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			this.m_currentTotal++;
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x00175DF3 File Offset: 0x00173FF3
		internal override object Result()
		{
			return this.m_currentTotal;
		}

		// Token: 0x04002F3E RID: 12094
		private int m_currentTotal;
	}
}
