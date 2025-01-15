using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000CD RID: 205
	internal sealed class LiteralToken : QueryToken
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001159C File Offset: 0x0000F79C
		public LiteralToken(object value)
		{
			this.value = value;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000115AB File Offset: 0x0000F7AB
		internal LiteralToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000115BB File Offset: 0x0000F7BB
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000115BE File Offset: 0x0000F7BE
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000115C6 File Offset: 0x0000F7C6
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000115CE File Offset: 0x0000F7CE
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040001DA RID: 474
		private readonly string originalText;

		// Token: 0x040001DB RID: 475
		private readonly object value;
	}
}
