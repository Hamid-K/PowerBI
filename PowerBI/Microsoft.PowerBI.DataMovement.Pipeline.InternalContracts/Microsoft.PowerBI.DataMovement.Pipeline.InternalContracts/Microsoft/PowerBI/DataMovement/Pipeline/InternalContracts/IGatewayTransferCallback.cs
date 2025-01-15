using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	public interface IGatewayTransferCallback
	{
		// Token: 0x0600000A RID: 10
		[OperationContract(IsOneWay = true)]
		Task TransferCallbackAsync(byte[] packet);
	}
}
