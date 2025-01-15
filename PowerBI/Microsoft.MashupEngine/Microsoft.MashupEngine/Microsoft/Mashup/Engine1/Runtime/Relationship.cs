using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001635 RID: 5685
	public sealed class Relationship : IRelationship
	{
		// Token: 0x06008F34 RID: 36660 RVA: 0x001DD38D File Offset: 0x001DB58D
		public Relationship(int[] leftKeyColumns, Value rightTable, Keys rightKey)
			: this(leftKeyColumns, rightTable, rightKey)
		{
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x001DD398 File Offset: 0x001DB598
		public Relationship(int[] leftKeyColumns, ColumnIdentity[] rightKeyColumns)
			: this(leftKeyColumns, null, rightKeyColumns)
		{
		}

		// Token: 0x06008F36 RID: 36662 RVA: 0x001DD3A3 File Offset: 0x001DB5A3
		private Relationship(int[] leftKeyColumns, Value rightTable, object rightKeyKeysOrColumns)
		{
			this.leftKeyColumns = leftKeyColumns;
			this.rightTable = rightTable;
			this.rightKeyKeysOrColumns = rightKeyKeysOrColumns;
		}

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x06008F37 RID: 36663 RVA: 0x001DD3C0 File Offset: 0x001DB5C0
		int IRelationship.KeyColumnCount
		{
			get
			{
				return this.leftKeyColumns.Length;
			}
		}

		// Token: 0x06008F38 RID: 36664 RVA: 0x001DD3CA File Offset: 0x001DB5CA
		int IRelationship.KeyColumn(int index)
		{
			return this.leftKeyColumns[index];
		}

		// Token: 0x06008F39 RID: 36665 RVA: 0x001DD3D4 File Offset: 0x001DB5D4
		IColumnIdentity IRelationship.OtherKeyColumn(int index)
		{
			return this.RightKeyColumns[index];
		}

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x06008F3A RID: 36666 RVA: 0x001DD3DE File Offset: 0x001DB5DE
		public int[] LeftKeyColumns
		{
			get
			{
				return this.leftKeyColumns;
			}
		}

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x06008F3B RID: 36667 RVA: 0x001DD3E6 File Offset: 0x001DB5E6
		public ColumnIdentity[] RightKeyColumns
		{
			get
			{
				this.EnsureRightKeyColumns();
				return (ColumnIdentity[])this.rightKeyKeysOrColumns;
			}
		}

		// Token: 0x06008F3C RID: 36668 RVA: 0x001DD3F9 File Offset: 0x001DB5F9
		public Relationship SelectColumns(int[] leftKeyColumns)
		{
			return new Relationship(leftKeyColumns, this.rightTable, this.rightKeyKeysOrColumns);
		}

		// Token: 0x06008F3D RID: 36669 RVA: 0x001DD410 File Offset: 0x001DB610
		private void EnsureRightKeyColumns()
		{
			if (!(this.rightKeyKeysOrColumns is ColumnIdentity[]) && this.rightTable != null)
			{
				FunctionValue functionValue = this.rightTable as FunctionValue;
				if (functionValue != null)
				{
					this.rightTable = functionValue.Invoke().AsTable;
				}
				Keys columns = this.rightTable.AsTable.Columns;
				ColumnIdentity[] columnIdentities = this.rightTable.AsTable.ColumnIdentities;
				ColumnIdentity[] array;
				if (columnIdentities != null)
				{
					Keys keys = (Keys)this.rightKeyKeysOrColumns;
					int[] columns2 = TableValue.GetColumns(columns, keys);
					array = new ColumnIdentity[keys.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = columnIdentities[columns2[i]];
					}
				}
				else
				{
					array = new ColumnIdentity[this.leftKeyColumns.Length];
				}
				this.rightTable = null;
				this.rightKeyKeysOrColumns = array;
			}
		}

		// Token: 0x04004D7C RID: 19836
		private readonly int[] leftKeyColumns;

		// Token: 0x04004D7D RID: 19837
		private Value rightTable;

		// Token: 0x04004D7E RID: 19838
		private object rightKeyKeysOrColumns;
	}
}
