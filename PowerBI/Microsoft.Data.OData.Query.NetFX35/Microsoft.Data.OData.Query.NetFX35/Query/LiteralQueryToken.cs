using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000046 RID: 70
	public sealed class LiteralQueryToken : QueryToken
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000971B File Offset: 0x0000791B
		public LiteralQueryToken(object value)
		{
			this.value = value;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000972A File Offset: 0x0000792A
		internal LiteralQueryToken(object value, string originalText)
			: this(value)
		{
			this.originalText = originalText;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000973A File Offset: 0x0000793A
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Literal;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000973D File Offset: 0x0000793D
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009745 File Offset: 0x00007945
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x040001B8 RID: 440
		private readonly string originalText;

		// Token: 0x040001B9 RID: 441
		private readonly object value;
	}
}
