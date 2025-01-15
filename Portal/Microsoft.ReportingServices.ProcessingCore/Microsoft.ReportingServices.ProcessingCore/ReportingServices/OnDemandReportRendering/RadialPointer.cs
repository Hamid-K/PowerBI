using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200011B RID: 283
	public sealed class RadialPointer : GaugePointer
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x00035FB2 File Offset: 0x000341B2
		internal RadialPointer(RadialPointer defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00035FCC File Offset: 0x000341CC
		public ReportEnumProperty<RadialPointerTypes> Type
		{
			get
			{
				if (this.m_type == null && this.RadialPointerDef.Type != null)
				{
					this.m_type = new ReportEnumProperty<RadialPointerTypes>(this.RadialPointerDef.Type.IsExpression, this.RadialPointerDef.Type.OriginalText, EnumTranslator.TranslateRadialPointerTypes(this.RadialPointerDef.Type.StringValue, null));
				}
				return this.m_type;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00036035 File Offset: 0x00034235
		public PointerCap PointerCap
		{
			get
			{
				if (this.m_pointerCap == null && this.RadialPointerDef.PointerCap != null)
				{
					this.m_pointerCap = new PointerCap(this.RadialPointerDef.PointerCap, this.m_gaugePanel);
				}
				return this.m_pointerCap;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00036070 File Offset: 0x00034270
		public ReportEnumProperty<RadialPointerNeedleStyles> NeedleStyle
		{
			get
			{
				if (this.m_needleStyle == null && this.RadialPointerDef.NeedleStyle != null)
				{
					this.m_needleStyle = new ReportEnumProperty<RadialPointerNeedleStyles>(this.RadialPointerDef.NeedleStyle.IsExpression, this.RadialPointerDef.NeedleStyle.OriginalText, EnumTranslator.TranslateRadialPointerNeedleStyles(this.RadialPointerDef.NeedleStyle.StringValue, null));
				}
				return this.m_needleStyle;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x000360D9 File Offset: 0x000342D9
		internal RadialPointer RadialPointerDef
		{
			get
			{
				return (RadialPointer)this.m_defObject;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000360E6 File Offset: 0x000342E6
		public new RadialPointerInstance Instance
		{
			get
			{
				return (RadialPointerInstance)this.GetInstance();
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000360F3 File Offset: 0x000342F3
		internal override GaugePointerInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new RadialPointerInstance(this);
			}
			return (GaugePointerInstance)this.m_instance;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00036128 File Offset: 0x00034328
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_pointerCap != null)
			{
				this.m_pointerCap.SetNewContext();
			}
		}

		// Token: 0x04000576 RID: 1398
		private ReportEnumProperty<RadialPointerTypes> m_type;

		// Token: 0x04000577 RID: 1399
		private PointerCap m_pointerCap;

		// Token: 0x04000578 RID: 1400
		private ReportEnumProperty<RadialPointerNeedleStyles> m_needleStyle;
	}
}
