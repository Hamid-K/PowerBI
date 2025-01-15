using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000191 RID: 401
	public sealed class MapDistanceScale : MapDockableSubItem
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x00045579 File Offset: 0x00043779
		internal MapDistanceScale(MapDistanceScale defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00045584 File Offset: 0x00043784
		public ReportColorProperty ScaleColor
		{
			get
			{
				if (this.m_scaleColor == null && this.MapDistanceScaleDef.ScaleColor != null)
				{
					ExpressionInfo scaleColor = this.MapDistanceScaleDef.ScaleColor;
					if (scaleColor != null)
					{
						this.m_scaleColor = new ReportColorProperty(scaleColor.IsExpression, this.MapDistanceScaleDef.ScaleColor.OriginalText, scaleColor.IsExpression ? null : new ReportColor(scaleColor.StringValue.Trim(), true), scaleColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_scaleColor;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00045614 File Offset: 0x00043814
		public ReportColorProperty ScaleBorderColor
		{
			get
			{
				if (this.m_scaleBorderColor == null && this.MapDistanceScaleDef.ScaleBorderColor != null)
				{
					ExpressionInfo scaleBorderColor = this.MapDistanceScaleDef.ScaleBorderColor;
					if (scaleBorderColor != null)
					{
						this.m_scaleBorderColor = new ReportColorProperty(scaleBorderColor.IsExpression, this.MapDistanceScaleDef.ScaleBorderColor.OriginalText, scaleBorderColor.IsExpression ? null : new ReportColor(scaleBorderColor.StringValue.Trim(), true), scaleBorderColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_scaleBorderColor;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000456A3 File Offset: 0x000438A3
		internal MapDistanceScale MapDistanceScaleDef
		{
			get
			{
				return (MapDistanceScale)this.m_defObject;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000456B0 File Offset: 0x000438B0
		public new MapDistanceScaleInstance Instance
		{
			get
			{
				return (MapDistanceScaleInstance)this.GetInstance();
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000456BD File Offset: 0x000438BD
		internal override MapSubItemInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapDistanceScaleInstance(this);
			}
			return (MapSubItemInstance)this.m_instance;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000456F2 File Offset: 0x000438F2
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400079D RID: 1949
		private ReportColorProperty m_scaleColor;

		// Token: 0x0400079E RID: 1950
		private ReportColorProperty m_scaleBorderColor;
	}
}
