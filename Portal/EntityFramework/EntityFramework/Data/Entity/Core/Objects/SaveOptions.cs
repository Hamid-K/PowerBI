using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200042F RID: 1071
	[Flags]
	public enum SaveOptions
	{
		// Token: 0x040010D0 RID: 4304
		None = 0,
		// Token: 0x040010D1 RID: 4305
		AcceptAllChangesAfterSave = 1,
		// Token: 0x040010D2 RID: 4306
		DetectChangesBeforeSave = 2
	}
}
