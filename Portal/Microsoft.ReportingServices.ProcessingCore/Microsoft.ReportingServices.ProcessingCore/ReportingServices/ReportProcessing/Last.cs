using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000668 RID: 1640
	internal sealed class Last : DataAggregate
	{
		// Token: 0x06005ACB RID: 23243 RVA: 0x00175891 File Offset: 0x00173A91
		internal override void Init()
		{
			this.m_value = null;
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x0017589A File Offset: 0x00173A9A
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			this.m_value = expressions[0];
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x001758C3 File Offset: 0x00173AC3
		internal override object Result()
		{
			return this.m_value;
		}

		// Token: 0x04002F2F RID: 12079
		private object m_value;
	}
}
