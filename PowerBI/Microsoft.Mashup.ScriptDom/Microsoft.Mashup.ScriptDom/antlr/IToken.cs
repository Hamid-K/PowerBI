using System;

namespace antlr
{
	// Token: 0x0200000E RID: 14
	internal interface IToken
	{
		// Token: 0x0600009B RID: 155
		int getColumn();

		// Token: 0x0600009C RID: 156
		void setColumn(int c);

		// Token: 0x0600009D RID: 157
		int getLine();

		// Token: 0x0600009E RID: 158
		void setLine(int l);

		// Token: 0x0600009F RID: 159
		string getFilename();

		// Token: 0x060000A0 RID: 160
		void setFilename(string name);

		// Token: 0x060000A1 RID: 161
		string getText();

		// Token: 0x060000A2 RID: 162
		void setText(string t);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		int Type { get; set; }
	}
}
