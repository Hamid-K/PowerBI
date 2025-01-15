using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000120 RID: 288
	public sealed class UriQueryExpressionParser
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00026D28 File Offset: 0x00024F28
		public UriQueryExpressionParser(int maxDepth)
			: this(maxDepth, false)
		{
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00026D32 File Offset: 0x00024F32
		internal UriQueryExpressionParser(int maxDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			hashSet.Add("$it");
			this.parameters = hashSet;
			base..ctor();
			this.maxDepth = maxDepth;
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00026D64 File Offset: 0x00024F64
		internal UriQueryExpressionParser(int maxDepth, ExpressionLexer lexer)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			hashSet.Add("$it");
			this.parameters = hashSet;
			base..ctor();
			this.maxDepth = maxDepth;
			this.lexer = lexer;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00026D96 File Offset: 0x00024F96
		internal ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00026D9E File Offset: 0x00024F9E
		public QueryToken ParseFilter(string filter)
		{
			return this.ParseExpressionText(filter);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00026DA8 File Offset: 0x00024FA8
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
			{
				LiteralToken literalToken = new LiteralToken(lexer.CurrentToken.Text, lexer.CurrentToken.Text);
				lexer.NextToken();
				return literalToken;
			}
			}
			return null;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00026EA0 File Offset: 0x000250A0
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

		// Token: 0x06000D4C RID: 3404 RVA: 0x00026F90 File Offset: 0x00025190
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

		// Token: 0x06000D4D RID: 3405 RVA: 0x00027008 File Offset: 0x00025208
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
							break;
						}
						list.Add(this.ParseGroupBy());
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
					goto IL_00DF;
				}
				this.lexer.NextToken();
			}
			throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_KeywordOrIdentifierExpected(UriQueryExpressionParser.supportedKeywords, this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			IL_00DF:
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return new ReadOnlyCollection<QueryToken>(list);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00027108 File Offset: 0x00025308
		internal AggregateToken ParseAggregate()
		{
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			List<AggregateExpressionToken> list = new List<AggregateExpressionToken>();
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
			return new AggregateToken(list);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000271F4 File Offset: 0x000253F4
		internal AggregateExpressionToken ParseAggregateExpression()
		{
			QueryToken queryToken = this.ParseExpression();
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
			return new AggregateExpressionToken(queryToken, aggregationMethodDefinition, stringLiteralToken.Text);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00027248 File Offset: 0x00025448
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

		// Token: 0x06000D51 RID: 3409 RVA: 0x00027465 File Offset: 0x00025665
		internal QueryToken ParseApplyFilter()
		{
			this.lexer.NextToken();
			return this.ParseParenExpression();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002747C File Offset: 0x0002567C
		internal ComputeExpressionToken ParseComputeExpression()
		{
			QueryToken queryToken = this.ParseExpression();
			StringLiteralToken stringLiteralToken = this.ParseAggregateAs();
			return new ComputeExpressionToken(queryToken, stringLiteralToken.Text);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000274A4 File Offset: 0x000256A4
		internal QueryToken ParseExpressionText(string expressionText)
		{
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(expressionText);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000274D8 File Offset: 0x000256D8
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

		// Token: 0x06000D55 RID: 3413 RVA: 0x00027588 File Offset: 0x00025788
		internal QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000275A9 File Offset: 0x000257A9
		private static ExpressionLexer CreateLexerForFilterOrOrderByOrApplyExpression(string expression)
		{
			return new ExpressionLexer(expression, true, false, true);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001F734 File Offset: 0x0001D934
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000275B4 File Offset: 0x000257B4
		private static Exception ParseError(string message, UriLiteralParsingException parsingException)
		{
			return new ODataException(message, parsingException);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000275C0 File Offset: 0x000257C0
		private static FunctionParameterAliasToken ParseParameterAlias(ExpressionLexer lexer)
		{
			FunctionParameterAliasToken functionParameterAliasToken = new FunctionParameterAliasToken(lexer.CurrentToken.Text);
			lexer.NextToken();
			return functionParameterAliasToken;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000275E8 File Offset: 0x000257E8
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

		// Token: 0x06000D5B RID: 3419 RVA: 0x00027694 File Offset: 0x00025894
		private static LiteralToken ParseNullLiteral(ExpressionLexer lexer)
		{
			LiteralToken literalToken = new LiteralToken(null, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalToken;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000276BC File Offset: 0x000258BC
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

		// Token: 0x06000D5D RID: 3421 RVA: 0x00027708 File Offset: 0x00025908
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

		// Token: 0x06000D5E RID: 3422 RVA: 0x00027754 File Offset: 0x00025954
		private QueryToken ParseComparison()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseAdditive();
			for (;;)
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
				QueryToken queryToken2 = this.ParseAdditive();
				queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002780C File Offset: 0x00025A0C
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

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002787C File Offset: 0x00025A7C
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

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002790C File Offset: 0x00025B0C
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

		// Token: 0x06000D62 RID: 3426 RVA: 0x000279F8 File Offset: 0x00025BF8
		private QueryToken ParsePrimary()
		{
			this.RecurseEnter();
			QueryToken queryToken;
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
			{
				queryToken = this.ParseSegment(null);
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
					IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this));
					queryToken = identifierTokenizer.ParseIdentifier(queryToken);
				}
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00027AC8 File Offset: 0x00025CC8
		private QueryToken ParsePrimaryStart()
		{
			ExpressionTokenKind kind = this.lexer.CurrentToken.Kind;
			if (kind <= ExpressionTokenKind.OpenParen)
			{
				if (kind == ExpressionTokenKind.Identifier)
				{
					IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this));
					return identifierTokenizer.ParseIdentifier(null);
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
					IdentifierTokenizer identifierTokenizer2 = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this));
					return identifierTokenizer2.ParseStarMemberAccess(null);
				}
				if (kind == ExpressionTokenKind.ParameterAlias)
				{
					return UriQueryExpressionParser.ParseParameterAlias(this.lexer);
				}
			}
			QueryToken queryToken = UriQueryExpressionParser.TryParseLiteral(this.lexer);
			if (queryToken == null)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			return queryToken;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00027B90 File Offset: 0x00025D90
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

		// Token: 0x06000D65 RID: 3429 RVA: 0x00027C3B File Offset: 0x00025E3B
		private QueryToken ParseAny(QueryToken parent)
		{
			return this.ParseAnyAll(parent, true);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00027C45 File Offset: 0x00025E45
		private QueryToken ParseAll(QueryToken parent)
		{
			return this.ParseAnyAll(parent, false);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00027C50 File Offset: 0x00025E50
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

		// Token: 0x06000D68 RID: 3432 RVA: 0x00027DD0 File Offset: 0x00025FD0
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

		// Token: 0x06000D69 RID: 3433 RVA: 0x00027E20 File Offset: 0x00026020
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

		// Token: 0x06000D6A RID: 3434 RVA: 0x00027F2C File Offset: 0x0002612C
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

		// Token: 0x06000D6B RID: 3435 RVA: 0x00027FA0 File Offset: 0x000261A0
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id, this.enableCaseInsensitiveBuiltinIdentifier);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00027FC7 File Offset: 0x000261C7
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00027FF0 File Offset: 0x000261F0
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400071F RID: 1823
		private readonly int maxDepth;

		// Token: 0x04000720 RID: 1824
		private static readonly string supportedKeywords = string.Join("|", new string[] { "aggregate", "filter", "groupby" });

		// Token: 0x04000721 RID: 1825
		private readonly HashSet<string> parameters;

		// Token: 0x04000722 RID: 1826
		private int recursionDepth;

		// Token: 0x04000723 RID: 1827
		private ExpressionLexer lexer;

		// Token: 0x04000724 RID: 1828
		private bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x020002CF RID: 719
		// (Invoke) Token: 0x060018F0 RID: 6384
		internal delegate QueryToken Parser();
	}
}
