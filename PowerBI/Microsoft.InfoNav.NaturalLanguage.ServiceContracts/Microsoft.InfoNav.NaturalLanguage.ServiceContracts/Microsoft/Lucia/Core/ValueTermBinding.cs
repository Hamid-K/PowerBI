using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000115 RID: 277
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ValueTermBinding : PropertyTermBaseBinding
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000A835 File Offset: 0x00008A35
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000A83D File Offset: 0x00008A3D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Value { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000A846 File Offset: 0x00008A46
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000A84E File Offset: 0x00008A4E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public ValueTermType ValueTermType { get; set; }

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000A857 File Offset: 0x00008A57
		public override string ToString()
		{
			if (base.Text == this.Value || this.Value == null)
			{
				return base.ToString();
			}
			return StringUtil.FormatInvariant("{0}_{1}", base.ToString(), this.Value);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000A891 File Offset: 0x00008A91
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
