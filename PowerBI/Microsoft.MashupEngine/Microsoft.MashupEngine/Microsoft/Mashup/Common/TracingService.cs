using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C31 RID: 7217
	public static class TracingService
	{
		// Token: 0x0600B41E RID: 46110 RVA: 0x0024914C File Offset: 0x0024734C
		public static ITracingService GetService(IEngineHost host)
		{
			if (host == null)
			{
				return TracingService.dummyTracingService;
			}
			return host.QueryService<ITracingService>() ?? TracingService.dummyTracingService;
		}

		// Token: 0x0600B41F RID: 46111 RVA: 0x00249168 File Offset: 0x00247368
		public static IHostTrace CreateTrace(this ITracingService service, IEvaluationConstants evaluationConstants, string entryName, TraceEventType severityLevel, bool forPerformance, IResource resource)
		{
			IHostTrace hostTrace = service.CreateTrace((evaluationConstants != null) ? evaluationConstants.GetActivityId() : null, (evaluationConstants != null) ? evaluationConstants.GetCorrelationId() : null, entryName, severityLevel, forPerformance, resource);
			if (evaluationConstants != null)
			{
				hostTrace.AddRange(evaluationConstants.GetTracedConstants());
			}
			return hostTrace;
		}

		// Token: 0x0600B420 RID: 46112 RVA: 0x002491B4 File Offset: 0x002473B4
		public static IHostTrace CreateUserTrace(this ITracingService service, IEvaluationConstants evaluationConstants, string entryName, TraceEventType severityLevel)
		{
			IHostTrace hostTrace = service.CreateUserTrace((evaluationConstants != null) ? evaluationConstants.GetActivityId() : null, (evaluationConstants != null) ? evaluationConstants.GetCorrelationId() : null, entryName, severityLevel);
			if (evaluationConstants != null)
			{
				hostTrace.AddRange(evaluationConstants.GetTracedConstants());
			}
			return hostTrace;
		}

		// Token: 0x0600B421 RID: 46113 RVA: 0x002491FA File Offset: 0x002473FA
		public static IHostTrace CreateTrace(IEngineHost host, string entryName, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return TracingService.GetService(host).CreateTrace(entryName, host.GetEvaluationConstants(), severityLevel, resource);
		}

		// Token: 0x0600B422 RID: 46114 RVA: 0x00249210 File Offset: 0x00247410
		public static IHostTrace CreateTrace(this ITracingService service, string entryName, IEvaluationConstants evaluationConstants = null, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return service.CreateTrace(evaluationConstants, entryName, severityLevel, false, resource);
		}

		// Token: 0x0600B423 RID: 46115 RVA: 0x0024921E File Offset: 0x0024741E
		public static IHostTrace CreatePerformanceTrace(IEngineHost host, string entryName, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return TracingService.GetService(host).CreatePerformanceTrace(entryName, host.GetEvaluationConstants(), severityLevel, resource);
		}

		// Token: 0x0600B424 RID: 46116 RVA: 0x00249234 File Offset: 0x00247434
		public static IHostTrace CreatePerformanceTrace(this ITracingService service, string entryName, IEvaluationConstants evaluationConstants = null, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return service.CreateTrace(evaluationConstants, entryName, severityLevel, true, resource);
		}

		// Token: 0x0600B425 RID: 46117 RVA: 0x0024921E File Offset: 0x0024741E
		public static IHostTrace ScopedPerformanceTrace(IEngineHost host, string entryName, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return TracingService.GetService(host).CreatePerformanceTrace(entryName, host.GetEvaluationConstants(), severityLevel, resource);
		}

		// Token: 0x0600B426 RID: 46118 RVA: 0x00249244 File Offset: 0x00247444
		public static IHostTrace ScopedPerformanceTrace(this ITracingService service, string entryName, IEvaluationConstants evaluationConstants = null, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			using (service.CreatePerformanceTrace(entryName + "/Begin", evaluationConstants, severityLevel, resource))
			{
			}
			return service.CreatePerformanceTrace(entryName + "/End  ", evaluationConstants, severityLevel, resource);
		}

		// Token: 0x0600B427 RID: 46119 RVA: 0x00249298 File Offset: 0x00247498
		public static void AddRange(this IHostTrace trace, IEnumerable<EvaluationConstant> constants)
		{
			foreach (EvaluationConstant evaluationConstant in constants)
			{
				trace.Add(evaluationConstant.Name, evaluationConstant.Value, evaluationConstant.IsPii);
			}
		}

		// Token: 0x0600B428 RID: 46120 RVA: 0x002492F4 File Offset: 0x002474F4
		public static void AddResource(this IHostTrace trace, IResource resource)
		{
			if (resource != null)
			{
				trace.Add("ResourceKind", resource.Kind, false);
				if (!string.IsNullOrEmpty(resource.Path))
				{
					trace.Add("ResourcePath", resource.Path, true);
				}
			}
		}

		// Token: 0x0600B429 RID: 46121 RVA: 0x0024932C File Offset: 0x0024752C
		public static void AddValues<T>(this IHostTrace trace, string name, bool isPii, IEnumerable<T> arrayValues)
		{
			using (IHostTraceValue hostTraceValue = trace.Begin(name, isPii))
			{
				hostTraceValue.Begin();
				foreach (T t in (arrayValues ?? EmptyArray<T>.Instance))
				{
					object obj = t;
					hostTraceValue.Add(obj);
				}
				hostTraceValue.End();
			}
		}

		// Token: 0x04005BB2 RID: 23474
		private static TracingService.DummyTracingService dummyTracingService = new TracingService.DummyTracingService();

		// Token: 0x02001C32 RID: 7218
		private class DummyTracingService : ITracingService, ITracingOptions
		{
			// Token: 0x17002D10 RID: 11536
			// (get) Token: 0x0600B42B RID: 46123 RVA: 0x000020FA File Offset: 0x000002FA
			public string TracePath
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002D11 RID: 11537
			// (get) Token: 0x0600B42C RID: 46124 RVA: 0x00002105 File Offset: 0x00000305
			public SourceLevels Levels
			{
				get
				{
					return SourceLevels.Off;
				}
			}

			// Token: 0x17002D12 RID: 11538
			// (get) Token: 0x0600B42D RID: 46125 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public ITracingOptions Options
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002D13 RID: 11539
			// (get) Token: 0x0600B42E RID: 46126 RVA: 0x00191195 File Offset: 0x0018F395
			public IEnumerable<string> Keys
			{
				get
				{
					return EmptyArray<string>.Instance;
				}
			}

			// Token: 0x0600B42F RID: 46127 RVA: 0x0000336E File Offset: 0x0000156E
			public void Trace(TraceEventType action, string message, TraceEventType severityLevel)
			{
			}

			// Token: 0x0600B430 RID: 46128 RVA: 0x002493BC File Offset: 0x002475BC
			public IHostTrace CreateTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel, bool forPerformance, IResource resource)
			{
				return TracingService.DummyHostTrace.Instance;
			}

			// Token: 0x0600B431 RID: 46129 RVA: 0x002493BC File Offset: 0x002475BC
			public IHostTrace CreateUserTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel)
			{
				return TracingService.DummyHostTrace.Instance;
			}

			// Token: 0x0600B432 RID: 46130 RVA: 0x0000336E File Offset: 0x0000156E
			public void Flush()
			{
			}

			// Token: 0x0600B433 RID: 46131 RVA: 0x0000336E File Offset: 0x0000156E
			public void Close()
			{
			}

			// Token: 0x0600B434 RID: 46132 RVA: 0x00002105 File Offset: 0x00000305
			public bool IsEnabled(string key)
			{
				return false;
			}
		}

		// Token: 0x02001C33 RID: 7219
		private sealed class DummyHostTrace : IHostTrace, IDisposable
		{
			// Token: 0x0600B436 RID: 46134 RVA: 0x000020FD File Offset: 0x000002FD
			private DummyHostTrace()
			{
			}

			// Token: 0x17002D14 RID: 11540
			// (get) Token: 0x0600B437 RID: 46135 RVA: 0x00002105 File Offset: 0x00000305
			public SourceLevels Levels
			{
				get
				{
					return SourceLevels.Off;
				}
			}

			// Token: 0x0600B438 RID: 46136 RVA: 0x0000336E File Offset: 0x0000156E
			public void Suspend()
			{
			}

			// Token: 0x0600B439 RID: 46137 RVA: 0x0000336E File Offset: 0x0000156E
			public void Resume()
			{
			}

			// Token: 0x0600B43A RID: 46138 RVA: 0x002493C3 File Offset: 0x002475C3
			public IHostTraceValue Begin(string name, bool isPii)
			{
				return new TracingService.DummyHostTraceValue();
			}

			// Token: 0x0600B43B RID: 46139 RVA: 0x0000336E File Offset: 0x0000156E
			public void Add(string name, object value, bool isPii)
			{
			}

			// Token: 0x0600B43C RID: 46140 RVA: 0x0000336E File Offset: 0x0000156E
			public void Add(Exception e, bool hasPii = true)
			{
			}

			// Token: 0x0600B43D RID: 46141 RVA: 0x0000336E File Offset: 0x0000156E
			public void Add(Exception e, TraceEventType type, bool hasPii = true)
			{
			}

			// Token: 0x0600B43E RID: 46142 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005BB3 RID: 23475
			public static readonly TracingService.DummyHostTrace Instance = new TracingService.DummyHostTrace();
		}

		// Token: 0x02001C34 RID: 7220
		private sealed class DummyHostTraceValue : IHostTraceValue, IDisposable
		{
			// Token: 0x0600B440 RID: 46144 RVA: 0x0000336E File Offset: 0x0000156E
			public void Add(object value)
			{
			}

			// Token: 0x0600B441 RID: 46145 RVA: 0x0000336E File Offset: 0x0000156E
			public void Begin()
			{
			}

			// Token: 0x0600B442 RID: 46146 RVA: 0x0000336E File Offset: 0x0000156E
			public void End()
			{
			}

			// Token: 0x0600B443 RID: 46147 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}
		}
	}
}
