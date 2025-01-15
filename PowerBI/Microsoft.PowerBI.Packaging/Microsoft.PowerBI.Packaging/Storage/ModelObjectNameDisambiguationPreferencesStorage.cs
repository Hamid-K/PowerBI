using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000038 RID: 56
	[DataContract]
	public class ModelObjectNameDisambiguationPreferencesStorage
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000626A File Offset: 0x0000446A
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00006272 File Offset: 0x00004472
		[DisplayName("Text")]
		[Description("The string appended to model object names to differentiate them.")]
		[DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
		public string Text { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000627B File Offset: 0x0000447B
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00006283 File Offset: 0x00004483
		[DisplayName("IsPrefix")]
		[Description("Determines location of the appended disambiguation text, either prefix or suffix.")]
		[DataMember(Name = "isPrefix", IsRequired = true, EmitDefaultValue = true)]
		public bool IsPrefix { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000628C File Offset: 0x0000448C
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00006294 File Offset: 0x00004494
		[DisplayName("TargetedModelObjects")]
		[Description("Specifies which model objects are subject to disambiguation - tables, measures, or both.")]
		[DataMember(Name = "targetedModelObjects", IsRequired = true, EmitDefaultValue = false)]
		[EnumDataType(typeof(TargetedModelObjectsStorage))]
		public string TargetedModelObjects { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000629D File Offset: 0x0000449D
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000062A5 File Offset: 0x000044A5
		[DisplayName("Mode")]
		[Description("Specifies how the disambiguation preferences are applied, either 'Always' or 'ConflictOnly'")]
		[DataMember(Name = "mode", IsRequired = true, EmitDefaultValue = false)]
		[EnumDataType(typeof(ModelObjectNameDisambiguationModeStorage))]
		public string Mode { get; set; }
	}
}
