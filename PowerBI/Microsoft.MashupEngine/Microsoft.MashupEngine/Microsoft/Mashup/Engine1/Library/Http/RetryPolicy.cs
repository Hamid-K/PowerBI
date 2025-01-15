using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A99 RID: 2713
	internal sealed class RetryPolicy
	{
		// Token: 0x06004BF2 RID: 19442 RVA: 0x000FAEBB File Offset: 0x000F90BB
		public RetryPolicy(int maxTries, int maxTriesForBusy)
			: this(maxTries, new Func<Exception, RetryHandlerResult>(RetryPolicy.IsTransient), null, RetryDelayAlgorithm.ExponentialBackoff, maxTriesForBusy)
		{
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x000FAED3 File Offset: 0x000F90D3
		public RetryPolicy(int maxTries, Func<Exception, RetryHandlerResult> retryPolicyHandler)
			: this(maxTries, retryPolicyHandler, null, RetryDelayAlgorithm.ExponentialBackoff, 6)
		{
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x000FAEE0 File Offset: 0x000F90E0
		public RetryPolicy(int maxTries, Func<Exception, RetryHandlerResult> retryPolicyHandler, int[] nonErrors, RetryDelayAlgorithm retryAlgorithm, int maxTriesForBusy)
		{
			this.maxTries = maxTries;
			this.retryPolicyHandler = retryPolicyHandler;
			this.nonErrors = nonErrors;
			this.delayAlgorithm = retryAlgorithm;
			this.maxTriesForBusy = maxTriesForBusy;
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x000FAF0D File Offset: 0x000F910D
		public int MaxTries
		{
			get
			{
				return this.maxTries;
			}
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x000FAF18 File Offset: 0x000F9118
		private void TraceRetriesSucceeded(Tracer tracer, int maxAttempts, int attempt)
		{
			if (attempt > 1)
			{
				using (IHostTrace hostTrace = tracer.CreateTrace("RunWithRetry/Success", TraceEventType.Information))
				{
					hostTrace.Add("Attempt", attempt - 1, false);
					hostTrace.Add("MaxAttempt", maxAttempts - 1, false);
				}
			}
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x000FAF7C File Offset: 0x000F917C
		private bool IsThrottledByFabric(Tracer tracer, Exception e, IEngineHost host, IResource resource, out Exception processedException)
		{
			if (FabricThrottle.IsThrottledByFabric(e, host, resource, out processedException))
			{
				using (IHostTrace hostTrace = tracer.CreateTrace("RunWithRetry/FabricThrottling", TraceEventType.Information))
				{
					hostTrace.Add(processedException, true);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x000FAFCC File Offset: 0x000F91CC
		private bool IsHandledByRetryPolicy(Tracer tracer, Exception e, IEngineHost host, int attempt, ref int maxAttempts, out Exception processedException)
		{
			bool flag = this.InManualStatusHandlingList(e);
			if (this.IsBusy(e))
			{
				maxAttempts = this.maxTriesForBusy;
			}
			RetryHandlerResult retryHandlerResult;
			bool flag2;
			bool flag3;
			if (!SafeExceptions.IsSafeException(e))
			{
				retryHandlerResult = RetryHandlerResult.FailWithOriginalException;
				flag2 = true;
				processedException = null;
				flag3 = false;
			}
			else
			{
				retryHandlerResult = this.HandleWithRetryPolicy(e, tracer);
				flag2 = retryHandlerResult.Type == RetryHandlerResultType.FailWithOriginalException || retryHandlerResult.Type == RetryHandlerResultType.FailWithCustomException;
				processedException = ((retryHandlerResult.Type == RetryHandlerResultType.FailWithCustomException) ? retryHandlerResult.Exception : null);
				flag3 = processedException == null && attempt < maxAttempts && !flag;
			}
			using (IHostTrace hostTrace = tracer.CreateTrace(flag2 ? "RunWithRetry/Failure" : "RunWithRetry/Exception", TraceEventType.Information))
			{
				hostTrace.Add("Attempt", attempt - 1, false);
				hostTrace.Add("MaxAttempt", maxAttempts - 1, false);
				hostTrace.Add("ManualStatusHandling", flag, false);
				hostTrace.Add(processedException ?? e, ((flag2 || attempt == maxAttempts) && !flag) ? TraceEventType.Error : TraceEventType.Warning, true);
				TimeSpan timeSpan;
				switch (retryHandlerResult.Type)
				{
				case RetryHandlerResultType.FailWithOriginalException:
					return false;
				case RetryHandlerResultType.FailWithCustomException:
					return true;
				case RetryHandlerResultType.RetryAfterDefaultDelay:
					if (!flag3)
					{
						return false;
					}
					hostTrace.Add("RetryAlgorithm", this.delayAlgorithm, false);
					if (this.delayAlgorithm == RetryDelayAlgorithm.ExponentialBackoff)
					{
						timeSpan = RetryPolicy.ExponentialBackoff(attempt);
					}
					else
					{
						timeSpan = RetryPolicy.ExponentialBackoff(0);
					}
					break;
				case RetryHandlerResultType.RetryAfterCustomDelay:
					if (!flag3)
					{
						return false;
					}
					timeSpan = retryHandlerResult.Delay;
					break;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				hostTrace.Add("RetryWaitTimeMs", timeSpan.TotalMilliseconds, false);
				host.LogIgnoredException(e);
				RetryPolicy.OnSleep(timeSpan);
			}
			return true;
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x000FB1C0 File Offset: 0x000F93C0
		private RetryHandlerResult HandleWithRetryPolicy(Exception e, Tracer tracer)
		{
			RetryHandlerResult retryHandlerResult;
			try
			{
				retryHandlerResult = this.retryPolicyHandler(e);
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				using (IHostTrace hostTrace = tracer.CreateTrace("RetryHandler", TraceEventType.Information))
				{
					hostTrace.Add(ex, TraceEventType.Warning, true);
					retryHandlerResult = RetryHandlerResult.FailWithOriginalException;
				}
			}
			return retryHandlerResult;
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x000FB23C File Offset: 0x000F943C
		public TResult Execute<TResult>(IEngineHost host, IResource resource, Func<TResult> func, Tracer tracer)
		{
			int num = this.maxTries;
			int num2 = 1;
			TResult tresult2;
			for (;;)
			{
				Exception ex2;
				try
				{
					TResult tresult = func();
					this.TraceRetriesSucceeded(tracer, num, num2);
					tresult2 = tresult;
					break;
				}
				catch (Exception ex) when (this.IsThrottledByFabric(tracer, ex, host, resource, out ex2) || this.IsHandledByRetryPolicy(tracer, ex, host, num2, ref num, out ex2))
				{
					if (ex2 != null)
					{
						throw ex2;
					}
				}
				num2++;
			}
			return tresult2;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x000FB2BC File Offset: 0x000F94BC
		private bool InManualStatusHandlingList(Exception ex)
		{
			WebException ex2 = ex as WebException;
			if (ex2 != null)
			{
				MashupHttpWebResponse mashupHttpWebResponse = ex2.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					HttpStatusCode statusCode = mashupHttpWebResponse.StatusCode;
					if (this.nonErrors != null && this.nonErrors.Contains((int)statusCode))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x000FB304 File Offset: 0x000F9504
		private bool IsBusy(Exception ex)
		{
			WebException ex2 = ex as WebException;
			if (ex2 != null)
			{
				MashupHttpWebResponse mashupHttpWebResponse = ex2.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null && mashupHttpWebResponse.StatusCode == (HttpStatusCode)429)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x000FB33C File Offset: 0x000F953C
		private static TimeSpan ExponentialBackoff(int retryNumber)
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(Math.Pow(2.0, (double)retryNumber) * 1000.0);
			if (!(timeSpan < RetryPolicy.MaxClientDrivenBackoff))
			{
				return RetryPolicy.MaxClientDrivenBackoff;
			}
			return timeSpan;
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x000FB37D File Offset: 0x000F957D
		public static RetryHandlerResult NoRetryPolicy(Exception ex)
		{
			return RetryHandlerResult.FailWithOriginalException;
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x000FB384 File Offset: 0x000F9584
		public static RetryHandlerResult IsTransient(Exception ex)
		{
			WebException ex2 = ex as WebException;
			if (ex2 != null)
			{
				if (ex2.Status == WebExceptionStatus.Timeout || ex2.Status == WebExceptionStatus.ConnectionClosed || ex2.Status == WebExceptionStatus.KeepAliveFailure)
				{
					return RetryHandlerResult.RetryAfterDefaultDelay;
				}
				MashupHttpWebResponse mashupHttpWebResponse = ex2.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					HttpStatusCode statusCode = mashupHttpWebResponse.StatusCode;
					if (statusCode == HttpStatusCode.RequestTimeout || statusCode == HttpStatusCode.ServiceUnavailable || statusCode == HttpStatusCode.GatewayTimeout || statusCode == (HttpStatusCode)429 || statusCode == (HttpStatusCode)509)
					{
						TimeSpan? retryAfter = RetryPolicy.GetRetryAfter(mashupHttpWebResponse);
						if (retryAfter == null)
						{
							return RetryHandlerResult.RetryAfterDefaultDelay;
						}
						return RetryHandlerResult.RetryAfterDelay(retryAfter.Value);
					}
				}
			}
			return RetryHandlerResult.FailWithOriginalException;
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x000FB428 File Offset: 0x000F9628
		public static TimeSpan? GetRetryAfter(MashupHttpWebResponse response)
		{
			double num;
			if (double.TryParse(response.Headers["Retry-After"], out num))
			{
				return new TimeSpan?(TimeSpan.FromSeconds(num));
			}
			return null;
		}

		// Token: 0x0400284C RID: 10316
		public const int MaxTryAttempt = 3;

		// Token: 0x0400284D RID: 10317
		public const int MaxTryAttemptForBusy = 6;

		// Token: 0x0400284E RID: 10318
		public static readonly RetryPolicy NoRetry = new RetryPolicy(1, new Func<Exception, RetryHandlerResult>(RetryPolicy.NoRetryPolicy), null, RetryDelayAlgorithm.ExponentialBackoff, 1);

		// Token: 0x0400284F RID: 10319
		public static readonly RetryPolicy Default = new RetryPolicy(3, new Func<Exception, RetryHandlerResult>(RetryPolicy.IsTransient), null, RetryDelayAlgorithm.ExponentialBackoff, 6);

		// Token: 0x04002850 RID: 10320
		private static readonly TimeSpan MaxClientDrivenBackoff = TimeSpan.FromSeconds(8.0);

		// Token: 0x04002851 RID: 10321
		internal static Action<TimeSpan> OnSleep = new Action<TimeSpan>(Thread.Sleep);

		// Token: 0x04002852 RID: 10322
		private readonly Func<Exception, RetryHandlerResult> retryPolicyHandler;

		// Token: 0x04002853 RID: 10323
		private readonly int maxTries;

		// Token: 0x04002854 RID: 10324
		private readonly int maxTriesForBusy;

		// Token: 0x04002855 RID: 10325
		private readonly int[] nonErrors;

		// Token: 0x04002856 RID: 10326
		private readonly RetryDelayAlgorithm delayAlgorithm;
	}
}
