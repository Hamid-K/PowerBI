using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200114F RID: 4431
	internal class TracingDbConnection : DelegatingDbConnection
	{
		// Token: 0x06007412 RID: 29714 RVA: 0x0018EF6C File Offset: 0x0018D16C
		public TracingDbConnection(Tracer tracer, DbConnection connection, Action<IHostTrace> additionalCommandTraces = null, bool requireEncryption = false)
			: base(connection)
		{
			this.tracer = tracer;
			this.additionalCommandTraces = additionalCommandTraces;
			this.requireEncryption = requireEncryption;
			this.generatedClientConnectionId = Guid.NewGuid().ToString();
			this.connectionId = "<unopened>";
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06007413 RID: 29715 RVA: 0x0018EFBA File Offset: 0x0018D1BA
		public string ConnectionId
		{
			get
			{
				return this.connectionId;
			}
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x0018EFC2 File Offset: 0x0018D1C2
		public override void Open()
		{
			this.tracer.TracePerformance("Connection/Open", delegate(IHostTrace trace)
			{
				trace.Add("RequireEncryption", this.requireEncryption, false);
				trace.Add("ConnectionTimeout", this.ConnectionTimeout, false);
				base.Open();
				this.connectionId = this.GetClientConnectionId(base.InnerConnection);
				trace.Add("ConnectionId", this.ConnectionId, false);
			});
		}

		// Token: 0x06007415 RID: 29717 RVA: 0x0018EFE0 File Offset: 0x0018D1E0
		public override DataTable GetSchema()
		{
			return this.tracer.Trace<DataTable>("Connection/GetSchema", (IHostTrace trace) => TracingDbConnection.AddRowCount(trace, base.GetSchema()));
		}

		// Token: 0x06007416 RID: 29718 RVA: 0x0018F000 File Offset: 0x0018D200
		public override DataTable GetSchema(string collectionName)
		{
			return this.tracer.Trace<DataTable>("Connection/GetSchema", delegate(IHostTrace trace)
			{
				trace.Add("CollectionName", collectionName, false);
				return TracingDbConnection.AddRowCount(trace, this.<>n__0(collectionName));
			});
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x0018F040 File Offset: 0x0018D240
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.tracer.Trace<DataTable>("Connection/GetSchema", delegate(IHostTrace trace)
			{
				trace.Add("CollectionName", collectionName, false);
				for (int i = 0; i < restrictionValues.Length; i++)
				{
					trace.Add("Restriction" + i.ToString(CultureInfo.InvariantCulture), restrictionValues[i], true);
				}
				return TracingDbConnection.AddRowCount(trace, this.<>n__1(collectionName, restrictionValues));
			});
		}

		// Token: 0x06007418 RID: 29720 RVA: 0x0018F084 File Offset: 0x0018D284
		protected override DbCommand CreateDbCommand()
		{
			return new TracingDbCommand(this.tracer, base.CreateDbCommand(), new Action<IHostTrace>(this.AdditionalCommandTraces));
		}

		// Token: 0x06007419 RID: 29721 RVA: 0x0018F0A3 File Offset: 0x0018D2A3
		protected virtual string GetClientConnectionId(DbConnection connection)
		{
			return this.generatedClientConnectionId;
		}

		// Token: 0x0600741A RID: 29722 RVA: 0x0018F0AC File Offset: 0x0018D2AC
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return this.tracer.Trace<TracingDbTransaction>("Connection/BeginTransaction", delegate(IHostTrace trace)
			{
				trace.Add("IsolationLevel", (int)isolationLevel, false);
				return new TracingDbTransaction(this.tracer, this.<>n__2(isolationLevel));
			});
		}

		// Token: 0x0600741B RID: 29723 RVA: 0x0018F0E9 File Offset: 0x0018D2E9
		public override void Close()
		{
			this.tracer.TracePerformance("Connection/Close", delegate(IHostTrace trace)
			{
				trace.Add("ConnectionId", this.ConnectionId, false);
				base.Close();
			});
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x0018F108 File Offset: 0x0018D308
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.TracePerformance("Connection/Dispose", delegate(IHostTrace trace)
				{
					trace.Add("ConnectionId", this.ConnectionId, false);
					this.<>n__3(disposing);
				});
			}
		}

		// Token: 0x0600741D RID: 29725 RVA: 0x0018F14D File Offset: 0x0018D34D
		private static DataTable AddRowCount(IHostTrace trace, DataTable dataTable)
		{
			trace.Add("RowCount", dataTable.Rows.Count, false);
			return dataTable;
		}

		// Token: 0x0600741E RID: 29726 RVA: 0x0018F16C File Offset: 0x0018D36C
		private void AdditionalCommandTraces(IHostTrace trace)
		{
			trace.Add("ConnectionId", this.ConnectionId, false);
			if (this.additionalCommandTraces != null)
			{
				this.additionalCommandTraces(trace);
			}
		}

		// Token: 0x04003FE1 RID: 16353
		protected const string Unopened = "<unopened>";

		// Token: 0x04003FE2 RID: 16354
		private readonly Tracer tracer;

		// Token: 0x04003FE3 RID: 16355
		private readonly Action<IHostTrace> additionalCommandTraces;

		// Token: 0x04003FE4 RID: 16356
		private readonly bool requireEncryption;

		// Token: 0x04003FE5 RID: 16357
		private readonly string generatedClientConnectionId;

		// Token: 0x04003FE6 RID: 16358
		private string connectionId;
	}
}
