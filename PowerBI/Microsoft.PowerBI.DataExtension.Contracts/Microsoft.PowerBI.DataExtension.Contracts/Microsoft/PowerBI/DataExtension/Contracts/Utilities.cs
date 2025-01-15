using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;

namespace Microsoft.PowerBI.DataExtension.Contracts
{
	// Token: 0x0200000A RID: 10
	public static class Utilities
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000272C File Offset: 0x0000092C
		public static void TraceDataExtensionError(this ITracer tracer, TraceLevel traceLevel, DataExtensionException ex, string message)
		{
			string text = Utilities.FormatInvariant("{0}.{1}", new object[]
			{
				message,
				ex.FormatDataExtensionErrorDetails(false)
			});
			tracer.Trace(traceLevel, text);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002760 File Offset: 0x00000960
		public static void SanitizedTraceDataExtensionError(this ITracer tracer, TraceLevel traceLevel, DataExtensionException ex, string message)
		{
			string text = Utilities.FormatInvariant("{0}.{1}", new object[]
			{
				message,
				ex.FormatDataExtensionErrorDetails(false)
			});
			tracer.SanitizedTrace(traceLevel, text);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002794 File Offset: 0x00000994
		public static string FormatDataExtensionErrorDetails(this DataExtensionException ex, bool includeStackTrace = false)
		{
			string text = Utilities.FormatInvariant("Details: ErrorCode={0}, ProviderErrorCode=0x{1:X8}, ErrorSource={2}, Message={3}, HResult=0x{4:X8}, Language={5}, ProviderErrorMessage=[{6}], ProviderGenericMessage=[{7}], ErrorSourceOrigin={8}, OnPremErrorCode={9}, InnerErrorDetails=[{10}]", new object[]
			{
				ex.ErrorCode,
				ex.ProviderErrorCode,
				ex.ErrorSource,
				ex.Message,
				ex.HResult,
				ex.Language,
				ex.ProviderMessage,
				ex.ProviderGenericMessage,
				ex.ErrorSourceOrigin,
				ex.OnPremErrorCode,
				Utilities.FormatInnerErrorDetails(ex)
			});
			if (includeStackTrace)
			{
				text = text + Environment.NewLine + ex.StackTrace;
			}
			return text;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000283D File Offset: 0x00000A3D
		internal static string FormatInnerErrorDetails(Exception ex)
		{
			if (ex.InnerException == null)
			{
				return string.Empty;
			}
			return Utilities.FormatInvariant("Type={0}, Message={1}", new object[]
			{
				ex.InnerException.GetType(),
				ex.InnerException.Message
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002879 File Offset: 0x00000A79
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002888 File Offset: 0x00000A88
		public static Task RunSynchronously(Action action)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			try
			{
				action();
				taskCompletionSource.SetResult(null);
			}
			catch (Exception ex)
			{
				taskCompletionSource.SetException(ex);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028CC File Offset: 0x00000ACC
		public static Task<TResult> RunSynchronously<TResult>(Func<TResult> func)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			try
			{
				taskCompletionSource.SetResult(func());
			}
			catch (Exception ex)
			{
				taskCompletionSource.SetException(ex);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002910 File Offset: 0x00000B10
		public static bool IsStoppingException(Exception ex)
		{
			while (ex != null)
			{
				if ((ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException || ex is StackOverflowException)
				{
					return true;
				}
				if (!(ex is AggregateException))
				{
					ex = ex.InnerException;
				}
			}
			return false;
		}
	}
}
