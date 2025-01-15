using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000265 RID: 613
	internal sealed class ExpressionReferenceNameToTableMapping
	{
		// Token: 0x06001A95 RID: 6805 RVA: 0x00049AF6 File Offset: 0x00047CF6
		internal ExpressionReferenceNameToTableMapping()
		{
			this._mapping = new Dictionary<string, QueryTable>();
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00049B0C File Offset: 0x00047D0C
		internal ExpressionReferenceNameToTableMapping(IEnumerable<KeyValuePair<string, QueryTable>> pairs)
		{
			this._mapping = pairs.ToDictionary((KeyValuePair<string, QueryTable> x) => x.Key, (KeyValuePair<string, QueryTable> x) => x.Value);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00049B6C File Offset: 0x00047D6C
		public QueryTable GetTable(string name)
		{
			QueryTable queryTable = null;
			this._mapping.TryGetValue(name, out queryTable);
			return queryTable;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00049B8B File Offset: 0x00047D8B
		public void AddTable(string name, QueryTable table)
		{
			this._mapping.Add(name, table);
		}

		// Token: 0x04000EAF RID: 3759
		private readonly Dictionary<string, QueryTable> _mapping;
	}
}
