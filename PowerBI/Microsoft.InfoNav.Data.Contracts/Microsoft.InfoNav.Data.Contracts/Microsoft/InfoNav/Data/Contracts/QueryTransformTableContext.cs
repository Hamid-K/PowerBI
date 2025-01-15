using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000091 RID: 145
	internal sealed class QueryTransformTableContext
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00009606 File Offset: 0x00007806
		internal QueryTransformTableContext()
		{
			this._tables = new Dictionary<string, ResolvedQueryTransformTable>(QueryNameComparer.Instance);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000961E File Offset: 0x0000781E
		internal bool TryResolveTable(string name, out ResolvedQueryTransformTable table)
		{
			return this._tables.TryGetValue(name, out table);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000962D File Offset: 0x0000782D
		internal bool TryAddTable(ResolvedQueryTransformTable table)
		{
			if (this._tables.ContainsKey(table.Name))
			{
				return false;
			}
			this._tables.Add(table.Name, table);
			return true;
		}

		// Token: 0x040001C9 RID: 457
		private readonly Dictionary<string, ResolvedQueryTransformTable> _tables;
	}
}
