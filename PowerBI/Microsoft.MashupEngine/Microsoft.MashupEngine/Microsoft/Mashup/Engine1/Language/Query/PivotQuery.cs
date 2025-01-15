using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001848 RID: 6216
	internal class PivotQuery : Query
	{
		// Token: 0x06009D85 RID: 40325 RVA: 0x00208780 File Offset: 0x00206980
		public static Query New(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] pivotValues, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
		{
			return new PivotQuery(innerQuery, inputTableType, outputTableType, pivotValues, attributeColumn, valueColumn, aggregateFunction);
		}

		// Token: 0x06009D86 RID: 40326 RVA: 0x00208794 File Offset: 0x00206994
		private PivotQuery(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] pivotValues, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
		{
			this.innerQuery = innerQuery;
			this.inputTableType = inputTableType;
			this.outputTableType = outputTableType;
			this.pivotValues = pivotValues;
			this.pivotColumnSet = new Dictionary<string, int>();
			for (int i = 0; i < pivotValues.Length; i++)
			{
				this.pivotColumnSet[pivotValues[i]] = i;
			}
			this.attributeColumn = attributeColumn;
			this.valueColumn = valueColumn;
			this.aggregateFunction = aggregateFunction;
		}

		// Token: 0x170028A5 RID: 10405
		// (get) Token: 0x06009D87 RID: 40327 RVA: 0x0006808E File Offset: 0x0006628E
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Pivot;
			}
		}

		// Token: 0x170028A6 RID: 10406
		// (get) Token: 0x06009D88 RID: 40328 RVA: 0x00208806 File Offset: 0x00206A06
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x170028A7 RID: 10407
		// (get) Token: 0x06009D89 RID: 40329 RVA: 0x0020880E File Offset: 0x00206A0E
		public override Keys Columns
		{
			get
			{
				return this.outputTableType.ItemType.FieldKeys;
			}
		}

		// Token: 0x170028A8 RID: 10408
		// (get) Token: 0x06009D8A RID: 40330 RVA: 0x00208820 File Offset: 0x00206A20
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.outputTableType.TableKeys;
			}
		}

		// Token: 0x170028A9 RID: 10409
		// (get) Token: 0x06009D8B RID: 40331 RVA: 0x001DEDD7 File Offset: 0x001DCFD7
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.ComputedColumns.None;
			}
		}

		// Token: 0x170028AA RID: 10410
		// (get) Token: 0x06009D8C RID: 40332 RVA: 0x00049E54 File Offset: 0x00048054
		public override TableSortOrder SortOrder
		{
			get
			{
				return TableSortOrder.None;
			}
		}

		// Token: 0x170028AB RID: 10411
		// (get) Token: 0x06009D8D RID: 40333 RVA: 0x0020882D File Offset: 0x00206A2D
		public TableTypeValue InputType
		{
			get
			{
				return this.inputTableType;
			}
		}

		// Token: 0x170028AC RID: 10412
		// (get) Token: 0x06009D8E RID: 40334 RVA: 0x00208835 File Offset: 0x00206A35
		public TableTypeValue OutputType
		{
			get
			{
				return this.outputTableType;
			}
		}

		// Token: 0x170028AD RID: 10413
		// (get) Token: 0x06009D8F RID: 40335 RVA: 0x0020883D File Offset: 0x00206A3D
		public string[] PivotValues
		{
			get
			{
				return this.pivotValues;
			}
		}

		// Token: 0x170028AE RID: 10414
		// (get) Token: 0x06009D90 RID: 40336 RVA: 0x00208845 File Offset: 0x00206A45
		public string AttributeColumn
		{
			get
			{
				return this.attributeColumn;
			}
		}

		// Token: 0x170028AF RID: 10415
		// (get) Token: 0x06009D91 RID: 40337 RVA: 0x0020884D File Offset: 0x00206A4D
		public string ValueColumn
		{
			get
			{
				return this.valueColumn;
			}
		}

		// Token: 0x170028B0 RID: 10416
		// (get) Token: 0x06009D92 RID: 40338 RVA: 0x00208855 File Offset: 0x00206A55
		public FunctionValue AggregateFunction
		{
			get
			{
				return this.aggregateFunction;
			}
		}

		// Token: 0x06009D93 RID: 40339 RVA: 0x00208860 File Offset: 0x00206A60
		public override TypeValue GetColumnType(int column)
		{
			bool flag;
			return this.outputTableType.ItemType.GetFieldType(column, out flag);
		}

		// Token: 0x06009D94 RID: 40340 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Query Unordered()
		{
			return this;
		}

		// Token: 0x170028B1 RID: 10417
		// (get) Token: 0x06009D95 RID: 40341 RVA: 0x00208880 File Offset: 0x00206A80
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009D96 RID: 40342 RVA: 0x0020888D File Offset: 0x00206A8D
		public override IEnumerable<IValueReference> GetRows()
		{
			return new PivotQuery.PivotEnumerable(this.innerQuery, this.inputTableType, this.outputTableType, this.pivotColumnSet, this.attributeColumn, this.valueColumn, this.aggregateFunction);
		}

		// Token: 0x040052CC RID: 21196
		private readonly Query innerQuery;

		// Token: 0x040052CD RID: 21197
		private readonly TableTypeValue inputTableType;

		// Token: 0x040052CE RID: 21198
		private readonly TableTypeValue outputTableType;

		// Token: 0x040052CF RID: 21199
		private readonly Dictionary<string, int> pivotColumnSet;

		// Token: 0x040052D0 RID: 21200
		private readonly string[] pivotValues;

		// Token: 0x040052D1 RID: 21201
		private readonly string attributeColumn;

		// Token: 0x040052D2 RID: 21202
		private readonly string valueColumn;

		// Token: 0x040052D3 RID: 21203
		private readonly FunctionValue aggregateFunction;

		// Token: 0x02001849 RID: 6217
		private sealed class PivotEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009D97 RID: 40343 RVA: 0x002088BE File Offset: 0x00206ABE
			public PivotEnumerable(Query inputQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, Dictionary<string, int> pivotColumnSet, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
			{
				this.inputQuery = inputQuery;
				this.inputTableType = inputTableType;
				this.outputTableType = outputTableType;
				this.pivotColumnSet = pivotColumnSet;
				this.attributeColumn = attributeColumn;
				this.valueColumn = valueColumn;
				this.aggregateFunction = aggregateFunction;
			}

			// Token: 0x06009D98 RID: 40344 RVA: 0x002088FC File Offset: 0x00206AFC
			public IEnumerator<IValueReference> GetEnumerator()
			{
				RecordValue fields = this.inputTableType.ItemType.Fields;
				Keys keys = fields.Keys;
				List<string> list = new List<string>();
				List<int> list2 = new List<int>();
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < keys.Length; i++)
				{
					string text = keys[i];
					if (text == this.attributeColumn)
					{
						num = i;
					}
					else if (text == this.valueColumn)
					{
						num2 = i;
					}
					else
					{
						ValueKind typeKind = fields[i]["Type"].AsType.TypeKind;
						if (typeKind == ValueKind.Table || typeKind == ValueKind.Record || typeKind == ValueKind.List)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.PivotColumn_NestedData_Error, null, null);
						}
						list.Add(keys[i]);
						list2.Add(i);
					}
				}
				return new PivotQuery.PivotEnumerable.PivotTableEnumerator(this.inputQuery.Sort(keys, ListValue.New(list.ToArray())).GetRows().GetEnumerator(), this.outputTableType.ItemType.FieldKeys, this.pivotColumnSet, list2.ToArray(), num, num2, this.aggregateFunction);
			}

			// Token: 0x06009D99 RID: 40345 RVA: 0x00208A21 File Offset: 0x00206C21
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040052D4 RID: 21204
			private readonly Query inputQuery;

			// Token: 0x040052D5 RID: 21205
			private readonly TableTypeValue inputTableType;

			// Token: 0x040052D6 RID: 21206
			private readonly TableTypeValue outputTableType;

			// Token: 0x040052D7 RID: 21207
			private readonly Dictionary<string, int> pivotColumnSet;

			// Token: 0x040052D8 RID: 21208
			private readonly string attributeColumn;

			// Token: 0x040052D9 RID: 21209
			private readonly string valueColumn;

			// Token: 0x040052DA RID: 21210
			private readonly FunctionValue aggregateFunction;

			// Token: 0x0200184A RID: 6218
			private sealed class PivotTableEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009D9A RID: 40346 RVA: 0x00208A2C File Offset: 0x00206C2C
				public PivotTableEnumerator(IEnumerator<IValueReference> inputEnumerator, Keys outputKeys, Dictionary<string, int> pivotStrings, int[] pivotKeyIndices, int attributeIndex, int valueIndex, FunctionValue aggregateFunction)
				{
					this.inputEnumerator = inputEnumerator;
					this.outputKeys = outputKeys;
					this.pivotStrings = pivotStrings;
					this.pivotKeyIndices = pivotKeyIndices;
					this.attributeIndex = attributeIndex;
					this.valueIndex = valueIndex;
					this.aggregateFunction = aggregateFunction;
					this.endOfStream = !inputEnumerator.MoveNext();
				}

				// Token: 0x170028B2 RID: 10418
				// (get) Token: 0x06009D9B RID: 40347 RVA: 0x00208A84 File Offset: 0x00206C84
				public IValueReference Current
				{
					get
					{
						if (this.currentOutputValue == null)
						{
							if (!this.startedRead)
							{
								throw new InvalidOperationException();
							}
							this.ProducePivotValues();
							for (int i = 0; i < this.pivotValues.Length; i++)
							{
								try
								{
									this.outputValues[i + this.pivotKeyIndices.Length] = this.aggregateFunction.Invoke(ListValue.New(this.pivotValues[i].ToArray()));
								}
								catch (ValueException ex)
								{
									this.outputValues[i + this.pivotKeyIndices.Length] = new ExceptionValueReference(ex);
								}
							}
							this.currentOutputValue = RecordValue.New(this.outputKeys, this.outputValues.ToArray<IValueReference>());
						}
						return this.currentOutputValue;
					}
				}

				// Token: 0x06009D9C RID: 40348 RVA: 0x00208B40 File Offset: 0x00206D40
				private void ProducePivotValues()
				{
					RecordValue recordValue = this.inputEnumerator.Current.Value.AsRecord;
					for (int i = 0; i < this.pivotStrings.Count; i++)
					{
						this.pivotValues[i] = new List<IValueReference>();
					}
					for (int j = 0; j < this.pivotKeyIndices.Length; j++)
					{
						this.outputValues[j] = recordValue[this.pivotKeyIndices[j]];
					}
					do
					{
						recordValue = this.inputEnumerator.Current.Value.AsRecord;
						Value value = recordValue[this.attributeIndex];
						int num;
						if (value.IsText && this.pivotStrings.TryGetValue(value.AsString, out num))
						{
							this.pivotValues[num].Add(recordValue.GetReference(this.valueIndex));
						}
					}
					while (!this.MoveNextAndTestForBoundary());
				}

				// Token: 0x06009D9D RID: 40349 RVA: 0x00208C14 File Offset: 0x00206E14
				private bool MoveNextAndTestForBoundary()
				{
					if (!this.inputEnumerator.MoveNext())
					{
						this.endOfStream = true;
						return true;
					}
					RecordValue asRecord = this.inputEnumerator.Current.Value.AsRecord;
					for (int i = 0; i < this.pivotKeyIndices.Length; i++)
					{
						if (!asRecord[this.pivotKeyIndices[i]].Equals(this.outputValues[i]))
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06009D9E RID: 40350 RVA: 0x00208C80 File Offset: 0x00206E80
				public void Dispose()
				{
					this.outputValues = null;
					this.endOfStream = true;
					this.inputEnumerator.Dispose();
				}

				// Token: 0x170028B3 RID: 10419
				// (get) Token: 0x06009D9F RID: 40351 RVA: 0x00208C9B File Offset: 0x00206E9B
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009DA0 RID: 40352 RVA: 0x00208CA3 File Offset: 0x00206EA3
				public void Reset()
				{
					this.inputEnumerator.Reset();
				}

				// Token: 0x06009DA1 RID: 40353 RVA: 0x00208CB0 File Offset: 0x00206EB0
				public bool MoveNext()
				{
					if (this.endOfStream)
					{
						return false;
					}
					if (!this.startedRead)
					{
						this.startedRead = true;
					}
					else if (this.currentOutputValue == null)
					{
						this.ProducePivotValues();
						if (this.endOfStream)
						{
							return false;
						}
					}
					this.outputValues = new IValueReference[this.outputKeys.Length];
					this.pivotValues = new List<IValueReference>[this.pivotStrings.Count];
					this.currentOutputValue = null;
					return true;
				}

				// Token: 0x040052DB RID: 21211
				private readonly IEnumerator<IValueReference> inputEnumerator;

				// Token: 0x040052DC RID: 21212
				private readonly Keys outputKeys;

				// Token: 0x040052DD RID: 21213
				private readonly Dictionary<string, int> pivotStrings;

				// Token: 0x040052DE RID: 21214
				private readonly int[] pivotKeyIndices;

				// Token: 0x040052DF RID: 21215
				private readonly int attributeIndex;

				// Token: 0x040052E0 RID: 21216
				private readonly int valueIndex;

				// Token: 0x040052E1 RID: 21217
				private readonly FunctionValue aggregateFunction;

				// Token: 0x040052E2 RID: 21218
				private bool startedRead;

				// Token: 0x040052E3 RID: 21219
				private bool endOfStream;

				// Token: 0x040052E4 RID: 21220
				private IValueReference[] outputValues;

				// Token: 0x040052E5 RID: 21221
				private List<IValueReference>[] pivotValues;

				// Token: 0x040052E6 RID: 21222
				private RecordValue currentOutputValue;
			}
		}
	}
}
