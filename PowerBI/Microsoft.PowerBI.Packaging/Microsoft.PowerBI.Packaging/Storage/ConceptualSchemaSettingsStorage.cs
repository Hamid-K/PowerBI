using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000035 RID: 53
	[DataContract]
	public sealed class ConceptualSchemaSettingsStorage
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000620D File Offset: 0x0000440D
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00006215 File Offset: 0x00004415
		[DisplayName("IncludeFutureArtifacts")]
		[Description("Indicates whether tables will be automatically added to the local model as they're added to the remote data source.")]
		[DataMember(IsRequired = true)]
		public bool IncludeFutureArtifacts { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000621E File Offset: 0x0000441E
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00006226 File Offset: 0x00004426
		[DisplayName("ModelObjectUrnToSerializedRemovedChildren")]
		[Description("A list of remote tables excluded from the local model.")]
		[DataMember(IsRequired = true)]
		public IReadOnlyDictionary<string, string> ModelObjectUrnToSerializedRemovedChildren { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000622F File Offset: 0x0000442F
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00006237 File Offset: 0x00004437
		[DisplayName("ModelObjectUrnToSerializedIncludedChildren")]
		[Description("A list of remote tables included in the local model.")]
		[DataMember(IsRequired = false)]
		public IReadOnlyDictionary<string, string> ModelObjectUrnToSerializedIncludedChildren { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006240 File Offset: 0x00004440
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00006248 File Offset: 0x00004448
		[DisplayName("PerspectiveName")]
		[Description("The name of the perspective used to select which tables will be included from the target model.")]
		[DataMember(IsRequired = true)]
		public string PerspectiveName { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006251 File Offset: 0x00004451
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00006259 File Offset: 0x00004459
		[DisplayName("ModelObjectNameDisambiguationPreferences")]
		[Description("Information used to resolve name conflicts between objects in various source semantic models.")]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ModelObjectNameDisambiguationPreferencesStorage ModelObjectNameDisambiguationPreferences { get; set; }
	}
}
