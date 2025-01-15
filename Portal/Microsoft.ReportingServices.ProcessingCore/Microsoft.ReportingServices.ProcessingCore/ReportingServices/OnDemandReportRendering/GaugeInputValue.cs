using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000110 RID: 272
	public sealed class GaugeInputValue
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x00034B5B File Offset: 0x00032D5B
		internal GaugeInputValue(GaugeInputValue defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00034B71 File Offset: 0x00032D71
		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null && this.m_defObject.Value != null)
				{
					this.m_value = new ReportVariantProperty(this.m_defObject.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00034BA4 File Offset: 0x00032DA4
		public ReportEnumProperty<GaugeInputValueFormulas> Formula
		{
			get
			{
				if (this.m_formula == null && this.m_defObject.Formula != null)
				{
					this.m_formula = new ReportEnumProperty<GaugeInputValueFormulas>(this.m_defObject.Formula.IsExpression, this.m_defObject.Formula.OriginalText, EnumTranslator.TranslateGaugeInputValueFormulas(this.m_defObject.Formula.StringValue, null));
				}
				return this.m_formula;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00034C0D File Offset: 0x00032E0D
		public ReportDoubleProperty MinPercent
		{
			get
			{
				if (this.m_minPercent == null && this.m_defObject.MinPercent != null)
				{
					this.m_minPercent = new ReportDoubleProperty(this.m_defObject.MinPercent);
				}
				return this.m_minPercent;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00034C40 File Offset: 0x00032E40
		public ReportDoubleProperty MaxPercent
		{
			get
			{
				if (this.m_maxPercent == null && this.m_defObject.MaxPercent != null)
				{
					this.m_maxPercent = new ReportDoubleProperty(this.m_defObject.MaxPercent);
				}
				return this.m_maxPercent;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00034C73 File Offset: 0x00032E73
		public ReportDoubleProperty Multiplier
		{
			get
			{
				if (this.m_multiplier == null && this.m_defObject.Multiplier != null)
				{
					this.m_multiplier = new ReportDoubleProperty(this.m_defObject.Multiplier);
				}
				return this.m_multiplier;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00034CA6 File Offset: 0x00032EA6
		public ReportDoubleProperty AddConstant
		{
			get
			{
				if (this.m_addConstant == null && this.m_defObject.AddConstant != null)
				{
					this.m_addConstant = new ReportDoubleProperty(this.m_defObject.AddConstant);
				}
				return this.m_addConstant;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00034CD9 File Offset: 0x00032ED9
		public string DataElementName
		{
			get
			{
				return this.m_defObject.DataElementName;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00034CE6 File Offset: 0x00032EE6
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_defObject.DataElementOutput;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00034CF3 File Offset: 0x00032EF3
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00034CFB File Offset: 0x00032EFB
		internal GaugeInputValue GaugeInputValueDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00034D03 File Offset: 0x00032F03
		public GaugeInputValueInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new GaugeInputValueInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00034D33 File Offset: 0x00032F33
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00034D46 File Offset: 0x00032F46
		public CompiledGaugeInputValueInstance CompiledInstance
		{
			get
			{
				this.GaugePanelDef.ProcessCompiledInstances();
				return this.m_compiledInstance;
			}
			internal set
			{
				this.m_compiledInstance = value;
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00034D4F File Offset: 0x00032F4F
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000535 RID: 1333
		private GaugePanel m_gaugePanel;

		// Token: 0x04000536 RID: 1334
		private GaugeInputValue m_defObject;

		// Token: 0x04000537 RID: 1335
		private GaugeInputValueInstance m_instance;

		// Token: 0x04000538 RID: 1336
		private CompiledGaugeInputValueInstance m_compiledInstance;

		// Token: 0x04000539 RID: 1337
		private ReportVariantProperty m_value;

		// Token: 0x0400053A RID: 1338
		private ReportEnumProperty<GaugeInputValueFormulas> m_formula;

		// Token: 0x0400053B RID: 1339
		private ReportDoubleProperty m_minPercent;

		// Token: 0x0400053C RID: 1340
		private ReportDoubleProperty m_maxPercent;

		// Token: 0x0400053D RID: 1341
		private ReportDoubleProperty m_multiplier;

		// Token: 0x0400053E RID: 1342
		private ReportDoubleProperty m_addConstant;
	}
}
