using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000120 RID: 288
	public sealed class RadialPointerInstance : GaugePointerInstance
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x000368DC File Offset: 0x00034ADC
		internal RadialPointerInstance(RadialPointer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000368EC File Offset: 0x00034AEC
		public RadialPointerTypes Type
		{
			get
			{
				if (this.m_type == null)
				{
					this.m_type = new RadialPointerTypes?(((RadialPointer)this.m_defObject.GaugePointerDef).EvaluateType(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_type.Value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003694C File Offset: 0x00034B4C
		public RadialPointerNeedleStyles NeedleStyle
		{
			get
			{
				if (this.m_needleStyle == null)
				{
					this.m_needleStyle = new RadialPointerNeedleStyles?(((RadialPointer)this.m_defObject.GaugePointerDef).EvaluateNeedleStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_needleStyle.Value;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000369AC File Offset: 0x00034BAC
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_type = null;
			this.m_needleStyle = null;
		}

		// Token: 0x0400058F RID: 1423
		private RadialPointer m_defObject;

		// Token: 0x04000590 RID: 1424
		private RadialPointerTypes? m_type;

		// Token: 0x04000591 RID: 1425
		private RadialPointerNeedleStyles? m_needleStyle;
	}
}
