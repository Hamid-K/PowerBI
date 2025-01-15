using System;

namespace antlr
{
	// Token: 0x0200000F RID: 15
	internal class Token : IToken
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003AEA File Offset: 0x00001CEA
		public Token()
		{
			this.type_ = 0;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003AF9 File Offset: 0x00001CF9
		public Token(int t)
		{
			this.type_ = t;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B08 File Offset: 0x00001D08
		public Token(int t, string txt)
		{
			this.type_ = t;
			this.setText(txt);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003B1E File Offset: 0x00001D1E
		public virtual int getColumn()
		{
			return 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B21 File Offset: 0x00001D21
		public virtual int getLine()
		{
			return 0;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B24 File Offset: 0x00001D24
		public virtual string getFilename()
		{
			return null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B27 File Offset: 0x00001D27
		public virtual void setFilename(string name)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual string getText()
		{
			return "<no text>";
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003B30 File Offset: 0x00001D30
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003B38 File Offset: 0x00001D38
		public int Type
		{
			get
			{
				return this.type_;
			}
			set
			{
				this.type_ = value;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B41 File Offset: 0x00001D41
		public virtual void setType(int newType)
		{
			this.Type = newType;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003B4A File Offset: 0x00001D4A
		public virtual void setColumn(int c)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003B4C File Offset: 0x00001D4C
		public virtual void setLine(int l)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003B4E File Offset: 0x00001D4E
		public virtual void setText(string t)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003B50 File Offset: 0x00001D50
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[\"",
				this.getText(),
				"\",<",
				this.type_,
				">]"
			});
		}

		// Token: 0x04000039 RID: 57
		public const int MIN_USER_TYPE = 4;

		// Token: 0x0400003A RID: 58
		public const int NULL_TREE_LOOKAHEAD = 3;

		// Token: 0x0400003B RID: 59
		public const int INVALID_TYPE = 0;

		// Token: 0x0400003C RID: 60
		public const int EOF_TYPE = 1;

		// Token: 0x0400003D RID: 61
		public static readonly int SKIP = -1;

		// Token: 0x0400003E RID: 62
		protected int type_;

		// Token: 0x0400003F RID: 63
		public static Token badToken = new Token(0, "<no text>");
	}
}
