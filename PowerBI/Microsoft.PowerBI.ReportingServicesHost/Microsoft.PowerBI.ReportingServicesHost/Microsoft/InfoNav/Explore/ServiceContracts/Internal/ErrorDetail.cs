using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000A RID: 10
	[DataContract]
	public sealed class ErrorDetail
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000021FF File Offset: 0x000003FF
		public ErrorDetail(string nameCode, ErrorDetailValue value)
		{
			this.NameCode = nameCode;
			this.Value = value;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002215 File Offset: 0x00000415
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000221D File Offset: 0x0000041D
		[DataMember(IsRequired = true, Order = 10, Name = "code")]
		public string NameCode { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002226 File Offset: 0x00000426
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000222E File Offset: 0x0000042E
		[DataMember(IsRequired = false, Order = 20, Name = "detail", EmitDefaultValue = false)]
		public ErrorDetailValue Value { get; private set; }
	}
}
