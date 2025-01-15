using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	public sealed class ConnectionsSettingsStorage
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006B55 File Offset: 0x00004D55
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00006B5D File Offset: 0x00004D5D
		[DataMember(Name = "Version", IsRequired = true, Order = 10)]
		public int Version { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00006B66 File Offset: 0x00004D66
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00006B6E File Offset: 0x00004D6E
		[DataMember(Name = "Connections", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public List<ConnectionPropertiesStorage> Connections { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006B77 File Offset: 0x00004D77
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00006B7F File Offset: 0x00004D7F
		[DataMember(Name = "RemoteArtifacts", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public List<RemoteArtifactPropertiesStorage> RemoteArtifacts { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006B88 File Offset: 0x00004D88
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00006B90 File Offset: 0x00004D90
		[DataMember(Name = "OriginalWorkspaceObjectId", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string OriginalWorkspaceObjectId { get; set; }
	}
}
