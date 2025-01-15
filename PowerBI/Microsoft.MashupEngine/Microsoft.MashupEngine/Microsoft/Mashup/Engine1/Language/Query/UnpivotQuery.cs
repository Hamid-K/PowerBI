using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001845 RID: 6213
	internal class UnpivotQuery : Query
	{
		// Token: 0x06009D6B RID: 40299 RVA: 0x002083E6 File Offset: 0x002065E6
		public static Query New(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] toPivot, string attributeColumn, string valueColumn)
		{
			if (toPivot.Length == 0)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.InvalidArgument, ListValue.Empty, null);
			}
			return new UnpivotQuery(innerQuery, inputTableType, outputTableType, toPivot, attributeColumn, valueColumn);
		}

		// Token: 0x06009D6C RID: 40300 RVA: 0x0020840A File Offset: 0x0020660A
		private UnpivotQuery(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] toPivot, string attributeColumn, string valueColumn)
		{
			this.innerQuery = innerQuery;
			this.inputTableType = inputTableType;
			this.outputTableType = outputTableType;
			this.toPivot = toPivot;
			this.attributeColumn = attributeColumn;
			this.valueColumn = valueColumn;
		}

		// Token: 0x17002897 RID: 10391
		// (get) Token: 0x06009D6D RID: 40301 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Unpivot;
			}
		}

		// Token: 0x17002898 RID: 10392
		// (get) Token: 0x06009D6E RID: 40302 RVA: 0x0020843F File Offset: 0x0020663F
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x17002899 RID: 10393
		// (get) Token: 0x06009D6F RID: 40303 RVA: 0x00208447 File Offset: 0x00206647
		public override Keys Columns
		{
			get
			{
				return this.outputTableType.ItemType.FieldKeys;
			}
		}

		// Token: 0x06009D70 RID: 40304 RVA: 0x0020845C File Offset: 0x0020665C
		public override TypeValue GetColumnType(int column)
		{
			bool flag;
			return this.outputTableType.ItemType.GetFieldType(column, out flag);
		}

		// Token: 0x1700289A RID: 10394
		// (get) Token: 0x06009D71 RID: 40305 RVA: 0x0020847C File Offset: 0x0020667C
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.outputTableType.TableKeys;
			}
		}

		// Token: 0x1700289B RID: 10395
		// (get) Token: 0x06009D72 RID: 40306 RVA: 0x001DEDD7 File Offset: 0x001DCFD7
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.ComputedColumns.None;
			}
		}

		// Token: 0x1700289C RID: 10396
		// (get) Token: 0x06009D73 RID: 40307 RVA: 0x00049E54 File Offset: 0x00048054
		public override TableSortOrder SortOrder
		{
			get
			{
				return TableSortOrder.None;
			}
		}

		// Token: 0x1700289D RID: 10397
		// (get) Token: 0x06009D74 RID: 40308 RVA: 0x00208489 File Offset: 0x00206689
		public TableTypeValue InputType
		{
			get
			{
				return this.inputTableType;
			}
		}

		// Token: 0x1700289E RID: 10398
		// (get) Token: 0x06009D75 RID: 40309 RVA: 0x00208491 File Offset: 0x00206691
		public TableTypeValue OutputType
		{
			get
			{
				return this.outputTableType;
			}
		}

		// Token: 0x1700289F RID: 10399
		// (get) Token: 0x06009D76 RID: 40310 RVA: 0x00208499 File Offset: 0x00206699
		public string[] PivotValues
		{
			get
			{
				return this.toPivot;
			}
		}

		// Token: 0x170028A0 RID: 10400
		// (get) Token: 0x06009D77 RID: 40311 RVA: 0x002084A1 File Offset: 0x002066A1
		public string AttributeColumn
		{
			get
			{
				return this.attributeColumn;
			}
		}

		// Token: 0x170028A1 RID: 10401
		// (get) Token: 0x06009D78 RID: 40312 RVA: 0x002084A9 File Offset: 0x002066A9
		public string ValueColumn
		{
			get
			{
				return this.valueColumn;
			}
		}

		// Token: 0x06009D79 RID: 40313 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Query Unordered()
		{
			return this;
		}

		// Token: 0x170028A2 RID: 10402
		// (get) Token: 0x06009D7A RID: 40314 RVA: 0x002084B1 File Offset: 0x002066B1
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x002084BE File Offset: 0x002066BE
		public override IEnumerable<IValueReference> GetRows()
		{
			return new UnpivotQuery.UnpivotEnumerable(this.innerQuery.GetRows(), this.inputTableType, this.outputTableType, this.toPivot, this.attributeColumn, this.valueColumn);
		}

		// Token: 0x040052B8 RID: 21176
		private readonly Query innerQuery;

		// Token: 0x040052B9 RID: 21177
		private readonly TableTypeValue inputTableType;

		// Token: 0x040052BA RID: 21178
		private readonly TableTypeValue outputTableType;

		// Token: 0x040052BB RID: 21179
		private readonly string[] toPivot;

		// Token: 0x040052BC RID: 21180
		private readonly string attributeColumn;

		// Token: 0x040052BD RID: 21181
		private readonly string valueColumn;

		// Token: 0x02001846 RID: 6214
		private sealed class UnpivotEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009D7C RID: 40316 RVA: 0x002084EE File Offset: 0x002066EE
			public UnpivotEnumerable(IEnumerable<IValueReference> inputRows, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] toPivot, string attributeColumn, string valueColumn)
			{
				this.inputRows = inputRows;
				this.inputTableType = inputTableType;
				this.outputTableType = outputTableType;
				this.toPivot = toPivot;
				this.pivotColumnSet = new HashSet<string>(toPivot);
			}

			// Token: 0x06009D7D RID: 40317 RVA: 0x00208520 File Offset: 0x00206720
			public IEnumerator<IValueReference> GetEnumerator()
			{
				Keys fieldKeys = this.inputTableType.ItemType.FieldKeys;
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				List<TextValue> list3 = new List<TextValue>();
				for (int i = 0; i < fieldKeys.Length; i++)
				{
					if (this.pivotColumnSet.Contains(fieldKeys[i]))
					{
						list.Add(i);
						list3.Add(TextValue.New(fieldKeys[i]));
					}
					else
					{
						list2.Add(i);
					}
				}
				return new UnpivotQuery.UnpivotEnumerable.UnpivotTableEnumerator(this.inputRows.GetEnumerator(), this.outputTableType.ItemType.FieldKeys, list.ToArray(), list3.ToArray(), list2.ToArray());
			}

			// Token: 0x06009D7E RID: 40318 RVA: 0x002085D1 File Offset: 0x002067D1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040052BE RID: 21182
			private readonly IEnumerable<IValueReference> inputRows;

			// Token: 0x040052BF RID: 21183
			private readonly TableTypeValue inputTableType;

			// Token: 0x040052C0 RID: 21184
			private readonly TableTypeValue outputTableType;

			// Token: 0x040052C1 RID: 21185
			private readonly string[] toPivot;

			// Token: 0x040052C2 RID: 21186
			private readonly HashSet<string> pivotColumnSet;

			// Token: 0x02001847 RID: 6215
			private sealed class UnpivotTableEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009D7F RID: 40319 RVA: 0x002085D9 File Offset: 0x002067D9
				public UnpivotTableEnumerator(IEnumerator<IValueReference> inputEnumerator, Keys outputKeys, int[] pivotColumns, TextValue[] pivotColumnNames, int[] nonPivotColumns)
				{
					this.inputEnumerator = inputEnumerator;
					this.outputKeys = outputKeys;
					this.pivotColumns = pivotColumns;
					this.pivotColumnNames = pivotColumnNames;
					this.nonPivotColumns = nonPivotColumns;
					this.pivotColumnPosition = pivotColumns.Length - 1;
				}

				// Token: 0x170028A3 RID: 10403
				// (get) Token: 0x06009D80 RID: 40320 RVA: 0x00208614 File Offset: 0x00206814
				public IValueReference Current
				{
					get
					{
						if (this.currentOutputRecord == null)
						{
							int num = 0;
							IValueReference[] array = new IValueReference[this.outputKeys.Length];
							foreach (int num2 in this.nonPivotColumns)
							{
								array[num] = this.currentInputRecord[num2];
								num++;
							}
							array[num] = this.pivotColumnNames[this.pivotColumnPosition];
							array[num + 1] = this.currentPivotValue;
							this.currentOutputRecord = RecordValue.New(this.outputKeys, array);
						}
						return this.currentOutputRecord;
					}
				}

				// Token: 0x06009D81 RID: 40321 RVA: 0x0020869D File Offset: 0x0020689D
				public void Dispose()
				{
					this.inputEnumerator.Dispose();
				}

				// Token: 0x170028A4 RID: 10404
				// (get) Token: 0x06009D82 RID: 40322 RVA: 0x002086AA File Offset: 0x002068AA
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009D83 RID: 40323 RVA: 0x002086B2 File Offset: 0x002068B2
				public void Reset()
				{
					this.inputEnumerator.Reset();
				}

				// Token: 0x06009D84 RID: 40324 RVA: 0x002086C0 File Offset: 0x002068C0
				public bool MoveNext()
				{
					for (;;)
					{
						this.pivotColumnPosition++;
						if (this.pivotColumnPosition >= this.pivotColumns.Length)
						{
							if (!this.inputEnumerator.MoveNext())
							{
								break;
							}
							this.currentInputRecord = this.inputEnumerator.Current.Value.AsRecord;
							this.pivotColumnPosition = 0;
						}
						bool flag = false;
						try
						{
							this.currentPivotValue = this.currentInputRecord[this.pivotColumns[this.pivotColumnPosition]];
						}
						catch (ValueException ex)
						{
							this.currentPivotValue = new ExceptionValueReference(ex);
							flag = true;
						}
						if (flag || !this.currentPivotValue.Value.IsNull)
						{
							goto IL_009A;
						}
					}
					return false;
					IL_009A:
					this.currentOutputRecord = null;
					return true;
				}

				// Token: 0x040052C3 RID: 21187
				private readonly IEnumerator<IValueReference> inputEnumerator;

				// Token: 0x040052C4 RID: 21188
				private readonly Keys outputKeys;

				// Token: 0x040052C5 RID: 21189
				private readonly int[] pivotColumns;

				// Token: 0x040052C6 RID: 21190
				private readonly TextValue[] pivotColumnNames;

				// Token: 0x040052C7 RID: 21191
				private readonly int[] nonPivotColumns;

				// Token: 0x040052C8 RID: 21192
				private IValueReference currentPivotValue;

				// Token: 0x040052C9 RID: 21193
				private RecordValue currentInputRecord;

				// Token: 0x040052CA RID: 21194
				private RecordValue currentOutputRecord;

				// Token: 0x040052CB RID: 21195
				private int pivotColumnPosition;
			}
		}
	}
}
