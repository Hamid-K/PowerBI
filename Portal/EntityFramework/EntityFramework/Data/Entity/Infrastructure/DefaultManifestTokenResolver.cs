using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200023E RID: 574
	public class DefaultManifestTokenResolver : IManifestTokenResolver
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x0005450C File Offset: 0x0005270C
		public string ResolveManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
			Tuple<Type, string, string> tuple = Tuple.Create<Type, string, string>(connection.GetType(), DbInterception.Dispatch.Connection.GetDataSource(connection, dbInterceptionContext), DbInterception.Dispatch.Connection.GetDatabase(connection, dbInterceptionContext));
			return this._cachedTokens.GetOrAdd(tuple, (Tuple<Type, string, string> k) => DbProviderServices.GetProviderServices(connection).GetProviderManifestTokenChecked(connection));
		}

		// Token: 0x04000B2E RID: 2862
		private readonly ConcurrentDictionary<Tuple<Type, string, string>, string> _cachedTokens = new ConcurrentDictionary<Tuple<Type, string, string>, string>();
	}
}
