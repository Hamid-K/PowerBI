using System;
using System.IO;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.PowerBI.ExploreServiceCommon.Interfaces
{
	// Token: 0x02000029 RID: 41
	internal interface ITransformFlow
	{
		// Token: 0x0600015C RID: 348
		bool Run(Stream dataShapeResultStream, ref QueryBindingDescriptor bindingDescriptor);
	}
}
