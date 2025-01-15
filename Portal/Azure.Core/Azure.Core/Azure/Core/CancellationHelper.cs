using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000080 RID: 128
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CancellationHelper
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000C604 File Offset: 0x0000A804
		internal static bool ShouldWrapInOperationCanceledException(Exception exception, CancellationToken cancellationToken)
		{
			return !(exception is OperationCanceledException) && cancellationToken.IsCancellationRequested;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000C617 File Offset: 0x0000A817
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken, string message = null)
		{
			return new TaskCanceledException(message ?? CancellationHelper.s_cancellationMessage, innerException);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000C629 File Offset: 0x0000A829
		[NullableContext(2)]
		private static void ThrowOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			throw CancellationHelper.CreateOperationCanceledException(innerException, cancellationToken, null);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000C633 File Offset: 0x0000A833
		internal static void ThrowIfCancellationRequested(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				CancellationHelper.ThrowOperationCanceledException(null, cancellationToken);
			}
		}

		// Token: 0x040001B8 RID: 440
		private static readonly string s_cancellationMessage = new OperationCanceledException().Message;
	}
}
