using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200009B RID: 155
	[NullableContext(1)]
	[Nullable(0)]
	internal class RequestActivityPolicy : HttpPipelinePolicy
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000F104 File Offset: 0x0000D304
		public RequestActivityPolicy(bool isDistributedTracingEnabled, [Nullable(2)] string resourceProviderNamespace, HttpMessageSanitizer httpMessageSanitizer)
		{
			this._isDistributedTracingEnabled = isDistributedTracingEnabled;
			this._resourceProviderNamespace = resourceProviderNamespace;
			this._sanitizer = httpMessageSanitizer;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000F121 File Offset: 0x0000D321
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (this.ShouldCreateActivity)
			{
				return this.ProcessAsync(message, pipeline, true);
			}
			return RequestActivityPolicy.ProcessNextAsync(message, pipeline, true);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000F13D File Offset: 0x0000D33D
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (this.ShouldCreateActivity)
			{
				this.ProcessAsync(message, pipeline, false).EnsureCompleted();
				return;
			}
			RequestActivityPolicy.ProcessNextAsync(message, pipeline, false).EnsureCompleted();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000F164 File Offset: 0x0000D364
		private ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			RequestActivityPolicy.<ProcessAsync>d__11 <ProcessAsync>d__;
			<ProcessAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<ProcessAsync>d__.<>4__this = this;
			<ProcessAsync>d__.message = message;
			<ProcessAsync>d__.pipeline = pipeline;
			<ProcessAsync>d__.async = async;
			<ProcessAsync>d__.<>1__state = -1;
			<ProcessAsync>d__.<>t__builder.Start<RequestActivityPolicy.<ProcessAsync>d__11>(ref <ProcessAsync>d__);
			return <ProcessAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000F1BF File Offset: 0x0000D3BF
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The values being passed into Write have the commonly used properties being preserved with DynamicallyAccessedMembers.")]
		private DiagnosticScope CreateDiagnosticScope<[Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(T sourceArgs)
		{
			return new DiagnosticScope("Azure.Core.Http.Request", RequestActivityPolicy.s_diagnosticSource, sourceArgs, RequestActivityPolicy.s_activitySource, ActivityKind.Client, false);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		private static ValueTask ProcessNextAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			Activity activity = Activity.Current;
			if (activity != null)
			{
				string text = activity.Id ?? string.Empty;
				if (activity.IdFormat == ActivityIdFormat.W3C)
				{
					if (!message.Request.Headers.Contains("traceparent"))
					{
						message.Request.Headers.Add("traceparent", text);
						string traceStateString = activity.TraceStateString;
						if (traceStateString != null)
						{
							message.Request.Headers.Add("tracestate", traceStateString);
						}
					}
				}
				else if (!message.Request.Headers.Contains("Request-Id"))
				{
					message.Request.Headers.Add("Request-Id", text);
				}
			}
			if (async)
			{
				return HttpPipelinePolicy.ProcessNextAsync(message, pipeline);
			}
			HttpPipelinePolicy.ProcessNext(message, pipeline);
			return default(ValueTask);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000F2B9 File Offset: 0x0000D4B9
		private bool ShouldCreateActivity
		{
			get
			{
				return this._isDistributedTracingEnabled && (RequestActivityPolicy.s_diagnosticSource.IsEnabled() || this.IsActivitySourceEnabled);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000F2D9 File Offset: 0x0000D4D9
		private bool IsActivitySourceEnabled
		{
			get
			{
				return this._isDistributedTracingEnabled && RequestActivityPolicy.s_activitySource.HasListeners();
			}
		}

		// Token: 0x04000205 RID: 517
		private readonly bool _isDistributedTracingEnabled;

		// Token: 0x04000206 RID: 518
		[Nullable(2)]
		private readonly string _resourceProviderNamespace;

		// Token: 0x04000207 RID: 519
		private readonly HttpMessageSanitizer _sanitizer;

		// Token: 0x04000208 RID: 520
		private const string TraceParentHeaderName = "traceparent";

		// Token: 0x04000209 RID: 521
		private const string TraceStateHeaderName = "tracestate";

		// Token: 0x0400020A RID: 522
		private const string RequestIdHeaderName = "Request-Id";

		// Token: 0x0400020B RID: 523
		private static readonly DiagnosticListener s_diagnosticSource = new DiagnosticListener("Azure.Core");

		// Token: 0x0400020C RID: 524
		private static readonly ActivitySource s_activitySource = new ActivitySource("Azure.Core.Http", "");
	}
}
