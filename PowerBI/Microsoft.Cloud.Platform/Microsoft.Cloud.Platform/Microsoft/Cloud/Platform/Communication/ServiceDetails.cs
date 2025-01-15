using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B4 RID: 1204
	[DataContract]
	[Serializable]
	public class ServiceDetails
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x00083A73 File Offset: 0x00081C73
		// (set) Token: 0x060024E2 RID: 9442 RVA: 0x00083A7B File Offset: 0x00081C7B
		[DataMember]
		public string Contract { get; private set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x00083A84 File Offset: 0x00081C84
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x00083A8C File Offset: 0x00081C8C
		[DataMember]
		public string ServiceName { get; private set; }

		// Token: 0x060024E5 RID: 9445 RVA: 0x00083A95 File Offset: 0x00081C95
		public ServiceDetails(string contract, string serviceName)
		{
			this.Contract = contract;
			this.ServiceName = serviceName;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00083AAB File Offset: 0x00081CAB
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Service: '{0}'; Contract: '{1}'; ", new object[] { this.ServiceName, this.Contract });
		}
	}
}
