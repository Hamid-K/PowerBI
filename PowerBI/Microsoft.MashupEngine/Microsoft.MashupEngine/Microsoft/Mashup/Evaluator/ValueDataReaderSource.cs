using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D79 RID: 7545
	internal static class ValueDataReaderSource
	{
		// Token: 0x0600BB6A RID: 47978 RVA: 0x0025F430 File Offset: 0x0025D630
		public static IDataReaderSource Create(IEngine engine, IValue value, bool reportRelationships)
		{
			ValueShape valueShape = ValueDataReaderSource.GetValueShape(value);
			ITableValue tableValue = engine.NormalizeToTable(value);
			return new ValueDataReaderSource.TableValueDataReaderSource(engine, valueShape, tableValue, reportRelationships);
		}

		// Token: 0x0600BB6B RID: 47979 RVA: 0x0025F455 File Offset: 0x0025D655
		private static ValueShape GetValueShape(IValue value)
		{
			if (value.IsTable)
			{
				return ValueShape.Table;
			}
			if (value.IsRecord)
			{
				return ValueShape.Record;
			}
			if (value.IsList)
			{
				return ValueShape.List;
			}
			return ValueShape.Primitive;
		}

		// Token: 0x02001D7A RID: 7546
		private sealed class TableValueDataReaderSource : IDataReaderSource, IDisposable, ITableSource
		{
			// Token: 0x0600BB6C RID: 47980 RVA: 0x0025F476 File Offset: 0x0025D676
			public TableValueDataReaderSource(IEngine engine, ValueShape valueShape, ITableValue table, bool reportRelationships)
			{
				this.engine = engine;
				this.valueShape = valueShape;
				this.table = table;
				this.reportRelationships = reportRelationships;
			}

			// Token: 0x17002E43 RID: 11843
			// (get) Token: 0x0600BB6D RID: 47981 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public ITableSource TableSource
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002E44 RID: 11844
			// (get) Token: 0x0600BB6E RID: 47982 RVA: 0x0025F49B File Offset: 0x0025D69B
			public ValueShape ValueShape
			{
				get
				{
					return this.valueShape;
				}
			}

			// Token: 0x17002E45 RID: 11845
			// (get) Token: 0x0600BB6F RID: 47983 RVA: 0x0025F4A3 File Offset: 0x0025D6A3
			public int ColumnCount
			{
				get
				{
					return this.table.Type.AsTableType.ItemType.Fields.Keys.Length;
				}
			}

			// Token: 0x17002E46 RID: 11846
			// (get) Token: 0x0600BB70 RID: 47984 RVA: 0x0025F4C9 File Offset: 0x0025D6C9
			public IEnumerable<string> KeyColumnNames
			{
				get
				{
					return this.table.Type.KeyColumnNames;
				}
			}

			// Token: 0x17002E47 RID: 11847
			// (get) Token: 0x0600BB71 RID: 47985 RVA: 0x0025F4DC File Offset: 0x0025D6DC
			public IEnumerable<IRelationship> Relationships
			{
				get
				{
					if (!this.reportRelationships)
					{
						return EmptyArray<IRelationship>.Instance;
					}
					return this.table.Relationships;
				}
			}

			// Token: 0x0600BB72 RID: 47986 RVA: 0x0025F504 File Offset: 0x0025D704
			public IColumnIdentity ColumnIdentity(int index)
			{
				return this.table.ColumnIdentity(index);
			}

			// Token: 0x17002E48 RID: 11848
			// (get) Token: 0x0600BB73 RID: 47987 RVA: 0x0025F512 File Offset: 0x0025D712
			public IPageReader PageReader
			{
				get
				{
					return this.engine.CreatePageReader(this.table);
				}
			}

			// Token: 0x0600BB74 RID: 47988 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005F6E RID: 24430
			private readonly IEngine engine;

			// Token: 0x04005F6F RID: 24431
			private readonly ValueShape valueShape;

			// Token: 0x04005F70 RID: 24432
			private readonly ITableValue table;

			// Token: 0x04005F71 RID: 24433
			private readonly bool reportRelationships;
		}
	}
}
