using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000087 RID: 135
	[DataContract]
	public sealed class DatasetUserConsent
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000A70A File Offset: 0x0000890A
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000A712 File Offset: 0x00008912
		[DisplayName("CompositeModel")]
		[Description("Whether the user has consented to the use of models with multiple direct query data sources.")]
		[DataMember(Name = "compositeModel", EmitDefaultValue = false, IsRequired = false)]
		public bool CompositeModels { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000A71B File Offset: 0x0000891B
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000A723 File Offset: 0x00008923
		[DisplayName("QnAForLiveConnect")]
		[Description("Whether the user has consented to the use of Q&A for live connections to models hosted outside Power BI.")]
		[DataMember(Name = "qnAForLiveConnect", EmitDefaultValue = false, IsRequired = false)]
		public bool QnAForLiveConnect { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000A72C File Offset: 0x0000892C
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000A734 File Offset: 0x00008934
		[DisplayName("DynamicQueryParameters")]
		[Description("Whether the user has consented to the use of dynamic M query parameters in their report.")]
		[DataMember(Name = "dynamicQueryParameters", EmitDefaultValue = false, IsRequired = false)]
		public bool DynamicQueryParameters { get; set; }
	}
}
