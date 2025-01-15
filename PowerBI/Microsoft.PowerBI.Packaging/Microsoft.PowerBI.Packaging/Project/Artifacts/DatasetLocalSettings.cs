using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000085 RID: 133
	[DisplayName("LocalSettings")]
	[Description("The LocalSettings stored as localSettings.json holds semantic model settings that only apply to the current user/machine. This file is optional.")]
	[DataContract]
	public sealed class DatasetLocalSettings : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000A667 File Offset: 0x00008867
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					new Version(1, 0),
					new Version(1, 1)
				};
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000A683 File Offset: 0x00008883
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000A68B File Offset: 0x0000888B
		public string FileName { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000A694 File Offset: 0x00008894
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000A69C File Offset: 0x0000889C
		[DisplayName("Version")]
		[Description("Version of the local settings file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(DatasetLocalSettings.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000A6A5 File Offset: 0x000088A5
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000A6AD File Offset: 0x000088AD
		[DisplayName("UserConsent")]
		[Description("Holds information about whether the user has agreed to the use of various features.")]
		[DataMember(Name = "userConsent", EmitDefaultValue = false, IsRequired = false)]
		public DatasetUserConsent UserConsent { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000A6B6 File Offset: 0x000088B6
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000A6BE File Offset: 0x000088BE
		[DisplayName("RemoteArtifacts")]
		[Description("Describes the published Power BI artifacts associated with this definition.")]
		[DataMember(Name = "remoteArtifacts", EmitDefaultValue = false, IsRequired = false)]
		public NonNulls<DatasetRemoteArtifact> RemoteArtifacts { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000A6C7 File Offset: 0x000088C7
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000A6CF File Offset: 0x000088CF
		[DisplayName("SecurityBindingsSignature")]
		[Description("A base64 encoded signature which when absent or invalid will reset saved properties which require user review or consent.")]
		[DataMember(Name = "securityBindingsSignature", EmitDefaultValue = false, IsRequired = false)]
		public byte[] SecurityBindingsSignature { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000A6D8 File Offset: 0x000088D8
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000A6E0 File Offset: 0x000088E0
		[DisplayName("RemoteModelingObjectId")]
		[Description("The object ID of the data model in the Power BI service for remote modeling.")]
		[DataMember(Name = "remoteModelingObjectId", EmitDefaultValue = false, IsRequired = false)]
		public string RemoteModelingObjectId { get; set; }

		// Token: 0x0200010F RID: 271
		private enum ArtifactVersions
		{
			// Token: 0x0400049A RID: 1178
			[EnumMember(Value = "1.0")]
			Version1_0,
			// Token: 0x0400049B RID: 1179
			[EnumMember(Value = "1.1")]
			Version1_1
		}
	}
}
