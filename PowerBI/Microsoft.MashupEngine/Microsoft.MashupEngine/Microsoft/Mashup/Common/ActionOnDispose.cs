using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BD3 RID: 7123
	public class ActionOnDispose : IDisposable
	{
		// Token: 0x0600B1B6 RID: 45494 RVA: 0x00243FA3 File Offset: 0x002421A3
		public ActionOnDispose(Action action)
		{
			this.action = action;
		}

		// Token: 0x0600B1B7 RID: 45495 RVA: 0x00243FB4 File Offset: 0x002421B4
		public void Dispose()
		{
			Action action = this.action;
			this.action = null;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x04005B16 RID: 23318
		private Action action;
	}
}
