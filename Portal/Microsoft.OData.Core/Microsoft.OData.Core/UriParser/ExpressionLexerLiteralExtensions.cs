using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013D RID: 317
	internal static class ExpressionLexerLiteralExtensions
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x0002E7B0 File Offset: 0x0002C9B0
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

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002E84A File Offset: 0x0002CA4A
		internal static object ReadLiteralToken(this ExpressionLexer expressionLexer)
		{
			expressionLexer.NextToken();
			if (expressionLexer.CurrentToken.Kind.IsLiteralType())
			{
				return expressionLexer.TryParseLiteral();
			}
			throw new ODataException(Strings.ExpressionLexer_ExpectedLiteralToken(expressionLexer.CurrentToken.Text));
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002E884 File Offset: 0x0002CA84
		private static object ParseNullLiteral(this ExpressionLexer expressionLexer)
		{
			expressionLexer.NextToken();
			return new ODataNullValue();
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
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

		// Token: 0x060010AA RID: 4266 RVA: 0x0002E944 File Offset: 0x0002CB44
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
