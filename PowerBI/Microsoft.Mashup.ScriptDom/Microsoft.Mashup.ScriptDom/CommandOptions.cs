using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000112 RID: 274
	[Flags]
	internal enum CommandOptions
	{
		// Token: 0x040010F4 RID: 4340
		None = 0,
		// Token: 0x040010F5 RID: 4341
		CreateDatabase = 1,
		// Token: 0x040010F6 RID: 4342
		CreateDefault = 2,
		// Token: 0x040010F7 RID: 4343
		CreateProcedure = 4,
		// Token: 0x040010F8 RID: 4344
		CreateFunction = 8,
		// Token: 0x040010F9 RID: 4345
		CreateRule = 16,
		// Token: 0x040010FA RID: 4346
		CreateTable = 32,
		// Token: 0x040010FB RID: 4347
		CreateView = 64,
		// Token: 0x040010FC RID: 4348
		BackupDatabase = 128,
		// Token: 0x040010FD RID: 4349
		BackupLog = 256
	}
}
