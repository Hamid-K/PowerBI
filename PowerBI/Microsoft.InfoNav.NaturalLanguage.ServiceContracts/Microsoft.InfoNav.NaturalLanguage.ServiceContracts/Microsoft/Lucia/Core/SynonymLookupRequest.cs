using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E8 RID: 232
	[DataContract(Name = "SynonymLookupRequest", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public sealed class SynonymLookupRequest
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000875A File Offset: 0x0000695A
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00008762 File Offset: 0x00006962
		[DataMember(IsRequired = true, Order = 1)]
		public LanguageIdentifier Language { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000876B File Offset: 0x0000696B
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00008773 File Offset: 0x00006973
		[DataMember(IsRequired = true, Order = 2)]
		public string[] Tokens { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000877C File Offset: 0x0000697C
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00008784 File Offset: 0x00006984
		[DataMember(IsRequired = true, Order = 3)]
		public SynonymLookupSearchDefinition[] SearchDefinitions { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000878D File Offset: 0x0000698D
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00008795 File Offset: 0x00006995
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public SynonymLookupContext Context { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000879E File Offset: 0x0000699E
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x000087A6 File Offset: 0x000069A6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public SynonymLookupRequestOptions RequestOptions { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000087AF File Offset: 0x000069AF
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x000087B7 File Offset: 0x000069B7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public int? MatchCountPerSearchDefinition { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000087C0 File Offset: 0x000069C0
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000087C8 File Offset: 0x000069C8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 7)]
		public string TokenSeparator { get; set; }
	}
}
