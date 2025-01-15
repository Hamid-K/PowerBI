using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	internal class MismatchedTokenException : RecognitionException
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000047D7 File Offset: 0x000029D7
		public MismatchedTokenException()
			: base("Mismatched Token: expecting any AST node", "<AST>", -1, -1)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000047EC File Offset: 0x000029EC
		public MismatchedTokenException(string[] tokenNames_, IToken token_, int lower, int upper_, bool matchNot, string fileName_)
			: base("Mismatched Token", fileName_, token_.getLine(), token_.getColumn())
		{
			this.tokenNames = tokenNames_;
			this.token = token_;
			this.tokenText = token_.getText();
			this.mismatchType = (matchNot ? MismatchedTokenException.TokenTypeEnum.NotRangeType : MismatchedTokenException.TokenTypeEnum.RangeType);
			this.expecting = lower;
			this.upper = upper_;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000484C File Offset: 0x00002A4C
		public MismatchedTokenException(string[] tokenNames_, IToken token_, int expecting_, bool matchNot, string fileName_)
			: base("Mismatched Token", fileName_, token_.getLine(), token_.getColumn())
		{
			this.tokenNames = tokenNames_;
			this.token = token_;
			this.tokenText = token_.getText();
			this.mismatchType = (matchNot ? MismatchedTokenException.TokenTypeEnum.NotTokenType : MismatchedTokenException.TokenTypeEnum.TokenType);
			this.expecting = expecting_;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000048A4 File Offset: 0x00002AA4
		public MismatchedTokenException(string[] tokenNames_, IToken token_, BitSet set_, bool matchNot, string fileName_)
			: base("Mismatched Token", fileName_, token_.getLine(), token_.getColumn())
		{
			this.tokenNames = tokenNames_;
			this.token = token_;
			this.tokenText = token_.getText();
			this.mismatchType = (matchNot ? MismatchedTokenException.TokenTypeEnum.NotSetType : MismatchedTokenException.TokenTypeEnum.SetType);
			this.bset = set_;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000048F9 File Offset: 0x00002AF9
		protected MismatchedTokenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004904 File Offset: 0x00002B04
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				switch (this.mismatchType)
				{
				case MismatchedTokenException.TokenTypeEnum.TokenType:
					stringBuilder.Append(string.Concat(new string[]
					{
						"expecting ",
						this.tokenName(this.expecting),
						", found '",
						this.tokenText,
						"'"
					}));
					break;
				case MismatchedTokenException.TokenTypeEnum.NotTokenType:
					stringBuilder.Append("expecting anything but " + this.tokenName(this.expecting) + "; got it anyway");
					break;
				case MismatchedTokenException.TokenTypeEnum.RangeType:
					stringBuilder.Append(string.Concat(new string[]
					{
						"expecting token in range: ",
						this.tokenName(this.expecting),
						"..",
						this.tokenName(this.upper),
						", found '",
						this.tokenText,
						"'"
					}));
					break;
				case MismatchedTokenException.TokenTypeEnum.NotRangeType:
					stringBuilder.Append(string.Concat(new string[]
					{
						"expecting token NOT in range: ",
						this.tokenName(this.expecting),
						"..",
						this.tokenName(this.upper),
						", found '",
						this.tokenText,
						"'"
					}));
					break;
				case MismatchedTokenException.TokenTypeEnum.SetType:
				case MismatchedTokenException.TokenTypeEnum.NotSetType:
				{
					stringBuilder.Append("expecting " + ((this.mismatchType == MismatchedTokenException.TokenTypeEnum.NotSetType) ? "NOT " : "") + "one of (");
					int[] array = this.bset.toArray();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(this.tokenName(array[i]));
					}
					stringBuilder.Append("), found '" + this.tokenText + "'");
					break;
				}
				default:
					stringBuilder.Append(base.Message);
					break;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B1E File Offset: 0x00002D1E
		private string tokenName(int tokenType)
		{
			if (tokenType == 0)
			{
				return "<Set of tokens>";
			}
			if (tokenType < 0 || tokenType >= this.tokenNames.Length)
			{
				return "<" + tokenType.ToString(CultureInfo.InvariantCulture) + ">";
			}
			return this.tokenNames[tokenType];
		}

		// Token: 0x04000074 RID: 116
		internal string[] tokenNames;

		// Token: 0x04000075 RID: 117
		public IToken token;

		// Token: 0x04000076 RID: 118
		internal string tokenText;

		// Token: 0x04000077 RID: 119
		public MismatchedTokenException.TokenTypeEnum mismatchType;

		// Token: 0x04000078 RID: 120
		public int expecting;

		// Token: 0x04000079 RID: 121
		public int upper;

		// Token: 0x0400007A RID: 122
		public BitSet bset;

		// Token: 0x0200001E RID: 30
		internal enum TokenTypeEnum
		{
			// Token: 0x0400007C RID: 124
			TokenType = 1,
			// Token: 0x0400007D RID: 125
			NotTokenType,
			// Token: 0x0400007E RID: 126
			RangeType,
			// Token: 0x0400007F RID: 127
			NotRangeType,
			// Token: 0x04000080 RID: 128
			SetType,
			// Token: 0x04000081 RID: 129
			NotSetType
		}
	}
}
