using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200021E RID: 542
	public abstract class ChartObjectCollectionItem<T> where T : BaseInstance
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x00053A7D File Offset: 0x00051C7D
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040009AA RID: 2474
		protected T m_instance;
	}
}
