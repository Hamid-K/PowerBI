using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000675 RID: 1653
	internal class Aggregate : DataAggregate
	{
		// Token: 0x06005AF6 RID: 23286 RVA: 0x001761F7 File Offset: 0x001743F7
		internal override void Init()
		{
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x001761F9 File Offset: 0x001743F9
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(expressions != null);
			Global.Tracer.Assert(1 == expressions.Length);
			this.m_value = expressions[0];
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x00176222 File Offset: 0x00174422
		internal override object Result()
		{
			return this.m_value;
		}

		// Token: 0x04002F44 RID: 12100
		private object m_value;
	}
}
