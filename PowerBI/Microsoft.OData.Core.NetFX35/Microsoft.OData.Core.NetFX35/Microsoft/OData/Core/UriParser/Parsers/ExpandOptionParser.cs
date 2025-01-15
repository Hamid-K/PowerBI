using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001F7 RID: 503
	internal sealed class ExpandOptionParser
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x0004249C File Offset: 0x0004069C
		internal ExpandOptionParser(int maxRecursionDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
		{
			this.maxRecursionDepth = maxRecursionDepth;
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000424B2 File Offset: 0x000406B2
		internal ExpandOptionParser(ODataUriResolver resolver, IEdmStructuredType parentEntityType, int maxRecursionDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
			: this(maxRecursionDepth, enableCaseInsensitiveBuiltinIdentifier)
		{
			this.resolver = resolver;
			this.parentEntityType = parentEntityType;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x000424CB File Offset: 0x000406CB
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x000424D3 File Offset: 0x000406D3
		internal int MaxFilterDepth { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x000424DC File Offset: 0x000406DC
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x000424E4 File Offset: 0x000406E4
		internal int MaxOrderByDepth { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x000424ED File Offset: 0x000406ED
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x000424F5 File Offset: 0x000406F5
		internal int MaxSearchDepth { get; set; }

		// Token: 0x06001270 RID: 4720 RVA: 0x00042500 File Offset: 0x00040700
		internal List<ExpandTermToken> BuildExpandTermToken(PathSegmentToken pathToken, string optionsText)
		{
			this.lexer = new ExpressionLexer(optionsText ?? "", true, true);
			if (pathToken.Identifier == "*" || (pathToken.Identifier == "$ref" && pathToken.NextToken.Identifier == "*"))
			{
				return this.BuildStarExpandTermToken(pathToken);
			}
			QueryToken queryToken = null;
			IEnumerable<OrderByToken> enumerable = null;
			long? num = default(long?);
			long? num2 = default(long?);
			bool? flag = default(bool?);
			long? num3 = default(long?);
			QueryToken queryToken2 = null;
			SelectToken selectToken = null;
			ExpandToken expandToken = null;
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
				{
					throw new ODataException(Strings.UriParser_MissingExpandOption(pathToken.Identifier));
				}
				while (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
				{
					string text = (this.enableCaseInsensitiveBuiltinIdentifier ? this.lexer.CurrentToken.Text.ToLowerInvariant() : this.lexer.CurrentToken.Text);
					string text2;
					if ((text2 = text) != null)
					{
						if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60011de-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
							dictionary.Add("$filter", 0);
							dictionary.Add("$orderby", 1);
							dictionary.Add("$top", 2);
							dictionary.Add("$skip", 3);
							dictionary.Add("$count", 4);
							dictionary.Add("$levels", 5);
							dictionary.Add("$search", 6);
							dictionary.Add("$select", 7);
							dictionary.Add("$expand", 8);
							<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60011de-1 = dictionary;
						}
						int num4;
						if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60011de-1.TryGetValue(text2, ref num4))
						{
							switch (num4)
							{
							case 0:
							{
								this.lexer.NextToken();
								string text3 = this.ReadQueryOption();
								UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.MaxFilterDepth, this.enableCaseInsensitiveBuiltinIdentifier);
								queryToken = uriQueryExpressionParser.ParseFilter(text3);
								continue;
							}
							case 1:
							{
								this.lexer.NextToken();
								string text4 = this.ReadQueryOption();
								UriQueryExpressionParser uriQueryExpressionParser2 = new UriQueryExpressionParser(this.MaxOrderByDepth, this.enableCaseInsensitiveBuiltinIdentifier);
								enumerable = uriQueryExpressionParser2.ParseOrderBy(text4);
								continue;
							}
							case 2:
							{
								this.lexer.NextToken();
								string text5 = this.ReadQueryOption();
								long num5;
								if (!long.TryParse(text5, ref num5) || num5 < 0L)
								{
									throw new ODataException(Strings.UriSelectParser_InvalidTopOption(text5));
								}
								num = new long?(num5);
								continue;
							}
							case 3:
							{
								this.lexer.NextToken();
								string text6 = this.ReadQueryOption();
								long num6;
								if (!long.TryParse(text6, ref num6) || num6 < 0L)
								{
									throw new ODataException(Strings.UriSelectParser_InvalidSkipOption(text6));
								}
								num2 = new long?(num6);
								continue;
							}
							case 4:
							{
								this.lexer.NextToken();
								string text7 = this.ReadQueryOption();
								string text8;
								if ((text8 = text7) != null)
								{
									if (text8 == "true")
									{
										flag = new bool?(true);
										continue;
									}
									if (text8 == "false")
									{
										flag = new bool?(false);
										continue;
									}
								}
								throw new ODataException(Strings.UriSelectParser_InvalidCountOption(text7));
							}
							case 5:
								num3 = this.ResolveLevelOption();
								continue;
							case 6:
							{
								this.lexer.NextToken();
								string text9 = this.ReadQueryOption();
								SearchParser searchParser = new SearchParser(this.MaxSearchDepth);
								queryToken2 = searchParser.ParseSearch(text9);
								continue;
							}
							case 7:
							{
								this.lexer.NextToken();
								string text10 = this.ReadQueryOption();
								SelectExpandParser selectExpandParser = new SelectExpandParser(text10, this.maxRecursionDepth, this.enableCaseInsensitiveBuiltinIdentifier);
								selectToken = selectExpandParser.ParseSelect();
								continue;
							}
							case 8:
							{
								this.lexer.NextToken();
								string text11 = this.ReadQueryOption();
								IEdmStructuredType edmStructuredType = null;
								if (this.resolver != null && this.parentEntityType != null)
								{
									IEdmNavigationProperty edmNavigationProperty = this.resolver.ResolveProperty(this.parentEntityType, pathToken.Identifier) as IEdmNavigationProperty;
									if (edmNavigationProperty != null)
									{
										edmStructuredType = edmNavigationProperty.ToEntityType();
									}
								}
								SelectExpandParser selectExpandParser2 = new SelectExpandParser(this.resolver, text11, edmStructuredType, this.maxRecursionDepth - 1, this.enableCaseInsensitiveBuiltinIdentifier);
								expandToken = selectExpandParser2.ParseExpand();
								continue;
							}
							}
						}
					}
					throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
				}
				this.lexer.NextToken();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			ExpandTermToken expandTermToken = new ExpandTermToken(pathToken, queryToken, enumerable, num, num2, flag, num3, queryToken2, selectToken, expandToken);
			list.Add(expandTermToken);
			return list;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000429AC File Offset: 0x00040BAC
		private List<ExpandTermToken> BuildStarExpandTermToken(PathSegmentToken pathToken)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			long? num = default(long?);
			bool flag = pathToken.Identifier == "$ref";
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
				{
					throw new ODataException(Strings.UriParser_MissingExpandOption(pathToken.Identifier));
				}
				while (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
				{
					string text = (this.enableCaseInsensitiveBuiltinIdentifier ? this.lexer.CurrentToken.Text.ToLowerInvariant() : this.lexer.CurrentToken.Text);
					string text2;
					if ((text2 = text) == null || !(text2 == "$levels"))
					{
						throw new ODataException(Strings.UriExpandParser_TermIsNotValidForStar(this.lexer.ExpressionText));
					}
					if (flag)
					{
						throw new ODataException(Strings.UriExpandParser_TermIsNotValidForStarRef(this.lexer.ExpressionText));
					}
					num = this.ResolveLevelOption();
				}
				this.lexer.NextToken();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			IEdmEntityType edmEntityType = this.parentEntityType as IEdmEntityType;
			if (edmEntityType == null)
			{
				throw new ODataException(Strings.UriExpandParser_ParentEntityIsNull(this.lexer.ExpressionText));
			}
			foreach (IEdmNavigationProperty edmNavigationProperty in edmEntityType.NavigationProperties())
			{
				PathSegmentToken pathSegmentToken;
				if (pathToken.Identifier.Equals("$ref"))
				{
					pathSegmentToken = new NonSystemToken(edmNavigationProperty.Name, null, pathToken.NextToken.NextToken);
					pathSegmentToken = new NonSystemToken("$ref", null, pathSegmentToken);
				}
				else
				{
					pathSegmentToken = new NonSystemToken(edmNavigationProperty.Name, null, pathToken.NextToken);
				}
				ExpandTermToken expandTermToken = new ExpandTermToken(pathSegmentToken, null, null, default(long?), default(long?), default(bool?), num, null, null, null);
				list.Add(expandTermToken);
			}
			return list;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00042BE4 File Offset: 0x00040DE4
		private long? ResolveLevelOption()
		{
			long? num = default(long?);
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			if (string.Equals("max", text, this.enableCaseInsensitiveBuiltinIdentifier ? 5 : 4))
			{
				num = new long?(long.MinValue);
			}
			else
			{
				long num2;
				if (!long.TryParse(text, 0, CultureInfo.InvariantCulture, ref num2) || num2 < 0L)
				{
					throw new ODataException(Strings.UriSelectParser_InvalidLevelsOption(text));
				}
				num = new long?(num2);
			}
			return num;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00042C64 File Offset: 0x00040E64
		private string ReadQueryOption()
		{
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Equal)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			string text = this.lexer.AdvanceThroughExpandOption();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.SemiColon)
			{
				this.lexer.NextToken();
				return text;
			}
			this.lexer.ValidateToken(ExpressionTokenKind.CloseParen);
			return text;
		}

		// Token: 0x040007F1 RID: 2033
		private readonly ODataUriResolver resolver;

		// Token: 0x040007F2 RID: 2034
		private readonly IEdmStructuredType parentEntityType;

		// Token: 0x040007F3 RID: 2035
		private readonly int maxRecursionDepth;

		// Token: 0x040007F4 RID: 2036
		private ExpressionLexer lexer;

		// Token: 0x040007F5 RID: 2037
		private bool enableCaseInsensitiveBuiltinIdentifier;
	}
}
