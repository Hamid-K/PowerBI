using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000117 RID: 279
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class VisualizationTypeTermBinding : CoreTermBaseBinding
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000A8A2 File Offset: 0x00008AA2
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000A8AA File Offset: 0x00008AAA
		[DataMember(IsRequired = true, Order = 10)]
		public VisualizationType Type { get; set; }

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000A8B3 File Offset: 0x00008AB3
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}_{1}", base.ToString(), this.Type);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
