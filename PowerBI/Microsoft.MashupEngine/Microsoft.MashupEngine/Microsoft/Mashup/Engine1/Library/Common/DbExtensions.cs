using System;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200108B RID: 4235
	internal static class DbExtensions
	{
		// Token: 0x06006EE1 RID: 28385 RVA: 0x0017E974 File Offset: 0x0017CB74
		public static void Open(this DbConnection connection, DbEnvironment environment)
		{
			environment.ConvertDbExceptions<object>(delegate
			{
				connection.Open();
				return null;
			});
		}
	}
}
