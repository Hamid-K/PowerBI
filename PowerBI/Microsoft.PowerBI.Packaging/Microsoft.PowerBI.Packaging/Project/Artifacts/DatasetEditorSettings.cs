using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000084 RID: 132
	[DisplayName("EditorSettings")]
	[Description("The EditorSettings stored as editorSettings.json holds semantic model editor settings that are saved as part of the semantic model definition for use across users or environments. This file is optional.")]
	[DataContract]
	public sealed class DatasetEditorSettings : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000A5A3 File Offset: 0x000087A3
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					new Version(1, 0)
				};
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000A5B5 File Offset: 0x000087B5
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000A5BD File Offset: 0x000087BD
		public string FileName { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000A5C6 File Offset: 0x000087C6
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000A5CE File Offset: 0x000087CE
		[DisplayName("Version")]
		[Description("Version of the semantic model editor settings file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(DatasetEditorSettings.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000A5D7 File Offset: 0x000087D7
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000A5DF File Offset: 0x000087DF
		[DisplayName("ShowHiddenFields")]
		[Description("Whether hidden fields should be shown in a field list.")]
		[DataMember(Name = "showHiddenFields", EmitDefaultValue = false, IsRequired = false)]
		public bool ShowHiddenFields { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000A5E8 File Offset: 0x000087E8
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000A5F0 File Offset: 0x000087F0
		[DisplayName("AutodetectRelationships")]
		[Description("Whether relationships should be automatically detected when adding tables.")]
		[DataMember(Name = "autodetectRelationships", EmitDefaultValue = false, IsRequired = false)]
		public bool AutodetectRelationships { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000A5F9 File Offset: 0x000087F9
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000A601 File Offset: 0x00008801
		[DisplayName("ParallelQueryLoading")]
		[Description("Whether multiple M queries should be run in parallel.")]
		[DataMember(Name = "parallelQueryLoading", EmitDefaultValue = false, IsRequired = false)]
		public bool ParallelQueryLoading { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000A60A File Offset: 0x0000880A
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000A612 File Offset: 0x00008812
		[DisplayName("TypeDetectionEnabled")]
		[Description("Whether to detect column types and headers for unstructured data sources.")]
		[DataMember(Name = "typeDetectionEnabled", EmitDefaultValue = false, IsRequired = false)]
		public bool TypeDetectionEnabled { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000A61B File Offset: 0x0000881B
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000A623 File Offset: 0x00008823
		[DisplayName("RelationshipImportEnabled")]
		[Description("Whether relationships should be imported from data sources during the first load of data.")]
		[DataMember(Name = "relationshipImportEnabled", EmitDefaultValue = false, IsRequired = false)]
		public bool RelationshipImportEnabled { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000A62C File Offset: 0x0000882C
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000A634 File Offset: 0x00008834
		[DisplayName("RelationshipRefreshEnabled")]
		[Description("Whether relationships should be updated or deleted when refreshing data.")]
		[DataMember(Name = "relationshipRefreshEnabled", EmitDefaultValue = false, IsRequired = false)]
		public bool RelationshipRefreshEnabled { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000A63D File Offset: 0x0000883D
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000A645 File Offset: 0x00008845
		[DisplayName("RunBackgroundAnalysis")]
		[Description("Whether to load data previews for the query editor in the background.")]
		[DataMember(Name = "runBackgroundAnalysis", EmitDefaultValue = false, IsRequired = false)]
		public bool RunBackgroundAnalysis { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000A64E File Offset: 0x0000884E
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000A656 File Offset: 0x00008856
		[DisplayName("ShouldNotifyUserOfNameConflictResolution")]
		[Description("Whether we should alert the user when we automatically update a user's table and/or measure names.")]
		[DataMember(Name = "shouldNotifyUserOfNameConflictResolution", EmitDefaultValue = false, IsRequired = false)]
		public bool ShouldNotifyUserOfNameConflictResolution { get; set; }

		// Token: 0x0200010E RID: 270
		private enum ArtifactVersions
		{
			// Token: 0x04000498 RID: 1176
			[EnumMember(Value = "1.0")]
			Version1_0
		}
	}
}
