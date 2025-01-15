using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Azure.Core.Diagnostics;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200008B RID: 139
	[NullableContext(1)]
	[Nullable(0)]
	public static class HttpPipelineBuilder
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0000D884 File Offset: 0x0000BA84
		public static HttpPipeline Build(ClientOptions options, params HttpPipelinePolicy[] perRetryPolicies)
		{
			return HttpPipelineBuilder.Build(options, Array.Empty<HttpPipelinePolicy>(), perRetryPolicies, ResponseClassifier.Shared);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000D898 File Offset: 0x0000BA98
		public static HttpPipeline Build(ClientOptions options, HttpPipelinePolicy[] perCallPolicies, HttpPipelinePolicy[] perRetryPolicies, [Nullable(2)] ResponseClassifier responseClassifier)
		{
			HttpPipelineOptions httpPipelineOptions = new HttpPipelineOptions(options);
			httpPipelineOptions.ResponseClassifier = responseClassifier;
			((List<HttpPipelinePolicy>)httpPipelineOptions.PerCallPolicies).AddRange(perCallPolicies);
			((List<HttpPipelinePolicy>)httpPipelineOptions.PerRetryPolicies).AddRange(perRetryPolicies);
			global::System.ValueTuple<ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool> valueTuple = HttpPipelineBuilder.BuildInternal(httpPipelineOptions, null);
			return new HttpPipeline(valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5, valueTuple.Item1);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000D900 File Offset: 0x0000BB00
		public static DisposableHttpPipeline Build(ClientOptions options, HttpPipelinePolicy[] perCallPolicies, HttpPipelinePolicy[] perRetryPolicies, HttpPipelineTransportOptions transportOptions, [Nullable(2)] ResponseClassifier responseClassifier)
		{
			Argument.AssertNotNull<HttpPipelineTransportOptions>(transportOptions, "transportOptions");
			HttpPipelineOptions httpPipelineOptions = new HttpPipelineOptions(options);
			httpPipelineOptions.ResponseClassifier = responseClassifier;
			((List<HttpPipelinePolicy>)httpPipelineOptions.PerCallPolicies).AddRange(perCallPolicies);
			((List<HttpPipelinePolicy>)httpPipelineOptions.PerRetryPolicies).AddRange(perRetryPolicies);
			global::System.ValueTuple<ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool> valueTuple = HttpPipelineBuilder.BuildInternal(httpPipelineOptions, transportOptions);
			return new DisposableHttpPipeline(valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5, valueTuple.Item1, valueTuple.Item6);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000D978 File Offset: 0x0000BB78
		public static HttpPipeline Build(HttpPipelineOptions options)
		{
			global::System.ValueTuple<ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool> valueTuple = HttpPipelineBuilder.BuildInternal(options, null);
			return new HttpPipeline(valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5, valueTuple.Item1);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		public static DisposableHttpPipeline Build(HttpPipelineOptions options, HttpPipelineTransportOptions transportOptions)
		{
			Argument.AssertNotNull<HttpPipelineTransportOptions>(transportOptions, "transportOptions");
			global::System.ValueTuple<ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool> valueTuple = HttpPipelineBuilder.BuildInternal(options, transportOptions);
			return new DisposableHttpPipeline(valueTuple.Item2, valueTuple.Item3, valueTuple.Item4, valueTuple.Item5, valueTuple.Item1, valueTuple.Item6);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Classifier", "Transport", "PerCallIndex", "PerRetryIndex", "Policies", "IsTransportOwned" })]
		[return: Nullable(new byte[] { 0, 1, 1, 1, 1 })]
		internal static global::System.ValueTuple<ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool> BuildInternal(HttpPipelineOptions buildOptions, [Nullable(2)] HttpPipelineTransportOptions defaultTransportOptions)
		{
			HttpPipelineBuilder.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.buildOptions = buildOptions;
			Argument.AssertNotNull<IList<HttpPipelinePolicy>>(CS$<>8__locals1.buildOptions.PerCallPolicies, "PerCallPolicies");
			Argument.AssertNotNull<IList<HttpPipelinePolicy>>(CS$<>8__locals1.buildOptions.PerRetryPolicies, "PerRetryPolicies");
			int num = 8;
			List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>> policies = CS$<>8__locals1.buildOptions.ClientOptions.Policies;
			CS$<>8__locals1.policies = new List<HttpPipelinePolicy>(num + ((policies != null) ? policies.Count : 0) + CS$<>8__locals1.buildOptions.PerCallPolicies.Count + CS$<>8__locals1.buildOptions.PerRetryPolicies.Count);
			DiagnosticsOptions diagnostics = CS$<>8__locals1.buildOptions.ClientOptions.Diagnostics;
			HttpMessageSanitizer httpMessageSanitizer = new HttpMessageSanitizer(diagnostics.LoggedQueryParameters.ToArray<string>(), diagnostics.LoggedHeaderNames.ToArray<string>(), "REDACTED");
			bool isDistributedTracingEnabled = CS$<>8__locals1.buildOptions.ClientOptions.Diagnostics.IsDistributedTracingEnabled;
			CS$<>8__locals1.policies.Add(ReadClientRequestIdPolicy.Shared);
			HttpPipelineBuilder.<BuildInternal>g__AddNonNullPolicies|5_1(CS$<>8__locals1.buildOptions.PerCallPolicies.ToArray<HttpPipelinePolicy>(), ref CS$<>8__locals1);
			HttpPipelineBuilder.<BuildInternal>g__AddCustomerPolicies|5_0(HttpPipelinePosition.PerCall, ref CS$<>8__locals1);
			int count = CS$<>8__locals1.policies.Count;
			CS$<>8__locals1.policies.Add(ClientRequestIdPolicy.Shared);
			if (diagnostics.IsTelemetryEnabled)
			{
				CS$<>8__locals1.policies.Add(HttpPipelineBuilder.CreateTelemetryPolicy(CS$<>8__locals1.buildOptions.ClientOptions));
			}
			RetryOptions retry = CS$<>8__locals1.buildOptions.ClientOptions.Retry;
			List<HttpPipelinePolicy> policies2 = CS$<>8__locals1.policies;
			HttpPipelinePolicy httpPipelinePolicy;
			if ((httpPipelinePolicy = CS$<>8__locals1.buildOptions.ClientOptions.RetryPolicy) == null)
			{
				httpPipelinePolicy = new RetryPolicy(retry.MaxRetries, (retry.Mode == RetryMode.Exponential) ? DelayStrategy.CreateExponentialDelayStrategy(new TimeSpan?(retry.Delay), new TimeSpan?(retry.MaxDelay)) : DelayStrategy.CreateFixedDelayStrategy(new TimeSpan?(retry.Delay)));
			}
			policies2.Add(httpPipelinePolicy);
			bool? flag = ((defaultTransportOptions != null) ? new bool?(defaultTransportOptions.IsClientRedirectEnabled) : null);
			RedirectPolicy redirectPolicy;
			if (flag != null && flag.GetValueOrDefault())
			{
				redirectPolicy = new RedirectPolicy(true);
			}
			else
			{
				redirectPolicy = RedirectPolicy.Shared;
			}
			RedirectPolicy redirectPolicy2 = redirectPolicy;
			CS$<>8__locals1.policies.Add(redirectPolicy2);
			HttpPipelineBuilder.<BuildInternal>g__AddNonNullPolicies|5_1(CS$<>8__locals1.buildOptions.PerRetryPolicies.ToArray<HttpPipelinePolicy>(), ref CS$<>8__locals1);
			HttpPipelineBuilder.<BuildInternal>g__AddCustomerPolicies|5_0(HttpPipelinePosition.PerRetry, ref CS$<>8__locals1);
			int count2 = CS$<>8__locals1.policies.Count;
			if (diagnostics.IsLoggingEnabled)
			{
				string name = CS$<>8__locals1.buildOptions.ClientOptions.GetType().Assembly.GetName().Name;
				CS$<>8__locals1.policies.Add(new LoggingPolicy(diagnostics.IsLoggingContentEnabled, diagnostics.LoggedContentSizeLimit, httpMessageSanitizer, name));
			}
			CS$<>8__locals1.policies.Add(new ResponseBodyPolicy(CS$<>8__locals1.buildOptions.ClientOptions.Retry.NetworkTimeout));
			CS$<>8__locals1.policies.Add(new RequestActivityPolicy(isDistributedTracingEnabled, ClientDiagnostics.GetResourceProviderNamespace(CS$<>8__locals1.buildOptions.ClientOptions.GetType().Assembly), httpMessageSanitizer));
			HttpPipelineBuilder.<BuildInternal>g__AddCustomerPolicies|5_0(HttpPipelinePosition.BeforeTransport, ref CS$<>8__locals1);
			HttpPipelineTransport httpPipelineTransport = CS$<>8__locals1.buildOptions.ClientOptions.Transport;
			bool flag2 = false;
			if (defaultTransportOptions != null)
			{
				if (CS$<>8__locals1.buildOptions.ClientOptions.IsCustomTransportSet)
				{
					AzureCoreEventSource.Singleton.PipelineTransportOptionsNotApplied(CS$<>8__locals1.buildOptions.ClientOptions.GetType());
				}
				else
				{
					httpPipelineTransport = HttpPipelineTransport.Create(defaultTransportOptions);
					flag2 = true;
				}
			}
			CS$<>8__locals1.policies.Add(new HttpPipelineTransportPolicy(httpPipelineTransport, httpMessageSanitizer, CS$<>8__locals1.buildOptions.RequestFailedDetailsParser));
			HttpPipelineOptions buildOptions2 = CS$<>8__locals1.buildOptions;
			if (buildOptions2.ResponseClassifier == null)
			{
				buildOptions2.ResponseClassifier = ResponseClassifier.Shared;
			}
			return new global::System.ValueTuple<ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool>(CS$<>8__locals1.buildOptions.ResponseClassifier, httpPipelineTransport, count, count2, CS$<>8__locals1.policies.ToArray(), flag2);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000DD84 File Offset: 0x0000BF84
		internal static TelemetryPolicy CreateTelemetryPolicy(ClientOptions options)
		{
			return new TelemetryPolicy(new TelemetryDetails(options.GetType().Assembly, options.Diagnostics.ApplicationId));
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		[CompilerGenerated]
		internal static void <BuildInternal>g__AddCustomerPolicies|5_0(HttpPipelinePosition position, ref HttpPipelineBuilder.<>c__DisplayClass5_0 A_1)
		{
			if (A_1.buildOptions.ClientOptions.Policies != null)
			{
				foreach (global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy> valueTuple in A_1.buildOptions.ClientOptions.Policies)
				{
					if (valueTuple.Item1 == position && valueTuple.Item2 != null)
					{
						A_1.policies.Add(valueTuple.Item2);
					}
				}
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000DE34 File Offset: 0x0000C034
		[CompilerGenerated]
		internal static void <BuildInternal>g__AddNonNullPolicies|5_1(HttpPipelinePolicy[] policiesToAdd, ref HttpPipelineBuilder.<>c__DisplayClass5_0 A_1)
		{
			foreach (HttpPipelinePolicy httpPipelinePolicy in policiesToAdd)
			{
				if (httpPipelinePolicy != null)
				{
					A_1.policies.Add(httpPipelinePolicy);
				}
			}
		}
	}
}
