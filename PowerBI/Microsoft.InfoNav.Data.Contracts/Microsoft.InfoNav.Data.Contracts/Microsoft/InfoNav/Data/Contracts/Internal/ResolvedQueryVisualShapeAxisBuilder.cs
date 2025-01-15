using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000229 RID: 553
	public class ResolvedQueryVisualShapeAxisBuilder<TParent>
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x0001E483 File Offset: 0x0001C683
		internal ResolvedQueryVisualShapeAxisBuilder(TParent parent, Action<ResolvedQueryAxis> addToParent, string axisName)
		{
			this.parent = parent;
			this.addToParent = addToParent;
			this.axisName = axisName;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0001E4AB File Offset: 0x0001C6AB
		public ResolvedQueryVisualShapeAxisGroupBuilder<ResolvedQueryVisualShapeAxisBuilder<TParent>> WithAxisGroup()
		{
			return new ResolvedQueryVisualShapeAxisGroupBuilder<ResolvedQueryVisualShapeAxisBuilder<TParent>>(this, delegate(ResolvedQueryAxisGroup group)
			{
				this.axisGroups.Add(group);
			});
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0001E4BF File Offset: 0x0001C6BF
		public TParent Parent
		{
			get
			{
				this.addToParent(new ResolvedQueryAxis(this.axisName, this.axisGroups));
				return this.parent;
			}
		}

		// Token: 0x0400076A RID: 1898
		private readonly TParent parent;

		// Token: 0x0400076B RID: 1899
		private readonly Action<ResolvedQueryAxis> addToParent;

		// Token: 0x0400076C RID: 1900
		private readonly string axisName;

		// Token: 0x0400076D RID: 1901
		private readonly List<ResolvedQueryAxisGroup> axisGroups = new List<ResolvedQueryAxisGroup>();
	}
}
