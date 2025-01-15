using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200025F RID: 607
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformTable : IEquatable<ResolvedQueryTransformTable>
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00020118 File Offset: 0x0001E318
		internal ResolvedQueryTransformTable(string name, IReadOnlyList<ResolvedQueryTransformTableColumn> columns)
		{
			this._name = name;
			this._columns = columns;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0002012E File Offset: 0x0001E32E
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00020136 File Offset: 0x0001E336
		public IReadOnlyList<ResolvedQueryTransformTableColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00020140 File Offset: 0x0001E340
		public bool TryGetColumn(string name, out ResolvedQueryTransformTableColumn column)
		{
			foreach (ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn in this._columns)
			{
				if (QueryNameComparer.Instance.Equals(resolvedQueryTransformTableColumn.Name, name))
				{
					column = resolvedQueryTransformTableColumn;
					return true;
				}
			}
			column = null;
			return false;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000201A8 File Offset: 0x0001E3A8
		public bool Equals(ResolvedQueryTransformTable other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000201B6 File Offset: 0x0001E3B6
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000201C3 File Offset: 0x0001E3C3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTransformTable);
		}

		// Token: 0x040007BA RID: 1978
		private readonly string _name;

		// Token: 0x040007BB RID: 1979
		private readonly IReadOnlyList<ResolvedQueryTransformTableColumn> _columns;
	}
}
