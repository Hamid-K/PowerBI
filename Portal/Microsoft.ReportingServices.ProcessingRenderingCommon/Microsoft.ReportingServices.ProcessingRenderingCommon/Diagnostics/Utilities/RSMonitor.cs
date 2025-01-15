using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AD RID: 173
	internal sealed class RSMonitor : ICounterProvider
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00010C4D File Offset: 0x0000EE4D
		public RSMonitor(string instanceName, RSTrace tracer)
		{
			this.m_Tracer = tracer;
			this.m_InstanceName = instanceName;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00010C63 File Offset: 0x0000EE63
		public RSCounter GetCounter(string categoryName, string counterName)
		{
			return new RSCounter(categoryName, counterName, this.m_InstanceName, this.m_Tracer);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00010C78 File Offset: 0x0000EE78
		public RSCounter GetCounter(string categoryName, string counterName, bool resetCounter)
		{
			return new RSCounter(categoryName, counterName, this.m_InstanceName, this.m_Tracer, false, resetCounter);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00010C8F File Offset: 0x0000EE8F
		public RSCounter GetPrivateCounter(string categoryName, string counterName)
		{
			return new RSCounter(categoryName, counterName, this.m_InstanceName, this.m_Tracer, true, false);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00010CA6 File Offset: 0x0000EEA6
		public RSCounter GetPrivateCounter(string categoryName, string counterName, bool resetCounter)
		{
			return new RSCounter(categoryName, counterName, this.m_InstanceName, this.m_Tracer, true, resetCounter);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00010CBD File Offset: 0x0000EEBD
		public ICounter GetCounterNumberOfItems(string categoryName, string counterName, bool resetCounter)
		{
			return new RSCounter(categoryName, counterName, this.m_InstanceName, this.m_Tracer, false, resetCounter);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00010CD4 File Offset: 0x0000EED4
		public ICounter GetCounterRatePerSecond(string categoryName, string counterNameTotal, string counterNamePerSecond, bool resetCounter)
		{
			RSCounter rscounter = new RSCounter(categoryName, counterNameTotal, this.m_InstanceName, RSTrace.CatalogTrace, false, resetCounter);
			return new TotalPerSecondCounter(new RSCounter(categoryName, counterNamePerSecond, this.m_InstanceName, RSTrace.CatalogTrace, false, resetCounter), rscounter);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00010D14 File Offset: 0x0000EF14
		public ICounter GetCounterAverageCount(string categoryName, string counterNameAverage, string counterNameBase, bool resetCounter)
		{
			RSCounter rscounter = new RSCounter(categoryName, counterNameAverage, this.m_InstanceName, RSTrace.CatalogTrace, false, resetCounter);
			RSCounter rscounter2 = new RSCounter(categoryName, counterNameBase, this.m_InstanceName, RSTrace.CatalogTrace, false, resetCounter);
			return new AverageValueCounter(rscounter, rscounter2);
		}

		// Token: 0x0400031F RID: 799
		private string m_InstanceName;

		// Token: 0x04000320 RID: 800
		private RSTrace m_Tracer;
	}
}
