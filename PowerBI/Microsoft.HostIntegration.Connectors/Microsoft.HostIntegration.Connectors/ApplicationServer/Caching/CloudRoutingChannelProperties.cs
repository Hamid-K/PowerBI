using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A6 RID: 678
	[DataContract]
	internal class CloudRoutingChannelProperties : IBinarySerializable
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x0004AEEB File Offset: 0x000490EB
		public CloudRoutingChannelProperties()
		{
			this.RoutingChannelConnectAction = CloudRoutingChannelConnectAction.ConnectAlways;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0004AEFA File Offset: 0x000490FA
		public void InitCloudRoutingChannel(string desiredHost, CloudRoutingChannelConnectAction action)
		{
			this.DestinationHostAddress = desiredHost;
			this.RoutingChannelConnectAction = action;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0004AF0A File Offset: 0x0004910A
		public void InitCloudRoutingChannel(string desiredHost, CloudRoutingChannelConnectAction action, string vipEndpoint)
		{
			this.VipEndpoint = vipEndpoint;
			this.InitCloudRoutingChannel(desiredHost, action);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0004AF1B File Offset: 0x0004911B
		public void ReadStream(ISerializationReader reader)
		{
			this.DestinationHostAddress = reader.ReadString();
			this.VipEndpoint = reader.ReadString();
			this.RoutingChannelConnectAction = (CloudRoutingChannelConnectAction)reader.ReadByte();
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0004AF41 File Offset: 0x00049141
		public void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this.DestinationHostAddress);
			writer.Write(this.VipEndpoint);
			writer.Write((byte)this.RoutingChannelConnectAction);
		}

		// Token: 0x04000D8A RID: 3466
		[DataMember]
		public string DestinationHostAddress;

		// Token: 0x04000D8B RID: 3467
		[DataMember]
		public CloudRoutingChannelConnectAction RoutingChannelConnectAction;

		// Token: 0x04000D8C RID: 3468
		[DataMember]
		public string VipEndpoint;
	}
}
