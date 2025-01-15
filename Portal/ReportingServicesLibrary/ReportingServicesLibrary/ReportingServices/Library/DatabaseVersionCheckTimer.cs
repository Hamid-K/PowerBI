using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000059 RID: 89
	internal sealed class DatabaseVersionCheckTimer : TimerActionBase
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00011104 File Offset: 0x0000F304
		public override void DoTimerAction()
		{
			Global.m_Tracer.Trace(TraceLevel.Verbose, "Starting database version check timer.");
			ConnectionManager connectionManager = null;
			try
			{
				connectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadUncommitted);
				connectionManager.WillDisconnectStorage();
				connectionManager.VerifyConnectionAndDbVersion();
			}
			catch (TimeoutException ex)
			{
				Global.m_Tracer.Trace(TraceLevel.Warning, "Timeout verifying the database version: {0}", new object[] { ex.Message });
			}
			catch (Exception ex2)
			{
				Global.m_Tracer.Trace(TraceLevel.Error, "Error verifying the database version: {0}", new object[] { ex2.ToString() });
			}
			finally
			{
				if (connectionManager != null)
				{
					connectionManager.DisconnectStorage();
				}
			}
			Global.m_Tracer.Trace(TraceLevel.Verbose, "Database version check timer executed.");
		}
	}
}
