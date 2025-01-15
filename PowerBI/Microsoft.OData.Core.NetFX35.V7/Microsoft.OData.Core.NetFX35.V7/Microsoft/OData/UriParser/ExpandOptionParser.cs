using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010D RID: 269
	internal sealed class ExpandOptionParser
	{
		// Token: 0x06000CBC RID: 3260 RVA: 0x00023038 File Offset: 0x00021238
		internal ExpandOptionParser(int maxRecursionDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
		{
			this.maxRecursionDepth = maxRecursionDepth;
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
			this.enableNoDollarQueryOptions = enableNoDollarQueryOptions;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00023055 File Offset: 0x00021255
		internal ExpandOptionParser(ODataUriResolver resolver, IEdmStructuredType parentEntityType, int maxRecursionDepth, bool enableCaseInsensitiveBuiltinIdentifier = false, bool enableNoDollarQueryOptions = false)
			: this(maxRecursionDepth, enableCaseInsensitiveBuiltinIdentifier, enableNoDollarQueryOptions)
		{
			this.resolver = resolver;
			this.parentEntityType = parentEntityType;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00023070 File Offset: 0x00021270
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00023078 File Offset: 0x00021278
		internal int MaxFilterDepth { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00023081 File Offset: 0x00021281
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00023089 File Offset: 0x00021289
		internal int MaxOrderByDepth { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00023092 File Offset: 0x00021292
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0002309A File Offset: 0x0002129A
		internal int MaxSearchDepth { get; set; }

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000230A4 File Offset: 0x000212A4
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
			ComputeToken computeToken = null;
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
					if (this.enableNoDollarQueryOptions && !text.StartsWith("$", 4))
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[] { "$", text });
					}
					uint num4 = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num4 <= 2221740796U)
					{
						if (num4 <= 789605668U)
						{
							if (num4 != 456103474U)
							{
								if (num4 == 789605668U)
								{
									if (text == "$orderby")
									{
										this.lexer.NextToken();
										string text2 = this.ReadQueryOption();
										UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.MaxOrderByDepth, this.enableCaseInsensitiveBuiltinIdentifier);
										enumerable = uriQueryExpressionParser.ParseOrderBy(text2);
										continue;
									}
								}
							}
							else if (text == "$count")
							{
								this.lexer.NextToken();
								string text3 = this.ReadQueryOption();
								if (text3 == "true")
								{
									flag = new bool?(true);
									continue;
								}
								if (!(text3 == "false"))
								{
									throw new ODataException(Strings.UriSelectParser_InvalidCountOption(text3));
								}
								flag = new bool?(false);
								continue;
							}
						}
						else if (num4 != 1211134378U)
						{
							if (num4 != 1469037347U)
							{
								if (num4 == 2221740796U)
								{
									if (text == "$compute")
									{
										this.lexer.NextToken();
										string text4 = this.ReadQueryOption();
										UriQueryExpressionParser uriQueryExpressionParser2 = new UriQueryExpressionParser(this.MaxOrderByDepth, this.enableCaseInsensitiveBuiltinIdentifier);
										computeToken = uriQueryExpressionParser2.ParseCompute(text4);
										continue;
									}
								}
							}
							else if (text == "$select")
							{
								this.lexer.NextToken();
								string text5 = this.ReadQueryOption();
								SelectExpandParser selectExpandParser = new SelectExpandParser(text5, this.maxRecursionDepth, this.enableCaseInsensitiveBuiltinIdentifier, false);
								selectToken = selectExpandParser.ParseSelect();
								continue;
							}
						}
						else if (text == "$top")
						{
							this.lexer.NextToken();
							string text6 = this.ReadQueryOption();
							long num5;
							if (!long.TryParse(text6, ref num5) || num5 < 0L)
							{
								throw new ODataException(Strings.UriSelectParser_InvalidTopOption(text6));
							}
							num = new long?(num5);
							continue;
						}
					}
					else if (num4 <= 2703919376U)
					{
						if (num4 != 2649853531U)
						{
							if (num4 == 2703919376U)
							{
								if (text == "$levels")
								{
									num3 = this.ResolveLevelOption();
									continue;
								}
							}
						}
						else if (text == "$expand")
						{
							this.lexer.NextToken();
							string text7 = this.ReadQueryOption();
							IEdmStructuredType edmStructuredType = null;
							if (this.resolver != null && this.parentEntityType != null)
							{
								IEdmNavigationProperty edmNavigationProperty = this.resolver.ResolveProperty(this.parentEntityType, pathToken.Identifier) as IEdmNavigationProperty;
								if (edmNavigationProperty != null)
								{
									edmStructuredType = edmNavigationProperty.ToEntityType();
								}
							}
							SelectExpandParser selectExpandParser2 = new SelectExpandParser(this.resolver, text7, edmStructuredType, this.maxRecursionDepth - 1, this.enableCaseInsensitiveBuiltinIdentifier, this.enableNoDollarQueryOptions);
							expandToken = selectExpandParser2.ParseExpand();
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
									this.lexer.NextToken();
									string text8 = this.ReadQueryOption();
									long num6;
									if (!long.TryParse(text8, ref num6) || num6 < 0L)
									{
										throw new ODataException(Strings.UriSelectParser_InvalidSkipOption(text8));
									}
									num2 = new long?(num6);
									continue;
								}
							}
						}
						else if (text == "$search")
						{
							this.lexer.NextToken();
							string text9 = this.ReadQueryOption();
							SearchParser searchParser = new SearchParser(this.MaxSearchDepth);
							queryToken2 = searchParser.ParseSearch(text9);
							continue;
						}
					}
					else if (text == "$filter")
					{
						this.lexer.NextToken();
						string text10 = this.ReadQueryOption();
						UriQueryExpressionParser uriQueryExpressionParser3 = new UriQueryExpressionParser(this.MaxFilterDepth, this.enableCaseInsensitiveBuiltinIdentifier);
						queryToken = uriQueryExpressionParser3.ParseFilter(text10);
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
			ExpandTermToken expandTermToken = new ExpandTermToken(pathToken, queryToken, enumerable, num, num2, flag, num3, queryToken2, selectToken, expandToken, computeToken);
			list.Add(expandTermToken);
			return list;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00023678 File Offset: 0x00021878
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
					if (!(text == "$levels"))
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
				ExpandTermToken expandTermToken = new ExpandTermToken(pathSegmentToken, null, null, default(long?), default(long?), default(bool?), num, null, null, null, null);
				list.Add(expandTermToken);
			}
			return list;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000238A8 File Offset: 0x00021AA8
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

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00023928 File Offset: 0x00021B28
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

		// Token: 0x040006EF RID: 1775
		private readonly ODataUriResolver resolver;

		// Token: 0x040006F0 RID: 1776
		private readonly IEdmStructuredType parentEntityType;

		// Token: 0x040006F1 RID: 1777
		private readonly int maxRecursionDepth;

		// Token: 0x040006F2 RID: 1778
		private readonly bool enableNoDollarQueryOptions;

		// Token: 0x040006F3 RID: 1779
		private readonly bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x040006F4 RID: 1780
		private ExpressionLexer lexer;
	}
}
