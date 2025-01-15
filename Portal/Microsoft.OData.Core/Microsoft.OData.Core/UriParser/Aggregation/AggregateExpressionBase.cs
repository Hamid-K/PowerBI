using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F7 RID: 503
	public abstract class AggregateExpressionBase
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0003EE2B File Offset: 0x0003D02B
		protected AggregateExpressionBase(AggregateExpressionKind kind, string alias)
		{
			this.AggregateKind = kind;
			this.Alias = alias;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0003EE41 File Offset: 0x0003D041
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x0003EE49 File Offset: 0x0003D049
		public AggregateExpressionKind AggregateKind { get; private set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0003EE52 File Offset: 0x0003D052
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x0003EE5A File Offset: 0x0003D05A
		public string Alias { get; private set; }
	}
}
