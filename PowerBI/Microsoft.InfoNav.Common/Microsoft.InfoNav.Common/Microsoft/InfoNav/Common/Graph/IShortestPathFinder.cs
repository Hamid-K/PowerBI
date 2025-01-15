using System;

namespace Microsoft.InfoNav.Common.Graph
{
	// Token: 0x02000081 RID: 129
	public interface IShortestPathFinder
	{
		// Token: 0x060004C8 RID: 1224
		ShortestPathSolution Solve(ShortestPathProblem problem);
	}
}
