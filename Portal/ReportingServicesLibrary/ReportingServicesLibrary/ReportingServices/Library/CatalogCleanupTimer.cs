using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000057 RID: 87
	internal sealed class CatalogCleanupTimer : TimerActionBase
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00010D78 File Offset: 0x0000EF78
		public override void DoTimerAction()
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Starting catalog cleanup.");
			}
			this.CleanupCatalog();
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Catalog cleanup executed.");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		internal static int SecondsToNextEvent
		{
			get
			{
				DateTime dateTime = DateTime.Today.AddMinutes((double)Globals.Configuration.DailyCleanupMinuteOfDay);
				DateTime dateTime2;
				if (dateTime > DateTime.Now)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.AddHours(1.0) > DateTime.Now)
				{
					dateTime2 = DateTime.Now.AddSeconds(10.0);
				}
				else
				{
					dateTime2 = dateTime.AddDays(1.0);
				}
				return (int)dateTime2.Subtract(DateTime.Now).TotalSeconds;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010E4C File Offset: 0x0000F04C
		private void CleanupCatalog()
		{
			RSService rsservice = new RSService(false);
			try
			{
				rsservice.WillDisconnectStorage();
				rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				rsservice.Storage.CleanupCatalog();
			}
			finally
			{
				rsservice.DisconnectStorage();
			}
		}

		// Token: 0x040001AB RID: 427
		private static object syncObject = new object();
	}
}
