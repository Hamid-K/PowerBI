using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Query.Contracts.DaxCapabilities
{
	// Token: 0x0200000D RID: 13
	[DataContract(Name = "DaxFunctionParameter")]
	public sealed class DaxFunctionParameter
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000026BD File Offset: 0x000008BD
		[DataMember(IsRequired = true, Order = 10)]
		public string Name { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000026C6 File Offset: 0x000008C6
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000026CE File Offset: 0x000008CE
		[DataMember(IsRequired = false, Order = 20, EmitDefaultValue = false)]
		public string Description { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000026D7 File Offset: 0x000008D7
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000026DF File Offset: 0x000008DF
		[DataMember(IsRequired = false, Order = 30)]
		public bool Optional { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000026F0 File Offset: 0x000008F0
		[DataMember(IsRequired = false, Order = 40)]
		public bool Repeatable { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000026F9 File Offset: 0x000008F9
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002701 File Offset: 0x00000901
		[DataMember(IsRequired = false, Order = 50, EmitDefaultValue = false)]
		public int RepeatGroup { get; set; }
	}
}
