using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000117 RID: 279
	public sealed class CompiledGaugeInputValueInstance
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x0003576D File Offset: 0x0003396D
		internal CompiledGaugeInputValueInstance(object value)
		{
			this.m_value = value;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0003577C File Offset: 0x0003397C
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000558 RID: 1368
		private object m_value;
	}
}
