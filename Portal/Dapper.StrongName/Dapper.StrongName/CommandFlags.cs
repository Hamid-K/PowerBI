using System;

namespace Dapper
{
	// Token: 0x02000004 RID: 4
	[Flags]
	public enum CommandFlags
	{
		// Token: 0x04000013 RID: 19
		None = 0,
		// Token: 0x04000014 RID: 20
		Buffered = 1,
		// Token: 0x04000015 RID: 21
		Pipelined = 2,
		// Token: 0x04000016 RID: 22
		NoCache = 4
	}
}
