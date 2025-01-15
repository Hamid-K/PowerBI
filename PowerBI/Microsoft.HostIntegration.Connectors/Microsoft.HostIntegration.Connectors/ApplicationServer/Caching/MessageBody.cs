using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200033F RID: 831
	[DataContract(Name = "MessageBody", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class MessageBody
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x00059C43 File Offset: 0x00057E43
		internal int ID
		{
			get
			{
				if (this.ServiceReqId == -1)
				{
					return this.ClientReqId;
				}
				return this.ServiceReqId;
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00059C5B File Offset: 0x00057E5B
		internal void ReadStream(ISerializationReader reader)
		{
			this.ClientReqId = reader.ReadInt32();
			this.ServiceReqId = reader.ReadInt32();
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00059C75 File Offset: 0x00057E75
		internal void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this.ClientReqId);
			writer.Write(this.ServiceReqId);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00059C8F File Offset: 0x00057E8F
		public int GetSerializedSize()
		{
			return 8;
		}

		// Token: 0x0400107E RID: 4222
		[DataMember]
		public int ClientReqId = -1;

		// Token: 0x0400107F RID: 4223
		[DataMember]
		public int ServiceReqId = -1;

		// Token: 0x04001080 RID: 4224
		[DataMember]
		public string UniqueTrackingId = "";
	}
}
