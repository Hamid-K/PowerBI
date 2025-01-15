using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C8 RID: 200
	public class Style : ReportObject, IShouldSerialize
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001CE2C File Offset: 0x0001B02C
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0001CE3D File Offset: 0x0001B03D
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string FormatSymbolCulture
		{
			get
			{
				return this.m_formatSymbolCulture ?? string.Empty;
			}
			set
			{
				if (this.m_formatSymbolCulture != value)
				{
					this.m_formatSymbolCulture = value;
				}
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001CE54 File Offset: 0x0001B054
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0001CE68 File Offset: 0x0001B068
		public Border Border
		{
			get
			{
				return (Border)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value != null)
				{
					if (value.Color == ReportColor.Empty)
					{
						value.Color = Constants.DefaultBorderColor;
					}
					if (value.Width == ReportSize.Empty)
					{
						value.Width = Constants.DefaultBorderWidth;
					}
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001CEC9 File Offset: 0x0001B0C9
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		public Border TopBorder
		{
			get
			{
				return (Border)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001CEEB File Offset: 0x0001B0EB
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0001CEFE File Offset: 0x0001B0FE
		public Border BottomBorder
		{
			get
			{
				return (Border)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001CF0D File Offset: 0x0001B10D
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0001CF20 File Offset: 0x0001B120
		public Border LeftBorder
		{
			get
			{
				return (Border)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001CF2F File Offset: 0x0001B12F
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0001CF42 File Offset: 0x0001B142
		public Border RightBorder
		{
			get
			{
				return (Border)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001CF51 File Offset: 0x0001B151
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0001CF5F File Offset: 0x0001B15F
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> BackgroundColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001CF73 File Offset: 0x0001B173
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0001CF81 File Offset: 0x0001B181
		[ReportExpressionDefaultValue(typeof(BackgroundGradients), BackgroundGradients.Default)]
		public ReportExpression<BackgroundGradients> BackgroundGradientType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BackgroundGradients>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0001CF95 File Offset: 0x0001B195
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x0001CFA3 File Offset: 0x0001B1A3
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> BackgroundGradientEndColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001CFB7 File Offset: 0x0001B1B7
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0001CFCA File Offset: 0x0001B1CA
		public BackgroundImage BackgroundImage
		{
			get
			{
				return (BackgroundImage)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001CFD9 File Offset: 0x0001B1D9
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		[ReportExpressionDefaultValue(typeof(FontStyles), FontStyles.Default)]
		public ReportExpression<FontStyles> FontStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<FontStyles>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001D000 File Offset: 0x0001B200
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0001D053 File Offset: 0x0001B253
		[ReportExpressionDefaultValue("Arial")]
		public ReportExpression FontFamily
		{
			get
			{
				if (base.PropertyStore.ContainsObject(10))
				{
					return base.PropertyStore.GetObject<ReportExpression>(10);
				}
				Report ancestor = base.GetAncestor<Report>();
				if (ancestor != null)
				{
					return ancestor.DefaultFontFamily ?? "Arial";
				}
				return "Arial";
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001D068 File Offset: 0x0001B268
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0001D077 File Offset: 0x0001B277
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultFontSize")]
		public ReportExpression<ReportSize> FontSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001D08C File Offset: 0x0001B28C
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0001D09B File Offset: 0x0001B29B
		[ReportExpressionDefaultValue(typeof(FontWeights), FontWeights.Default)]
		public ReportExpression<FontWeights> FontWeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<FontWeights>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0001D0BF File Offset: 0x0001B2BF
		[ReportExpressionDefaultValue]
		public ReportExpression Format
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0001D0E3 File Offset: 0x0001B2E3
		[ReportExpressionDefaultValue(typeof(TextDecorations), TextDecorations.Default)]
		public ReportExpression<TextDecorations> TextDecoration
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextDecorations>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0001D107 File Offset: 0x0001B307
		[ReportExpressionDefaultValue(typeof(TextAlignments), TextAlignments.Default)]
		public ReportExpression<TextAlignments> TextAlign
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextAlignments>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001D11C File Offset: 0x0001B31C
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0001D12B File Offset: 0x0001B32B
		[ReportExpressionDefaultValue(typeof(VerticalAlignments), VerticalAlignments.Default)]
		public ReportExpression<VerticalAlignments> VerticalAlign
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<VerticalAlignments>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001D140 File Offset: 0x0001B340
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0001D14F File Offset: 0x0001B34F
		[ReportExpressionDefaultValueConstant(typeof(ReportColor), "DefaultColor")]
		public ReportExpression<ReportColor> Color
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001D164 File Offset: 0x0001B364
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0001D173 File Offset: 0x0001B373
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> PaddingLeft
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001D188 File Offset: 0x0001B388
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0001D197 File Offset: 0x0001B397
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> PaddingRight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0001D1AC File Offset: 0x0001B3AC
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0001D1BB File Offset: 0x0001B3BB
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> PaddingTop
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0001D1DF File Offset: 0x0001B3DF
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> PaddingBottom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001D1F4 File Offset: 0x0001B3F4
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0001D203 File Offset: 0x0001B403
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> LineHeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001D218 File Offset: 0x0001B418
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0001D227 File Offset: 0x0001B427
		[ReportExpressionDefaultValue(typeof(TextDirections), TextDirections.Default)]
		public ReportExpression<TextDirections> Direction
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextDirections>>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001D23C File Offset: 0x0001B43C
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0001D24B File Offset: 0x0001B44B
		[ReportExpressionDefaultValue(typeof(WritingModes), WritingModes.Default)]
		public ReportExpression<WritingModes> WritingMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<WritingModes>>(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001D260 File Offset: 0x0001B460
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0001D26F File Offset: 0x0001B46F
		[ReportExpressionDefaultValue]
		public ReportExpression Language
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001D284 File Offset: 0x0001B484
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0001D293 File Offset: 0x0001B493
		[ReportExpressionDefaultValue(typeof(UnicodeBiDiTypes), UnicodeBiDiTypes.Normal)]
		public ReportExpression<UnicodeBiDiTypes> UnicodeBiDi
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<UnicodeBiDiTypes>>(26);
			}
			set
			{
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x0001D2B7 File Offset: 0x0001B4B7
		[ReportExpressionDefaultValue(typeof(Calendars), Calendars.Default)]
		public ReportExpression<Calendars> Calendar
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Calendars>>(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0001D2DB File Offset: 0x0001B4DB
		[ReportExpressionDefaultValue]
		public ReportExpression NumeralLanguage
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0001D2FF File Offset: 0x0001B4FF
		[ReportExpressionDefaultValue(typeof(int), 1)]
		[ValidValues(1, 7)]
		public ReportExpression<int> NumeralVariant
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(29);
			}
			set
			{
				DefinitionStore<Style, Style.Definition.Properties>.GetProperty(29).Validate(this, value);
				base.PropertyStore.SetObject(29, value);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0001D327 File Offset: 0x0001B527
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x0001D336 File Offset: 0x0001B536
		[ReportExpressionDefaultValue(typeof(TextEffects), TextEffects.Default)]
		public ReportExpression<TextEffects> TextEffect
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextEffects>>(30);
			}
			set
			{
				base.PropertyStore.SetObject(30, value);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0001D34B File Offset: 0x0001B54B
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0001D35A File Offset: 0x0001B55A
		[ReportExpressionDefaultValue(typeof(BackgroundHatchTypes), BackgroundHatchTypes.Default)]
		public ReportExpression<BackgroundHatchTypes> BackgroundHatchType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BackgroundHatchTypes>>(31);
			}
			set
			{
				base.PropertyStore.SetObject(31, value);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0001D36F File Offset: 0x0001B56F
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0001D37E File Offset: 0x0001B57E
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> ShadowColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(32);
			}
			set
			{
				base.PropertyStore.SetObject(32, value);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001D393 File Offset: 0x0001B593
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0001D3A2 File Offset: 0x0001B5A2
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> ShadowOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(33);
			}
			set
			{
				base.PropertyStore.SetObject(33, value);
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001D3B7 File Offset: 0x0001B5B7
		public Style()
		{
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001D3BF File Offset: 0x0001B5BF
		public override void Initialize()
		{
			base.Initialize();
			this.FontSize = Constants.DefaultFontSize;
			this.Color = Constants.DefaultColor;
			this.NumeralVariant = 1;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D3F3 File Offset: 0x0001B5F3
		internal Style(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001D400 File Offset: 0x0001B600
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
		{
			if (!(property == "FontFamily") || this.FontFamily.IsExpression)
			{
				return SerializationMethod.Auto;
			}
			Report ancestor = base.GetAncestor<Report>();
			string text = "Arial";
			if (ancestor != null)
			{
				text = ancestor.DefaultFontFamily ?? "Arial";
			}
			if (this.FontFamily.Value == text)
			{
				return SerializationMethod.Never;
			}
			return SerializationMethod.Always;
		}

		// Token: 0x0400017C RID: 380
		private string m_formatSymbolCulture;

		// Token: 0x02000372 RID: 882
		internal class Definition : DefinitionStore<Style, Style.Definition.Properties>
		{
			// Token: 0x06001807 RID: 6151 RVA: 0x0003B254 File Offset: 0x00039454
			private Definition()
			{
			}

			// Token: 0x0200048D RID: 1165
			internal enum Properties
			{
				// Token: 0x04000B49 RID: 2889
				Border,
				// Token: 0x04000B4A RID: 2890
				TopBorder,
				// Token: 0x04000B4B RID: 2891
				BottomBorder,
				// Token: 0x04000B4C RID: 2892
				LeftBorder,
				// Token: 0x04000B4D RID: 2893
				RightBorder,
				// Token: 0x04000B4E RID: 2894
				BackgroundColor,
				// Token: 0x04000B4F RID: 2895
				BackgroundGradientType,
				// Token: 0x04000B50 RID: 2896
				BackgroundGradientEndColor,
				// Token: 0x04000B51 RID: 2897
				BackgroundImage,
				// Token: 0x04000B52 RID: 2898
				FontStyle,
				// Token: 0x04000B53 RID: 2899
				FontFamily,
				// Token: 0x04000B54 RID: 2900
				FontSize,
				// Token: 0x04000B55 RID: 2901
				FontWeight,
				// Token: 0x04000B56 RID: 2902
				Format,
				// Token: 0x04000B57 RID: 2903
				TextDecoration,
				// Token: 0x04000B58 RID: 2904
				TextAlign,
				// Token: 0x04000B59 RID: 2905
				VerticalAlign,
				// Token: 0x04000B5A RID: 2906
				Color,
				// Token: 0x04000B5B RID: 2907
				PaddingLeft,
				// Token: 0x04000B5C RID: 2908
				PaddingRight,
				// Token: 0x04000B5D RID: 2909
				PaddingTop,
				// Token: 0x04000B5E RID: 2910
				PaddingBottom,
				// Token: 0x04000B5F RID: 2911
				LineHeight,
				// Token: 0x04000B60 RID: 2912
				Direction,
				// Token: 0x04000B61 RID: 2913
				WritingMode,
				// Token: 0x04000B62 RID: 2914
				Language,
				// Token: 0x04000B63 RID: 2915
				UnicodeBiDi,
				// Token: 0x04000B64 RID: 2916
				Calendar,
				// Token: 0x04000B65 RID: 2917
				NumeralLanguage,
				// Token: 0x04000B66 RID: 2918
				NumeralVariant,
				// Token: 0x04000B67 RID: 2919
				TextEffect,
				// Token: 0x04000B68 RID: 2920
				BackgroundHatchType,
				// Token: 0x04000B69 RID: 2921
				ShadowColor,
				// Token: 0x04000B6A RID: 2922
				ShadowOffset,
				// Token: 0x04000B6B RID: 2923
				PropertyCount
			}
		}
	}
}
