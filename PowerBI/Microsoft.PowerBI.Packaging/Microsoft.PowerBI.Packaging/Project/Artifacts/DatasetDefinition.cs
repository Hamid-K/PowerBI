using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000083 RID: 131
	[DisplayName("DatasetDefinition")]
	[Description("The DatasetDefinition stored as definition.pbism holds information about the overall semantic model definition. This file is required.")]
	[DataContract]
	public sealed class DatasetDefinition : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000A505 File Offset: 0x00008705
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					DatasetDefinition.CurrentVersion,
					DatasetDefinition.DaxQueryViewVersion,
					DatasetDefinition.FirstTmdlVersion,
					DatasetDefinition.ArtifactDetailsVersion
				};
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000A52D File Offset: 0x0000872D
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000A535 File Offset: 0x00008735
		public string FileName { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000A53E File Offset: 0x0000873E
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000A546 File Offset: 0x00008746
		[DisplayName("Version")]
		[Description("Version of the semantic model item file format. This also serves as the version number for the .pbism file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(DatasetDefinition.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000A54F File Offset: 0x0000874F
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000A557 File Offset: 0x00008757
		[DisplayName("Settings")]
		[Description("Settings for this semantic model that do not impact the behavior of the data model itself. Instead, these settings control the behavior of other Power BI features associated with the semantic model.")]
		[DataMember(Name = "settings", EmitDefaultValue = false, IsRequired = false)]
		public DatasetSettings Settings { get; set; }

		// Token: 0x040001EB RID: 491
		private const string DaxQueryViewVersionString = "2.0";

		// Token: 0x040001EC RID: 492
		private const string FirstTmdlVersionString = "3.0";

		// Token: 0x040001ED RID: 493
		private const string ArtifactDetailsVersionString = "4.0";

		// Token: 0x040001EE RID: 494
		public static readonly Version CurrentVersion = new Version(1, 0);

		// Token: 0x040001EF RID: 495
		public static readonly Version DaxQueryViewVersion = new Version("2.0");

		// Token: 0x040001F0 RID: 496
		public static readonly Version FirstTmdlVersion = new Version("3.0");

		// Token: 0x040001F1 RID: 497
		public static readonly Version ArtifactDetailsVersion = new Version("4.0");

		// Token: 0x0200010D RID: 269
		private enum ArtifactVersions
		{
			// Token: 0x04000493 RID: 1171
			[EnumMember(Value = "1.0")]
			Version1_0,
			// Token: 0x04000494 RID: 1172
			[EnumMember(Value = "2.0")]
			VersionDaxView,
			// Token: 0x04000495 RID: 1173
			[EnumMember(Value = "3.0")]
			VersionTmdl,
			// Token: 0x04000496 RID: 1174
			[EnumMember(Value = "4.0")]
			VersionArtifactDetails
		}
	}
}
