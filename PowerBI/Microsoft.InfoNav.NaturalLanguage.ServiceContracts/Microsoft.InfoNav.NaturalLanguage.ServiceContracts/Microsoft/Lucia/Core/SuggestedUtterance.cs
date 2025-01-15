using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000DE RID: 222
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SuggestedUtterance
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000083EE File Offset: 0x000065EE
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x000083F6 File Offset: 0x000065F6
		[DataMember(IsRequired = true, Order = 10)]
		public string Text { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x000083FF File Offset: 0x000065FF
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00008407 File Offset: 0x00006607
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public UtteranceSource Source { get; set; }

		// Token: 0x0600045A RID: 1114 RVA: 0x00008410 File Offset: 0x00006610
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}) {1}", this.Source, this.Text);
		}
	}
}
