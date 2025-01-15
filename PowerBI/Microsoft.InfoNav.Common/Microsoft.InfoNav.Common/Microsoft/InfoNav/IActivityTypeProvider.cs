using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000016 RID: 22
	public interface IActivityTypeProvider<TType>
	{
		// Token: 0x060001A1 RID: 417
		IActivityType GetActivityType(TType activityType);
	}
}
