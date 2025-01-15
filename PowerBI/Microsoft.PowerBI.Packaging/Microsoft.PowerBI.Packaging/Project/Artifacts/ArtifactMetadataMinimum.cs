using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000081 RID: 129
	[DataContract]
	public class ArtifactMetadataMinimum
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000A4A0 File Offset: 0x000086A0
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000A4A8 File Offset: 0x000086A8
		[DisplayName("Type")]
		[Description("The type of the item either \"report\" or \"dataset\" or \"SemanticModel\".")]
		[DataMember(Name = "type", EmitDefaultValue = false, IsRequired = true)]
		public string Type { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000A4B1 File Offset: 0x000086B1
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000A4B9 File Offset: 0x000086B9
		[DisplayName("DisplayName")]
		[Description("The display name of the item.")]
		[DataMember(Name = "displayName", EmitDefaultValue = false, IsRequired = false)]
		public string DisplayName { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000A4C2 File Offset: 0x000086C2
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000A4CA File Offset: 0x000086CA
		[DisplayName("Description")]
		[Description("The description of the item.")]
		[DataMember(Name = "description", EmitDefaultValue = false, IsRequired = false)]
		public string Description { get; set; }
	}
}
