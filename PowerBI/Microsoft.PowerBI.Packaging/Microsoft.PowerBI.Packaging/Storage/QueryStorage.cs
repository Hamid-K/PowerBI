using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200003A RID: 58
	[DataContract]
	public sealed class QueryStorage
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006336 File Offset: 0x00004536
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000633E File Offset: 0x0000453E
		[DisplayName("Name")]
		[Description("The name of the query.")]
		[DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
		[JsonProperty(Required = Required.Always)]
		public string Name { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006347 File Offset: 0x00004547
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000634F File Offset: 0x0000454F
		[DisplayName("ModelLineageTag")]
		[Description("An optional tag that can be used to define the lineage of an expression across different versions of a model.")]
		[DataMember(Name = "lineageTag", IsRequired = false, EmitDefaultValue = false)]
		public string ModelLineageTag { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006358 File Offset: 0x00004558
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006360 File Offset: 0x00004560
		[DisplayName("Description")]
		[Description("A description of the query.")]
		[DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
		public string Description { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006369 File Offset: 0x00004569
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00006371 File Offset: 0x00004571
		[DisplayName("QueryGroupId")]
		[Description("The GUID of the query group that the query belongs to.")]
		[DataMember(Name = "queryGroupId", IsRequired = false, EmitDefaultValue = false)]
		public Guid? QueryGroupId { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000637A File Offset: 0x0000457A
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00006382 File Offset: 0x00004582
		[DisplayName("NavigationStepName")]
		[Description("The localized name of the navigation step from Power Query.")]
		[DataMember(Name = "navigationStepName", IsRequired = false, EmitDefaultValue = false)]
		public string NavigationStepName { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000638B File Offset: 0x0000458B
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00006393 File Offset: 0x00004593
		[DisplayName("RefreshWhenRefreshingAll")]
		[Description("A boolean indicating if this query should be refreshed when refreshing all queries.")]
		[DataMember(Name = "refreshWhenRefreshingAll", IsRequired = false, EmitDefaultValue = false)]
		public bool? RefreshWhenRefreshingAll { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000639C File Offset: 0x0000459C
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000063A4 File Offset: 0x000045A4
		[DisplayName("Text")]
		[Description("The text of the M expression for this query. Each element of the array is a line of query text. The full query text is produced by concatenating each array element, separated by a newline character.")]
		[DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
		[JsonProperty(Required = Required.Always)]
		public string[] Text { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000063AD File Offset: 0x000045AD
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000063B5 File Offset: 0x000045B5
		[DisplayName("IsDirectQuery")]
		[Description("A boolean indicating if this query should use DirectQuery when applied to the data model.")]
		[DataMember(Name = "isDirectQuery", IsRequired = false, EmitDefaultValue = false)]
		public bool? IsDirectQuery { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000063BE File Offset: 0x000045BE
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000063C6 File Offset: 0x000045C6
		[DisplayName("LastLoadedAsTableFormulaText")]
		[Description("This property is only used in PBIX files that have not been updated to the enhanced metadata format.")]
		[DataMember(Name = "lastLoadedAsTableFormulaText", IsRequired = false, EmitDefaultValue = false)]
		public string LastLoadedAsTableFormulaText { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000063CF File Offset: 0x000045CF
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000063D7 File Offset: 0x000045D7
		[DisplayName("LoadedAsTable")]
		[Description("This property is only used in PBIX files that have not been updated to the enhanced metadata format.")]
		[DataMember(Name = "loadedAsTable", IsRequired = false, EmitDefaultValue = false)]
		public bool? LoadedAsTable { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000063E0 File Offset: 0x000045E0
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000063E8 File Offset: 0x000045E8
		[DisplayName("LoadAsTableDisabled")]
		[Description("A boolean indicating if the query should be loaded as a table.")]
		[DataMember(Name = "loadAsTableDisabled", IsRequired = false, EmitDefaultValue = false)]
		public bool? LoadAsTableDisabled { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000063F1 File Offset: 0x000045F1
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000063F9 File Offset: 0x000045F9
		[DisplayName("ResultType")]
		[Description("The mashup object type that results from the M expression. Some examples would be Table, List, or Number.")]
		[DataMember(Name = "resultType", IsRequired = false, EmitDefaultValue = false)]
		public string ResultType { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006402 File Offset: 0x00004602
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000640A File Offset: 0x0000460A
		[DisplayName("IsHidden")]
		[Description("A boolean indicating if the query is a multi-table source. This property is managed by Power BI Desktop and should never be updated manually.")]
		[DataMember(Name = "isHidden", IsRequired = false, EmitDefaultValue = false)]
		public bool? IsHidden { get; set; }
	}
}
