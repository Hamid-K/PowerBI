using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017F4 RID: 6132
	public class IndexNestedJoinAlgorithm : NestedJoinAlgorithm
	{
		// Token: 0x06009AD8 RID: 39640 RVA: 0x00200AE0 File Offset: 0x001FECE0
		public override IEnumerable<IValueReference> NestedJoin(NestedJoinParameters parameters)
		{
			if (parameters.JoinKind == TableTypeAlgebra.JoinKind.LeftOuter)
			{
				return new IndexNestedJoinAlgorithm.IndexNestedJoinEnumerable(parameters);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06009AD9 RID: 39641 RVA: 0x00200AF8 File Offset: 0x001FECF8
		public static IEnumerator<IValueReference> Join(NestedJoinParameters parameters)
		{
			IEnumerator<IValueReference> enumerator = parameters.LeftQuery.GetRows().GetEnumerator();
			return new IndexNestedJoinAlgorithm.IndexNestedJoinEnumerator(parameters.RightTable, parameters.JoinColumns, parameters.LeftKeyColumns, parameters.RightKey, enumerator);
		}

		// Token: 0x020017F5 RID: 6133
		private class IndexNestedJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009ADB RID: 39643 RVA: 0x00200B34 File Offset: 0x001FED34
			public IndexNestedJoinEnumerable(NestedJoinParameters parameters)
			{
				this.parameters = parameters;
			}

			// Token: 0x06009ADC RID: 39644 RVA: 0x00200B43 File Offset: 0x001FED43
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009ADD RID: 39645 RVA: 0x00200B4B File Offset: 0x001FED4B
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return IndexNestedJoinAlgorithm.Join(this.parameters);
			}

			// Token: 0x040051EE RID: 20974
			private NestedJoinParameters parameters;
		}

		// Token: 0x020017F6 RID: 6134
		public class IndexNestedJoinEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06009ADE RID: 39646 RVA: 0x00200B58 File Offset: 0x001FED58
			public IndexNestedJoinEnumerator(Value rightTable, Keys joinColumns, int[] leftKeyColumns, Keys rightKey, IEnumerator<IValueReference> leftEnumerator)
			{
				this.rightTable = rightTable;
				this.joinColumns = joinColumns;
				this.leftKeyColumns = leftKeyColumns;
				this.rightKey = rightKey;
				this.leftEnumerator = leftEnumerator;
			}

			// Token: 0x170027D0 RID: 10192
			// (get) Token: 0x06009ADF RID: 39647 RVA: 0x00200B85 File Offset: 0x001FED85
			protected Value RightTable
			{
				get
				{
					return this.rightTable;
				}
			}

			// Token: 0x170027D1 RID: 10193
			// (get) Token: 0x06009AE0 RID: 39648 RVA: 0x00200B8D File Offset: 0x001FED8D
			protected int[] LeftKeyColumns
			{
				get
				{
					return this.leftKeyColumns;
				}
			}

			// Token: 0x06009AE1 RID: 39649 RVA: 0x00200B95 File Offset: 0x001FED95
			public virtual void Dispose()
			{
				this.leftEnumerator.Dispose();
			}

			// Token: 0x170027D2 RID: 10194
			// (get) Token: 0x06009AE2 RID: 39650 RVA: 0x00200BA2 File Offset: 0x001FEDA2
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009AE3 RID: 39651 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06009AE4 RID: 39652 RVA: 0x00200BAA File Offset: 0x001FEDAA
			protected virtual IValueReference GetNestedTableValue(RecordValue leftRow, RecordValue rightKeyValue)
			{
				return TypeServices.ConvertToLimitedPreview(new IndexNestedJoinAlgorithm.SelectRowsTableValue(this, rightKeyValue));
			}

			// Token: 0x170027D3 RID: 10195
			// (get) Token: 0x06009AE5 RID: 39653 RVA: 0x00200BB8 File Offset: 0x001FEDB8
			public IValueReference Current
			{
				get
				{
					if (this.current == null)
					{
						RecordValue asRecord = this.leftEnumerator.Current.Value.AsRecord;
						IValueReference[] array = new IValueReference[this.joinColumns.Length];
						for (int i = 0; i < this.joinColumns.Length - 1; i++)
						{
							array[i] = asRecord.GetReference(i);
						}
						Value[] array2 = new Value[this.leftKeyColumns.Length];
						for (int j = 0; j < this.leftKeyColumns.Length; j++)
						{
							array2[j] = asRecord[this.leftKeyColumns[j]];
						}
						RecordValue recordValue = RecordValue.New(this.rightKey, array2);
						array[this.joinColumns.Length - 1] = this.GetNestedTableValue(asRecord, recordValue);
						this.current = RecordValue.New(this.joinColumns, array);
					}
					return this.current;
				}
			}

			// Token: 0x06009AE6 RID: 39654 RVA: 0x00200C93 File Offset: 0x001FEE93
			public bool MoveNext()
			{
				this.current = null;
				return this.leftEnumerator.MoveNext();
			}

			// Token: 0x170027D4 RID: 10196
			// (get) Token: 0x06009AE7 RID: 39655 RVA: 0x00200CA8 File Offset: 0x001FEEA8
			public TableValue BaseTable
			{
				get
				{
					if (this.rightTable == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_CyclicReference, null, null);
					}
					if (this.rightTable.IsFunction)
					{
						Value value = this.rightTable;
						this.rightTable = null;
						this.rightTable = value.AsFunction.Invoke();
					}
					return this.rightTable.AsTable;
				}
			}

			// Token: 0x040051EF RID: 20975
			private Value rightTable;

			// Token: 0x040051F0 RID: 20976
			private Keys joinColumns;

			// Token: 0x040051F1 RID: 20977
			private int[] leftKeyColumns;

			// Token: 0x040051F2 RID: 20978
			private Keys rightKey;

			// Token: 0x040051F3 RID: 20979
			private IEnumerator<IValueReference> leftEnumerator;

			// Token: 0x040051F4 RID: 20980
			private RecordValue current;
		}

		// Token: 0x020017F7 RID: 6135
		private sealed class SelectRowsTableValue : TableValue
		{
			// Token: 0x06009AE8 RID: 39656 RVA: 0x00200D01 File Offset: 0x001FEF01
			public SelectRowsTableValue(IndexNestedJoinAlgorithm.IndexNestedJoinEnumerator enumerator, RecordValue key)
			{
				this.enumerator = enumerator;
				this.key = key;
			}

			// Token: 0x170027D5 RID: 10197
			// (get) Token: 0x06009AE9 RID: 39657 RVA: 0x00200D18 File Offset: 0x001FEF18
			private TableValue Table
			{
				get
				{
					if (this.key != null)
					{
						TableValue baseTable = this.enumerator.BaseTable;
						foreach (string text in this.key.Keys)
						{
							if (!baseTable.Columns.Contains(text))
							{
								throw ValueException.TableColumnNotFound(text);
							}
						}
						this.table = baseTable.SelectRows(this.key);
						this.key = null;
					}
					return this.table;
				}
			}

			// Token: 0x170027D6 RID: 10198
			// (get) Token: 0x06009AEA RID: 39658 RVA: 0x00200DB4 File Offset: 0x001FEFB4
			public override TypeValue Type
			{
				get
				{
					return this.enumerator.BaseTable.Type;
				}
			}

			// Token: 0x06009AEB RID: 39659 RVA: 0x00200DC6 File Offset: 0x001FEFC6
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.Table.GetEnumerator();
			}

			// Token: 0x040051F5 RID: 20981
			private IndexNestedJoinAlgorithm.IndexNestedJoinEnumerator enumerator;

			// Token: 0x040051F6 RID: 20982
			private RecordValue key;

			// Token: 0x040051F7 RID: 20983
			private TableValue table;
		}
	}
}
