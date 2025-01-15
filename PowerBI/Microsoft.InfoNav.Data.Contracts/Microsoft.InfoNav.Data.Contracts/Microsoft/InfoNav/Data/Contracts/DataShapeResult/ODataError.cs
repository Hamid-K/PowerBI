using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200011B RID: 283
	[DataContract]
	public sealed class ODataError
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000F62E File Offset: 0x0000D82E
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0000F636 File Offset: 0x0000D836
		[DataMember(Name = "code", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string Code { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0000F63F File Offset: 0x0000D83F
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0000F647 File Offset: 0x0000D847
		[DataMember(Name = "source", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Source { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0000F650 File Offset: 0x0000D850
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0000F658 File Offset: 0x0000D858
		[DataMember(Name = "message", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public ErrorMessage Message { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0000F661 File Offset: 0x0000D861
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0000F669 File Offset: 0x0000D869
		[DataMember(Name = "azure:values", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public AzureValue[] AzureValues { get; set; }
	}
}
