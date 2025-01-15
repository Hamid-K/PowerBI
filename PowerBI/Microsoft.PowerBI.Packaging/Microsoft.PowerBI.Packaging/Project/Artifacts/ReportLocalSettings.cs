using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200008D RID: 141
	[DisplayName("ReportLocalSettings")]
	[Description("The ReportLocalSettings stored as localSettings.json holds report settings that only apply for the current user/machine. This file is optional.")]
	[DataContract]
	public sealed class ReportLocalSettings : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000A903 File Offset: 0x00008B03
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000A915 File Offset: 0x00008B15
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000A91D File Offset: 0x00008B1D
		public string FileName { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000A926 File Offset: 0x00008B26
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000A92E File Offset: 0x00008B2E
		[DisplayName("Version")]
		[Description("Version of the local settings file format.")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(ReportLocalSettings.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000A937 File Offset: 0x00008B37
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000A93F File Offset: 0x00008B3F
		[DisplayName("RemoteArtifacts")]
		[Description("Describes the published Power BI artifacts associated with this definition.")]
		[DataMember(Name = "remoteArtifacts", EmitDefaultValue = false, IsRequired = false)]
		public NonNulls<ReportRemoteArtifact> RemoteArtifacts { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000A948 File Offset: 0x00008B48
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000A950 File Offset: 0x00008B50
		[DisplayName("SecurityBindingsSignature")]
		[Description("A base64 encoded signature which when absent or invalid will reset saved properties which require user review or consent.")]
		[DataMember(Name = "securityBindingsSignature", EmitDefaultValue = false, IsRequired = false)]
		public byte[] SecurityBindingsSignature { get; set; }

		// Token: 0x02000112 RID: 274
		private enum ArtifactVersions
		{
			// Token: 0x040004A4 RID: 1188
			[EnumMember(Value = "1.0")]
			Version1_0
		}
	}
}
