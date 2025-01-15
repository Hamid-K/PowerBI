using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000261 RID: 609
	[DataContract]
	internal class NotificationRequest
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x00040090 File Offset: 0x0003E290
		internal NotificationRequest(IPartitionRequest[] list)
		{
			this._partitionReqList = list;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0004009F File Offset: 0x0003E29F
		internal IPartitionRequest[] PartitionReqList
		{
			get
			{
				return this._partitionReqList;
			}
		}

		// Token: 0x04000C39 RID: 3129
		[DataMember]
		private IPartitionRequest[] _partitionReqList;
	}
}
