using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000196 RID: 406
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapDockableSubItemInstance : MapSubItemInstance
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x00045F5C File Offset: 0x0004415C
		internal MapDockableSubItemInstance(MapDockableSubItem defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00045F6C File Offset: 0x0004416C
		public MapPosition Position
		{
			get
			{
				if (this.m_position == null)
				{
					this.m_position = new MapPosition?(((MapDockableSubItem)this.m_defObject.MapSubItemDef).EvaluatePosition(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00045FCC File Offset: 0x000441CC
		public bool DockOutsideViewport
		{
			get
			{
				if (this.m_dockOutsideViewport == null)
				{
					this.m_dockOutsideViewport = new bool?(((MapDockableSubItem)this.m_defObject.MapSubItemDef).EvaluateDockOutsideViewport(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_dockOutsideViewport.Value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0004602C File Offset: 0x0004422C
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(((MapDockableSubItem)this.m_defObject.MapSubItemDef).EvaluateHidden(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0004608C File Offset: 0x0004428C
		public string ToolTip
		{
			get
			{
				if (!this.m_toolTipEvaluated)
				{
					this.m_toolTip = ((MapDockableSubItem)this.m_defObject.MapSubItemDef).EvaluateToolTip(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_toolTipEvaluated = true;
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000460E4 File Offset: 0x000442E4
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_position = null;
			this.m_dockOutsideViewport = null;
			this.m_hidden = null;
			this.m_toolTip = null;
			this.m_toolTipEvaluated = false;
		}

		// Token: 0x040007B8 RID: 1976
		private MapDockableSubItem m_defObject;

		// Token: 0x040007B9 RID: 1977
		private MapPosition? m_position;

		// Token: 0x040007BA RID: 1978
		private bool? m_dockOutsideViewport;

		// Token: 0x040007BB RID: 1979
		private bool? m_hidden;

		// Token: 0x040007BC RID: 1980
		private string m_toolTip;

		// Token: 0x040007BD RID: 1981
		private bool m_toolTipEvaluated;
	}
}
