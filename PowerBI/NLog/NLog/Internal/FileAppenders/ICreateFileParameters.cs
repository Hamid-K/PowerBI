using System;
using NLog.Targets;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000161 RID: 353
	internal interface ICreateFileParameters
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060010AF RID: 4271
		int ConcurrentWriteAttemptDelay { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060010B0 RID: 4272
		int ConcurrentWriteAttempts { get; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060010B1 RID: 4273
		bool ConcurrentWrites { get; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060010B2 RID: 4274
		bool CreateDirs { get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060010B3 RID: 4275
		bool EnableFileDelete { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060010B4 RID: 4276
		int BufferSize { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060010B5 RID: 4277
		bool ForceManaged { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060010B6 RID: 4278
		Win32FileAttributes FileAttributes { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060010B7 RID: 4279
		bool IsArchivingEnabled { get; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060010B8 RID: 4280
		bool EnableFileDeleteSimpleMonitor { get; }
	}
}
