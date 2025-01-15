using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200114D RID: 4429
	internal class TracingDbCommand : DelegatingDbCommand
	{
		// Token: 0x06007402 RID: 29698 RVA: 0x0018ECEB File Offset: 0x0018CEEB
		public TracingDbCommand(Tracer tracer, DbCommand command, Action<IHostTrace> additionalCommandTraces = null)
			: base(command)
		{
			this.tracer = tracer;
			if (additionalCommandTraces != null)
			{
				this.additionalTracers = new Action<IHostTrace>[] { additionalCommandTraces };
				return;
			}
			this.additionalTracers = EmptyArray<Action<IHostTrace>>.Instance;
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x06007403 RID: 29699 RVA: 0x0018ED1A File Offset: 0x0018CF1A
		// (set) Token: 0x06007404 RID: 29700 RVA: 0x0018ED22 File Offset: 0x0018CF22
		protected override DbTransaction DbTransaction
		{
			get
			{
				return base.DbTransaction;
			}
			set
			{
				base.DbTransaction = TracingDbTransaction.RemoveTracing(value);
			}
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x0018ED30 File Offset: 0x0018CF30
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.tracer.TracePerformance<TracingDbDataReader>("Command/ExecuteDbDataReader", delegate(IHostTrace trace)
			{
				this.TraceCommand(trace);
				trace.Add("Behavior", behavior, false);
				TracingDbDataReader tracingDbDataReader = new TracingDbDataReader(this.tracer, this.<>n__0(behavior).WithTableSchema());
				this.TraceAdditionalValues(tracingDbDataReader.Trace);
				return tracingDbDataReader;
			});
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x0018ED6D File Offset: 0x0018CF6D
		public override int ExecuteNonQuery()
		{
			return this.tracer.TracePerformance<int>("Command/ExecuteNonQuery", delegate(IHostTrace trace)
			{
				this.TraceCommand(trace);
				int num = base.ExecuteNonQuery();
				trace.Add("Count", num, false);
				return num;
			});
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x0018ED8B File Offset: 0x0018CF8B
		public override object ExecuteScalar()
		{
			return this.tracer.TracePerformance<object>("Command/ExecuteScalar", delegate(IHostTrace trace)
			{
				this.TraceCommand(trace);
				return base.ExecuteScalar();
			});
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x0018EDA9 File Offset: 0x0018CFA9
		public override void Prepare()
		{
			this.tracer.TracePerformance("Command/Prepare", delegate(IHostTrace trace)
			{
				this.TraceCommand(trace);
				base.Prepare();
			});
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x0018EDC8 File Offset: 0x0018CFC8
		private void TraceCommand(IHostTrace trace)
		{
			trace.Add("CommandText", this.CommandText, true);
			try
			{
				trace.Add("CommandTimeout", this.CommandTimeout, false);
			}
			catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
			{
			}
			this.TraceAdditionalValues(trace);
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x0018EE30 File Offset: 0x0018D030
		private void TraceAdditionalValues(IHostTrace trace)
		{
			Action<IHostTrace>[] array = this.additionalTracers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i](trace);
			}
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x0018EE5C File Offset: 0x0018D05C
		public static bool TryAddTracer(DbCommand command, Action<IHostTrace> tracer)
		{
			while (command is DelegatingDbCommand && !(command is TracingDbCommand))
			{
				command = ((DelegatingDbCommand)command).InnerCommand;
			}
			if (command is TracingDbCommand)
			{
				TracingDbCommand tracingDbCommand = (TracingDbCommand)command;
				tracingDbCommand.additionalTracers = tracingDbCommand.additionalTracers.Add(tracer);
				return true;
			}
			return false;
		}

		// Token: 0x04003FDD RID: 16349
		private readonly Tracer tracer;

		// Token: 0x04003FDE RID: 16350
		private Action<IHostTrace>[] additionalTracers;
	}
}
