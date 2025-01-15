using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200000D RID: 13
	public static class DbCommandExtensions
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002968 File Offset: 0x00000B68
		public static IDataReader ExecuteReader(this IDbCommand command, ITracer tracer, CancellationToken cancellationToken)
		{
			DbCommandExtensions.CommandWrapper commandWrapper = new DbCommandExtensions.CommandWrapper(command, tracer);
			IDataReader dataReader;
			using (cancellationToken.Register(new Action<object>(DbCommandExtensions.TriggerCancellation), commandWrapper))
			{
				dataReader = command.ExecuteReader();
			}
			return dataReader;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029BC File Offset: 0x00000BBC
		private static void TriggerCancellation(object commandWraperAsObject)
		{
			((DbCommandExtensions.CommandWrapper)commandWraperAsObject).Cancel();
		}

		// Token: 0x02000026 RID: 38
		private sealed class CommandWrapper
		{
			// Token: 0x06000093 RID: 147 RVA: 0x00002FB8 File Offset: 0x000011B8
			public CommandWrapper(IDbCommand command, ITracer tracer)
			{
				this._command = command;
				this._tracer = tracer;
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00002FCE File Offset: 0x000011CE
			public void Cancel()
			{
				if (Interlocked.Increment(ref this._cancelAttemptCounts) > 1)
				{
					this._tracer.Trace(TraceLevel.Info, "Not calling Cancel because it has already been called.");
					return;
				}
				Task.Run(async delegate
				{
					try
					{
						if (this._command.IsOpen)
						{
							await this._command.CancelAsync();
						}
					}
					catch (DataExtensionException ex)
					{
						this._tracer.Trace(TraceLevel.Error, Utilities.FormatInvariant("DataExtensionException encountered when calling IDbCommand.CancelAsync. This is treated as non-fatal. Exception details: {0}", new object[] { ex.ToErrorDetailsString() }));
					}
					catch (Exception ex2) when (!Utilities.IsStoppingException(ex2))
					{
						this._tracer.Trace(TraceLevel.Error, Utilities.FormatInvariant("Exception encountered when calling IDbCommand.CancelAsync. This is treated as non-fatal. Exception details: Type={0}, StackTrace={1}", new object[]
						{
							ex2.GetType(),
							ex2.StackTrace
						}));
					}
				});
			}

			// Token: 0x04000092 RID: 146
			private readonly IDbCommand _command;

			// Token: 0x04000093 RID: 147
			private readonly ITracer _tracer;

			// Token: 0x04000094 RID: 148
			private int _cancelAttemptCounts;
		}
	}
}
