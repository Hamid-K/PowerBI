using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000DF RID: 223
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class TraceSourceBase<[Nullable(0)] T> where T : TraceSourceBase<T>, new()
	{
		// Token: 0x060010EF RID: 4335 RVA: 0x00046658 File Offset: 0x00044858
		protected TraceSourceBase()
		{
			this.m_traceMessageHeader = string.Format(CultureInfo.InvariantCulture, "[{0}] ", this.ID);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060010F0 RID: 4336
		public abstract TraceSourceIdentifier ID { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004667B File Offset: 0x0004487B
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00046682 File Offset: 0x00044882
		public static T Tracer { get; private set; } = new T();

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004668A File Offset: 0x0004488A
		public char Delimiter
		{
			get
			{
				return ';';
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004668E File Offset: 0x0004488E
		public void TraceFatal(string message)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Fatal))
			{
				this.TraceInternal(PipelineTraceVerbosity.Fatal, message);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000466A1 File Offset: 0x000448A1
		public void TraceFatal(string format, params object[] args)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Fatal))
			{
				this.TraceInternal(PipelineTraceVerbosity.Fatal, format, args);
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000466B5 File Offset: 0x000448B5
		public void TraceError(string message)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Error))
			{
				this.TraceInternal(PipelineTraceVerbosity.Error, message);
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000466C8 File Offset: 0x000448C8
		public void TraceError(string format, params object[] args)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Error))
			{
				this.TraceInternal(PipelineTraceVerbosity.Error, format, args);
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000466DC File Offset: 0x000448DC
		public void TraceWarning(string message)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Warning))
			{
				this.TraceInternal(PipelineTraceVerbosity.Warning, message);
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000466EF File Offset: 0x000448EF
		public void TraceWarning(string format, params object[] args)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Warning))
			{
				this.TraceInternal(PipelineTraceVerbosity.Warning, format, args);
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00046703 File Offset: 0x00044903
		public void TraceInformation(string message)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Info))
			{
				this.TraceInternal(PipelineTraceVerbosity.Info, message);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00046716 File Offset: 0x00044916
		public void TraceInformation(string format, params object[] args)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Info))
			{
				this.TraceInternal(PipelineTraceVerbosity.Info, format, args);
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0004672A File Offset: 0x0004492A
		public void TraceVerbose(string message)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Verbose))
			{
				this.TraceInternal(PipelineTraceVerbosity.Verbose, message);
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0004673D File Offset: 0x0004493D
		public void TraceVerbose(string format, params object[] args)
		{
			if (this.ShouldTrace(PipelineTraceVerbosity.Verbose))
			{
				this.TraceInternal(PipelineTraceVerbosity.Verbose, format, args);
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00046751 File Offset: 0x00044951
		public void Trace(PipelineTraceVerbosity verbosity, string message)
		{
			if (this.ShouldTrace(verbosity))
			{
				this.TraceInternal(verbosity, message);
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00046764 File Offset: 0x00044964
		public void Trace(PipelineTraceVerbosity verbosity, string format, params object[] args)
		{
			if (this.ShouldTrace(verbosity))
			{
				this.TraceInternal(verbosity, format, args);
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00046778 File Offset: 0x00044978
		public bool ShouldTrace(PipelineTraceVerbosity currentTraceLevel)
		{
			return currentTraceLevel <= TraceSourceBase<T>.s_tracingVerbosity;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00046785 File Offset: 0x00044985
		private void TraceInternal(PipelineTraceVerbosity verbosity, string message)
		{
			this.TraceInternal(verbosity, message, null);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00046790 File Offset: 0x00044990
		private void TraceInternal(PipelineTraceVerbosity verbosity, string format, params object[] args)
		{
			string text = this.FormatTraceMessage(format, args);
			DiagnosticsContext.TracingService.Trace(this.ID.ToString(), verbosity, text);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000467BD File Offset: 0x000449BD
		public string FormatTraceMessage(string format, params object[] args)
		{
			if (args == null || args.Length == 0)
			{
				return this.m_traceMessageHeader + format;
			}
			return this.m_traceMessageHeader + string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000467EA File Offset: 0x000449EA
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Trace source with ID: {0}. Verbosity: {1}.", this.ID, TraceSourceBase<T>.s_tracingVerbosity);
		}

		// Token: 0x04000368 RID: 872
		private static readonly PipelineTraceVerbosity s_tracingVerbosity = (PipelineTraceVerbosity)DiagnosticsSettings.Default.TracingVerbosity;

		// Token: 0x04000369 RID: 873
		private readonly string m_traceMessageHeader;
	}
}
