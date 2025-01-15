using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200038B RID: 907
	[DataContract(Name = "ServerInformation", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class ServerInformation
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00061017 File Offset: 0x0005F217
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x0006101F File Offset: 0x0005F21F
		[DataMember]
		public Uri InstanceAddressInternal
		{
			get
			{
				return this._instanceAddressInternalEndPoint;
			}
			set
			{
				this._instanceAddressInternalEndPoint = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00061028 File Offset: 0x0005F228
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00061030 File Offset: 0x0005F230
		public Uri InstanceAddressInputEndpoint
		{
			get
			{
				return this._instanceAddressInputEndpoint;
			}
			set
			{
				this._instanceAddressInputEndpoint = value;
			}
		}

		// Token: 0x040012E4 RID: 4836
		private Uri _instanceAddressInternalEndPoint;

		// Token: 0x040012E5 RID: 4837
		private Uri _instanceAddressInputEndpoint;
	}
}
