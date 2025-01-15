using System;
using Microsoft.ReportingServices.RdlExpressions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000116 RID: 278
	public sealed class GaugeInputValueInstance : BaseInstance
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x00035487 File Offset: 0x00033687
		internal GaugeInputValueInstance(GaugeInputValue defObject)
			: base((GaugeCell)defObject.GaugePanelDef.RowCollection.GetIfExists(0).GetIfExists(0))
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000354B2 File Offset: 0x000336B2
		public object Value
		{
			get
			{
				this.EnsureValueIsEvaluated();
				return this.m_valueResult.Value.Value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000354CA File Offset: 0x000336CA
		internal TypeCode ValueTypeCode
		{
			get
			{
				this.EnsureValueIsEvaluated();
				return this.m_valueResult.Value.TypeCode;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000354E2 File Offset: 0x000336E2
		internal bool ErrorOccured
		{
			get
			{
				this.EnsureValueIsEvaluated();
				return this.m_valueResult.Value.ErrorOccurred;
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000354FC File Offset: 0x000336FC
		private void EnsureValueIsEvaluated()
		{
			if (this.m_valueResult == null)
			{
				this.m_valueResult = new VariantResult?(this.m_defObject.GaugeInputValueDef.EvaluateValue(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0003554C File Offset: 0x0003374C
		public GaugeInputValueFormulas Formula
		{
			get
			{
				if (this.m_formula == null)
				{
					this.m_formula = new GaugeInputValueFormulas?(this.m_defObject.GaugeInputValueDef.EvaluateFormula(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_formula.Value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x000355A8 File Offset: 0x000337A8
		public double MinPercent
		{
			get
			{
				if (this.m_minPercent == null)
				{
					this.m_minPercent = new double?(this.m_defObject.GaugeInputValueDef.EvaluateMinPercent(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_minPercent.Value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00035604 File Offset: 0x00033804
		public double MaxPercent
		{
			get
			{
				if (this.m_maxPercent == null)
				{
					this.m_maxPercent = new double?(this.m_defObject.GaugeInputValueDef.EvaluateMaxPercent(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_maxPercent.Value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00035660 File Offset: 0x00033860
		public double Multiplier
		{
			get
			{
				if (this.m_multiplier == null)
				{
					this.m_multiplier = new double?(this.m_defObject.GaugeInputValueDef.EvaluateMultiplier(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_multiplier.Value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000356BC File Offset: 0x000338BC
		public double AddConstant
		{
			get
			{
				if (this.m_addConstant == null)
				{
					this.m_addConstant = new double?(this.m_defObject.GaugeInputValueDef.EvaluateAddConstant(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_addConstant.Value;
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00035718 File Offset: 0x00033918
		protected override void ResetInstanceCache()
		{
			this.m_valueResult = null;
			this.m_formula = null;
			this.m_minPercent = null;
			this.m_maxPercent = null;
			this.m_multiplier = null;
			this.m_addConstant = null;
		}

		// Token: 0x04000551 RID: 1361
		private GaugeInputValue m_defObject;

		// Token: 0x04000552 RID: 1362
		private VariantResult? m_valueResult;

		// Token: 0x04000553 RID: 1363
		private GaugeInputValueFormulas? m_formula;

		// Token: 0x04000554 RID: 1364
		private double? m_minPercent;

		// Token: 0x04000555 RID: 1365
		private double? m_maxPercent;

		// Token: 0x04000556 RID: 1366
		private double? m_multiplier;

		// Token: 0x04000557 RID: 1367
		private double? m_addConstant;
	}
}
