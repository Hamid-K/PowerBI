using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200003B RID: 59
	[DataContract]
	public sealed class QueryGroupStorage
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000641B File Offset: 0x0000461B
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00006423 File Offset: 0x00004623
		[DisplayName("Name")]
		[Description("The user visible name of this group.")]
		[DataMember(Name = "name", IsRequired = false, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000642C File Offset: 0x0000462C
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00006434 File Offset: 0x00004634
		[DisplayName("Id")]
		[Description("The identifier used to refer to this group in a query's queryGroupId or group's parentId.")]
		[DataMember(Name = "id", IsRequired = false, EmitDefaultValue = false)]
		public Guid? Id { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000643D File Offset: 0x0000463D
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00006445 File Offset: 0x00004645
		[DisplayName("Order")]
		[Description("The order of the group.")]
		[DataMember(Name = "order", IsRequired = false, EmitDefaultValue = false)]
		public int Order { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000644E File Offset: 0x0000464E
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006456 File Offset: 0x00004656
		[DisplayName("Description")]
		[Description("The description of the group.")]
		[DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
		public string Description { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000645F File Offset: 0x0000465F
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00006467 File Offset: 0x00004667
		[DisplayName("ParentId")]
		[Description("The id of the parent group.")]
		[DataMember(Name = "parentId", IsRequired = false, EmitDefaultValue = false)]
		public Guid? ParentId { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006470 File Offset: 0x00004670
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00006478 File Offset: 0x00004678
		[DisplayName("IsUngroupedQueriesVirtualGroup")]
		[Description("A boolean indicating if this is the Other Queries group.")]
		[DataMember(Name = "isUngroupedQueriesVirtualGroup", IsRequired = false, EmitDefaultValue = false)]
		public bool IsUngroupedQueriesVirtualGroup { get; set; }
	}
}
