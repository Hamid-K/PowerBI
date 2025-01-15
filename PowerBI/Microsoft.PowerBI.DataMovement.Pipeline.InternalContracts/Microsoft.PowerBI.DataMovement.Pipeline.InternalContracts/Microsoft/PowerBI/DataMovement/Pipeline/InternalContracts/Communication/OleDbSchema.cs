using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200004F RID: 79
	[DataContract]
	internal sealed class OleDbSchema : OperationDataContract
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00002CC7 File Offset: 0x00000EC7
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00002CCF File Offset: 0x00000ECF
		[DataMember(Name = "schemaGuid", IsRequired = true, EmitDefaultValue = false)]
		internal Guid SchemaGuid { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00002CE0 File Offset: 0x00000EE0
		[DataMember(Name = "restrictions", IsRequired = true, EmitDefaultValue = true)]
		internal uint Restrictions { get; set; }
	}
}
