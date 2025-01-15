using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F4 RID: 500
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapView
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x0004C847 File Offset: 0x0004AA47
		internal MapView(MapView defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0004C85D File Offset: 0x0004AA5D
		public ReportDoubleProperty Zoom
		{
			get
			{
				if (this.m_zoom == null && this.m_defObject.Zoom != null)
				{
					this.m_zoom = new ReportDoubleProperty(this.m_defObject.Zoom);
				}
				return this.m_zoom;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004C890 File Offset: 0x0004AA90
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0004C898 File Offset: 0x0004AA98
		internal MapView MapViewDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0004C8A0 File Offset: 0x0004AAA0
		internal MapViewInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x060012D8 RID: 4824
		internal abstract MapViewInstance GetInstance();

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004C8A8 File Offset: 0x0004AAA8
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000907 RID: 2311
		protected Map m_map;

		// Token: 0x04000908 RID: 2312
		private MapView m_defObject;

		// Token: 0x04000909 RID: 2313
		protected MapViewInstance m_instance;

		// Token: 0x0400090A RID: 2314
		private ReportDoubleProperty m_zoom;
	}
}
