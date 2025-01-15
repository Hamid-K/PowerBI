using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015F RID: 351
	internal sealed class SelectExpandParser
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x000345AC File Offset: 0x000327AC
		public SelectExpandParser(string clauseToParse, int maxRecursiveDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
		{
			this.maxRecursiveDepth = maxRecursiveDepth;
			this.MaxPathDepth = maxRecursiveDepth;
			this.MaxFilterDepth = maxRecursiveDepth;
			this.MaxOrderByDepth = maxRecursiveDepth;
			this.MaxSearchDepth = maxRecursiveDepth;
			this.lexer = ((clauseToParse != null) ? new ExpressionLexer(clauseToParse, false, false) : null);
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
			this.enableNoDollarQueryOptions = enableNoDollarQueryOptions;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00034605 File Offset: 0x00032805
		public SelectExpandParser(ODataUriResolver resolver, string clauseToParse, IEdmStructuredType parentStructuredType, int maxRecursiveDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
			: this(clauseToParse, maxRecursiveDepth, enableCaseInsensitiveBuiltinIdentifier, enableNoDollarQueryOptions)
		{
			this.resolver = resolver;
			this.parentStructuredType = parentStructuredType;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00034624 File Offset: 0x00032824
		internal SelectExpandOptionParser SelectExpandOptionParser
		{
			get
			{
				if (this.selectExpandOptionParser == null)
				{
					this.selectExpandOptionParser = new SelectExpandOptionParser(this.resolver, this.parentStructuredType, this.maxRecursiveDepth, this.enableCaseInsensitiveBuiltinIdentifier, this.enableNoDollarQueryOptions)
					{
						MaxFilterDepth = this.MaxFilterDepth,
						MaxOrderByDepth = this.MaxOrderByDepth,
						MaxSearchDepth = this.MaxSearchDepth
					};
				}
				return this.selectExpandOptionParser;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0003468C File Offset: 0x0003288C
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00034694 File Offset: 0x00032894
		internal int MaxPathDepth { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0003469D File Offset: 0x0003289D
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x000346A5 File Offset: 0x000328A5
		internal int MaxFilterDepth { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000346AE File Offset: 0x000328AE
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x000346B6 File Offset: 0x000328B6
		internal int MaxOrderByDepth { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x000346BF File Offset: 0x000328BF
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x000346C7 File Offset: 0x000328C7
		internal int MaxSearchDepth { get; set; }

		// Token: 0x060011EA RID: 4586 RVA: 0x000346D0 File Offset: 0x000328D0
		public SelectToken ParseSelect()
		{
			this.isSelect = true;
			return this.ParseCommaSeperatedSelectList((IEnumerable<SelectTermToken> termTokens) => new SelectToken(termTokens), new Func<SelectTermToken>(this.ParseSingleSelectTerm));
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003470A File Offset: 0x0003290A
		public ExpandToken ParseExpand()
		{
			this.isSelect = false;
			return this.ParseCommaSeperatedExpandList((IEnumerable<ExpandTermToken> termTokens) => new ExpandToken(termTokens), new Func<List<ExpandTermToken>>(this.ParseSingleExpandTerm));
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00034744 File Offset: 0x00032944
		private SelectTermToken ParseSingleSelectTerm()
		{
			this.isSelect = true;
			SelectExpandTermParser selectExpandTermParser = new SelectExpandTermParser(this.lexer, this.MaxPathDepth, this.isSelect);
			PathSegmentToken pathSegmentToken = selectExpandTermParser.ParseTerm(false);
			string text = null;
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				text = this.lexer.AdvanceThroughBalancedParentheticalExpression();
				this.lexer.NextToken();
			}
			return this.SelectExpandOptionParser.BuildSelectTermToken(pathSegmentToken, text);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000347B4 File Offset: 0x000329B4
		private List<ExpandTermToken> ParseSingleExpandTerm()
		{
			this.isSelect = false;
			SelectExpandTermParser selectExpandTermParser = new SelectExpandTermParser(this.lexer, this.MaxPathDepth, this.isSelect);
			PathSegmentToken pathSegmentToken = selectExpandTermParser.ParseTerm(true);
			string text = null;
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				text = this.lexer.AdvanceThroughBalancedParentheticalExpression();
				this.lexer.NextToken();
			}
			return this.SelectExpandOptionParser.BuildExpandTermToken(pathSegmentToken, text);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00034824 File Offset: 0x00032A24
		private ExpandToken ParseCommaSeperatedExpandList(Func<IEnumerable<ExpandTermToken>, ExpandToken> ctor, Func<List<ExpandTermToken>> termParsingFunc)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			List<ExpandTermToken> list2 = new List<ExpandTermToken>();
			if (this.lexer == null)
			{
				return ctor(list);
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
			{
				return ctor(list);
			}
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Star)
			{
				list2 = termParsingFunc();
			}
			else
			{
				list.AddRange(termParsingFunc());
			}
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Comma)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.lexer.CurrentToken.Kind != ExpressionTokenKind.Star)
				{
					list.AddRange(termParsingFunc());
				}
				else
				{
					if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Star)
					{
						break;
					}
					if (list2.Count > 0)
					{
						throw new ODataException(Strings.UriExpandParser_TermWithMultipleStarNotAllowed(this.lexer.ExpressionText));
					}
					list2 = termParsingFunc();
				}
			}
			if (list2.Count > 0)
			{
				List<string> list3 = new List<string>();
				foreach (ExpandTermToken expandTermToken in list)
				{
					PathSegmentToken pathToNavigationProp = expandTermToken.PathToNavigationProp;
					if (pathToNavigationProp.Identifier != "$ref")
					{
						list3.Add(pathToNavigationProp.Identifier);
					}
					else
					{
						list3.Add(pathToNavigationProp.NextToken.Identifier);
					}
				}
				foreach (ExpandTermToken expandTermToken2 in list2)
				{
					PathSegmentToken pathToNavigationProp2 = expandTermToken2.PathToNavigationProp;
					if (pathToNavigationProp2.Identifier != "$ref" && !list3.Contains(pathToNavigationProp2.Identifier))
					{
						list.Add(expandTermToken2);
					}
					else if (pathToNavigationProp2.Identifier == "$ref" && !list3.Contains(pathToNavigationProp2.NextToken.Identifier))
					{
						list.Add(expandTermToken2);
					}
				}
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			return ctor(list);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00034A8C File Offset: 0x00032C8C
		private SelectToken ParseCommaSeperatedSelectList(Func<IEnumerable<SelectTermToken>, SelectToken> ctor, Func<SelectTermToken> termParsingFunc)
		{
			List<SelectTermToken> list = new List<SelectTermToken>();
			if (this.lexer == null)
			{
				return ctor(list);
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
			{
				return ctor(list);
			}
			list.Add(termParsingFunc());
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Comma)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
				{
					break;
				}
				list.Add(termParsingFunc());
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			return ctor(list);
		}

		// Token: 0x04000825 RID: 2085
		private readonly ODataUriResolver resolver;

		// Token: 0x04000826 RID: 2086
		private readonly IEdmStructuredType parentStructuredType;

		// Token: 0x04000827 RID: 2087
		private readonly int maxRecursiveDepth;

		// Token: 0x04000828 RID: 2088
		private readonly bool enableNoDollarQueryOptions;

		// Token: 0x04000829 RID: 2089
		private readonly bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x0400082A RID: 2090
		private SelectExpandOptionParser selectExpandOptionParser;

		// Token: 0x0400082B RID: 2091
		private ExpressionLexer lexer;

		// Token: 0x0400082C RID: 2092
		private bool isSelect;
	}
}
