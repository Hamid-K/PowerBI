using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Tracing;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000018 RID: 24
	internal static class ProviderTracing
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00005BB3 File Offset: 0x00003DB3
		static ProviderTracing()
		{
			EvaluatorTracing.Service = ProviderTracing.tracingService;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005BC9 File Offset: 0x00003DC9
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public static ITracingService Service
		{
			get
			{
				return ProviderTracing.tracingService;
			}
			set
			{
				ProviderTracing.tracingService = value;
				EvaluatorTracing.Service = ProviderTracing.tracingService;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public static bool TraceIsSafeException(string entryName, Exception e, IEvaluationConstants evaluationConstants = null, IResource resource = null)
		{
			bool flag;
			using (IHostTrace hostTrace = ProviderTracing.CreateTrace(entryName, evaluationConstants, TraceEventType.Information, resource))
			{
				flag = SafeExceptions.TraceIsSafeException(hostTrace, e);
			}
			return flag;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005C20 File Offset: 0x00003E20
		public static IHostTrace CreateTrace(string entryName, IEvaluationConstants evaluationConstants = null, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return ProviderTracing.Service.CreateTrace(evaluationConstants, entryName, severityLevel, false, resource);
		}

		// Token: 0x04000097 RID: 151
		private static ITracingService tracingService = Microsoft.Mashup.Tracing.TracingService.Instance;
	}
}
