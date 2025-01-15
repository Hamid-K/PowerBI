using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Diagnostics;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A1 RID: 161
	[NullableContext(1)]
	[Nullable(0)]
	public class RetryPolicy : HttpPipelinePolicy
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		[NullableContext(2)]
		public RetryPolicy(int maxRetries = 3, DelayStrategy delayStrategy = null)
		{
			this._maxRetries = maxRetries;
			this._delayStrategy = delayStrategy ?? DelayStrategy.CreateExponentialDelayStrategy(null, null);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return this.ProcessAsync(message, pipeline, true);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000FA33 File Offset: 0x0000DC33
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this.ProcessAsync(message, pipeline, false).EnsureCompleted();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000FA44 File Offset: 0x0000DC44
		private async ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			List<Exception> exceptions = null;
			Exception lastException;
			for (;;)
			{
				lastException = null;
				long before = Stopwatch.GetTimestamp();
				if (async)
				{
					await this.OnSendingRequestAsync(message).ConfigureAwait(false);
				}
				else
				{
					this.OnSendingRequest(message);
				}
				try
				{
					if (async)
					{
						await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(false);
					}
					else
					{
						HttpPipelinePolicy.ProcessNext(message, pipeline);
					}
				}
				catch (Exception ex)
				{
					if (exceptions == null)
					{
						exceptions = new List<Exception>();
					}
					exceptions.Add(ex);
					lastException = ex;
				}
				if (async)
				{
					await this.OnRequestSentAsync(message).ConfigureAwait(false);
				}
				else
				{
					this.OnRequestSent(message);
				}
				long timestamp = Stopwatch.GetTimestamp();
				double elapsed = (double)(timestamp - before) / (double)Stopwatch.Frequency;
				bool flag = false;
				if (lastException != null || (message.HasResponse && message.Response.IsError))
				{
					flag = ((!async) ? this.ShouldRetry(message, lastException) : (await this.ShouldRetryAsync(message, lastException).ConfigureAwait(false)));
				}
				if (!flag)
				{
					break;
				}
				TimeSpan? timeSpan = (message.HasResponse ? message.Response.Headers.RetryAfter : null);
				TimeSpan timeSpan2 = ((!async) ? this.GetNextDelay(message, timeSpan) : (await this.GetNextDelayAsync(message, timeSpan).ConfigureAwait(false)));
				if (timeSpan2 > TimeSpan.Zero)
				{
					if (async)
					{
						await this.WaitAsync(timeSpan2, message.CancellationToken).ConfigureAwait(false);
					}
					else
					{
						this.Wait(timeSpan2, message.CancellationToken);
					}
				}
				if (message.HasResponse)
				{
					Stream contentStream = message.Response.ContentStream;
					if (contentStream != null)
					{
						contentStream.Dispose();
					}
				}
				message.RetryNumber++;
				AzureCoreEventSource.Singleton.RequestRetrying(message.Request.ClientRequestId, message.RetryNumber, elapsed);
			}
			if (lastException != null)
			{
				if (exceptions.Count == 1)
				{
					ExceptionDispatchInfo.Capture(lastException).Throw();
				}
				throw new AggregateException(string.Format("Retry failed after {0} tries. Retry settings can be adjusted in {1}.{2}", message.RetryNumber + 1, "ClientOptions", "Retry") + " or by configuring a custom retry policy in ClientOptions.RetryPolicy.", exceptions);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		internal virtual async Task WaitAsync(TimeSpan time, CancellationToken cancellationToken)
		{
			await Task.Delay(time, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		internal virtual void Wait(TimeSpan time, CancellationToken cancellationToken)
		{
			cancellationToken.WaitHandle.WaitOne(time);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000FAFB File Offset: 0x0000DCFB
		protected internal virtual bool ShouldRetry(HttpMessage message, [Nullable(2)] Exception exception)
		{
			return this.ShouldRetryInternal(message, exception);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000FB05 File Offset: 0x0000DD05
		[NullableContext(0)]
		protected internal virtual ValueTask<bool> ShouldRetryAsync([Nullable(1)] HttpMessage message, [Nullable(2)] Exception exception)
		{
			return new ValueTask<bool>(this.ShouldRetryInternal(message, exception));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000FB14 File Offset: 0x0000DD14
		private bool ShouldRetryInternal(HttpMessage message, [Nullable(2)] Exception exception)
		{
			if (message.RetryNumber >= this._maxRetries)
			{
				return false;
			}
			if (exception != null)
			{
				return message.ResponseClassifier.IsRetriable(message, exception);
			}
			return message.ResponseClassifier.IsRetriableResponse(message);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000FB43 File Offset: 0x0000DD43
		internal TimeSpan GetNextDelay(HttpMessage message, TimeSpan? retryAfter)
		{
			return this.GetNextDelayInternal(message);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		[NullableContext(0)]
		internal ValueTask<TimeSpan> GetNextDelayAsync([Nullable(1)] HttpMessage message, TimeSpan? retryAfter)
		{
			return new ValueTask<TimeSpan>(this.GetNextDelayInternal(message));
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000FB5A File Offset: 0x0000DD5A
		protected internal virtual void OnSendingRequest(HttpMessage message)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		protected internal virtual ValueTask OnSendingRequestAsync(HttpMessage message)
		{
			return default(ValueTask);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000FB72 File Offset: 0x0000DD72
		protected internal virtual void OnRequestSent(HttpMessage message)
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000FB74 File Offset: 0x0000DD74
		protected internal virtual ValueTask OnRequestSentAsync(HttpMessage message)
		{
			return default(ValueTask);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000FB8A File Offset: 0x0000DD8A
		private TimeSpan GetNextDelayInternal(HttpMessage message)
		{
			return this._delayStrategy.GetNextDelay(message.HasResponse ? message.Response : null, message.RetryNumber + 1);
		}

		// Token: 0x04000214 RID: 532
		private readonly int _maxRetries;

		// Token: 0x04000215 RID: 533
		private readonly DelayStrategy _delayStrategy;
	}
}
