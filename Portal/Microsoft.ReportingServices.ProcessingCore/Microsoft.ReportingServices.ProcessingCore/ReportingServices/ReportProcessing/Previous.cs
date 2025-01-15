using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000676 RID: 1654
	internal sealed class Previous : DataAggregate
	{
		// Token: 0x06005AFA RID: 23290 RVA: 0x00176232 File Offset: 0x00174432
		internal override void Init()
		{
			this.m_value = null;
			this.m_previous = null;
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x00176242 File Offset: 0x00174442
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			this.m_previous = this.m_value;
			this.m_value = expressions[0];
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x00176277 File Offset: 0x00174477
		internal override object Result()
		{
			return this.m_previous;
		}

		// Token: 0x04002F45 RID: 12101
		private object m_value;

		// Token: 0x04002F46 RID: 12102
		private object m_previous;
	}
}
