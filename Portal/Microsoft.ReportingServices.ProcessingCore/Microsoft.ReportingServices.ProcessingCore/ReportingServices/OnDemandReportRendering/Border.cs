using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000345 RID: 837
	public sealed class Border
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x0007ABB4 File Offset: 0x00078DB4
		internal Border(Microsoft.ReportingServices.OnDemandReportRendering.Style owner, Border.Position position, bool defaultSolidBorderStyle)
		{
			this.m_owner = owner;
			this.m_position = position;
			this.m_defaultSolidBorderStyle = defaultSolidBorderStyle;
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0007ABD1 File Offset: 0x00078DD1
		public ReportColorProperty Color
		{
			get
			{
				return this.m_owner[this.ColorAttrName] as ReportColorProperty;
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x0007ABE9 File Offset: 0x00078DE9
		public ReportEnumProperty<BorderStyles> Style
		{
			get
			{
				return this.m_owner[this.StyleAttrName] as ReportEnumProperty<BorderStyles>;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0007AC01 File Offset: 0x00078E01
		public ReportSizeProperty Width
		{
			get
			{
				return this.m_owner[this.WidthAttrName] as ReportSizeProperty;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0007AC1C File Offset: 0x00078E1C
		public BorderInstance Instance
		{
			get
			{
				this.GetInstance();
				Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = this.Owner.ReportElement as Microsoft.ReportingServices.OnDemandReportRendering.ReportItem;
				if (reportItem != null)
				{
					reportItem.CriEvaluateInstance();
				}
				return this.m_instance;
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0007AC50 File Offset: 0x00078E50
		internal BorderInstance GetInstance()
		{
			if (this.m_owner.m_renderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				BorderStyles borderStyles = (this.m_defaultSolidBorderStyle ? BorderStyles.Solid : BorderStyles.None);
				if (this.m_owner.IsOldSnapshot)
				{
					this.m_instance = new BorderInstance(this, null, borderStyles);
				}
				else
				{
					this.m_instance = new BorderInstance(this, this.m_owner.ReportScope, borderStyles);
				}
			}
			return this.m_instance;
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0007ACC1 File Offset: 0x00078EC1
		internal Microsoft.ReportingServices.OnDemandReportRendering.Style Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x0007ACC9 File Offset: 0x00078EC9
		internal Border.Position BorderPosition
		{
			get
			{
				return this.m_position;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0007ACD4 File Offset: 0x00078ED4
		internal StyleAttributeNames ColorAttrName
		{
			get
			{
				switch (this.m_position)
				{
				case Border.Position.Default:
					return StyleAttributeNames.BorderColor;
				case Border.Position.Top:
					return StyleAttributeNames.BorderColorTop;
				case Border.Position.Left:
					return StyleAttributeNames.BorderColorLeft;
				case Border.Position.Right:
					return StyleAttributeNames.BorderColorRight;
				case Border.Position.Bottom:
					return StyleAttributeNames.BorderColorBottom;
				default:
					Global.Tracer.Assert(false);
					return StyleAttributeNames.BorderColor;
				}
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0007AD1C File Offset: 0x00078F1C
		internal StyleAttributeNames StyleAttrName
		{
			get
			{
				switch (this.m_position)
				{
				case Border.Position.Default:
					return StyleAttributeNames.BorderStyle;
				case Border.Position.Top:
					return StyleAttributeNames.BorderStyleTop;
				case Border.Position.Left:
					return StyleAttributeNames.BorderStyleLeft;
				case Border.Position.Right:
					return StyleAttributeNames.BorderStyleRight;
				case Border.Position.Bottom:
					return StyleAttributeNames.BorderStyleBottom;
				default:
					Global.Tracer.Assert(false);
					return StyleAttributeNames.BorderColor;
				}
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x0007AD64 File Offset: 0x00078F64
		internal StyleAttributeNames WidthAttrName
		{
			get
			{
				switch (this.m_position)
				{
				case Border.Position.Default:
					return StyleAttributeNames.BorderWidth;
				case Border.Position.Top:
					return StyleAttributeNames.BorderWidthTop;
				case Border.Position.Left:
					return StyleAttributeNames.BorderWidthLeft;
				case Border.Position.Right:
					return StyleAttributeNames.BorderWidthRight;
				case Border.Position.Bottom:
					return StyleAttributeNames.BorderWidthBottom;
				default:
					Global.Tracer.Assert(false);
					return StyleAttributeNames.BorderColor;
				}
			}
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0007ADB0 File Offset: 0x00078FB0
		internal void ConstructBorderDefinition()
		{
			Global.Tracer.Assert(this.m_owner.ReportElement != null, "(m_owner.ReportElement != null)");
			Global.Tracer.Assert(this.m_owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition, "(m_owner.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition)");
			Global.Tracer.Assert(this.m_owner.StyleContainer.StyleClass != null, "(m_owner.StyleContainer.StyleClass != null)");
			Global.Tracer.Assert(!this.Color.IsExpression, "(!this.Color.IsExpression)");
			if (this.Instance.IsColorAssigned)
			{
				string text = ((this.Instance.Color != null) ? this.Instance.Color.ToString() : null);
				this.m_owner.StyleContainer.StyleClass.AddAttribute(this.m_owner.GetStyleStringFromEnum(this.ColorAttrName), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text));
			}
			else
			{
				this.m_owner.StyleContainer.StyleClass.AddAttribute(this.m_owner.GetStyleStringFromEnum(this.ColorAttrName), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(this.Style == null || !this.Style.IsExpression, "(this.Style == null || !this.Style.IsExpression)");
			if (this.Instance.IsStyleAssigned)
			{
				this.m_owner.StyleContainer.StyleClass.AddAttribute(this.m_owner.GetStyleStringFromEnum(this.StyleAttrName), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(this.Instance.Style.ToString()));
			}
			else
			{
				this.m_owner.StyleContainer.StyleClass.AddAttribute(this.m_owner.GetStyleStringFromEnum(this.StyleAttrName), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(this.Width == null || !this.Width.IsExpression, "(this.Width == null || !this.Width.IsExpression)");
			if (this.Instance.IsWidthAssigned)
			{
				string text2 = ((this.Instance.Width != null) ? this.Instance.Width.ToString() : null);
				this.m_owner.StyleContainer.StyleClass.AddAttribute(this.m_owner.GetStyleStringFromEnum(this.WidthAttrName), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text2));
				return;
			}
			this.m_owner.StyleContainer.StyleClass.AddAttribute(this.m_owner.GetStyleStringFromEnum(this.WidthAttrName), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
		}

		// Token: 0x0400102F RID: 4143
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_owner;

		// Token: 0x04001030 RID: 4144
		private Border.Position m_position;

		// Token: 0x04001031 RID: 4145
		private bool m_defaultSolidBorderStyle;

		// Token: 0x04001032 RID: 4146
		private BorderInstance m_instance;

		// Token: 0x02000950 RID: 2384
		internal enum Position
		{
			// Token: 0x04004065 RID: 16485
			Default,
			// Token: 0x04004066 RID: 16486
			Top,
			// Token: 0x04004067 RID: 16487
			Left,
			// Token: 0x04004068 RID: 16488
			Right,
			// Token: 0x04004069 RID: 16489
			Bottom
		}
	}
}
