using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x0200007C RID: 124
	[NullableContext(2)]
	internal interface IOperation<T>
	{
		// Token: 0x0600040F RID: 1039
		[return: Nullable(new byte[] { 0, 0, 1 })]
		ValueTask<OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken);
	}
}
