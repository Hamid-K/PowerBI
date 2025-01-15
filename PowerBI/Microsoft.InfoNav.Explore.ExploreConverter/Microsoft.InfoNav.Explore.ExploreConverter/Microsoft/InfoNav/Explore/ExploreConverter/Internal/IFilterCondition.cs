using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200006A RID: 106
	internal interface IFilterCondition<T>
	{
		// Token: 0x06000221 RID: 545
		void Accept(IFilterConditionVisitor<T> visitor);
	}
}
