using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000089 RID: 137
	[DisplayName("ReportDefinition")]
	[Description("The ReportDefinition is stored as definition.pbir. It contains metadata about the overall file structure, holds core settings, and holds a reference to the semantic model used by this report. This file is required.")]
	[DataContract]
	public sealed class ReportDefinition : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					ReportDefinition.CurrentVersion,
					ReportDefinition.DaxQueryViewVersion,
					ReportDefinition.FirstEnhancedReportVersion,
					ReportDefinition.ArtifactDetailsVersion
				};
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000A7DC File Offset: 0x000089DC
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000A7E4 File Offset: 0x000089E4
		public string FileName { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000A7ED File Offset: 0x000089ED
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000A7F5 File Offset: 0x000089F5
		[DisplayName("Version")]
		[Description("Version of the report artifact file format.  This also serves as the version number for the .pbir file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(ReportDefinition.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000A7FE File Offset: 0x000089FE
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000A806 File Offset: 0x00008A06
		[DisplayName("DatasetReference")]
		[Description("The DatasetReference defines the semantic model that is bound to this report. It is required to have exactly one of either the byPath or byConnection properties. The byConnection property would typically be used for LiveConnect scenarios whereas byPath would point at a local semantic model.")]
		[DataMember(Name = "datasetReference", EmitDefaultValue = false, IsRequired = true)]
		public DatasetReference DatasetReference { get; set; }

		// Token: 0x0400020E RID: 526
		private const string DaxQueryViewVersionString = "2.0";

		// Token: 0x0400020F RID: 527
		private const string FirstEnhancedReportVersionString = "3.0";

		// Token: 0x04000210 RID: 528
		private const string ArtifactDetailsVersionString = "4.0";

		// Token: 0x04000211 RID: 529
		public static readonly Version CurrentVersion = new Version(1, 0);

		// Token: 0x04000212 RID: 530
		public static readonly Version DaxQueryViewVersion = new Version("2.0");

		// Token: 0x04000213 RID: 531
		public static readonly Version FirstEnhancedReportVersion = new Version("3.0");

		// Token: 0x04000214 RID: 532
		public static readonly Version ArtifactDetailsVersion = new Version("4.0");

		// Token: 0x02000111 RID: 273
		private enum ArtifactVersions
		{
			// Token: 0x0400049F RID: 1183
			[EnumMember(Value = "1.0")]
			Version1_0,
			// Token: 0x040004A0 RID: 1184
			[EnumMember(Value = "2.0")]
			VersionDaxView,
			// Token: 0x040004A1 RID: 1185
			[EnumMember(Value = "3.0")]
			VersionEnhancedReport,
			// Token: 0x040004A2 RID: 1186
			[EnumMember(Value = "4.0")]
			VersionArtifactDetails
		}
	}
}
