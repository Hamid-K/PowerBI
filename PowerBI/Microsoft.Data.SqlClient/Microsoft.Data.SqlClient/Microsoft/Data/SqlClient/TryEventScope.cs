using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200005F RID: 95
	internal readonly ref struct TryEventScope
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00016BAF File Offset: 0x00014DAF
		public TryEventScope(long scopeID)
		{
			this._scopeId = scopeID;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public void Dispose()
		{
			if (this._scopeId != 0L)
			{
				SqlClientEventSource.Log.TryScopeLeaveEvent(this._scopeId);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00016BD2 File Offset: 0x00014DD2
		public static TryEventScope Create<T0>(string message, T0 args0)
		{
			return new TryEventScope(SqlClientEventSource.Log.TryScopeEnterEvent<T0>(message, args0));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00016BE5 File Offset: 0x00014DE5
		public static TryEventScope Create<T0, T1>(string message, T0 args0, T1 args1)
		{
			return new TryEventScope(SqlClientEventSource.Log.TryScopeEnterEvent<T0, T1>(message, args0, args1));
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00016BF9 File Offset: 0x00014DF9
		public static TryEventScope Create(string className, [CallerMemberName] string memberName = "")
		{
			return new TryEventScope(SqlClientEventSource.Log.TryScopeEnterEvent(className, memberName));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00016C0C File Offset: 0x00014E0C
		public static TryEventScope Create(long scopeId)
		{
			return new TryEventScope(scopeId);
		}

		// Token: 0x0400015F RID: 351
		private readonly long _scopeId;
	}
}
