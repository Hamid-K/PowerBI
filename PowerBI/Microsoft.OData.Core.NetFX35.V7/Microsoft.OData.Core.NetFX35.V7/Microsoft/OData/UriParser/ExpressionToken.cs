using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000100 RID: 256
	[DebuggerDisplay("{InternalKind} @ {Position}: [{Text}]")]
	internal struct ExpressionToken
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x000214FC File Offset: 0x0001F6FC
		internal bool IsKeyValueToken
		{
			get
			{
				return this.Kind == ExpressionTokenKind.BinaryLiteral || this.Kind == ExpressionTokenKind.BooleanLiteral || this.Kind == ExpressionTokenKind.DateLiteral || this.Kind == ExpressionTokenKind.DateTimeLiteral || this.Kind == ExpressionTokenKind.DateTimeOffsetLiteral || this.Kind == ExpressionTokenKind.DurationLiteral || this.Kind == ExpressionTokenKind.GuidLiteral || this.Kind == ExpressionTokenKind.StringLiteral || this.Kind == ExpressionTokenKind.GeographyLiteral || this.Kind == ExpressionTokenKind.GeometryLiteral || this.Kind == ExpressionTokenKind.QuotedLiteral || this.Kind == ExpressionTokenKind.TimeOfDayLiteral || ExpressionLexerUtils.IsNumeric(this.Kind);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002158C File Offset: 0x0001F78C
		internal bool IsFunctionParameterToken
		{
			get
			{
				return this.IsKeyValueToken || this.Kind == ExpressionTokenKind.BracketedExpression || this.Kind == ExpressionTokenKind.BracedExpression || this.Kind == ExpressionTokenKind.NullLiteral;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000215B5 File Offset: 0x0001F7B5
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} @ {1}: [{2}]", new object[] { this.Kind, this.Position, this.Text });
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000215F4 File Offset: 0x0001F7F4
		internal string GetIdentifier()
		{
			if (this.Kind != ExpressionTokenKind.Identifier)
			{
				string text = Strings.ExpressionToken_IdentifierExpected(this.Position);
				throw new ODataException(text);
			}
			return this.Text;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00021628 File Offset: 0x0001F828
		internal bool IdentifierIs(string id, bool enableCaseInsensitive)
		{
			return this.Kind == ExpressionTokenKind.Identifier && string.Equals(this.Text, id, enableCaseInsensitive ? 5 : 4);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00021648 File Offset: 0x0001F848
		internal void SetCustomEdmTypeLiteral(IEdmTypeReference edmType)
		{
			this.Kind = ExpressionTokenKind.CustomTypeLiteral;
			this.LiteralEdmType = edmType;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00021659 File Offset: 0x0001F859
		internal IEdmTypeReference GetLiteralEdmTypeReference()
		{
			if (this.LiteralEdmType == null && this.Kind != ExpressionTokenKind.CustomTypeLiteral)
			{
				this.LiteralEdmType = UriParserHelper.GetLiteralEdmTypeReference(this.Kind);
			}
			return this.LiteralEdmType;
		}

		// Token: 0x040006AA RID: 1706
		internal static readonly ExpressionToken GreaterThan = new ExpressionToken
		{
			Text = "gt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040006AB RID: 1707
		internal static readonly ExpressionToken EqualsTo = new ExpressionToken
		{
			Text = "eq",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040006AC RID: 1708
		internal static readonly ExpressionToken LessThan = new ExpressionToken
		{
			Text = "lt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040006AD RID: 1709
		internal ExpressionTokenKind Kind;

		// Token: 0x040006AE RID: 1710
		internal string Text;

		// Token: 0x040006AF RID: 1711
		internal int Position;

		// Token: 0x040006B0 RID: 1712
		private IEdmTypeReference LiteralEdmType;
	}
}
