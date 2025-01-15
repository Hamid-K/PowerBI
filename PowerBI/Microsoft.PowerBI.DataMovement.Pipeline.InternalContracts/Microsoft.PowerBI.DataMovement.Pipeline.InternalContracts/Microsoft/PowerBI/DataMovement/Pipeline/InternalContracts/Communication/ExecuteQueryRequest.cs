using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class ExecuteQueryRequest : DatabasesRequestBase
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020F5 File Offset: 0x000002F5
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020FD File Offset: 0x000002FD
		[DataMember(Name = "query", IsRequired = true, EmitDefaultValue = false)]
		public string Query { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002106 File Offset: 0x00000306
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000210E File Offset: 0x0000030E
		[DataMember(Name = "commandType", IsRequired = false, EmitDefaultValue = false)]
		public CommandType CommandType { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002117 File Offset: 0x00000317
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000211F File Offset: 0x0000031F
		[DataMember(Name = "parameters", IsRequired = false, EmitDefaultValue = false)]
		public ExecuteQueryRequestParameter[] Parameters { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002130 File Offset: 0x00000330
		[DataMember(Name = "properties", IsRequired = false, EmitDefaultValue = false)]
		public ExecuteQueryRequestProperty[] CommandProperties { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002139 File Offset: 0x00000339
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002141 File Offset: 0x00000341
		[DataMember(Name = "executionProperties", IsRequired = false, EmitDefaultValue = false)]
		public ExecuteQueryRequestProperty[] ExecutionProperties { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000214A File Offset: 0x0000034A
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002152 File Offset: 0x00000352
		[DataMember(Name = "commandTimeout", IsRequired = false, EmitDefaultValue = false)]
		public int CommandTimeout { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000215B File Offset: 0x0000035B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002163 File Offset: 0x00000363
		[DataMember(Name = "requestId", IsRequired = false, EmitDefaultValue = false)]
		public Guid RequestId { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002174 File Offset: 0x00000374
		[DataMember(Name = "resultIndex", IsRequired = false, EmitDefaultValue = false)]
		public int ResultIndex { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000217D File Offset: 0x0000037D
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002185 File Offset: 0x00000385
		[DataMember(Name = "connectionId", IsRequired = false, EmitDefaultValue = false)]
		public Guid ConnectionId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000218E File Offset: 0x0000038E
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002196 File Offset: 0x00000396
		[DataMember(Name = "isStateful", IsRequired = false)]
		public bool ExplicitCloseConnectionRequired { get; set; }
	}
}
