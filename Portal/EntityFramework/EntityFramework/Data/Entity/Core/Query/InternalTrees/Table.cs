using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F2 RID: 1010
	internal class Table
	{
		// Token: 0x06002F3E RID: 12094 RVA: 0x00095888 File Offset: 0x00093A88
		internal Table(Command command, TableMD tableMetadata, int tableId)
		{
			this.m_tableMetadata = tableMetadata;
			this.m_columns = Command.CreateVarList();
			this.m_keys = command.CreateVarVec();
			this.m_nonnullableColumns = command.CreateVarVec();
			this.m_tableId = tableId;
			Dictionary<string, ColumnVar> dictionary = new Dictionary<string, ColumnVar>();
			foreach (ColumnMD columnMD in tableMetadata.Columns)
			{
				ColumnVar columnVar = command.CreateColumnVar(this, columnMD);
				dictionary[columnMD.Name] = columnVar;
				if (!columnMD.IsNullable)
				{
					this.m_nonnullableColumns.Set(columnVar);
				}
			}
			foreach (ColumnMD columnMD2 in tableMetadata.Keys)
			{
				ColumnVar columnVar2 = dictionary[columnMD2.Name];
				this.m_keys.Set(columnVar2);
			}
			this.m_referencedColumns = command.CreateVarVec(this.m_columns);
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000959A8 File Offset: 0x00093BA8
		internal TableMD TableMetadata
		{
			get
			{
				return this.m_tableMetadata;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000959B0 File Offset: 0x00093BB0
		internal VarList Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000959B8 File Offset: 0x00093BB8
		internal VarVec ReferencedColumns
		{
			get
			{
				return this.m_referencedColumns;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000959C0 File Offset: 0x00093BC0
		internal VarVec NonNullableColumns
		{
			get
			{
				return this.m_nonnullableColumns;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000959C8 File Offset: 0x00093BC8
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000959D0 File Offset: 0x00093BD0
		internal int TableId
		{
			get
			{
				return this.m_tableId;
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000959D8 File Offset: 0x00093BD8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}::{1}", new object[] { this.m_tableMetadata, this.TableId });
		}

		// Token: 0x04000FED RID: 4077
		private readonly TableMD m_tableMetadata;

		// Token: 0x04000FEE RID: 4078
		private readonly VarList m_columns;

		// Token: 0x04000FEF RID: 4079
		private readonly VarVec m_referencedColumns;

		// Token: 0x04000FF0 RID: 4080
		private readonly VarVec m_keys;

		// Token: 0x04000FF1 RID: 4081
		private readonly VarVec m_nonnullableColumns;

		// Token: 0x04000FF2 RID: 4082
		private readonly int m_tableId;
	}
}
