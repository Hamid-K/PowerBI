using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000026 RID: 38
	internal sealed class ExpandOptionSelectExpandTermParser : SelectExpandTermParser
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x0000487F File Offset: 0x00002A7F
		public ExpandOptionSelectExpandTermParser(string clauseToParse, int maxDepth)
			: base(clauseToParse, maxDepth)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000488C File Offset: 0x00002A8C
		internal override ExpandTermToken BuildExpandTermToken(bool isInnerTerm, PathSegmentToken pathToken)
		{
			QueryToken queryToken = null;
			OrderByToken orderByToken = null;
			long? num = default(long?);
			long? num2 = default(long?);
			InlineCountKind? inlineCountKind = default(InlineCountKind?);
			SelectToken selectToken = null;
			ExpandToken expandToken = null;
			if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.CloseParen)
				{
					string text;
					if ((text = this.Lexer.NextToken().Text) != null)
					{
						if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x60000ec-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
							dictionary.Add("$filter", 0);
							dictionary.Add("$orderby", 1);
							dictionary.Add("$top", 2);
							dictionary.Add("$skip", 3);
							dictionary.Add("$inlinecount", 4);
							dictionary.Add("$select", 5);
							dictionary.Add("$expand", 6);
							<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x60000ec-1 = dictionary;
						}
						int num3;
						if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x60000ec-1.TryGetValue(text, ref num3))
						{
							switch (num3)
							{
							case 0:
							{
								this.Lexer.NextToken();
								string text2 = this.ReadQueryOption();
								UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(base.MaxDepth);
								queryToken = uriQueryExpressionParser.ParseFilter(text2);
								continue;
							}
							case 1:
							{
								this.Lexer.NextToken();
								string text3 = this.ReadQueryOption();
								UriQueryExpressionParser uriQueryExpressionParser2 = new UriQueryExpressionParser(base.MaxDepth);
								orderByToken = Enumerable.Single<OrderByToken>(uriQueryExpressionParser2.ParseOrderBy(text3));
								continue;
							}
							case 2:
							{
								this.Lexer.NextToken();
								string text4 = this.ReadQueryOption();
								long num4;
								if (!long.TryParse(text4, ref num4))
								{
									throw new ODataException(Strings.UriSelectParser_InvalidTopOption(text4));
								}
								num = new long?(num4);
								continue;
							}
							case 3:
							{
								this.Lexer.NextToken();
								string text5 = this.ReadQueryOption();
								long num5;
								if (!long.TryParse(text5, ref num5))
								{
									throw new ODataException(Strings.UriSelectParser_InvalidSkipOption(text5));
								}
								num2 = new long?(num5);
								continue;
							}
							case 4:
							{
								this.Lexer.NextToken();
								string text6 = this.ReadQueryOption();
								string text7;
								if ((text7 = text6) != null)
								{
									if (text7 == "none")
									{
										inlineCountKind = new InlineCountKind?(InlineCountKind.None);
										continue;
									}
									if (text7 == "allpages")
									{
										inlineCountKind = new InlineCountKind?(InlineCountKind.AllPages);
										continue;
									}
								}
								throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
							}
							case 5:
								this.Lexer.NextToken();
								selectToken = base.ParseSelect();
								continue;
							case 6:
								this.Lexer.NextToken();
								expandToken = base.ParseExpand();
								continue;
							}
						}
					}
					throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
				}
			}
			else if (this.IsNotEndOfTerm(isInnerTerm))
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			return new ExpandTermToken(pathToken, queryToken, orderByToken, num, num2, inlineCountKind, selectToken, expandToken);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004B5C File Offset: 0x00002D5C
		internal override bool IsNotEndOfTerm(bool isInnerTerm)
		{
			if (!isInnerTerm)
			{
				return this.Lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma;
			}
			return this.Lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.SemiColon;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004BDC File Offset: 0x00002DDC
		private string ReadQueryOption()
		{
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Equal)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			this.Lexer.NextToken();
			string text = this.Lexer.ExpressionText.Substring(this.Lexer.Position);
			text = Enumerable.First<string>(text.Split(new char[] { ';' }));
			while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.SemiColon)
			{
				this.Lexer.NextToken();
			}
			this.Lexer.NextToken();
			return text;
		}
	}
}
