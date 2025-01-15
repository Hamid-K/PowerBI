using System;

namespace System.Spatial
{
	// Token: 0x02000039 RID: 57
	internal class ActionOnDispose : IDisposable
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000479A File Offset: 0x0000299A
		public ActionOnDispose(Action action)
		{
			Util.CheckArgumentNull(action, "action");
			this.action = action;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000047B4 File Offset: 0x000029B4
		public void Dispose()
		{
			if (this.action != null)
			{
				this.action.Invoke();
				this.action = null;
			}
		}

		// Token: 0x04000033 RID: 51
		private Action action;
	}
}
