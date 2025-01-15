using System;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x020020C7 RID: 8391
	internal class SecureObjectPool
	{
		// Token: 0x060119C2 RID: 72130 RVA: 0x003C35A4 File Offset: 0x003C17A4
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref SecureObjectPool.s_poolUserIdCounter);
			}
			while (num == -1);
			return num;
		}

		// Token: 0x0400699A RID: 27034
		private static int s_poolUserIdCounter;

		// Token: 0x0400699B RID: 27035
		internal const int UnassignedId = -1;
	}
}
