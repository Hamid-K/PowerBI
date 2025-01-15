using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace antlr
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class NoViableAltForCharException : RecognitionException
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00004BAC File Offset: 0x00002DAC
		public NoViableAltForCharException(char c, CharScanner scanner)
			: base("NoViableAlt", scanner.getFilename(), scanner.getLine(), scanner.getColumn())
		{
			this.foundChar = c;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004BD2 File Offset: 0x00002DD2
		public NoViableAltForCharException(char c, string fileName, int line, int column)
			: base("NoViableAlt", fileName, line, column)
		{
			this.foundChar = c;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004BEA File Offset: 0x00002DEA
		protected NoViableAltForCharException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("unexpected char: ");
				if (this.foundChar >= ' ' && this.foundChar <= '~')
				{
					stringBuilder.Append('\'');
					stringBuilder.Append(this.foundChar);
					stringBuilder.Append('\'');
				}
				else
				{
					stringBuilder.Append("0x");
					StringBuilder stringBuilder2 = stringBuilder;
					int num = (int)this.foundChar;
					stringBuilder2.Append(num.ToString("X", CultureInfo.InvariantCulture));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x04000083 RID: 131
		public char foundChar;
	}
}
