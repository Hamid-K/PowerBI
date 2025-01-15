using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001034 RID: 4148
	internal sealed class CancelingDbConnection : DelegatingDbConnection
	{
		// Token: 0x06006C45 RID: 27717 RVA: 0x00175204 File Offset: 0x00173404
		private CancelingDbConnection(Tracer tracer, ICancellationService cancellationService, DbConnection connection)
			: base(connection)
		{
			this.tracer = tracer;
			this.cancellationService = cancellationService;
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x0017521C File Offset: 0x0017341C
		public static DbConnection New(DbEnvironment environment, DbConnection connection)
		{
			ICancellationService cancellationService = environment.Host.QueryService<ICancellationService>();
			if (cancellationService == null)
			{
				return connection;
			}
			return new CancelingDbConnection(environment.Tracer, cancellationService, connection);
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x00175247 File Offset: 0x00173447
		protected override DbCommand CreateDbCommand()
		{
			return new CancelingDbConnection.CancelingDbCommand(this.tracer, this.cancellationService, base.CreateDbCommand());
		}

		// Token: 0x04003C40 RID: 15424
		private readonly Tracer tracer;

		// Token: 0x04003C41 RID: 15425
		private readonly ICancellationService cancellationService;

		// Token: 0x02001035 RID: 4149
		private sealed class CancelingDbCommand : DelegatingDbCommand
		{
			// Token: 0x06006C48 RID: 27720 RVA: 0x00175260 File Offset: 0x00173460
			public CancelingDbCommand(Tracer tracer, ICancellationService cancellationService, DbCommand command)
				: base(command)
			{
				this.tracer = tracer;
				this.cancellationService = cancellationService;
			}

			// Token: 0x06006C49 RID: 27721 RVA: 0x00175278 File Offset: 0x00173478
			protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
			{
				CancelingDbConnection.CancellationHandle cancellationHandle = new CancelingDbConnection.CancellationHandle(this.tracer, this.cancellationService, this);
				DbDataReader dbDataReader;
				try
				{
					dbDataReader = base.ExecuteDbDataReader(behavior).WithTableSchema().OnDispose(new Action(cancellationHandle.Dispose));
				}
				catch (Exception)
				{
					cancellationHandle.Dispose();
					throw;
				}
				return dbDataReader;
			}

			// Token: 0x06006C4A RID: 27722 RVA: 0x001752D4 File Offset: 0x001734D4
			public override int ExecuteNonQuery()
			{
				int num;
				using (new CancelingDbConnection.CancellationHandle(this.tracer, this.cancellationService, this))
				{
					num = base.ExecuteNonQuery();
				}
				return num;
			}

			// Token: 0x06006C4B RID: 27723 RVA: 0x00175318 File Offset: 0x00173518
			public override object ExecuteScalar()
			{
				object obj;
				using (new CancelingDbConnection.CancellationHandle(this.tracer, this.cancellationService, this))
				{
					obj = base.ExecuteScalar();
				}
				return obj;
			}

			// Token: 0x04003C42 RID: 15426
			private readonly Tracer tracer;

			// Token: 0x04003C43 RID: 15427
			private readonly ICancellationService cancellationService;
		}

		// Token: 0x02001036 RID: 4150
		private class CancellationHandle : ICancellable, IDisposable
		{
			// Token: 0x06006C4C RID: 27724 RVA: 0x0017535C File Offset: 0x0017355C
			public CancellationHandle(Tracer tracer, ICancellationService cancellationService, DbCommand command)
			{
				this.syncRoot = new object();
				this.tracer = tracer;
				this.cancellationService = cancellationService;
				this.command = command;
				this.cancellationService.Register(this);
			}

			// Token: 0x06006C4D RID: 27725 RVA: 0x00175390 File Offset: 0x00173590
			public bool Cancel()
			{
				using (IHostTrace hostTrace = this.tracer.CreateTrace("CancelingDbConnection/CancellationHandle/Cancel", TraceEventType.Information))
				{
					object obj = this.syncRoot;
					DbCommand dbCommand;
					lock (obj)
					{
						dbCommand = this.command;
						this.command = null;
					}
					hostTrace.Add("Attempt", dbCommand != null, false);
					if (dbCommand != null)
					{
						try
						{
							dbCommand.Cancel();
							hostTrace.Add("Success", true, false);
							return true;
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							hostTrace.Add(ex, true);
							hostTrace.Add("Success", false, false);
						}
					}
				}
				return false;
			}

			// Token: 0x06006C4E RID: 27726 RVA: 0x00175480 File Offset: 0x00173680
			public void Dispose()
			{
				object obj = this.syncRoot;
				DbCommand dbCommand;
				lock (obj)
				{
					dbCommand = this.command;
					this.command = null;
				}
				if (dbCommand != null)
				{
					this.cancellationService.Unregister(this);
				}
			}

			// Token: 0x04003C44 RID: 15428
			private readonly object syncRoot;

			// Token: 0x04003C45 RID: 15429
			private readonly Tracer tracer;

			// Token: 0x04003C46 RID: 15430
			private readonly ICancellationService cancellationService;

			// Token: 0x04003C47 RID: 15431
			private DbCommand command;
		}
	}
}
