using System;

namespace antlr
{
	// Token: 0x02000021 RID: 33
	internal class ParserSharedInputState
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00004C72 File Offset: 0x00002E72
		public virtual void reset()
		{
			this.guessing = 0;
			this.filename = null;
			this.input.reset();
		}

		// Token: 0x04000084 RID: 132
		protected internal TokenBuffer input;

		// Token: 0x04000085 RID: 133
		public int guessing;

		// Token: 0x04000086 RID: 134
		protected internal string filename;
	}
}
