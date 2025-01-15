using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200006B RID: 107
	internal interface IFilterConditionVisitor<T>
	{
		// Token: 0x06000222 RID: 546
		void Visit(UnaryFilterCondition<T> filterCondition);

		// Token: 0x06000223 RID: 547
		void Visit(BinaryFilterCondition<T> filterCondition);

		// Token: 0x06000224 RID: 548
		void Visit(CompoundFilterCondition<T> filterCondition);
	}
}
