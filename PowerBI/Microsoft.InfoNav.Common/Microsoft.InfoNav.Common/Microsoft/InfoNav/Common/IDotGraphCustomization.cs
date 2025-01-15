using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000042 RID: 66
	public interface IDotGraphCustomization
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000314 RID: 788
		bool SortContent { get; }

		// Token: 0x06000315 RID: 789
		string GetVertexName(int vertexId);

		// Token: 0x06000316 RID: 790
		string GetEdgeName(int edgeId);

		// Token: 0x06000317 RID: 791
		IEnumerable<KeyValuePair<string, string>> GetGraphAttributes();

		// Token: 0x06000318 RID: 792
		IEnumerable<KeyValuePair<string, string>> GetVertexAttributes(int vertexId);

		// Token: 0x06000319 RID: 793
		IEnumerable<KeyValuePair<string, string>> GetEdgeAttributes(int edgeId);

		// Token: 0x0600031A RID: 794
		int ComparePrecedence(int xVertexId, int yVertexId);
	}
}
