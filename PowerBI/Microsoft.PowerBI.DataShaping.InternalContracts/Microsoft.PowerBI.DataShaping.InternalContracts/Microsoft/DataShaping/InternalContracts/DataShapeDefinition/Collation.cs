using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000109 RID: 265
	[DataContract]
	internal sealed class Collation
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000F47D File Offset: 0x0000D67D
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000F485 File Offset: 0x0000D685
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Culture { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0000F48E File Offset: 0x0000D68E
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0000F496 File Offset: 0x0000D696
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal bool IgnoreCase { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0000F49F File Offset: 0x0000D69F
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0000F4A7 File Offset: 0x0000D6A7
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal bool IgnoreNonSpace { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal bool IgnoreKanaType { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0000F4C9 File Offset: 0x0000D6C9
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal bool IgnoreWidth { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0000F4D2 File Offset: 0x0000D6D2
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0000F4DA File Offset: 0x0000D6DA
		[DataMember(EmitDefaultValue = false, Order = 6)]
		internal bool PreferOrdinalStringEquality { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0000F4E3 File Offset: 0x0000D6E3
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0000F4EB File Offset: 0x0000D6EB
		[DataMember(EmitDefaultValue = false, Order = 7)]
		internal bool UseOrdinalStringKeyGeneration { get; set; }
	}
}
