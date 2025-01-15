using System;
using System.Globalization;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel.Expression
{
	// Token: 0x02000211 RID: 529
	internal static class RDLExceptionHelper
	{
		// Token: 0x060011C7 RID: 4551 RVA: 0x00028351 File Offset: 0x00026551
		public static void WriteEndExpected(string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.EndExpected", currentToken, startCol, endCol);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00028360 File Offset: 0x00026560
		public static void WriteExpectedOperand(string method, string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ExpectedOperand", method, currentToken, startCol, endCol);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00028370 File Offset: 0x00026570
		public static void WriteInvalidExpression(int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidExpression", "", "", 0, endCol);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00028388 File Offset: 0x00026588
		public static void WriteBadTypeOfSyntax(string method, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadTypeOfSyntax", method, startCol, endCol);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00028397 File Offset: 0x00026597
		public static void WriteUnknownFunction(string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnknownFunction", currentToken, startCol, endCol);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000283A6 File Offset: 0x000265A6
		public static void WriteArrayOperand(string method, string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ArrayOperand", method, currentToken, startCol, endCol);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000283B6 File Offset: 0x000265B6
		public static void WriteExpectedOperator(string method, string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ExpectedOperator", method, currentToken, startCol, endCol);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000283C6 File Offset: 0x000265C6
		public static void WriteDivisionByZero(string method, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.DivisionByZero", method, startCol, endCol);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000283D5 File Offset: 0x000265D5
		public static void WriteOperandTypesInvalid(string operand, string theOperator, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.OperandTypesInvalid", operand, theOperator, startCol, endCol);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000283E5 File Offset: 0x000265E5
		public static void WriteOverflow(string method, string overflowingValue, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.Overflow", method, startCol, endCol, new object[] { overflowingValue });
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000283FE File Offset: 0x000265FE
		public static void WriteBadCollectionSyntaxMissingThirdPart(string expression, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntaxMissingThirdPart", expression, startCol, endCol);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0002840D File Offset: 0x0002660D
		public static void WriteBadCollectionSyntax(string method, string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntax", method, currentToken, startCol, endCol);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0002841D File Offset: 0x0002661D
		public static void WriteBadSyntax(string method, string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadSyntax", method, currentToken, startCol, endCol);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0002842D File Offset: 0x0002662D
		public static void WriteBadSyntax(string method, ExpressionToken curToken)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadSyntax", method, curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0002844C File Offset: 0x0002664C
		public static void WriteBadCollectionSyntaxNoQuote(string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntaxNoQuote", currentToken, startCol, endCol);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0002845B File Offset: 0x0002665B
		public static void WriteBadCollectionSyntaxParen(string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.BadCollectionSyntaxParen", currentToken, startCol, endCol);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0002846A File Offset: 0x0002666A
		public static void WriteAggregateErrorsGroups(string function, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.AggregateErrorsGroups", function, startCol, endCol);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00028479 File Offset: 0x00026679
		public static void WriteAggregateErrorsNested(string function, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.AggregateErrorsNested", function, startCol, endCol);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00028488 File Offset: 0x00026688
		public static void WriteMethodOrPropertyNotFound(string currentToken, string prevExpression, int startCol, int endCol)
		{
			throw new ExpressionParserInvalidMemberNameException(currentToken, "RDLEngine.Error.RDLObjects.Expression.MethodOrPropertyNotFound", currentToken, prevExpression, startCol, endCol);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00028499 File Offset: 0x00026699
		public static void WriteMethodOrPropertyNotFound(ExpressionToken curToken, string typeName)
		{
			throw new ExpressionParserInvalidMemberNameException(curToken._Value, "RDLEngine.Error.RDLObjects.Expression.MethodOrPropertyNotFound", curToken.ToString(), typeName, curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000284BE File Offset: 0x000266BE
		public static void WriteMethodOrPropertyExpected(string currentToken, string prevExpression, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.MethodOrPropertyExpected", currentToken, prevExpression, startCol, endCol);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000284CE File Offset: 0x000266CE
		public static void WriteUnknownEnumeration(string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnknownEnumeration", currentToken, startCol, endCol);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000284DD File Offset: 0x000266DD
		public static void WriteIndexNotIntegerType(string methodName, string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.IndexNotIntegerType", currentToken, methodName, startCol, endCol);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000284ED File Offset: 0x000266ED
		public static void WriteInvalidFunction(string currentToken, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidFunction", currentToken, startCol, endCol);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000284FC File Offset: 0x000266FC
		public static void WriteReportParameterCollectionSyntaxOnCountOrIsMultiValue(ReportParameter reportParameter, int startCol, int endCol)
		{
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.ReportParameterCollectionSyntaxOnCountOrIsMultiValue", reportParameter.Name, startCol, endCol);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00028510 File Offset: 0x00026710
		public static void WriteUnexpectedToken(TokenTypes expectedToken, ExpressionToken curToken)
		{
			throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Expected token: {0} but found token: {1}", expectedToken.ToString(), curToken._TokenType.ToString()), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00028561 File Offset: 0x00026761
		public static void WriteTypeNotFound(string typeName, ExpressionToken curToken)
		{
			throw new ExpressionParserInvalidTypeNameException(typeName, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Type name: '{0}' could not be resolved", typeName), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0002858B File Offset: 0x0002678B
		public static void WriteUnknownIdentifier(string name, ExpressionToken curToken)
		{
			throw new ExpressionParserUnknownIdentifierException(name, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Identifier: '{0}' could not be resolved", name), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000285B5 File Offset: 0x000267B5
		public static void WriteInvalidCollectionItem(string collectionName, string itemName)
		{
			throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Item '{0}' does not exist in the {1} collection.", collectionName, itemName));
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000285CD File Offset: 0x000267CD
		public static void WriteMissingIdentifierForDictionaryOperator(ExpressionToken curToken)
		{
			throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Expected identifier for DictionaryAccessExpression but found: {0}", curToken._Value), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x000285FB File Offset: 0x000267FB
		public static void WriteInvalidDateTimeLiteral(ExpressionToken curToken)
		{
			throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "{0} is not a valid DateTime literal", curToken._Value), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00028629 File Offset: 0x00026829
		public static void WriteMissingArgumentsForExpression(ExpressionToken curToken)
		{
			throw new ExpressionParserException(string.Format(CultureInfo.InvariantCulture, "Expected arguments for IndexExpression or DefaultPropertyExpression but found: {0}", curToken._Value), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00028657 File Offset: 0x00026857
		public static void WriteInvalidArrayType(string typeName, ExpressionToken curToken)
		{
			throw new ExpressionParserInvalidArrayTypeException(typeName, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Type name: '{0}' cannot be used as an array type", typeName), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00028681 File Offset: 0x00026881
		public static void WriteInvalidNewType(string typeName, ExpressionToken curToken)
		{
			throw new ExpressionParserInvalidNewTypeException(typeName, string.Format(CultureInfo.InvariantCulture, "Unable to parse expression.  Type name: '{0}' cannot be used with the New operator.", typeName), curToken.ToString(), curToken.StartColumn, curToken.EndColumn);
		}
	}
}
