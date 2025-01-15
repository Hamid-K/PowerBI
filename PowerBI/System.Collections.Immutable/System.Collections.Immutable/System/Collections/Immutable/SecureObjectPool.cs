using System;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000045 RID: 69
	internal class SecureObjectPool
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00009200 File Offset: 0x00007400
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

		// Token: 0x04000040 RID: 64
		private static int s_poolUserIdCounter;

		// Token: 0x04000041 RID: 65
		internal const int UnassignedId = -1;
	}
}
