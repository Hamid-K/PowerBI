using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002AC RID: 684
	public sealed class ThrottledMultiAsyncOperationRunner : MultiAsyncOperationRunner
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x00040654 File Offset: 0x0003E854
		public ThrottledMultiAsyncOperationRunner(IThrottler throttler, bool enableMonitoredExceptionOnThrottlerOverflow = false)
			: this(throttler, Enumerable.Empty<MultiAsyncOperationRunner.AsyncOperation>(), enableMonitoredExceptionOnThrottlerOverflow)
		{
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00040664 File Offset: 0x0003E864
		public ThrottledMultiAsyncOperationRunner([NotNull] IThrottler throttler, [NotNull] IEnumerable<MultiAsyncOperationRunner.AsyncOperation> asyncOperations, bool enableMonitoredExceptionOnThrottlerOverflow = false)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IThrottler>(throttler, "throttler");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<MultiAsyncOperationRunner.AsyncOperation>>(asyncOperations, "asyncOperations");
			this.m_throttler = throttler;
			this.m_enableMonitoredExceptionOnThrottlerOverflow = enableMonitoredExceptionOnThrottlerOverflow;
			foreach (MultiAsyncOperationRunner.AsyncOperation asyncOperation in asyncOperations)
			{
				MultiAsyncOperationRunner.AsyncOperation asyncOperation2 = this.CreateThrottledAsyncOperation(asyncOperation.Name, asyncOperation.BeginMethod, asyncOperation.EndMethod);
				base.AddAsyncOperationInternal(asyncOperation2);
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000406F0 File Offset: 0x0003E8F0
		public override void AddAsyncOperation(string name, Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod)
		{
			MultiAsyncOperationRunner.AsyncOperation asyncOperation = this.CreateThrottledAsyncOperation(name, beginMethod, endMethod);
			base.AddAsyncOperationInternal(asyncOperation);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00040710 File Offset: 0x0003E910
		private MultiAsyncOperationRunner.AsyncOperation CreateThrottledAsyncOperation(string name, Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod)
		{
			ThrottledMultiAsyncOperationRunner.ThrottledOperationFlow throttledOperationFlow = new ThrottledMultiAsyncOperationRunner.ThrottledOperationFlow(this.m_throttler, beginMethod, endMethod, name, this.m_enableMonitoredExceptionOnThrottlerOverflow);
			return new MultiAsyncOperationRunner.AsyncOperation(name, new Func<AsyncCallback, object, IAsyncResult>(throttledOperationFlow.BeginExecute), new Action<IAsyncResult>(throttledOperationFlow.EndExecute));
		}

		// Token: 0x040006DE RID: 1758
		private IThrottler m_throttler;

		// Token: 0x040006DF RID: 1759
		private bool m_enableMonitoredExceptionOnThrottlerOverflow;

		// Token: 0x02000777 RID: 1911
		private class ThrottledOperationFlow : Sequencer
		{
			// Token: 0x06003075 RID: 12405 RVA: 0x000A6673 File Offset: 0x000A4873
			public ThrottledOperationFlow(IThrottler throttler, Func<AsyncCallback, object, IAsyncResult> beginFunction, Action<IAsyncResult> endFunction, string name, bool enableMonitoredExceptionOnThrottlerOverflow = false)
			{
				this.m_throttler = throttler;
				this.m_beginFunction = beginFunction;
				this.m_endFunction = endFunction;
				this.m_name = name;
				this.m_enableMonitoredExceptionOnThrottlerOverflow = enableMonitoredExceptionOnThrottlerOverflow;
			}

			// Token: 0x06003076 RID: 12406 RVA: 0x000A66A0 File Offset: 0x000A48A0
			protected override IEnumerable<IFlowStep> Run()
			{
				IDisposable throttlerHandler = null;
				yield return base.RunAsyncStep<string>("Acquire operation throttler", new Sequencer.AsyncBeginFunction<string>(this.m_throttler.BeginTryAcquireLock), delegate(IAsyncResult ar)
				{
					throttlerHandler = this.m_throttler.EndTryAcquireLock(ar);
				}, this.m_name);
				if (throttlerHandler != null)
				{
					using (throttlerHandler)
					{
						yield return base.RunAsyncStep("Running operation: {0}".FormatWithInvariantCulture(new object[] { this.m_name }), (AsyncCallback callback, object context) => this.m_beginFunction(callback, context), delegate(IAsyncResult ar)
						{
							this.m_endFunction(ar);
						});
					}
					IDisposable disposable = null;
					yield break;
				}
				if (this.m_enableMonitoredExceptionOnThrottlerOverflow)
				{
					throw new ThrottlerOverflowException();
				}
				throw new InvalidOperationException("Throttler overflow");
				yield break;
			}

			// Token: 0x0400161E RID: 5662
			private readonly IThrottler m_throttler;

			// Token: 0x0400161F RID: 5663
			private readonly Func<AsyncCallback, object, IAsyncResult> m_beginFunction;

			// Token: 0x04001620 RID: 5664
			private readonly Action<IAsyncResult> m_endFunction;

			// Token: 0x04001621 RID: 5665
			private readonly string m_name;

			// Token: 0x04001622 RID: 5666
			private readonly bool m_enableMonitoredExceptionOnThrottlerOverflow;
		}
	}
}
