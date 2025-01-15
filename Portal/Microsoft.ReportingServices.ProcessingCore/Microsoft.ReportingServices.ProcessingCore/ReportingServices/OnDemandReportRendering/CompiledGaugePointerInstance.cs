using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200011D RID: 285
	public sealed class CompiledGaugePointerInstance
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000365C3 File Offset: 0x000347C3
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x000365CB File Offset: 0x000347CB
		public CompiledGaugeInputValueInstance GaugeInputValue
		{
			get
			{
				return this.m_gaugeInputValue;
			}
			internal set
			{
				this.m_gaugeInputValue = value;
			}
		}

		// Token: 0x04000585 RID: 1413
		private CompiledGaugeInputValueInstance m_gaugeInputValue;
	}
}
