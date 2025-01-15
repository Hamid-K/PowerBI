using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200015E RID: 350
	public interface ITraceSource
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000906 RID: 2310
		TraceSourceIdentifier ID { get; }

		// Token: 0x06000907 RID: 2311
		void Trace(TraceVerbosity verbosity, string message);

		// Token: 0x06000908 RID: 2312
		void TraceWithHeader(TraceVerbosity verbosity, string header, string message);

		// Token: 0x06000909 RID: 2313
		[StringFormatMethod("format")]
		void Trace(TraceVerbosity verbosity, [NotNull] string format, params object[] args);

		// Token: 0x0600090A RID: 2314
		[StringFormatMethod("format")]
		void Trace(TraceEventType verbosity, [NotNull] string format, params object[] args);

		// Token: 0x0600090B RID: 2315
		void TraceFatal(string message);

		// Token: 0x0600090C RID: 2316
		[StringFormatMethod("format")]
		void TraceFatal([NotNull] string format, params object[] args);

		// Token: 0x0600090D RID: 2317
		void TraceError(string message);

		// Token: 0x0600090E RID: 2318
		[StringFormatMethod("format")]
		void TraceError([NotNull] string format, params object[] args);

		// Token: 0x0600090F RID: 2319
		void TraceWarning(string message);

		// Token: 0x06000910 RID: 2320
		[StringFormatMethod("format")]
		void TraceWarning([NotNull] string format, params object[] args);

		// Token: 0x06000911 RID: 2321
		void TraceInformation(string message);

		// Token: 0x06000912 RID: 2322
		[StringFormatMethod("format")]
		void TraceInformation([NotNull] string format, params object[] args);

		// Token: 0x06000913 RID: 2323
		void TraceVerbose(string message);

		// Token: 0x06000914 RID: 2324
		[StringFormatMethod("format")]
		void TraceVerbose([NotNull] string format, params object[] args);

		// Token: 0x06000915 RID: 2325
		bool ShouldTrace(TraceVerbosity level);

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000916 RID: 2326
		char Delimiter { get; }
	}
}
