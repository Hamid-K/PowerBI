using System;
using System.Runtime.Serialization;
using System.Text;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	internal class MismatchedCharException : RecognitionException
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000044C0 File Offset: 0x000026C0
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				switch (this.mismatchType)
				{
				case MismatchedCharException.CharTypeEnum.CharType:
					stringBuilder.Append("expecting ");
					MismatchedCharException.appendCharName(stringBuilder, this.expecting);
					stringBuilder.Append(", found ");
					MismatchedCharException.appendCharName(stringBuilder, this.foundChar);
					break;
				case MismatchedCharException.CharTypeEnum.NotCharType:
					stringBuilder.Append("expecting anything but '");
					MismatchedCharException.appendCharName(stringBuilder, this.expecting);
					stringBuilder.Append("'; got it anyway");
					break;
				case MismatchedCharException.CharTypeEnum.RangeType:
				case MismatchedCharException.CharTypeEnum.NotRangeType:
					stringBuilder.Append("expecting token ");
					if (this.mismatchType == MismatchedCharException.CharTypeEnum.NotRangeType)
					{
						stringBuilder.Append("NOT ");
					}
					stringBuilder.Append("in range: ");
					MismatchedCharException.appendCharName(stringBuilder, this.expecting);
					stringBuilder.Append("..");
					MismatchedCharException.appendCharName(stringBuilder, this.upper);
					stringBuilder.Append(", found ");
					MismatchedCharException.appendCharName(stringBuilder, this.foundChar);
					break;
				case MismatchedCharException.CharTypeEnum.SetType:
				case MismatchedCharException.CharTypeEnum.NotSetType:
				{
					stringBuilder.Append("expecting " + ((this.mismatchType == MismatchedCharException.CharTypeEnum.NotSetType) ? "NOT " : "") + "one of (");
					int[] array = this.bset.toArray();
					for (int i = 0; i < array.Length; i++)
					{
						MismatchedCharException.appendCharName(stringBuilder, array[i]);
					}
					stringBuilder.Append("), found ");
					MismatchedCharException.appendCharName(stringBuilder, this.foundChar);
					break;
				}
				default:
					stringBuilder.Append(base.Message);
					break;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004643 File Offset: 0x00002843
		public MismatchedCharException()
			: base("Mismatched char")
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004650 File Offset: 0x00002850
		public MismatchedCharException(char c, char lower, char upper_, bool matchNot, CharScanner scanner_)
			: base("Mismatched char", scanner_.getFilename(), scanner_.getLine(), scanner_.getColumn())
		{
			this.mismatchType = (matchNot ? MismatchedCharException.CharTypeEnum.NotRangeType : MismatchedCharException.CharTypeEnum.RangeType);
			this.foundChar = (int)c;
			this.expecting = (int)lower;
			this.upper = (int)upper_;
			this.scanner = scanner_;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000046A8 File Offset: 0x000028A8
		public MismatchedCharException(char c, char expecting_, bool matchNot, CharScanner scanner_)
			: base("Mismatched char", scanner_.getFilename(), scanner_.getLine(), scanner_.getColumn())
		{
			this.mismatchType = (matchNot ? MismatchedCharException.CharTypeEnum.NotCharType : MismatchedCharException.CharTypeEnum.CharType);
			this.foundChar = (int)c;
			this.expecting = (int)expecting_;
			this.scanner = scanner_;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000046F8 File Offset: 0x000028F8
		public MismatchedCharException(char c, BitSet set_, bool matchNot, CharScanner scanner_)
			: base("Mismatched char", scanner_.getFilename(), scanner_.getLine(), scanner_.getColumn())
		{
			this.mismatchType = (matchNot ? MismatchedCharException.CharTypeEnum.NotSetType : MismatchedCharException.CharTypeEnum.SetType);
			this.foundChar = (int)c;
			this.bset = set_;
			this.scanner = scanner_;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004748 File Offset: 0x00002948
		protected MismatchedCharException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004754 File Offset: 0x00002954
		private static void appendCharName(StringBuilder sb, int c)
		{
			switch (c)
			{
			case 9:
				sb.Append("'\\t'");
				return;
			case 10:
				sb.Append("'\\n'");
				return;
			case 11:
			case 12:
				break;
			case 13:
				sb.Append("'\\r'");
				return;
			default:
				if (c == 65535)
				{
					sb.Append("'<EOF>'");
					return;
				}
				break;
			}
			sb.Append('\'');
			sb.Append((char)c);
			sb.Append('\'');
		}

		// Token: 0x04000067 RID: 103
		public MismatchedCharException.CharTypeEnum mismatchType;

		// Token: 0x04000068 RID: 104
		public int foundChar;

		// Token: 0x04000069 RID: 105
		public int expecting;

		// Token: 0x0400006A RID: 106
		public int upper;

		// Token: 0x0400006B RID: 107
		public BitSet bset;

		// Token: 0x0400006C RID: 108
		public CharScanner scanner;

		// Token: 0x0200001C RID: 28
		internal enum CharTypeEnum
		{
			// Token: 0x0400006E RID: 110
			CharType = 1,
			// Token: 0x0400006F RID: 111
			NotCharType,
			// Token: 0x04000070 RID: 112
			RangeType,
			// Token: 0x04000071 RID: 113
			NotRangeType,
			// Token: 0x04000072 RID: 114
			SetType,
			// Token: 0x04000073 RID: 115
			NotSetType
		}
	}
}
