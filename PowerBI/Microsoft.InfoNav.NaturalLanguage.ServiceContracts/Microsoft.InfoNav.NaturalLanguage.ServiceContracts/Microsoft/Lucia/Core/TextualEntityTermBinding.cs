using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000114 RID: 276
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class TextualEntityTermBinding : EntityTermBinding
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000A7FB File Offset: 0x000089FB
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000A803 File Offset: 0x00008A03
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string TextualEntityName { get; set; }

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000A80C File Offset: 0x00008A0C
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}_{1}", base.ToString(), this.TextualEntityName);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000A824 File Offset: 0x00008A24
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
