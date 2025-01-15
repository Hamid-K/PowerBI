using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000323 RID: 803
	[DataContract]
	internal class HeaderBody
	{
		// Token: 0x0400102D RID: 4141
		[DataMember]
		public int ClientReqId = -1;

		// Token: 0x0400102E RID: 4142
		[DataMember]
		public int ServiceReqId = -1;

		// Token: 0x0400102F RID: 4143
		[DataMember]
		public ReqType RequestType;

		// Token: 0x04001030 RID: 4144
		[DataMember]
		public ForwardingType ForwardingType;

		// Token: 0x04001031 RID: 4145
		[DataMember]
		public string RegionName;

		// Token: 0x04001032 RID: 4146
		[DataMember]
		public string CacheName;

		// Token: 0x04001033 RID: 4147
		[DataMember]
		public Key Key;
	}
}
