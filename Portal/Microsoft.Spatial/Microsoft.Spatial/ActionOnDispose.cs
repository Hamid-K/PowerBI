using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200003A RID: 58
	internal class ActionOnDispose : IDisposable
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00004B12 File Offset: 0x00002D12
		public ActionOnDispose(Action action)
		{
			Util.CheckArgumentNull(action, "action");
			this.action = action;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004B2C File Offset: 0x00002D2C
		public void Dispose()
		{
			if (this.action != null)
			{
				this.action();
				this.action = null;
			}
		}

		// Token: 0x0400003D RID: 61
		private Action action;
	}
}
