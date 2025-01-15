using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E7 RID: 487
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSpatialElementTemplateInstance : BaseInstance
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x0004B8F7 File Offset: 0x00049AF7
		internal MapSpatialElementTemplateInstance(MapSpatialElementTemplate defObject)
			: base(defObject.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004B90C File Offset: 0x00049B0C
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.ReportScope, this.m_defObject.MapDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0004B948 File Offset: 0x00049B48
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.MapSpatialElementTemplateDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0004B9A4 File Offset: 0x00049BA4
		public double OffsetX
		{
			get
			{
				if (this.m_offsetX == null)
				{
					this.m_offsetX = new double?(this.m_defObject.MapSpatialElementTemplateDef.EvaluateOffsetX(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_offsetX.Value;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004BA00 File Offset: 0x00049C00
		public double OffsetY
		{
			get
			{
				if (this.m_offsetY == null)
				{
					this.m_offsetY = new double?(this.m_defObject.MapSpatialElementTemplateDef.EvaluateOffsetY(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_offsetY.Value;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004BA5C File Offset: 0x00049C5C
		public string Label
		{
			get
			{
				if (!this.m_labelEvaluated)
				{
					this.m_label = this.m_defObject.MapSpatialElementTemplateDef.EvaluateLabel(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_labelEvaluated = true;
				}
				return this.m_label;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0004BAB0 File Offset: 0x00049CB0
		public string ToolTip
		{
			get
			{
				if (!this.m_toolTipEvaluated)
				{
					this.m_toolTip = this.m_defObject.MapSpatialElementTemplateDef.EvaluateToolTip(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_toolTipEvaluated = true;
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004BB04 File Offset: 0x00049D04
		public string DataElementLabel
		{
			get
			{
				if (!this.m_dataElementLabelEvaluated)
				{
					this.m_dataElementLabel = this.m_defObject.MapSpatialElementTemplateDef.EvaluateDataElementLabel(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_dataElementLabelEvaluated = true;
				}
				if (this.m_dataElementLabel == null)
				{
					return this.Label;
				}
				return this.m_dataElementLabel;
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004BB68 File Offset: 0x00049D68
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_hidden = null;
			this.m_offsetX = null;
			this.m_offsetY = null;
			this.m_label = null;
			this.m_labelEvaluated = false;
			this.m_toolTip = null;
			this.m_toolTipEvaluated = false;
			this.m_dataElementLabel = null;
			this.m_dataElementLabelEvaluated = false;
		}

		// Token: 0x040008CE RID: 2254
		private MapSpatialElementTemplate m_defObject;

		// Token: 0x040008CF RID: 2255
		private StyleInstance m_style;

		// Token: 0x040008D0 RID: 2256
		private bool? m_hidden;

		// Token: 0x040008D1 RID: 2257
		private double? m_offsetX;

		// Token: 0x040008D2 RID: 2258
		private double? m_offsetY;

		// Token: 0x040008D3 RID: 2259
		private string m_label;

		// Token: 0x040008D4 RID: 2260
		private bool m_labelEvaluated;

		// Token: 0x040008D5 RID: 2261
		private string m_toolTip;

		// Token: 0x040008D6 RID: 2262
		private bool m_toolTipEvaluated;

		// Token: 0x040008D7 RID: 2263
		private string m_dataElementLabel;

		// Token: 0x040008D8 RID: 2264
		private bool m_dataElementLabelEvaluated;
	}
}
