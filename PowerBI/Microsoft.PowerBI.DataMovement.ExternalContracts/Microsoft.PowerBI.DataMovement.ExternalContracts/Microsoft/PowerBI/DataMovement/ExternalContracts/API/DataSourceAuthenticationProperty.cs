using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200003A RID: 58
	[DataContract]
	public class DataSourceAuthenticationProperty
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000341C File Offset: 0x0000161C
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003424 File Offset: 0x00001624
		[DataMember(Name = "name", Order = 0)]
		public string Name { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000342D File Offset: 0x0000162D
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00003435 File Offset: 0x00001635
		[DataMember(Name = "label", Order = 10)]
		public string Label { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000343E File Offset: 0x0000163E
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00003446 File Offset: 0x00001646
		[DataMember(Name = "isRequired", Order = 20)]
		public bool IsRequired { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000344F File Offset: 0x0000164F
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00003457 File Offset: 0x00001657
		[DataMember(Name = "isSecret", Order = 30)]
		public bool IsSecret { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00003468 File Offset: 0x00001668
		[DataMember(Name = "propertyType", Order = 40)]
		public string PropertyType { get; set; }
	}
}
