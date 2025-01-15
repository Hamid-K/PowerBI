using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000119 RID: 281
	[DataContract]
	internal sealed class ItemSourceLocation
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 10)]
		internal int WrapperLineStart { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000F7EA File Offset: 0x0000D9EA
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0000F7F2 File Offset: 0x0000D9F2
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 20)]
		internal int SourceLineStart { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0000F7FB File Offset: 0x0000D9FB
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0000F803 File Offset: 0x0000DA03
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 30)]
		internal int SourceLineEnd { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0000F80C File Offset: 0x0000DA0C
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0000F814 File Offset: 0x0000DA14
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 40)]
		internal int WrapperLineEnd { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0000F81D File Offset: 0x0000DA1D
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x0000F825 File Offset: 0x0000DA25
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 50)]
		internal int SourceIndent { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0000F82E File Offset: 0x0000DA2E
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0000F836 File Offset: 0x0000DA36
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 60)]
		internal string QuerySourceName { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0000F83F File Offset: 0x0000DA3F
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0000F847 File Offset: 0x0000DA47
		[DataMember(EmitDefaultValue = true, IsRequired = true, Order = 70)]
		internal ItemSourceType SourceType { get; set; }
	}
}
