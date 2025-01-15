using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DFA RID: 7674
	public interface IReportPartitionResources
	{
		// Token: 0x0600BDB0 RID: 48560
		void PartitionResources(IPartitionKey partitionKey, IEnumerable<IResource> resources);
	}
}
