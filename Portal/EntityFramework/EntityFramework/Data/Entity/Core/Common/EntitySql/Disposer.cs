using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000649 RID: 1609
	internal class Disposer : IDisposable
	{
		// Token: 0x06004DC6 RID: 19910 RVA: 0x00117DE4 File Offset: 0x00115FE4
		internal Disposer(Action action)
		{
			this._action = action;
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x00117DF3 File Offset: 0x00115FF3
		public void Dispose()
		{
			this._action();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001C1F RID: 7199
		private readonly Action _action;
	}
}
