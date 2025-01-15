using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000164 RID: 356
	public sealed class Transposer : ITransposeDataView, IDataView, ISchematized, IDisposable
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00026177 File Offset: 0x00024377
		public ITransposeSchema TransposeSchema
		{
			get
			{
				return this._tschema;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00026180 File Offset: 0x00024380
		public static Transposer Create(IHostEnvironment env, IDataView view, bool forceSave, params string[] columns)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Transposer");
			Contracts.CheckValue<IDataView>(host, view, "view");
			int[] array = Transposer.CheckNamesAndGetIndices(host, view, columns);
			return new Transposer(host, view, forceSave, array);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000261C4 File Offset: 0x000243C4
		public static Transposer Create(IHostEnvironment env, IDataView view, bool forceSave, params int[] columns)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Transposer");
			Contracts.CheckValue<IDataView>(host, view, "view");
			int[] array = Transposer.CheckIndices(host, view, columns);
			return new Transposer(host, view, forceSave, array);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00026224 File Offset: 0x00024424
		private Transposer(IHost host, IDataView view, bool forceSave, int[] columns)
		{
			this._host = host;
			Contracts.CheckParam(this._host, Utils.Size<int>(columns) > 0, "columns", "Cannot be empty");
			this._view = view;
			this._tview = this._view as ITransposeDataView;
			IEnumerable<int> enumerable = from c in columns.Distinct<int>()
				orderby c
				select c;
			if (this._tview != null)
			{
				ITransposeSchema ttschema = this._tview.TransposeSchema;
				enumerable = enumerable.Where((int c) => ttschema.GetSlotType(c) == null);
			}
			columns = enumerable.ToArray<int>();
			this._cols = new ColumnInfo[columns.Length];
			ISchema schema = this._view.Schema;
			this._nameToICol = new Dictionary<string, int>();
			this._inputToTransposed = Utils.CreateArray<int>(schema.ColumnCount, -1);
			for (int i = 0; i < columns.Length; i++)
			{
				this._nameToICol[(this._cols[i] = ColumnInfo.CreateFromIndex(schema, columns[i])).Name] = i;
				this._inputToTransposed[columns[i]] = i;
			}
			using (IChannel channel = this._host.Start("Init"))
			{
				BinarySaver binarySaver = new BinarySaver(new BinarySaver.Arguments
				{
					compression = new SubComponent<Compression, SignatureCompression>("deflate", new string[] { "c=2" }),
					maxBytesPerBlock = new long?(268435456L),
					silent = true
				}, this._host);
				for (int j = 0; j < this._cols.Length; j++)
				{
					ColumnType columnType = schema.GetColumnType(this._cols[j].Index);
					if (!binarySaver.IsColumnSavable(columnType))
					{
						throw Contracts.ExceptParam(channel, "view", "Column named '{0}' is not serializable by the transposer", new object[] { this._cols[j].Name });
					}
					if (columnType.IsVector && !columnType.IsKnownSizeVector)
					{
						throw Contracts.ExceptParam(channel, "view", "Column named '{0}' is vector, but not of known size, and so cannot be transposed", new object[] { this._cols[j].Name });
					}
				}
				Transposer.DataViewSlicer dataViewSlicer = new Transposer.DataViewSlicer(this._host, view, columns);
				ISchema schema2 = dataViewSlicer.Schema;
				this._splitLim = new int[this._cols.Length];
				List<int> list = new List<int>();
				int num = 0;
				int num2 = 0;
				for (int k = 0; k < this._cols.Length; k++)
				{
					int num3;
					int num4;
					dataViewSlicer.InColToOutRange(k, out num3, out num4);
					int num5 = num4 - num3;
					if (forceSave || num5 > 1)
					{
						list.AddRange(Enumerable.Range(num3, num5));
						num2++;
						num += num5;
					}
					this._splitLim[k] = num;
				}
				channel.Trace("{0} of {1} input columns sliced into {2} columns", new object[]
				{
					num2,
					this._cols.Length,
					list.Count
				});
				long num6;
				if (list.Count > 0)
				{
					HybridMemoryStream hybridMemoryStream = new HybridMemoryStream(1073741824);
					binarySaver.SaveData(hybridMemoryStream, dataViewSlicer, list.ToArray());
					hybridMemoryStream.Seek(0L, SeekOrigin.Begin);
					channel.Trace("Sliced data saved to {0} bytes", new object[] { hybridMemoryStream.Length });
					BinaryLoader.Arguments arguments = new BinaryLoader.Arguments();
					this._splitView = new BinaryLoader(arguments, this._host, hybridMemoryStream, false);
					num6 = DataViewUtils.ComputeRowCount(this._splitView);
				}
				else
				{
					num6 = DataViewUtils.ComputeRowCount(this._view);
				}
				if (num6 > 2146435071L)
				{
					throw Contracts.ExceptParam(this._host, "view", "View has {0} rows, we cannot transpose with more than {1}", new object[] { num6, 2146435071 });
				}
				this.RowCount = (int)num6;
				this._tschema = new Transposer.SchemaImpl(this);
				channel.Done();
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00026658 File Offset: 0x00024858
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (this._splitView != null)
				{
					this._splitView.Dispose();
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0002667C File Offset: 0x0002487C
		private static int[] CheckNamesAndGetIndices(IHost host, IDataView view, string[] columns)
		{
			Contracts.CheckParam(host, Utils.Size<string>(columns) > 0, "columns", "Cannot be empty");
			ISchema schema = view.Schema;
			int[] array = new int[columns.Length];
			for (int i = 0; i < columns.Length; i++)
			{
				if (!schema.TryGetColumnIndex(columns[i], ref array[i]))
				{
					throw Contracts.ExceptParam(host, "columns", "Column named '{0}' not found", new object[] { columns[i] });
				}
			}
			return array;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000266F4 File Offset: 0x000248F4
		private static int[] CheckIndices(IHost host, IDataView view, int[] columns)
		{
			ISchema schema = view.Schema;
			for (int i = 0; i < columns.Length; i++)
			{
				if (0 > columns[i] || columns[i] >= schema.ColumnCount)
				{
					throw Contracts.ExceptParam(host, "columns", "Column index {0} illegal for data with {1} column", new object[]
					{
						columns[i],
						schema.ColumnCount
					});
				}
			}
			return columns;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0002675C File Offset: 0x0002495C
		public ISlotCursor GetSlotCursor(int col)
		{
			Contracts.CheckParam(this._host, 0 <= col && col < this._tschema.ColumnCount, "col");
			if (this._inputToTransposed[col] != -1)
			{
				Type rawType = this._tschema.GetSlotType(col).ItemType.RawType;
				int num = this._inputToTransposed[col];
				return Utils.MarshalInvoke<int, ISlotCursor>(new Func<int, ISlotCursor>(this.GetSlotCursorCore<int>), rawType, col);
			}
			if (this._tview != null && this._tview.TransposeSchema.GetSlotType(col) != null)
			{
				return this._tview.GetSlotCursor(col);
			}
			throw Contracts.ExceptParam(this._host, "col", "Bad call to GetSlotCursor on untransposable column '{0}'", new object[] { this._tschema.GetColumnName(col) });
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00026821 File Offset: 0x00024A21
		private ISlotCursor GetSlotCursorCore<T>(int col)
		{
			if (this._tschema.GetColumnType(col).IsVector)
			{
				return new Transposer.SlotCursorVec<T>(this, col);
			}
			return new Transposer.SlotCursorOne<T>(this, col);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00026845 File Offset: 0x00024A45
		public ISchema Schema
		{
			get
			{
				return this._view.Schema;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00026852 File Offset: 0x00024A52
		public bool CanShuffle
		{
			get
			{
				return this._view.CanShuffle;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0002685F File Offset: 0x00024A5F
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			return this._view.GetRowCursor(predicate, rand);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0002686E File Offset: 0x00024A6E
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			return this._view.GetRowCursorSet(ref consolidator, predicate, n, rand);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00026880 File Offset: 0x00024A80
		public long? GetRowCount(bool lazy = true)
		{
			return new long?((long)this.RowCount);
		}

		// Token: 0x040003AB RID: 939
		private readonly IHost _host;

		// Token: 0x040003AC RID: 940
		private readonly IDataView _view;

		// Token: 0x040003AD RID: 941
		private readonly ITransposeDataView _tview;

		// Token: 0x040003AE RID: 942
		private readonly Dictionary<string, int> _nameToICol;

		// Token: 0x040003AF RID: 943
		private readonly BinaryLoader _splitView;

		// Token: 0x040003B0 RID: 944
		public readonly int RowCount;

		// Token: 0x040003B1 RID: 945
		private readonly int[] _inputToTransposed;

		// Token: 0x040003B2 RID: 946
		private readonly ColumnInfo[] _cols;

		// Token: 0x040003B3 RID: 947
		private readonly int[] _splitLim;

		// Token: 0x040003B4 RID: 948
		private readonly Transposer.SchemaImpl _tschema;

		// Token: 0x040003B5 RID: 949
		private bool _disposed;

		// Token: 0x02000165 RID: 357
		private sealed class SchemaImpl : ITransposeSchema, ISchema
		{
			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000715 RID: 1813 RVA: 0x0002688E File Offset: 0x00024A8E
			private ISchema InputSchema
			{
				get
				{
					return this._parent._view.Schema;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000716 RID: 1814 RVA: 0x000268A0 File Offset: 0x00024AA0
			public int ColumnCount
			{
				get
				{
					return this.InputSchema.ColumnCount;
				}
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x000268B0 File Offset: 0x00024AB0
			public SchemaImpl(Transposer parent)
			{
				this._parent = parent;
				this._ectx = this._parent._host;
				this._slotTypes = new VectorType[this._parent._cols.Length];
				for (int i = 0; i < this._slotTypes.Length; i++)
				{
					ColumnInfo columnInfo = this._parent._cols[i];
					ColumnType itemType = columnInfo.Type.ItemType;
					this._slotTypes[i] = new VectorType(itemType.AsPrimitive, this._parent.RowCount);
				}
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x0002693E File Offset: 0x00024B3E
			public bool TryGetColumnIndex(string name, out int col)
			{
				return this.InputSchema.TryGetColumnIndex(name, ref col);
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x0002694D File Offset: 0x00024B4D
			public string GetColumnName(int col)
			{
				return this.InputSchema.GetColumnName(col);
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x0002695B File Offset: 0x00024B5B
			public ColumnType GetColumnType(int col)
			{
				return this.InputSchema.GetColumnType(col);
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x00026969 File Offset: 0x00024B69
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				return this.InputSchema.GetMetadataTypes(col);
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x00026977 File Offset: 0x00024B77
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				return this.InputSchema.GetMetadataTypeOrNull(kind, col);
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x00026986 File Offset: 0x00024B86
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				this.InputSchema.GetMetadata<TValue>(kind, col, ref value);
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x00026998 File Offset: 0x00024B98
			public VectorType GetSlotType(int col)
			{
				Contracts.Check(this._ectx, 0 <= col && col < this.ColumnCount, "col");
				if (this._parent._inputToTransposed[col] != -1)
				{
					return this._slotTypes[this._parent._inputToTransposed[col]];
				}
				if (this._parent._tview != null)
				{
					return this._parent._tview.TransposeSchema.GetSlotType(col);
				}
				return null;
			}

			// Token: 0x040003B7 RID: 951
			private readonly Transposer _parent;

			// Token: 0x040003B8 RID: 952
			private readonly IExceptionContext _ectx;

			// Token: 0x040003B9 RID: 953
			private readonly VectorType[] _slotTypes;
		}

		// Token: 0x02000166 RID: 358
		private abstract class SlotCursor<T> : RootCursorBase, ISlotCursor, ICursor, ICounted, IDisposable
		{
			// Token: 0x17000092 RID: 146
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x00026A0F File Offset: 0x00024C0F
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x00026A13 File Offset: 0x00024C13
			protected SlotCursor(Transposer parent, int col)
				: base(parent._host)
			{
				this._parent = parent;
				this._col = col;
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x00026A5A File Offset: 0x00024C5A
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
					val = new UInt128((ulong)base.Position, 0UL);
				};
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x00026A68 File Offset: 0x00024C68
			public ValueGetter<VBuffer<TValue>> GetGetter<TValue>()
			{
				if (this._getter == null)
				{
					this._getter = this.GetGetterCore();
				}
				ValueGetter<VBuffer<TValue>> valueGetter = this._getter as ValueGetter<VBuffer<TValue>>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000723 RID: 1827 RVA: 0x00026ABF File Offset: 0x00024CBF
			public VectorType GetSlotType()
			{
				return this._parent.TransposeSchema.GetSlotType(this._col);
			}

			// Token: 0x06000724 RID: 1828
			protected abstract ValueGetter<VBuffer<T>> GetGetterCore();

			// Token: 0x040003BA RID: 954
			private readonly Transposer _parent;

			// Token: 0x040003BB RID: 955
			private readonly int _col;

			// Token: 0x040003BC RID: 956
			private ValueGetter<VBuffer<T>> _getter;
		}

		// Token: 0x02000167 RID: 359
		private sealed class SlotCursorOne<T> : Transposer.SlotCursor<T>
		{
			// Token: 0x06000726 RID: 1830 RVA: 0x00026AD8 File Offset: 0x00024CD8
			public SlotCursorOne(Transposer parent, int col)
				: base(parent, col)
			{
				int num = parent._inputToTransposed[col];
				int num2 = ((num == 0) ? 0 : parent._splitLim[num - 1]);
				if (parent._splitLim[num] == num2)
				{
					this._view = parent._view;
					this._col = parent._cols[num].Index;
				}
				else
				{
					this._view = parent._splitView;
					this._col = num2;
				}
				this._len = parent.RowCount;
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x00026B52 File Offset: 0x00024D52
			protected override bool MoveNextCore()
			{
				return base.State == 0;
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x00026CC4 File Offset: 0x00024EC4
			protected override ValueGetter<VBuffer<T>> GetGetterCore()
			{
				RefPredicate<T> isDefault = Conversions.Instance.GetIsDefaultPredicate<T>(this._view.Schema.GetColumnType(this._col));
				bool valid = false;
				VBuffer<T> cached = default(VBuffer<T>);
				return delegate(ref VBuffer<T> dst)
				{
					Contracts.Check(this._ch, this.IsGood, "Cannot get values in the cursor's current state");
					if (!valid)
					{
						using (IRowCursor rowCursor = this._view.GetRowCursor((int c) => c == this._col, null))
						{
							int[] array = null;
							T[] array2 = null;
							int num = -1;
							int num2 = 0;
							T t = default(T);
							ValueGetter<T> getter = rowCursor.GetGetter<T>(this._col);
							while (rowCursor.MoveNext())
							{
								num++;
								getter.Invoke(ref t);
								if (!isDefault.Invoke(ref t))
								{
									Utils.EnsureSize<int>(ref array, ++num2, true);
									array[num2 - 1] = num;
									Utils.EnsureSize<T>(ref array2, num2, true);
									array2[num2 - 1] = t;
								}
							}
							num++;
							if (num2 < num / 2 || num2 == num)
							{
								cached = new VBuffer<T>(num, num2, array2, (num2 == num) ? null : array);
							}
							else
							{
								new VBuffer<T>(num, num2, array2, array).CopyToDense(ref cached);
							}
						}
						valid = true;
					}
					cached.CopyTo(ref dst);
				};
			}

			// Token: 0x040003BD RID: 957
			private readonly IDataView _view;

			// Token: 0x040003BE RID: 958
			private readonly int _col;

			// Token: 0x040003BF RID: 959
			private readonly int _len;
		}

		// Token: 0x02000168 RID: 360
		private sealed class SlotCursorVec<T> : Transposer.SlotCursor<T>
		{
			// Token: 0x06000729 RID: 1833 RVA: 0x00026D24 File Offset: 0x00024F24
			public SlotCursorVec(Transposer parent, int col)
				: base(parent, col)
			{
				int num = parent._inputToTransposed[col];
				int num2 = ((num == 0) ? 0 : parent._splitLim[num - 1]);
				if (parent._splitLim[num] == num2)
				{
					this._view = parent._view;
					this._colMin = parent._cols[num].Index;
					this._colLim = this._colMin + 1;
				}
				else
				{
					this._view = parent._splitView;
					this._colMin = num2;
					this._colLim = parent._splitLim[num];
				}
				this._colStored = (this._colCurr = this._colMin - 1);
				this._slotLim = 0;
				this._slotCurr = -1;
				this._rbuff = new VBuffer<T>[16];
				this._rbuffIndices = new int[this._rbuff.Length];
				this._len = parent.RowCount;
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x00027260 File Offset: 0x00025460
			private void EnsureValid()
			{
				Contracts.Check(this._ch, base.State == 1, "Cursor is not in good state, cannot get values");
				if (this._colStored == this._colCurr)
				{
					return;
				}
				ColumnType columnType = this._view.Schema.GetColumnType(this._colCurr);
				RefPredicate<T> isDefault = Conversions.Instance.GetIsDefaultPredicate<T>(columnType.ItemType);
				int vecLen = columnType.ValueCount;
				int num = this._rbuff.Length * vecLen;
				int sparseThreshold = (num + 5 - 1) / 5;
				Array.Clear(this._rbuffIndices, 0, this._rbuffIndices.Length);
				int offset = 0;
				using (IRowCursor rowCursor = this._view.GetRowCursor((int c) => c == this._colCurr, null))
				{
					Utils.EnsureSize<int[]>(ref this._indices, vecLen, true);
					for (int i = 0; i < columnType.ValueCount; i++)
					{
						this._indices[i] = this._indices[i] ?? new int[this._len];
					}
					Utils.EnsureSize<T[]>(ref this._values, vecLen, true);
					for (int j = 0; j < columnType.ValueCount; j++)
					{
						this._values[j] = this._values[j] ?? new T[this._len];
					}
					Utils.EnsureSize<int>(ref this._counts, vecLen, false);
					if (vecLen > 0)
					{
						Array.Clear(this._counts, 0, vecLen);
					}
					ValueGetter<VBuffer<T>> getter = rowCursor.GetGetter<VBuffer<T>>(this._colCurr);
					int irbuff = 0;
					int countSum = 0;
					Heap<KeyValuePair<int, int>> heap = new Heap<KeyValuePair<int, int>>((KeyValuePair<int, int> p1, KeyValuePair<int, int> p2) => p1.Key > p2.Key || (p1.Key == p2.Key && p1.Value > p2.Value), this._rbuff.Length);
					Action action = delegate
					{
						if (countSum >= sparseThreshold)
						{
							for (int l = 0; l < vecLen; l++)
							{
								int[] array = this._indices[l];
								T[] array2 = this._values[l];
								for (int m = 0; m < irbuff; m++)
								{
									int num3 = offset + m;
									VBuffer<T> vbuffer2 = this._rbuff[m];
									if (vbuffer2.IsDense)
									{
										if (!isDefault.Invoke(ref vbuffer2.Values[l]))
										{
											array[this._counts[l]] = num3;
											array2[this._counts[l]++] = vbuffer2.Values[l];
										}
									}
									else
									{
										int num4 = this._rbuffIndices[m];
										if (num4 < vbuffer2.Count && vbuffer2.Indices[num4] == l)
										{
											if (!isDefault.Invoke(ref vbuffer2.Values[num4]))
											{
												array[this._counts[l]] = num3;
												array2[this._counts[l]++] = vbuffer2.Values[num4];
											}
											this._rbuffIndices[m]++;
										}
									}
								}
							}
						}
						else
						{
							int num5 = -1;
							int[] array3 = null;
							T[] array4 = null;
							for (int n = 0; n < irbuff; n++)
							{
								VBuffer<T> vbuffer3 = this._rbuff[n];
								if (vbuffer3.Count > 0)
								{
									heap.Add(new KeyValuePair<int, int>(vbuffer3.IsDense ? 0 : vbuffer3.Indices[0], n));
								}
							}
							while (heap.Count > 0)
							{
								KeyValuePair<int, int> keyValuePair = heap.Pop();
								if (keyValuePair.Key != num5)
								{
									num5 = keyValuePair.Key;
									array3 = this._indices[num5];
									array4 = this._values[num5];
								}
								VBuffer<T> vbuffer4 = this._rbuff[keyValuePair.Value];
								int num6 = (vbuffer4.IsDense ? num5 : this._rbuffIndices[keyValuePair.Value]++);
								array3[this._counts[num5]] = keyValuePair.Value + offset;
								array4[this._counts[num5]++] = vbuffer4.Values[num6];
								if (++num6 < vbuffer4.Count)
								{
									heap.Add(new KeyValuePair<int, int>(vbuffer4.IsDense ? (num5 + 1) : vbuffer4.Indices[num6], keyValuePair.Value));
								}
							}
						}
						Array.Clear(this._rbuffIndices, 0, irbuff);
						offset += irbuff;
						countSum = (irbuff = 0);
					};
					while (rowCursor.MoveNext())
					{
						int num2 = checked((int)rowCursor.Position);
						getter.Invoke(ref this._rbuff[irbuff]);
						countSum += this._rbuff[irbuff].Count;
						if (++irbuff == this._rbuff.Length)
						{
							action();
						}
					}
					if (irbuff > 0)
					{
						action();
					}
				}
				Utils.EnsureSize<VBuffer<T>>(ref this._cbuff, vecLen, true);
				for (int k = 0; k < vecLen; k++)
				{
					VBuffer<T> vbuffer = new VBuffer<T>(this._len, this._counts[k], this._values[k], this._indices[k]);
					if (vbuffer.Count < this._len / 2)
					{
						Utils.Swap<VBuffer<T>>(ref vbuffer, ref this._cbuff[k]);
						this._indices[k] = vbuffer.Indices ?? new int[this._len];
						this._values[k] = vbuffer.Values ?? new T[this._len];
					}
					else
					{
						vbuffer.CopyToDense(ref this._cbuff[k]);
					}
				}
				this._colStored = this._colCurr;
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x00027600 File Offset: 0x00025800
			protected override bool MoveNextCore()
			{
				if (++this._slotCurr < this._slotLim)
				{
					return true;
				}
				this._slotCurr = 0;
				if (++this._colCurr == this._colLim)
				{
					return false;
				}
				this._slotLim = this._view.Schema.GetColumnType(this._colCurr).ValueCount;
				return true;
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x0002766C File Offset: 0x0002586C
			private void Getter(ref VBuffer<T> dst)
			{
				Contracts.Check(this._ch, base.IsGood, "Cannot get values in the cursor's current state");
				this.EnsureValid();
				this._cbuff[this._slotCurr].CopyTo(ref dst);
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x000276A1 File Offset: 0x000258A1
			protected override ValueGetter<VBuffer<T>> GetGetterCore()
			{
				return new ValueGetter<VBuffer<T>>(this.Getter);
			}

			// Token: 0x040003C0 RID: 960
			private readonly IDataView _view;

			// Token: 0x040003C1 RID: 961
			private readonly int _colMin;

			// Token: 0x040003C2 RID: 962
			private readonly int _colLim;

			// Token: 0x040003C3 RID: 963
			private readonly int _len;

			// Token: 0x040003C4 RID: 964
			private readonly VBuffer<T>[] _rbuff;

			// Token: 0x040003C5 RID: 965
			private readonly int[] _rbuffIndices;

			// Token: 0x040003C6 RID: 966
			private int[][] _indices;

			// Token: 0x040003C7 RID: 967
			private T[][] _values;

			// Token: 0x040003C8 RID: 968
			private int[] _counts;

			// Token: 0x040003C9 RID: 969
			private VBuffer<T>[] _cbuff;

			// Token: 0x040003CA RID: 970
			private int _colStored;

			// Token: 0x040003CB RID: 971
			private int _colCurr;

			// Token: 0x040003CC RID: 972
			private int _slotCurr;

			// Token: 0x040003CD RID: 973
			private int _slotLim;
		}

		// Token: 0x02000169 RID: 361
		private sealed class DataViewSlicer : IDataView, ISchematized
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x000276AF File Offset: 0x000258AF
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000731 RID: 1841 RVA: 0x000276B7 File Offset: 0x000258B7
			public bool CanShuffle
			{
				get
				{
					return this._input.CanShuffle;
				}
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x000276C4 File Offset: 0x000258C4
			public DataViewSlicer(IHost host, IDataView input, int[] toSlice)
			{
				this._host = host;
				this._input = input;
				this._splitters = new Transposer.DataViewSlicer.Splitter[toSlice.Length];
				this._incolToLim = new int[toSlice.Length];
				int num = 0;
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (int i = 0; i < toSlice.Length; i++)
				{
					Transposer.DataViewSlicer.Splitter splitter = (this._splitters[i] = Transposer.DataViewSlicer.Splitter.Create(this._input, toSlice[i]));
					num = (this._incolToLim[i] = num + splitter.ColumnCount);
					dictionary[this._input.Schema.GetColumnName(toSlice[i])] = num - 1;
				}
				this._colToSplitIndex = new int[num];
				this._colToSplitCol = new int[num];
				num = 0;
				for (int j = 0; j < this._splitters.Length; j++)
				{
					int columnCount = this._splitters[j].ColumnCount;
					for (int k = 0; k < columnCount; k++)
					{
						this._colToSplitIndex[num] = j;
						this._colToSplitCol[num++] = k;
					}
				}
				this._schema = new Transposer.DataViewSlicer.SchemaImpl(this, dictionary);
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x000277D8 File Offset: 0x000259D8
			public long? GetRowCount(bool lazy = true)
			{
				return this._input.GetRowCount(lazy);
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x000277E6 File Offset: 0x000259E6
			public void InColToOutRange(int incol, out int outMin, out int outLim)
			{
				outMin = ((incol == 0) ? 0 : this._incolToLim[incol - 1]);
				outLim = this._incolToLim[incol];
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x00027804 File Offset: 0x00025A04
			private void OutputColumnToSplitterIndices(int col, out int splitInd, out int splitCol)
			{
				splitInd = this._colToSplitIndex[col];
				splitCol = this._colToSplitCol[col];
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x0002781C File Offset: 0x00025A1C
			public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				bool[] array;
				Func<int, bool> func = this.CreateInputPredicate(predicate, out array);
				return new Transposer.DataViewSlicer.Cursor(this._host, this, this._input.GetRowCursor(func, rand), predicate, array);
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x00027860 File Offset: 0x00025A60
			public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				bool[] array;
				Func<int, bool> func = this.CreateInputPredicate(predicate, out array);
				IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, func, n, rand);
				for (int i = 0; i < rowCursorSet.Length; i++)
				{
					rowCursorSet[i] = new Transposer.DataViewSlicer.Cursor(this._host, this, rowCursorSet[i], predicate, array);
				}
				return rowCursorSet;
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x000278D4 File Offset: 0x00025AD4
			private Func<int, bool> CreateInputPredicate(Func<int, bool> pred, out bool[] activeSplitters)
			{
				activeSplitters = new bool[this._splitters.Length];
				HashSet<int> hashSet = new HashSet<int>();
				int num = 0;
				for (int i = 0; i < activeSplitters.Length; i++)
				{
					Transposer.DataViewSlicer.Splitter splitter = this._splitters[i];
					bool flag;
					if (pred != null)
					{
						flag = Enumerable.Range(num, splitter.ColumnCount).Any((int c) => pred(c));
					}
					else
					{
						flag = true;
					}
					bool flag2 = flag;
					if (flag2)
					{
						activeSplitters[i] = flag2;
						hashSet.Add(splitter.SrcCol);
					}
					num += splitter.ColumnCount;
				}
				return new Func<int, bool>(hashSet.Contains);
			}

			// Token: 0x040003CF RID: 975
			private readonly IDataView _input;

			// Token: 0x040003D0 RID: 976
			private readonly Transposer.DataViewSlicer.Splitter[] _splitters;

			// Token: 0x040003D1 RID: 977
			private readonly int[] _incolToLim;

			// Token: 0x040003D2 RID: 978
			private readonly int[] _colToSplitIndex;

			// Token: 0x040003D3 RID: 979
			private readonly int[] _colToSplitCol;

			// Token: 0x040003D4 RID: 980
			private readonly Transposer.DataViewSlicer.SchemaImpl _schema;

			// Token: 0x040003D5 RID: 981
			private readonly IHost _host;

			// Token: 0x0200016A RID: 362
			private abstract class NoMetadataSchema : ISchema
			{
				// Token: 0x17000095 RID: 149
				// (get) Token: 0x06000739 RID: 1849
				public abstract int ColumnCount { get; }

				// Token: 0x0600073A RID: 1850
				public abstract bool TryGetColumnIndex(string name, out int col);

				// Token: 0x0600073B RID: 1851
				public abstract string GetColumnName(int col);

				// Token: 0x0600073C RID: 1852
				public abstract ColumnType GetColumnType(int col);

				// Token: 0x0600073D RID: 1853 RVA: 0x00027982 File Offset: 0x00025B82
				public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
				}

				// Token: 0x0600073E RID: 1854 RVA: 0x000279A3 File Offset: 0x00025BA3
				public ColumnType GetMetadataTypeOrNull(string kind, int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					return null;
				}

				// Token: 0x0600073F RID: 1855 RVA: 0x000279C0 File Offset: 0x00025BC0
				public void GetMetadata<TValue>(string kind, int col, ref TValue value)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					throw MetadataUtils.ExceptGetMetadata();
				}
			}

			// Token: 0x0200016B RID: 363
			private sealed class SchemaImpl : Transposer.DataViewSlicer.NoMetadataSchema
			{
				// Token: 0x17000096 RID: 150
				// (get) Token: 0x06000741 RID: 1857 RVA: 0x000279E9 File Offset: 0x00025BE9
				public override int ColumnCount
				{
					get
					{
						return this._slicer._colToSplitIndex.Length;
					}
				}

				// Token: 0x06000742 RID: 1858 RVA: 0x000279F8 File Offset: 0x00025BF8
				public SchemaImpl(Transposer.DataViewSlicer slicer, Dictionary<string, int> nameToCol)
				{
					this._slicer = slicer;
					this._nameToCol = nameToCol;
				}

				// Token: 0x06000743 RID: 1859 RVA: 0x00027A0E File Offset: 0x00025C0E
				public override bool TryGetColumnIndex(string name, out int col)
				{
					return Utils.TryGetValue<string, int>(this._nameToCol, name, ref col);
				}

				// Token: 0x06000744 RID: 1860 RVA: 0x00027A20 File Offset: 0x00025C20
				public override string GetColumnName(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					int num;
					int num2;
					this._slicer.OutputColumnToSplitterIndices(col, out num, out num2);
					return this._slicer._splitters[num].GetColumnName(num2);
				}

				// Token: 0x06000745 RID: 1861 RVA: 0x00027A6C File Offset: 0x00025C6C
				public override ColumnType GetColumnType(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					int num;
					int num2;
					this._slicer.OutputColumnToSplitterIndices(col, out num, out num2);
					return this._slicer._splitters[num].GetColumnType(num2);
				}

				// Token: 0x040003D6 RID: 982
				private readonly Transposer.DataViewSlicer _slicer;

				// Token: 0x040003D7 RID: 983
				private readonly Dictionary<string, int> _nameToCol;
			}

			// Token: 0x0200016C RID: 364
			private abstract class Splitter : Transposer.DataViewSlicer.NoMetadataSchema
			{
				// Token: 0x17000097 RID: 151
				// (get) Token: 0x06000746 RID: 1862 RVA: 0x00027AB6 File Offset: 0x00025CB6
				public int SrcCol
				{
					get
					{
						return this._col;
					}
				}

				// Token: 0x06000747 RID: 1863 RVA: 0x00027ABE File Offset: 0x00025CBE
				protected Splitter(IDataView view, int col)
				{
					this._view = view;
					this._col = col;
				}

				// Token: 0x06000748 RID: 1864 RVA: 0x00027AD4 File Offset: 0x00025CD4
				public static Transposer.DataViewSlicer.Splitter Create(IDataView view, int col)
				{
					ColumnType columnType = view.Schema.GetColumnType(col);
					if (columnType.VectorSize <= 16)
					{
						return Utils.MarshalInvoke<IDataView, int, Transposer.DataViewSlicer.Splitter>(new Func<IDataView, int, Transposer.DataViewSlicer.Splitter>(Transposer.DataViewSlicer.Splitter.CreateCore<int>), columnType.RawType, view, col);
					}
					int num = (columnType.VectorSize - 1) / 16 + 1;
					int[] array;
					if (num <= 256)
					{
						array = new int[num];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = (i + 1) * 16;
						}
					}
					else
					{
						array = new int[256];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = (int)((long)(j + 1) * (long)columnType.VectorSize) / 256;
						}
					}
					array[array.Length - 1] = columnType.VectorSize;
					return Utils.MarshalInvoke<IDataView, int, int[], Transposer.DataViewSlicer.Splitter>(new Func<IDataView, int, int[], Transposer.DataViewSlicer.Splitter>(Transposer.DataViewSlicer.Splitter.CreateCore<int>), columnType.ItemType.RawType, view, col, array);
				}

				// Token: 0x06000749 RID: 1865
				public abstract IRow Bind(IRow row, Func<int, bool> pred);

				// Token: 0x0600074A RID: 1866 RVA: 0x00027BA9 File Offset: 0x00025DA9
				private static Transposer.DataViewSlicer.Splitter CreateCore<T>(IDataView view, int col)
				{
					return new Transposer.DataViewSlicer.Splitter.NoSplitter<T>(view, col);
				}

				// Token: 0x0600074B RID: 1867 RVA: 0x00027BB2 File Offset: 0x00025DB2
				private static Transposer.DataViewSlicer.Splitter CreateCore<T>(IDataView view, int col, int[] ends)
				{
					return new Transposer.DataViewSlicer.Splitter.ColumnSplitter<T>(view, col, ends);
				}

				// Token: 0x0600074C RID: 1868 RVA: 0x00027BBC File Offset: 0x00025DBC
				public override bool TryGetColumnIndex(string name, out int col)
				{
					Contracts.CheckNonEmpty(name, "name");
					if (name != this._view.Schema.GetColumnName(this.SrcCol))
					{
						col = 0;
						return false;
					}
					col = this.ColumnCount - 1;
					return true;
				}

				// Token: 0x0600074D RID: 1869 RVA: 0x00027BF8 File Offset: 0x00025DF8
				public override string GetColumnName(int col)
				{
					Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
					return this._view.Schema.GetColumnName(this.SrcCol);
				}

				// Token: 0x0600074E RID: 1870
				public abstract override ColumnType GetColumnType(int col);

				// Token: 0x040003D8 RID: 984
				private readonly IDataView _view;

				// Token: 0x040003D9 RID: 985
				private readonly int _col;

				// Token: 0x0200016D RID: 365
				private abstract class RowBase<S> : IRow, ISchematized, ICounted where S : Transposer.DataViewSlicer.Splitter
				{
					// Token: 0x17000098 RID: 152
					// (get) Token: 0x0600074F RID: 1871 RVA: 0x00027C2A File Offset: 0x00025E2A
					public ISchema Schema
					{
						get
						{
							return this._parent;
						}
					}

					// Token: 0x17000099 RID: 153
					// (get) Token: 0x06000750 RID: 1872 RVA: 0x00027C37 File Offset: 0x00025E37
					public long Position
					{
						get
						{
							return this._input.Position;
						}
					}

					// Token: 0x1700009A RID: 154
					// (get) Token: 0x06000751 RID: 1873 RVA: 0x00027C44 File Offset: 0x00025E44
					public long Batch
					{
						get
						{
							return this._input.Batch;
						}
					}

					// Token: 0x06000752 RID: 1874 RVA: 0x00027C51 File Offset: 0x00025E51
					public RowBase(S parent, IRow input)
					{
						this._parent = parent;
						this._input = input;
					}

					// Token: 0x06000753 RID: 1875 RVA: 0x00027C67 File Offset: 0x00025E67
					public ValueGetter<UInt128> GetIdGetter()
					{
						return this._input.GetIdGetter();
					}

					// Token: 0x06000754 RID: 1876
					public abstract bool IsColumnActive(int col);

					// Token: 0x06000755 RID: 1877
					public abstract ValueGetter<TValue> GetGetter<TValue>(int col);

					// Token: 0x040003DA RID: 986
					protected readonly S _parent;

					// Token: 0x040003DB RID: 987
					protected readonly IRow _input;
				}

				// Token: 0x0200016E RID: 366
				private sealed class NoSplitter<T> : Transposer.DataViewSlicer.Splitter
				{
					// Token: 0x1700009B RID: 155
					// (get) Token: 0x06000756 RID: 1878 RVA: 0x00027C74 File Offset: 0x00025E74
					public override int ColumnCount
					{
						get
						{
							return 1;
						}
					}

					// Token: 0x06000757 RID: 1879 RVA: 0x00027C77 File Offset: 0x00025E77
					public NoSplitter(IDataView view, int col)
						: base(view, col)
					{
					}

					// Token: 0x06000758 RID: 1880 RVA: 0x00027C81 File Offset: 0x00025E81
					public override ColumnType GetColumnType(int col)
					{
						Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
						return this._view.Schema.GetColumnType(base.SrcCol);
					}

					// Token: 0x06000759 RID: 1881 RVA: 0x00027CB3 File Offset: 0x00025EB3
					public override IRow Bind(IRow row, Func<int, bool> pred)
					{
						return new Transposer.DataViewSlicer.Splitter.NoSplitter<T>.Row(this, row, pred(0));
					}

					// Token: 0x0200016F RID: 367
					private sealed class Row : Transposer.DataViewSlicer.Splitter.RowBase<Transposer.DataViewSlicer.Splitter.NoSplitter<T>>
					{
						// Token: 0x0600075A RID: 1882 RVA: 0x00027CC3 File Offset: 0x00025EC3
						public Row(Transposer.DataViewSlicer.Splitter.NoSplitter<T> parent, IRow input, bool isActive)
							: base(parent, input)
						{
							this._isActive = isActive;
						}

						// Token: 0x0600075B RID: 1883 RVA: 0x00027CD4 File Offset: 0x00025ED4
						public override bool IsColumnActive(int col)
						{
							Contracts.CheckParam(0 <= col && col < this._parent.ColumnCount, "col");
							return this._isActive;
						}

						// Token: 0x0600075C RID: 1884 RVA: 0x00027CFB File Offset: 0x00025EFB
						public override ValueGetter<TValue> GetGetter<TValue>(int col)
						{
							Contracts.Check(this.IsColumnActive(col));
							return this._input.GetGetter<TValue>(this._parent.SrcCol);
						}

						// Token: 0x040003DC RID: 988
						private readonly bool _isActive;
					}
				}

				// Token: 0x02000170 RID: 368
				private sealed class ColumnSplitter<T> : Transposer.DataViewSlicer.Splitter
				{
					// Token: 0x1700009C RID: 156
					// (get) Token: 0x0600075D RID: 1885 RVA: 0x00027D1F File Offset: 0x00025F1F
					public override int ColumnCount
					{
						get
						{
							return this._lims.Length;
						}
					}

					// Token: 0x0600075E RID: 1886 RVA: 0x00027D2C File Offset: 0x00025F2C
					public ColumnSplitter(IDataView view, int col, int[] lims)
						: base(view, col)
					{
						VectorType asVector = this._view.Schema.GetColumnType(base.SrcCol).AsVector;
						this._lims = lims;
						this._types = new VectorType[this._lims.Length];
						this._types[0] = new VectorType(asVector.ItemType, this._lims[0]);
						for (int i = 1; i < this._lims.Length; i++)
						{
							this._types[i] = new VectorType(asVector.ItemType, this._lims[i] - this._lims[i - 1]);
						}
					}

					// Token: 0x0600075F RID: 1887 RVA: 0x00027DCB File Offset: 0x00025FCB
					public override ColumnType GetColumnType(int col)
					{
						Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
						return this._types[col];
					}

					// Token: 0x06000760 RID: 1888 RVA: 0x00027DEF File Offset: 0x00025FEF
					public override IRow Bind(IRow row, Func<int, bool> pred)
					{
						return new Transposer.DataViewSlicer.Splitter.ColumnSplitter<T>.Row(this, row, pred);
					}

					// Token: 0x040003DD RID: 989
					private readonly int[] _lims;

					// Token: 0x040003DE RID: 990
					private readonly VectorType[] _types;

					// Token: 0x02000171 RID: 369
					private sealed class Row : Transposer.DataViewSlicer.Splitter.RowBase<Transposer.DataViewSlicer.Splitter.ColumnSplitter<T>>
					{
						// Token: 0x1700009D RID: 157
						// (get) Token: 0x06000761 RID: 1889 RVA: 0x00027DF9 File Offset: 0x00025FF9
						private int[] Lims
						{
							get
							{
								return this._parent._lims;
							}
						}

						// Token: 0x06000762 RID: 1890 RVA: 0x00027E08 File Offset: 0x00026008
						public Row(Transposer.DataViewSlicer.Splitter.ColumnSplitter<T> parent, IRow input, Func<int, bool> pred)
							: base(parent, input)
						{
							this._inputGetter = input.GetGetter<VBuffer<T>>(this._parent.SrcCol);
							this._srcIndicesLims = new int[this.Lims.Length];
							this._lastValid = -1L;
							this._getters = new ValueGetter<VBuffer<T>>[this.Lims.Length];
							for (int i = 0; i < this._getters.Length; i++)
							{
								this._getters[i] = (pred(i) ? this.CreateGetter(i) : null);
							}
						}

						// Token: 0x06000763 RID: 1891 RVA: 0x00027E90 File Offset: 0x00026090
						public override bool IsColumnActive(int col)
						{
							Contracts.CheckParam(0 <= col && col < this._parent.ColumnCount, "col");
							return this._getters[col] != null;
						}

						// Token: 0x06000764 RID: 1892 RVA: 0x00027EC0 File Offset: 0x000260C0
						public override ValueGetter<TValue> GetGetter<TValue>(int col)
						{
							Contracts.Check(this.IsColumnActive(col));
							ValueGetter<TValue> valueGetter = this._getters[col] as ValueGetter<TValue>;
							if (valueGetter == null)
							{
								throw Contracts.Except("Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
							}
							return valueGetter;
						}

						// Token: 0x06000765 RID: 1893 RVA: 0x00028080 File Offset: 0x00026280
						private ValueGetter<VBuffer<T>> CreateGetter(int col)
						{
							int min = ((col == 0) ? 0 : this.Lims[col - 1]);
							int len = this.Lims[col] - min;
							return delegate(ref VBuffer<T> value)
							{
								this.EnsureValid();
								T[] values = value.Values;
								if (this._inputValue.IsDense)
								{
									Utils.EnsureSize<T>(ref values, len, true);
									Array.Copy(this._inputValue.Values, min, values, 0, len);
									value = new VBuffer<T>(len, values, value.Indices);
									return;
								}
								int num = ((col == 0) ? 0 : this._srcIndicesLims[col - 1]);
								int num2 = this._srcIndicesLims[col];
								int num3 = num2 - num;
								if (num3 == 0)
								{
									value = new VBuffer<T>(len, 0, value.Values, value.Indices);
									return;
								}
								int[] indices = value.Indices;
								Utils.EnsureSize<int>(ref indices, num3, true);
								Utils.EnsureSize<T>(ref values, num3, true);
								Array.Copy(this._inputValue.Indices, num, indices, 0, num3);
								if (min != 0)
								{
									for (int i = 0; i < num3; i++)
									{
										indices[i] -= min;
									}
								}
								Array.Copy(this._inputValue.Values, num, values, 0, num3);
								value = new VBuffer<T>(len, num3, values, indices);
							};
						}

						// Token: 0x06000766 RID: 1894 RVA: 0x000280E8 File Offset: 0x000262E8
						private void EnsureValid()
						{
							if (this._lastValid == this._input.Position)
							{
								return;
							}
							this._inputGetter.Invoke(ref this._inputValue);
							if (this._inputValue.IsDense)
							{
								return;
							}
							if (this._inputValue.Count == 0)
							{
								Array.Clear(this._srcIndicesLims, 0, this._srcIndicesLims.Length);
								return;
							}
							int[] indices = this._inputValue.Indices;
							int num = 0;
							for (int i = 0; i < this.Lims.Length; i++)
							{
								int num2 = this.Lims[i];
								while (num < this._inputValue.Count && indices[num] < num2)
								{
									num++;
								}
								this._srcIndicesLims[i] = num;
							}
							this._lastValid = this._input.Position;
						}

						// Token: 0x040003DF RID: 991
						private long _lastValid;

						// Token: 0x040003E0 RID: 992
						private VBuffer<T> _inputValue;

						// Token: 0x040003E1 RID: 993
						private readonly ValueGetter<VBuffer<T>> _inputGetter;

						// Token: 0x040003E2 RID: 994
						private readonly int[] _srcIndicesLims;

						// Token: 0x040003E3 RID: 995
						private readonly ValueGetter<VBuffer<T>>[] _getters;
					}
				}
			}

			// Token: 0x02000172 RID: 370
			private sealed class Cursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
			{
				// Token: 0x1700009E RID: 158
				// (get) Token: 0x06000767 RID: 1895 RVA: 0x000281A8 File Offset: 0x000263A8
				public ISchema Schema
				{
					get
					{
						return this._slicer.Schema;
					}
				}

				// Token: 0x06000768 RID: 1896 RVA: 0x000281E4 File Offset: 0x000263E4
				public Cursor(IChannelProvider provider, Transposer.DataViewSlicer slicer, IRowCursor input, Func<int, bool> pred, bool[] activeSplitters)
					: base(provider, input)
				{
					this._slicer = slicer;
					this._sliceRows = new IRow[this._slicer._splitters.Length];
					Transposer.DataViewSlicer.Splitter[] splitters = slicer._splitters;
					new HashSet<int>();
					int num = 0;
					Func<int, bool> func = null;
					if (pred == null)
					{
						func = (int col) => true;
					}
					for (int i = 0; i < activeSplitters.Length; i++)
					{
						Transposer.DataViewSlicer.Splitter splitter = this._slicer._splitters[i];
						int localOffset = num;
						if (activeSplitters[i])
						{
							this._sliceRows[i] = splitter.Bind(input, (pred == null) ? func : ((int col) => pred(col + localOffset)));
						}
						num += splitter.ColumnCount;
					}
				}

				// Token: 0x06000769 RID: 1897 RVA: 0x000282CC File Offset: 0x000264CC
				public bool IsColumnActive(int col)
				{
					Contracts.Check(this._ch, 0 <= col && col < this.Schema.ColumnCount, "col");
					int num;
					int num2;
					this._slicer.OutputColumnToSplitterIndices(col, out num, out num2);
					return this._sliceRows[num] != null && this._sliceRows[num].IsColumnActive(num2);
				}

				// Token: 0x0600076A RID: 1898 RVA: 0x00028328 File Offset: 0x00026528
				public ValueGetter<TValue> GetGetter<TValue>(int col)
				{
					Contracts.Check(this._ch, this.IsColumnActive(col));
					int num;
					int num2;
					this._slicer.OutputColumnToSplitterIndices(col, out num, out num2);
					return this._sliceRows[num].GetGetter<TValue>(num2);
				}

				// Token: 0x040003E4 RID: 996
				private readonly Transposer.DataViewSlicer _slicer;

				// Token: 0x040003E5 RID: 997
				private readonly IRow[] _sliceRows;
			}
		}
	}
}
