using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E6 RID: 486
	public sealed class MapPolygonTemplateInstance : MapSpatialElementTemplateInstance
	{
		// Token: 0x0600127B RID: 4731 RVA: 0x0004B6AB File Offset: 0x000498AB
		internal MapPolygonTemplateInstance(MapPolygonTemplate defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004B6BC File Offset: 0x000498BC
		public double ScaleFactor
		{
			get
			{
				if (this.m_scaleFactor == null)
				{
					this.m_scaleFactor = new double?(((MapPolygonTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateScaleFactor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_scaleFactor.Value;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004B720 File Offset: 0x00049920
		public double CenterPointOffsetX
		{
			get
			{
				if (this.m_centerPointOffsetX == null)
				{
					this.m_centerPointOffsetX = new double?(((MapPolygonTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateCenterPointOffsetX(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_centerPointOffsetX.Value;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0004B784 File Offset: 0x00049984
		public double CenterPointOffsetY
		{
			get
			{
				if (this.m_centerPointOffsetY == null)
				{
					this.m_centerPointOffsetY = new double?(((MapPolygonTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateCenterPointOffsetY(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_centerPointOffsetY.Value;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0004B7E8 File Offset: 0x000499E8
		public MapAutoBool ShowLabel
		{
			get
			{
				if (this.m_showLabel == null)
				{
					this.m_showLabel = new MapAutoBool?(((MapPolygonTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateShowLabel(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_showLabel.Value;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0004B848 File Offset: 0x00049A48
		public MapPolygonLabelPlacement LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null)
				{
					this.m_labelPlacement = new MapPolygonLabelPlacement?(((MapPolygonTemplate)this.m_defObject.MapSpatialElementTemplateDef).EvaluateLabelPlacement(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelPlacement.Value;
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004B8A8 File Offset: 0x00049AA8
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_scaleFactor = null;
			this.m_centerPointOffsetX = null;
			this.m_centerPointOffsetY = null;
			this.m_showLabel = null;
			this.m_labelPlacement = null;
		}

		// Token: 0x040008C8 RID: 2248
		private MapPolygonTemplate m_defObject;

		// Token: 0x040008C9 RID: 2249
		private double? m_scaleFactor;

		// Token: 0x040008CA RID: 2250
		private double? m_centerPointOffsetX;

		// Token: 0x040008CB RID: 2251
		private double? m_centerPointOffsetY;

		// Token: 0x040008CC RID: 2252
		private MapAutoBool? m_showLabel;

		// Token: 0x040008CD RID: 2253
		private MapPolygonLabelPlacement? m_labelPlacement;
	}
}
