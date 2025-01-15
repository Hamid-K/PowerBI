using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000341 RID: 833
	[SkipStaticValidation]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class StyleInstance : StyleBaseInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x00077F49 File Offset: 0x00076149
		internal StyleInstance(IROMStyleDefinitionContainer styleDefinitionContainer, IReportScope reportScope, RenderingContext context)
			: base(context, reportScope)
		{
			this.m_styleDefinition = styleDefinitionContainer.Style;
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00077F5F File Offset: 0x0007615F
		public override List<StyleAttributeNames> StyleAttributes
		{
			get
			{
				return this.m_styleDefinition.NonSharedStyleAttributes;
			}
		}

		// Token: 0x170011AE RID: 4526
		public override object this[StyleAttributeNames style]
		{
			get
			{
				switch (style)
				{
				case StyleAttributeNames.BorderColor:
					return this.m_styleDefinition.Border.Instance.Color;
				case StyleAttributeNames.BorderColorTop:
				{
					Border border = this.m_styleDefinition.TopBorder;
					if (border != null)
					{
						return border.Instance.Color;
					}
					return null;
				}
				case StyleAttributeNames.BorderColorLeft:
				{
					Border border = this.m_styleDefinition.LeftBorder;
					if (border != null)
					{
						return border.Instance.Color;
					}
					return null;
				}
				case StyleAttributeNames.BorderColorRight:
				{
					Border border = this.m_styleDefinition.RightBorder;
					if (border != null)
					{
						return border.Instance.Color;
					}
					return null;
				}
				case StyleAttributeNames.BorderColorBottom:
				{
					Border border = this.m_styleDefinition.BottomBorder;
					if (border != null)
					{
						return border.Instance.Color;
					}
					return null;
				}
				case StyleAttributeNames.BorderStyle:
					return this.m_styleDefinition.Border.Instance.Style;
				case StyleAttributeNames.BorderStyleTop:
				{
					Border border = this.m_styleDefinition.TopBorder;
					if (border != null)
					{
						return border.Instance.Style;
					}
					return null;
				}
				case StyleAttributeNames.BorderStyleLeft:
				{
					Border border = this.m_styleDefinition.LeftBorder;
					if (border != null)
					{
						return border.Instance.Style;
					}
					return null;
				}
				case StyleAttributeNames.BorderStyleRight:
				{
					Border border = this.m_styleDefinition.RightBorder;
					if (border != null)
					{
						return border.Instance.Style;
					}
					return null;
				}
				case StyleAttributeNames.BorderStyleBottom:
				{
					Border border = this.m_styleDefinition.BottomBorder;
					if (border != null)
					{
						return border.Instance.Style;
					}
					return null;
				}
				case StyleAttributeNames.BorderWidth:
					return this.m_styleDefinition.Border.Instance.Width;
				case StyleAttributeNames.BorderWidthTop:
				{
					Border border = this.m_styleDefinition.TopBorder;
					if (border != null)
					{
						return border.Instance.Width;
					}
					return null;
				}
				case StyleAttributeNames.BorderWidthLeft:
				{
					Border border = this.m_styleDefinition.LeftBorder;
					if (border != null)
					{
						return border.Instance.Width;
					}
					return null;
				}
				case StyleAttributeNames.BorderWidthRight:
				{
					Border border = this.m_styleDefinition.RightBorder;
					if (border != null)
					{
						return border.Instance.Width;
					}
					return null;
				}
				case StyleAttributeNames.BorderWidthBottom:
				{
					Border border = this.m_styleDefinition.BottomBorder;
					if (border != null)
					{
						return border.Instance.Width;
					}
					return null;
				}
				case StyleAttributeNames.BackgroundColor:
					return this.BackgroundColor;
				case StyleAttributeNames.FontStyle:
					return this.FontStyle;
				case StyleAttributeNames.FontFamily:
					return this.FontFamily;
				case StyleAttributeNames.FontSize:
					return this.FontSize;
				case StyleAttributeNames.FontWeight:
					return this.FontWeight;
				case StyleAttributeNames.Format:
					return this.Format;
				case StyleAttributeNames.TextDecoration:
					return this.TextDecoration;
				case StyleAttributeNames.TextAlign:
					return this.TextAlign;
				case StyleAttributeNames.VerticalAlign:
					return this.VerticalAlign;
				case StyleAttributeNames.Color:
					return this.Color;
				case StyleAttributeNames.PaddingLeft:
					return this.PaddingLeft;
				case StyleAttributeNames.PaddingRight:
					return this.PaddingRight;
				case StyleAttributeNames.PaddingTop:
					return this.PaddingTop;
				case StyleAttributeNames.PaddingBottom:
					return this.PaddingBottom;
				case StyleAttributeNames.LineHeight:
					return this.LineHeight;
				case StyleAttributeNames.Direction:
					return this.Direction;
				case StyleAttributeNames.WritingMode:
					return this.WritingMode;
				case StyleAttributeNames.Language:
					return this.Language;
				case StyleAttributeNames.UnicodeBiDi:
					return this.UnicodeBiDi;
				case StyleAttributeNames.Calendar:
					return this.Calendar;
				case StyleAttributeNames.NumeralLanguage:
					return this.NumeralLanguage;
				case StyleAttributeNames.NumeralVariant:
					return this.NumeralVariant;
				case StyleAttributeNames.BackgroundGradientType:
					return this.BackgroundGradientType;
				case StyleAttributeNames.BackgroundGradientEndColor:
					return this.BackgroundGradientEndColor;
				case StyleAttributeNames.BackgroundHatchType:
					return this.BackgroundHatchType;
				case StyleAttributeNames.ShadowColor:
					return this.ShadowColor;
				case StyleAttributeNames.ShadowOffset:
					return this.ShadowOffset;
				case StyleAttributeNames.TextEffect:
					return this.TextEffect;
				case StyleAttributeNames.CurrencyLanguage:
					return this.CurrencyLanguage;
				}
				return null;
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x00078316 File Offset: 0x00076516
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0007833C File Offset: 0x0007653C
		public override ReportColor BackgroundGradientEndColor
		{
			get
			{
				if (this.m_backgroundGradientEndColor == null)
				{
					this.m_backgroundGradientEndColor = this.m_styleDefinition.EvaluateInstanceReportColor(StyleAttributeNames.BackgroundGradientEndColor);
				}
				return this.m_backgroundGradientEndColor;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.BackgroundGradientEndColor.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.BackgroundGradientEndColor);
				this.m_backgroundGradientEndColor = value;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000783A7 File Offset: 0x000765A7
		internal bool IsBackgroundGradientEndColorAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.BackgroundGradientEndColor);
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000783C0 File Offset: 0x000765C0
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x000783E4 File Offset: 0x000765E4
		public override ReportColor Color
		{
			get
			{
				if (this.m_color == null)
				{
					this.m_color = this.m_styleDefinition.EvaluateInstanceReportColor(StyleAttributeNames.Color);
				}
				return this.m_color;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.Color.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.Color);
				this.m_color = value;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0007844F File Offset: 0x0007664F
		internal bool IsColorAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.Color);
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00078468 File Offset: 0x00076668
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x0007848C File Offset: 0x0007668C
		public override ReportColor BackgroundColor
		{
			get
			{
				if (this.m_backgroundColor == null)
				{
					this.m_backgroundColor = this.m_styleDefinition.EvaluateInstanceReportColor(StyleAttributeNames.BackgroundColor);
				}
				return this.m_backgroundColor;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.BackgroundColor.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.BackgroundColor);
				this.m_backgroundColor = value;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000784F7 File Offset: 0x000766F7
		internal bool IsBackgroundColorAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.BackgroundColor);
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00078510 File Offset: 0x00076710
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x00078544 File Offset: 0x00076744
		public override FontStyles FontStyle
		{
			get
			{
				if (this.m_fontStyle == null)
				{
					this.m_fontStyle = new FontStyles?((FontStyles)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.FontStyle));
				}
				return this.m_fontStyle.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.FontStyle.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.FontStyle);
				this.m_fontStyle = new FontStyles?(value);
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000785B4 File Offset: 0x000767B4
		internal bool IsFontStyleAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.FontStyle);
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000785CD File Offset: 0x000767CD
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x000785F0 File Offset: 0x000767F0
		public override string FontFamily
		{
			get
			{
				if (this.m_fontFamily == null)
				{
					this.m_fontFamily = this.m_styleDefinition.EvaluateInstanceStyleString(StyleAttributeNames.FontFamily);
				}
				return this.m_fontFamily;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.FontFamily.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.FontFamily);
				this.m_fontFamily = value;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0007865B File Offset: 0x0007685B
		internal bool IsFontFamilyAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.FontFamily);
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x00078674 File Offset: 0x00076874
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x000786A8 File Offset: 0x000768A8
		public override FontWeights FontWeight
		{
			get
			{
				if (this.m_fontWeight == null)
				{
					this.m_fontWeight = new FontWeights?((FontWeights)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.FontWeight));
				}
				return this.m_fontWeight.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.FontWeight.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.FontWeight);
				this.m_fontWeight = new FontWeights?(value);
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x00078718 File Offset: 0x00076918
		internal bool IsFontWeightAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.FontWeight);
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00078731 File Offset: 0x00076931
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x00078754 File Offset: 0x00076954
		public override string Format
		{
			get
			{
				if (this.m_format == null)
				{
					this.m_format = this.m_styleDefinition.EvaluateInstanceStyleString(StyleAttributeNames.Format);
				}
				return this.m_format;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.Format.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.Format);
				this.m_format = value;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x000787BF File Offset: 0x000769BF
		internal bool IsFormatAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.Format);
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x000787D8 File Offset: 0x000769D8
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x0007880C File Offset: 0x00076A0C
		public override TextDecorations TextDecoration
		{
			get
			{
				if (this.m_textDecoration == null)
				{
					this.m_textDecoration = new TextDecorations?((TextDecorations)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.TextDecoration));
				}
				return this.m_textDecoration.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.TextDecoration.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.TextDecoration);
				this.m_textDecoration = new TextDecorations?(value);
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x0007887C File Offset: 0x00076A7C
		internal bool IsTextDecorationAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.TextDecoration);
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00078895 File Offset: 0x00076A95
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x000788C8 File Offset: 0x00076AC8
		public override TextAlignments TextAlign
		{
			get
			{
				if (this.m_textAlign == null)
				{
					this.m_textAlign = new TextAlignments?((TextAlignments)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.TextAlign));
				}
				return this.m_textAlign.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.TextAlign.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.TextAlign);
				this.m_textAlign = new TextAlignments?(value);
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00078938 File Offset: 0x00076B38
		internal bool IsTextAlignAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.TextAlign);
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x00078951 File Offset: 0x00076B51
		// (set) Token: 0x06001F92 RID: 8082 RVA: 0x00078984 File Offset: 0x00076B84
		public override VerticalAlignments VerticalAlign
		{
			get
			{
				if (this.m_verticalAlign == null)
				{
					this.m_verticalAlign = new VerticalAlignments?((VerticalAlignments)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.VerticalAlign));
				}
				return this.m_verticalAlign.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.VerticalAlign.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.VerticalAlign);
				this.m_verticalAlign = new VerticalAlignments?(value);
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000789F4 File Offset: 0x00076BF4
		internal bool IsVerticalAlignAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.VerticalAlign);
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00078A0D File Offset: 0x00076C0D
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x00078A40 File Offset: 0x00076C40
		public override Directions Direction
		{
			get
			{
				if (this.m_direction == null)
				{
					this.m_direction = new Directions?((Directions)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.Direction));
				}
				return this.m_direction.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.Direction.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.Direction);
				this.m_direction = new Directions?(value);
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00078AB0 File Offset: 0x00076CB0
		internal bool IsDirectionAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.Direction);
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x00078AC9 File Offset: 0x00076CC9
		// (set) Token: 0x06001F98 RID: 8088 RVA: 0x00078AFC File Offset: 0x00076CFC
		public override WritingModes WritingMode
		{
			get
			{
				if (this.m_writingMode == null)
				{
					this.m_writingMode = new WritingModes?((WritingModes)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.WritingMode));
				}
				return this.m_writingMode.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.WritingMode.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.WritingMode);
				this.m_writingMode = new WritingModes?(value);
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x00078B6C File Offset: 0x00076D6C
		internal bool IsWritingModeAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.WritingMode);
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00078B85 File Offset: 0x00076D85
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x00078BA8 File Offset: 0x00076DA8
		public override string Language
		{
			get
			{
				if (this.m_language == null)
				{
					this.m_language = this.m_styleDefinition.EvaluateInstanceStyleString(StyleAttributeNames.Language);
				}
				return this.m_language;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.Language.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.Language);
				this.m_language = value;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x00078C13 File Offset: 0x00076E13
		internal bool IsLanguageAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.Language);
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x00078C2C File Offset: 0x00076E2C
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x00078C60 File Offset: 0x00076E60
		public override UnicodeBiDiTypes UnicodeBiDi
		{
			get
			{
				if (this.m_unicodeBiDi == null)
				{
					this.m_unicodeBiDi = new UnicodeBiDiTypes?((UnicodeBiDiTypes)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.UnicodeBiDi));
				}
				return this.m_unicodeBiDi.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.UnicodeBiDi.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.UnicodeBiDi);
				this.m_unicodeBiDi = new UnicodeBiDiTypes?(value);
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x00078CD0 File Offset: 0x00076ED0
		internal bool IsUnicodeBiDiAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.UnicodeBiDi);
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x00078CE9 File Offset: 0x00076EE9
		// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x00078D1C File Offset: 0x00076F1C
		public override Calendars Calendar
		{
			get
			{
				if (this.m_calendar == null)
				{
					this.m_calendar = new Calendars?((Calendars)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.Calendar));
				}
				return this.m_calendar.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.Calendar.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.Calendar);
				this.m_calendar = new Calendars?(value);
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x00078D8C File Offset: 0x00076F8C
		internal bool IsCalendarAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.Calendar);
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x00078DA5 File Offset: 0x00076FA5
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00078DC8 File Offset: 0x00076FC8
		public override string CurrencyLanguage
		{
			get
			{
				if (this.m_currencyLanguage == null)
				{
					this.m_currencyLanguage = this.m_styleDefinition.EvaluateInstanceStyleString(StyleAttributeNames.CurrencyLanguage);
				}
				return this.m_currencyLanguage;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.CurrencyLanguage.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.CurrencyLanguage);
				this.m_currencyLanguage = value;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x00078E33 File Offset: 0x00077033
		internal bool IsCurrencyLanguageAssigned
		{
			get
			{
				return this.m_currencyLanguage != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.CurrencyLanguage);
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x00078E4C File Offset: 0x0007704C
		// (set) Token: 0x06001FA7 RID: 8103 RVA: 0x00078E70 File Offset: 0x00077070
		public override string NumeralLanguage
		{
			get
			{
				if (this.m_numeralLanguage == null)
				{
					this.m_numeralLanguage = this.m_styleDefinition.EvaluateInstanceStyleString(StyleAttributeNames.NumeralLanguage);
				}
				return this.m_numeralLanguage;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.NumeralLanguage.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.NumeralLanguage);
				this.m_numeralLanguage = value;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x00078EDB File Offset: 0x000770DB
		internal bool IsNumeralLanguageAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.NumeralLanguage);
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00078EF4 File Offset: 0x000770F4
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x00078F28 File Offset: 0x00077128
		public override BackgroundGradients BackgroundGradientType
		{
			get
			{
				if (this.m_backgroundGradientType == null)
				{
					this.m_backgroundGradientType = new BackgroundGradients?((BackgroundGradients)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.BackgroundGradientType));
				}
				return this.m_backgroundGradientType.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.BackgroundGradientType.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.BackgroundGradientType);
				this.m_backgroundGradientType = new BackgroundGradients?(value);
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00078F98 File Offset: 0x00077198
		internal bool IsBackgroundGradientTypeAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.BackgroundGradientType);
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x00078FB1 File Offset: 0x000771B1
		// (set) Token: 0x06001FAD RID: 8109 RVA: 0x00078FD4 File Offset: 0x000771D4
		public override ReportSize FontSize
		{
			get
			{
				if (this.m_fontSize == null)
				{
					this.m_fontSize = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.FontSize);
				}
				return this.m_fontSize;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.FontSize.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.FontSize);
				this.m_fontSize = value;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x0007903F File Offset: 0x0007723F
		internal bool IsFontSizeAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.FontSize);
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00079058 File Offset: 0x00077258
		// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x0007907C File Offset: 0x0007727C
		public override ReportSize PaddingLeft
		{
			get
			{
				if (this.m_paddingLeft == null)
				{
					this.m_paddingLeft = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.PaddingLeft);
				}
				return this.m_paddingLeft;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.PaddingLeft.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.PaddingLeft);
				this.m_paddingLeft = value;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x000790E7 File Offset: 0x000772E7
		internal bool IsPaddingLeftAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.PaddingLeft);
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x00079100 File Offset: 0x00077300
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x00079124 File Offset: 0x00077324
		public override ReportSize PaddingRight
		{
			get
			{
				if (this.m_paddingRight == null)
				{
					this.m_paddingRight = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.PaddingRight);
				}
				return this.m_paddingRight;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.PaddingRight.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.PaddingRight);
				this.m_paddingRight = value;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0007918F File Offset: 0x0007738F
		internal bool IsPaddingRightAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.PaddingRight);
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000791A8 File Offset: 0x000773A8
		// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x000791CC File Offset: 0x000773CC
		public override ReportSize PaddingTop
		{
			get
			{
				if (this.m_paddingTop == null)
				{
					this.m_paddingTop = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.PaddingTop);
				}
				return this.m_paddingTop;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.PaddingTop.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.PaddingTop);
				this.m_paddingTop = value;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00079237 File Offset: 0x00077437
		internal bool IsPaddingTopAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.PaddingTop);
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00079250 File Offset: 0x00077450
		// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x00079274 File Offset: 0x00077474
		public override ReportSize PaddingBottom
		{
			get
			{
				if (this.m_paddingBottom == null)
				{
					this.m_paddingBottom = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.PaddingBottom);
				}
				return this.m_paddingBottom;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.PaddingBottom.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.PaddingBottom);
				this.m_paddingBottom = value;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x000792DF File Offset: 0x000774DF
		internal bool IsPaddingBottomAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.PaddingBottom);
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x000792F8 File Offset: 0x000774F8
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x0007931C File Offset: 0x0007751C
		public override ReportSize LineHeight
		{
			get
			{
				if (this.m_lineHeight == null)
				{
					this.m_lineHeight = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.LineHeight);
				}
				return this.m_lineHeight;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.LineHeight.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.LineHeight);
				this.m_lineHeight = value;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x00079387 File Offset: 0x00077587
		internal bool IsLineHeightAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.LineHeight);
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x000793A0 File Offset: 0x000775A0
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x000793C8 File Offset: 0x000775C8
		public override int NumeralVariant
		{
			get
			{
				if (this.m_numeralVariant == -1)
				{
					this.m_numeralVariant = this.m_styleDefinition.EvaluateInstanceStyleInt(StyleAttributeNames.NumeralVariant, 1);
				}
				return this.m_numeralVariant;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.NumeralVariant.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.NumeralVariant);
				this.m_numeralVariant = value;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x00079433 File Offset: 0x00077633
		internal bool IsNumeralVariantAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.NumeralVariant);
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0007944C File Offset: 0x0007764C
		// (set) Token: 0x06001FC2 RID: 8130 RVA: 0x00079480 File Offset: 0x00077680
		public override TextEffects TextEffect
		{
			get
			{
				if (this.m_textEffect == null)
				{
					this.m_textEffect = new TextEffects?((TextEffects)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.TextEffect));
				}
				return this.m_textEffect.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.NumeralVariant.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.TextEffect);
				this.m_textEffect = new TextEffects?(value);
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x000794F0 File Offset: 0x000776F0
		internal bool IsTextEffectAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.TextEffect);
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00079509 File Offset: 0x00077709
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x0007953C File Offset: 0x0007773C
		public override BackgroundHatchTypes BackgroundHatchType
		{
			get
			{
				if (this.m_backgroundHatchType == null)
				{
					this.m_backgroundHatchType = new BackgroundHatchTypes?((BackgroundHatchTypes)this.m_styleDefinition.EvaluateInstanceStyleEnum(StyleAttributeNames.BackgroundHatchType));
				}
				return this.m_backgroundHatchType.Value;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.NumeralVariant.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.BackgroundHatchType);
				this.m_backgroundHatchType = new BackgroundHatchTypes?(value);
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000795AC File Offset: 0x000777AC
		internal bool IsBackgroundHatchTypeAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.BackgroundHatchType);
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x000795C5 File Offset: 0x000777C5
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x000795E8 File Offset: 0x000777E8
		public override ReportColor ShadowColor
		{
			get
			{
				if (this.m_shadowColor == null)
				{
					this.m_shadowColor = this.m_styleDefinition.EvaluateInstanceReportColor(StyleAttributeNames.ShadowColor);
				}
				return this.m_shadowColor;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.NumeralVariant.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.ShadowColor);
				this.m_shadowColor = value;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x00079653 File Offset: 0x00077853
		internal bool IsShadowColorAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.ShadowColor);
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0007966C File Offset: 0x0007786C
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x00079690 File Offset: 0x00077890
		public override ReportSize ShadowOffset
		{
			get
			{
				if (this.m_shadowOffset == null)
				{
					this.m_shadowOffset = this.m_styleDefinition.EvaluateInstanceReportSize(StyleAttributeNames.ShadowOffset);
				}
				return this.m_shadowOffset;
			}
			set
			{
				if (this.m_styleDefinition.ReportElement == null || this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_styleDefinition.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_styleDefinition.NumeralVariant.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.AssignedValueTo(StyleAttributeNames.ShadowColor);
				this.m_shadowOffset = value;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000796FB File Offset: 0x000778FB
		internal bool IsShadowOffsetAssigned
		{
			get
			{
				return this.m_assignedValues != null && this.m_assignedValues.ContainsKey(StyleAttributeNames.ShadowOffset);
			}
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00079714 File Offset: 0x00077914
		protected override void ResetInstanceCache()
		{
			this.m_backgroundColor = null;
			this.m_backgroundGradientEndColor = null;
			this.m_color = null;
			this.m_fontStyle = null;
			this.m_fontFamily = null;
			this.m_fontWeight = null;
			this.m_format = null;
			this.m_textDecoration = null;
			this.m_textAlign = null;
			this.m_verticalAlign = null;
			this.m_direction = null;
			this.m_writingMode = null;
			this.m_language = null;
			this.m_unicodeBiDi = null;
			this.m_calendar = null;
			this.m_currencyLanguage = null;
			this.m_numeralLanguage = null;
			this.m_backgroundGradientType = null;
			this.m_fontSize = null;
			this.m_paddingLeft = null;
			this.m_paddingRight = null;
			this.m_paddingTop = null;
			this.m_paddingBottom = null;
			this.m_lineHeight = null;
			this.m_numeralVariant = -1;
			this.m_textEffect = null;
			this.m_backgroundHatchType = null;
			this.m_shadowColor = null;
			this.m_shadowOffset = null;
			this.m_assignedValues = null;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0007982F File Offset: 0x00077A2F
		private void AssignedValueTo(StyleAttributeNames styleName)
		{
			if (this.m_assignedValues == null)
			{
				this.m_assignedValues = new Dictionary<StyleAttributeNames, bool>();
			}
			if (!this.m_assignedValues.ContainsKey(styleName))
			{
				this.m_assignedValues.Add(styleName, true);
			}
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x00079860 File Offset: 0x00077A60
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(StyleInstance.m_Declaration);
			List<int> list;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo> list2;
			this.GetStyleDynamicValues(out list, out list2);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StyleAttributes)
				{
					if (memberName != MemberName.StyleAttributeValues)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo>(list2);
					}
				}
				else
				{
					writer.WriteListOfPrimitives<int>(list);
				}
			}
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000798CC File Offset: 0x00077ACC
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(StyleInstance.m_Declaration);
			List<int> list = null;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo> list2 = null;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StyleAttributes)
				{
					if (memberName != MemberName.StyleAttributeValues)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						list2 = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo>>();
					}
				}
				else
				{
					list = reader.ReadListOfPrimitives<int>();
				}
			}
			this.SetStyleDynamicValues(list, list2);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0007993A File Offset: 0x00077B3A
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x00079947 File Offset: 0x00077B47
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StyleInstance;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x00079950 File Offset: 0x00077B50
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StyleInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StyleAttributes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
				new MemberInfo(MemberName.StyleAttributeValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AttributeInfo)
			});
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0007999C File Offset: 0x00077B9C
		private void GetStyleDynamicValues(out List<int> styles, out List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo> values)
		{
			styles = new List<int>();
			values = new List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo>();
			this.m_styleDefinition.Border.Instance.GetAssignedDynamicValues(styles, values);
			this.m_styleDefinition.TopBorder.Instance.GetAssignedDynamicValues(styles, values);
			this.m_styleDefinition.BottomBorder.Instance.GetAssignedDynamicValues(styles, values);
			this.m_styleDefinition.LeftBorder.Instance.GetAssignedDynamicValues(styles, values);
			this.m_styleDefinition.RightBorder.Instance.GetAssignedDynamicValues(styles, values);
			if (this.IsBackgroundColorAssigned && this.m_styleDefinition.BackgroundColor.IsExpression)
			{
				styles.Add(15);
				values.Add(StyleInstance.CreateAttrInfo(this.m_backgroundColor));
			}
			if (this.IsBackgroundGradientEndColorAssigned && this.m_styleDefinition.BackgroundGradientEndColor.IsExpression)
			{
				styles.Add(38);
				values.Add(StyleInstance.CreateAttrInfo(this.m_backgroundGradientEndColor));
			}
			if (this.IsColorAssigned && this.m_styleDefinition.Color.IsExpression)
			{
				styles.Add(24);
				values.Add(StyleInstance.CreateAttrInfo(this.m_color));
			}
			if (this.IsFontStyleAssigned && this.m_styleDefinition.FontStyle.IsExpression)
			{
				styles.Add(16);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_fontStyle.Value));
			}
			if (this.IsFontFamilyAssigned && this.m_styleDefinition.FontFamily.IsExpression)
			{
				styles.Add(17);
				values.Add(StyleInstance.CreateAttrInfo(this.m_fontFamily));
			}
			if (this.IsFontWeightAssigned && this.m_styleDefinition.FontWeight.IsExpression)
			{
				styles.Add(19);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_fontWeight.Value));
			}
			if (this.IsFormatAssigned && this.m_styleDefinition.Format.IsExpression)
			{
				styles.Add(20);
				values.Add(StyleInstance.CreateAttrInfo(this.m_format));
			}
			if (this.IsTextDecorationAssigned && this.m_styleDefinition.TextDecoration.IsExpression)
			{
				styles.Add(21);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_textDecoration.Value));
			}
			if (this.IsTextAlignAssigned && this.m_styleDefinition.TextAlign.IsExpression)
			{
				styles.Add(22);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_textAlign.Value));
			}
			if (this.IsVerticalAlignAssigned && this.m_styleDefinition.VerticalAlign.IsExpression)
			{
				styles.Add(23);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_verticalAlign.Value));
			}
			if (this.IsDirectionAssigned && this.m_styleDefinition.Direction.IsExpression)
			{
				styles.Add(30);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_direction.Value));
			}
			if (this.IsWritingModeAssigned && this.m_styleDefinition.WritingMode.IsExpression)
			{
				styles.Add(31);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_writingMode.Value));
			}
			if (this.IsLanguageAssigned && this.m_styleDefinition.Language.IsExpression)
			{
				styles.Add(32);
				values.Add(StyleInstance.CreateAttrInfo(this.m_language));
			}
			if (this.IsUnicodeBiDiAssigned && this.m_styleDefinition.UnicodeBiDi.IsExpression)
			{
				styles.Add(33);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_unicodeBiDi.Value));
			}
			if (this.IsCalendarAssigned && this.m_styleDefinition.Calendar.IsExpression)
			{
				styles.Add(34);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_calendar.Value));
			}
			if (this.IsCurrencyLanguageAssigned && this.m_styleDefinition.CurrencyLanguage.IsExpression)
			{
				styles.Add(50);
				values.Add(StyleInstance.CreateAttrInfo(this.m_currencyLanguage));
			}
			if (this.IsNumeralLanguageAssigned && this.m_styleDefinition.NumeralLanguage.IsExpression)
			{
				styles.Add(35);
				values.Add(StyleInstance.CreateAttrInfo(this.m_numeralLanguage));
			}
			if (this.IsBackgroundGradientTypeAssigned && this.m_styleDefinition.BackgroundGradientType.IsExpression)
			{
				styles.Add(37);
				values.Add(StyleInstance.CreateAttrInfo((int)this.m_backgroundGradientType.Value));
			}
			if (this.IsFontSizeAssigned && this.m_styleDefinition.FontSize.IsExpression)
			{
				styles.Add(18);
				values.Add(StyleInstance.CreateAttrInfo(this.m_fontSize));
			}
			if (this.IsPaddingLeftAssigned && this.m_styleDefinition.PaddingLeft.IsExpression)
			{
				styles.Add(25);
				values.Add(StyleInstance.CreateAttrInfo(this.m_paddingLeft));
			}
			if (this.IsPaddingRightAssigned && this.m_styleDefinition.PaddingRight.IsExpression)
			{
				styles.Add(26);
				values.Add(StyleInstance.CreateAttrInfo(this.m_paddingRight));
			}
			if (this.IsPaddingTopAssigned && this.m_styleDefinition.PaddingTop.IsExpression)
			{
				styles.Add(27);
				values.Add(StyleInstance.CreateAttrInfo(this.m_paddingTop));
			}
			if (this.IsPaddingBottomAssigned && this.m_styleDefinition.PaddingBottom.IsExpression)
			{
				styles.Add(28);
				values.Add(StyleInstance.CreateAttrInfo(this.m_paddingBottom));
			}
			if (this.IsLineHeightAssigned && this.m_styleDefinition.LineHeight.IsExpression)
			{
				styles.Add(29);
				values.Add(StyleInstance.CreateAttrInfo(this.m_lineHeight));
			}
			if (this.IsNumeralVariantAssigned && this.m_styleDefinition.NumeralVariant.IsExpression)
			{
				styles.Add(36);
				values.Add(StyleInstance.CreateAttrInfo(this.m_numeralVariant));
			}
			Global.Tracer.Assert(styles.Count == values.Count, "styles.Count == values.Count");
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00079FB2 File Offset: 0x000781B2
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo CreateAttrInfo(ReportColor reportColor)
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo
			{
				IsExpression = true,
				Value = ((reportColor != null) ? reportColor.ToString() : null)
			};
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00079FD2 File Offset: 0x000781D2
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo CreateAttrInfo(ReportSize reportSize)
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo
			{
				IsExpression = true,
				Value = ((reportSize != null) ? reportSize.ToString() : null)
			};
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00079FF2 File Offset: 0x000781F2
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo CreateAttrInfo(string strValue)
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo
			{
				IsExpression = true,
				Value = ((strValue != null) ? strValue.ToString() : null)
			};
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0007A012 File Offset: 0x00078212
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo CreateAttrInfo(int intValue)
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo
			{
				IsExpression = true,
				IntValue = intValue
			};
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0007A028 File Offset: 0x00078228
		private void SetStyleDynamicValues(List<int> styles, List<Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo> values)
		{
			if (styles == null && values == null)
			{
				return;
			}
			Global.Tracer.Assert(styles != null && values != null && styles.Count == values.Count, "styles != null && values != null && styles.Count == values.Count");
			for (int i = 0; i < styles.Count; i++)
			{
				StyleAttributeNames styleAttributeNames = (StyleAttributeNames)styles[i];
				Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = values[i];
				switch (styleAttributeNames)
				{
				case StyleAttributeNames.BorderColor:
					this.m_styleDefinition.Border.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Color, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderColorTop:
					this.m_styleDefinition.TopBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Color, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderColorLeft:
					this.m_styleDefinition.LeftBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Color, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderColorRight:
					this.m_styleDefinition.RightBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Color, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderColorBottom:
					this.m_styleDefinition.BottomBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Color, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderStyle:
					this.m_styleDefinition.Border.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Style, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderStyleTop:
					this.m_styleDefinition.TopBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Style, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderStyleLeft:
					this.m_styleDefinition.LeftBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Style, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderStyleRight:
					this.m_styleDefinition.RightBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Style, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderStyleBottom:
					this.m_styleDefinition.BottomBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Style, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderWidth:
					this.m_styleDefinition.Border.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Width, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderWidthTop:
					this.m_styleDefinition.TopBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Width, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderWidthLeft:
					this.m_styleDefinition.LeftBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Width, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderWidthRight:
					this.m_styleDefinition.RightBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Width, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BorderWidthBottom:
					this.m_styleDefinition.BottomBorder.Instance.SetAssignedDynamicValue(BorderInstance.BorderStyleProperty.Width, attributeInfo, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.BackgroundColor:
					this.m_backgroundColor = new ReportColor(attributeInfo.Value, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.FontStyle:
					this.m_fontStyle = new FontStyles?((FontStyles)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.FontFamily:
					this.m_fontFamily = attributeInfo.Value;
					break;
				case StyleAttributeNames.FontSize:
					this.m_fontSize = new ReportSize(attributeInfo.Value);
					break;
				case StyleAttributeNames.FontWeight:
					this.m_fontWeight = new FontWeights?((FontWeights)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.Format:
					this.m_format = attributeInfo.Value;
					break;
				case StyleAttributeNames.TextDecoration:
					this.m_textDecoration = new TextDecorations?((TextDecorations)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.TextAlign:
					this.m_textAlign = new TextAlignments?((TextAlignments)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.VerticalAlign:
					this.m_verticalAlign = new VerticalAlignments?((VerticalAlignments)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.Color:
					this.m_color = new ReportColor(attributeInfo.Value, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.PaddingLeft:
					this.m_paddingLeft = new ReportSize(attributeInfo.Value);
					break;
				case StyleAttributeNames.PaddingRight:
					this.m_paddingRight = new ReportSize(attributeInfo.Value);
					break;
				case StyleAttributeNames.PaddingTop:
					this.m_paddingTop = new ReportSize(attributeInfo.Value);
					break;
				case StyleAttributeNames.PaddingBottom:
					this.m_paddingBottom = new ReportSize(attributeInfo.Value);
					break;
				case StyleAttributeNames.LineHeight:
					this.m_lineHeight = new ReportSize(attributeInfo.Value);
					break;
				case StyleAttributeNames.Direction:
					this.m_direction = new Directions?((Directions)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.WritingMode:
					this.m_writingMode = new WritingModes?((WritingModes)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.Language:
					this.m_language = attributeInfo.Value;
					break;
				case StyleAttributeNames.UnicodeBiDi:
					this.m_unicodeBiDi = new UnicodeBiDiTypes?((UnicodeBiDiTypes)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.Calendar:
					this.m_calendar = new Calendars?((Calendars)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.NumeralLanguage:
					this.m_numeralLanguage = attributeInfo.Value;
					break;
				case StyleAttributeNames.NumeralVariant:
					this.m_numeralVariant = attributeInfo.IntValue;
					break;
				case StyleAttributeNames.BackgroundGradientType:
					this.m_backgroundGradientType = new BackgroundGradients?((BackgroundGradients)attributeInfo.IntValue);
					break;
				case StyleAttributeNames.BackgroundGradientEndColor:
					this.m_backgroundGradientEndColor = new ReportColor(attributeInfo.Value, this.m_styleDefinition.IsDynamicImageStyle);
					break;
				case StyleAttributeNames.CurrencyLanguage:
					this.m_currencyLanguage = attributeInfo.Value;
					break;
				}
			}
		}

		// Token: 0x04000FCC RID: 4044
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_styleDefinition;

		// Token: 0x04000FCD RID: 4045
		protected ReportColor m_backgroundColor;

		// Token: 0x04000FCE RID: 4046
		protected ReportColor m_backgroundGradientEndColor;

		// Token: 0x04000FCF RID: 4047
		protected ReportColor m_color;

		// Token: 0x04000FD0 RID: 4048
		protected FontStyles? m_fontStyle;

		// Token: 0x04000FD1 RID: 4049
		protected string m_fontFamily;

		// Token: 0x04000FD2 RID: 4050
		protected FontWeights? m_fontWeight;

		// Token: 0x04000FD3 RID: 4051
		protected string m_format;

		// Token: 0x04000FD4 RID: 4052
		protected TextDecorations? m_textDecoration;

		// Token: 0x04000FD5 RID: 4053
		protected TextAlignments? m_textAlign;

		// Token: 0x04000FD6 RID: 4054
		protected VerticalAlignments? m_verticalAlign;

		// Token: 0x04000FD7 RID: 4055
		protected Directions? m_direction;

		// Token: 0x04000FD8 RID: 4056
		protected WritingModes? m_writingMode;

		// Token: 0x04000FD9 RID: 4057
		protected string m_language;

		// Token: 0x04000FDA RID: 4058
		protected UnicodeBiDiTypes? m_unicodeBiDi;

		// Token: 0x04000FDB RID: 4059
		protected Calendars? m_calendar;

		// Token: 0x04000FDC RID: 4060
		protected string m_currencyLanguage;

		// Token: 0x04000FDD RID: 4061
		protected string m_numeralLanguage;

		// Token: 0x04000FDE RID: 4062
		protected BackgroundGradients? m_backgroundGradientType;

		// Token: 0x04000FDF RID: 4063
		protected ReportSize m_fontSize;

		// Token: 0x04000FE0 RID: 4064
		protected ReportSize m_paddingLeft;

		// Token: 0x04000FE1 RID: 4065
		protected ReportSize m_paddingRight;

		// Token: 0x04000FE2 RID: 4066
		protected ReportSize m_paddingTop;

		// Token: 0x04000FE3 RID: 4067
		protected ReportSize m_paddingBottom;

		// Token: 0x04000FE4 RID: 4068
		protected ReportSize m_lineHeight;

		// Token: 0x04000FE5 RID: 4069
		protected int m_numeralVariant;

		// Token: 0x04000FE6 RID: 4070
		protected TextEffects? m_textEffect;

		// Token: 0x04000FE7 RID: 4071
		protected BackgroundHatchTypes? m_backgroundHatchType;

		// Token: 0x04000FE8 RID: 4072
		protected ReportColor m_shadowColor;

		// Token: 0x04000FE9 RID: 4073
		protected ReportSize m_shadowOffset;

		// Token: 0x04000FEA RID: 4074
		protected Dictionary<StyleAttributeNames, bool> m_assignedValues;

		// Token: 0x04000FEB RID: 4075
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = StyleInstance.GetDeclaration();
	}
}
