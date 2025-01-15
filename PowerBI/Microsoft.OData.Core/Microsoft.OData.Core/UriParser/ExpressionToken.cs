using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013F RID: 319
	[DebuggerDisplay("{InternalKind} @ {Position}: [{Text}]")]
	internal struct ExpressionToken
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0002EB38 File Offset: 0x0002CD38
		internal bool IsKeyValueToken
		{
			get
			{
				return this.Kind == ExpressionTokenKind.BinaryLiteral || this.Kind == ExpressionTokenKind.BooleanLiteral || this.Kind == ExpressionTokenKind.DateLiteral || this.Kind == ExpressionTokenKind.DateTimeLiteral || this.Kind == ExpressionTokenKind.DateTimeOffsetLiteral || this.Kind == ExpressionTokenKind.DurationLiteral || this.Kind == ExpressionTokenKind.GuidLiteral || this.Kind == ExpressionTokenKind.StringLiteral || this.Kind == ExpressionTokenKind.GeographyLiteral || this.Kind == ExpressionTokenKind.GeometryLiteral || this.Kind == ExpressionTokenKind.QuotedLiteral || this.Kind == ExpressionTokenKind.TimeOfDayLiteral || ExpressionLexerUtils.IsNumeric(this.Kind);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0002EBC8 File Offset: 0x0002CDC8
		internal bool IsFunctionParameterToken
		{
			get
			{
				return this.IsKeyValueToken || this.Kind == ExpressionTokenKind.BracketedExpression || this.Kind == ExpressionTokenKind.BracedExpression || this.Kind == ExpressionTokenKind.NullLiteral;
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0002EBF1 File Offset: 0x0002CDF1
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} @ {1}: [{2}]", new object[] { this.Kind, this.Position, this.Text });
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0002EC30 File Offset: 0x0002CE30
		internal string GetIdentifier()
		{
			if (this.Kind != ExpressionTokenKind.Identifier)
			{
				string text = Strings.ExpressionToken_IdentifierExpected(this.Position);
				throw new ODataException(text);
			}
			return this.Text;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0002EC64 File Offset: 0x0002CE64
		internal bool IdentifierIs(string id, bool enableCaseInsensitive)
		{
			return this.Kind == ExpressionTokenKind.Identifier && string.Equals(this.Text, id, enableCaseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0002EC84 File Offset: 0x0002CE84
		internal void SetCustomEdmTypeLiteral(IEdmTypeReference edmType)
		{
			this.Kind = ExpressionTokenKind.CustomTypeLiteral;
			this.LiteralEdmType = edmType;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0002EC95 File Offset: 0x0002CE95
		internal IEdmTypeReference GetLiteralEdmTypeReference()
		{
			if (this.LiteralEdmType == null && this.Kind != ExpressionTokenKind.CustomTypeLiteral)
			{
				this.LiteralEdmType = UriParserHelper.GetLiteralEdmTypeReference(this.Kind);
			}
			return this.LiteralEdmType;
		}

		// Token: 0x040007C2 RID: 1986
		internal static readonly ExpressionToken GreaterThan = new ExpressionToken
		{
			Text = "gt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040007C3 RID: 1987
		internal static readonly ExpressionToken EqualsTo = new ExpressionToken
		{
			Text = "eq",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040007C4 RID: 1988
		internal static readonly ExpressionToken LessThan = new ExpressionToken
		{
			Text = "lt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040007C5 RID: 1989
		internal ExpressionTokenKind Kind;

		// Token: 0x040007C6 RID: 1990
		internal string Text;

		// Token: 0x040007C7 RID: 1991
		internal int Position;

		// Token: 0x040007C8 RID: 1992
		private IEdmTypeReference LiteralEdmType;
	}
}
