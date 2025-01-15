using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200004D RID: 77
	[DataContract]
	public sealed class DatabaseLicensingData
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
		public DatabaseLicensingData(bool isLicenseRequired, byte[] additionalData)
		{
			this.LicenseRequired = isLicenseRequired;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		[DataMember]
		public bool LicenseRequired { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
		[DataMember]
		public byte[] AdditionalData { get; private set; }
	}
}
