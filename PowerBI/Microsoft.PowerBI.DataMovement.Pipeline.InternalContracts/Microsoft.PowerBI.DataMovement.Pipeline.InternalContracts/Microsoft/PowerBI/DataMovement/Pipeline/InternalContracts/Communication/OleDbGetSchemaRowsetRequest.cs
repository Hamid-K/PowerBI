using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbGetSchemaRowsetRequest : OleDbRequestBase
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00002C1F File Offset: 0x00000E1F
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00002C27 File Offset: 0x00000E27
		[DataMember(Name = "schemaGuid", IsRequired = true, EmitDefaultValue = false)]
		public Guid SchemaGuid { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00002C30 File Offset: 0x00000E30
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00002C38 File Offset: 0x00000E38
		[DataMember(Name = "restrictions", IsRequired = false, EmitDefaultValue = false)]
		public OleDbRestriction[] Restrictions { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00002C41 File Offset: 0x00000E41
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00002C49 File Offset: 0x00000E49
		[DataMember(Name = "rowsetProperties", IsRequired = false, EmitDefaultValue = false)]
		public OleDbProperty[] RowsetProperties { get; set; }
	}
}
