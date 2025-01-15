using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200005A RID: 90
	public interface IDataIndexBuilder
	{
		// Token: 0x060002A2 RID: 674
		Task<DataIndex> BuildIndexAsync(IDatabaseContext context, CancellationToken token);
	}
}
