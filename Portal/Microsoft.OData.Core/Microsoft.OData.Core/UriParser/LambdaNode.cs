using System;
using System.Collections.ObjectModel;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000186 RID: 390
	public abstract class LambdaNode : SingleValueNode
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x000393E0 File Offset: 0x000375E0
		protected LambdaNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000393EA File Offset: 0x000375EA
		protected LambdaNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
		{
			this.rangeVariables = rangeVariables;
			this.currentRangeVariable = currentRangeVariable;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00039400 File Offset: 0x00037600
		public Collection<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00039408 File Offset: 0x00037608
		public RangeVariable CurrentRangeVariable
		{
			get
			{
				return this.currentRangeVariable;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00039410 File Offset: 0x00037610
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x00039418 File Offset: 0x00037618
		public SingleValueNode Body { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00039421 File Offset: 0x00037621
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x00039429 File Offset: 0x00037629
		public CollectionNode Source { get; set; }

		// Token: 0x0400089A RID: 2202
		private readonly Collection<RangeVariable> rangeVariables;

		// Token: 0x0400089B RID: 2203
		private readonly RangeVariable currentRangeVariable;
	}
}
