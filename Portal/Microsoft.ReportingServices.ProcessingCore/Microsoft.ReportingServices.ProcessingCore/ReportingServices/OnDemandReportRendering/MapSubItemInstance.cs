using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000195 RID: 405
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSubItemInstance : BaseInstance
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x00045D0B File Offset: 0x00043F0B
		internal MapSubItemInstance(MapSubItem defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00045D28 File Offset: 0x00043F28
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.MapDef.ReportScope, this.m_defObject.MapDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00045D74 File Offset: 0x00043F74
		public ReportSize LeftMargin
		{
			get
			{
				if (this.m_leftMargin == null)
				{
					this.m_leftMargin = new ReportSize(this.m_defObject.MapSubItemDef.EvaluateLeftMargin(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_leftMargin;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00045DC8 File Offset: 0x00043FC8
		public ReportSize RightMargin
		{
			get
			{
				if (this.m_rightMargin == null)
				{
					this.m_rightMargin = new ReportSize(this.m_defObject.MapSubItemDef.EvaluateRightMargin(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_rightMargin;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00045E1C File Offset: 0x0004401C
		public ReportSize TopMargin
		{
			get
			{
				if (this.m_topMargin == null)
				{
					this.m_topMargin = new ReportSize(this.m_defObject.MapSubItemDef.EvaluateTopMargin(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_topMargin;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00045E70 File Offset: 0x00044070
		public ReportSize BottomMargin
		{
			get
			{
				if (this.m_bottomMargin == null)
				{
					this.m_bottomMargin = new ReportSize(this.m_defObject.MapSubItemDef.EvaluateBottomMargin(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_bottomMargin;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00045EC4 File Offset: 0x000440C4
		public int ZIndex
		{
			get
			{
				if (this.m_zIndex == null)
				{
					this.m_zIndex = new int?(this.m_defObject.MapSubItemDef.EvaluateZIndex(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_zIndex.Value;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00045F1F File Offset: 0x0004411F
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_leftMargin = null;
			this.m_rightMargin = null;
			this.m_topMargin = null;
			this.m_bottomMargin = null;
			this.m_zIndex = null;
		}

		// Token: 0x040007B1 RID: 1969
		private MapSubItem m_defObject;

		// Token: 0x040007B2 RID: 1970
		private StyleInstance m_style;

		// Token: 0x040007B3 RID: 1971
		private ReportSize m_leftMargin;

		// Token: 0x040007B4 RID: 1972
		private ReportSize m_rightMargin;

		// Token: 0x040007B5 RID: 1973
		private ReportSize m_topMargin;

		// Token: 0x040007B6 RID: 1974
		private ReportSize m_bottomMargin;

		// Token: 0x040007B7 RID: 1975
		private int? m_zIndex;
	}
}
