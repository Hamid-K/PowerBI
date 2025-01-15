using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200003F RID: 63
	[DebuggerDisplay("{Kind} @ {Position}: [{Text}]")]
	internal struct ExpressionToken
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000939C File Offset: 0x0000759C
		internal bool IsComparisonOperator
		{
			get
			{
				return this.Kind == ExpressionTokenKind.Identifier && (this.Text == "eq" || this.Text == "ne" || this.Text == "lt" || this.Text == "gt" || this.Text == "le" || this.Text == "ge");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009420 File Offset: 0x00007620
		internal bool IsEqualityOperator
		{
			get
			{
				return this.Kind == ExpressionTokenKind.Identifier && (this.Text == "eq" || this.Text == "ne");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009454 File Offset: 0x00007654
		internal bool IsKeyValueToken
		{
			get
			{
				return this.Kind == ExpressionTokenKind.BinaryLiteral || this.Kind == ExpressionTokenKind.BooleanLiteral || this.Kind == ExpressionTokenKind.DateTimeLiteral || this.Kind == ExpressionTokenKind.DateTimeOffsetLiteral || this.Kind == ExpressionTokenKind.TimeLiteral || this.Kind == ExpressionTokenKind.GuidLiteral || this.Kind == ExpressionTokenKind.StringLiteral || this.Kind == ExpressionTokenKind.GeographyLiteral || this.Kind == ExpressionTokenKind.GeometryLiteral || ExpressionLexer.IsNumeric(this.Kind);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000094C8 File Offset: 0x000076C8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} @ {1}: [{2}]", new object[] { this.Kind, this.Position, this.Text });
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009511 File Offset: 0x00007711
		internal string GetIdentifier()
		{
			if (this.Kind != ExpressionTokenKind.Identifier)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionToken_IdentifierExpected(this.Position));
			}
			return this.Text;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009538 File Offset: 0x00007738
		internal bool IdentifierIs(string id)
		{
			return this.Kind == ExpressionTokenKind.Identifier && this.Text == id;
		}

		// Token: 0x0400018A RID: 394
		internal static readonly ExpressionToken GreaterThan = new ExpressionToken
		{
			Text = "gt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x0400018B RID: 395
		internal static readonly ExpressionToken EqualsTo = new ExpressionToken
		{
			Text = "eq",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x0400018C RID: 396
		internal static readonly ExpressionToken LessThan = new ExpressionToken
		{
			Text = "lt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x0400018D RID: 397
		internal ExpressionTokenKind Kind;

		// Token: 0x0400018E RID: 398
		internal string Text;

		// Token: 0x0400018F RID: 399
		internal int Position;
	}
}
