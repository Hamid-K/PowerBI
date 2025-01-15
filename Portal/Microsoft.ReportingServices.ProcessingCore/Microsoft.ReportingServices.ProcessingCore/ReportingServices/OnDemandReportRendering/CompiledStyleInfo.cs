using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200020B RID: 523
	internal class CompiledStyleInfo
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0005141F File Offset: 0x0004F61F
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x00051427 File Offset: 0x0004F627
		internal HtmlElement.HtmlElementType ElementType
		{
			get
			{
				return this.m_elementType;
			}
			set
			{
				this.m_elementType = value;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00051430 File Offset: 0x0004F630
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x00051456 File Offset: 0x0004F656
		internal ReportColor Color
		{
			get
			{
				if (this.m_colorSet)
				{
					return this.m_color;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.Color;
				}
				return null;
			}
			set
			{
				this.m_colorSet = true;
				this.m_color = value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00051466 File Offset: 0x0004F666
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0005148C File Offset: 0x0004F68C
		internal FontStyles FontStyle
		{
			get
			{
				if (this.m_fontStyleSet)
				{
					return this.m_fontStyle;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.FontStyle;
				}
				return FontStyles.Default;
			}
			set
			{
				this.m_fontStyleSet = true;
				this.m_fontStyle = value;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0005149C File Offset: 0x0004F69C
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x000514C2 File Offset: 0x0004F6C2
		internal string FontFamily
		{
			get
			{
				if (this.m_fontFamilySet)
				{
					return this.m_fontFamily;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.FontFamily;
				}
				return null;
			}
			set
			{
				this.m_fontFamilySet = true;
				this.m_fontFamily = value;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000514D2 File Offset: 0x0004F6D2
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x000514F8 File Offset: 0x0004F6F8
		internal ReportSize FontSize
		{
			get
			{
				if (this.m_fontSizeSet)
				{
					return this.m_fontSize;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.FontSize;
				}
				return null;
			}
			set
			{
				this.m_fontSizeSet = true;
				this.m_fontSize = value;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00051508 File Offset: 0x0004F708
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0005152E File Offset: 0x0004F72E
		internal TextAlignments TextAlign
		{
			get
			{
				if (this.m_textAlignSet)
				{
					return this.m_textAlign;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.TextAlign;
				}
				return TextAlignments.Default;
			}
			set
			{
				this.m_textAlignSet = true;
				this.m_textAlign = value;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0005153E File Offset: 0x0004F73E
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00051564 File Offset: 0x0004F764
		internal FontWeights FontWeight
		{
			get
			{
				if (this.m_fontWeightSet)
				{
					return this.m_fontWeight;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.FontWeight;
				}
				return FontWeights.Default;
			}
			set
			{
				this.m_fontWeightSet = true;
				this.m_fontWeight = value;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00051574 File Offset: 0x0004F774
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x0005159A File Offset: 0x0004F79A
		internal TextDecorations TextDecoration
		{
			get
			{
				if (this.m_textDecorationSet)
				{
					return this.m_textDecoration;
				}
				if (this.m_parentStyle != null)
				{
					return this.m_parentStyle.TextDecoration;
				}
				return TextDecorations.Default;
			}
			set
			{
				this.m_textDecorationSet = true;
				this.m_textDecoration = value;
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x000515AC File Offset: 0x0004F7AC
		internal CompiledStyleInfo CreateChildStyle(HtmlElement.HtmlElementType elementType)
		{
			CompiledStyleInfo compiledStyleInfo = new CompiledStyleInfo();
			compiledStyleInfo.m_elementType = elementType;
			compiledStyleInfo.m_parentStyle = this;
			this.m_childStyle = compiledStyleInfo;
			return compiledStyleInfo;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000515D8 File Offset: 0x0004F7D8
		internal CompiledStyleInfo RemoveStyle(HtmlElement.HtmlElementType elementType)
		{
			if (this.m_elementType == elementType)
			{
				if (this.m_parentStyle != null)
				{
					this.m_parentStyle.m_childStyle = null;
					return this.m_parentStyle;
				}
				this.ResetStyle();
			}
			else if (this.m_parentStyle != null)
			{
				this.m_parentStyle.InternalRemoveStyle(elementType);
			}
			return this;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00051628 File Offset: 0x0004F828
		internal void InternalRemoveStyle(HtmlElement.HtmlElementType elementType)
		{
			if (this.m_elementType == elementType)
			{
				if (this.m_parentStyle != null)
				{
					this.m_parentStyle.m_childStyle = this.m_childStyle;
					this.m_childStyle.m_parentStyle = this.m_parentStyle;
					return;
				}
				if (this.m_parentStyle == null)
				{
					this.m_childStyle.m_parentStyle = null;
					return;
				}
			}
			else if (this.m_parentStyle != null)
			{
				this.m_parentStyle.InternalRemoveStyle(elementType);
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00051692 File Offset: 0x0004F892
		private void ResetStyle()
		{
			this.m_colorSet = false;
			this.m_fontFamilySet = false;
			this.m_fontSizeSet = false;
			this.m_fontStyleSet = false;
			this.m_fontWeightSet = false;
			this.m_textAlignSet = false;
			this.m_textDecorationSet = false;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000516C8 File Offset: 0x0004F8C8
		internal void PopulateStyleInstance(ICompiledStyleInstance styleInstance, bool isParagraphStyle)
		{
			if (isParagraphStyle)
			{
				TextAlignments textAlign = this.TextAlign;
				if (textAlign != TextAlignments.Default)
				{
					styleInstance.TextAlign = textAlign;
					return;
				}
			}
			else
			{
				ReportColor color = this.Color;
				if (color != null)
				{
					styleInstance.Color = color;
				}
				string fontFamily = this.FontFamily;
				if (!string.IsNullOrEmpty(fontFamily))
				{
					styleInstance.FontFamily = fontFamily;
				}
				ReportSize fontSize = this.FontSize;
				if (fontSize != null)
				{
					styleInstance.FontSize = fontSize;
				}
				FontStyles fontStyle = this.FontStyle;
				if (fontStyle != FontStyles.Default)
				{
					styleInstance.FontStyle = fontStyle;
				}
				FontWeights fontWeight = this.FontWeight;
				if (fontWeight != FontWeights.Default)
				{
					styleInstance.FontWeight = fontWeight;
				}
				TextDecorations textDecoration = this.TextDecoration;
				if (textDecoration != TextDecorations.Default)
				{
					styleInstance.TextDecoration = textDecoration;
				}
			}
		}

		// Token: 0x0400095C RID: 2396
		private HtmlElement.HtmlElementType m_elementType;

		// Token: 0x0400095D RID: 2397
		private ReportColor m_color;

		// Token: 0x0400095E RID: 2398
		private FontStyles m_fontStyle;

		// Token: 0x0400095F RID: 2399
		private string m_fontFamily;

		// Token: 0x04000960 RID: 2400
		private ReportSize m_fontSize;

		// Token: 0x04000961 RID: 2401
		private TextAlignments m_textAlign;

		// Token: 0x04000962 RID: 2402
		private TextDecorations m_textDecoration;

		// Token: 0x04000963 RID: 2403
		private FontWeights m_fontWeight;

		// Token: 0x04000964 RID: 2404
		private bool m_colorSet;

		// Token: 0x04000965 RID: 2405
		private bool m_fontStyleSet;

		// Token: 0x04000966 RID: 2406
		private bool m_fontFamilySet;

		// Token: 0x04000967 RID: 2407
		private bool m_fontSizeSet;

		// Token: 0x04000968 RID: 2408
		private bool m_textAlignSet;

		// Token: 0x04000969 RID: 2409
		private bool m_textDecorationSet;

		// Token: 0x0400096A RID: 2410
		private bool m_fontWeightSet;

		// Token: 0x0400096B RID: 2411
		private CompiledStyleInfo m_parentStyle;

		// Token: 0x0400096C RID: 2412
		private CompiledStyleInfo m_childStyle;
	}
}
