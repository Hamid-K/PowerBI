using System;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200005C RID: 92
	public interface IDataIndexDisposer
	{
		// Token: 0x060002A4 RID: 676
		void WaitForCompletion();

		// Token: 0x060002A5 RID: 677
		void Dispose(DataIndex index);
	}
}
