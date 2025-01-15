using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000079 RID: 121
	internal interface IOperation
	{
		// Token: 0x060003F7 RID: 1015
		ValueTask<OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken);
	}
}
