using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000344 RID: 836
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class StyleBase
	{
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0007AB94 File Offset: 0x00078D94
		internal static IEnumerable<StyleAttributeNames> StyleNames
		{
			get
			{
				int num;
				for (int i = 0; i < 51; i = num)
				{
					yield return (StyleAttributeNames)i;
					num = i + 1;
				}
				yield break;
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0007AB9D File Offset: 0x00078D9D
		internal StyleBase(RenderingContext context)
		{
			this.m_renderingContext = context;
		}

		// Token: 0x170011F1 RID: 4593
		public abstract ReportProperty this[StyleAttributeNames style] { get; }

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06001FEE RID: 8174
		public abstract List<StyleAttributeNames> SharedStyleAttributes { get; }

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06001FEF RID: 8175
		public abstract List<StyleAttributeNames> NonSharedStyleAttributes { get; }

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06001FF0 RID: 8176
		public abstract BackgroundImage BackgroundImage { get; }

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06001FF1 RID: 8177
		public abstract Border Border { get; }

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06001FF2 RID: 8178
		public abstract Border TopBorder { get; }

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06001FF3 RID: 8179
		public abstract Border LeftBorder { get; }

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06001FF4 RID: 8180
		public abstract Border RightBorder { get; }

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06001FF5 RID: 8181
		public abstract Border BottomBorder { get; }

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06001FF6 RID: 8182
		public abstract ReportColorProperty BackgroundGradientEndColor { get; }

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06001FF7 RID: 8183
		public abstract ReportColorProperty BackgroundColor { get; }

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06001FF8 RID: 8184
		public abstract ReportColorProperty Color { get; }

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06001FF9 RID: 8185
		public abstract ReportEnumProperty<FontStyles> FontStyle { get; }

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06001FFA RID: 8186
		public abstract ReportStringProperty FontFamily { get; }

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06001FFB RID: 8187
		public abstract ReportEnumProperty<FontWeights> FontWeight { get; }

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06001FFC RID: 8188
		public abstract ReportStringProperty Format { get; }

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06001FFD RID: 8189
		public abstract ReportEnumProperty<TextDecorations> TextDecoration { get; }

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06001FFE RID: 8190
		public abstract ReportEnumProperty<TextAlignments> TextAlign { get; }

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06001FFF RID: 8191
		public abstract ReportEnumProperty<VerticalAlignments> VerticalAlign { get; }

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06002000 RID: 8192
		public abstract ReportEnumProperty<Directions> Direction { get; }

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06002001 RID: 8193
		public abstract ReportEnumProperty<WritingModes> WritingMode { get; }

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06002002 RID: 8194
		public abstract ReportStringProperty Language { get; }

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06002003 RID: 8195
		public abstract ReportEnumProperty<UnicodeBiDiTypes> UnicodeBiDi { get; }

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06002004 RID: 8196
		public abstract ReportEnumProperty<Calendars> Calendar { get; }

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06002005 RID: 8197
		public abstract ReportStringProperty CurrencyLanguage { get; }

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06002006 RID: 8198
		public abstract ReportStringProperty NumeralLanguage { get; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06002007 RID: 8199
		public abstract ReportEnumProperty<BackgroundGradients> BackgroundGradientType { get; }

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06002008 RID: 8200
		public abstract ReportSizeProperty FontSize { get; }

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06002009 RID: 8201
		public abstract ReportSizeProperty PaddingLeft { get; }

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600200A RID: 8202
		public abstract ReportSizeProperty PaddingRight { get; }

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x0600200B RID: 8203
		public abstract ReportSizeProperty PaddingTop { get; }

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x0600200C RID: 8204
		public abstract ReportSizeProperty PaddingBottom { get; }

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x0600200D RID: 8205
		public abstract ReportSizeProperty LineHeight { get; }

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x0600200E RID: 8206
		public abstract ReportIntProperty NumeralVariant { get; }

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x0600200F RID: 8207
		public abstract ReportEnumProperty<TextEffects> TextEffect { get; }

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06002010 RID: 8208
		public abstract ReportEnumProperty<BackgroundHatchTypes> BackgroundHatchType { get; }

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06002011 RID: 8209
		public abstract ReportColorProperty ShadowColor { get; }

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06002012 RID: 8210
		public abstract ReportSizeProperty ShadowOffset { get; }

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x0007ABAC File Offset: 0x00078DAC
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x04000FF8 RID: 4088
		public const int StyleAttributeCount = 51;

		// Token: 0x04000FF9 RID: 4089
		protected const string cBorderColor = "BorderColor";

		// Token: 0x04000FFA RID: 4090
		protected const string cBorderColorLeft = "BorderColorLeft";

		// Token: 0x04000FFB RID: 4091
		protected const string cBorderColorRight = "BorderColorRight";

		// Token: 0x04000FFC RID: 4092
		protected const string cBorderColorTop = "BorderColorTop";

		// Token: 0x04000FFD RID: 4093
		protected const string cBorderColorBottom = "BorderColorBottom";

		// Token: 0x04000FFE RID: 4094
		protected const string cBorderStyle = "BorderStyle";

		// Token: 0x04000FFF RID: 4095
		protected const string cBorderStyleLeft = "BorderStyleLeft";

		// Token: 0x04001000 RID: 4096
		protected const string cBorderStyleRight = "BorderStyleRight";

		// Token: 0x04001001 RID: 4097
		protected const string cBorderStyleTop = "BorderStyleTop";

		// Token: 0x04001002 RID: 4098
		protected const string cBorderStyleBottom = "BorderStyleBottom";

		// Token: 0x04001003 RID: 4099
		protected const string cBorderWidth = "BorderWidth";

		// Token: 0x04001004 RID: 4100
		protected const string cBorderWidthLeft = "BorderWidthLeft";

		// Token: 0x04001005 RID: 4101
		protected const string cBorderWidthRight = "BorderWidthRight";

		// Token: 0x04001006 RID: 4102
		protected const string cBorderWidthTop = "BorderWidthTop";

		// Token: 0x04001007 RID: 4103
		protected const string cBorderWidthBottom = "BorderWidthBottom";

		// Token: 0x04001008 RID: 4104
		protected const string cBackgroundImage = "BackgroundImage";

		// Token: 0x04001009 RID: 4105
		protected const string cBackgroundImageSource = "BackgroundImageSource";

		// Token: 0x0400100A RID: 4106
		protected const string cBackgroundImageValue = "BackgroundImageValue";

		// Token: 0x0400100B RID: 4107
		protected const string cBackgroundImageMIMEType = "BackgroundImageMIMEType";

		// Token: 0x0400100C RID: 4108
		protected const string cBackgroundColor = "BackgroundColor";

		// Token: 0x0400100D RID: 4109
		protected const string cBackgroundGradientEndColor = "BackgroundGradientEndColor";

		// Token: 0x0400100E RID: 4110
		protected const string cBackgroundGradientType = "BackgroundGradientType";

		// Token: 0x0400100F RID: 4111
		protected const string cBackgroundRepeat = "BackgroundRepeat";

		// Token: 0x04001010 RID: 4112
		protected const string cFontStyle = "FontStyle";

		// Token: 0x04001011 RID: 4113
		protected const string cFontFamily = "FontFamily";

		// Token: 0x04001012 RID: 4114
		protected const string cFontSize = "FontSize";

		// Token: 0x04001013 RID: 4115
		protected const string cFontWeight = "FontWeight";

		// Token: 0x04001014 RID: 4116
		protected const string cFormat = "Format";

		// Token: 0x04001015 RID: 4117
		protected const string cTextDecoration = "TextDecoration";

		// Token: 0x04001016 RID: 4118
		protected const string cTextAlign = "TextAlign";

		// Token: 0x04001017 RID: 4119
		protected const string cVerticalAlign = "VerticalAlign";

		// Token: 0x04001018 RID: 4120
		protected const string cColor = "Color";

		// Token: 0x04001019 RID: 4121
		protected const string cPaddingLeft = "PaddingLeft";

		// Token: 0x0400101A RID: 4122
		protected const string cPaddingRight = "PaddingRight";

		// Token: 0x0400101B RID: 4123
		protected const string cPaddingTop = "PaddingTop";

		// Token: 0x0400101C RID: 4124
		protected const string cPaddingBottom = "PaddingBottom";

		// Token: 0x0400101D RID: 4125
		protected const string cLineHeight = "LineHeight";

		// Token: 0x0400101E RID: 4126
		protected const string cDirection = "Direction";

		// Token: 0x0400101F RID: 4127
		protected const string cWritingMode = "WritingMode";

		// Token: 0x04001020 RID: 4128
		protected const string cLanguage = "Language";

		// Token: 0x04001021 RID: 4129
		protected const string cUnicodeBiDi = "UnicodeBiDi";

		// Token: 0x04001022 RID: 4130
		protected const string cCalendar = "Calendar";

		// Token: 0x04001023 RID: 4131
		protected const string cCurrencyLanguage = "CurrencyLanguage";

		// Token: 0x04001024 RID: 4132
		protected const string cNumeralLanguage = "NumeralLanguage";

		// Token: 0x04001025 RID: 4133
		protected const string cNumeralVariant = "NumeralVariant";

		// Token: 0x04001026 RID: 4134
		protected const string cTextEffect = "TextEffect";

		// Token: 0x04001027 RID: 4135
		protected const string cBackgroundHatchType = "BackgroundHatchType";

		// Token: 0x04001028 RID: 4136
		protected const string cShadowColor = "ShadowColor";

		// Token: 0x04001029 RID: 4137
		protected const string cShadowOffset = "ShadowOffset";

		// Token: 0x0400102A RID: 4138
		protected const string cPosition = "Position";

		// Token: 0x0400102B RID: 4139
		protected const string cTransparentColor = "TransparentColor";

		// Token: 0x0400102C RID: 4140
		internal RenderingContext m_renderingContext;

		// Token: 0x0400102D RID: 4141
		protected List<StyleAttributeNames> m_sharedStyles;

		// Token: 0x0400102E RID: 4142
		protected List<StyleAttributeNames> m_nonSharedStyles;
	}
}
