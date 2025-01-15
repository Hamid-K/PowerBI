using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000FE RID: 254
	internal static class ExpressionLexerLiteralExtensions
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00021174 File Offset: 0x0001F374
		internal static bool IsLiteralType(this ExpressionTokenKind tokenKind)
		{
			switch (tokenKind)
			{
			case ExpressionTokenKind.NullLiteral:
			case ExpressionTokenKind.BooleanLiteral:
			case ExpressionTokenKind.StringLiteral:
			case ExpressionTokenKind.IntegerLiteral:
			case ExpressionTokenKind.Int64Literal:
			case ExpressionTokenKind.SingleLiteral:
			case ExpressionTokenKind.DateTimeLiteral:
			case ExpressionTokenKind.DateTimeOffsetLiteral:
			case ExpressionTokenKind.DurationLiteral:
			case ExpressionTokenKind.DecimalLiteral:
			case ExpressionTokenKind.DoubleLiteral:
			case ExpressionTokenKind.GuidLiteral:
			case ExpressionTokenKind.BinaryLiteral:
			case ExpressionTokenKind.GeographyLiteral:
			case ExpressionTokenKind.GeometryLiteral:
			case ExpressionTokenKind.DateLiteral:
			case ExpressionTokenKind.TimeOfDayLiteral:
				return true;
			}
			return false;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002120E File Offset: 0x0001F40E
		internal static object ReadLiteralToken(this ExpressionLexer expressionLexer)
		{
			expressionLexer.NextToken();
			if (expressionLexer.CurrentToken.Kind.IsLiteralType())
			{
				return expressionLexer.TryParseLiteral();
			}
			throw new ODataException(Strings.ExpressionLexer_ExpectedLiteralToken(expressionLexer.CurrentToken.Text));
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00021248 File Offset: 0x0001F448
		private static object ParseNullLiteral(this ExpressionLexer expressionLexer)
		{
			expressionLexer.NextToken();
			return new ODataNullValue();
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00021264 File Offset: 0x0001F464
		private static object ParseTypedLiteral(this ExpressionLexer expressionLexer, IEdmTypeReference targetTypeReference)
		{
			UriLiteralParsingException ex;
			object obj = DefaultUriLiteralParser.Instance.ParseUriStringToType(expressionLexer.CurrentToken.Text, targetTypeReference, out ex);
			if (obj != null)
			{
				expressionLexer.NextToken();
				return obj;
			}
			string text;
			if (ex == null)
			{
				text = Strings.UriQueryExpressionParser_UnrecognizedLiteral(targetTypeReference.FullName(), expressionLexer.CurrentToken.Text, expressionLexer.CurrentToken.Position, expressionLexer.ExpressionText);
				throw new ODataException(text);
			}
			text = Strings.UriQueryExpressionParser_UnrecognizedLiteralWithReason(targetTypeReference.FullName(), expressionLexer.CurrentToken.Text, expressionLexer.CurrentToken.Position, expressionLexer.ExpressionText, ex.Message);
			throw new ODataException(text, ex);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00021308 File Offset: 0x0001F508
		private static object TryParseLiteral(this ExpressionLexer expressionLexer)
		{
			switch (expressionLexer.CurrentToken.Kind)
			{
			case ExpressionTokenKind.NullLiteral:
				return expressionLexer.ParseNullLiteral();
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
				return expressionLexer.ParseTypedLiteral(expressionLexer.CurrentToken.GetLiteralEdmTypeReference());
			}
			return null;
		}
	}
}
