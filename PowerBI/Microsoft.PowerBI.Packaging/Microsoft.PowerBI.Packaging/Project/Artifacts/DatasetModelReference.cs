using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000088 RID: 136
	[DisplayName("DatasetModelReference")]
	[Description("The DatasetModelReference stored as modelReference.json holds a reference to a data model hosted outside Power BI. This file is optional.")]
	[DataContract]
	public sealed class DatasetModelReference : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A745 File Offset: 0x00008945
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					new Version(2, 0)
				};
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000A757 File Offset: 0x00008957
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000A75F File Offset: 0x0000895F
		public string FileName { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000A768 File Offset: 0x00008968
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000A770 File Offset: 0x00008970
		[DisplayName("Version")]
		[Description("Version of the model reference file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(DatasetModelReference.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000A779 File Offset: 0x00008979
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000A781 File Offset: 0x00008981
		[DisplayName("ConnectionString")]
		[Description("The Analysis Services connection string for the target data model.")]
		[DataMember(Name = "connectionString", EmitDefaultValue = false, IsRequired = false)]
		public string ConnectionString { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000A78A File Offset: 0x0000898A
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000A792 File Offset: 0x00008992
		[DisplayName("IsMultiDimentional")]
		[Description("Whether the target data model is an Analysis Services multidimensional cube.  False if the target data model is a tabular model.")]
		[DataMember(Name = "isMultiDimentional", EmitDefaultValue = false, IsRequired = false)]
		public bool IsMultiDimentional { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000A79B File Offset: 0x0000899B
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000A7A3 File Offset: 0x000089A3
		[DisplayName("ConnectionType")]
		[Description("The type of connection. This is for any connection type that is hosted outside of Power BI.")]
		[DataMember(Name = "connectionType", EmitDefaultValue = true, IsRequired = true)]
		public string ConnectionType { get; set; }

		// Token: 0x02000110 RID: 272
		private enum ArtifactVersions
		{
			// Token: 0x0400049D RID: 1181
			[EnumMember(Value = "2.0")]
			Version2_0
		}
	}
}
