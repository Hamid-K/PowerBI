using System;
using System.Diagnostics;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200003D RID: 61
	internal sealed class SqlConfigurableRetryLogicManager
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x000027D1 File Offset: 0x000009D1
		private SqlConfigurableRetryLogicManager()
		{
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		internal static SqlRetryLogicBaseProvider ConnectionProvider
		{
			get
			{
				SqlRetryLogicBaseProvider sqlRetryLogicBaseProvider;
				try
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Requested the {1} value.", "SqlConfigurableRetryLogicManager", "ConnectionProvider");
					sqlRetryLogicBaseProvider = SqlConfigurableRetryLogicManager.s_loader.Value.ConnectionProvider;
				}
				catch (Exception ex)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, Exception>("<sc.{0}.{1}|INFO> An exception occurred in access to the instance of the class, and the default non-retriable provider will apply: {2}", "SqlConfigurableRetryLogicManager", "ConnectionProvider", ex);
					if (SqlClientEventSource.Log.IsAdvancedTraceOn())
					{
						StackTrace stackTrace = new StackTrace();
						SqlClientEventSource.Log.AdvancedTraceEvent<string, string, StackTrace>("<sc.{0}.{1}|ADV|INFO> An exception occurred in access to the instance of the class, and the default non-retriable provider will apply: {2}", "SqlConfigurableRetryLogicManager", "ConnectionProvider", stackTrace);
					}
					sqlRetryLogicBaseProvider = SqlConfigurableRetryFactory.CreateNoneRetryProvider();
				}
				return sqlRetryLogicBaseProvider;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0000FB58 File Offset: 0x0000DD58
		internal static SqlRetryLogicBaseProvider CommandProvider
		{
			get
			{
				SqlRetryLogicBaseProvider sqlRetryLogicBaseProvider;
				try
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Requested the {1} value.", "SqlConfigurableRetryLogicManager", "CommandProvider");
					sqlRetryLogicBaseProvider = SqlConfigurableRetryLogicManager.s_loader.Value.CommandProvider;
				}
				catch (Exception ex)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, Exception>("<sc.{0}.{1}|INFO> An exception occurred in access to the instance of the class, and the default non-retriable provider will apply: {2}", "SqlConfigurableRetryLogicManager", "CommandProvider", ex);
					if (SqlClientEventSource.Log.IsAdvancedTraceOn())
					{
						StackTrace stackTrace = new StackTrace();
						SqlClientEventSource.Log.AdvancedTraceEvent<string, string, StackTrace>("<sc.{0}.{1}|ADV|INFO> An exception occurred in access to the instance of the class, and the default non-retriable provider will apply: {2}", "SqlConfigurableRetryLogicManager", "CommandProvider", stackTrace);
					}
					sqlRetryLogicBaseProvider = SqlConfigurableRetryFactory.CreateNoneRetryProvider();
				}
				return sqlRetryLogicBaseProvider;
			}
		}

		// Token: 0x040000D4 RID: 212
		private const string TypeName = "SqlConfigurableRetryLogicManager";

		// Token: 0x040000D5 RID: 213
		private static readonly Lazy<SqlConfigurableRetryLogicLoader> s_loader = new Lazy<SqlConfigurableRetryLogicLoader>(delegate
		{
			ISqlConfigurableRetryConnectionSection sqlConfigurableRetryConnectionSection = AppConfigManager.FetchConfigurationSection<SqlConfigurableRetryConnectionSection>("SqlConfigurableRetryLogicConnection");
			ISqlConfigurableRetryCommandSection sqlConfigurableRetryCommandSection = AppConfigManager.FetchConfigurationSection<SqlConfigurableRetryCommandSection>("SqlConfigurableRetryLogicCommand");
			return new SqlConfigurableRetryLogicLoader(sqlConfigurableRetryConnectionSection, sqlConfigurableRetryCommandSection, "SqlConfigurableRetryLogicConnection", "SqlConfigurableRetryLogicCommand");
		});
	}
}
