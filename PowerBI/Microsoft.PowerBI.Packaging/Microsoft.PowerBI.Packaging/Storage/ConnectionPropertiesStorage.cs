using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200003F RID: 63
	[DataContract]
	public sealed class ConnectionPropertiesStorage
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000069CE File Offset: 0x00004BCE
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000069D6 File Offset: 0x00004BD6
		[DataMember(Name = "Name", IsRequired = true, Order = 10)]
		public string Name { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000069DF File Offset: 0x00004BDF
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000069E7 File Offset: 0x00004BE7
		[DataMember(Name = "ConnectionString", IsRequired = true, Order = 20)]
		public string ConnectionString { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000069F0 File Offset: 0x00004BF0
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000069F8 File Offset: 0x00004BF8
		[DataMember(Name = "IsMultiDimensional", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool IsMultiDimensional { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006A01 File Offset: 0x00004C01
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006A09 File Offset: 0x00004C09
		[DataMember(Name = "ConnectionType", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string ConnectionType { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006A12 File Offset: 0x00004C12
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00006A1A File Offset: 0x00004C1A
		[DataMember(Name = "PbiServiceModelId", IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public long? PbiServiceModelId { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006A23 File Offset: 0x00004C23
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00006A2B File Offset: 0x00004C2B
		[DataMember(Name = "PbiServiceGroupId", IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public string PbiServiceGroupId { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006A34 File Offset: 0x00004C34
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00006A3C File Offset: 0x00004C3C
		[DataMember(Name = "PbiModelVirtualServerName", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public string PbiModelVirtualServerName { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006A45 File Offset: 0x00004C45
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00006A4D File Offset: 0x00004C4D
		[DataMember(Name = "PbiModelDatabaseName", IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public string PbiModelDatabaseName { get; set; }
	}
}
