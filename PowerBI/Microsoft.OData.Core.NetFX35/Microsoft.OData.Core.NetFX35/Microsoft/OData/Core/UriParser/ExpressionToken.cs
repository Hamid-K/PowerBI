using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.OData.Core.UriParser.Parsers.UriParsers;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001E4 RID: 484
	[DebuggerDisplay("{InternalKind} @ {Position}: [{Text}]")]
	internal struct ExpressionToken
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0003FEEC File Offset: 0x0003E0EC
		internal bool IsKeyValueToken
		{
			get
			{
				return this.Kind == ExpressionTokenKind.BinaryLiteral || this.Kind == ExpressionTokenKind.BooleanLiteral || this.Kind == ExpressionTokenKind.DateLiteral || this.Kind == ExpressionTokenKind.DateTimeLiteral || this.Kind == ExpressionTokenKind.DateTimeOffsetLiteral || this.Kind == ExpressionTokenKind.DurationLiteral || this.Kind == ExpressionTokenKind.GuidLiteral || this.Kind == ExpressionTokenKind.StringLiteral || this.Kind == ExpressionTokenKind.GeographyLiteral || this.Kind == ExpressionTokenKind.GeometryLiteral || this.Kind == ExpressionTokenKind.QuotedLiteral || this.Kind == ExpressionTokenKind.TimeOfDayLiteral || ExpressionLexerUtils.IsNumeric(this.Kind);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0003FF7C File Offset: 0x0003E17C
		internal bool IsFunctionParameterToken
		{
			get
			{
				return this.IsKeyValueToken || this.Kind == ExpressionTokenKind.BracketedExpression || this.Kind == ExpressionTokenKind.NullLiteral;
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003FF9C File Offset: 0x0003E19C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} @ {1}: [{2}]", new object[] { this.Kind, this.Position, this.Text });
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		internal string GetIdentifier()
		{
			if (this.Kind != ExpressionTokenKind.Identifier)
			{
				string text = Strings.ExpressionToken_IdentifierExpected(this.Position);
				throw new ODataException(text);
			}
			return this.Text;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004001C File Offset: 0x0003E21C
		internal bool IdentifierIs(string id, bool enableCaseInsensitive)
		{
			return this.Kind == ExpressionTokenKind.Identifier && string.Equals(this.Text, id, enableCaseInsensitive ? 5 : 4);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004003C File Offset: 0x0003E23C
		internal void SetCustomEdmTypeLiteral(IEdmTypeReference edmType)
		{
			this.Kind = ExpressionTokenKind.CustomTypeLiteral;
			this.LiteralEdmType = edmType;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004004D File Offset: 0x0003E24D
		internal IEdmTypeReference GetLiteralEdmTypeReference()
		{
			if (this.LiteralEdmType == null && this.Kind != ExpressionTokenKind.CustomTypeLiteral)
			{
				this.LiteralEdmType = UriParserHelper.GetLiteralEdmTypeReference(this.Kind);
			}
			return this.LiteralEdmType;
		}

		// Token: 0x040007A3 RID: 1955
		internal static readonly ExpressionToken GreaterThan = new ExpressionToken
		{
			Text = "gt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040007A4 RID: 1956
		internal static readonly ExpressionToken EqualsTo = new ExpressionToken
		{
			Text = "eq",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040007A5 RID: 1957
		internal static readonly ExpressionToken LessThan = new ExpressionToken
		{
			Text = "lt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040007A6 RID: 1958
		internal ExpressionTokenKind Kind;

		// Token: 0x040007A7 RID: 1959
		internal string Text;

		// Token: 0x040007A8 RID: 1960
		internal int Position;

		// Token: 0x040007A9 RID: 1961
		private IEdmTypeReference LiteralEdmType;
	}
}
