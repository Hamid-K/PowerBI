using System;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200003B RID: 59
	[Flags]
	public enum ImportOptions
	{
		// Token: 0x0400007F RID: 127
		Once = 1,
		// Token: 0x04000080 RID: 128
		Multiple = 2,
		// Token: 0x04000081 RID: 129
		Optional = 4,
		// Token: 0x04000082 RID: 130
		Css = 8,
		// Token: 0x04000083 RID: 131
		Less = 16,
		// Token: 0x04000084 RID: 132
		Inline = 32,
		// Token: 0x04000085 RID: 133
		Reference = 64
	}
}
