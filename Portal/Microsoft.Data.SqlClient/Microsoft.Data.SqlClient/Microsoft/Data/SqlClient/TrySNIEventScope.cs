using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200005E RID: 94
	internal readonly struct TrySNIEventScope : IDisposable
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x00016B78 File Offset: 0x00014D78
		public TrySNIEventScope(long scopeID)
		{
			this._scopeId = scopeID;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00016B81 File Offset: 0x00014D81
		public void Dispose()
		{
			if (this._scopeId == 0L)
			{
				return;
			}
			SqlClientEventSource.Log.TrySNIScopeLeaveEvent(this._scopeId);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00016B9C File Offset: 0x00014D9C
		public static TrySNIEventScope Create(string className, [CallerMemberName] string memberName = "")
		{
			return new TrySNIEventScope(SqlClientEventSource.Log.TrySNIScopeEnterEvent(className, memberName));
		}

		// Token: 0x0400015E RID: 350
		private readonly long _scopeId;
	}
}
