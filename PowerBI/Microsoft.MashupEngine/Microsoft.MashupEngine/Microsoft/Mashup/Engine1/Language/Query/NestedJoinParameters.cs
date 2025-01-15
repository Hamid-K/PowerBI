using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017F9 RID: 6137
	public class NestedJoinParameters
	{
		// Token: 0x06009AF1 RID: 39665 RVA: 0x00200F8B File Offset: 0x001FF18B
		public NestedJoinParameters(Query leftQuery, int[] leftKeys, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinColumns)
		{
			this.leftQuery = leftQuery;
			this.leftKeys = leftKeys;
			this.rightTable = rightTable;
			this.rightKey = rightKey;
			this.joinKind = joinKind;
			this.newColumnName = newColumnName;
			this.joinColumns = joinColumns;
		}

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06009AF2 RID: 39666 RVA: 0x00200FC8 File Offset: 0x001FF1C8
		public Query LeftQuery
		{
			get
			{
				return this.leftQuery;
			}
		}

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x06009AF3 RID: 39667 RVA: 0x00200FD0 File Offset: 0x001FF1D0
		public int[] LeftKeyColumns
		{
			get
			{
				return this.leftKeys;
			}
		}

		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x06009AF4 RID: 39668 RVA: 0x00200FD8 File Offset: 0x001FF1D8
		public Value RightTable
		{
			get
			{
				return this.rightTable;
			}
		}

		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x06009AF5 RID: 39669 RVA: 0x00200FE0 File Offset: 0x001FF1E0
		public Keys RightKey
		{
			get
			{
				return this.rightKey;
			}
		}

		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x06009AF6 RID: 39670 RVA: 0x00200FE8 File Offset: 0x001FF1E8
		public TableTypeAlgebra.JoinKind JoinKind
		{
			get
			{
				return this.joinKind;
			}
		}

		// Token: 0x170027DC RID: 10204
		// (get) Token: 0x06009AF7 RID: 39671 RVA: 0x00200FF0 File Offset: 0x001FF1F0
		public string NewColumnName
		{
			get
			{
				return this.newColumnName;
			}
		}

		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x06009AF8 RID: 39672 RVA: 0x00200FF8 File Offset: 0x001FF1F8
		public Keys JoinColumns
		{
			get
			{
				return this.joinColumns;
			}
		}

		// Token: 0x040051F8 RID: 20984
		private Query leftQuery;

		// Token: 0x040051F9 RID: 20985
		private int[] leftKeys;

		// Token: 0x040051FA RID: 20986
		private Value rightTable;

		// Token: 0x040051FB RID: 20987
		private Keys rightKey;

		// Token: 0x040051FC RID: 20988
		private TableTypeAlgebra.JoinKind joinKind;

		// Token: 0x040051FD RID: 20989
		private string newColumnName;

		// Token: 0x040051FE RID: 20990
		private Keys joinColumns;
	}
}
