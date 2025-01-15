using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200006E RID: 110
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class PowerBIErrorDetail
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00004783 File Offset: 0x00002983
		public PowerBIErrorDetail(string nameCode, PowerBIErrorDetailValue value)
		{
			this.NameCode = nameCode;
			this.Value = value;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00004799 File Offset: 0x00002999
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000047A1 File Offset: 0x000029A1
		[DataMember(IsRequired = true, Order = 10, Name = "code")]
		public string NameCode { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000047AA File Offset: 0x000029AA
		// (set) Token: 0x06000327 RID: 807 RVA: 0x000047B2 File Offset: 0x000029B2
		[DataMember(IsRequired = false, Order = 20, Name = "detail", EmitDefaultValue = false)]
		public PowerBIErrorDetailValue Value { get; private set; }
	}
}
