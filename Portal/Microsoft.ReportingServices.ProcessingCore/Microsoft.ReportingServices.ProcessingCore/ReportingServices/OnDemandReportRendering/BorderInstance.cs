using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000342 RID: 834
	public sealed class BorderInstance : BaseInstance
	{
		// Token: 0x06001FDB RID: 8155 RVA: 0x0007A5C7 File Offset: 0x000787C7
		internal BorderInstance(Border owner, IReportScope reportScope, BorderStyles defaultStyleValueIfExpressionNull)
			: base(reportScope)
		{
			this.m_owner = owner;
			this.m_defaultStyleValueIfExpressionNull = defaultStyleValueIfExpressionNull;
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0007A5DE File Offset: 0x000787DE
		// (set) Token: 0x06001FDD RID: 8157 RVA: 0x0007A618 File Offset: 0x00078818
		public ReportColor Color
		{
			get
			{
				if (!this.m_colorEvaluated)
				{
					this.m_colorEvaluated = true;
					this.m_color = this.m_owner.Owner.EvaluateInstanceReportColor(this.m_owner.ColorAttrName);
				}
				return this.m_color;
			}
			set
			{
				if (this.m_owner.Owner.ReportElement == null || this.m_owner.Owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_owner.Owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_owner.Color.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_colorEvaluated = true;
				this.m_colorAssigned = true;
				this.m_color = value;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x0007A698 File Offset: 0x00078898
		internal bool IsColorAssigned
		{
			get
			{
				return this.m_colorAssigned;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0007A6A0 File Offset: 0x000788A0
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0007A70C File Offset: 0x0007890C
		public BorderStyles Style
		{
			get
			{
				if (!this.m_styleEvaluated)
				{
					this.m_styleEvaluated = true;
					this.m_style = (BorderStyles)this.m_owner.Owner.EvaluateInstanceStyleEnum(this.m_owner.StyleAttrName, (int)this.m_defaultStyleValueIfExpressionNull);
					if (this.m_style == BorderStyles.Default && this.m_owner.BorderPosition == Border.Position.Default)
					{
						this.m_style = this.m_defaultStyleValueIfExpressionNull;
					}
				}
				return this.m_style;
			}
			set
			{
				if (this.m_owner.Owner.ReportElement == null || this.m_owner.Owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_owner.Owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_owner.Style.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				string text = value.ToString();
				string text2;
				if (!Microsoft.ReportingServices.ReportPublishing.Validator.ValidateBorderStyle(text, this.m_owner.BorderPosition == Border.Position.Default, this.m_owner.Owner.StyleContainer.ObjectType, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageSubElement(this.m_owner.Owner.StyleContainer), out text2))
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_styleEvaluated = true;
				this.m_styleAssigned = true;
				if (text2 != text)
				{
					this.m_style = StyleTranslator.TranslateBorderStyle(text2, null);
					return;
				}
				this.m_style = value;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0007A7FE File Offset: 0x000789FE
		internal bool IsStyleAssigned
		{
			get
			{
				return this.m_styleAssigned;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0007A806 File Offset: 0x00078A06
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x0007A840 File Offset: 0x00078A40
		public ReportSize Width
		{
			get
			{
				if (!this.m_widthEvaluated)
				{
					this.m_widthEvaluated = true;
					this.m_width = this.m_owner.Owner.EvaluateInstanceReportSize(this.m_owner.WidthAttrName);
				}
				return this.m_width;
			}
			set
			{
				if (this.m_owner.Owner.ReportElement == null || this.m_owner.Owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_owner.Owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_owner.Width.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_widthEvaluated = true;
				this.m_widthAssigned = true;
				this.m_width = value;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x0007A8C0 File Offset: 0x00078AC0
		internal bool IsWidthAssigned
		{
			get
			{
				return this.m_widthAssigned;
			}
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0007A8C8 File Offset: 0x00078AC8
		protected override void ResetInstanceCache()
		{
			this.m_colorEvaluated = false;
			this.m_colorAssigned = false;
			this.m_color = null;
			this.m_styleEvaluated = false;
			this.m_styleAssigned = false;
			this.m_widthEvaluated = false;
			this.m_widthAssigned = false;
			this.m_width = null;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0007A904 File Offset: 0x00078B04
		internal void GetAssignedDynamicValues(List<int> styles, List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo> values)
		{
			if (this.m_colorAssigned && this.m_owner.Color.IsExpression)
			{
				styles.Add((int)this.m_owner.ColorAttrName);
				values.Add(StyleInstance.CreateAttrInfo(this.m_color));
			}
			if (this.m_styleAssigned && this.m_owner.Style.IsExpression)
			{
				styles.Add((int)this.m_owner.StyleAttrName);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_style));
			}
			if (this.m_widthAssigned && this.m_owner.Width.IsExpression)
			{
				styles.Add((int)this.m_owner.WidthAttrName);
				values.Add(StyleInstance.CreateAttrInfo(this.m_width));
			}
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0007A9C8 File Offset: 0x00078BC8
		internal void SetAssignedDynamicValue(BorderInstance.BorderStyleProperty prop, Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo value, bool allowTransparency)
		{
			switch (prop)
			{
			case BorderInstance.BorderStyleProperty.Color:
				this.m_colorEvaluated = true;
				this.m_color = new ReportColor(value.Value, allowTransparency);
				return;
			case BorderInstance.BorderStyleProperty.Style:
				this.m_styleEvaluated = true;
				this.m_style = (BorderStyles)value.IntValue;
				return;
			case BorderInstance.BorderStyleProperty.Width:
				this.m_widthEvaluated = true;
				this.m_width = new ReportSize(value.Value);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000FEC RID: 4076
		private Border m_owner;

		// Token: 0x04000FED RID: 4077
		private BorderStyles m_defaultStyleValueIfExpressionNull;

		// Token: 0x04000FEE RID: 4078
		private ReportColor m_color;

		// Token: 0x04000FEF RID: 4079
		private BorderStyles m_style;

		// Token: 0x04000FF0 RID: 4080
		private ReportSize m_width;

		// Token: 0x04000FF1 RID: 4081
		private bool m_colorEvaluated;

		// Token: 0x04000FF2 RID: 4082
		private bool m_colorAssigned;

		// Token: 0x04000FF3 RID: 4083
		private bool m_styleEvaluated;

		// Token: 0x04000FF4 RID: 4084
		private bool m_styleAssigned;

		// Token: 0x04000FF5 RID: 4085
		private bool m_widthEvaluated;

		// Token: 0x04000FF6 RID: 4086
		private bool m_widthAssigned;

		// Token: 0x0200094E RID: 2382
		internal enum BorderStyleProperty
		{
			// Token: 0x0400405D RID: 16477
			Color,
			// Token: 0x0400405E RID: 16478
			Style,
			// Token: 0x0400405F RID: 16479
			Width
		}
	}
}
