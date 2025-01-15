using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C2 RID: 450
	public sealed class MapColorRangeRule : MapColorRule
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x000490B2 File Offset: 0x000472B2
		internal MapColorRangeRule(MapColorRangeRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000490C0 File Offset: 0x000472C0
		public ReportColorProperty StartColor
		{
			get
			{
				if (this.m_startColor == null && this.MapColorRangeRuleDef.StartColor != null)
				{
					ExpressionInfo startColor = this.MapColorRangeRuleDef.StartColor;
					if (startColor != null)
					{
						this.m_startColor = new ReportColorProperty(startColor.IsExpression, this.MapColorRangeRuleDef.StartColor.OriginalText, startColor.IsExpression ? null : new ReportColor(startColor.StringValue.Trim(), true), startColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_startColor;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00049150 File Offset: 0x00047350
		public ReportColorProperty MiddleColor
		{
			get
			{
				if (this.m_middleColor == null && this.MapColorRangeRuleDef.MiddleColor != null)
				{
					ExpressionInfo middleColor = this.MapColorRangeRuleDef.MiddleColor;
					if (middleColor != null)
					{
						this.m_middleColor = new ReportColorProperty(middleColor.IsExpression, this.MapColorRangeRuleDef.MiddleColor.OriginalText, middleColor.IsExpression ? null : new ReportColor(middleColor.StringValue.Trim(), true), middleColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_middleColor;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000491E0 File Offset: 0x000473E0
		public ReportColorProperty EndColor
		{
			get
			{
				if (this.m_endColor == null && this.MapColorRangeRuleDef.EndColor != null)
				{
					ExpressionInfo endColor = this.MapColorRangeRuleDef.EndColor;
					if (endColor != null)
					{
						this.m_endColor = new ReportColorProperty(endColor.IsExpression, this.MapColorRangeRuleDef.EndColor.OriginalText, endColor.IsExpression ? null : new ReportColor(endColor.StringValue.Trim(), true), endColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_endColor;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004926F File Offset: 0x0004746F
		internal MapColorRangeRule MapColorRangeRuleDef
		{
			get
			{
				return (MapColorRangeRule)base.MapAppearanceRuleDef;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004927C File Offset: 0x0004747C
		public new MapColorRangeRuleInstance Instance
		{
			get
			{
				return (MapColorRangeRuleInstance)this.GetInstance();
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00049289 File Offset: 0x00047489
		internal override MapAppearanceRuleInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapColorRangeRuleInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000492B9 File Offset: 0x000474B9
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000854 RID: 2132
		private ReportColorProperty m_startColor;

		// Token: 0x04000855 RID: 2133
		private ReportColorProperty m_middleColor;

		// Token: 0x04000856 RID: 2134
		private ReportColorProperty m_endColor;
	}
}
