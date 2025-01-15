using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000119 RID: 281
	public sealed class LinearPointer : GaugePointer
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x00035C44 File Offset: 0x00033E44
		internal LinearPointer(LinearPointer defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00035C5C File Offset: 0x00033E5C
		public ReportEnumProperty<LinearPointerTypes> Type
		{
			get
			{
				if (this.m_type == null && this.LinearPointerDef.Type != null)
				{
					this.m_type = new ReportEnumProperty<LinearPointerTypes>(this.LinearPointerDef.Type.IsExpression, this.LinearPointerDef.Type.OriginalText, EnumTranslator.TranslateLinearPointerTypes(this.LinearPointerDef.Type.StringValue, null));
				}
				return this.m_type;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00035CC5 File Offset: 0x00033EC5
		public Thermometer Thermometer
		{
			get
			{
				if (this.m_thermometer == null && this.LinearPointerDef.Thermometer != null)
				{
					this.m_thermometer = new Thermometer(this.LinearPointerDef.Thermometer, this.m_gaugePanel);
				}
				return this.m_thermometer;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00035CFE File Offset: 0x00033EFE
		internal LinearPointer LinearPointerDef
		{
			get
			{
				return (LinearPointer)this.m_defObject;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00035D0B File Offset: 0x00033F0B
		public new LinearPointerInstance Instance
		{
			get
			{
				return (LinearPointerInstance)this.GetInstance();
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00035D18 File Offset: 0x00033F18
		internal override GaugePointerInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new LinearPointerInstance(this);
			}
			return (GaugePointerInstance)this.m_instance;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00035D4D File Offset: 0x00033F4D
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_thermometer != null)
			{
				this.m_thermometer.SetNewContext();
			}
		}

		// Token: 0x0400056A RID: 1386
		private ReportEnumProperty<LinearPointerTypes> m_type;

		// Token: 0x0400056B RID: 1387
		private Thermometer m_thermometer;
	}
}
