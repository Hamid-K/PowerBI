using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000044 RID: 68
	public class DefaultDotGraph : IDotGraphCustomization
	{
		// Token: 0x0600031B RID: 795 RVA: 0x00008E45 File Offset: 0x00007045
		protected DefaultDotGraph()
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00008E4D File Offset: 0x0000704D
		public virtual bool SortContent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008E50 File Offset: 0x00007050
		public virtual string GetVertexName(int vertexId)
		{
			return vertexId.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008E5E File Offset: 0x0000705E
		public virtual string GetEdgeName(int edgeId)
		{
			return edgeId.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008E6C File Offset: 0x0000706C
		public virtual IEnumerable<KeyValuePair<string, string>> GetGraphAttributes()
		{
			return Enumerable.Empty<KeyValuePair<string, string>>();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008E73 File Offset: 0x00007073
		public virtual IEnumerable<KeyValuePair<string, string>> GetVertexAttributes(int vertexId)
		{
			return Enumerable.Empty<KeyValuePair<string, string>>();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008E7A File Offset: 0x0000707A
		public virtual IEnumerable<KeyValuePair<string, string>> GetEdgeAttributes(int edgeId)
		{
			return Enumerable.Empty<KeyValuePair<string, string>>();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008E81 File Offset: 0x00007081
		public int ComparePrecedence(int xVertexId, int yVertexId)
		{
			return 0;
		}

		// Token: 0x040000A8 RID: 168
		public static readonly IDotGraphCustomization DefaultRawDotGraph = new DefaultDotGraph();
	}
}
