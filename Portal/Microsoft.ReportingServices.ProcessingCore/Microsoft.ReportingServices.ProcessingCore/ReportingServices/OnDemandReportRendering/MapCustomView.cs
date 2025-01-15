using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F2 RID: 498
	public sealed class MapCustomView : MapView
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0004C684 File Offset: 0x0004A884
		internal MapCustomView(MapCustomView defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0004C68E File Offset: 0x0004A88E
		public ReportDoubleProperty CenterX
		{
			get
			{
				if (this.m_centerX == null && this.MapCustomViewDef.CenterX != null)
				{
					this.m_centerX = new ReportDoubleProperty(this.MapCustomViewDef.CenterX);
				}
				return this.m_centerX;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004C6C1 File Offset: 0x0004A8C1
		public ReportDoubleProperty CenterY
		{
			get
			{
				if (this.m_centerY == null && this.MapCustomViewDef.CenterY != null)
				{
					this.m_centerY = new ReportDoubleProperty(this.MapCustomViewDef.CenterY);
				}
				return this.m_centerY;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0004C6F4 File Offset: 0x0004A8F4
		internal MapCustomView MapCustomViewDef
		{
			get
			{
				return (MapCustomView)base.MapViewDef;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004C701 File Offset: 0x0004A901
		public new MapCustomViewInstance Instance
		{
			get
			{
				return (MapCustomViewInstance)this.GetInstance();
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0004C70E File Offset: 0x0004A90E
		internal override MapViewInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapCustomViewInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0004C73E File Offset: 0x0004A93E
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000903 RID: 2307
		private ReportDoubleProperty m_centerX;

		// Token: 0x04000904 RID: 2308
		private ReportDoubleProperty m_centerY;
	}
}
