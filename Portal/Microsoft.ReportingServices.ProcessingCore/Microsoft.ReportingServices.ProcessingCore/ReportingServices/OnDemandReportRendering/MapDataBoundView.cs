using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F5 RID: 501
	public sealed class MapDataBoundView : MapView
	{
		// Token: 0x060012DA RID: 4826 RVA: 0x0004C8BD File Offset: 0x0004AABD
		internal MapDataBoundView(MapDataBoundView defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004C8C7 File Offset: 0x0004AAC7
		internal MapDataBoundView MapDataBoundViewDef
		{
			get
			{
				return (MapDataBoundView)base.MapViewDef;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0004C8D4 File Offset: 0x0004AAD4
		public new MapDataBoundViewInstance Instance
		{
			get
			{
				return (MapDataBoundViewInstance)this.GetInstance();
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004C8E1 File Offset: 0x0004AAE1
		internal override MapViewInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapDataBoundViewInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004C911 File Offset: 0x0004AB11
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}
