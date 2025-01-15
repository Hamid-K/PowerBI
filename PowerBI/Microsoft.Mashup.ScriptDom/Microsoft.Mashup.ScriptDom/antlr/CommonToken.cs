using System;

namespace antlr
{
	// Token: 0x02000010 RID: 16
	internal class CommonToken : Token
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003BB1 File Offset: 0x00001DB1
		public CommonToken()
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003BB9 File Offset: 0x00001DB9
		public CommonToken(int t, string txt)
		{
			this.type_ = t;
			this.setText(txt);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003BCF File Offset: 0x00001DCF
		public CommonToken(string s)
		{
			this.text = s;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003BDE File Offset: 0x00001DDE
		public override int getLine()
		{
			return this.line;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003BE6 File Offset: 0x00001DE6
		public override string getText()
		{
			return this.text;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003BEE File Offset: 0x00001DEE
		public override void setLine(int l)
		{
			this.line = l;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003BF7 File Offset: 0x00001DF7
		public override void setText(string s)
		{
			this.text = s;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003C00 File Offset: 0x00001E00
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[\"",
				this.getText(),
				"\",<",
				this.type_,
				">,line=",
				this.line,
				",col=",
				this.col,
				"]"
			});
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003C76 File Offset: 0x00001E76
		public override int getColumn()
		{
			return this.col;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003C7E File Offset: 0x00001E7E
		public override void setColumn(int c)
		{
			this.col = c;
		}

		// Token: 0x04000040 RID: 64
		public static readonly CommonToken.CommonTokenCreator Creator = new CommonToken.CommonTokenCreator();

		// Token: 0x04000041 RID: 65
		protected internal int line;

		// Token: 0x04000042 RID: 66
		protected internal string text;

		// Token: 0x04000043 RID: 67
		protected internal int col;

		// Token: 0x02000011 RID: 17
		internal class CommonTokenCreator : TokenCreator
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003C9B File Offset: 0x00001E9B
			public override string TokenTypeName
			{
				get
				{
					return typeof(CommonToken).FullName;
				}
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00003CAC File Offset: 0x00001EAC
			public override IToken Create()
			{
				return new CommonToken();
			}
		}
	}
}
