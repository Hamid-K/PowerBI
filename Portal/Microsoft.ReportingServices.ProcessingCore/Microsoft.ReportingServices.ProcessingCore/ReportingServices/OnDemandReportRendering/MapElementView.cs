using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F3 RID: 499
	public sealed class MapElementView : MapView
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0004C759 File Offset: 0x0004A959
		internal MapElementView(MapElementView defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0004C763 File Offset: 0x0004A963
		public ReportStringProperty LayerName
		{
			get
			{
				if (this.m_layerName == null && this.MapElementViewDef.LayerName != null)
				{
					this.m_layerName = new ReportStringProperty(this.MapElementViewDef.LayerName);
				}
				return this.m_layerName;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004C796 File Offset: 0x0004A996
		public MapBindingFieldPairCollection MapBindingFieldPairs
		{
			get
			{
				if (this.m_mapBindingFieldPairs == null && this.MapElementViewDef.MapBindingFieldPairs != null)
				{
					this.m_mapBindingFieldPairs = new MapBindingFieldPairCollection(this.MapElementViewDef.MapBindingFieldPairs, this.m_map);
				}
				return this.m_mapBindingFieldPairs;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004C7CF File Offset: 0x0004A9CF
		internal MapElementView MapElementViewDef
		{
			get
			{
				return (MapElementView)base.MapViewDef;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		public new MapElementViewInstance Instance
		{
			get
			{
				return (MapElementViewInstance)this.GetInstance();
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0004C7E9 File Offset: 0x0004A9E9
		internal override MapViewInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapElementViewInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004C819 File Offset: 0x0004AA19
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				this.m_mapBindingFieldPairs.SetNewContext();
			}
		}

		// Token: 0x04000905 RID: 2309
		private ReportStringProperty m_layerName;

		// Token: 0x04000906 RID: 2310
		private MapBindingFieldPairCollection m_mapBindingFieldPairs;
	}
}
