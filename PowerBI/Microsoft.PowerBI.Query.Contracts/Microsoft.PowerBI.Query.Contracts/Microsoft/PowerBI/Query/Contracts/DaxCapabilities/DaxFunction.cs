using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Query.Contracts.DaxCapabilities
{
	// Token: 0x0200000C RID: 12
	[DataContract(Name = "DaxFunction")]
	public sealed class DaxFunction
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002658 File Offset: 0x00000858
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002660 File Offset: 0x00000860
		[DataMember(IsRequired = true, Order = 10)]
		public string Name { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002669 File Offset: 0x00000869
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002671 File Offset: 0x00000871
		[DataMember(IsRequired = false, Order = 20, EmitDefaultValue = false)]
		public string Description { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000267A File Offset: 0x0000087A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002682 File Offset: 0x00000882
		[DataMember(IsRequired = true, Order = 30)]
		public FunctionCategory Category { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000268B File Offset: 0x0000088B
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002693 File Offset: 0x00000893
		[DataMember(IsRequired = true, Order = 40)]
		public PushableToDirectQuery PushableToDirectQuery { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000269C File Offset: 0x0000089C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000026A4 File Offset: 0x000008A4
		[DataMember(IsRequired = false, Order = 50, EmitDefaultValue = false)]
		public IList<DaxFunctionParameter> Parameters { get; set; }
	}
}
