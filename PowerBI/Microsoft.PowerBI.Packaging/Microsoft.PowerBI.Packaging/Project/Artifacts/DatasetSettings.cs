using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000082 RID: 130
	[DataContract]
	public sealed class DatasetSettings
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000A4DB File Offset: 0x000086DB
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000A4E3 File Offset: 0x000086E3
		[DisplayName("QnaEnabled")]
		[Description("Whether Q&A is enabled for this semantic model.")]
		[DataMember(Name = "qnaEnabled", EmitDefaultValue = false, IsRequired = false)]
		public bool? QnaEnabled { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000A4EC File Offset: 0x000086EC
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000A4F4 File Offset: 0x000086F4
		[DisplayName("QnaLsdlSharingPermissions")]
		[Description("Describes how the linguistic schema (LSDL) can be shared with other users within the same tenant. Allowed values are 0 (LSDL can be shared with users with read permission) and 1 (LSDL can be shared with all users).")]
		[DataMember(Name = "qnaLsdlSharingPermissions", EmitDefaultValue = false, IsRequired = false)]
		[EnumDataType(typeof(DatasetSettings.QnaLsdlSharingPermissionsValues))]
		public int QnaLsdlSharingPermissions { get; set; }

		// Token: 0x0200010C RID: 268
		private enum QnaLsdlSharingPermissionsValues
		{
			// Token: 0x04000490 RID: 1168
			SharedRead,
			// Token: 0x04000491 RID: 1169
			SharedAll
		}
	}
}
