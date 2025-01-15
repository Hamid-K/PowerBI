using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D8 RID: 472
	[DataContract(Name = "Execute", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SemanticQueryDataShapeCommandRequest
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00018E0D File Offset: 0x0001700D
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00018E15 File Offset: 0x00017015
		[DataMember(Name = "Command", IsRequired = true, Order = 10)]
		public SemanticQueryDataShapeCommand Command { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00018E1E File Offset: 0x0001701E
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00018E26 File Offset: 0x00017026
		[DataMember(Name = "CancellationTokenKey", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string CancellationTokenKey { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00018E2F File Offset: 0x0001702F
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00018E37 File Offset: 0x00017037
		[DataMember(Name = "RequestPriority", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public RequestPriority? RequestPriority { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00018E40 File Offset: 0x00017040
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00018E48 File Offset: 0x00017048
		[DataMember(Name = "ApplicationContext", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public ApplicationContext ApplicationContext { get; set; }
	}
}
