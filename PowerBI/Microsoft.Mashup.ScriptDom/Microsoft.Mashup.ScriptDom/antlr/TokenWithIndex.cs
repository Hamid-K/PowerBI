using System;

namespace antlr
{
	// Token: 0x02000032 RID: 50
	internal class TokenWithIndex : CommonToken
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00005ABD File Offset: 0x00003CBD
		public TokenWithIndex()
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005AC5 File Offset: 0x00003CC5
		public TokenWithIndex(int i, string t)
			: base(i, t)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005ACF File Offset: 0x00003CCF
		public void setIndex(int i)
		{
			this.index = i;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public int getIndex()
		{
			return this.index;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[",
				this.index,
				":\"",
				this.getText(),
				"\",<",
				base.Type,
				">,line=",
				this.line,
				",col=",
				this.col,
				"]\n"
			});
		}

		// Token: 0x040000A7 RID: 167
		private int index;
	}
}
