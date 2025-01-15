using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E4 RID: 740
	[DataContract(Name = "TransformTableColumn", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransformTableColumn
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0002C316 File Offset: 0x0002A516
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0002C31E File Offset: 0x0002A51E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string Role { get; set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0002C327 File Offset: 0x0002A527
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x0002C32F File Offset: 0x0002A52F
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer Expression { get; set; }
	}
}
