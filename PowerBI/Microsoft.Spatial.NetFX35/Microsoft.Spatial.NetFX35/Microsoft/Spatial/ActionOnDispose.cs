using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200003B RID: 59
	internal class ActionOnDispose : IDisposable
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000048B2 File Offset: 0x00002AB2
		public ActionOnDispose(Action action)
		{
			Util.CheckArgumentNull(action, "action");
			this.action = action;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000048CC File Offset: 0x00002ACC
		public void Dispose()
		{
			if (this.action != null)
			{
				this.action.Invoke();
				this.action = null;
			}
		}

		// Token: 0x04000037 RID: 55
		private Action action;
	}
}
