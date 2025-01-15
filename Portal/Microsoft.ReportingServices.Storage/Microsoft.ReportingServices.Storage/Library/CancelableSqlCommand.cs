using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000037 RID: 55
	internal class CancelableSqlCommand : InstrumentedSqlCommand
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000090F5 File Offset: 0x000072F5
		protected CancelableSqlCommand(SqlCommand cmd, IDisposable connectionLockContext)
			: base(cmd, connectionLockContext)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000090FF File Offset: 0x000072FF
		internal static CancelableSqlCommand GetCancelableSqlCommand(SqlCommand command, IDisposable connectionLockContext)
		{
			return new CancelableSqlCommand(command, connectionLockContext);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009108 File Offset: 0x00007308
		public override int ExecuteNonQuery()
		{
			ThreadJobContext threadContext = ProcessingContext.ThreadContext;
			int num;
			try
			{
				if (threadContext != null)
				{
					threadContext.AddCommand(this);
				}
				num = base.ExecuteNonQuery();
			}
			finally
			{
				if (threadContext != null)
				{
					threadContext.RemoveCommand(this);
				}
			}
			return num;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000914C File Offset: 0x0000734C
		public override IDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009158 File Offset: 0x00007358
		public override IDataReader ExecuteReader(CommandBehavior behavior)
		{
			ThreadJobContext threadContext = ProcessingContext.ThreadContext;
			IDataReader dataReader;
			try
			{
				if (threadContext != null)
				{
					threadContext.AddCommand(this);
				}
				dataReader = base.ExecuteReader();
			}
			finally
			{
				if (threadContext != null)
				{
					threadContext.RemoveCommand(this);
				}
			}
			return dataReader;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000919C File Offset: 0x0000739C
		public override object ExecuteScalar()
		{
			ThreadJobContext threadContext = ProcessingContext.ThreadContext;
			object obj;
			try
			{
				if (threadContext != null)
				{
					threadContext.AddCommand(this);
				}
				obj = base.ExecuteScalar();
			}
			finally
			{
				if (threadContext != null)
				{
					threadContext.RemoveCommand(this);
				}
			}
			return obj;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000091E0 File Offset: 0x000073E0
		public override void Cancel()
		{
			if (base.Command.Connection != null && base.Command.Connection.State != ConnectionState.Open)
			{
				base.Cancel();
				return;
			}
			if (RSTrace.CatalogTrace.TraceInfo)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "CancelableSqlCommand.Cancel not called, connection is not valid");
			}
		}

		// Token: 0x02000055 RID: 85
		private sealed class CancelableSqlCommandDebug : CancelableSqlCommand
		{
			// Token: 0x0600028F RID: 655 RVA: 0x0000B02C File Offset: 0x0000922C
			internal CancelableSqlCommandDebug(SqlCommand cmd, IDisposable connectionLockContext)
				: base(cmd, connectionLockContext)
			{
			}

			// Token: 0x06000290 RID: 656 RVA: 0x0000B038 File Offset: 0x00009238
			~CancelableSqlCommandDebug()
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Detected finalization of CancelableSqlCommandDebug. This is an error condition and should never happen");
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000B070 File Offset: 0x00009270
			public override void Dispose()
			{
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}
