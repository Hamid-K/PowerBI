using System;
using System.Collections.ObjectModel;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000220 RID: 544
	public abstract class LambdaNode : SingleValueNode
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x00048A63 File Offset: 0x00046C63
		protected LambdaNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00048A6D File Offset: 0x00046C6D
		protected LambdaNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
		{
			this.rangeVariables = rangeVariables;
			this.currentRangeVariable = currentRangeVariable;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00048A83 File Offset: 0x00046C83
		public Collection<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00048A8B File Offset: 0x00046C8B
		public RangeVariable CurrentRangeVariable
		{
			get
			{
				return this.currentRangeVariable;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00048A93 File Offset: 0x00046C93
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00048A9B File Offset: 0x00046C9B
		public SingleValueNode Body { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00048AA4 File Offset: 0x00046CA4
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00048AAC File Offset: 0x00046CAC
		public CollectionNode Source { get; set; }

		// Token: 0x04000858 RID: 2136
		private readonly Collection<RangeVariable> rangeVariables;

		// Token: 0x04000859 RID: 2137
		private readonly RangeVariable currentRangeVariable;
	}
}
