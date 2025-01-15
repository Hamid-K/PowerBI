using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200023A RID: 570
	public sealed class ArrayDataViewBuilder
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00045E2C File Offset: 0x0004402C
		private int? RowCount
		{
			get
			{
				if (this._columns.Count == 0)
				{
					return null;
				}
				return new int?(this._columns[0].Length);
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00045E68 File Offset: 0x00044068
		public ArrayDataViewBuilder(IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("ArrayDataViewBuilder");
			this._columns = new List<ArrayDataViewBuilder.Column>();
			this._names = new List<string>();
			this._slotNames = new Dictionary<string, VBuffer<DvText>>();
			this._keyValues = new Dictionary<string, VBuffer<DvText>>();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00045EC4 File Offset: 0x000440C4
		private void CheckLength<T>(string name, T[] values)
		{
			Contracts.CheckValue<string>(this._host, name, "name");
			Contracts.CheckValue<T[]>(this._host, values, "values");
			if (this._columns.Count > 0 && values.Length != this._columns[0].Length)
			{
				throw Contracts.Except(this._host, "Previous inputs were of length {0}, but new input is of length {1}", new object[]
				{
					this._columns[0].Length,
					values.Length
				});
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00045F54 File Offset: 0x00044154
		public void AddColumn<T>(string name, PrimitiveType type, params T[] values)
		{
			Contracts.CheckParam(this._host, type != null && type.RawType == typeof(T), "type");
			this.CheckLength<T>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.AssignmentColumn<T>(type, values));
			this._names.Add(name);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00045FB4 File Offset: 0x000441B4
		public void AddColumn(string name, ref VBuffer<DvText> keyValues, ulong keyMin, int keyCount, params uint[] values)
		{
			Contracts.CheckParam(this._host, keyCount > 0, "type");
			this.CheckLength<uint>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.AssignmentColumn<uint>(new KeyType(6, keyMin, keyCount, true), values));
			this._keyValues.Add(name, keyValues);
			this._names.Add(name);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0004601C File Offset: 0x0004421C
		public void AddColumn<T>(string name, ref VBuffer<DvText> names, PrimitiveType itemType, params T[][] values)
		{
			Contracts.CheckParam(this._host, itemType != null && itemType.RawType == typeof(T), "itemType");
			this.CheckLength<T[]>(name, values);
			ArrayDataViewBuilder.ArrayToVBufferColumn<T> arrayToVBufferColumn = new ArrayDataViewBuilder.ArrayToVBufferColumn<T>(itemType, values);
			this._columns.Add(arrayToVBufferColumn);
			this._slotNames.Add(name, names);
			this._names.Add(name);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00046090 File Offset: 0x00044290
		public void AddColumn<T>(string name, PrimitiveType itemType, params T[][] values)
		{
			Contracts.CheckParam(this._host, itemType != null && itemType.RawType == typeof(T), "itemType");
			this.CheckLength<T[]>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.ArrayToVBufferColumn<T>(itemType, values));
			this._names.Add(name);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000460F0 File Offset: 0x000442F0
		public void AddColumn<T>(string name, ref VBuffer<DvText> names, PrimitiveType itemType, Combiner<T> combiner, params T[][] values)
		{
			Contracts.CheckParam(this._host, itemType != null && itemType.RawType == typeof(T), "itemType");
			this.CheckLength<T[]>(name, values);
			ArrayDataViewBuilder.ArrayToSparseVBufferColumn<T> arrayToSparseVBufferColumn = new ArrayDataViewBuilder.ArrayToSparseVBufferColumn<T>(itemType, combiner, values);
			this._columns.Add(arrayToSparseVBufferColumn);
			this._slotNames.Add(name, names);
			this._names.Add(name);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00046168 File Offset: 0x00044368
		public void AddColumn<T>(string name, PrimitiveType itemType, Combiner<T> combiner, params T[][] values)
		{
			Contracts.CheckParam(this._host, itemType != null && itemType.RawType == typeof(T), "itemType");
			this.CheckLength<T[]>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.ArrayToSparseVBufferColumn<T>(itemType, combiner, values));
			this._names.Add(name);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000461CC File Offset: 0x000443CC
		public void AddColumn<T>(string name, PrimitiveType itemType, params VBuffer<T>[] values)
		{
			Contracts.CheckParam(this._host, itemType != null && itemType.RawType == typeof(T), "itemType");
			this.CheckLength<VBuffer<T>>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.VBufferColumn<T>(itemType, values));
			this._names.Add(name);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0004622C File Offset: 0x0004442C
		public void AddColumn<T>(string name, ref VBuffer<DvText> names, PrimitiveType itemType, params VBuffer<T>[] values)
		{
			Contracts.CheckParam(this._host, itemType != null && itemType.RawType == typeof(T), "itemType");
			this.CheckLength<VBuffer<T>>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.VBufferColumn<T>(itemType, values));
			this._slotNames.Add(name, names);
			this._names.Add(name);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0004629E File Offset: 0x0004449E
		public void AddColumn(string name, params string[] values)
		{
			this.CheckLength<string>(name, values);
			this._columns.Add(new ArrayDataViewBuilder.StringToTextColumn(values));
			this._names.Add(name);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000462C8 File Offset: 0x000444C8
		public IDataView GetDataView(int? rowCount = null)
		{
			if (rowCount != null)
			{
				Contracts.Check(this._host, this.RowCount == null || this.RowCount.Value == rowCount.Value, "Specified row count incompatible with existing columns");
				return new ArrayDataViewBuilder.DataView(this._host, this, rowCount.Value);
			}
			Contracts.Check(this._host, this._columns.Count > 0, "Cannot construct data-view with neither any columns nor a specified row count");
			return new ArrayDataViewBuilder.DataView(this._host, this, this.RowCount.Value);
		}

		// Token: 0x04000703 RID: 1795
		private readonly IHost _host;

		// Token: 0x04000704 RID: 1796
		private readonly List<ArrayDataViewBuilder.Column> _columns;

		// Token: 0x04000705 RID: 1797
		private readonly List<string> _names;

		// Token: 0x04000706 RID: 1798
		private readonly Dictionary<string, VBuffer<DvText>> _slotNames;

		// Token: 0x04000707 RID: 1799
		private readonly Dictionary<string, VBuffer<DvText>> _keyValues;

		// Token: 0x0200023B RID: 571
		private sealed class DataView : IDataView, ISchematized
		{
			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00046364 File Offset: 0x00044564
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x0004636C File Offset: 0x0004456C
			public long? GetRowCount(bool lazy = true)
			{
				return new long?((long)this._rowCount);
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0004637A File Offset: 0x0004457A
			public bool CanShuffle
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x00046388 File Offset: 0x00044588
			public DataView(IHostEnvironment env, ArrayDataViewBuilder builder, int rowCount)
			{
				this._host = env.Register("ArrayDataView");
				this._columns = builder._columns.ToArray();
				this._schema = new ArrayDataViewBuilder.DataView.SchemaImpl(this._host, this._columns.Select((ArrayDataViewBuilder.Column c) => c.Type).ToArray<ColumnType>(), builder._names.ToArray(), builder);
				this._rowCount = rowCount;
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x0004640E File Offset: 0x0004460E
			public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				return new ArrayDataViewBuilder.DataView.RowCursor(this._host, this, predicate, rand);
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x00046430 File Offset: 0x00044630
			public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				consolidator = null;
				return new IRowCursor[]
				{
					new ArrayDataViewBuilder.DataView.RowCursor(this._host, this, predicate, rand)
				};
			}

			// Token: 0x04000708 RID: 1800
			private readonly int _rowCount;

			// Token: 0x04000709 RID: 1801
			private readonly ArrayDataViewBuilder.Column[] _columns;

			// Token: 0x0400070A RID: 1802
			private readonly ArrayDataViewBuilder.DataView.SchemaImpl _schema;

			// Token: 0x0400070B RID: 1803
			private readonly IHost _host;

			// Token: 0x0200023C RID: 572
			private class SchemaImpl : ISchema
			{
				// Token: 0x06000CE7 RID: 3303 RVA: 0x0004646C File Offset: 0x0004466C
				public SchemaImpl(IExceptionContext ectx, ColumnType[] columnTypes, string[] names, ArrayDataViewBuilder builder)
				{
					this._ectx = ectx;
					this._columnTypes = columnTypes;
					this._names = names;
					this._name2col = new Dictionary<string, int>();
					for (int i = 0; i < this._names.Length; i++)
					{
						this._name2col[this._names[i]] = i;
					}
					this._slotNamesDict = builder._slotNames;
					this._keyValuesDict = builder._keyValues;
				}

				// Token: 0x17000179 RID: 377
				// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x000464E0 File Offset: 0x000446E0
				public int ColumnCount
				{
					get
					{
						return this._columnTypes.Length;
					}
				}

				// Token: 0x06000CE9 RID: 3305 RVA: 0x000464EA File Offset: 0x000446EA
				public string GetColumnName(int col)
				{
					Contracts.CheckParam(this._ectx, (0 <= col) & (col < this.ColumnCount), "col");
					return this._names[col];
				}

				// Token: 0x06000CEA RID: 3306 RVA: 0x00046515 File Offset: 0x00044715
				public ColumnType GetColumnType(int col)
				{
					Contracts.CheckParam(this._ectx, (0 <= col) & (col < this.ColumnCount), "col");
					return this._columnTypes[col];
				}

				// Token: 0x06000CEB RID: 3307 RVA: 0x00046540 File Offset: 0x00044740
				public bool TryGetColumnIndex(string name, out int col)
				{
					if (name == null)
					{
						col = 0;
						return false;
					}
					return this._name2col.TryGetValue(name, out col);
				}

				// Token: 0x06000CEC RID: 3308 RVA: 0x000466FC File Offset: 0x000448FC
				public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
				{
					Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
					if (this._slotNamesDict.ContainsKey(this._names[col]))
					{
						yield return MetadataUtils.GetSlotNamesPair(this._columnTypes[col].VectorSize);
					}
					if (this._keyValuesDict.ContainsKey(this._names[col]))
					{
						yield return MetadataUtils.GetKeyNamesPair(this._columnTypes[col].VectorSize);
					}
					yield break;
				}

				// Token: 0x06000CED RID: 3309 RVA: 0x00046720 File Offset: 0x00044920
				public ColumnType GetMetadataTypeOrNull(string kind, int col)
				{
					Contracts.CheckNonEmpty(this._ectx, kind, "kind");
					Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
					if (this._slotNamesDict.ContainsKey(this._names[col]))
					{
						return MetadataUtils.GetNamesType(this._columnTypes[col].VectorSize);
					}
					if (this._keyValuesDict.ContainsKey(this._names[col]))
					{
						return MetadataUtils.GetNamesType(this._columnTypes[col].KeyCount);
					}
					return null;
				}

				// Token: 0x06000CEE RID: 3310 RVA: 0x000467B0 File Offset: 0x000449B0
				public void GetMetadata<TValue>(string kind, int col, ref TValue value)
				{
					Contracts.CheckNonEmpty(this._ectx, kind, "kind");
					Contracts.CheckParam(this._ectx, 0 <= col && col < this.ColumnCount, "col");
					if (kind == "SlotNames" && this._slotNamesDict.ContainsKey(this._names[col]))
					{
						MetadataUtils.Marshal<VBuffer<DvText>, TValue>(new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames), col, ref value);
						return;
					}
					if (kind == "KeyValues" && this._keyValuesDict.ContainsKey(this._names[col]))
					{
						MetadataUtils.Marshal<VBuffer<DvText>, TValue>(new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetKeyValues), col, ref value);
						return;
					}
					throw MetadataUtils.ExceptGetMetadata();
				}

				// Token: 0x06000CEF RID: 3311 RVA: 0x00046860 File Offset: 0x00044A60
				private void GetSlotNames(int col, ref VBuffer<DvText> dst)
				{
					this._slotNamesDict.TryGetValue(this._names[col], out dst);
				}

				// Token: 0x06000CF0 RID: 3312 RVA: 0x00046877 File Offset: 0x00044A77
				private void GetKeyValues(int col, ref VBuffer<DvText> dst)
				{
					this._keyValuesDict.TryGetValue(this._names[col], out dst);
				}

				// Token: 0x0400070D RID: 1805
				private readonly IExceptionContext _ectx;

				// Token: 0x0400070E RID: 1806
				private readonly ColumnType[] _columnTypes;

				// Token: 0x0400070F RID: 1807
				private readonly string[] _names;

				// Token: 0x04000710 RID: 1808
				private readonly Dictionary<string, int> _name2col;

				// Token: 0x04000711 RID: 1809
				private readonly Dictionary<string, VBuffer<DvText>> _slotNamesDict;

				// Token: 0x04000712 RID: 1810
				private readonly Dictionary<string, VBuffer<DvText>> _keyValuesDict;
			}

			// Token: 0x0200023D RID: 573
			private sealed class RowCursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
			{
				// Token: 0x1700017A RID: 378
				// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0004688E File Offset: 0x00044A8E
				public ISchema Schema
				{
					get
					{
						return this._view.Schema;
					}
				}

				// Token: 0x1700017B RID: 379
				// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0004689B File Offset: 0x00044A9B
				public override long Batch
				{
					get
					{
						return 0L;
					}
				}

				// Token: 0x06000CF3 RID: 3315 RVA: 0x000468A0 File Offset: 0x00044AA0
				public RowCursor(IChannelProvider provider, ArrayDataViewBuilder.DataView view, Func<int, bool> predicate, IRandom rand)
					: base(provider)
				{
					this._view = view;
					this._active = new BitArray(view.Schema.ColumnCount);
					if (predicate == null)
					{
						this._active.SetAll(true);
					}
					else
					{
						for (int i = 0; i < view.Schema.ColumnCount; i++)
						{
							this._active[i] = predicate(i);
						}
					}
					if (rand != null)
					{
						this._indices = Utils.GetRandomPermutation(rand, view._rowCount);
					}
				}

				// Token: 0x06000CF4 RID: 3316 RVA: 0x0004697C File Offset: 0x00044B7C
				public override ValueGetter<UInt128> GetIdGetter()
				{
					if (this._indices == null)
					{
						return delegate(ref UInt128 val)
						{
							Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
							val = new UInt128((ulong)base.Position, 0UL);
						};
					}
					return delegate(ref UInt128 val)
					{
						Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
						val = new UInt128((ulong)((long)this.MappedIndex()), 0UL);
					};
				}

				// Token: 0x06000CF5 RID: 3317 RVA: 0x000469B8 File Offset: 0x00044BB8
				public bool IsColumnActive(int col)
				{
					Contracts.Check(this._ch, (0 <= col) & (col < this.Schema.ColumnCount));
					return this._active[col];
				}

				// Token: 0x06000CF6 RID: 3318 RVA: 0x00046A24 File Offset: 0x00044C24
				public ValueGetter<TValue> GetGetter<TValue>(int col)
				{
					Contracts.Check(this._ch, (0 <= col) & (col < this.Schema.ColumnCount));
					Contracts.Check(this._ch, this._active[col], "column is not active");
					ArrayDataViewBuilder.Column<TValue> column = this._view._columns[col] as ArrayDataViewBuilder.Column<TValue>;
					if (column == null)
					{
						throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
					}
					return delegate(ref TValue value)
					{
						Contracts.Check(this._ch, this.IsGood);
						column.CopyOut(this.MappedIndex(), ref value);
					};
				}

				// Token: 0x06000CF7 RID: 3319 RVA: 0x00046ACD File Offset: 0x00044CCD
				protected override bool MoveNextCore()
				{
					return 1L < (long)this._view._rowCount - base.Position;
				}

				// Token: 0x06000CF8 RID: 3320 RVA: 0x00046AE6 File Offset: 0x00044CE6
				protected override bool MoveManyCore(long count)
				{
					return count < (long)this._view._rowCount - base.Position;
				}

				// Token: 0x06000CF9 RID: 3321 RVA: 0x00046AFE File Offset: 0x00044CFE
				private int MappedIndex()
				{
					if (this._indices == null)
					{
						return (int)base.Position;
					}
					return this._indices[(int)base.Position];
				}

				// Token: 0x04000713 RID: 1811
				private readonly ArrayDataViewBuilder.DataView _view;

				// Token: 0x04000714 RID: 1812
				private readonly BitArray _active;

				// Token: 0x04000715 RID: 1813
				private readonly int[] _indices;
			}
		}

		// Token: 0x0200023E RID: 574
		private abstract class Column
		{
			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000CFC RID: 3324
			public abstract int Length { get; }

			// Token: 0x06000CFD RID: 3325 RVA: 0x00046B1E File Offset: 0x00044D1E
			public Column(ColumnType type)
			{
				this.Type = type;
			}

			// Token: 0x04000716 RID: 1814
			public readonly ColumnType Type;
		}

		// Token: 0x0200023F RID: 575
		private abstract class Column<TOut> : ArrayDataViewBuilder.Column
		{
			// Token: 0x06000CFE RID: 3326
			public abstract void CopyOut(int index, ref TOut value);

			// Token: 0x06000CFF RID: 3327 RVA: 0x00046B2D File Offset: 0x00044D2D
			public Column(ColumnType type)
				: base(type)
			{
			}
		}

		// Token: 0x02000240 RID: 576
		private abstract class Column<TIn, TOut> : ArrayDataViewBuilder.Column<TOut>
		{
			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00046B36 File Offset: 0x00044D36
			public override int Length
			{
				get
				{
					return this._values.Length;
				}
			}

			// Token: 0x06000D01 RID: 3329 RVA: 0x00046B40 File Offset: 0x00044D40
			public Column(ColumnType type, TIn[] values)
				: base(type)
			{
				this._values = values;
			}

			// Token: 0x06000D02 RID: 3330
			protected abstract void CopyOut(ref TIn src, ref TOut dst);

			// Token: 0x06000D03 RID: 3331 RVA: 0x00046B50 File Offset: 0x00044D50
			public override void CopyOut(int index, ref TOut value)
			{
				this.CopyOut(ref this._values[index], ref value);
			}

			// Token: 0x04000717 RID: 1815
			private readonly TIn[] _values;
		}

		// Token: 0x02000241 RID: 577
		private sealed class AssignmentColumn<T> : ArrayDataViewBuilder.Column<T, T>
		{
			// Token: 0x06000D04 RID: 3332 RVA: 0x00046B65 File Offset: 0x00044D65
			public AssignmentColumn(PrimitiveType type, T[] values)
				: base(type, values)
			{
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x00046B6F File Offset: 0x00044D6F
			protected override void CopyOut(ref T src, ref T dst)
			{
				dst = src;
			}
		}

		// Token: 0x02000242 RID: 578
		private sealed class StringToTextColumn : ArrayDataViewBuilder.Column<string, DvText>
		{
			// Token: 0x06000D06 RID: 3334 RVA: 0x00046B7D File Offset: 0x00044D7D
			public StringToTextColumn(string[] values)
				: base(TextType.Instance, values)
			{
			}

			// Token: 0x06000D07 RID: 3335 RVA: 0x00046B8B File Offset: 0x00044D8B
			protected override void CopyOut(ref string src, ref DvText dst)
			{
				dst = new DvText(src);
			}
		}

		// Token: 0x02000243 RID: 579
		private abstract class VectorColumn<TIn, TOut> : ArrayDataViewBuilder.Column<TIn, VBuffer<TOut>>
		{
			// Token: 0x06000D08 RID: 3336 RVA: 0x00046B9A File Offset: 0x00044D9A
			public VectorColumn(PrimitiveType itemType, TIn[] values, Func<TIn, int> lengthFunc)
				: base(ArrayDataViewBuilder.VectorColumn<TIn, TOut>.InferType(itemType, values, lengthFunc), values)
			{
			}

			// Token: 0x06000D09 RID: 3337 RVA: 0x00046BAC File Offset: 0x00044DAC
			private static ColumnType InferType(PrimitiveType itemType, TIn[] values, Func<TIn, int> lengthFunc)
			{
				int num = 0;
				if (Utils.Size<TIn>(values) > 0)
				{
					num = lengthFunc(values[0]);
					for (int i = 1; i < values.Length; i++)
					{
						if (num != lengthFunc(values[i]))
						{
							num = 0;
							break;
						}
					}
				}
				return new VectorType(itemType, num);
			}
		}

		// Token: 0x02000244 RID: 580
		private sealed class VBufferColumn<T> : ArrayDataViewBuilder.VectorColumn<VBuffer<T>, T>
		{
			// Token: 0x06000D0A RID: 3338 RVA: 0x00046C04 File Offset: 0x00044E04
			public VBufferColumn(PrimitiveType itemType, VBuffer<T>[] values)
				: base(itemType, values, (VBuffer<T> v) => v.Length)
			{
			}

			// Token: 0x06000D0B RID: 3339 RVA: 0x00046C2B File Offset: 0x00044E2B
			protected override void CopyOut(ref VBuffer<T> src, ref VBuffer<T> dst)
			{
				src.CopyTo(ref dst);
			}
		}

		// Token: 0x02000245 RID: 581
		private sealed class ArrayToVBufferColumn<T> : ArrayDataViewBuilder.VectorColumn<T[], T>
		{
			// Token: 0x06000D0D RID: 3341 RVA: 0x00046C34 File Offset: 0x00044E34
			public ArrayToVBufferColumn(PrimitiveType itemType, T[][] values)
				: base(itemType, values, new Func<T[], int>(Utils.Size<T>))
			{
			}

			// Token: 0x06000D0E RID: 3342 RVA: 0x00046C4A File Offset: 0x00044E4A
			protected override void CopyOut(ref T[] src, ref VBuffer<T> dst)
			{
				VBuffer<T>.Copy(src, 0, ref dst, Utils.Size<T>(src));
			}
		}

		// Token: 0x02000246 RID: 582
		private sealed class ArrayToSparseVBufferColumn<T> : ArrayDataViewBuilder.VectorColumn<T[], T>
		{
			// Token: 0x06000D0F RID: 3343 RVA: 0x00046C5C File Offset: 0x00044E5C
			public ArrayToSparseVBufferColumn(PrimitiveType itemType, Combiner<T> combiner, T[][] values)
				: base(itemType, values, new Func<T[], int>(Utils.Size<T>))
			{
				this._bldr = new VBufferBuilder<T>(combiner);
			}

			// Token: 0x06000D10 RID: 3344 RVA: 0x00046C80 File Offset: 0x00044E80
			protected override void CopyOut(ref T[] src, ref VBuffer<T> dst)
			{
				int num = Utils.Size<T>(src);
				this._bldr.Reset(num, false);
				for (int i = 0; i < num; i++)
				{
					this._bldr.AddFeature(i, src[i]);
				}
				this._bldr.GetResult(ref dst);
			}

			// Token: 0x04000719 RID: 1817
			private readonly VBufferBuilder<T> _bldr;
		}
	}
}
