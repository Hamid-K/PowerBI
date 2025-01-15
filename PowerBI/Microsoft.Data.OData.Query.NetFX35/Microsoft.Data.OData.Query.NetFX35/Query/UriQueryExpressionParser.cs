using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000058 RID: 88
	internal sealed class UriQueryExpressionParser
	{
		// Token: 0x06000217 RID: 535 RVA: 0x0000B664 File Offset: 0x00009864
		internal UriQueryExpressionParser(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B674 File Offset: 0x00009874
		internal static LiteralQueryToken TryParseLiteral(ExpressionLexer lexer)
		{
			switch (lexer.CurrentToken.Kind)
			{
			case ExpressionTokenKind.NullLiteral:
				return UriQueryExpressionParser.ParseNullLiteral(lexer);
			case ExpressionTokenKind.BooleanLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetBoolean(false), "Edm.Boolean");
			case ExpressionTokenKind.StringLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetString(true), "Edm.String");
			case ExpressionTokenKind.IntegerLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetInt32(false), "Edm.Int32");
			case ExpressionTokenKind.Int64Literal:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetInt64(false), "Edm.Int64");
			case ExpressionTokenKind.SingleLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetSingle(false), "Edm.Single");
			case ExpressionTokenKind.DateTimeLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, false), "Edm.DateTime");
			case ExpressionTokenKind.DateTimeOffsetLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false), "Edm.DateTimeOffset");
			case ExpressionTokenKind.TimeLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Time, false), "Edm.Time");
			case ExpressionTokenKind.DecimalLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetDecimal(false), "Edm.Decimal");
			case ExpressionTokenKind.DoubleLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetDouble(false), "Edm.Double");
			case ExpressionTokenKind.GuidLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetGuid(false), "Edm.Guid");
			case ExpressionTokenKind.BinaryLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetBinary(true), "Edm.Binary");
			case ExpressionTokenKind.GeographyLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.Geography, false), "Edm.Geography");
			case ExpressionTokenKind.GeometryLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.Geometry, false), "Edm.Geometry");
			default:
				return null;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B828 File Offset: 0x00009A28
		internal QueryToken ParseFilter(string filter)
		{
			this.recursionDepth = 0;
			this.lexer = new ExpressionLexer(filter, true);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B860 File Offset: 0x00009A60
		internal IEnumerable<OrderByQueryToken> ParseOrderBy(string orderBy)
		{
			this.recursionDepth = 0;
			this.lexer = new ExpressionLexer(orderBy, true);
			List<OrderByQueryToken> list = new List<OrderByQueryToken>();
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
				OrderByQueryToken orderByQueryToken = new OrderByQueryToken(queryToken, flag ? OrderByDirection.Ascending : OrderByDirection.Descending);
				list.Add(orderByQueryToken);
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return new ReadOnlyCollection<OrderByQueryToken>(list);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B910 File Offset: 0x00009B10
		internal SelectQueryToken ParseSelect(string select)
		{
			this.lexer = new ExpressionLexer(select, true);
			List<QueryToken> list = new List<QueryToken>();
			for (;;)
			{
				list.Add(this.ParseExpression());
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			return new SelectQueryToken(list);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B964 File Offset: 0x00009B64
		internal ExpandQueryToken ParseExpand(string expand)
		{
			this.lexer = new ExpressionLexer(expand, true);
			List<QueryToken> list = new List<QueryToken>();
			for (;;)
			{
				list.Add(this.ParseExpression());
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			return new ExpandQueryToken(list);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		private static LiteralQueryToken ParseTypedLiteral(ExpressionLexer lexer, IEdmPrimitiveTypeReference targetTypeReference, string targetTypeName)
		{
			object obj;
			if (!UriPrimitiveTypeParser.TryUriStringToPrimitive(lexer.CurrentToken.Text, targetTypeReference, out obj))
			{
				string text = Strings.UriQueryExpressionParser_UnrecognizedLiteral(targetTypeName, lexer.CurrentToken.Text, lexer.CurrentToken.Position, lexer.ExpressionText);
				throw UriQueryExpressionParser.ParseError(text);
			}
			LiteralQueryToken literalQueryToken = new LiteralQueryToken(obj, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalQueryToken;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000BA2C File Offset: 0x00009C2C
		private static LiteralQueryToken ParseNullLiteral(ExpressionLexer lexer)
		{
			LiteralQueryToken literalQueryToken = new LiteralQueryToken(null, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalQueryToken;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BA54 File Offset: 0x00009C54
		private QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BA78 File Offset: 0x00009C78
		private QueryToken ParseLogicalOr()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalAnd();
			while (this.TokenIdentifierIs("or"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseLogicalAnd();
				queryToken = new BinaryOperatorQueryToken(BinaryOperatorKind.Or, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		private QueryToken ParseLogicalAnd()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseComparison();
			while (this.TokenIdentifierIs("and"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseComparison();
				queryToken = new BinaryOperatorQueryToken(BinaryOperatorKind.And, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BB10 File Offset: 0x00009D10
		private QueryToken ParseComparison()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseAdditive();
			while (this.lexer.CurrentToken.IsComparisonOperator)
			{
				string text;
				if ((text = this.lexer.CurrentToken.Text) != null)
				{
					if (<PrivateImplementationDetails>{DB2F74E1-B935-40F7-ACE3-457F9F4ACC9C}.$$method0x6000217-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("eq", 0);
						dictionary.Add("ne", 1);
						dictionary.Add("gt", 2);
						dictionary.Add("ge", 3);
						dictionary.Add("lt", 4);
						dictionary.Add("le", 5);
						<PrivateImplementationDetails>{DB2F74E1-B935-40F7-ACE3-457F9F4ACC9C}.$$method0x6000217-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{DB2F74E1-B935-40F7-ACE3-457F9F4ACC9C}.$$method0x6000217-1.TryGetValue(text, ref num))
					{
						BinaryOperatorKind binaryOperatorKind;
						switch (num)
						{
						case 0:
							binaryOperatorKind = BinaryOperatorKind.Equal;
							break;
						case 1:
							binaryOperatorKind = BinaryOperatorKind.NotEqual;
							break;
						case 2:
							binaryOperatorKind = BinaryOperatorKind.GreaterThan;
							break;
						case 3:
							binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
							break;
						case 4:
							binaryOperatorKind = BinaryOperatorKind.LessThan;
							break;
						case 5:
							binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
							break;
						default:
							goto IL_00D1;
						}
						this.lexer.NextToken();
						QueryToken queryToken2 = this.ParseAdditive();
						queryToken = new BinaryOperatorQueryToken(binaryOperatorKind, queryToken, queryToken2);
						continue;
					}
				}
				IL_00D1:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriQueryExpressionParser_ParseComparison));
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BC3C File Offset: 0x00009E3C
		private QueryToken ParseAdditive()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseMultiplicative();
			while (this.lexer.CurrentToken.IdentifierIs("add") || this.lexer.CurrentToken.IdentifierIs("sub"))
			{
				BinaryOperatorKind binaryOperatorKind;
				if (this.lexer.CurrentToken.IdentifierIs("add"))
				{
					binaryOperatorKind = BinaryOperatorKind.Add;
				}
				else
				{
					binaryOperatorKind = BinaryOperatorKind.Subtract;
				}
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseMultiplicative();
				queryToken = new BinaryOperatorQueryToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		private QueryToken ParseMultiplicative()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseUnary();
			while (this.lexer.CurrentToken.IdentifierIs("mul") || this.lexer.CurrentToken.IdentifierIs("div") || this.lexer.CurrentToken.IdentifierIs("mod"))
			{
				BinaryOperatorKind binaryOperatorKind;
				if (this.lexer.CurrentToken.IdentifierIs("mul"))
				{
					binaryOperatorKind = BinaryOperatorKind.Multiply;
				}
				else if (this.lexer.CurrentToken.IdentifierIs("div"))
				{
					binaryOperatorKind = BinaryOperatorKind.Divide;
				}
				else
				{
					binaryOperatorKind = BinaryOperatorKind.Modulo;
				}
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseUnary();
				queryToken = new BinaryOperatorQueryToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BDAC File Offset: 0x00009FAC
		private QueryToken ParseUnary()
		{
			this.RecurseEnter();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Minus && !this.lexer.CurrentToken.IdentifierIs("not"))
			{
				this.RecurseLeave();
				return this.ParsePrimary();
			}
			ExpressionToken currentToken = this.lexer.CurrentToken;
			this.lexer.NextToken();
			if (currentToken.Kind == ExpressionTokenKind.Minus && ExpressionLexer.IsNumeric(this.lexer.CurrentToken.Kind))
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
			return new UnaryOperatorQueryToken(unaryOperatorKind, queryToken);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		private QueryToken ParsePrimary()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParsePrimaryStart();
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Slash)
			{
				this.lexer.NextToken();
				queryToken = this.ParseMemberAccess(queryToken);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		private QueryToken ParsePrimaryStart()
		{
			ExpressionTokenKind kind = this.lexer.CurrentToken.Kind;
			if (kind == ExpressionTokenKind.Identifier)
			{
				return this.ParseIdentifier();
			}
			if (kind == ExpressionTokenKind.OpenParen)
			{
				return this.ParseParenExpression();
			}
			if (kind == ExpressionTokenKind.Star)
			{
				return this.ParseStarMemberAccess(null);
			}
			QueryToken queryToken = UriQueryExpressionParser.TryParseLiteral(this.lexer);
			if (queryToken == null)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			return queryToken;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BF74 File Offset: 0x0000A174
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

		// Token: 0x0600022A RID: 554 RVA: 0x0000C020 File Offset: 0x0000A220
		private QueryToken ParseIdentifier()
		{
			this.lexer.ValidateToken(ExpressionTokenKind.Identifier);
			bool flag = this.lexer.PeekNextToken().Kind == ExpressionTokenKind.OpenParen;
			if (flag)
			{
				return this.ParseIdentifierAsFunction();
			}
			return this.ParseMemberAccess(null);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C060 File Offset: 0x0000A260
		private QueryToken ParseIdentifierAsFunction()
		{
			ExpressionToken currentToken = this.lexer.CurrentToken;
			this.lexer.NextToken();
			QueryToken[] array = this.ParseArgumentList();
			return new FunctionCallQueryToken(currentToken.Text, array);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C09C File Offset: 0x0000A29C
		private QueryToken[] ParseArgumentList()
		{
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			QueryToken[] array = ((this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen) ? this.ParseArguments() : QueryToken.EmptyTokens);
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return array;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C164 File Offset: 0x0000A364
		private QueryToken[] ParseArguments()
		{
			List<QueryToken> list = new List<QueryToken>();
			for (;;)
			{
				list.Add(this.ParseExpression());
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			return list.ToArray();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		private QueryToken ParseMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text == "*")
			{
				return this.ParseStarMemberAccess(instance);
			}
			string identifier = this.lexer.CurrentToken.GetIdentifier();
			this.lexer.NextToken();
			return new PropertyAccessQueryToken(identifier, instance);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000C204 File Offset: 0x0000A404
		private QueryToken ParseStarMemberAccess(QueryToken instance)
		{
			this.lexer.NextToken();
			return new StarQueryToken(instance);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C218 File Offset: 0x0000A418
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C239 File Offset: 0x0000A439
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C262 File Offset: 0x0000A462
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400020F RID: 527
		private int maxDepth;

		// Token: 0x04000210 RID: 528
		private int recursionDepth;

		// Token: 0x04000211 RID: 529
		private ExpressionLexer lexer;
	}
}
