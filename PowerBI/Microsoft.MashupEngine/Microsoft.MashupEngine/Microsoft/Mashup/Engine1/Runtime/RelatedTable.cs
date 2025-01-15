using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200162F RID: 5679
	public class RelatedTable
	{
		// Token: 0x06008F14 RID: 36628 RVA: 0x001DCA81 File Offset: 0x001DAC81
		public RelatedTable(int column, bool expanded, Value delayedTable)
		{
			this.column = column;
			this.expanded = expanded;
			this.value = delayedTable;
		}

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x06008F15 RID: 36629 RVA: 0x001DCA9E File Offset: 0x001DAC9E
		public bool Expanded
		{
			get
			{
				return this.expanded;
			}
		}

		// Token: 0x17002581 RID: 9601
		// (get) Token: 0x06008F16 RID: 36630 RVA: 0x001DCAA6 File Offset: 0x001DACA6
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x06008F17 RID: 36631 RVA: 0x001DCAAE File Offset: 0x001DACAE
		public TableValue Table
		{
			get
			{
				if (this.value.IsFunction)
				{
					this.value = this.value.AsFunction.Invoke();
				}
				return this.value.AsTable;
			}
		}

		// Token: 0x06008F18 RID: 36632 RVA: 0x001DCAE0 File Offset: 0x001DACE0
		public RelatedTable SelectColumns(ColumnSelection.SelectMap map)
		{
			int num = map.MapColumn(this.column);
			if (num != -1)
			{
				return this.SelectColumns(num);
			}
			return null;
		}

		// Token: 0x06008F19 RID: 36633 RVA: 0x001DCB07 File Offset: 0x001DAD07
		public RelatedTable SelectColumns(int newColumn)
		{
			return new RelatedTable(newColumn, this.expanded, this.value);
		}

		// Token: 0x06008F1A RID: 36634 RVA: 0x001DCB1B File Offset: 0x001DAD1B
		public RelatedTable TransformColumns(ColumnTransforms columnTransforms)
		{
			if (columnTransforms.Dictionary.ContainsKey(this.column))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06008F1B RID: 36635 RVA: 0x001DCB33 File Offset: 0x001DAD33
		public RelatedTable ExpandListColumn(int columnIndex)
		{
			if (this.column != columnIndex)
			{
				return this;
			}
			if (!this.expanded)
			{
				return new RelatedTable(this.column, true, this.value);
			}
			return null;
		}

		// Token: 0x06008F1C RID: 36636 RVA: 0x001DCB5C File Offset: 0x001DAD5C
		public IList<RelatedTable> ExpandRecordColumn(int columnToExpand, Keys fieldsToProject)
		{
			if (this.column < columnToExpand)
			{
				return new RelatedTable[] { this };
			}
			if (this.column == columnToExpand)
			{
				if (this.expanded)
				{
					int[] columnsOrNull = TableValue.GetColumnsOrNull(this.Table.Columns, fieldsToProject);
					if (columnsOrNull != null)
					{
						List<RelatedTable> list = new List<RelatedTable>();
						foreach (RelatedTable relatedTable in this.Table.RelatedTables)
						{
							int num = Array.IndexOf<int>(columnsOrNull, relatedTable.Column);
							if (num != -1)
							{
								list.Add(new RelatedTable(columnToExpand + num, relatedTable.expanded, relatedTable.value));
							}
						}
						if (list.Count > 0)
						{
							return list;
						}
					}
				}
				return RelatedTables.None;
			}
			return new RelatedTable[]
			{
				new RelatedTable(this.column + fieldsToProject.Length - 1, this.expanded, this.value)
			};
		}

		// Token: 0x04004D75 RID: 19829
		private readonly int column;

		// Token: 0x04004D76 RID: 19830
		private readonly bool expanded;

		// Token: 0x04004D77 RID: 19831
		private Value value;
	}
}
