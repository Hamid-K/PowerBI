using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000141 RID: 321
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class CompletedUtterance
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0000B450 File Offset: 0x00009650
		public CompletedUtterance()
		{
			this.Terms = new List<UtteranceTerm>();
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000B463 File Offset: 0x00009663
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0000B46B File Offset: 0x0000966B
		[DataMember(IsRequired = true, Order = 1)]
		public string Text { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000B474 File Offset: 0x00009674
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0000B47C File Offset: 0x0000967C
		[DataMember(IsRequired = true, Order = 2)]
		public List<UtteranceTerm> Terms { get; set; }

		// Token: 0x06000660 RID: 1632 RVA: 0x0000B485 File Offset: 0x00009685
		public override string ToString()
		{
			return this.Text;
		}
	}
}
