using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000007 RID: 7
	internal abstract class PollWorker
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000327A File Offset: 0x0000147A
		public PollWorker()
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003294 File Offset: 0x00001494
		protected bool ContinueWorking
		{
			get
			{
				return this.m_continueWorking;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000329C File Offset: 0x0000149C
		public virtual string PollCatalog
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000038 RID: 56
		public abstract bool Poll { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000039 RID: 57
		public abstract InstrumentedSqlCommand PollCommand { get; }

		// Token: 0x0600003A RID: 58
		public abstract void ProcessData(IDataReader reader);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003B RID: 59
		protected abstract string PollingTraceName { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003C RID: 60
		public abstract bool IsStillWorking { get; }

		// Token: 0x0600003D RID: 61 RVA: 0x000032A4 File Offset: 0x000014A4
		public virtual void Start()
		{
			Thread.CurrentThread.CurrentUICulture = Localization.FallbackUICulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.m_continueWorking = true;
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace(TraceLevel.Info, "{0} polling service started", new object[] { this.PollingTraceName });
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003303 File Offset: 0x00001503
		public virtual void Stop(Globals.ServiceStopMode mode)
		{
			this.m_continueWorking = false;
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace(TraceLevel.Info, "{0} polling service stopped", new object[] { this.PollingTraceName });
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003339 File Offset: 0x00001539
		protected static InstrumentedSqlCommand NewStandardSqlCommand(string storedProcedureName, ConnectionManager connManager)
		{
			InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(storedProcedureName, connManager.Connection, connManager.Transaction));
			instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
			instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
			return instrumentedSqlCommand;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003364 File Offset: 0x00001564
		protected void ExecuteStoredProcedure(string spName)
		{
			this.ExecuteStoredProcedure(spName, null);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003370 File Offset: 0x00001570
		protected void ExecuteStoredProcedure(string spName, Dictionary<string, object> spParameterBag)
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			if (!string.IsNullOrEmpty(this.PollCatalog))
			{
				connectionManager.ChangeDatabase(this.PollCatalog);
			}
			try
			{
				spName = string.Format(CultureInfo.InvariantCulture, "{0}", spName);
				using (InstrumentedSqlCommand instrumentedSqlCommand = PollWorker.NewStandardSqlCommand(spName, connectionManager))
				{
					if (spParameterBag != null)
					{
						foreach (string text in spParameterBag.Keys)
						{
							object obj = spParameterBag[text];
							instrumentedSqlCommand.Parameters.AddWithValue(text, obj);
						}
					}
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x04000063 RID: 99
		private bool m_continueWorking = true;

		// Token: 0x04000064 RID: 100
		private RSTrace m_tracer = RSTrace.DbPollingTracer;
	}
}
