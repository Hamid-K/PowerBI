using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E92 RID: 7826
	public class DbLiteralInfo
	{
		// Token: 0x0600C183 RID: 49539 RVA: 0x0026E5BE File Offset: 0x0026C7BE
		public DbLiteralInfo(DBLITERAL literal, string literalValue, string invalidChars, string invalidStartingChars, int maxLength)
		{
			this.literal = literal;
			this.literalValue = literalValue;
			this.invalidChars = invalidChars;
			this.invalidStartingChars = invalidStartingChars;
			this.maxLength = maxLength;
		}

		// Token: 0x17002F54 RID: 12116
		// (get) Token: 0x0600C184 RID: 49540 RVA: 0x0026E5EB File Offset: 0x0026C7EB
		public DBLITERAL Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x17002F55 RID: 12117
		// (get) Token: 0x0600C185 RID: 49541 RVA: 0x0026E5F3 File Offset: 0x0026C7F3
		public string LiteralValue
		{
			get
			{
				return this.literalValue;
			}
		}

		// Token: 0x17002F56 RID: 12118
		// (get) Token: 0x0600C186 RID: 49542 RVA: 0x0026E5FB File Offset: 0x0026C7FB
		public string InvalidChars
		{
			get
			{
				return this.invalidChars;
			}
		}

		// Token: 0x17002F57 RID: 12119
		// (get) Token: 0x0600C187 RID: 49543 RVA: 0x0026E603 File Offset: 0x0026C803
		public string InvalidStartingChars
		{
			get
			{
				return this.invalidStartingChars;
			}
		}

		// Token: 0x17002F58 RID: 12120
		// (get) Token: 0x0600C188 RID: 49544 RVA: 0x0026E60B File Offset: 0x0026C80B
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x04006196 RID: 24982
		private readonly DBLITERAL literal;

		// Token: 0x04006197 RID: 24983
		private readonly string literalValue;

		// Token: 0x04006198 RID: 24984
		private readonly string invalidChars;

		// Token: 0x04006199 RID: 24985
		private readonly string invalidStartingChars;

		// Token: 0x0400619A RID: 24986
		private readonly int maxLength;
	}
}
