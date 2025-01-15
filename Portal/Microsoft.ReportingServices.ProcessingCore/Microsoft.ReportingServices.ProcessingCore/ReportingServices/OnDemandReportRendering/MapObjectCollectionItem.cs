using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000178 RID: 376
	public abstract class MapObjectCollectionItem : IMapObjectCollectionItem
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0004413F File Offset: 0x0004233F
		void IMapObjectCollectionItem.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00044147 File Offset: 0x00042347
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400074D RID: 1869
		protected BaseInstance m_instance;
	}
}
