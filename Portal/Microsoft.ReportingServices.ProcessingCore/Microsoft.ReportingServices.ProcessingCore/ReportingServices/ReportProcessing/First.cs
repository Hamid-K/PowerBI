using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000667 RID: 1639
	internal sealed class First : DataAggregate
	{
		// Token: 0x06005AC7 RID: 23239 RVA: 0x00175839 File Offset: 0x00173A39
		internal override void Init()
		{
			this.m_value = null;
			this.m_updated = false;
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x00175849 File Offset: 0x00173A49
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			if (!this.m_updated)
			{
				this.m_value = expressions[0];
				this.m_updated = true;
			}
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x00175881 File Offset: 0x00173A81
		internal override object Result()
		{
			return this.m_value;
		}

		// Token: 0x04002F2D RID: 12077
		private object m_value;

		// Token: 0x04002F2E RID: 12078
		private bool m_updated;
	}
}
