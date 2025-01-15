using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200005B RID: 91
	internal sealed class UpdatePoliciesTimer : TimerActionBase
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00011230 File Offset: 0x0000F430
		public override void DoTimerAction()
		{
			Global.m_Tracer.Trace(TraceLevel.Verbose, "Starting policy update based on the update policies timer");
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			try
			{
				connectionManager.WillDisconnectStorage();
				new DBInterface().ConnectionManager = connectionManager;
				Security security = new Security(new UserContext(WebConfigUtil.WebServerAuthMode), false);
				security.ConnectionManager = connectionManager;
				try
				{
					while (security.UpdateSecurityPolicies(Globals.Configuration.UpdatePoliciesChunkSizeParam) != 0)
					{
					}
				}
				catch (Exception ex)
				{
					Global.m_Tracer.Trace(TraceLevel.Error, "Error on policy update.\n" + ex.ToString());
				}
			}
			finally
			{
				if (connectionManager != null)
				{
					connectionManager.DisconnectStorage();
				}
			}
			Global.m_Tracer.Trace(TraceLevel.Verbose, "Policy update based on the update policies timer executed");
		}
	}
}
