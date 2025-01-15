using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200003B RID: 59
	public sealed class SqlConfigurableRetryFactory
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0000EF26 File Offset: 0x0000D126
		public static SqlRetryLogicBaseProvider CreateExponentialRetryProvider(SqlRetryLogicOption retryLogicOption)
		{
			return SqlConfigurableRetryFactory.InternalCreateRetryProvider(retryLogicOption, (retryLogicOption != null) ? new SqlExponentialIntervalEnumerator(retryLogicOption.DeltaTime, retryLogicOption.MaxTimeInterval, retryLogicOption.MinTimeInterval) : null);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000EF4B File Offset: 0x0000D14B
		public static SqlRetryLogicBaseProvider CreateIncrementalRetryProvider(SqlRetryLogicOption retryLogicOption)
		{
			return SqlConfigurableRetryFactory.InternalCreateRetryProvider(retryLogicOption, (retryLogicOption != null) ? new SqlIncrementalIntervalEnumerator(retryLogicOption.DeltaTime, retryLogicOption.MaxTimeInterval, retryLogicOption.MinTimeInterval) : null);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000EF70 File Offset: 0x0000D170
		public static SqlRetryLogicBaseProvider CreateFixedRetryProvider(SqlRetryLogicOption retryLogicOption)
		{
			return SqlConfigurableRetryFactory.InternalCreateRetryProvider(retryLogicOption, (retryLogicOption != null) ? new SqlFixedIntervalEnumerator(retryLogicOption.DeltaTime, retryLogicOption.MaxTimeInterval, retryLogicOption.MinTimeInterval) : null);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000EF98 File Offset: 0x0000D198
		private static SqlRetryLogicBaseProvider InternalCreateRetryProvider(SqlRetryLogicOption retryLogicOption, SqlRetryIntervalBaseEnumerator enumerator)
		{
			if (retryLogicOption == null)
			{
				throw new ArgumentNullException("retryLogicOption");
			}
			SqlRetryLogic sqlRetryLogic = new SqlRetryLogic(retryLogicOption.NumberOfTries, enumerator, (Exception e) => SqlConfigurableRetryFactory.TransientErrorsCondition(e, retryLogicOption.TransientErrors ?? SqlConfigurableRetryFactory.s_defaultTransientErrors), retryLogicOption.AuthorizedSqlCondition);
			return new SqlRetryLogicProvider(sqlRetryLogic);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		public static SqlRetryLogicBaseProvider CreateNoneRetryProvider()
		{
			SqlRetryLogic sqlRetryLogic = new SqlRetryLogic(new SqlNoneIntervalEnumerator(), (Exception _) => false);
			return new SqlRetryLogicProvider(sqlRetryLogic);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000F031 File Offset: 0x0000D231
		internal static bool IsRetriable(SqlRetryLogicBaseProvider provider)
		{
			return provider != null && (provider.RetryLogic == null || !(provider.RetryLogic.RetryIntervalEnumerator is SqlNoneIntervalEnumerator));
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000F058 File Offset: 0x0000D258
		private static bool TransientErrorsCondition(Exception e, IEnumerable<int> retriableConditions)
		{
			bool flag = false;
			if (retriableConditions != null)
			{
				SqlException ex = e as SqlException;
				if (ex != null && !ex._doNotReconnect)
				{
					foreach (object obj in ex.Errors)
					{
						SqlError sqlError = (SqlError)obj;
						object obj2 = SqlConfigurableRetryFactory.s_syncObject;
						bool flag3;
						lock (obj2)
						{
							flag3 = retriableConditions.Contains(sqlError.Number);
						}
						if (flag3)
						{
							SqlClientEventSource.Log.TryTraceEvent<string, string, int, string>("<sc.{0}.{1}|ERR|CATCH> Found a transient error: number = <{2}>, message = <{3}>", "SqlConfigurableRetryFactory", MethodBase.GetCurrentMethod().Name, sqlError.Number, sqlError.Message);
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x040000CF RID: 207
		private static readonly object s_syncObject = new object();

		// Token: 0x040000D0 RID: 208
		private static readonly HashSet<int> s_defaultTransientErrors = new HashSet<int>
		{
			1204, 1205, 1222, 49918, 49919, 49920, 4060, 4221, 40143, 40613,
			40501, 40540, 40197, 42108, 42109, 10929, 10928, 10060, 997, 233
		};
	}
}
