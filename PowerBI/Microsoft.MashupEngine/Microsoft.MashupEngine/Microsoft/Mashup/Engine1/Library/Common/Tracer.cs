using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200114B RID: 4427
	internal sealed class Tracer
	{
		// Token: 0x060073F1 RID: 29681 RVA: 0x0018EA40 File Offset: 0x0018CC40
		public Tracer(IEngineHost host, string tracePrefix, IResource resource, Dictionary<string, object> additionalValuesToTrace = null, Func<IHostTrace, Exception, bool> traceException = null)
		{
			this.service = TracingService.GetService(host);
			this.evaluationConstants = host.GetEvaluationConstants();
			this.featureLogging = host.QueryService<IFeatureLoggingService>();
			this.tracePrefix = tracePrefix;
			if (host.QueryService<IExtensibilityService>() != null)
			{
				this.resource = InvocationEngineHost.GetDataSourceChain(host, resource).LastOrDefault<IResource>();
			}
			else
			{
				this.resource = resource;
			}
			this.additionalValuesToTrace = additionalValuesToTrace;
			this.traceException = traceException ?? new Func<IHostTrace, Exception, bool>(TraceExtensions.AddExceptionInFilter);
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x060073F2 RID: 29682 RVA: 0x0018EAC1 File Offset: 0x0018CCC1
		public bool IsEmpty
		{
			get
			{
				return this.service == null;
			}
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x060073F3 RID: 29683 RVA: 0x0018EACC File Offset: 0x0018CCCC
		public bool VerboseEnabled
		{
			get
			{
				return this.service.VerboseEnabled();
			}
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x060073F4 RID: 29684 RVA: 0x0018EAD9 File Offset: 0x0018CCD9
		public bool Enabled
		{
			get
			{
				return this.service.Enabled();
			}
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x0018EAE6 File Offset: 0x0018CCE6
		public void LogFeature(string feature)
		{
			if (this.featureLogging != null)
			{
				this.featureLogging.LogFeature(feature);
			}
		}

		// Token: 0x060073F6 RID: 29686 RVA: 0x0018EAFC File Offset: 0x0018CCFC
		public IHostTrace CreateTrace(string method, TraceEventType level = TraceEventType.Information)
		{
			IHostTrace hostTrace = this.service.CreateTrace(this.tracePrefix + method, this.evaluationConstants, level, this.resource);
			this.AddAdditionalValuesToTrace(hostTrace);
			return hostTrace;
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x0018EB38 File Offset: 0x0018CD38
		public IHostTrace ScopedPerformanceTrace(string method, TraceEventType level = TraceEventType.Information)
		{
			IHostTrace hostTrace = this.service.ScopedPerformanceTrace(this.tracePrefix + method, this.evaluationConstants, level, this.resource);
			this.AddAdditionalValuesToTrace(hostTrace);
			return hostTrace;
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x0018EB74 File Offset: 0x0018CD74
		public IHostTrace CreatePerformanceTrace(string method, TraceEventType level = TraceEventType.Information)
		{
			IHostTrace hostTrace = this.service.CreatePerformanceTrace(this.tracePrefix + method, this.evaluationConstants, level, this.resource);
			this.AddAdditionalValuesToTrace(hostTrace);
			return hostTrace;
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x0018EBAE File Offset: 0x0018CDAE
		public T Trace<T>(string method, Func<IHostTrace, T> func)
		{
			return this.TraceCommon<T>(this.CreateTrace(method, TraceEventType.Information), func);
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x0018EBBF File Offset: 0x0018CDBF
		public void Trace(string method, Action<IHostTrace> action)
		{
			this.Trace<int>(method, Tracer.AsFunction(action));
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x0018EBCF File Offset: 0x0018CDCF
		public T TracePerformance<T>(string method, Func<IHostTrace, T> func)
		{
			return this.TraceCommon<T>(this.CreatePerformanceTrace(method, TraceEventType.Information), func);
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x0018EBE0 File Offset: 0x0018CDE0
		public void TracePerformance(string method, Action<IHostTrace> action)
		{
			this.TracePerformance<int>(method, Tracer.AsFunction(action));
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x0018EBF0 File Offset: 0x0018CDF0
		private void AddAdditionalValuesToTrace(IHostTrace trace)
		{
			if (this.additionalValuesToTrace != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.additionalValuesToTrace)
				{
					trace.Add(keyValuePair.Key, keyValuePair.Value, true);
				}
			}
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x0018EC5C File Offset: 0x0018CE5C
		private static Func<IHostTrace, int> AsFunction(Action<IHostTrace> action)
		{
			return delegate(IHostTrace trace)
			{
				action(trace);
				return 0;
			};
		}

		// Token: 0x060073FF RID: 29695 RVA: 0x0018EC78 File Offset: 0x0018CE78
		private T TraceCommon<T>(IHostTrace trace, Func<IHostTrace, T> func)
		{
			T t;
			try
			{
				t = func(trace);
			}
			catch (Exception ex) when (this.traceException(trace, ex))
			{
				throw;
			}
			finally
			{
				if (trace != null)
				{
					trace.Dispose();
				}
			}
			return t;
		}

		// Token: 0x04003FD5 RID: 16341
		private readonly ITracingService service;

		// Token: 0x04003FD6 RID: 16342
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04003FD7 RID: 16343
		private readonly IFeatureLoggingService featureLogging;

		// Token: 0x04003FD8 RID: 16344
		private readonly string tracePrefix;

		// Token: 0x04003FD9 RID: 16345
		private readonly IResource resource;

		// Token: 0x04003FDA RID: 16346
		private readonly Dictionary<string, object> additionalValuesToTrace;

		// Token: 0x04003FDB RID: 16347
		private readonly Func<IHostTrace, Exception, bool> traceException;
	}
}
