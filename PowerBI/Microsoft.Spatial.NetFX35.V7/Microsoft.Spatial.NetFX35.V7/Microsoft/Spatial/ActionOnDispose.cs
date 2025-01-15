using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000035 RID: 53
	internal class ActionOnDispose : IDisposable
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00003E3E File Offset: 0x0000203E
		public ActionOnDispose(Action action)
		{
			Util.CheckArgumentNull(action, "action");
			this.action = action;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00003E58 File Offset: 0x00002058
		public void Dispose()
		{
			if (this.action != null)
			{
				this.action.Invoke();
				this.action = null;
			}
		}

		// Token: 0x04000030 RID: 48
		private Action action;
	}
}
