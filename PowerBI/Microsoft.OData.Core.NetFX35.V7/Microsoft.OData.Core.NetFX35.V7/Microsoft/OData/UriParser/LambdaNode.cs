using System;
using System.Collections.ObjectModel;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013C RID: 316
	public abstract class LambdaNode : SingleValueNode
	{
		// Token: 0x06000E31 RID: 3633 RVA: 0x00029718 File Offset: 0x00027918
		protected LambdaNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00029722 File Offset: 0x00027922
		protected LambdaNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
		{
			this.rangeVariables = rangeVariables;
			this.currentRangeVariable = currentRangeVariable;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00029738 File Offset: 0x00027938
		public Collection<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00029740 File Offset: 0x00027940
		public RangeVariable CurrentRangeVariable
		{
			get
			{
				return this.currentRangeVariable;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00029748 File Offset: 0x00027948
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x00029750 File Offset: 0x00027950
		public SingleValueNode Body { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00029759 File Offset: 0x00027959
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x00029761 File Offset: 0x00027961
		public CollectionNode Source { get; set; }

		// Token: 0x04000765 RID: 1893
		private readonly Collection<RangeVariable> rangeVariables;

		// Token: 0x04000766 RID: 1894
		private readonly RangeVariable currentRangeVariable;
	}
}
