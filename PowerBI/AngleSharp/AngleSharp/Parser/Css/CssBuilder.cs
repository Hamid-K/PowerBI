using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000077 RID: 119
	internal sealed class CssBuilder
	{
		// Token: 0x06000350 RID: 848 RVA: 0x000172BC File Offset: 0x000154BC
		public CssBuilder(CssTokenizer tokenizer, CssParser parser)
		{
			this._tokenizer = tokenizer;
			this._parser = parser;
			this._nodes = new Stack<CssNode>();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000172E0 File Offset: 0x000154E0
		public CssRule CreateAtRule(CssToken token)
		{
			if (token.Data.Is(RuleNames.Media))
			{
				return this.CreateMedia(token);
			}
			if (token.Data.Is(RuleNames.FontFace))
			{
				return this.CreateFontFace(token);
			}
			if (token.Data.Is(RuleNames.Keyframes))
			{
				return this.CreateKeyframes(token);
			}
			if (token.Data.Is(RuleNames.Import))
			{
				return this.CreateImport(token);
			}
			if (token.Data.Is(RuleNames.Charset))
			{
				return this.CreateCharset(token);
			}
			if (token.Data.Is(RuleNames.Namespace))
			{
				return this.CreateNamespace(token);
			}
			if (token.Data.Is(RuleNames.Page))
			{
				return this.CreatePage(token);
			}
			if (token.Data.Is(RuleNames.Supports))
			{
				return this.CreateSupports(token);
			}
			if (token.Data.Is(RuleNames.ViewPort))
			{
				return this.CreateViewport(token);
			}
			if (token.Data.Is(RuleNames.Document))
			{
				return this.CreateDocument(token);
			}
			return this.CreateUnknown(token);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000173F8 File Offset: 0x000155F8
		public CssRule CreateRule(CssToken token)
		{
			CssTokenType type = token.Type;
			if (type <= CssTokenType.Url)
			{
				if (type != CssTokenType.String && type != CssTokenType.Url)
				{
					goto IL_006F;
				}
			}
			else
			{
				if (type == CssTokenType.AtKeyword)
				{
					return this.CreateAtRule(token);
				}
				switch (type)
				{
				case CssTokenType.RoundBracketClose:
				case CssTokenType.CurlyBracketClose:
				case CssTokenType.SquareBracketClose:
					break;
				case CssTokenType.CurlyBracketOpen:
					this.RaiseErrorOccurred(CssParseError.InvalidBlockStart, token.Position);
					this.JumpToRuleEnd(ref token);
					return null;
				case CssTokenType.SquareBracketOpen:
					goto IL_006F;
				default:
					goto IL_006F;
				}
			}
			this.RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
			this.JumpToRuleEnd(ref token);
			return null;
			IL_006F:
			return this.CreateStyle(token);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001747C File Offset: 0x0001567C
		public CssRule CreateCharset(CssToken current)
		{
			CssCharsetRule cssCharsetRule = new CssCharsetRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssCharsetRule);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.String)
			{
				cssCharsetRule.CharacterSet = cssToken.Data;
			}
			this.JumpToEnd(ref cssToken);
			cssCharsetRule.SourceCode = this.CreateView(position, cssToken.Position);
			this._nodes.Pop();
			return cssCharsetRule;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000174F4 File Offset: 0x000156F4
		public CssRule CreateDocument(CssToken current)
		{
			CssDocumentRule rule = new CssDocumentRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(rule);
			this.CollectTrivia(ref cssToken);
			this.FillFunctions(delegate(DocumentFunction function)
			{
				rule.AppendChild(function);
			}, ref cssToken);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.CurlyBracketOpen)
			{
				TextPosition textPosition = this.FillRules(rule);
				rule.SourceCode = this.CreateView(position, textPosition);
				this._nodes.Pop();
				return rule;
			}
			this._nodes.Pop();
			return this.SkipDeclarations(cssToken);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000175AC File Offset: 0x000157AC
		public CssRule CreateViewport(CssToken current)
		{
			CssViewportRule cssViewportRule = new CssViewportRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssViewportRule);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.CurlyBracketOpen)
			{
				TextPosition textPosition = this.FillDeclarations(cssViewportRule, new Func<string, CssProperty>(Factory.Properties.CreateViewport));
				cssViewportRule.SourceCode = this.CreateView(position, textPosition);
				this._nodes.Pop();
				return cssViewportRule;
			}
			this._nodes.Pop();
			return this.SkipDeclarations(cssToken);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001763C File Offset: 0x0001583C
		public CssRule CreateFontFace(CssToken current)
		{
			CssFontFaceRule cssFontFaceRule = new CssFontFaceRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssFontFaceRule);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.CurlyBracketOpen)
			{
				TextPosition textPosition = this.FillDeclarations(cssFontFaceRule, new Func<string, CssProperty>(Factory.Properties.CreateFont));
				cssFontFaceRule.SourceCode = this.CreateView(position, textPosition);
				this._nodes.Pop();
				return cssFontFaceRule;
			}
			this._nodes.Pop();
			return this.SkipDeclarations(cssToken);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000176CC File Offset: 0x000158CC
		public CssRule CreateImport(CssToken current)
		{
			CssImportRule cssImportRule = new CssImportRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssImportRule);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Is(CssTokenType.String, CssTokenType.Url))
			{
				cssImportRule.Href = cssToken.Data;
				cssToken = this.NextToken();
				this.CollectTrivia(ref cssToken);
				this.FillMediaList(cssImportRule.Media, CssTokenType.Semicolon, ref cssToken);
			}
			this.CollectTrivia(ref cssToken);
			this.JumpToEnd(ref cssToken);
			cssImportRule.SourceCode = this.CreateView(position, cssToken.Position);
			this._nodes.Pop();
			return cssImportRule;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001776C File Offset: 0x0001596C
		public CssRule CreateKeyframes(CssToken current)
		{
			CssKeyframesRule cssKeyframesRule = new CssKeyframesRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssKeyframesRule);
			this.CollectTrivia(ref cssToken);
			cssKeyframesRule.Name = this.GetRuleName(ref cssToken);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.CurlyBracketOpen)
			{
				TextPosition textPosition = this.FillKeyframeRules(cssKeyframesRule);
				cssKeyframesRule.SourceCode = this.CreateView(position, textPosition);
				this._nodes.Pop();
				return cssKeyframesRule;
			}
			this._nodes.Pop();
			return this.SkipDeclarations(cssToken);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00017800 File Offset: 0x00015A00
		public CssRule CreateMedia(CssToken current)
		{
			CssMediaRule cssMediaRule = new CssMediaRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssMediaRule);
			this.CollectTrivia(ref cssToken);
			this.FillMediaList(cssMediaRule.Media, CssTokenType.CurlyBracketOpen, ref cssToken);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type != CssTokenType.CurlyBracketOpen)
			{
				while (cssToken.Type != CssTokenType.EndOfFile)
				{
					if (cssToken.Type == CssTokenType.Semicolon)
					{
						this._nodes.Pop();
						return null;
					}
					if (cssToken.Type == CssTokenType.CurlyBracketOpen)
					{
						break;
					}
					cssToken = this.NextToken();
				}
			}
			TextPosition textPosition = this.FillRules(cssMediaRule);
			cssMediaRule.SourceCode = this.CreateView(position, textPosition);
			this._nodes.Pop();
			return cssMediaRule;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000178B8 File Offset: 0x00015AB8
		public CssRule CreateNamespace(CssToken current)
		{
			CssNamespaceRule cssNamespaceRule = new CssNamespaceRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssNamespaceRule);
			this.CollectTrivia(ref cssToken);
			cssNamespaceRule.Prefix = this.GetRuleName(ref cssToken);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.Url)
			{
				cssNamespaceRule.NamespaceUri = cssToken.Data;
			}
			this.JumpToEnd(ref cssToken);
			cssNamespaceRule.SourceCode = this.CreateView(position, cssToken.Position);
			this._nodes.Pop();
			return cssNamespaceRule;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00017948 File Offset: 0x00015B48
		public CssRule CreatePage(CssToken current)
		{
			CssPageRule cssPageRule = new CssPageRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssPageRule);
			this.CollectTrivia(ref cssToken);
			cssPageRule.Selector = this.CreateSelector(ref cssToken);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.CurlyBracketOpen)
			{
				TextPosition textPosition = this.FillDeclarations(cssPageRule.Style);
				cssPageRule.SourceCode = this.CreateView(position, textPosition);
				this._nodes.Pop();
				return cssPageRule;
			}
			this._nodes.Pop();
			return this.SkipDeclarations(cssToken);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000179E0 File Offset: 0x00015BE0
		public CssRule CreateSupports(CssToken current)
		{
			CssSupportsRule cssSupportsRule = new CssSupportsRule(this._parser);
			TextPosition position = current.Position;
			CssToken cssToken = this.NextToken();
			this._nodes.Push(cssSupportsRule);
			this.CollectTrivia(ref cssToken);
			cssSupportsRule.Condition = this.AggregateCondition(ref cssToken);
			this.CollectTrivia(ref cssToken);
			if (cssToken.Type == CssTokenType.CurlyBracketOpen)
			{
				TextPosition textPosition = this.FillRules(cssSupportsRule);
				cssSupportsRule.SourceCode = this.CreateView(position, textPosition);
				this._nodes.Pop();
				return cssSupportsRule;
			}
			this._nodes.Pop();
			return this.SkipDeclarations(cssToken);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00017A74 File Offset: 0x00015C74
		public CssRule CreateStyle(CssToken current)
		{
			CssStyleRule cssStyleRule = new CssStyleRule(this._parser);
			TextPosition position = current.Position;
			this._nodes.Push(cssStyleRule);
			this.CollectTrivia(ref current);
			cssStyleRule.Selector = this.CreateSelector(ref current);
			TextPosition textPosition = this.FillDeclarations(cssStyleRule.Style);
			cssStyleRule.SourceCode = this.CreateView(position, textPosition);
			this._nodes.Pop();
			if (cssStyleRule.Selector == null)
			{
				return null;
			}
			return cssStyleRule;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00017AE8 File Offset: 0x00015CE8
		public CssKeyframeRule CreateKeyframeRule(CssToken current)
		{
			CssKeyframeRule cssKeyframeRule = new CssKeyframeRule(this._parser);
			TextPosition position = current.Position;
			this._nodes.Push(cssKeyframeRule);
			this.CollectTrivia(ref current);
			cssKeyframeRule.Key = this.CreateKeyframeSelector(ref current);
			TextPosition textPosition = this.FillDeclarations(cssKeyframeRule.Style);
			cssKeyframeRule.SourceCode = this.CreateView(position, textPosition);
			this._nodes.Pop();
			if (cssKeyframeRule.Key == null)
			{
				return null;
			}
			return cssKeyframeRule;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00017B5C File Offset: 0x00015D5C
		public CssRule CreateUnknown(CssToken current)
		{
			TextPosition position = current.Position;
			if (this._parser.Options.IsIncludingUnknownRules)
			{
				CssToken cssToken = this.NextToken();
				CssUnknownRule cssUnknownRule = new CssUnknownRule(current.Data, this._parser);
				this._nodes.Push(cssUnknownRule);
				while (cssToken.IsNot(CssTokenType.CurlyBracketOpen, CssTokenType.Semicolon, CssTokenType.EndOfFile))
				{
					cssToken = this.NextToken();
				}
				if (cssToken.Type == CssTokenType.CurlyBracketOpen)
				{
					int num = 1;
					do
					{
						cssToken = this.NextToken();
						CssTokenType type = cssToken.Type;
						if (type != CssTokenType.CurlyBracketOpen)
						{
							if (type != CssTokenType.CurlyBracketClose)
							{
								if (type == CssTokenType.EndOfFile)
								{
									num = 0;
								}
							}
							else
							{
								num--;
							}
						}
						else
						{
							num++;
						}
					}
					while (num != 0);
				}
				cssUnknownRule.SourceCode = this.CreateView(position, cssToken.Position);
				this._nodes.Pop();
				return cssUnknownRule;
			}
			this.RaiseErrorOccurred(CssParseError.UnknownAtRule, position);
			this.JumpToRuleEnd(ref current);
			return null;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00017C40 File Offset: 0x00015E40
		public CssValue CreateValue(ref CssToken token)
		{
			bool flag = false;
			return this.CreateValue(CssTokenType.CurlyBracketClose, ref token, out flag);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00017C5C File Offset: 0x00015E5C
		public List<CssMedium> CreateMedia(ref CssToken token)
		{
			List<CssMedium> list = new List<CssMedium>();
			this.CollectTrivia(ref token);
			while (token.Type != CssTokenType.EndOfFile)
			{
				CssMedium cssMedium = this.CreateMedium(ref token);
				if (cssMedium == null || token.IsNot(CssTokenType.Comma, CssTokenType.EndOfFile))
				{
					throw new DomException(DomError.Syntax);
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
				list.Add(cssMedium);
			}
			return list;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00017CBC File Offset: 0x00015EBC
		public TextPosition CreateRules(CssStyleSheet sheet)
		{
			CssToken cssToken = this.NextToken();
			this._nodes.Push(sheet);
			this.CollectTrivia(ref cssToken);
			while (cssToken.Type != CssTokenType.EndOfFile)
			{
				CssRule cssRule = this.CreateRule(cssToken);
				cssToken = this.NextToken();
				this.CollectTrivia(ref cssToken);
				sheet.Rules.Add(cssRule);
			}
			this._nodes.Pop();
			return cssToken.Position;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00017D25 File Offset: 0x00015F25
		public IConditionFunction CreateCondition(ref CssToken token)
		{
			this.CollectTrivia(ref token);
			return this.AggregateCondition(ref token);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00017D38 File Offset: 0x00015F38
		public KeyframeSelector CreateKeyframeSelector(ref CssToken token)
		{
			List<Percent> list = new List<Percent>();
			bool flag = true;
			TextPosition position = token.Position;
			this.CollectTrivia(ref token);
			while (token.Type != CssTokenType.EndOfFile)
			{
				if (list.Count > 0)
				{
					if (token.Type == CssTokenType.CurlyBracketOpen)
					{
						break;
					}
					if (token.Type != CssTokenType.Comma)
					{
						flag = false;
					}
					else
					{
						token = this.NextToken();
					}
					this.CollectTrivia(ref token);
				}
				if (token.Type == CssTokenType.Percentage)
				{
					list.Add(new Percent(((CssUnitToken)token).Value));
				}
				else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.From))
				{
					list.Add(Percent.Zero);
				}
				else if (token.Type == CssTokenType.Ident && token.Data.Is(Keywords.To))
				{
					list.Add(Percent.Hundred);
				}
				else
				{
					flag = false;
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
			}
			if (!flag)
			{
				this.RaiseErrorOccurred(CssParseError.InvalidSelector, position);
			}
			return new KeyframeSelector(list);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00017E40 File Offset: 0x00016040
		public List<DocumentFunction> CreateFunctions(ref CssToken token)
		{
			List<DocumentFunction> functions = new List<DocumentFunction>();
			this.CollectTrivia(ref token);
			this.FillFunctions(delegate(DocumentFunction function)
			{
				functions.Add(function);
			}, ref token);
			return functions;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00017E80 File Offset: 0x00016080
		public TextPosition FillDeclarations(CssStyleDeclaration style)
		{
			CssToken cssToken = this.NextToken();
			this._nodes.Push(style);
			this.CollectTrivia(ref cssToken);
			while (cssToken.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
			{
				CssProperty cssProperty = this.CreateDeclarationWith(new Func<string, CssProperty>(Factory.Properties.Create), ref cssToken);
				if (cssProperty != null && cssProperty.HasValue)
				{
					style.SetProperty(cssProperty);
				}
				this.CollectTrivia(ref cssToken);
			}
			this._nodes.Pop();
			return cssToken.Position;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00017EFC File Offset: 0x000160FC
		public CssProperty CreateDeclarationWith(Func<string, CssProperty> createProperty, ref CssToken token)
		{
			CssProperty cssProperty = null;
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			TextPosition position = token.Position;
			while (token.IsDeclarationName())
			{
				stringBuilder.Append(token.ToValue());
				token = this.NextToken();
			}
			string text = stringBuilder.ToPool();
			if (text.Length > 0)
			{
				cssProperty = ((this._parser.Options.IsIncludingUnknownDeclarations || this._parser.Options.IsToleratingInvalidValues) ? new CssUnknownProperty(text) : createProperty(text));
				if (cssProperty == null)
				{
					this.RaiseErrorOccurred(CssParseError.UnknownDeclarationName, position);
				}
				else
				{
					this._nodes.Push(cssProperty);
				}
				this.CollectTrivia(ref token);
				if (token.Type == CssTokenType.Colon)
				{
					bool flag = false;
					CssValue cssValue = this.CreateValue(CssTokenType.CurlyBracketClose, ref token, out flag);
					if (cssValue == null)
					{
						this.RaiseErrorOccurred(CssParseError.ValueMissing, token.Position);
					}
					else if (cssProperty != null && cssProperty.TrySetValue(cssValue))
					{
						cssProperty.IsImportant = flag;
					}
					this.CollectTrivia(ref token);
				}
				else
				{
					this.RaiseErrorOccurred(CssParseError.ColonMissing, token.Position);
				}
				this.JumpToDeclEnd(ref token);
				if (cssProperty != null)
				{
					this._nodes.Pop();
				}
			}
			else if (token.Type != CssTokenType.EndOfFile)
			{
				this.RaiseErrorOccurred(CssParseError.IdentExpected, position);
				this.JumpToDeclEnd(ref token);
			}
			if (token.Type == CssTokenType.Semicolon)
			{
				token = this.NextToken();
			}
			return cssProperty;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001804E File Offset: 0x0001624E
		public CssProperty CreateDeclaration(ref CssToken token)
		{
			this.CollectTrivia(ref token);
			return this.CreateDeclarationWith(new Func<string, CssProperty>(Factory.Properties.Create), ref token);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00018070 File Offset: 0x00016270
		public CssMedium CreateMedium(ref CssToken token)
		{
			CssMedium cssMedium = new CssMedium();
			this.CollectTrivia(ref token);
			if (token.Type == CssTokenType.Ident)
			{
				string data = token.Data;
				if (data.Isi(Keywords.Not))
				{
					cssMedium.IsInverse = true;
					token = this.NextToken();
					this.CollectTrivia(ref token);
				}
				else if (data.Isi(Keywords.Only))
				{
					cssMedium.IsExclusive = true;
					token = this.NextToken();
					this.CollectTrivia(ref token);
				}
			}
			if (token.Type == CssTokenType.Ident)
			{
				cssMedium.Type = token.Data;
				token = this.NextToken();
				this.CollectTrivia(ref token);
				if (token.Type != CssTokenType.Ident || !token.Data.Isi(Keywords.And))
				{
					return cssMedium;
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
			}
			while (token.Type == CssTokenType.RoundBracketOpen)
			{
				token = this.NextToken();
				this.CollectTrivia(ref token);
				MediaFeature mediaFeature = this.CreateFeature(ref token);
				if (mediaFeature != null)
				{
					cssMedium.AppendChild(mediaFeature);
				}
				if (token.Type != CssTokenType.RoundBracketClose)
				{
					return null;
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
				if (mediaFeature == null)
				{
					return null;
				}
				if (token.Type == CssTokenType.Ident && token.Data.Isi(Keywords.And))
				{
					token = this.NextToken();
					this.CollectTrivia(ref token);
					if (token.Type != CssTokenType.EndOfFile)
					{
						continue;
					}
				}
				return cssMedium;
			}
			return null;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000181C2 File Offset: 0x000163C2
		private void JumpToEnd(ref CssToken current)
		{
			while (current.IsNot(CssTokenType.EndOfFile, CssTokenType.Semicolon))
			{
				current = this.NextToken();
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000181DC File Offset: 0x000163DC
		private void JumpToRuleEnd(ref CssToken current)
		{
			int num = 0;
			while (current.Type != CssTokenType.EndOfFile)
			{
				if (current.Type == CssTokenType.CurlyBracketOpen)
				{
					num++;
				}
				else if (current.Type == CssTokenType.CurlyBracketClose)
				{
					num--;
				}
				if (num <= 0 && current.Is(CssTokenType.CurlyBracketClose, CssTokenType.Semicolon))
				{
					break;
				}
				current = this.NextToken();
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00018234 File Offset: 0x00016434
		private void JumpToArgEnd(ref CssToken current)
		{
			int num = 0;
			while (current.Type != CssTokenType.EndOfFile)
			{
				if (current.Type == CssTokenType.RoundBracketOpen)
				{
					num++;
				}
				else
				{
					if (num <= 0 && current.Type == CssTokenType.RoundBracketClose)
					{
						break;
					}
					if (current.Type == CssTokenType.RoundBracketClose)
					{
						num--;
					}
				}
				current = this.NextToken();
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00018288 File Offset: 0x00016488
		private void JumpToDeclEnd(ref CssToken current)
		{
			int num = 0;
			while (current.Type != CssTokenType.EndOfFile)
			{
				if (current.Type == CssTokenType.CurlyBracketOpen)
				{
					num++;
				}
				else
				{
					if (num <= 0 && current.Is(CssTokenType.CurlyBracketClose, CssTokenType.Semicolon))
					{
						break;
					}
					if (current.Type == CssTokenType.CurlyBracketClose)
					{
						num--;
					}
				}
				current = this.NextToken();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000182DD File Offset: 0x000164DD
		private CssToken NextToken()
		{
			return this._tokenizer.Get();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000182EA File Offset: 0x000164EA
		private TextView CreateView(TextPosition start, TextPosition end)
		{
			return new TextView(new TextRange(start, end), this._tokenizer.Source);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00018304 File Offset: 0x00016504
		private void CollectTrivia(ref CssToken token)
		{
			bool isStoringTrivia = this._parser.Options.IsStoringTrivia;
			while (token.Type == CssTokenType.Whitespace || token.Type == CssTokenType.Comment || token.Type == CssTokenType.Cdc || token.Type == CssTokenType.Cdo)
			{
				if (isStoringTrivia && token.Type == CssTokenType.Comment)
				{
					CssNode cssNode = this._nodes.Peek();
					CssComment cssComment = new CssComment(token.Data);
					TextPosition position = token.Position;
					TextPosition textPosition = position.After(token.ToValue());
					cssComment.SourceCode = this.CreateView(position, textPosition);
					cssNode.AppendChild(cssComment);
				}
				token = this._tokenizer.Get();
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000183B4 File Offset: 0x000165B4
		private CssRule SkipDeclarations(CssToken token)
		{
			this.RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
			this.JumpToRuleEnd(ref token);
			return null;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000183CD File Offset: 0x000165CD
		private void RaiseErrorOccurred(CssParseError code, TextPosition position)
		{
			this._tokenizer.RaiseErrorOccurred(code, position);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000183DC File Offset: 0x000165DC
		private IConditionFunction AggregateCondition(ref CssToken token)
		{
			IConditionFunction conditionFunction = this.ExtractCondition(ref token);
			if (conditionFunction != null)
			{
				this.CollectTrivia(ref token);
				string data = token.Data;
				Func<IEnumerable<IConditionFunction>, IConditionFunction> creator = data.GetCreator();
				if (creator != null)
				{
					token = this.NextToken();
					this.CollectTrivia(ref token);
					List<IConditionFunction> list = this.MultipleConditions(conditionFunction, data, ref token);
					conditionFunction = creator(list);
				}
			}
			return conditionFunction;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00018430 File Offset: 0x00016630
		private IConditionFunction ExtractCondition(ref CssToken token)
		{
			if (token.Type == CssTokenType.RoundBracketOpen)
			{
				token = this.NextToken();
				this.CollectTrivia(ref token);
				IConditionFunction conditionFunction = this.AggregateCondition(ref token);
				if (conditionFunction != null)
				{
					conditionFunction = new GroupCondition
					{
						Content = conditionFunction
					};
				}
				else if (token.Type == CssTokenType.Ident)
				{
					conditionFunction = this.DeclarationCondition(ref token);
				}
				if (token.Type == CssTokenType.RoundBracketClose)
				{
					token = this.NextToken();
					this.CollectTrivia(ref token);
				}
				return conditionFunction;
			}
			if (token.Data.Isi(Keywords.Not))
			{
				NotCondition notCondition = new NotCondition();
				token = this.NextToken();
				this.CollectTrivia(ref token);
				notCondition.Content = this.ExtractCondition(ref token);
				return notCondition;
			}
			return null;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000184D8 File Offset: 0x000166D8
		private IConditionFunction DeclarationCondition(ref CssToken token)
		{
			CssProperty cssProperty = Factory.Properties.Create(token.Data) ?? new CssUnknownProperty(token.Data);
			DeclarationCondition declarationCondition = null;
			token = this.NextToken();
			this.CollectTrivia(ref token);
			if (token.Type == CssTokenType.Colon)
			{
				bool flag = false;
				CssValue cssValue = this.CreateValue(CssTokenType.RoundBracketClose, ref token, out flag);
				cssProperty.IsImportant = flag;
				if (cssValue != null)
				{
					declarationCondition = new DeclarationCondition(cssProperty, cssValue);
				}
			}
			return declarationCondition;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00018544 File Offset: 0x00016744
		private List<IConditionFunction> MultipleConditions(IConditionFunction condition, string connector, ref CssToken token)
		{
			List<IConditionFunction> list = new List<IConditionFunction>();
			this.CollectTrivia(ref token);
			list.Add(condition);
			while (token.Type != CssTokenType.EndOfFile)
			{
				condition = this.ExtractCondition(ref token);
				if (condition == null)
				{
					break;
				}
				list.Add(condition);
				if (!token.Data.Isi(connector))
				{
					break;
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
			}
			return list;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000185A4 File Offset: 0x000167A4
		private void FillFunctions(Action<DocumentFunction> add, ref CssToken token)
		{
			do
			{
				DocumentFunction documentFunction = token.ToDocumentFunction();
				if (documentFunction == null)
				{
					break;
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
				add(documentFunction);
				if (token.Type != CssTokenType.Comma)
				{
					break;
				}
				token = this.NextToken();
				this.CollectTrivia(ref token);
			}
			while (token.Type != CssTokenType.EndOfFile);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000185F8 File Offset: 0x000167F8
		private TextPosition FillKeyframeRules(CssKeyframesRule parentRule)
		{
			CssToken cssToken = this.NextToken();
			this.CollectTrivia(ref cssToken);
			while (cssToken.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
			{
				CssKeyframeRule cssKeyframeRule = this.CreateKeyframeRule(cssToken);
				cssToken = this.NextToken();
				this.CollectTrivia(ref cssToken);
				parentRule.Rules.Add(cssKeyframeRule);
			}
			return cssToken.Position;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001864C File Offset: 0x0001684C
		private TextPosition FillDeclarations(CssDeclarationRule rule, Func<string, CssProperty> createProperty)
		{
			CssToken cssToken = this.NextToken();
			this.CollectTrivia(ref cssToken);
			while (cssToken.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
			{
				CssProperty cssProperty = this.CreateDeclarationWith(createProperty, ref cssToken);
				if (cssProperty != null && cssProperty.HasValue)
				{
					rule.SetProperty(cssProperty);
				}
				this.CollectTrivia(ref cssToken);
			}
			return cssToken.Position;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000186A0 File Offset: 0x000168A0
		private TextPosition FillRules(CssGroupingRule group)
		{
			CssToken cssToken = this.NextToken();
			this.CollectTrivia(ref cssToken);
			while (cssToken.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
			{
				CssRule cssRule = this.CreateRule(cssToken);
				cssToken = this.NextToken();
				this.CollectTrivia(ref cssToken);
				group.Rules.Add(cssRule);
			}
			return cssToken.Position;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000186F4 File Offset: 0x000168F4
		private void FillMediaList(MediaList list, CssTokenType end, ref CssToken token)
		{
			this._nodes.Push(list);
			if (token.Type != end)
			{
				while (token.Type != CssTokenType.EndOfFile)
				{
					CssMedium cssMedium = this.CreateMedium(ref token);
					if (cssMedium != null)
					{
						list.AppendChild(cssMedium);
					}
					if (token.Type != CssTokenType.Comma)
					{
						break;
					}
					token = this.NextToken();
					this.CollectTrivia(ref token);
				}
				if (token.Type != end || list.Length == 0)
				{
					list.Clear();
					list.AppendChild(new CssMedium
					{
						IsInverse = true,
						Type = Keywords.All
					});
				}
			}
			this._nodes.Pop();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00018794 File Offset: 0x00016994
		private ISelector CreateSelector(ref CssToken token)
		{
			CssSelectorConstructor selectorCreator = this._parser.GetSelectorCreator();
			TextPosition position = token.Position;
			while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketOpen, CssTokenType.CurlyBracketClose))
			{
				selectorCreator.Apply(token);
				token = this.NextToken();
			}
			bool isValid = selectorCreator.IsValid;
			ISelector selector = selectorCreator.ToPool();
			CssNode cssNode = selector as CssNode;
			if (cssNode != null)
			{
				TextPosition textPosition = token.Position.Shift(-1);
				cssNode.SourceCode = this.CreateView(position, textPosition);
			}
			if (!isValid && !this._parser.Options.IsToleratingInvalidValues)
			{
				this.RaiseErrorOccurred(CssParseError.InvalidSelector, position);
				selector = null;
			}
			return selector;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00018838 File Offset: 0x00016A38
		private CssValue CreateValue(CssTokenType closing, ref CssToken token, out bool important)
		{
			CssValueBuilder cssValueBuilder = Pool.NewValueBuilder();
			this._tokenizer.IsInValue = true;
			token = this.NextToken();
			TextPosition position = token.Position;
			while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.Semicolon, closing))
			{
				cssValueBuilder.Apply(token);
				token = this.NextToken();
			}
			important = cssValueBuilder.IsImportant;
			this._tokenizer.IsInValue = false;
			bool isValid = cssValueBuilder.IsValid;
			CssValue cssValue = cssValueBuilder.ToPool();
			CssNode cssNode = cssValue;
			if (cssNode != null)
			{
				TextPosition textPosition = token.Position.Shift(-1);
				cssNode.SourceCode = this.CreateView(position, textPosition);
			}
			if (!isValid && !this._parser.Options.IsToleratingInvalidValues)
			{
				this.RaiseErrorOccurred(CssParseError.InvalidValue, position);
				cssValue = null;
			}
			return cssValue;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000188F4 File Offset: 0x00016AF4
		private string GetRuleName(ref CssToken token)
		{
			string text = string.Empty;
			if (token.Type == CssTokenType.Ident)
			{
				text = token.Data;
				token = this.NextToken();
			}
			return text;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00018924 File Offset: 0x00016B24
		private MediaFeature CreateFeature(ref CssToken token)
		{
			if (token.Type == CssTokenType.Ident)
			{
				TextPosition position = token.Position;
				CssValue cssValue = CssValue.Empty;
				MediaFeature mediaFeature = (this._parser.Options.IsToleratingInvalidConstraints ? new UnknownMediaFeature(token.Data) : Factory.MediaFeatures.Create(token.Data));
				token = this.NextToken();
				if (token.Type == CssTokenType.Colon)
				{
					CssValueBuilder cssValueBuilder = Pool.NewValueBuilder();
					token = this.NextToken();
					while (token.IsNot(CssTokenType.RoundBracketClose, CssTokenType.EndOfFile) || !cssValueBuilder.IsReady)
					{
						cssValueBuilder.Apply(token);
						token = this.NextToken();
					}
					cssValue = cssValueBuilder.ToPool();
				}
				else if (token.Type == CssTokenType.EndOfFile)
				{
					return null;
				}
				if (mediaFeature != null && mediaFeature.TrySetValue(cssValue))
				{
					CssNode cssNode = mediaFeature;
					if (cssNode != null)
					{
						TextPosition textPosition = token.Position.Shift(-1);
						cssNode.SourceCode = this.CreateView(position, textPosition);
					}
					return mediaFeature;
				}
			}
			else
			{
				this.JumpToArgEnd(ref token);
			}
			return null;
		}

		// Token: 0x040002D2 RID: 722
		private readonly CssTokenizer _tokenizer;

		// Token: 0x040002D3 RID: 723
		private readonly CssParser _parser;

		// Token: 0x040002D4 RID: 724
		private readonly Stack<CssNode> _nodes;
	}
}
