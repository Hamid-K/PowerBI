using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Utils;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x02000039 RID: 57
	public static class TracerAdapter
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00003C64 File Offset: 0x00001E64
		public static ITracer AsLegacyTracer(this ITracingProvider tracingProvider)
		{
			TracerAdapter.TracingProviderAdapter tracingProviderAdapter = tracingProvider as TracerAdapter.TracingProviderAdapter;
			if (tracingProviderAdapter != null)
			{
				return tracingProviderAdapter.LegacyTracer;
			}
			return new TracerAdapter.LegacyTracerAdapter(tracingProvider);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003C88 File Offset: 0x00001E88
		public static ITracingProvider AsTracingProvider(this ITracer legacyTracer)
		{
			TracerAdapter.LegacyTracerAdapter legacyTracerAdapter = legacyTracer as TracerAdapter.LegacyTracerAdapter;
			if (legacyTracerAdapter != null)
			{
				return legacyTracerAdapter.TracingProvider;
			}
			return new TracerAdapter.TracingProviderAdapter(legacyTracer);
		}

		// Token: 0x020001F7 RID: 503
		private sealed class LegacyTracerAdapter : ITracer
		{
			// Token: 0x06000ACE RID: 2766 RVA: 0x000140E4 File Offset: 0x000122E4
			internal LegacyTracerAdapter(ITracingProvider tracingProvider)
			{
				this.TracingProvider = tracingProvider;
			}

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000140F3 File Offset: 0x000122F3
			internal ITracingProvider TracingProvider { get; }

			// Token: 0x06000AD0 RID: 2768 RVA: 0x000140FC File Offset: 0x000122FC
			public bool ShouldTrace(TraceLevel level)
			{
				switch (level)
				{
				case TraceLevel.Error:
					return this.TracingProvider.Error != null;
				case TraceLevel.Warning:
					return this.TracingProvider.Warning != null;
				case TraceLevel.Info:
					return this.TracingProvider.Info != null;
				case TraceLevel.Verbose:
					return this.TracingProvider.Verbose != null;
				default:
					return true;
				}
			}

			// Token: 0x06000AD1 RID: 2769 RVA: 0x00014160 File Offset: 0x00012360
			public void TraceError(string message)
			{
				LevelTracer error = this.TracingProvider.Error;
				if (error == null)
				{
					return;
				}
				error.Trace(message);
			}

			// Token: 0x06000AD2 RID: 2770 RVA: 0x00014178 File Offset: 0x00012378
			public void TraceError(string format, object arg0)
			{
				LevelTracer error = this.TracingProvider.Error;
				if (error == null)
				{
					return;
				}
				error.Trace(FormattableStringFactory.Create(format, new object[] { arg0 }));
			}

			// Token: 0x06000AD3 RID: 2771 RVA: 0x0001419F File Offset: 0x0001239F
			public void TraceError(string format, object arg0, object arg1)
			{
				LevelTracer error = this.TracingProvider.Error;
				if (error == null)
				{
					return;
				}
				error.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1 }));
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x000141CA File Offset: 0x000123CA
			public void TraceError(string format, object arg0, object arg1, object arg2)
			{
				LevelTracer error = this.TracingProvider.Error;
				if (error == null)
				{
					return;
				}
				error.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2 }));
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x000141FA File Offset: 0x000123FA
			public void TraceError(string format, object arg0, object arg1, object arg2, object arg3)
			{
				LevelTracer error = this.TracingProvider.Error;
				if (error == null)
				{
					return;
				}
				error.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2, arg3 }));
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x0001422F File Offset: 0x0001242F
			public void TraceFatal(string message)
			{
				LevelTracer fatal = this.TracingProvider.Fatal;
				if (fatal == null)
				{
					return;
				}
				fatal.Trace(message);
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x00014247 File Offset: 0x00012447
			public void TraceFatal(string format, object arg0)
			{
				LevelTracer fatal = this.TracingProvider.Fatal;
				if (fatal == null)
				{
					return;
				}
				fatal.Trace(FormattableStringFactory.Create(format, new object[] { arg0 }));
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0001426E File Offset: 0x0001246E
			public void TraceFatal(string format, object arg0, object arg1)
			{
				LevelTracer fatal = this.TracingProvider.Fatal;
				if (fatal == null)
				{
					return;
				}
				fatal.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1 }));
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x00014299 File Offset: 0x00012499
			public void TraceFatal(string format, object arg0, object arg1, object arg2)
			{
				LevelTracer fatal = this.TracingProvider.Fatal;
				if (fatal == null)
				{
					return;
				}
				fatal.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2 }));
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x000142C9 File Offset: 0x000124C9
			public void TraceFatal(string format, object arg0, object arg1, object arg2, object arg3)
			{
				LevelTracer fatal = this.TracingProvider.Fatal;
				if (fatal == null)
				{
					return;
				}
				fatal.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2, arg3 }));
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x000142FE File Offset: 0x000124FE
			public void TraceInformation(string message)
			{
				LevelTracer info = this.TracingProvider.Info;
				if (info == null)
				{
					return;
				}
				info.Trace(message);
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x00014316 File Offset: 0x00012516
			public void TraceInformation(string format, object arg0)
			{
				LevelTracer info = this.TracingProvider.Info;
				if (info == null)
				{
					return;
				}
				info.Trace(FormattableStringFactory.Create(format, new object[] { arg0 }));
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x0001433D File Offset: 0x0001253D
			public void TraceInformation(string format, object arg0, object arg1)
			{
				LevelTracer info = this.TracingProvider.Info;
				if (info == null)
				{
					return;
				}
				info.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1 }));
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x00014368 File Offset: 0x00012568
			public void TraceInformation(string format, object arg0, object arg1, object arg2)
			{
				LevelTracer info = this.TracingProvider.Info;
				if (info == null)
				{
					return;
				}
				info.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2 }));
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x00014398 File Offset: 0x00012598
			public void TraceInformation(string format, object arg0, object arg1, object arg2, object arg3)
			{
				LevelTracer info = this.TracingProvider.Info;
				if (info == null)
				{
					return;
				}
				info.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2, arg3 }));
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x000143CD File Offset: 0x000125CD
			public void TraceVerbose(string message)
			{
				LevelTracer verbose = this.TracingProvider.Verbose;
				if (verbose == null)
				{
					return;
				}
				verbose.Trace(message);
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x000143E5 File Offset: 0x000125E5
			public void TraceVerbose(string format, object arg0)
			{
				LevelTracer verbose = this.TracingProvider.Verbose;
				if (verbose == null)
				{
					return;
				}
				verbose.Trace(FormattableStringFactory.Create(format, new object[] { arg0 }));
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x0001440C File Offset: 0x0001260C
			public void TraceVerbose(string format, object arg0, object arg1)
			{
				LevelTracer verbose = this.TracingProvider.Verbose;
				if (verbose == null)
				{
					return;
				}
				verbose.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1 }));
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00014437 File Offset: 0x00012637
			public void TraceVerbose(string format, object arg0, object arg1, object arg2)
			{
				LevelTracer verbose = this.TracingProvider.Verbose;
				if (verbose == null)
				{
					return;
				}
				verbose.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2 }));
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x00014467 File Offset: 0x00012667
			public void TraceVerbose(string format, object arg0, object arg1, object arg2, object arg3)
			{
				LevelTracer verbose = this.TracingProvider.Verbose;
				if (verbose == null)
				{
					return;
				}
				verbose.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2, arg3 }));
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x0001449C File Offset: 0x0001269C
			public void TraceWarning(string message)
			{
				LevelTracer warning = this.TracingProvider.Warning;
				if (warning == null)
				{
					return;
				}
				warning.Trace(message);
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x000144B4 File Offset: 0x000126B4
			public void TraceWarning(string format, object arg0)
			{
				LevelTracer warning = this.TracingProvider.Warning;
				if (warning == null)
				{
					return;
				}
				warning.Trace(FormattableStringFactory.Create(format, new object[] { arg0 }));
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x000144DB File Offset: 0x000126DB
			public void TraceWarning(string format, object arg0, object arg1)
			{
				LevelTracer warning = this.TracingProvider.Warning;
				if (warning == null)
				{
					return;
				}
				warning.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1 }));
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x00014506 File Offset: 0x00012706
			public void TraceWarning(string format, object arg0, object arg1, object arg2)
			{
				LevelTracer warning = this.TracingProvider.Warning;
				if (warning == null)
				{
					return;
				}
				warning.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2 }));
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x00014536 File Offset: 0x00012736
			public void TraceWarning(string format, object arg0, object arg1, object arg2, object arg3)
			{
				LevelTracer warning = this.TracingProvider.Warning;
				if (warning == null)
				{
					return;
				}
				warning.Trace(FormattableStringFactory.Create(format, new object[] { arg0, arg1, arg2, arg3 }));
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x0001456C File Offset: 0x0001276C
			public void SanitizedTrace(TraceLevel level, string format, params string[] args)
			{
				LevelTracer levelTracer;
				switch (level)
				{
				case TraceLevel.Error:
					levelTracer = this.TracingProvider.Error;
					break;
				case TraceLevel.Warning:
					levelTracer = this.TracingProvider.Warning;
					break;
				case TraceLevel.Info:
					levelTracer = this.TracingProvider.Info;
					break;
				case TraceLevel.Verbose:
					levelTracer = this.TracingProvider.Verbose;
					break;
				default:
					throw new ArgumentException(string.Format("Unexpected TraceLevel {0}", level));
				}
				if (levelTracer != null)
				{
					levelTracer.SanitizedTrace(FormattableStringFactory.Create(format, args));
				}
			}
		}

		// Token: 0x020001F8 RID: 504
		private sealed class TracingProviderAdapter : ITracingProvider
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x000145F3 File Offset: 0x000127F3
			internal TracingProviderAdapter(ITracer legacyTracer)
			{
				this.LegacyTracer = legacyTracer;
			}

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00014602 File Offset: 0x00012802
			internal ITracer LegacyTracer { get; }

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000AED RID: 2797 RVA: 0x0001460A File Offset: 0x0001280A
			public LevelTracer Fatal
			{
				get
				{
					if (this._fatal == null)
					{
						this._fatal = new TracerAdapter.FatalTracerAdapter(this.LegacyTracer);
					}
					return this._fatal;
				}
			}

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001462B File Offset: 0x0001282B
			public LevelTracer Error
			{
				get
				{
					return this.EnsureLevelTracer(ref this._error, TraceLevel.Error);
				}
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001463A File Offset: 0x0001283A
			public LevelTracer Warning
			{
				get
				{
					return this.EnsureLevelTracer(ref this._warning, TraceLevel.Warning);
				}
			}

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00014649 File Offset: 0x00012849
			public LevelTracer Info
			{
				get
				{
					return this.EnsureLevelTracer(ref this._info, TraceLevel.Info);
				}
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00014658 File Offset: 0x00012858
			public LevelTracer Verbose
			{
				get
				{
					return this.EnsureLevelTracer(ref this._verbose, TraceLevel.Verbose);
				}
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x00014667 File Offset: 0x00012867
			private LevelTracer EnsureLevelTracer(ref LevelTracer tracer, TraceLevel traceLevel)
			{
				if (!this.LegacyTracer.ShouldTrace(traceLevel))
				{
					return null;
				}
				if (tracer == null)
				{
					tracer = new TracerAdapter.LevelTracerAdapter(this.LegacyTracer, traceLevel);
				}
				return tracer;
			}

			// Token: 0x04000819 RID: 2073
			private LevelTracer _fatal;

			// Token: 0x0400081A RID: 2074
			private LevelTracer _error;

			// Token: 0x0400081B RID: 2075
			private LevelTracer _warning;

			// Token: 0x0400081C RID: 2076
			private LevelTracer _info;

			// Token: 0x0400081D RID: 2077
			private LevelTracer _verbose;
		}

		// Token: 0x020001F9 RID: 505
		private abstract class LevelTracerAdapterBase : LevelTracer
		{
			// Token: 0x06000AF3 RID: 2803 RVA: 0x0001468D File Offset: 0x0001288D
			internal LevelTracerAdapterBase(ITracer legacyTracer)
				: base(TracerAdapter.LegacyTraceFormatProvider.Instance)
			{
				this.LegacyTracer = legacyTracer;
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000146A1 File Offset: 0x000128A1
			protected ITracer LegacyTracer { get; }
		}

		// Token: 0x020001FA RID: 506
		private sealed class FatalTracerAdapter : TracerAdapter.LevelTracerAdapterBase
		{
			// Token: 0x06000AF5 RID: 2805 RVA: 0x000146A9 File Offset: 0x000128A9
			internal FatalTracerAdapter(ITracer legacyTracer)
				: base(legacyTracer)
			{
			}

			// Token: 0x06000AF6 RID: 2806 RVA: 0x000146B2 File Offset: 0x000128B2
			protected internal override void TraceStringCore(string message)
			{
				base.LegacyTracer.TraceFatal(message);
			}

			// Token: 0x06000AF7 RID: 2807 RVA: 0x000146C0 File Offset: 0x000128C0
			protected internal override void SanitizedTraceStringCore(string message)
			{
				base.LegacyTracer.TraceFatal(message);
			}
		}

		// Token: 0x020001FB RID: 507
		private sealed class LevelTracerAdapter : TracerAdapter.LevelTracerAdapterBase
		{
			// Token: 0x06000AF8 RID: 2808 RVA: 0x000146CE File Offset: 0x000128CE
			internal LevelTracerAdapter(ITracer legacyTracer, TraceLevel traceLevel)
				: base(legacyTracer)
			{
				this._traceLevel = traceLevel;
			}

			// Token: 0x06000AF9 RID: 2809 RVA: 0x000146DE File Offset: 0x000128DE
			protected internal override void TraceStringCore(string message)
			{
				base.LegacyTracer.Trace(this._traceLevel, message);
			}

			// Token: 0x06000AFA RID: 2810 RVA: 0x000146F2 File Offset: 0x000128F2
			protected internal override void SanitizedTraceStringCore(string message)
			{
				base.LegacyTracer.SanitizedTrace(this._traceLevel, message, Array.Empty<string>());
			}

			// Token: 0x04000820 RID: 2080
			private readonly TraceLevel _traceLevel;
		}

		// Token: 0x020001FC RID: 508
		private sealed class LegacyTraceFormatProvider : DiagnosticFormatProvider
		{
			// Token: 0x06000AFB RID: 2811 RVA: 0x0001470B File Offset: 0x0001290B
			private LegacyTraceFormatProvider()
				: base(null)
			{
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00014714 File Offset: 0x00012914
			internal static TracerAdapter.LegacyTraceFormatProvider Instance { get; } = new TracerAdapter.LegacyTraceFormatProvider();

			// Token: 0x06000AFD RID: 2813 RVA: 0x0001471C File Offset: 0x0001291C
			public override string Format(string format, object arg, IFormatProvider formatProvider)
			{
				if (string.IsNullOrEmpty(format))
				{
					IContainsMarkedInformation containsMarkedInformation = arg as IContainsMarkedInformation;
					if (containsMarkedInformation != null)
					{
						return containsMarkedInformation.BuildMarkedString();
					}
					IContainsTelemetryMarkup containsTelemetryMarkup = arg as IContainsTelemetryMarkup;
					if (containsTelemetryMarkup != null)
					{
						return containsTelemetryMarkup.ToCustomerContentString();
					}
				}
				return base.Format(format, arg, formatProvider);
			}

			// Token: 0x06000AFE RID: 2814 RVA: 0x0001475C File Offset: 0x0001295C
			protected override string FormatTaggedContent(ContentClassificationKind contentClassification, [Nullable] string format, object arg, [Nullable] IFormatProvider formatProvider)
			{
				IContainsTelemetryMarkup containsTelemetryMarkup = arg as IContainsTelemetryMarkup;
				if (containsTelemetryMarkup != null)
				{
					switch (contentClassification)
					{
					case ContentClassificationKind.CustomerContent:
						return containsTelemetryMarkup.ToCustomerContentString();
					case ContentClassificationKind.Euii:
						return containsTelemetryMarkup.ToEUIIString();
					case ContentClassificationKind.IPAddress:
						return containsTelemetryMarkup.ToIPString();
					}
				}
				string text = this.Format(format, arg, formatProvider);
				switch (contentClassification)
				{
				case ContentClassificationKind.CustomerContent:
					return text.MarkAsCustomerContent();
				case ContentClassificationKind.Euii:
					return text.MarkAsEUII();
				case ContentClassificationKind.IPAddress:
					return text.MarkAsIPAddress();
				default:
					return text;
				}
			}
		}
	}
}
