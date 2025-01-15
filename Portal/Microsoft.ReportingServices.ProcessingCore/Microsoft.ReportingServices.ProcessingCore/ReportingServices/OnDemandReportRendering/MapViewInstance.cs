using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001FC RID: 508
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapViewInstance : BaseInstance
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x0004D2F0 File Offset: 0x0004B4F0
		internal MapViewInstance(MapView defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0004D30C File Offset: 0x0004B50C
		public double Zoom
		{
			get
			{
				if (this.m_zoom == null)
				{
					this.m_zoom = new double?(this.m_defObject.MapViewDef.EvaluateZoom(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_zoom.Value;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004D368 File Offset: 0x0004B568
		protected override void ResetInstanceCache()
		{
			this.m_zoom = null;
		}

		// Token: 0x04000927 RID: 2343
		private MapView m_defObject;

		// Token: 0x04000928 RID: 2344
		private double? m_zoom;
	}
}
