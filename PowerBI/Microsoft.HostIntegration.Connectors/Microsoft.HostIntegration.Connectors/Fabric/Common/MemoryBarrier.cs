using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003EB RID: 1003
	internal static class MemoryBarrier
	{
		// Token: 0x06002347 RID: 9031 RVA: 0x0006C66E File Offset: 0x0006A86E
		[Conditional("WEAK_MEMORY_MODEL")]
		public static void WriteBarrier()
		{
			MemoryBarrier.FullBarrier();
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0006C66E File Offset: 0x0006A86E
		public static void ReadBarrier()
		{
			MemoryBarrier.FullBarrier();
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0006C675 File Offset: 0x0006A875
		public static void FullBarrier()
		{
			Thread.MemoryBarrier();
		}
	}
}
