using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts
{
	// Token: 0x0200000A RID: 10
	public interface IGatewayTransferServiceChannel : IGatewayTransferService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
	}
}
