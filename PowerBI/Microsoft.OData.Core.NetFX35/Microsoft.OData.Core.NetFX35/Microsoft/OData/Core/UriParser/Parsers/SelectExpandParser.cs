using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000212 RID: 530
	internal sealed class SelectExpandParser
	{
		// Token: 0x0600134F RID: 4943 RVA: 0x00046698 File Offset: 0x00044898
		public SelectExpandParser(string clauseToParse, int maxRecursiveDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
		{
			this.maxRecursiveDepth = maxRecursiveDepth;
			this.MaxPathDepth = maxRecursiveDepth;
			this.MaxFilterDepth = maxRecursiveDepth;
			this.MaxOrderByDepth = maxRecursiveDepth;
			this.MaxSearchDepth = maxRecursiveDepth;
			this.lexer = ((clauseToParse != null) ? new ExpressionLexer(clauseToParse, false, false) : null);
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000466E9 File Offset: 0x000448E9
		public SelectExpandParser(ODataUriResolver resolver, string clauseToParse, IEdmStructuredType parentEntityType, int maxRecursiveDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
			: this(clauseToParse, maxRecursiveDepth, enableCaseInsensitiveBuiltinIdentifier)
		{
			this.resolver = resolver;
			this.parentEntityType = parentEntityType;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00046704 File Offset: 0x00044904
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x0004670C File Offset: 0x0004490C
		internal int MaxPathDepth { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00046715 File Offset: 0x00044915
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x0004671D File Offset: 0x0004491D
		internal int MaxFilterDepth { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00046726 File Offset: 0x00044926
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x0004672E File Offset: 0x0004492E
		internal int MaxOrderByDepth { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00046737 File Offset: 0x00044937
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x0004673F File Offset: 0x0004493F
		internal int MaxSearchDepth { get; set; }

		// Token: 0x06001359 RID: 4953 RVA: 0x00046750 File Offset: 0x00044950
		public SelectToken ParseSelect()
		{
			this.isSelect = true;
			return this.ParseCommaSeperatedSelectList((IEnumerable<PathSegmentToken> termTokens) => new SelectToken(termTokens), new Func<PathSegmentToken>(this.ParseSingleSelectTerm));
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00046790 File Offset: 0x00044990
		public ExpandToken ParseExpand()
		{
			this.isSelect = false;
			return this.ParseCommaSeperatedExpandList((IEnumerable<ExpandTermToken> termTokens) => new ExpandToken(termTokens), new Func<List<ExpandTermToken>>(this.ParseSingleExpandTerm));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000467C8 File Offset: 0x000449C8
		private PathSegmentToken ParseSingleSelectTerm()
		{
			this.isSelect = true;
			SelectExpandTermParser selectExpandTermParser = new SelectExpandTermParser(this.lexer, this.MaxPathDepth, this.isSelect);
			return selectExpandTermParser.ParseTerm(false);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000467FC File Offset: 0x000449FC
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
				this.expandOptionParser = new ExpandOptionParser(this.resolver, this.parentEntityType, this.maxRecursiveDepth, this.enableCaseInsensitiveBuiltinIdentifier)
				{
					MaxFilterDepth = this.MaxFilterDepth,
					MaxOrderByDepth = this.MaxOrderByDepth,
					MaxSearchDepth = this.MaxSearchDepth
				};
			}
			return this.expandOptionParser.BuildExpandTermToken(pathSegmentToken, text);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000468BC File Offset: 0x00044ABC
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
					PathSegmentToken pathToNavProp = expandTermToken.PathToNavProp;
					if (pathToNavProp.Identifier != "$ref")
					{
						list3.Add(pathToNavProp.Identifier);
					}
					else
					{
						list3.Add(pathToNavProp.NextToken.Identifier);
					}
				}
				foreach (ExpandTermToken expandTermToken2 in list2)
				{
					PathSegmentToken pathToNavProp2 = expandTermToken2.PathToNavProp;
					if (pathToNavProp2.Identifier != "$ref" && !list3.Contains(pathToNavProp2.Identifier))
					{
						list.Add(expandTermToken2);
					}
					else if (pathToNavProp2.Identifier == "$ref" && !list3.Contains(pathToNavProp2.NextToken.Identifier))
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

		// Token: 0x0600135E RID: 4958 RVA: 0x00046B24 File Offset: 0x00044D24
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

		// Token: 0x04000837 RID: 2103
		private readonly ODataUriResolver resolver;

		// Token: 0x04000838 RID: 2104
		private readonly IEdmStructuredType parentEntityType;

		// Token: 0x04000839 RID: 2105
		private readonly int maxRecursiveDepth;

		// Token: 0x0400083A RID: 2106
		private ExpandOptionParser expandOptionParser;

		// Token: 0x0400083B RID: 2107
		private ExpressionLexer lexer;

		// Token: 0x0400083C RID: 2108
		private bool isSelect;

		// Token: 0x0400083D RID: 2109
		private bool enableCaseInsensitiveBuiltinIdentifier;
	}
}
