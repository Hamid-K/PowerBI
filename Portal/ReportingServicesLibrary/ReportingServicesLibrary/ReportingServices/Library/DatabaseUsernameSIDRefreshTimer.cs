using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200005A RID: 90
	internal sealed class DatabaseUsernameSIDRefreshTimer : TimerActionBase
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000111C4 File Offset: 0x0000F3C4
		public override void DoTimerAction()
		{
			Global.m_Tracer.Trace(TraceLevel.Verbose, "Starting catalog refresh of the username based on the SID timer");
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			try
			{
				connectionManager.WillDisconnectStorage();
				new DBInterface
				{
					ConnectionManager = connectionManager
				}.UpdateUsernameFromSID();
			}
			finally
			{
				if (connectionManager != null)
				{
					connectionManager.DisconnectStorage();
				}
			}
			Global.m_Tracer.Trace(TraceLevel.Verbose, "Catalog refresh of the username based on the SID timer executed");
		}
	}
}
