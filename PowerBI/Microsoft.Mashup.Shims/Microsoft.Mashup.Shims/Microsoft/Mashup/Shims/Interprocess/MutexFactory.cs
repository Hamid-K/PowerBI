using System;
using System.Threading;

namespace Microsoft.Mashup.Shims.Interprocess
{
	// Token: 0x02000014 RID: 20
	public static class MutexFactory
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002452 File Offset: 0x00000652
		public static Mutex Create(bool initiallyOwned)
		{
			return new Mutex(initiallyOwned);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000245C File Offset: 0x0000065C
		public static Mutex Create(bool initiallyOwned, string name)
		{
			bool flag;
			return MutexFactory.Create(initiallyOwned, name, out flag);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002472 File Offset: 0x00000672
		public static Mutex Create(bool initiallyOwned, string name, out bool createdNew)
		{
			return new Mutex(initiallyOwned, MutexFactory.GetMutexName(name), out createdNew);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002481 File Offset: 0x00000681
		public static Mutex OpenExisting(string name)
		{
			return Mutex.OpenExisting(MutexFactory.GetMutexName(name));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000248E File Offset: 0x0000068E
		private static string GetMutexName(string name)
		{
			return name;
		}
	}
}
