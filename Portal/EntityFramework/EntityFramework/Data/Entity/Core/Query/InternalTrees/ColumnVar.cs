using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200038D RID: 909
	internal sealed class ColumnVar : Var
	{
		// Token: 0x06002C19 RID: 11289 RVA: 0x0008EFBF File Offset: 0x0008D1BF
		internal ColumnVar(int id, Table table, ColumnMD columnMetadata)
			: base(id, VarType.Column, columnMetadata.Type)
		{
			this.m_table = table;
			this.m_columnMetadata = columnMetadata;
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x0008EFDD File Offset: 0x0008D1DD
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x0008EFE5 File Offset: 0x0008D1E5
		internal ColumnMD ColumnMetadata
		{
			get
			{
				return this.m_columnMetadata;
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0008EFED File Offset: 0x0008D1ED
		internal override bool TryGetName(out string name)
		{
			name = this.m_columnMetadata.Name;
			return true;
		}

		// Token: 0x04000EEE RID: 3822
		private readonly ColumnMD m_columnMetadata;

		// Token: 0x04000EEF RID: 3823
		private readonly Table m_table;
	}
}
