using System;
using System.Threading;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x0200000F RID: 15
	public static class CancelUtilities
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000033A4 File Offset: 0x000015A4
		public static T RunWithCancelTimer<T>(Func<CancellationToken, T> action, int maximumDurationInMs, Action callbackOnDurationExpiry, CancellationToken externalCancellationToken)
		{
			T t;
			using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
			{
				cancellationTokenSource.CancelAfter(maximumDurationInMs);
				using (CancellationTokenSource cancellationTokenSource2 = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken, cancellationTokenSource.Token))
				{
					try
					{
						t = action(cancellationTokenSource2.Token);
					}
					finally
					{
						if (cancellationTokenSource.IsCancellationRequested)
						{
							callbackOnDurationExpiry();
						}
					}
				}
			}
			return t;
		}
	}
}
