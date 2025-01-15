using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000043 RID: 67
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class CommandArgument
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003DD8 File Offset: 0x00001FD8
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[DataMember(IsRequired = true, Order = 10)]
		public string Name { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003DE9 File Offset: 0x00001FE9
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003DF1 File Offset: 0x00001FF1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string ValueExpression { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003DFA File Offset: 0x00001FFA
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00003E02 File Offset: 0x00002002
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Value { get; set; }
	}
}
