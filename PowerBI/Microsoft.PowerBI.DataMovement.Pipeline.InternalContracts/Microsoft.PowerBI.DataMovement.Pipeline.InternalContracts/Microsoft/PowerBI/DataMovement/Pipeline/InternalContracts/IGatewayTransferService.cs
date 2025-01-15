using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[ServiceContract(Namespace = "urn:ps", SessionMode = SessionMode.Required, CallbackContract = typeof(IGatewayTransferCallback))]
	public interface IGatewayTransferService
	{
		// Token: 0x06000008 RID: 8
		[OperationContract(IsOneWay = true)]
		Task PingAsync();

		// Token: 0x06000009 RID: 9
		[OperationContract(IsOneWay = true)]
		Task TransferAsync(byte[] packet);
	}
}
