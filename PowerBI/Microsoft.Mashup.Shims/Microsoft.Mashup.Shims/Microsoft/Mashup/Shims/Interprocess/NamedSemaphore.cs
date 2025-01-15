using System;
using System.Threading;

namespace Microsoft.Mashup.Shims.Interprocess
{
	// Token: 0x02000015 RID: 21
	public class NamedSemaphore : Waitable
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002491 File Offset: 0x00000691
		public NamedSemaphore(string name)
			: this(name, 0)
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000249B File Offset: 0x0000069B
		protected override WaitHandle WaitHandle
		{
			get
			{
				return this.semaphore;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024A3 File Offset: 0x000006A3
		public NamedSemaphore(string name, int initialCount)
		{
			this.semaphore = new Semaphore(initialCount, int.MaxValue, name);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024BD File Offset: 0x000006BD
		public void Release()
		{
			this.semaphore.Release();
		}

		// Token: 0x04000008 RID: 8
		private readonly Semaphore semaphore;
	}
}
