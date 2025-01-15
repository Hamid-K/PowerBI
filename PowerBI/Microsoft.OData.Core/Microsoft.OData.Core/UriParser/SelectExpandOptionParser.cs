using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000160 RID: 352
	internal sealed class SelectExpandOptionParser
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x00034B53 File Offset: 0x00032D53
		internal SelectExpandOptionParser(int maxRecursionDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
		{
			this.maxRecursionDepth = maxRecursionDepth;
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
			this.enableNoDollarQueryOptions = enableNoDollarQueryOptions;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00034B70 File Offset: 0x00032D70
		internal SelectExpandOptionParser(ODataUriResolver resolver, IEdmStructuredType parentStructuredType, int maxRecursionDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
			: this(maxRecursionDepth, enableCaseInsensitiveBuiltinIdentifier, enableNoDollarQueryOptions)
		{
			this.resolver = resolver;
			this.parentStructuredType = parentStructuredType;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00034B8B File Offset: 0x00032D8B
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00034B93 File Offset: 0x00032D93
		internal int MaxFilterDepth { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00034B9C File Offset: 0x00032D9C
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x00034BA4 File Offset: 0x00032DA4
		internal int MaxOrderByDepth { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00034BAD File Offset: 0x00032DAD
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x00034BB5 File Offset: 0x00032DB5
		internal int MaxSearchDepth { get; set; }

		// Token: 0x060011F8 RID: 4600 RVA: 0x00034BC0 File Offset: 0x00032DC0
		internal SelectTermToken BuildSelectTermToken(PathSegmentToken pathToken, string optionsText)
		{
			this.lexer = new ExpressionLexer(optionsText ?? "", true, true);
			QueryToken queryToken = null;
			IEnumerable<OrderByToken> enumerable = null;
			long? num = null;
			long? num2 = null;
			bool? flag = null;
			QueryToken queryToken2 = null;
			SelectToken selectToken = null;
			ComputeToken computeToken = null;
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
				{
					throw new ODataException(Strings.UriParser_MissingSelectOption(pathToken.Identifier));
				}
				while (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
				{
					string text = (this.enableCaseInsensitiveBuiltinIdentifier ? this.lexer.CurrentToken.Text.ToLowerInvariant() : this.lexer.CurrentToken.Text);
					if (this.enableNoDollarQueryOptions && !text.StartsWith("$", StringComparison.Ordinal))
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[] { "$", text });
					}
					uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num3 <= 1469037347U)
					{
						if (num3 <= 789605668U)
						{
							if (num3 != 456103474U)
							{
								if (num3 == 789605668U)
								{
									if (text == "$orderby")
									{
										enumerable = this.ParseInnerOrderBy();
										continue;
									}
								}
							}
							else if (text == "$count")
							{
								flag = this.ParseInnerCount();
								continue;
							}
						}
						else if (num3 != 1211134378U)
						{
							if (num3 == 1469037347U)
							{
								if (text == "$select")
								{
									selectToken = this.ParseInnerSelect(pathToken);
									continue;
								}
							}
						}
						else if (text == "$top")
						{
							num = this.ParseInnerTop();
							continue;
						}
					}
					else if (num3 <= 3803867261U)
					{
						if (num3 != 2221740796U)
						{
							if (num3 == 3803867261U)
							{
								if (text == "$filter")
								{
									queryToken = this.ParseInnerFilter();
									continue;
								}
							}
						}
						else if (text == "$compute")
						{
							computeToken = this.ParseInnerCompute();
							continue;
						}
					}
					else if (num3 != 3922661099U)
					{
						if (num3 == 4027612776U)
						{
							if (text == "$skip")
							{
								num2 = this.ParseInnerSkip();
								continue;
							}
						}
					}
					else if (text == "$search")
					{
						queryToken2 = this.ParseInnerSearch();
						continue;
					}
					throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
				}
				this.lexer.NextToken();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			return new SelectTermToken(pathToken, queryToken, enumerable, num, num2, flag, queryToken2, selectToken, computeToken);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00034E98 File Offset: 0x00033098
		internal List<ExpandTermToken> BuildExpandTermToken(PathSegmentToken pathToken, string optionsText)
		{
			this.lexer = new ExpressionLexer(optionsText ?? "", true, true);
			if (pathToken.Identifier == "*" || (pathToken.Identifier == "$ref" && pathToken.NextToken.Identifier == "*"))
			{
				return this.BuildStarExpandTermToken(pathToken);
			}
			QueryToken queryToken = null;
			IEnumerable<OrderByToken> enumerable = null;
			long? num = null;
			long? num2 = null;
			bool? flag = null;
			long? num3 = null;
			QueryToken queryToken2 = null;
			SelectToken selectToken = null;
			ExpandToken expandToken = null;
			ComputeToken computeToken = null;
			IEnumerable<QueryToken> enumerable2 = null;
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
					if (this.enableNoDollarQueryOptions && !text.StartsWith("$", StringComparison.Ordinal))
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[] { "$", text });
					}
					uint num4 = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num4 <= 2110202789U)
					{
						if (num4 <= 789605668U)
						{
							if (num4 != 456103474U)
							{
								if (num4 == 789605668U)
								{
									if (text == "$orderby")
									{
										enumerable = this.ParseInnerOrderBy();
										continue;
									}
								}
							}
							else if (text == "$count")
							{
								flag = this.ParseInnerCount();
								continue;
							}
						}
						else if (num4 != 1211134378U)
						{
							if (num4 != 1469037347U)
							{
								if (num4 == 2110202789U)
								{
									if (text == "$apply")
									{
										enumerable2 = this.ParseInnerApply();
										continue;
									}
								}
							}
							else if (text == "$select")
							{
								selectToken = this.ParseInnerSelect(pathToken);
								continue;
							}
						}
						else if (text == "$top")
						{
							num = this.ParseInnerTop();
							continue;
						}
					}
					else if (num4 <= 2703919376U)
					{
						if (num4 != 2221740796U)
						{
							if (num4 != 2649853531U)
							{
								if (num4 == 2703919376U)
								{
									if (text == "$levels")
									{
										num3 = this.ParseInnerLevel();
										continue;
									}
								}
							}
							else if (text == "$expand")
							{
								expandToken = this.ParseInnerExpand(pathToken);
								continue;
							}
						}
						else if (text == "$compute")
						{
							computeToken = this.ParseInnerCompute();
							continue;
						}
					}
					else if (num4 != 3803867261U)
					{
						if (num4 != 3922661099U)
						{
							if (num4 == 4027612776U)
							{
								if (text == "$skip")
								{
									num2 = this.ParseInnerSkip();
									continue;
								}
							}
						}
						else if (text == "$search")
						{
							queryToken2 = this.ParseInnerSearch();
							continue;
						}
					}
					else if (text == "$filter")
					{
						queryToken = this.ParseInnerFilter();
						continue;
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
			ExpandTermToken expandTermToken = new ExpandTermToken(pathToken, queryToken, enumerable, num, num2, flag, num3, queryToken2, selectToken, expandToken, computeToken, enumerable2);
			list.Add(expandTermToken);
			return list;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00035278 File Offset: 0x00033478
		private List<ExpandTermToken> BuildStarExpandTermToken(PathSegmentToken pathToken)
		{
			if (this.parentStructuredType == null)
			{
				throw new ODataException(Strings.UriExpandParser_ParentStructuredTypeIsNull(this.lexer.ExpressionText));
			}
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			long? num = null;
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
					if (!(text == "$levels"))
					{
						throw new ODataException(Strings.UriExpandParser_TermIsNotValidForStar(this.lexer.ExpressionText));
					}
					if (flag)
					{
						throw new ODataException(Strings.UriExpandParser_TermIsNotValidForStarRef(this.lexer.ExpressionText));
					}
					num = this.ParseInnerLevel();
				}
				this.lexer.NextToken();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.lexer.ExpressionText));
			}
			foreach (IEdmNavigationProperty edmNavigationProperty in this.parentStructuredType.NavigationProperties())
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
				ExpandTermToken expandTermToken = new ExpandTermToken(pathSegmentToken, null, null, null, null, null, num, null, null, null, null, null);
				list.Add(expandTermToken);
			}
			return list;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000354A8 File Offset: 0x000336A8
		private QueryToken ParseInnerFilter()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.MaxFilterDepth, this.enableCaseInsensitiveBuiltinIdentifier);
			return uriQueryExpressionParser.ParseFilter(text);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000354E4 File Offset: 0x000336E4
		private IEnumerable<OrderByToken> ParseInnerOrderBy()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.MaxOrderByDepth, this.enableCaseInsensitiveBuiltinIdentifier);
			return uriQueryExpressionParser.ParseOrderBy(text);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00035520 File Offset: 0x00033720
		private long? ParseInnerTop()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			long num;
			if (!long.TryParse(text, out num) || num < 0L)
			{
				throw new ODataException(Strings.UriSelectParser_InvalidTopOption(text));
			}
			return new long?(num);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00035564 File Offset: 0x00033764
		private long? ParseInnerSkip()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			long num;
			if (!long.TryParse(text, out num) || num < 0L)
			{
				throw new ODataException(Strings.UriSelectParser_InvalidSkipOption(text));
			}
			return new long?(num);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000355A8 File Offset: 0x000337A8
		private bool? ParseInnerCount()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			if (text == "true")
			{
				return new bool?(true);
			}
			if (!(text == "false"))
			{
				throw new ODataException(Strings.UriSelectParser_InvalidCountOption(text));
			}
			return new bool?(false);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00035600 File Offset: 0x00033800
		private QueryToken ParseInnerSearch()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			SearchParser searchParser = new SearchParser(this.MaxSearchDepth);
			return searchParser.ParseSearch(text);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00035634 File Offset: 0x00033834
		private SelectToken ParseInnerSelect(PathSegmentToken pathToken)
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			IEdmStructuredType edmStructuredType = null;
			if (this.resolver != null && this.parentStructuredType != null)
			{
				IEdmProperty edmProperty = this.resolver.ResolveProperty(this.parentStructuredType, pathToken.Identifier);
				if (edmProperty != null)
				{
					edmStructuredType = edmProperty.Type.ToStructuredType();
				}
			}
			SelectExpandParser selectExpandParser = new SelectExpandParser(this.resolver, text, edmStructuredType, this.maxRecursionDepth - 1, this.enableCaseInsensitiveBuiltinIdentifier, this.enableNoDollarQueryOptions);
			return selectExpandParser.ParseSelect();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000356B8 File Offset: 0x000338B8
		private ExpandToken ParseInnerExpand(PathSegmentToken pathToken)
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			IEdmStructuredType edmStructuredType = null;
			if (this.resolver != null && this.parentStructuredType != null)
			{
				IEdmProperty edmProperty = this.resolver.ResolveProperty(this.parentStructuredType, pathToken.Identifier);
				if (edmProperty != null)
				{
					edmStructuredType = edmProperty.Type.ToStructuredType();
				}
			}
			SelectExpandParser selectExpandParser = new SelectExpandParser(this.resolver, text, edmStructuredType, this.maxRecursionDepth - 1, this.enableCaseInsensitiveBuiltinIdentifier, this.enableNoDollarQueryOptions);
			return selectExpandParser.ParseExpand();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0003573C File Offset: 0x0003393C
		private long? ParseInnerLevel()
		{
			long? num = null;
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			if (string.Equals("max", text, this.enableCaseInsensitiveBuiltinIdentifier ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
			{
				num = new long?(long.MinValue);
			}
			else
			{
				long num2;
				if (!long.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out num2) || num2 < 0L)
				{
					throw new ODataException(Strings.UriSelectParser_InvalidLevelsOption(text));
				}
				num = new long?(num2);
			}
			return num;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000357BC File Offset: 0x000339BC
		private ComputeToken ParseInnerCompute()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.MaxOrderByDepth, this.enableCaseInsensitiveBuiltinIdentifier);
			return uriQueryExpressionParser.ParseCompute(text);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000357F8 File Offset: 0x000339F8
		private IEnumerable<QueryToken> ParseInnerApply()
		{
			this.lexer.NextToken();
			string text = this.ReadQueryOption();
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.MaxOrderByDepth, this.enableCaseInsensitiveBuiltinIdentifier);
			return uriQueryExpressionParser.ParseApply(text);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00035834 File Offset: 0x00033A34
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

		// Token: 0x04000831 RID: 2097
		private readonly ODataUriResolver resolver;

		// Token: 0x04000832 RID: 2098
		private readonly IEdmStructuredType parentStructuredType;

		// Token: 0x04000833 RID: 2099
		private readonly int maxRecursionDepth;

		// Token: 0x04000834 RID: 2100
		private readonly bool enableNoDollarQueryOptions;

		// Token: 0x04000835 RID: 2101
		private readonly bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x04000836 RID: 2102
		private ExpressionLexer lexer;
	}
}
