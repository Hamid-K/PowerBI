using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x0200000B RID: 11
	[DataContract]
	internal class CalculationSchemaInfo
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000260D File Offset: 0x0000080D
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002615 File Offset: 0x00000815
		[DataMember(Name = "N", EmitDefaultValue = false, Order = 10)]
		public string Id { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000261E File Offset: 0x0000081E
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002626 File Offset: 0x00000826
		[DataMember(Name = "DN", EmitDefaultValue = false, Order = 20)]
		public string DictionaryId { get; set; }
	}
}
