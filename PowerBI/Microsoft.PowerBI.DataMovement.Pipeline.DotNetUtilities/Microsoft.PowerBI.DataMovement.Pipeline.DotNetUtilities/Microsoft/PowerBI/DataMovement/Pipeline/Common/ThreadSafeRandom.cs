using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public static class ThreadSafeRandom
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002498 File Offset: 0x00000698
		private static Random RandomGenerator
		{
			get
			{
				if (ThreadSafeRandom.s_randomGenerator == null)
				{
					ThreadSafeRandom.s_randomGenerator = new Random((int)DateTime.UtcNow.Ticks ^ Thread.CurrentThread.GetHashCode());
				}
				return ThreadSafeRandom.s_randomGenerator;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D4 File Offset: 0x000006D4
		public static int Next()
		{
			return ThreadSafeRandom.RandomGenerator.Next();
		}

		// Token: 0x04000019 RID: 25
		[ThreadStatic]
		private static Random s_randomGenerator;
	}
}
