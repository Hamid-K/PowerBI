using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011C RID: 284
	internal sealed class SelectExpandParser
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x000263B0 File Offset: 0x000245B0
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

		// Token: 0x06000D2F RID: 3375 RVA: 0x00026409 File Offset: 0x00024609
		public SelectExpandParser(ODataUriResolver resolver, string clauseToParse, IEdmStructuredType parentEntityType, int maxRecursiveDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
			: this(clauseToParse, maxRecursiveDepth, enableCaseInsensitiveBuiltinIdentifier, enableNoDollarQueryOptions)
		{
			this.resolver = resolver;
			this.parentEntityType = parentEntityType;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00026426 File Offset: 0x00024626
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0002642E File Offset: 0x0002462E
		internal int MaxPathDepth { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00026437 File Offset: 0x00024637
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0002643F File Offset: 0x0002463F
		internal int MaxFilterDepth { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00026448 File Offset: 0x00024648
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00026450 File Offset: 0x00024650
		internal int MaxOrderByDepth { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00026459 File Offset: 0x00024659
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00026461 File Offset: 0x00024661
		internal int MaxSearchDepth { get; set; }

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002646A File Offset: 0x0002466A
		public SelectToken ParseSelect()
		{
			this.isSelect = true;
			return this.ParseCommaSeperatedSelectList((IEnumerable<PathSegmentToken> termTokens) => new SelectToken(termTokens), new Func<PathSegmentToken>(this.ParseSingleSelectTerm));
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000264A4 File Offset: 0x000246A4
		public ExpandToken ParseExpand()
		{
			this.isSelect = false;
			return this.ParseCommaSeperatedExpandList((IEnumerable<ExpandTermToken> termTokens) => new ExpandToken(termTokens), new Func<List<ExpandTermToken>>(this.ParseSingleExpandTerm));
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000264E0 File Offset: 0x000246E0
		private PathSegmentToken ParseSingleSelectTerm()
		{
			this.isSelect = true;
			SelectExpandTermParser selectExpandTermParser = new SelectExpandTermParser(this.lexer, this.MaxPathDepth, this.isSelect);
			return selectExpandTermParser.ParseTerm(false);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00026514 File Offset: 0x00024714
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
			if (this.expandOptionParser == null)
			{
				this.expandOptionParser = new ExpandOptionParser(this.resolver, this.parentEntityType, this.maxRecursiveDepth, this.enableCaseInsensitiveBuiltinIdentifier, this.enableNoDollarQueryOptions)
				{
					MaxFilterDepth = this.MaxFilterDepth,
					MaxOrderByDepth = this.MaxOrderByDepth,
					MaxSearchDepth = this.MaxSearchDepth
				};
			}
			return this.expandOptionParser.BuildExpandTermToken(pathSegmentToken, text);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000265D8 File Offset: 0x000247D8
		private ExpandToken ParseCommaSeperatedExpandList(Func<IEnumerable<ExpandTermToken>, ExpandToken> ctor, Func<List<ExpandTermToken>> termParsingFunc)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			List<ExpandTermToken> list2 = new List<ExpandTermToken>();
			if (this.lexer == null)
			{
				return ctor.Invoke(list);
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
			{
				return ctor.Invoke(list);
			}
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Star)
			{
				list2 = termParsingFunc.Invoke();
			}
			else
			{
				list.AddRange(termParsingFunc.Invoke());
			}
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Comma)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.lexer.CurrentToken.Kind != ExpressionTokenKind.Star)
				{
					list.AddRange(termParsingFunc.Invoke());
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
					list2 = termParsingFunc.Invoke();
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
			return ctor.Invoke(list);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00026840 File Offset: 0x00024A40
		private SelectToken ParseCommaSeperatedSelectList(Func<IEnumerable<PathSegmentToken>, SelectToken> ctor, Func<PathSegmentToken> termParsingFunc)
		{
			List<PathSegmentToken> list = new List<PathSegmentToken>();
			if (this.lexer == null)
			{
				return ctor.Invoke(list);
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
			{
				return ctor.Invoke(list);
			}
			list.Add(termParsingFunc.Invoke());
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Comma)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
				{
					break;
				}
				list.Add(termParsingFunc.Invoke());
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			return ctor.Invoke(list);
		}

		// Token: 0x0400070F RID: 1807
		private readonly ODataUriResolver resolver;

		// Token: 0x04000710 RID: 1808
		private readonly IEdmStructuredType parentEntityType;

		// Token: 0x04000711 RID: 1809
		private readonly int maxRecursiveDepth;

		// Token: 0x04000712 RID: 1810
		private readonly bool enableNoDollarQueryOptions;

		// Token: 0x04000713 RID: 1811
		private readonly bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x04000714 RID: 1812
		private ExpandOptionParser expandOptionParser;

		// Token: 0x04000715 RID: 1813
		private ExpressionLexer lexer;

		// Token: 0x04000716 RID: 1814
		private bool isSelect;
	}
}
