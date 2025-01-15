using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200010B RID: 267
	[DataContract]
	public sealed class DataShapeMessage
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0000EF10 File Offset: 0x0000D110
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0000EF18 File Offset: 0x0000D118
		[DataMember(Name = "Code", IsRequired = true, EmitDefaultValue = false, Order = 0)]
		public string Code { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0000EF21 File Offset: 0x0000D121
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0000EF29 File Offset: 0x0000D129
		[DataMember(Name = "Severity", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string Severity { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0000EF32 File Offset: 0x0000D132
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0000EF3A File Offset: 0x0000D13A
		[DataMember(Name = "Message", IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public string Message { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0000EF43 File Offset: 0x0000D143
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0000EF4B File Offset: 0x0000D14B
		[DataMember(Name = "ObjectType", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string ObjectType { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000EF54 File Offset: 0x0000D154
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0000EF5C File Offset: 0x0000D15C
		[DataMember(Name = "ObjectName", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string ObjectName { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0000EF65 File Offset: 0x0000D165
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0000EF6D File Offset: 0x0000D16D
		[DataMember(Name = "PropertyName", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string PropertyName { get; set; }
	}
}
