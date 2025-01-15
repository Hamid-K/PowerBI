using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200011E RID: 286
	internal sealed class ColumnByNameComparer<TColumn> : IEqualityComparer<TColumn>
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x0002C6E2 File Offset: 0x0002A8E2
		internal ColumnByNameComparer(Func<TColumn, string> getColumnName)
		{
			this._getColumnName = getColumnName;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002C6F4 File Offset: 0x0002A8F4
		public bool Equals(TColumn x, TColumn y)
		{
			return (x == null && y == null) || (x != null && y != null && DaxRef.NameComparer.Equals(this._getColumnName(x), this._getColumnName(y)));
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002C747 File Offset: 0x0002A947
		public int GetHashCode(TColumn obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return DaxRef.NameComparer.GetHashCode(this._getColumnName(obj));
		}

		// Token: 0x04000A66 RID: 2662
		private readonly Func<TColumn, string> _getColumnName;
	}
}
