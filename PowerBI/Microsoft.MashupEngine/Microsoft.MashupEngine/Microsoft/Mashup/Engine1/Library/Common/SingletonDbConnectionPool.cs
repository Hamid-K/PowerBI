using System;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001128 RID: 4392
	internal static class SingletonDbConnectionPool
	{
		// Token: 0x060072CB RID: 29387 RVA: 0x0018A718 File Offset: 0x00188918
		public static SingletonDbConnectionPool<T> New<T>(T connection) where T : DbConnection
		{
			return new SingletonDbConnectionPool<T>(connection);
		}
	}
}
