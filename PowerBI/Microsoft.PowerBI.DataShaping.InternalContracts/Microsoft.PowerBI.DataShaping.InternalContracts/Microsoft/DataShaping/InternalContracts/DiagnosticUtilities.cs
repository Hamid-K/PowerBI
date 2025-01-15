using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DsqGeneration;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200000F RID: 15
	internal static class DiagnosticUtilities
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024B6 File Offset: 0x000006B6
		internal static void TraceSanitizedError(this ITracer tracer, Exception ex, string message)
		{
			tracer.TraceSanitizedException(TraceLevel.Error, ex, message);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024C4 File Offset: 0x000006C4
		internal static void TraceSanitizedException(this ITracer tracer, TraceLevel traceLevel, Exception ex, string message)
		{
			string text = ex.FormatExceptionDetails(false);
			string text2 = StringUtil.FormatInvariant("{0}.{1}", new object[] { message, text });
			tracer.SanitizedTrace(traceLevel, text2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024FC File Offset: 0x000006FC
		internal static string FormatExceptionDetails(this Exception ex, bool omitStackTrace = false)
		{
			DataExtensionException ex2 = ex as DataExtensionException;
			if (ex2 != null)
			{
				return ex2.FormatDataExtensionErrorDetails(false);
			}
			DataTransformException ex3 = ex as DataTransformException;
			if (ex3 != null)
			{
				return DiagnosticUtilities.FormatDataTransformErrorDetails(ex3);
			}
			RawDataException ex4 = ex as RawDataException;
			if (ex4 != null)
			{
				return DiagnosticUtilities.FormatRawDataErrorDetails(ex4);
			}
			DataShapeEngineException ex5 = ex as DataShapeEngineException;
			if (ex5 != null)
			{
				return DiagnosticUtilities.FormatEngineErrorDetails(ex5);
			}
			return DiagnosticUtilities.FormatGeneralErrorDetails(ex, omitStackTrace);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002558 File Offset: 0x00000758
		private static string FormatEngineErrorDetails(DataShapeEngineException ex)
		{
			string text = null;
			DataExtensionEngineException ex2 = ex as DataExtensionEngineException;
			if (ex2 != null)
			{
				text = ex2.ExtensionException.ToErrorDetailsString();
			}
			else if (ex.InnerException != null)
			{
				text = ex.InnerException.FormatExceptionDetails(true);
			}
			string text2 = null;
			IErrorContextContainer errorContextContainer = ex as IErrorContextContainer;
			if (errorContextContainer != null && errorContextContainer.HasMessages())
			{
				text2 = errorContextContainer.SummarizeMessages();
			}
			if (text2 != null && text != null)
			{
				return StringUtil.FormatInvariant("Details: ErrorCode={0}, ErrorSource={1}, Message={2}, Language={3}, Messages={4}, Inner: {5}", new object[] { ex.ErrorCode, ex.ErrorSource, ex.Message, ex.Language, text2, text });
			}
			if (text2 != null)
			{
				return StringUtil.FormatInvariant("Details: ErrorCode={0}, ErrorSource={1}, Message={2}, Language={3}, Messages={4}", new object[] { ex.ErrorCode, ex.ErrorSource, ex.Message, ex.Language, text2 });
			}
			if (text != null)
			{
				return StringUtil.FormatInvariant("Details: ErrorCode={0}, ErrorSource={1}, Message={2}, Language={3}, Inner: {4}", new object[] { ex.ErrorCode, ex.ErrorSource, ex.Message, ex.Language, text });
			}
			return StringUtil.FormatInvariant("Details: ErrorCode={0}, ErrorSource={1}, Message={2}, Language={3}", new object[] { ex.ErrorCode, ex.ErrorSource, ex.Message, ex.Language });
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026B4 File Offset: 0x000008B4
		private static string FormatDataTransformErrorDetails(DataTransformException ex)
		{
			return StringUtil.FormatInvariant("Details: ErrorCode={0}, ErrorSource={1}, Message={2}, Language={3}, ProviderMessage={4}", new object[] { ex.ErrorCode, ex.ErrorSource, ex.Message, ex.Language, ex.ProviderMessage });
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002704 File Offset: 0x00000904
		private static string FormatRawDataErrorDetails(RawDataException ex)
		{
			return StringUtil.FormatInvariant("Details: ErrorCode={0}, ErrorSource={1}, Message={2}, Language={3}, ProviderMessage={4}", new object[] { ex.ErrorCode, ex.ErrorSource, ex.Message, ex.Language, ex.ProviderMessage });
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002754 File Offset: 0x00000954
		internal static string FormatGeneralErrorDetails(Exception ex, bool omitStackTrace)
		{
			if (ex == null)
			{
				return null;
			}
			if (ex.InnerException == null)
			{
				if (omitStackTrace)
				{
					return StringUtil.FormatInvariant("Details:Type={0}, Message={1}", new object[]
					{
						ex.GetType(),
						ex.Message
					});
				}
				return StringUtil.FormatInvariant("Details:Type={0}, Message={1}, StackTrace={2}", new object[]
				{
					ex.GetType(),
					ex.Message,
					ex.StackTrace
				});
			}
			else
			{
				List<Exception> innermostExceptions = ErrorUtils.GetInnermostExceptions(ex);
				string text = "Type=" + string.Join<Type>(",", innermostExceptions.Select((Exception e) => e.GetType()));
				if (omitStackTrace)
				{
					return StringUtil.FormatInvariant("Details:Type={0}, Message={1}, InnermostErrors=[{2}]", new object[]
					{
						ex.GetType(),
						ex.Message,
						text
					});
				}
				return StringUtil.FormatInvariant("Details:Type={0}, Message={1}, InnermostErrors=[{2}], StackTrace={3}", new object[]
				{
					ex.GetType(),
					ex.Message,
					text,
					ex.StackTrace
				});
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000285A File Offset: 0x00000A5A
		internal static void TraceSanitizedQueryError(this ITracer tracer, Exception ex)
		{
			tracer.TraceSanitizedError(ex, "An error occurred running the query");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002868 File Offset: 0x00000A68
		internal static void TraceSanitizedQuery(this ITracer tracer, int memoryLimit, int timeout, RequestPriorityKind requestPriority, RequestExecutionMetricsKind requestExecutionMetrics, string category, string query, ApplicationContext applicationContextObject, int? appContextLength)
		{
			string text = StringUtil.FormatInvariant("Running the query. Memory Limit={0}, Timeout={1}, RequestPriority={2}, RequestExecutionMetrics=[{3}], ApplicationContext={4}, ApplicationContextLength={5}, ConnectionCategory={6}, Query={7}", new object[]
			{
				memoryLimit,
				timeout,
				requestPriority,
				requestExecutionMetrics,
				ApplicationContextSerializer.SerializeForTelemetry(applicationContextObject),
				appContextLength,
				category,
				query.MarkAsCustomerContent()
			});
			tracer.SanitizedTrace(TraceLevel.Info, text);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028D8 File Offset: 0x00000AD8
		internal static void SanitizedTrace(this ITracer tracer, EngineMessageBase engineMessage)
		{
			TraceLevel traceLevel = engineMessage.Severity.ToTraceLevel();
			tracer.SanitizedTrace(traceLevel, "{0} - Source: '{1}'", engineMessage.TraceMessage, engineMessage.Source);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000290E File Offset: 0x00000B0E
		internal static TraceLevel ToTraceLevel(this EngineMessageSeverity severity)
		{
			if (severity == EngineMessageSeverity.Error)
			{
				return TraceLevel.Error;
			}
			if (severity != EngineMessageSeverity.Warning)
			{
				Contract.RetailFail("Unexpected EngineMessageSeverity: {0}", severity.ToString());
				throw new InvalidOperationException("Unexpected EngineMessageSeverity");
			}
			return TraceLevel.Warning;
		}
	}
}
