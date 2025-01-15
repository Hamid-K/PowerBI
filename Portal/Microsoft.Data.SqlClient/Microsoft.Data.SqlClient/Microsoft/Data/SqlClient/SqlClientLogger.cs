using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000060 RID: 96
	public class SqlClientLogger
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00016C14 File Offset: 0x00014E14
		public void LogInfo(string type, string method, string message)
		{
			SqlClientEventSource.Log.TryTraceEvent<string, string, SqlClientLogger.LogLevel, string>("<sc|{0}|{1}|{2}>{3}", type, method, SqlClientLogger.LogLevel.Info, message);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00016C29 File Offset: 0x00014E29
		public void LogWarning(string type, string method, string message)
		{
			Console.Out.WriteLine(message);
			SqlClientEventSource.Log.TryTraceEvent<string, string, SqlClientLogger.LogLevel, string>("<sc|{0}|{1}|{2}>{3}", type, method, SqlClientLogger.LogLevel.Warning, message);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00016C49 File Offset: 0x00014E49
		public void LogError(string type, string method, string message)
		{
			SqlClientEventSource.Log.TryTraceEvent<string, string, SqlClientLogger.LogLevel, string>("<sc|{0}|{1}|{2}>{3}", type, method, SqlClientLogger.LogLevel.Error, message);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00016C5E File Offset: 0x00014E5E
		public bool LogAssert(bool value, string type, string method, string message)
		{
			if (!value)
			{
				this.LogError(type, method, message);
			}
			return value;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00016C6E File Offset: 0x00014E6E
		public bool IsLoggingEnabled
		{
			get
			{
				return SqlClientEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x020001BC RID: 444
		internal enum LogLevel
		{
			// Token: 0x04001326 RID: 4902
			Info,
			// Token: 0x04001327 RID: 4903
			Warning,
			// Token: 0x04001328 RID: 4904
			Error
		}
	}
}
