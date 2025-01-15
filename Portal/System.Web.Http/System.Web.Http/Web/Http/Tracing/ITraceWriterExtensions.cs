using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Tracing
{
	// Token: 0x0200011A RID: 282
	public static class ITraceWriterExtensions
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x000127C6 File Offset: 0x000109C6
		public static void Debug(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Debug, messageFormat, messageArguments);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000127D4 File Offset: 0x000109D4
		public static void Debug(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Debug, exception);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000127E0 File Offset: 0x000109E0
		public static void Debug(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Debug, exception, messageFormat, messageArguments);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000127F0 File Offset: 0x000109F0
		public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Error, messageFormat, messageArguments);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000127FE File Offset: 0x000109FE
		public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Error, exception);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001280A File Offset: 0x00010A0A
		public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Error, exception, messageFormat, messageArguments);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001281A File Offset: 0x00010A1A
		public static void Fatal(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Fatal, messageFormat, messageArguments);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00012828 File Offset: 0x00010A28
		public static void Fatal(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Fatal, exception);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00012834 File Offset: 0x00010A34
		public static void Fatal(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Fatal, exception, messageFormat, messageArguments);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00012844 File Offset: 0x00010A44
		public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Info, messageFormat, messageArguments);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00012852 File Offset: 0x00010A52
		public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Info, exception);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001285E File Offset: 0x00010A5E
		public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Info, exception, messageFormat, messageArguments);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00012870 File Offset: 0x00010A70
		public static void Trace(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, Exception exception)
		{
			if (traceWriter == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (exception == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("exception");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Exception = exception;
			});
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000128C4 File Offset: 0x00010AC4
		public static void Trace(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, Exception exception, string messageFormat, params object[] messageArguments)
		{
			if (traceWriter == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (exception == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("exception");
			}
			if (messageFormat == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("messageFormat");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Exception = exception;
				traceRecord.Message = global::System.Web.Http.Error.Format(messageFormat, messageArguments);
			});
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00012938 File Offset: 0x00010B38
		public static void Trace(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string messageFormat, params object[] messageArguments)
		{
			if (traceWriter == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (messageFormat == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("messageFormat");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Message = global::System.Web.Http.Error.Format(messageFormat, messageArguments);
			});
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00012994 File Offset: 0x00010B94
		public static void TraceBeginEnd(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> beginTrace, Action execute, Action<TraceRecord> endTrace, Action<TraceRecord> errorTrace)
		{
			if (traceWriter == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (execute == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("execute");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Begin;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				if (beginTrace != null)
				{
					beginTrace(traceRecord);
				}
			});
			try
			{
				execute();
				traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					if (endTrace != null)
					{
						endTrace(traceRecord);
					}
				});
			}
			catch (Exception ex)
			{
				traceWriter.TraceError(ex, request, category, operatorName, operationName, errorTrace);
				throw;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00012A44 File Offset: 0x00010C44
		public static Task<TResult> TraceBeginEndAsync<TResult>(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> beginTrace, Func<Task<TResult>> execute, Action<TraceRecord, TResult> endTrace, Action<TraceRecord> errorTrace)
		{
			if (traceWriter == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (execute == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("execute");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Begin;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				if (beginTrace != null)
				{
					beginTrace(traceRecord);
				}
			});
			Task<TResult> task2;
			try
			{
				Task<TResult> task = execute();
				if (task == null)
				{
					task2 = task;
				}
				else
				{
					task2 = traceWriter.TraceBeginEndAsyncCore(request, category, level, operatorName, operationName, endTrace, errorTrace, task);
				}
			}
			catch (Exception ex)
			{
				traceWriter.TraceError(ex, request, category, operatorName, operationName, errorTrace);
				throw;
			}
			return task2;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00012AF8 File Offset: 0x00010CF8
		private static async Task<TResult> TraceBeginEndAsyncCore<TResult>(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord, TResult> endTrace, Action<TraceRecord> errorTrace, Task<TResult> task)
		{
			TResult result2;
			try
			{
				TResult tresult = await task;
				TResult result = tresult;
				traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					if (endTrace != null)
					{
						endTrace(traceRecord, result);
					}
				});
				result2 = result;
			}
			catch (OperationCanceledException)
			{
				traceWriter.Trace(request, category, TraceLevel.Warn, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					traceRecord.Message = SRResources.TraceCancelledMessage;
					if (errorTrace != null)
					{
						errorTrace(traceRecord);
					}
				});
				throw;
			}
			catch (Exception ex)
			{
				traceWriter.TraceError(ex, request, category, operatorName, operationName, errorTrace);
				throw;
			}
			return result2;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00012B84 File Offset: 0x00010D84
		public static Task TraceBeginEndAsync(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> beginTrace, Func<Task> execute, Action<TraceRecord> endTrace, Action<TraceRecord> errorTrace)
		{
			if (traceWriter == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (execute == null)
			{
				throw global::System.Web.Http.Error.ArgumentNull("execute");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Begin;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				if (beginTrace != null)
				{
					beginTrace(traceRecord);
				}
			});
			Task task2;
			try
			{
				Task task = execute();
				if (task == null)
				{
					task2 = task;
				}
				else
				{
					task2 = traceWriter.TraceBeginEndAsyncCore(request, category, level, operatorName, operationName, endTrace, errorTrace, task);
				}
			}
			catch (Exception ex)
			{
				traceWriter.TraceError(ex, request, category, operatorName, operationName, errorTrace);
				throw;
			}
			return task2;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00012C38 File Offset: 0x00010E38
		private static async Task TraceBeginEndAsyncCore(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> endTrace, Action<TraceRecord> errorTrace, Task task)
		{
			try
			{
				await task;
				traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					if (endTrace != null)
					{
						endTrace(traceRecord);
					}
				});
			}
			catch (OperationCanceledException)
			{
				traceWriter.Trace(request, category, TraceLevel.Warn, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					traceRecord.Message = SRResources.TraceCancelledMessage;
					if (errorTrace != null)
					{
						errorTrace(traceRecord);
					}
				});
				throw;
			}
			catch (Exception ex)
			{
				traceWriter.TraceError(ex, request, category, operatorName, operationName, errorTrace);
				throw;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00012CC2 File Offset: 0x00010EC2
		public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Warn, messageFormat, messageArguments);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Warn, exception);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00012CDC File Offset: 0x00010EDC
		public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Warn, exception, messageFormat, messageArguments);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00012CEC File Offset: 0x00010EEC
		private static void TraceError(this ITraceWriter traceWriter, Exception exception, HttpRequestMessage request, string category, string operatorName, string operationName, Action<TraceRecord> errorTrace)
		{
			TraceLevel traceLevel = TraceWriterExceptionMapper.GetMappedTraceLevel(exception) ?? TraceLevel.Error;
			traceWriter.Trace(request, category, traceLevel, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.End;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				traceRecord.Exception = exception;
				TraceWriterExceptionMapper.TranslateHttpResponseException(traceRecord);
				if (errorTrace != null)
				{
					errorTrace(traceRecord);
				}
			});
		}
	}
}
