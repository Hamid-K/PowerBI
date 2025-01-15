using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200003D RID: 61
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class DbLiteralInfo
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00006698 File Offset: 0x00004898
		public DbLiteralInfo(DBLITERAL literal, string literalValue, string invalidChars, string invalidStartingChars, int maxLength)
		{
			this.literal = literal;
			this.literalValue = literalValue;
			this.invalidChars = invalidChars;
			this.invalidStartingChars = invalidStartingChars;
			this.maxLength = maxLength;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000066C5 File Offset: 0x000048C5
		public DBLITERAL Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000066CD File Offset: 0x000048CD
		public string LiteralValue
		{
			get
			{
				return this.literalValue;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000066D5 File Offset: 0x000048D5
		public string InvalidChars
		{
			get
			{
				return this.invalidChars;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000066DD File Offset: 0x000048DD
		public string InvalidStartingChars
		{
			get
			{
				return this.invalidStartingChars;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000066E5 File Offset: 0x000048E5
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x04000081 RID: 129
		private readonly DBLITERAL literal;

		// Token: 0x04000082 RID: 130
		private readonly string literalValue;

		// Token: 0x04000083 RID: 131
		private readonly string invalidChars;

		// Token: 0x04000084 RID: 132
		private readonly string invalidStartingChars;

		// Token: 0x04000085 RID: 133
		private readonly int maxLength;
	}
}
