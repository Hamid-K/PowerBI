using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000165 RID: 357
	public sealed class UriQueryExpressionParser
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x00036210 File Offset: 0x00034410
		public UriQueryExpressionParser(int maxDepth)
			: this(maxDepth, false)
		{
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0003621A File Offset: 0x0003441A
		internal UriQueryExpressionParser(int maxDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
		{
			this.maxDepth = maxDepth;
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00036257 File Offset: 0x00034457
		internal UriQueryExpressionParser(int maxDepth, ExpressionLexer lexer)
			: this(maxDepth)
		{
			this.lexer = lexer;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00036267 File Offset: 0x00034467
		internal ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0003626F File Offset: 0x0003446F
		private bool IsInAggregateExpression
		{
			get
			{
				return this.parseAggregateExpresionDepth > 0;
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0003627A File Offset: 0x0003447A
		public QueryToken ParseFilter(string filter)
		{
			return this.ParseExpressionText(filter);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00036284 File Offset: 0x00034484
		internal static LiteralToken TryParseLiteral(ExpressionLexer lexer)
		{
			switch (lexer.CurrentToken.Kind)
			{
			case ExpressionTokenKind.NullLiteral:
				return UriQueryExpressionParser.ParseNullLiteral(lexer);
			case ExpressionTokenKind.BooleanLiteral:
			case ExpressionTokenKind.StringLiteral:
			case ExpressionTokenKind.IntegerLiteral:
			case ExpressionTokenKind.Int64Literal:
			case ExpressionTokenKind.SingleLiteral:
			case ExpressionTokenKind.DateTimeOffsetLiteral:
			case ExpressionTokenKind.DurationLiteral:
			case ExpressionTokenKind.DecimalLiteral:
			case ExpressionTokenKind.DoubleLiteral:
			case ExpressionTokenKind.GuidLiteral:
			case ExpressionTokenKind.BinaryLiteral:
			case ExpressionTokenKind.GeographyLiteral:
			case ExpressionTokenKind.GeometryLiteral:
			case ExpressionTokenKind.QuotedLiteral:
			case ExpressionTokenKind.DateLiteral:
			case ExpressionTokenKind.TimeOfDayLiteral:
			case ExpressionTokenKind.CustomTypeLiteral:
			{
				IEdmTypeReference literalEdmTypeReference = lexer.CurrentToken.GetLiteralEdmTypeReference();
				string edmConstantNames = UriQueryExpressionParser.GetEdmConstantNames(literalEdmTypeReference);
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, literalEdmTypeReference, edmConstantNames);
			}
			case ExpressionTokenKind.BracedExpression:
			case ExpressionTokenKind.BracketedExpression:
			case ExpressionTokenKind.ParenthesesExpression:
			{
				LiteralToken literalToken = new LiteralToken(lexer.CurrentToken.Text, lexer.CurrentToken.Text);
				lexer.NextToken();
				return literalToken;
			}
			}
			return null;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00036380 File Offset: 0x00034580
		internal static string GetEdmConstantNames(IEdmTypeReference edmTypeReference)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmTypeReference.PrimitiveKind();
			switch (edmPrimitiveTypeKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return "Edm.Binary";
			case EdmPrimitiveTypeKind.Boolean:
				return "Edm.Boolean";
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Stream:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				break;
			case EdmPrimitiveTypeKind.DateTimeOffset:
				return "Edm.DateTimeOffset";
			case EdmPrimitiveTypeKind.Decimal:
				return "Edm.Decimal";
			case EdmPrimitiveTypeKind.Double:
				return "Edm.Double";
			case EdmPrimitiveTypeKind.Guid:
				return "Edm.Guid";
			case EdmPrimitiveTypeKind.Int32:
				return "Edm.Int32";
			case EdmPrimitiveTypeKind.Int64:
				return "Edm.Int64";
			case EdmPrimitiveTypeKind.Single:
				return "Edm.Single";
			case EdmPrimitiveTypeKind.String:
				return "Edm.String";
			case EdmPrimitiveTypeKind.Duration:
				return "Edm.Duration";
			case EdmPrimitiveTypeKind.Geography:
				return "Edm.Geography";
			case EdmPrimitiveTypeKind.Geometry:
				return "Edm.Geometry";
			default:
				if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Date)
				{
					return "Edm.Date";
				}
				if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.TimeOfDay)
				{
					return "Edm.TimeOfDay";
				}
				break;
			}
			return edmTypeReference.Definition.FullTypeName();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00036470 File Offset: 0x00034670
		internal ComputeToken ParseCompute()
		{
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			List<ComputeExpressionToken> list = new List<ComputeExpressionToken>();
			for (;;)
			{
				ComputeExpressionToken computeExpressionToken = this.ParseComputeExpression();
				list.Add(computeExpressionToken);
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return new ComputeToken(list);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003655C File Offset: 0x0003475C
		internal ComputeToken ParseCompute(string compute)
		{
			List<ComputeExpressionToken> list = new List<ComputeExpressionToken>();
			if (string.IsNullOrEmpty(compute))
			{
				return new ComputeToken(list);
			}
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(compute);
			for (;;)
			{
				ComputeExpressionToken computeExpressionToken = this.ParseComputeExpression();
				list.Add(computeExpressionToken);
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return new ComputeToken(list);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000365D4 File Offset: 0x000347D4
		internal ExpandToken ParseExpand()
		{
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			SelectExpandTermParser selectExpandTermParser = new SelectExpandTermParser(this.lexer, this.maxDepth - 1, false);
			PathSegmentToken pathSegmentToken = selectExpandTermParser.ParseTerm(true);
			QueryToken queryToken = null;
			ExpandToken expandToken = null;
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Comma)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Identifier)
				{
					string identifier = this.lexer.CurrentToken.GetIdentifier();
					if (!(identifier == "filter"))
					{
						if (!(identifier == "expand"))
						{
							throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_KeywordOrIdentifierExpected(UriQueryExpressionParser.supportedKeywords, this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
						}
						ExpandToken expandToken2 = this.ParseExpand();
						expandToken = ((expandToken == null) ? expandToken2 : new ExpandToken(expandToken.ExpandTerms.Concat(expandToken2.ExpandTerms)));
					}
					else
					{
						queryToken = this.ParseApplyFilter();
					}
				}
			}
			if (queryToken == null && expandToken == null)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_InnerMostExpandRequireFilter(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			ExpandTermToken expandTermToken = new ExpandTermToken(pathSegmentToken, queryToken, null, null, null, null, null, null, null, expandToken);
			list.Add(expandTermToken);
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return new ExpandToken(list);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000367F8 File Offset: 0x000349F8
		internal IEnumerable<QueryToken> ParseApply(string apply)
		{
			List<QueryToken> list = new List<QueryToken>();
			if (string.IsNullOrEmpty(apply))
			{
				return list;
			}
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(apply);
			for (;;)
			{
				string identifier = this.lexer.CurrentToken.GetIdentifier();
				if (!(identifier == "aggregate"))
				{
					if (!(identifier == "filter"))
					{
						if (!(identifier == "groupby"))
						{
							if (!(identifier == "compute"))
							{
								if (!(identifier == "expand"))
								{
									break;
								}
								list.Add(this.ParseExpand());
							}
							else
							{
								list.Add(this.ParseCompute());
							}
						}
						else
						{
							list.Add(this.ParseGroupBy());
						}
					}
					else
					{
						list.Add(this.ParseApplyFilter());
					}
				}
				else
				{
					list.Add(this.ParseAggregate());
				}
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Slash)
				{
					goto IL_0115;
				}
				this.lexer.NextToken();
			}
			throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_KeywordOrIdentifierExpected(UriQueryExpressionParser.supportedKeywords, this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			IL_0115:
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return new ReadOnlyCollection<QueryToken>(list);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0003692C File Offset: 0x00034B2C
		internal AggregateToken ParseAggregate()
		{
			this.lexer.NextToken();
			return new AggregateToken(this.ParseAggregateExpressions());
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00036948 File Offset: 0x00034B48
		internal List<AggregateTokenBase> ParseAggregateExpressions()
		{
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			List<AggregateTokenBase> list = new List<AggregateTokenBase>();
			for (;;)
			{
				list.Add(this.ParseAggregateExpression());
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return list;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00036A20 File Offset: 0x00034C20
		internal AggregateTokenBase ParseAggregateExpression()
		{
			AggregateTokenBase aggregateTokenBase;
			try
			{
				this.parseAggregateExpresionDepth++;
				QueryToken queryToken = this.ParseLogicalOr();
				if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
				{
					this.aggregateExpressionParents.Push(queryToken);
					List<AggregateTokenBase> list = this.ParseAggregateExpressions();
					this.aggregateExpressionParents.Pop();
					aggregateTokenBase = new EntitySetAggregateToken(queryToken, list);
				}
				else
				{
					EndPathToken endPathToken = queryToken as EndPathToken;
					AggregationMethodDefinition aggregationMethodDefinition;
					if (endPathToken != null && endPathToken.Identifier == "$count")
					{
						aggregationMethodDefinition = AggregationMethodDefinition.VirtualPropertyCount;
					}
					else
					{
						aggregationMethodDefinition = this.ParseAggregateWith();
					}
					StringLiteralToken stringLiteralToken = this.ParseAggregateAs();
					aggregateTokenBase = new AggregateExpressionToken(queryToken, aggregationMethodDefinition, stringLiteralToken.Text);
				}
			}
			finally
			{
				this.parseAggregateExpresionDepth--;
			}
			return aggregateTokenBase;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00036AE8 File Offset: 0x00034CE8
		internal GroupByToken ParseGroupBy()
		{
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			List<EndPathToken> list = new List<EndPathToken>();
			for (;;)
			{
				EndPathToken endPathToken = this.ParsePrimary() as EndPathToken;
				if (endPathToken == null)
				{
					break;
				}
				list.Add(endPathToken);
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					goto IL_010B;
				}
				this.lexer.NextToken();
			}
			throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			IL_010B:
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrOperatorExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			ApplyTransformationToken applyTransformationToken = null;
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Comma)
			{
				this.lexer.NextToken();
				if (!this.TokenIdentifierIs("aggregate"))
				{
					throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_KeywordOrIdentifierExpected("aggregate", this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
				}
				applyTransformationToken = this.ParseAggregate();
			}
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return new GroupByToken(list, applyTransformationToken);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00036D05 File Offset: 0x00034F05
		internal QueryToken ParseApplyFilter()
		{
			this.lexer.NextToken();
			return this.ParseParenExpression();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00036D1C File Offset: 0x00034F1C
		internal ComputeExpressionToken ParseComputeExpression()
		{
			QueryToken queryToken = this.ParseExpression();
			StringLiteralToken stringLiteralToken = this.ParseAggregateAs();
			return new ComputeExpressionToken(queryToken, stringLiteralToken.Text);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00036D44 File Offset: 0x00034F44
		internal QueryToken ParseExpressionText(string expressionText)
		{
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(expressionText);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00036D78 File Offset: 0x00034F78
		internal IEnumerable<OrderByToken> ParseOrderBy(string orderBy)
		{
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(orderBy);
			List<OrderByToken> list = new List<OrderByToken>();
			for (;;)
			{
				QueryToken queryToken = this.ParseExpression();
				bool flag = true;
				if (this.TokenIdentifierIs("asc"))
				{
					this.lexer.NextToken();
				}
				else if (this.TokenIdentifierIs("desc"))
				{
					this.lexer.NextToken();
					flag = false;
				}
				OrderByToken orderByToken = new OrderByToken(queryToken, flag ? OrderByDirection.Ascending : OrderByDirection.Descending);
				list.Add(orderByToken);
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return new ReadOnlyCollection<OrderByToken>(list);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00036E28 File Offset: 0x00035028
		internal QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00036E49 File Offset: 0x00035049
		private static ExpressionLexer CreateLexerForFilterOrOrderByOrApplyExpression(string expression)
		{
			return new ExpressionLexer(expression, true, false, true);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00036E54 File Offset: 0x00035054
		private static Exception ParseError(string message, UriLiteralParsingException parsingException)
		{
			return new ODataException(message, parsingException);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00036E60 File Offset: 0x00035060
		private static FunctionParameterAliasToken ParseParameterAlias(ExpressionLexer lexer)
		{
			FunctionParameterAliasToken functionParameterAliasToken = new FunctionParameterAliasToken(lexer.CurrentToken.Text);
			lexer.NextToken();
			return functionParameterAliasToken;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00036E88 File Offset: 0x00035088
		private static LiteralToken ParseTypedLiteral(ExpressionLexer lexer, IEdmTypeReference targetTypeReference, string targetTypeName)
		{
			UriLiteralParsingException ex;
			object obj = DefaultUriLiteralParser.Instance.ParseUriStringToType(lexer.CurrentToken.Text, targetTypeReference, out ex);
			if (obj != null)
			{
				LiteralToken literalToken = new LiteralToken(obj, lexer.CurrentToken.Text);
				lexer.NextToken();
				return literalToken;
			}
			string text;
			if (ex == null)
			{
				text = Strings.UriQueryExpressionParser_UnrecognizedLiteral(targetTypeName, lexer.CurrentToken.Text, lexer.CurrentToken.Position, lexer.ExpressionText);
				throw UriQueryExpressionParser.ParseError(text);
			}
			text = Strings.UriQueryExpressionParser_UnrecognizedLiteralWithReason(targetTypeName, lexer.CurrentToken.Text, lexer.CurrentToken.Position, lexer.ExpressionText, ex.Message);
			throw UriQueryExpressionParser.ParseError(text, ex);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00036F34 File Offset: 0x00035134
		private static LiteralToken ParseNullLiteral(ExpressionLexer lexer)
		{
			LiteralToken literalToken = new LiteralToken(null, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalToken;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00036F5C File Offset: 0x0003515C
		private QueryToken ParseLogicalOr()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalAnd();
			while (this.TokenIdentifierIs("or"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseLogicalAnd();
				queryToken = new BinaryOperatorToken(BinaryOperatorKind.Or, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00036FA8 File Offset: 0x000351A8
		private QueryToken ParseLogicalAnd()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseComparison();
			while (this.TokenIdentifierIs("and"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseComparison();
				queryToken = new BinaryOperatorToken(BinaryOperatorKind.And, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00036FF4 File Offset: 0x000351F4
		private QueryToken ParseComparison()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseAdditive();
			for (;;)
			{
				if (this.TokenIdentifierIs("in"))
				{
					this.lexer.NextToken();
					QueryToken queryToken2 = this.ParseAdditive();
					queryToken = new InToken(queryToken, queryToken2);
				}
				else
				{
					BinaryOperatorKind binaryOperatorKind;
					if (this.TokenIdentifierIs("eq"))
					{
						binaryOperatorKind = BinaryOperatorKind.Equal;
					}
					else if (this.TokenIdentifierIs("ne"))
					{
						binaryOperatorKind = BinaryOperatorKind.NotEqual;
					}
					else if (this.TokenIdentifierIs("gt"))
					{
						binaryOperatorKind = BinaryOperatorKind.GreaterThan;
					}
					else if (this.TokenIdentifierIs("ge"))
					{
						binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
					}
					else if (this.TokenIdentifierIs("lt"))
					{
						binaryOperatorKind = BinaryOperatorKind.LessThan;
					}
					else if (this.TokenIdentifierIs("le"))
					{
						binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
					}
					else
					{
						if (!this.TokenIdentifierIs("has"))
						{
							break;
						}
						binaryOperatorKind = BinaryOperatorKind.Has;
					}
					this.lexer.NextToken();
					QueryToken queryToken3 = this.ParseAdditive();
					queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken3);
				}
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000370D8 File Offset: 0x000352D8
		private QueryToken ParseAdditive()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseMultiplicative();
			while (this.TokenIdentifierIs("add") || this.TokenIdentifierIs("sub"))
			{
				BinaryOperatorKind binaryOperatorKind;
				if (this.TokenIdentifierIs("add"))
				{
					binaryOperatorKind = BinaryOperatorKind.Add;
				}
				else
				{
					binaryOperatorKind = BinaryOperatorKind.Subtract;
				}
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseMultiplicative();
				queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00037148 File Offset: 0x00035348
		private QueryToken ParseMultiplicative()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseUnary();
			while (this.TokenIdentifierIs("mul") || this.TokenIdentifierIs("div") || this.TokenIdentifierIs("mod"))
			{
				BinaryOperatorKind binaryOperatorKind;
				if (this.TokenIdentifierIs("mul"))
				{
					binaryOperatorKind = BinaryOperatorKind.Multiply;
				}
				else if (this.TokenIdentifierIs("div"))
				{
					binaryOperatorKind = BinaryOperatorKind.Divide;
				}
				else
				{
					binaryOperatorKind = BinaryOperatorKind.Modulo;
				}
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseUnary();
				queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000371D8 File Offset: 0x000353D8
		private QueryToken ParseUnary()
		{
			this.RecurseEnter();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Minus && !this.TokenIdentifierIs("not"))
			{
				this.RecurseLeave();
				return this.ParsePrimary();
			}
			ExpressionToken currentToken = this.lexer.CurrentToken;
			this.lexer.NextToken();
			if (currentToken.Kind == ExpressionTokenKind.Minus && ExpressionLexerUtils.IsNumeric(this.lexer.CurrentToken.Kind))
			{
				ExpressionToken currentToken2 = this.lexer.CurrentToken;
				currentToken2.Text = "-" + currentToken2.Text;
				currentToken2.Position = currentToken.Position;
				this.lexer.CurrentToken = currentToken2;
				this.RecurseLeave();
				return this.ParsePrimary();
			}
			QueryToken queryToken = this.ParseUnary();
			UnaryOperatorKind unaryOperatorKind;
			if (currentToken.Kind == ExpressionTokenKind.Minus)
			{
				unaryOperatorKind = UnaryOperatorKind.Negate;
			}
			else
			{
				unaryOperatorKind = UnaryOperatorKind.Not;
			}
			this.RecurseLeave();
			return new UnaryOperatorToken(unaryOperatorKind, queryToken);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000372C4 File Offset: 0x000354C4
		private QueryToken ParsePrimary()
		{
			this.RecurseEnter();
			QueryToken queryToken = ((this.aggregateExpressionParents.Count > 0) ? this.aggregateExpressionParents.Peek() : null);
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
			{
				queryToken = this.ParseSegment(queryToken);
			}
			else
			{
				queryToken = this.ParsePrimaryStart();
			}
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Slash)
			{
				this.lexer.NextToken();
				if (this.TokenIdentifierIs("any"))
				{
					queryToken = this.ParseAny(queryToken);
				}
				else if (this.TokenIdentifierIs("all"))
				{
					queryToken = this.ParseAll(queryToken);
				}
				else if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
				{
					queryToken = this.ParseSegment(queryToken);
				}
				else
				{
					IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this, this.IsInAggregateExpression));
					queryToken = identifierTokenizer.ParseIdentifier(queryToken);
				}
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000373BC File Offset: 0x000355BC
		private QueryToken ParsePrimaryStart()
		{
			ExpressionTokenKind kind = this.lexer.CurrentToken.Kind;
			if (kind <= ExpressionTokenKind.OpenParen)
			{
				if (kind == ExpressionTokenKind.Identifier)
				{
					IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this, this.IsInAggregateExpression));
					QueryToken queryToken = ((this.aggregateExpressionParents.Count > 0) ? this.aggregateExpressionParents.Peek() : null);
					return identifierTokenizer.ParseIdentifier(queryToken);
				}
				if (kind == ExpressionTokenKind.OpenParen)
				{
					return this.ParseParenExpression();
				}
			}
			else
			{
				if (kind == ExpressionTokenKind.Star)
				{
					IdentifierTokenizer identifierTokenizer2 = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this, this.IsInAggregateExpression));
					return identifierTokenizer2.ParseStarMemberAccess(null);
				}
				if (kind == ExpressionTokenKind.ParameterAlias)
				{
					return UriQueryExpressionParser.ParseParameterAlias(this.lexer);
				}
			}
			QueryToken queryToken2 = UriQueryExpressionParser.TryParseLiteral(this.lexer);
			if (queryToken2 == null)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			return queryToken2;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000374B4 File Offset: 0x000356B4
		private QueryToken ParseParenExpression()
		{
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			QueryToken queryToken = this.ParseExpression();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrOperatorExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return queryToken;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0003755F File Offset: 0x0003575F
		private QueryToken ParseAny(QueryToken parent)
		{
			return this.ParseAnyAll(parent, true);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00037569 File Offset: 0x00035769
		private QueryToken ParseAll(QueryToken parent)
		{
			return this.ParseAnyAll(parent, false);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00037574 File Offset: 0x00035774
		private QueryToken ParseAnyAll(QueryToken parent, bool isAny)
		{
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
			{
				this.lexer.NextToken();
				if (isAny)
				{
					return new AnyToken(new LiteralToken(true, "True"), null, parent);
				}
				return new AllToken(new LiteralToken(true, "True"), null, parent);
			}
			else
			{
				string identifier = this.lexer.CurrentToken.GetIdentifier();
				if (!this.parameters.Add(identifier))
				{
					throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_RangeVariableAlreadyDeclared(identifier));
				}
				this.lexer.NextToken();
				this.lexer.ValidateToken(ExpressionTokenKind.Colon);
				this.lexer.NextToken();
				QueryToken queryToken = this.ParseExpression();
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
				{
					throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
				}
				this.parameters.Remove(identifier);
				this.lexer.NextToken();
				if (isAny)
				{
					return new AnyToken(queryToken, identifier, parent);
				}
				return new AllToken(queryToken, identifier, parent);
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000376F4 File Offset: 0x000358F4
		private QueryToken ParseSegment(QueryToken parent)
		{
			string identifier = this.lexer.CurrentToken.GetIdentifier();
			this.lexer.NextToken();
			if (this.parameters.Contains(identifier) && parent == null)
			{
				return new RangeVariableToken(identifier);
			}
			return new InnerPathToken(identifier, parent, null);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00037744 File Offset: 0x00035944
		private AggregationMethodDefinition ParseAggregateWith()
		{
			if (!this.TokenIdentifierIs("with"))
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_WithExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			int position = this.lexer.CurrentToken.Position;
			string text = this.lexer.ReadDottedIdentifier(false);
			AggregationMethodDefinition aggregationMethodDefinition;
			if (!(text == "average"))
			{
				if (!(text == "countdistinct"))
				{
					if (!(text == "max"))
					{
						if (!(text == "min"))
						{
							if (!(text == "sum"))
							{
								if (!text.Contains("."))
								{
									throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_UnrecognizedWithMethod(text, position, this.lexer.ExpressionText));
								}
								aggregationMethodDefinition = AggregationMethodDefinition.Custom(text);
							}
							else
							{
								aggregationMethodDefinition = AggregationMethodDefinition.Sum;
							}
						}
						else
						{
							aggregationMethodDefinition = AggregationMethodDefinition.Min;
						}
					}
					else
					{
						aggregationMethodDefinition = AggregationMethodDefinition.Max;
					}
				}
				else
				{
					aggregationMethodDefinition = AggregationMethodDefinition.CountDistinct;
				}
			}
			else
			{
				aggregationMethodDefinition = AggregationMethodDefinition.Average;
			}
			return aggregationMethodDefinition;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00037850 File Offset: 0x00035A50
		private StringLiteralToken ParseAggregateAs()
		{
			if (!this.TokenIdentifierIs("as"))
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_AsExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			StringLiteralToken stringLiteralToken = new StringLiteralToken(this.lexer.CurrentToken.Text);
			this.lexer.NextToken();
			return stringLiteralToken;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000378C4 File Offset: 0x00035AC4
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id, this.enableCaseInsensitiveBuiltinIdentifier);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000378EB File Offset: 0x00035AEB
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00037914 File Offset: 0x00035B14
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400083E RID: 2110
		private readonly int maxDepth;

		// Token: 0x0400083F RID: 2111
		private static readonly string supportedKeywords = string.Join("|", new string[] { "aggregate", "filter", "groupby", "compute", "expand" });

		// Token: 0x04000840 RID: 2112
		private readonly HashSet<string> parameters = new HashSet<string>(StringComparer.Ordinal) { "$it" };

		// Token: 0x04000841 RID: 2113
		private int recursionDepth;

		// Token: 0x04000842 RID: 2114
		private ExpressionLexer lexer;

		// Token: 0x04000843 RID: 2115
		private bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x04000844 RID: 2116
		private int parseAggregateExpresionDepth;

		// Token: 0x04000845 RID: 2117
		private Stack<QueryToken> aggregateExpressionParents = new Stack<QueryToken>();

		// Token: 0x020003B8 RID: 952
		// (Invoke) Token: 0x06002002 RID: 8194
		internal delegate QueryToken Parser();
	}
}
