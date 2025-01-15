using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000201 RID: 513
	internal sealed class HtmlParser : RichTextParser
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x0004FB92 File Offset: 0x0004DD92
		internal HtmlParser(bool multipleParagraphsAllowed, IRichTextInstanceCreator iRichTextInstanceCreator, IRichTextLogger richTextLogger)
			: base(multipleParagraphsAllowed, iRichTextInstanceCreator, richTextLogger)
		{
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004FBA0 File Offset: 0x0004DDA0
		private string HtmlTrimStart(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (!char.IsWhiteSpace(c) || c == '\u00a0')
				{
					return input.Substring(i);
				}
			}
			return string.Empty;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004FBE4 File Offset: 0x0004DDE4
		protected override void InternalParse(string richText)
		{
			this.m_htmlLexer = new HtmlLexer(richText);
			int num = 0;
			FunctionalList<ListStyle> functionalList = FunctionalList<ListStyle>.Empty;
			HtmlElement.HtmlNodeType htmlNodeType = HtmlElement.HtmlNodeType.Element;
			HtmlElement.HtmlElementType htmlElementType = HtmlElement.HtmlElementType.None;
			while (this.m_htmlLexer.Read())
			{
				this.m_currentHtmlElement = this.m_htmlLexer.CurrentElement;
				HtmlElement.HtmlElementType htmlElementType2 = this.m_currentHtmlElement.ElementType;
				HtmlElement.HtmlNodeType htmlNodeType2 = this.m_currentHtmlElement.NodeType;
				switch (htmlNodeType2)
				{
				case HtmlElement.HtmlNodeType.Element:
					if (num == 0 || htmlElementType2 == HtmlElement.HtmlElementType.TITLE)
					{
						switch (htmlElementType2)
						{
						case HtmlElement.HtmlElementType.P:
						case HtmlElement.HtmlElementType.DIV:
						case HtmlElement.HtmlElementType.LI:
						case HtmlElement.HtmlElementType.H1:
						case HtmlElement.HtmlElementType.H2:
						case HtmlElement.HtmlElementType.H3:
						case HtmlElement.HtmlElementType.H4:
						case HtmlElement.HtmlElementType.H5:
						case HtmlElement.HtmlElementType.H6:
							this.ParseParagraphElement(htmlElementType2, functionalList);
							goto IL_0345;
						case HtmlElement.HtmlElementType.BR:
							if (htmlNodeType != HtmlElement.HtmlNodeType.EndElement)
							{
								this.AppendText(Environment.NewLine);
								goto IL_0345;
							}
							this.SetTextRunValue(Environment.NewLine);
							goto IL_0345;
						case HtmlElement.HtmlElementType.UL:
						case HtmlElement.HtmlElementType.OL:
						{
							this.FlushPendingLI();
							this.CloseParagraph();
							ListStyle listStyleForElement = this.GetListStyleForElement(htmlElementType2);
							functionalList = functionalList.Add(listStyleForElement);
							this.m_currentParagraph.ListLevel = functionalList.Count;
							goto IL_0345;
						}
						case HtmlElement.HtmlElementType.SPAN:
						case HtmlElement.HtmlElementType.FONT:
						case HtmlElement.HtmlElementType.STRONG:
						case HtmlElement.HtmlElementType.STRIKE:
						case HtmlElement.HtmlElementType.B:
						case HtmlElement.HtmlElementType.I:
						case HtmlElement.HtmlElementType.U:
						case HtmlElement.HtmlElementType.S:
						case HtmlElement.HtmlElementType.EM:
							this.ParseTextRunElement(htmlElementType2);
							goto IL_0345;
						case HtmlElement.HtmlElementType.A:
							this.ParseActionElement(functionalList.Count);
							goto IL_0345;
						case HtmlElement.HtmlElementType.TITLE:
							if (!this.m_currentHtmlElement.IsEmptyElement)
							{
								num++;
							}
							htmlElementType2 = htmlElementType;
							htmlNodeType2 = htmlNodeType;
							goto IL_0345;
						}
						htmlElementType2 = htmlElementType;
						htmlNodeType2 = htmlNodeType;
					}
					break;
				case HtmlElement.HtmlNodeType.EndElement:
					if (num == 0 || htmlElementType2 == HtmlElement.HtmlElementType.TITLE)
					{
						switch (htmlElementType2)
						{
						case HtmlElement.HtmlElementType.P:
						case HtmlElement.HtmlElementType.DIV:
						case HtmlElement.HtmlElementType.H1:
						case HtmlElement.HtmlElementType.H2:
						case HtmlElement.HtmlElementType.H3:
						case HtmlElement.HtmlElementType.H4:
						case HtmlElement.HtmlElementType.H5:
						case HtmlElement.HtmlElementType.H6:
							this.CloseParagraph();
							this.m_currentParagraph = this.m_currentParagraph.RemoveParagraph(htmlElementType2);
							break;
						case HtmlElement.HtmlElementType.BR:
						case HtmlElement.HtmlElementType.DD:
						case HtmlElement.HtmlElementType.DT:
						case HtmlElement.HtmlElementType.BLOCKQUOTE:
							goto IL_033F;
						case HtmlElement.HtmlElementType.UL:
						case HtmlElement.HtmlElementType.OL:
						{
							this.FlushPendingLI();
							this.CloseParagraph();
							if (functionalList.Count <= 0)
							{
								goto IL_0345;
							}
							ListStyle listStyleForElement2 = this.GetListStyleForElement(htmlElementType2);
							FunctionalList<ListStyle> functionalList2 = functionalList;
							bool flag;
							do
							{
								flag = functionalList2.First == listStyleForElement2;
								functionalList2 = functionalList2.Rest;
							}
							while (!flag && functionalList2.Count > 0);
							if (flag)
							{
								functionalList = functionalList2;
								this.m_currentParagraph.ListLevel = functionalList.Count;
								goto IL_0345;
							}
							goto IL_0345;
						}
						case HtmlElement.HtmlElementType.LI:
							this.CloseParagraph();
							goto IL_0345;
						case HtmlElement.HtmlElementType.SPAN:
						case HtmlElement.HtmlElementType.FONT:
						case HtmlElement.HtmlElementType.STRONG:
						case HtmlElement.HtmlElementType.STRIKE:
						case HtmlElement.HtmlElementType.B:
						case HtmlElement.HtmlElementType.I:
						case HtmlElement.HtmlElementType.U:
						case HtmlElement.HtmlElementType.S:
						case HtmlElement.HtmlElementType.EM:
							break;
						case HtmlElement.HtmlElementType.A:
							this.RevertActionElement(htmlElementType2);
							goto IL_0345;
						case HtmlElement.HtmlElementType.TITLE:
							if (num > 0)
							{
								num--;
							}
							htmlElementType2 = htmlElementType;
							htmlNodeType2 = htmlNodeType;
							goto IL_0345;
						default:
							goto IL_033F;
						}
						this.m_currentStyle = this.m_currentStyle.RemoveStyle(htmlElementType2);
						break;
						IL_033F:
						htmlElementType2 = htmlElementType;
						htmlNodeType2 = htmlNodeType;
					}
					break;
				case HtmlElement.HtmlNodeType.Text:
					if (num == 0)
					{
						string text = this.m_currentHtmlElement.Value;
						if (htmlNodeType == HtmlElement.HtmlNodeType.Text)
						{
							this.AppendText(text);
						}
						else if (htmlElementType == HtmlElement.HtmlElementType.BR)
						{
							this.AppendText(this.HtmlTrimStart(text));
						}
						else
						{
							if (this.m_currentParagraphInstance == null)
							{
								text = this.HtmlTrimStart(text);
							}
							if (!string.IsNullOrEmpty(text))
							{
								this.SetTextRunValue(text);
							}
							else
							{
								htmlElementType2 = htmlElementType;
								htmlNodeType2 = htmlNodeType;
							}
						}
					}
					break;
				}
				IL_0345:
				htmlNodeType = htmlNodeType2;
				htmlElementType = htmlElementType2;
			}
			if (this.m_paragraphInstanceCollection.Count == 0)
			{
				this.CreateTextRunInstance();
			}
			this.m_currentParagraph = this.m_currentParagraph.RemoveAll();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004FF71 File Offset: 0x0004E171
		private ListStyle GetListStyleForElement(HtmlElement.HtmlElementType elementType)
		{
			if (elementType == HtmlElement.HtmlElementType.OL)
			{
				return ListStyle.Numbered;
			}
			return ListStyle.Bulleted;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004FF7C File Offset: 0x0004E17C
		private void ParseParagraphElement(HtmlElement.HtmlElementType elementType, FunctionalList<ListStyle> listStyles)
		{
			this.CloseParagraph();
			if (this.m_currentParagraph.ElementType == HtmlElement.HtmlElementType.P)
			{
				this.m_currentParagraph = this.m_currentParagraph.RemoveParagraph(HtmlElement.HtmlElementType.P);
				this.m_currentStyle = this.m_currentStyle.RemoveStyle(HtmlElement.HtmlElementType.P);
			}
			if (elementType == HtmlElement.HtmlElementType.LI)
			{
				this.FlushPendingLI();
				if (listStyles.Count > 0)
				{
					this.m_currentParagraph.ListStyle = listStyles.First;
				}
				else
				{
					this.m_currentParagraph.ListStyle = ListStyle.Bulleted;
				}
			}
			else
			{
				this.m_currentStyle = this.m_currentStyle.CreateChildStyle(elementType);
				this.m_currentParagraph = this.m_currentParagraph.CreateChildParagraph(elementType);
				if (elementType != HtmlElement.HtmlElementType.P)
				{
					if (elementType != HtmlElement.HtmlElementType.DIV)
					{
						switch (elementType)
						{
						case HtmlElement.HtmlElementType.H1:
							this.m_currentStyle.FontSize = HtmlParser.StyleDefaults.H1FontSize;
							this.m_currentStyle.FontWeight = FontWeights.Bold;
							this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.H1Margin);
							break;
						case HtmlElement.HtmlElementType.H2:
							this.m_currentStyle.FontSize = HtmlParser.StyleDefaults.H2FontSize;
							this.m_currentStyle.FontWeight = FontWeights.Bold;
							this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.H2Margin);
							break;
						case HtmlElement.HtmlElementType.H3:
							this.m_currentStyle.FontSize = HtmlParser.StyleDefaults.H3FontSize;
							this.m_currentStyle.FontWeight = FontWeights.Bold;
							this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.H3Margin);
							break;
						case HtmlElement.HtmlElementType.H4:
							this.m_currentStyle.FontSize = HtmlParser.StyleDefaults.H4FontSize;
							this.m_currentStyle.FontWeight = FontWeights.Bold;
							this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.H4Margin);
							break;
						case HtmlElement.HtmlElementType.H5:
							this.m_currentStyle.FontSize = HtmlParser.StyleDefaults.H5FontSize;
							this.m_currentStyle.FontWeight = FontWeights.Bold;
							this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.H5Margin);
							break;
						case HtmlElement.HtmlElementType.H6:
							this.m_currentStyle.FontSize = HtmlParser.StyleDefaults.H6FontSize;
							this.m_currentStyle.FontWeight = FontWeights.Bold;
							this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.H6Margin);
							break;
						}
					}
				}
				else
				{
					this.SetMarginTopAndBottom(HtmlParser.StyleDefaults.PMargin);
				}
				string text;
				if (!this.m_currentHtmlElement.IsEmptyElement && this.m_currentHtmlElement.HasAttributes && this.m_allowMultipleParagraphs && this.m_currentHtmlElement.Attributes.TryGetValue("align", out text))
				{
					TextAlignments textAlignments;
					if (RichTextStyleTranslator.TranslateTextAlign(text, out textAlignments))
					{
						this.m_currentStyle.TextAlign = textAlignments;
					}
					else
					{
						this.m_richTextLogger.RegisterInvalidValueWarning("align", text, this.m_currentHtmlElement.CharacterPosition);
					}
				}
			}
			this.SetStyleValues(true);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000501D4 File Offset: 0x0004E3D4
		private void FlushPendingLI()
		{
			if (this.m_allowMultipleParagraphs && this.m_currentParagraph.ListStyle != ListStyle.None)
			{
				this.CreateParagraphInstance();
				this.CloseParagraph();
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000501F8 File Offset: 0x0004E3F8
		private void SetMarginTopAndBottom(ReportSize marginValue)
		{
			this.m_currentParagraph.UpdateMarginTop(marginValue);
			this.m_currentParagraph.AddMarginBottom(marginValue);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00050214 File Offset: 0x0004E414
		private void ParseTextRunElement(HtmlElement.HtmlElementType elementType)
		{
			this.m_currentStyle = this.m_currentStyle.CreateChildStyle(elementType);
			bool flag = false;
			switch (this.m_currentHtmlElement.ElementType)
			{
			case HtmlElement.HtmlElementType.SPAN:
			case HtmlElement.HtmlElementType.FONT:
				flag = true;
				break;
			case HtmlElement.HtmlElementType.STRONG:
			case HtmlElement.HtmlElementType.B:
				this.m_currentStyle.FontWeight = FontWeights.Bold;
				break;
			case HtmlElement.HtmlElementType.STRIKE:
			case HtmlElement.HtmlElementType.S:
				this.m_currentStyle.TextDecoration = TextDecorations.LineThrough;
				break;
			case HtmlElement.HtmlElementType.I:
			case HtmlElement.HtmlElementType.EM:
				this.m_currentStyle.FontStyle = FontStyles.Italic;
				break;
			case HtmlElement.HtmlElementType.U:
				this.m_currentStyle.TextDecoration = TextDecorations.Underline;
				break;
			}
			if (flag && !this.m_currentHtmlElement.IsEmptyElement && this.m_currentHtmlElement.HasAttributes)
			{
				if (this.m_currentHtmlElement.ElementType == HtmlElement.HtmlElementType.FONT)
				{
					string text;
					if (this.m_currentHtmlElement.Attributes.TryGetValue("size", out text))
					{
						string text2;
						if (RichTextStyleTranslator.TranslateHtmlFontSize(text, out text2))
						{
							this.m_currentStyle.FontSize = new ReportSize(text2);
						}
						else
						{
							this.m_richTextLogger.RegisterInvalidSizeWarning("size", text, this.m_currentHtmlElement.CharacterPosition);
						}
					}
					if (this.m_currentHtmlElement.Attributes.TryGetValue("face", out text))
					{
						this.m_currentStyle.FontFamily = text;
					}
					if (this.m_currentHtmlElement.Attributes.TryGetValue("color", out text))
					{
						ReportColor reportColor;
						if (ReportColor.TryParse(RichTextStyleTranslator.TranslateHtmlColor(text), out reportColor))
						{
							this.m_currentStyle.Color = reportColor;
							return;
						}
						this.m_richTextLogger.RegisterInvalidColorWarning("color", text, this.m_currentHtmlElement.CharacterPosition);
						return;
					}
				}
				else if (this.m_currentHtmlElement.ElementType == HtmlElement.HtmlElementType.SPAN)
				{
					this.SetStyleValues(false);
				}
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000503C6 File Offset: 0x0004E5C6
		private void RevertActionElement(HtmlElement.HtmlElementType elementType)
		{
			if (this.m_currentHyperlinkText != null)
			{
				this.m_currentHyperlinkText = null;
				this.m_currentStyle = this.m_currentStyle.RemoveStyle(elementType);
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000503EC File Offset: 0x0004E5EC
		private void ParseActionElement(int listLevel)
		{
			this.RevertActionElement(HtmlElement.HtmlElementType.A);
			string text;
			if (!this.m_currentHtmlElement.IsEmptyElement && this.m_currentHtmlElement.HasAttributes && this.m_currentHtmlElement.Attributes.TryGetValue("href", out text))
			{
				IActionInstance actionInstance = this.m_IRichTextInstanceCreator.CreateActionInstance();
				actionInstance.SetHyperlinkText(text);
				if (actionInstance.HyperlinkText != null)
				{
					this.m_currentStyle = this.m_currentStyle.CreateChildStyle(HtmlElement.HtmlElementType.A);
					this.m_currentStyle.Color = new ReportColor("Blue");
					this.m_currentStyle.TextDecoration = TextDecorations.Underline;
					this.m_currentHyperlinkText = text;
				}
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00050488 File Offset: 0x0004E688
		protected override ICompiledTextRunInstance CreateTextRunInstance()
		{
			ICompiledTextRunInstance compiledTextRunInstance = base.CreateTextRunInstance();
			compiledTextRunInstance.MarkupType = MarkupType.HTML;
			if (this.m_currentHyperlinkText != null)
			{
				IActionInstance actionInstance = this.m_IRichTextInstanceCreator.CreateActionInstance();
				actionInstance.SetHyperlinkText(this.m_currentHyperlinkText);
				compiledTextRunInstance.ActionInstance = actionInstance;
			}
			return compiledTextRunInstance;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000504CC File Offset: 0x0004E6CC
		private void SetStyleValues(bool isParagraph)
		{
			if (this.m_currentHtmlElement.CssStyle != null)
			{
				string text;
				if (isParagraph && this.m_allowMultipleParagraphs)
				{
					if (this.m_currentHtmlElement.CssStyle.TryGetValue("text-align", out text))
					{
						TextAlignments textAlignments;
						if (RichTextStyleTranslator.TranslateTextAlign(text, out textAlignments))
						{
							this.m_currentStyle.TextAlign = textAlignments;
						}
						else
						{
							this.m_richTextLogger.RegisterInvalidValueWarning("text-align", text, this.m_currentHtmlElement.CharacterPosition);
						}
					}
					if (this.m_currentHtmlElement.CssStyle.TryGetValue("text-indent", out text))
					{
						ReportSize reportSize;
						if (ReportSize.TryParse(text, true, out reportSize))
						{
							this.m_currentParagraph.HangingIndent = reportSize;
						}
						else
						{
							this.m_richTextLogger.RegisterInvalidSizeWarning("text-indent", text, this.m_currentHtmlElement.CharacterPosition);
						}
					}
					ReportSize reportSize2 = null;
					if (this.m_currentHtmlElement.CssStyle.TryGetValue("padding", out text))
					{
						ReportSize reportSize;
						if (ReportSize.TryParse(text, out reportSize))
						{
							reportSize2 = reportSize;
						}
						else
						{
							this.m_richTextLogger.RegisterInvalidSizeWarning("padding", text, this.m_currentHtmlElement.CharacterPosition);
						}
					}
					ReportSize reportSize3;
					if (this.HasPaddingValue("padding-top", reportSize2, out reportSize3))
					{
						this.m_currentParagraph.AddSpaceBefore(reportSize3);
					}
					if (this.HasPaddingValue("padding-bottom", reportSize2, out reportSize3))
					{
						this.m_currentParagraph.AddSpaceAfter(reportSize3);
					}
					if (this.HasPaddingValue("padding-left", reportSize2, out reportSize3))
					{
						this.m_currentParagraph.AddLeftIndent(reportSize3);
					}
					if (this.HasPaddingValue("padding-right", reportSize2, out reportSize3))
					{
						this.m_currentParagraph.AddRightIndent(reportSize3);
					}
				}
				if (this.m_currentHtmlElement.CssStyle.TryGetValue("font-family", out text))
				{
					this.m_currentStyle.FontFamily = text;
				}
				if (this.m_currentHtmlElement.CssStyle.TryGetValue("font-size", out text))
				{
					ReportSize reportSize;
					if (ReportSize.TryParse(text, out reportSize))
					{
						this.m_currentStyle.FontSize = reportSize;
					}
					else
					{
						this.m_richTextLogger.RegisterInvalidSizeWarning("font-size", text, this.m_currentHtmlElement.CharacterPosition);
					}
				}
				if (this.m_currentHtmlElement.CssStyle.TryGetValue("font-weight", out text))
				{
					FontWeights fontWeights;
					if (RichTextStyleTranslator.TranslateFontWeight(text, out fontWeights))
					{
						this.m_currentStyle.FontWeight = fontWeights;
					}
					else
					{
						this.m_richTextLogger.RegisterInvalidValueWarning("font-weight", text, this.m_currentHtmlElement.CharacterPosition);
					}
				}
				if (this.m_currentHtmlElement.CssStyle.TryGetValue("color", out text))
				{
					ReportColor reportColor;
					if (ReportColor.TryParse(RichTextStyleTranslator.TranslateHtmlColor(text), out reportColor))
					{
						this.m_currentStyle.Color = reportColor;
						return;
					}
					this.m_richTextLogger.RegisterInvalidColorWarning("color", text, this.m_currentHtmlElement.CharacterPosition);
				}
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00050760 File Offset: 0x0004E960
		private bool HasPaddingValue(string attrName, ReportSize generalPadding, out ReportSize effectivePadding)
		{
			string text;
			if (this.m_currentHtmlElement.CssStyle.TryGetValue(attrName, out text))
			{
				ReportSize reportSize;
				if (ReportSize.TryParse(text, out reportSize))
				{
					effectivePadding = reportSize;
					return true;
				}
				this.m_richTextLogger.RegisterInvalidSizeWarning("padding", text, this.m_currentHtmlElement.CharacterPosition);
			}
			if (generalPadding != null)
			{
				effectivePadding = generalPadding;
				return true;
			}
			effectivePadding = null;
			return false;
		}

		// Token: 0x0400093B RID: 2363
		private HtmlElement m_currentHtmlElement;

		// Token: 0x0400093C RID: 2364
		private string m_currentHyperlinkText;

		// Token: 0x0400093D RID: 2365
		private HtmlLexer m_htmlLexer;

		// Token: 0x0200093A RID: 2362
		internal sealed class Constants
		{
			// Token: 0x04003FE3 RID: 16355
			internal const string HtmlSize = "size";

			// Token: 0x04003FE4 RID: 16356
			internal const string HtmlColor = "color";

			// Token: 0x04003FE5 RID: 16357
			internal const string HtmlAlign = "align";

			// Token: 0x04003FE6 RID: 16358
			internal const string CssFontSize = "font-size";

			// Token: 0x04003FE7 RID: 16359
			internal const string CssFontStyle = "font-style";

			// Token: 0x04003FE8 RID: 16360
			internal const string CssFontWeight = "font-weight";

			// Token: 0x04003FE9 RID: 16361
			internal const string CssTextAlign = "text-align";

			// Token: 0x04003FEA RID: 16362
			internal const string CssTextIndent = "text-indent";

			// Token: 0x04003FEB RID: 16363
			internal const string CssPadding = "padding";

			// Token: 0x04003FEC RID: 16364
			internal const string CssColor = "color";

			// Token: 0x04003FED RID: 16365
			internal const char NonBreakingSpace = '\u00a0';
		}

		// Token: 0x0200093B RID: 2363
		internal static class StyleDefaults
		{
			// Token: 0x04003FEE RID: 16366
			internal static ReportSize H1FontSize = new ReportSize("24pt");

			// Token: 0x04003FEF RID: 16367
			internal static ReportSize H2FontSize = new ReportSize("18pt");

			// Token: 0x04003FF0 RID: 16368
			internal static ReportSize H3FontSize = new ReportSize("14pt");

			// Token: 0x04003FF1 RID: 16369
			internal static ReportSize H4FontSize = new ReportSize("12pt");

			// Token: 0x04003FF2 RID: 16370
			internal static ReportSize H5FontSize = new ReportSize("10pt");

			// Token: 0x04003FF3 RID: 16371
			internal static ReportSize H6FontSize = new ReportSize("8pt");

			// Token: 0x04003FF4 RID: 16372
			internal static ReportSize PFontSize = new ReportSize("10pt");

			// Token: 0x04003FF5 RID: 16373
			internal static ReportSize H1Margin = HtmlParser.StyleDefaults.H1FontSize;

			// Token: 0x04003FF6 RID: 16374
			internal static ReportSize H2Margin = HtmlParser.StyleDefaults.H2FontSize;

			// Token: 0x04003FF7 RID: 16375
			internal static ReportSize H3Margin = HtmlParser.StyleDefaults.H3FontSize;

			// Token: 0x04003FF8 RID: 16376
			internal static ReportSize H4Margin = HtmlParser.StyleDefaults.H4FontSize;

			// Token: 0x04003FF9 RID: 16377
			internal static ReportSize H5Margin = HtmlParser.StyleDefaults.H5FontSize;

			// Token: 0x04003FFA RID: 16378
			internal static ReportSize H6Margin = HtmlParser.StyleDefaults.H6FontSize;

			// Token: 0x04003FFB RID: 16379
			internal static ReportSize PMargin = HtmlParser.StyleDefaults.PFontSize;
		}
	}
}
