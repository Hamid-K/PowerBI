using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200008A RID: 138
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpPipeline
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		public HttpPipeline(HttpPipelineTransport transport, [Nullable(new byte[] { 2, 1 })] HttpPipelinePolicy[] policies = null, [Nullable(2)] ResponseClassifier responseClassifier = null)
		{
			if (transport == null)
			{
				throw new ArgumentNullException("transport");
			}
			this._transport = transport;
			this.ResponseClassifier = responseClassifier ?? ResponseClassifier.Shared;
			if (policies == null)
			{
				policies = Array.Empty<HttpPipelinePolicy>();
			}
			HttpPipelinePolicy[] array = new HttpPipelinePolicy[policies.Length + 1];
			array[policies.Length] = new HttpPipelineTransportPolicy(this._transport, ClientDiagnostics.CreateMessageSanitizer(new DiagnosticsOptions()), null);
			policies.CopyTo(array, 0);
			this._pipeline = array;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000D378 File Offset: 0x0000B578
		internal HttpPipeline(HttpPipelineTransport transport, int perCallIndex, int perRetryIndex, HttpPipelinePolicy[] pipeline, ResponseClassifier responseClassifier)
		{
			if (responseClassifier == null)
			{
				throw new ArgumentNullException("responseClassifier");
			}
			this.ResponseClassifier = responseClassifier;
			if (transport == null)
			{
				throw new ArgumentNullException("transport");
			}
			this._transport = transport;
			if (pipeline == null)
			{
				throw new ArgumentNullException("pipeline");
			}
			this._pipeline = pipeline;
			this._perCallIndex = perCallIndex;
			this._perRetryIndex = perRetryIndex;
			this._internallyConstructed = true;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000D3E9 File Offset: 0x0000B5E9
		public Request CreateRequest()
		{
			return this._transport.CreateRequest();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000D3F6 File Offset: 0x0000B5F6
		public HttpMessage CreateMessage()
		{
			return new HttpMessage(this.CreateRequest(), this.ResponseClassifier);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000D409 File Offset: 0x0000B609
		public HttpMessage CreateMessage([Nullable(2)] RequestContext context)
		{
			return this.CreateMessage(context, null);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000D414 File Offset: 0x0000B614
		[NullableContext(2)]
		[return: Nullable(1)]
		public HttpMessage CreateMessage(RequestContext context, ResponseClassifier classifier = null)
		{
			HttpMessage httpMessage = this.CreateMessage();
			if (classifier != null)
			{
				httpMessage.ResponseClassifier = classifier;
			}
			httpMessage.ApplyRequestContext(context, classifier);
			return httpMessage;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000D43B File Offset: 0x0000B63B
		public ResponseClassifier ResponseClassifier { get; }

		// Token: 0x06000477 RID: 1143 RVA: 0x0000D444 File Offset: 0x0000B644
		public unsafe ValueTask SendAsync(HttpMessage message, CancellationToken cancellationToken)
		{
			message.CancellationToken = cancellationToken;
			message.ProcessingStartTime = DateTimeOffset.UtcNow;
			HttpPipeline.AddHttpMessageProperties(message);
			if (message.Policies == null || message.Policies.Count == 0)
			{
				return this._pipeline.Span[0]->ProcessAsync(message, this._pipeline.Slice(1));
			}
			return this.SendAsync(message);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		private unsafe async ValueTask SendAsync(HttpMessage message)
		{
			int num = this._pipeline.Length + message.Policies.Count;
			HttpPipelinePolicy[] policies = ArrayPool<HttpPipelinePolicy>.Shared.Rent(num);
			try
			{
				ReadOnlyMemory<HttpPipelinePolicy> readOnlyMemory = this.CreateRequestPipeline(policies, message.Policies);
				await readOnlyMemory.Span[0]->ProcessAsync(message, readOnlyMemory.Slice(1)).ConfigureAwait(false);
			}
			finally
			{
				ArrayPool<HttpPipelinePolicy>.Shared.Return(policies, false);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public unsafe void Send(HttpMessage message, CancellationToken cancellationToken)
		{
			message.CancellationToken = cancellationToken;
			message.ProcessingStartTime = DateTimeOffset.UtcNow;
			HttpPipeline.AddHttpMessageProperties(message);
			if (message.Policies == null || message.Policies.Count == 0)
			{
				this._pipeline.Span[0]->Process(message, this._pipeline.Slice(1));
				return;
			}
			int num = this._pipeline.Length + message.Policies.Count;
			HttpPipelinePolicy[] array = ArrayPool<HttpPipelinePolicy>.Shared.Rent(num);
			try
			{
				ReadOnlyMemory<HttpPipelinePolicy> readOnlyMemory = this.CreateRequestPipeline(array, message.Policies);
				readOnlyMemory.Span[0]->Process(message, readOnlyMemory.Slice(1));
			}
			finally
			{
				ArrayPool<HttpPipelinePolicy>.Shared.Return(array, false);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		[return: Nullable(new byte[] { 0, 1 })]
		public async ValueTask<Response> SendRequestAsync(Request request, CancellationToken cancellationToken)
		{
			HttpMessage message = new HttpMessage(request, this.ResponseClassifier);
			await this.SendAsync(message, cancellationToken).ConfigureAwait(false);
			return message.Response;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000D624 File Offset: 0x0000B824
		public Response SendRequest(Request request, CancellationToken cancellationToken)
		{
			HttpMessage httpMessage = new HttpMessage(request, this.ResponseClassifier);
			this.Send(httpMessage, cancellationToken);
			return httpMessage.Response;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000D64C File Offset: 0x0000B84C
		public static IDisposable CreateClientRequestIdScope([Nullable(2)] string clientRequestId)
		{
			return HttpPipeline.CreateHttpMessagePropertiesScope(new Dictionary<string, object> { { "x-ms-client-request-id", clientRequestId } });
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000D664 File Offset: 0x0000B864
		public static IDisposable CreateHttpMessagePropertiesScope([Nullable(new byte[] { 1, 1, 2 })] IDictionary<string, object> messageProperties)
		{
			Argument.AssertNotNull<IDictionary<string, object>>(messageProperties, "messageProperties");
			HttpPipeline.CurrentHttpMessagePropertiesScope.Value = new HttpPipeline.HttpMessagePropertiesScope(messageProperties, HttpPipeline.CurrentHttpMessagePropertiesScope.Value);
			return HttpPipeline.CurrentHttpMessagePropertiesScope.Value;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000D698 File Offset: 0x0000B898
		[return: Nullable(new byte[] { 0, 1 })]
		private unsafe ReadOnlyMemory<HttpPipelinePolicy> CreateRequestPipeline(HttpPipelinePolicy[] policies, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })] [Nullable(new byte[] { 1, 0, 1 })] List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>> customPolicies)
		{
			if (!this._internallyConstructed)
			{
				throw new InvalidOperationException("Cannot send messages with per-request policies if the pipeline wasn't constructed with HttpPipelineBuilder.");
			}
			ReadOnlySpan<HttpPipelinePolicy> span = this._pipeline.Span;
			int num = span.Length - 1;
			span.Slice(0, this._perCallIndex).CopyTo(policies);
			int num2 = this._perCallIndex;
			int num3 = HttpPipeline.AddCustomPolicies(customPolicies, policies, HttpPipelinePosition.PerCall, num2);
			num2 += num3;
			num3 = this._perRetryIndex - this._perCallIndex;
			span.Slice(this._perCallIndex, num3).CopyTo(MemoryExtensions.AsSpan<HttpPipelinePolicy>(policies, num2, num3));
			num2 += num3;
			num3 = HttpPipeline.AddCustomPolicies(customPolicies, policies, HttpPipelinePosition.PerRetry, num2);
			num2 += num3;
			num3 = num - this._perRetryIndex;
			span.Slice(this._perRetryIndex, num3).CopyTo(MemoryExtensions.AsSpan<HttpPipelinePolicy>(policies, num2, num3));
			num2 += num3;
			num3 = HttpPipeline.AddCustomPolicies(customPolicies, policies, HttpPipelinePosition.BeforeTransport, num2);
			num2 += num3;
			policies[num2] = *span[num];
			return new ReadOnlyMemory<HttpPipelinePolicy>(policies, 0, num2 + 1);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000D790 File Offset: 0x0000B990
		private static int AddCustomPolicies([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })] [Nullable(new byte[] { 1, 0, 1 })] List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>> source, HttpPipelinePolicy[] target, HttpPipelinePosition position, int start)
		{
			int num = 0;
			if (source != null)
			{
				foreach (global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy> valueTuple in source)
				{
					if (valueTuple.Item1 == position)
					{
						target[start + num] = valueTuple.Item2;
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		private static void AddHttpMessageProperties(HttpMessage message)
		{
			if (HttpPipeline.CurrentHttpMessagePropertiesScope.Value != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in HttpPipeline.CurrentHttpMessagePropertiesScope.Value.Properties)
				{
					if (keyValuePair.Value != null)
					{
						message.SetProperty(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x040001CF RID: 463
		[Nullable(new byte[] { 1, 2 })]
		private static readonly AsyncLocal<HttpPipeline.HttpMessagePropertiesScope> CurrentHttpMessagePropertiesScope = new AsyncLocal<HttpPipeline.HttpMessagePropertiesScope>();

		// Token: 0x040001D0 RID: 464
		private protected readonly HttpPipelineTransport _transport;

		// Token: 0x040001D1 RID: 465
		[Nullable(new byte[] { 0, 1 })]
		private readonly ReadOnlyMemory<HttpPipelinePolicy> _pipeline;

		// Token: 0x040001D2 RID: 466
		private readonly bool _internallyConstructed;

		// Token: 0x040001D3 RID: 467
		private readonly int _perCallIndex;

		// Token: 0x040001D4 RID: 468
		private readonly int _perRetryIndex;

		// Token: 0x02000119 RID: 281
		[NullableContext(0)]
		private class HttpMessagePropertiesScope : IDisposable
		{
			// Token: 0x060007DD RID: 2013 RVA: 0x0001CD14 File Offset: 0x0001AF14
			[NullableContext(2)]
			internal HttpMessagePropertiesScope([Nullable(new byte[] { 1, 1, 2 })] IDictionary<string, object> messageProperties, HttpPipeline.HttpMessagePropertiesScope parent)
			{
				if (parent != null)
				{
					this.Properties = new Dictionary<string, object>(parent.Properties);
					using (IEnumerator<KeyValuePair<string, object>> enumerator = messageProperties.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							this.Properties[keyValuePair.Key] = keyValuePair.Value;
						}
						goto IL_0063;
					}
				}
				this.Properties = new Dictionary<string, object>(messageProperties);
				IL_0063:
				this._parent = parent;
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001CD9C File Offset: 0x0001AF9C
			[Nullable(new byte[] { 1, 1, 2 })]
			public Dictionary<string, object> Properties
			{
				[return: Nullable(new byte[] { 1, 1, 2 })]
				get;
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
			public void Dispose()
			{
				if (this._disposed)
				{
					return;
				}
				HttpPipeline.CurrentHttpMessagePropertiesScope.Value = this._parent;
				this._disposed = true;
			}

			// Token: 0x04000420 RID: 1056
			[Nullable(2)]
			private readonly HttpPipeline.HttpMessagePropertiesScope _parent;

			// Token: 0x04000421 RID: 1057
			private bool _disposed;
		}
	}
}
