using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019DC RID: 6620
	public static class FileSystemAccessHelper
	{
		// Token: 0x0600A79C RID: 42908 RVA: 0x0022AD64 File Offset: 0x00228F64
		public static bool TryIgnoringAccessExceptions<T>(string source, Func<T> func, IEvaluationConstants evaluationConstants, out T result)
		{
			Exception ex;
			return FileSystemAccessHelper.TryIgnoringAccessExceptions<T, FileSystemAccessHelper.DefaultFileSystemAccessHelperTraceSource>(func, new FileSystemAccessHelper.DefaultFileSystemAccessHelperTraceSource(source, evaluationConstants), out result, out ex);
		}

		// Token: 0x0600A79D RID: 42909 RVA: 0x0022AD84 File Offset: 0x00228F84
		public static bool TryIgnoringAccessExceptions(string source, Action action, IEvaluationConstants evaluationConstants)
		{
			int num;
			return FileSystemAccessHelper.TryIgnoringAccessExceptions<int>(source, delegate
			{
				action();
				return 0;
			}, evaluationConstants, out num);
		}

		// Token: 0x0600A79E RID: 42910 RVA: 0x0022ADB4 File Offset: 0x00228FB4
		public static T IgnoringAccessExceptions<T>(string source, Func<T> func, IEvaluationConstants evaluationConstants)
		{
			T t;
			FileSystemAccessHelper.TryIgnoringAccessExceptions<T>(source, func, evaluationConstants, out t);
			return t;
		}

		// Token: 0x0600A79F RID: 42911 RVA: 0x0022ADD0 File Offset: 0x00228FD0
		public static void IgnoringAccessExceptions(string source, Action action, IEvaluationConstants evaluationConstants)
		{
			FileSystemAccessHelper.IgnoringAccessExceptions<int>(source, delegate
			{
				action();
				return 0;
			}, evaluationConstants);
		}

		// Token: 0x0600A7A0 RID: 42912 RVA: 0x0022AE00 File Offset: 0x00229000
		public static void CreateDirectory(string path, IEvaluationConstants evaluationConstants)
		{
			DirectoryInfo directoryInfo;
			Exception ex;
			if (!FileSystemAccessHelper.TryIgnoringAccessExceptions<DirectoryInfo, FileSystemAccessHelper.DefaultFileSystemAccessHelperTraceSource>(() => Directory.CreateDirectory(path), new FileSystemAccessHelper.DefaultFileSystemAccessHelperTraceSource("CreateDirectory", evaluationConstants), out directoryInfo, out ex))
			{
				throw new HostingException(Strings.CannotCreateDirectory(path, ex.GetType().FullName, ex.Message), "DirectoryCouldNotBeCreated");
			}
		}

		// Token: 0x0600A7A1 RID: 42913 RVA: 0x0022AE64 File Offset: 0x00229064
		public static void IgnoringAccessExceptions(IHostTraceSource traceSource, Action action)
		{
			int num;
			Exception ex;
			FileSystemAccessHelper.TryIgnoringAccessExceptions<int, IHostTraceSource>(delegate
			{
				action();
				return 0;
			}, traceSource, out num, out ex);
		}

		// Token: 0x0600A7A2 RID: 42914 RVA: 0x0022AE93 File Offset: 0x00229093
		public static MaxCountTraceSource CreateTraceSource(string source, IEvaluationConstants evaluationConstants, int maxCount)
		{
			return new MaxCountTraceSource(new FileSystemAccessHelper.DefaultFileSystemAccessHelperTraceSource(source, evaluationConstants), maxCount);
		}

		// Token: 0x0600A7A3 RID: 42915 RVA: 0x0022AEA8 File Offset: 0x002290A8
		private static int GetHResult(Exception e)
		{
			object obj = null;
			try
			{
				obj = FileSystemAccessHelper.hResult.GetValue(e, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is TargetException || ex is TargetParameterCountException || ex is MethodAccessException || ex is TargetInvocationException)
				{
					return 0;
				}
				throw;
			}
			int? num = obj as int?;
			if (num == null)
			{
				return 0;
			}
			return num.Value;
		}

		// Token: 0x0600A7A4 RID: 42916 RVA: 0x0022AF2C File Offset: 0x0022912C
		private static bool IsErrorOfType(IOException e, int[] errorCodes)
		{
			int num = FileSystemAccessHelper.GetHResult(e) & 65535;
			return Array.IndexOf<int>(errorCodes, num) >= 0;
		}

		// Token: 0x0600A7A5 RID: 42917 RVA: 0x0022AF54 File Offset: 0x00229154
		private static bool IsFileInUseException(Exception e)
		{
			IOException ex = e as IOException;
			return ex != null && FileSystemAccessHelper.IsErrorOfType(ex, FileSystemAccessHelper.FileInUseErrorCodes);
		}

		// Token: 0x0600A7A6 RID: 42918 RVA: 0x0022AF78 File Offset: 0x00229178
		private static bool TryIgnoringAccessExceptions<T, S>(Func<T> func, S traceSource, out T result, out Exception accessException) where S : IHostTraceSource
		{
			try
			{
				result = func();
				accessException = null;
				return true;
			}
			catch (UnauthorizedAccessException ex)
			{
				accessException = ex;
			}
			catch (IOException ex2)
			{
				accessException = ex2;
			}
			using (IHostTrace hostTrace = traceSource.CreateTrace())
			{
				if (accessException is FileNotFoundException || accessException is DirectoryNotFoundException || FileSystemAccessHelper.IsFileInUseException(accessException))
				{
					hostTrace.Add("NonFatalError", accessException.Message, true);
				}
				else
				{
					hostTrace.Add(accessException, TraceEventType.Information, true);
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x0400574B RID: 22347
		private const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x0400574C RID: 22348
		private const int ERROR_LOCK_VIOLATION = 33;

		// Token: 0x0400574D RID: 22349
		private const int HRESULT_CODE_MASK = 65535;

		// Token: 0x0400574E RID: 22350
		private static readonly int[] FileInUseErrorCodes = new int[] { 32, 33 };

		// Token: 0x0400574F RID: 22351
		private static readonly PropertyInfo hResult = typeof(Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x020019DD RID: 6621
		private struct DefaultFileSystemAccessHelperTraceSource : IHostTraceSource
		{
			// Token: 0x0600A7A8 RID: 42920 RVA: 0x0022B05E File Offset: 0x0022925E
			public DefaultFileSystemAccessHelperTraceSource(string source, IEvaluationConstants evaluationConstants)
			{
				this.source = source;
				this.evaluationConstants = evaluationConstants;
			}

			// Token: 0x0600A7A9 RID: 42921 RVA: 0x0022B06E File Offset: 0x0022926E
			public IHostTrace CreateTrace()
			{
				IHostTrace hostTrace = EvaluatorTracing.CreateTrace("FileSystemAccessHelper/TryIgnoringAccessExceptions", this.evaluationConstants, TraceEventType.Information, null);
				hostTrace.Add("Source", this.source, false);
				return hostTrace;
			}

			// Token: 0x04005750 RID: 22352
			private readonly IEvaluationConstants evaluationConstants;

			// Token: 0x04005751 RID: 22353
			private readonly string source;
		}
	}
}
