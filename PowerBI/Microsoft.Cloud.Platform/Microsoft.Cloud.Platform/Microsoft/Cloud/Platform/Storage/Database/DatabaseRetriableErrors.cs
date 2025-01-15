using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000033 RID: 51
	public static class DatabaseRetriableErrors
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00004E78 File Offset: 0x00003078
		public static bool IsRetriable(int errNumber)
		{
			return DatabaseRetriableErrors.s_retriableErrors.Contains(errNumber);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004E88 File Offset: 0x00003088
		public static bool IsRetriable(SqlException ex)
		{
			if (ex == null)
			{
				return false;
			}
			bool flag = false;
			foreach (object obj in ex.Errors)
			{
				flag = DatabaseRetriableErrors.IsRetriable(((SqlError)obj).Number);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004EF4 File Offset: 0x000030F4
		public static IEnumerable<int> ListRetriableErrors()
		{
			return DatabaseRetriableErrors.s_retriableErrors;
		}

		// Token: 0x04000094 RID: 148
		private static HashSet<int> s_retriableErrors = new HashSet<int>(new List<int>
		{
			1204, 1205, 1222, 49918, 49919, 49920, 4060, 4221, 40613, 40501,
			40197, 10929, 10928, 10060, 10054, 10053, 233, 64, 20
		});
	}
}
