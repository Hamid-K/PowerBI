using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000101 RID: 257
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class LiteralTermBinding : TermBinding
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000091BD File Offset: 0x000073BD
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000091C5 File Offset: 0x000073C5
		[DataMember(IsRequired = true, Order = 10)]
		public DataType DataType { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000091CE File Offset: 0x000073CE
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x000091D6 File Offset: 0x000073D6
		public DataValue DataValue { get; set; }

		// Token: 0x06000507 RID: 1287 RVA: 0x000091DF File Offset: 0x000073DF
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}_{1}", base.ToString(), this.DataType);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000091FC File Offset: 0x000073FC
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
