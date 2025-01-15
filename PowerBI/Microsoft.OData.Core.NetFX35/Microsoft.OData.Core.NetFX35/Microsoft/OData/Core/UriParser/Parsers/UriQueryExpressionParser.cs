using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Parsers.Common;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000216 RID: 534
	internal sealed class UriQueryExpressionParser
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x00047050 File Offset: 0x00045250
		internal UriQueryExpressionParser(int maxDepth, bool enableCaseInsensitiveBuiltinIdentifier = false)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			hashSet.Add("$it");
			this.parameters = hashSet;
			base..ctor();
			this.maxDepth = maxDepth;
			this.enableCaseInsensitiveBuiltinIdentifier = enableCaseInsensitiveBuiltinIdentifier;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00047090 File Offset: 0x00045290
		internal UriQueryExpressionParser(int maxDepth, ExpressionLexer lexer)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			hashSet.Add("$it");
			this.parameters = hashSet;
			base..ctor();
			this.maxDepth = maxDepth;
			this.lexer = lexer;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x000470CF File Offset: 0x000452CF
		internal ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000470D8 File Offset: 0x000452D8
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
			case ExpressionTokenKind.BracketedExpression:
			{
				LiteralToken literalToken = new LiteralToken(lexer.CurrentToken.Text, lexer.CurrentToken.Text);
				lexer.NextToken();
				return literalToken;
			}
			}
			return null;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000471C8 File Offset: 0x000453C8
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
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Date:
					return "Edm.Date";
				case EdmPrimitiveTypeKind.TimeOfDay:
					return "Edm.TimeOfDay";
				}
				break;
			}
			return edmTypeReference.Definition.FullTypeName();
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000472BC File Offset: 0x000454BC
		internal QueryToken ParseFilter(string filter)
		{
			return this.ParseExpressionText(filter);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000472C8 File Offset: 0x000454C8
		internal IEnumerable<QueryToken> ParseApply(string apply)
		{
			List<QueryToken> list = new List<QueryToken>();
			if (string.IsNullOrEmpty(apply))
			{
				return list;
			}
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(apply);
			string identifier;
			while ((identifier = this.lexer.CurrentToken.GetIdentifier()) != null)
			{
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
					this.lexer.ValidateToken(ExpressionTokenKind.End);
					return new ReadOnlyCollection<QueryToken>(list);
				}
				this.lexer.NextToken();
			}
			throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_KeywordOrIdentifierExpected(UriQueryExpressionParser.supportedKeywords, this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000473CC File Offset: 0x000455CC
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

		// Token: 0x06001371 RID: 4977 RVA: 0x000474B8 File Offset: 0x000456B8
		internal AggregateExpressionToken ParseAggregateExpression()
		{
			QueryToken queryToken = this.ParseExpression();
			AggregationMethod aggregationMethod = this.ParseAggregateWith();
			StringLiteralToken stringLiteralToken = this.ParseAggregateAs();
			return new AggregateExpressionToken(queryToken, aggregationMethod, stringLiteralToken.Text);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000474E8 File Offset: 0x000456E8
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

		// Token: 0x06001373 RID: 4979 RVA: 0x00047705 File Offset: 0x00045905
		internal QueryToken ParseApplyFilter()
		{
			this.lexer.NextToken();
			return this.ParseParenExpression();
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004771C File Offset: 0x0004591C
		internal QueryToken ParseExpressionText(string expressionText)
		{
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByOrApplyExpression(expressionText);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00047750 File Offset: 0x00045950
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

		// Token: 0x06001376 RID: 4982 RVA: 0x00047800 File Offset: 0x00045A00
		internal QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00047821 File Offset: 0x00045A21
		private static ExpressionLexer CreateLexerForFilterOrOrderByOrApplyExpression(string expression)
		{
			return new ExpressionLexer(expression, true, false, true);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004782C File Offset: 0x00045A2C
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00047834 File Offset: 0x00045A34
		private static Exception ParseError(string message, UriLiteralParsingException parsingException)
		{
			return new ODataException(message, parsingException);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00047840 File Offset: 0x00045A40
		private static FunctionParameterAliasToken ParseParameterAlias(ExpressionLexer lexer)
		{
			FunctionParameterAliasToken functionParameterAliasToken = new FunctionParameterAliasToken(lexer.CurrentToken.Text);
			lexer.NextToken();
			return functionParameterAliasToken;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00047868 File Offset: 0x00045A68
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

		// Token: 0x0600137C RID: 4988 RVA: 0x00047914 File Offset: 0x00045B14
		private static LiteralToken ParseNullLiteral(ExpressionLexer lexer)
		{
			LiteralToken literalToken = new LiteralToken(null, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalToken;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004793C File Offset: 0x00045B3C
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

		// Token: 0x0600137E RID: 4990 RVA: 0x00047988 File Offset: 0x00045B88
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

		// Token: 0x0600137F RID: 4991 RVA: 0x000479D4 File Offset: 0x00045BD4
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

		// Token: 0x06001380 RID: 4992 RVA: 0x00047A8C File Offset: 0x00045C8C
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

		// Token: 0x06001381 RID: 4993 RVA: 0x00047AFC File Offset: 0x00045CFC
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

		// Token: 0x06001382 RID: 4994 RVA: 0x00047B8C File Offset: 0x00045D8C
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

		// Token: 0x06001383 RID: 4995 RVA: 0x00047C7C File Offset: 0x00045E7C
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

		// Token: 0x06001384 RID: 4996 RVA: 0x00047D4C File Offset: 0x00045F4C
		private QueryToken ParsePrimaryStart()
		{
			ExpressionTokenKind kind = this.lexer.CurrentToken.Kind;
			if (kind == ExpressionTokenKind.Identifier)
			{
				IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this));
				return identifierTokenizer.ParseIdentifier(null);
			}
			if (kind == ExpressionTokenKind.OpenParen)
			{
				return this.ParseParenExpression();
			}
			switch (kind)
			{
			case ExpressionTokenKind.Star:
			{
				IdentifierTokenizer identifierTokenizer2 = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, this));
				return identifierTokenizer2.ParseStarMemberAccess(null);
			}
			case ExpressionTokenKind.ParameterAlias:
				return UriQueryExpressionParser.ParseParameterAlias(this.lexer);
			}
			QueryToken queryToken = UriQueryExpressionParser.TryParseLiteral(this.lexer);
			if (queryToken == null)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			return queryToken;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00047E18 File Offset: 0x00046018
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

		// Token: 0x06001386 RID: 4998 RVA: 0x00047EC3 File Offset: 0x000460C3
		private QueryToken ParseAny(QueryToken parent)
		{
			return this.ParseAnyAll(parent, true);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00047ECD File Offset: 0x000460CD
		private QueryToken ParseAll(QueryToken parent)
		{
			return this.ParseAnyAll(parent, false);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00047ED8 File Offset: 0x000460D8
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

		// Token: 0x06001389 RID: 5001 RVA: 0x00048058 File Offset: 0x00046258
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

		// Token: 0x0600138A RID: 5002 RVA: 0x000480A8 File Offset: 0x000462A8
		private AggregationMethod ParseAggregateWith()
		{
			if (!this.TokenIdentifierIs("with"))
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_WithExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			string identifier;
			if ((identifier = this.lexer.CurrentToken.GetIdentifier()) != null)
			{
				AggregationMethod aggregationMethod;
				if (!(identifier == "average"))
				{
					if (!(identifier == "countdistinct"))
					{
						if (!(identifier == "max"))
						{
							if (!(identifier == "min"))
							{
								if (!(identifier == "sum"))
								{
									goto IL_00B2;
								}
								aggregationMethod = AggregationMethod.Sum;
							}
							else
							{
								aggregationMethod = AggregationMethod.Min;
							}
						}
						else
						{
							aggregationMethod = AggregationMethod.Max;
						}
					}
					else
					{
						aggregationMethod = AggregationMethod.CountDistinct;
					}
				}
				else
				{
					aggregationMethod = AggregationMethod.Average;
				}
				this.lexer.NextToken();
				return aggregationMethod;
			}
			IL_00B2:
			throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_UnrecognizedWithVerb(this.lexer.CurrentToken.GetIdentifier(), this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000481B4 File Offset: 0x000463B4
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

		// Token: 0x0600138C RID: 5004 RVA: 0x00048228 File Offset: 0x00046428
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id, this.enableCaseInsensitiveBuiltinIdentifier);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004824F File Offset: 0x0004644F
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00048278 File Offset: 0x00046478
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000848 RID: 2120
		private readonly int maxDepth;

		// Token: 0x04000849 RID: 2121
		private static readonly string supportedKeywords = string.Join("|", new string[] { "aggregate", "filter", "groupby" });

		// Token: 0x0400084A RID: 2122
		private readonly HashSet<string> parameters;

		// Token: 0x0400084B RID: 2123
		private int recursionDepth;

		// Token: 0x0400084C RID: 2124
		private ExpressionLexer lexer;

		// Token: 0x0400084D RID: 2125
		private bool enableCaseInsensitiveBuiltinIdentifier;

		// Token: 0x02000217 RID: 535
		// (Invoke) Token: 0x06001391 RID: 5009
		internal delegate QueryToken Parser();
	}
}
