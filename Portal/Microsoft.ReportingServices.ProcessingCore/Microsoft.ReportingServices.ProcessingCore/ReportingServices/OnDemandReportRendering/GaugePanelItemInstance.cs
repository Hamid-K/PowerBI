using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000115 RID: 277
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class GaugePanelItemInstance : BaseInstance
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00035153 File Offset: 0x00033353
		internal GaugePanelItemInstance(GaugePanelItem defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00035168 File Offset: 0x00033368
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.GaugePanelDef, this.m_defObject.GaugePanelDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000351A4 File Offset: 0x000333A4
		public double Top
		{
			get
			{
				if (this.m_top == null)
				{
					this.m_top = new double?(this.m_defObject.GaugePanelItemDef.EvaluateTop(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_top.Value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00035200 File Offset: 0x00033400
		public double Left
		{
			get
			{
				if (this.m_left == null)
				{
					this.m_left = new double?(this.m_defObject.GaugePanelItemDef.EvaluateLeft(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_left.Value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0003525C File Offset: 0x0003345C
		public double Height
		{
			get
			{
				if (this.m_height == null)
				{
					this.m_height = new double?(this.m_defObject.GaugePanelItemDef.EvaluateHeight(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_height.Value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x000352B8 File Offset: 0x000334B8
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.GaugePanelItemDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00035314 File Offset: 0x00033514
		public int ZIndex
		{
			get
			{
				if (this.m_zIndex == null)
				{
					this.m_zIndex = new int?(this.m_defObject.GaugePanelItemDef.EvaluateZIndex(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_zIndex.Value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00035370 File Offset: 0x00033570
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.GaugePanelItemDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000353CC File Offset: 0x000335CC
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_defObject.GaugePanelItemDef.EvaluateToolTip(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00035418 File Offset: 0x00033618
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_top = null;
			this.m_left = null;
			this.m_height = null;
			this.m_width = null;
			this.m_zIndex = null;
			this.m_hidden = null;
			this.m_toolTip = null;
		}

		// Token: 0x04000548 RID: 1352
		private GaugePanelItem m_defObject;

		// Token: 0x04000549 RID: 1353
		private StyleInstance m_style;

		// Token: 0x0400054A RID: 1354
		private double? m_top;

		// Token: 0x0400054B RID: 1355
		private double? m_left;

		// Token: 0x0400054C RID: 1356
		private double? m_height;

		// Token: 0x0400054D RID: 1357
		private double? m_width;

		// Token: 0x0400054E RID: 1358
		private int? m_zIndex;

		// Token: 0x0400054F RID: 1359
		private bool? m_hidden;

		// Token: 0x04000550 RID: 1360
		private string m_toolTip;
	}
}
