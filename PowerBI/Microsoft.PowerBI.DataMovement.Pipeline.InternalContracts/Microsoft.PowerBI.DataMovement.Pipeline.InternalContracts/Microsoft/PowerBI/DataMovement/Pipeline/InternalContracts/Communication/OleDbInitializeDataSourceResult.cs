using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200004E RID: 78
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbInitializeDataSourceResult : OleDbResultBase
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00002C8C File Offset: 0x00000E8C
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00002C94 File Offset: 0x00000E94
		[DataMember(Name = "schemas", IsRequired = true, EmitDefaultValue = false)]
		public OleDbSchema[] Schemas { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00002C9D File Offset: 0x00000E9D
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00002CA5 File Offset: 0x00000EA5
		[DataMember(Name = "dataSourceProperties", IsRequired = false, EmitDefaultValue = false)]
		public OleDbProperty[] DataSourceProperties { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00002CAE File Offset: 0x00000EAE
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00002CB6 File Offset: 0x00000EB6
		[DataMember(Name = "requestId", IsRequired = false, EmitDefaultValue = false)]
		public Guid RequestId { get; set; }
	}
}
