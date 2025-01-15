using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class ExecuteQueryResult : DatabaseResultBase
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000221D File Offset: 0x0000041D
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002225 File Offset: 0x00000425
		[DataMember(Name = "tableDescriptor", IsRequired = true, EmitDefaultValue = false)]
		public AdoTableDescriptor TableDescriptor { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000222E File Offset: 0x0000042E
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002236 File Offset: 0x00000436
		[IgnoreDataMember]
		public IDataReader Reader { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000223F File Offset: 0x0000043F
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002247 File Offset: 0x00000447
		[IgnoreDataMember]
		public object DbConnectionPoolObject { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002258 File Offset: 0x00000458
		[IgnoreDataMember]
		public IDbCommand DbCommand { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002261 File Offset: 0x00000461
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002269 File Offset: 0x00000469
		[IgnoreDataMember]
		public Guid RequestId { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002272 File Offset: 0x00000472
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000227A File Offset: 0x0000047A
		[IgnoreDataMember]
		public int ResultIndex { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002283 File Offset: 0x00000483
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000228B File Offset: 0x0000048B
		[IgnoreDataMember]
		public Guid ConnectionId { get; set; }
	}
}
