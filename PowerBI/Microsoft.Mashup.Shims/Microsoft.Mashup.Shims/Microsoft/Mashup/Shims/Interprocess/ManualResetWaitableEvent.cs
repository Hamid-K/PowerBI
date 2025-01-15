using System;
using System.Threading;

namespace Microsoft.Mashup.Shims.Interprocess
{
	// Token: 0x02000013 RID: 19
	public sealed class ManualResetWaitableEvent : Waitable
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002406 File Offset: 0x00000606
		private ManualResetWaitableEvent(string name, bool initialState)
		{
			this.eventWaitHandle = new EventWaitHandle(initialState, EventResetMode.ManualReset, name);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000241C File Offset: 0x0000061C
		protected override WaitHandle WaitHandle
		{
			get
			{
				return this.eventWaitHandle;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002424 File Offset: 0x00000624
		public static ManualResetWaitableEvent Create(bool initialState = false)
		{
			return new ManualResetWaitableEvent(null, initialState);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000242D File Offset: 0x0000062D
		public static ManualResetWaitableEvent Create(string name, bool initialState = false)
		{
			return new ManualResetWaitableEvent(name, initialState);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002436 File Offset: 0x00000636
		public void Set()
		{
			this.eventWaitHandle.Set();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002444 File Offset: 0x00000644
		public void Reset()
		{
			this.eventWaitHandle.Reset();
		}

		// Token: 0x04000007 RID: 7
		private readonly EventWaitHandle eventWaitHandle;
	}
}
