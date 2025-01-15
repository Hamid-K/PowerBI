using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000F4 RID: 244
	public abstract class LeftJoinDataViewBase : IDataView, ISchematized
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001B5D5 File Offset: 0x000197D5
		public bool CanShuffle
		{
			get
			{
				return this._leftDv.CanShuffle;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060004F2 RID: 1266
		public abstract ISchema Schema { get; }

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001B5F8 File Offset: 0x000197F8
		protected LeftJoinDataViewBase(IHostEnvironment env, string name, IDataView left, IDataView right, LeftJoinDataViewBase.JoinKeyColumn[] jointKeyColumns, bool keepAllRightCols)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonWhiteSpace(env, name, "name");
			this._host = env.Register(name);
			Contracts.CheckValue<IDataView>(this._host, left, "left");
			Contracts.CheckValue<IDataView>(this._host, right, "right");
			Contracts.CheckValue<LeftJoinDataViewBase.JoinKeyColumn[]>(this._host, jointKeyColumns, "jointKeyColumns");
			if (keepAllRightCols)
			{
				List<int> list = null;
				ISchema schema = right.Schema;
				for (int i = 0; i < schema.ColumnCount; i++)
				{
					if (MetadataUtils.IsHidden(schema, i))
					{
						Utils.Add<int>(ref list, i);
					}
					else if (!schema.GetColumnType(i).IsCachable())
					{
						Utils.Add<int>(ref list, i);
					}
				}
				if (Utils.Size<int>(list) > 0)
				{
					right = new ChooseColumnsByIndexTransform(new ChooseColumnsByIndexTransform.Arguments
					{
						drop = true,
						index = Utils.ToArray<int>(list)
					}, this._host, right);
				}
			}
			ISchema schema2 = right.Schema;
			this._leftDv = left;
			ISchema schema3 = this._leftDv.Schema;
			this._jointKeyColumns = jointKeyColumns;
			int num = this._jointKeyColumns.Length;
			Contracts.Check(this._host, num > 0, "At least one join key column must be specified.");
			this._leftKeyIndices = new int[num];
			this._rightKeyIndices = new int[num];
			ColumnType[] array = new ColumnType[num];
			this._isLeftColumnKey = new bool[schema3.ColumnCount];
			this._isRightColumnKey = new bool[schema2.ColumnCount];
			HashSet<string> hashSet = new HashSet<string>();
			HashSet<string> hashSet2 = new HashSet<string>();
			for (int j = 0; j < num; j++)
			{
				string left2 = this._jointKeyColumns[j].Left;
				Contracts.Check(this._host, !string.IsNullOrWhiteSpace(left2), "The left key column name is empty.");
				if (!schema3.TryGetColumnIndex(left2, ref this._leftKeyIndices[j]))
				{
					throw Contracts.Except(this._host, "Column '{0}' not found in left data view.", new object[] { left2 });
				}
				hashSet.Add(left2);
				this._isLeftColumnKey[this._leftKeyIndices[j]] = true;
				string right2 = this._jointKeyColumns[j].Right;
				Contracts.Check(this._host, !string.IsNullOrWhiteSpace(right2), "The right key column name is empty.");
				if (!schema2.TryGetColumnIndex(right2, ref this._rightKeyIndices[j]))
				{
					throw Contracts.Except(this._host, "Column '{0}' is not found in right data view or is not cachable.", new object[] { right2 });
				}
				hashSet2.Add(right2);
				this._isRightColumnKey[this._rightKeyIndices[j]] = true;
				ColumnType columnType = schema3.GetColumnType(this._leftKeyIndices[j]);
				ColumnType columnType2 = schema2.GetColumnType(this._rightKeyIndices[j]);
				if (!LeftJoinDataViewBase.IsJoiningKeyTypeValid(columnType))
				{
					throw Contracts.Except(this._host, "The column type '{0}' of the left key column '{1}' is not support as a join key type.", new object[] { columnType, left2 });
				}
				if (!LeftJoinDataViewBase.IsJoiningKeyTypeValid(columnType2))
				{
					throw Contracts.Except(this._host, "The column type '{0}' of the right key column '{1}' is not support as a join key type.", new object[] { columnType2, right2 });
				}
				if (!columnType.Equals(columnType2))
				{
					throw Contracts.Except(this._host, "The left key column '{0}' has type '{1}', which does not match the right key column '{2}' with type '{3}'.", new object[] { left2, columnType, right2, columnType2 });
				}
				array[j] = columnType;
			}
			if (keepAllRightCols)
			{
				this._rightDv = new CacheDataView(this._host, right, this._rightKeyIndices);
			}
			else
			{
				ChooseColumnsByIndexTransform chooseColumnsByIndexTransform = new ChooseColumnsByIndexTransform(new ChooseColumnsByIndexTransform.Arguments
				{
					index = this._rightKeyIndices.Distinct<int>().ToArray<int>()
				}, this._host, right);
				this._rightDv = new CacheDataView(this._host, chooseColumnsByIndexTransform, Utils.GetIdentityPermutation(chooseColumnsByIndexTransform.Schema.ColumnCount));
				schema2 = this._rightDv.Schema;
				this._isRightColumnKey = Utils.CreateArray<bool>(schema2.ColumnCount, true);
				for (int k = 0; k < this._jointKeyColumns.Length; k++)
				{
					schema2.TryGetColumnIndex(this._jointKeyColumns[k].Right, ref this._rightKeyIndices[k]);
				}
			}
			using (IChannel channel = this._host.Start("Building Right Index"))
			{
				long value = this._rightDv.GetRowCount(false).Value;
				if (value > 2146435071L)
				{
					throw Contracts.Except(channel, "Right data has too many rows: {0} vs {1}", new object[] { value, 2146435071 });
				}
				int num2 = (int)value;
				this._nextSameKeys = new int[num2];
				this._nextSameHash = new int[num2];
				this._hashToHead = new Dictionary<uint, int>();
				this._hasUniqueKeys = true;
				int[] array2 = new int[num2];
				using (IRowCursor rowCursor = this._rightDv.GetRowCursor((int x) => this._isRightColumnKey[x], null))
				{
					using (IRowSeeker seeker = this._rightDv.GetSeeker((int x) => this._isRightColumnKey[x]))
					{
						LeftJoinDataViewBase.KeyComparer keyComparer = LeftJoinDataViewBase.KeyComparer.Create(rowCursor, seeker, this._rightKeyIndices, this._rightKeyIndices);
						int num3 = 0;
						while (rowCursor.MoveNext())
						{
							this._nextSameKeys[num3] = -1;
							array2[num3] = -1;
							this._nextSameHash[num3] = -1;
							uint hash = keyComparer.GetHash1();
							int num4;
							if (!this._hashToHead.TryGetValue(hash, out num4))
							{
								this._hashToHead.Add(hash, num3);
								array2[num3] = num3;
							}
							else
							{
								for (;;)
								{
									seeker.MoveTo((long)num4);
									if (keyComparer.Same())
									{
										break;
									}
									int num5 = this._nextSameHash[num4];
									if (num5 < 0)
									{
										goto Block_24;
									}
									num4 = num5;
								}
								this._hasUniqueKeys = false;
								this._nextSameKeys[array2[num4]] = num3;
								array2[num4] = num3;
								goto IL_05CB;
								Block_24:
								this._nextSameHash[num4] = num3;
								array2[num3] = num3;
							}
							IL_05CB:
							num3++;
						}
					}
				}
				channel.Done();
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001BC60 File Offset: 0x00019E60
		public long? GetRowCount(bool lazy = true)
		{
			return null;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001BC94 File Offset: 0x00019E94
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> func = (int x) => this.IsColumnNeededForLeftCursor(predicate, x);
			IRowCursor rowCursor = this._leftDv.GetRowCursor(func, rand);
			return this.GetRowCursorFromLeftCursor(rowCursor, predicate);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001BD10 File Offset: 0x00019F10
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> func = (int x) => this.IsColumnNeededForLeftCursor(predicate, x);
			IRowCursor[] array = this._leftDv.GetRowCursorSet(ref consolidator, func, n, rand);
			if (array.Length == 1 && n > 1)
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.GetRowCursorFromLeftCursor(array[i], predicate);
			}
			return array2;
		}

		// Token: 0x060004F7 RID: 1271
		protected abstract IRowCursor GetRowCursorFromLeftCursor(IRowCursor leftCursor, Func<int, bool> predicate);

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		protected bool IsColumnNeededForLeftCursor(Func<int, bool> predicate, int col)
		{
			return col >= 0 && col < this._isLeftColumnKey.Length && (this._isLeftColumnKey[col] || predicate(col));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001BDD6 File Offset: 0x00019FD6
		private static bool IsJoiningKeyTypeValid(ColumnType type)
		{
			return type.IsStandardScalar || type.IsKey;
		}

		// Token: 0x04000262 RID: 610
		protected readonly IHost _host;

		// Token: 0x04000263 RID: 611
		protected readonly IDataView _leftDv;

		// Token: 0x04000264 RID: 612
		protected readonly CacheDataView _rightDv;

		// Token: 0x04000265 RID: 613
		protected readonly LeftJoinDataViewBase.JoinKeyColumn[] _jointKeyColumns;

		// Token: 0x04000266 RID: 614
		protected readonly int[] _leftKeyIndices;

		// Token: 0x04000267 RID: 615
		protected readonly int[] _rightKeyIndices;

		// Token: 0x04000268 RID: 616
		protected readonly bool[] _isLeftColumnKey;

		// Token: 0x04000269 RID: 617
		protected readonly bool[] _isRightColumnKey;

		// Token: 0x0400026A RID: 618
		protected readonly int[] _nextSameKeys;

		// Token: 0x0400026B RID: 619
		protected readonly int[] _nextSameHash;

		// Token: 0x0400026C RID: 620
		protected readonly Dictionary<uint, int> _hashToHead;

		// Token: 0x0400026D RID: 621
		protected readonly bool _hasUniqueKeys;

		// Token: 0x020000F5 RID: 245
		public class JoinKeyColumn
		{
			// Token: 0x060004FC RID: 1276 RVA: 0x0001BDE8 File Offset: 0x00019FE8
			public JoinKeyColumn()
			{
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x0001BDF0 File Offset: 0x00019FF0
			public JoinKeyColumn(string left, string right)
			{
				Contracts.CheckNonEmpty(left, "left");
				Contracts.CheckNonEmpty(right, "right");
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x0001BE1E File Offset: 0x0001A01E
			internal JoinKeyColumn(ModelLoadContext ctx, IHostEnvironment env)
			{
				this.Left = ctx.LoadNonEmptyString();
				this.Right = ctx.LoadNonEmptyString();
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x0001BE3E File Offset: 0x0001A03E
			internal void Save(ModelSaveContext ctx)
			{
				ctx.SaveNonEmptyString(this.Left);
				ctx.SaveNonEmptyString(this.Right);
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0001BE58 File Offset: 0x0001A058
			public static LeftJoinDataViewBase.JoinKeyColumn Parse(string str)
			{
				string text;
				string text2;
				if (!ColumnParsingUtils.TryParse(str, out text, out text2))
				{
					return null;
				}
				return new LeftJoinDataViewBase.JoinKeyColumn(text, text2);
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0001BE7A File Offset: 0x0001A07A
			public bool TryUnparse(StringBuilder sb)
			{
				if (CmdQuoter.NeedsQuoting(this.Left) || CmdQuoter.NeedsQuoting(this.Right))
				{
					return false;
				}
				sb.Append(this.Left).Append(':').Append(this.Right);
				return true;
			}

			// Token: 0x0400026E RID: 622
			[Argument(0, HelpText = "Name of the column in the left data view", ShortName = "left")]
			public string Left;

			// Token: 0x0400026F RID: 623
			[Argument(0, HelpText = "Name of the column in the right data view", ShortName = "right")]
			public string Right;
		}

		// Token: 0x020000F6 RID: 246
		protected abstract class KeyComparer
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
			public static LeftJoinDataViewBase.KeyComparer Create(IRow row1, IRow row2, int[] keys1, int[] keys2)
			{
				if (keys1.Length == 1)
				{
					return LeftJoinDataViewBase.KeyComparer.OneKeyComparer.Create(row1, keys1[0], row2, keys2[0]);
				}
				LeftJoinDataViewBase.KeyComparer.OneKeyComparer[] array = new LeftJoinDataViewBase.KeyComparer.OneKeyComparer[keys1.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = LeftJoinDataViewBase.KeyComparer.OneKeyComparer.Create(row1, keys1[i], row2, keys2[i]);
				}
				return new LeftJoinDataViewBase.KeyComparer.CompositeKeyComparer(row1, row2, array);
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x0001BF08 File Offset: 0x0001A108
			private KeyComparer(IRow row1, IRow row2)
			{
				this.Row1 = row1;
				this.Row2 = row2;
			}

			// Token: 0x06000504 RID: 1284
			public abstract uint GetHash1();

			// Token: 0x06000505 RID: 1285
			public abstract uint GetHash2();

			// Token: 0x06000506 RID: 1286
			public abstract bool Same();

			// Token: 0x06000507 RID: 1287
			public abstract ValueGetter<T> GetValueGetter1<T>(int col);

			// Token: 0x04000270 RID: 624
			public readonly IRow Row1;

			// Token: 0x04000271 RID: 625
			public readonly IRow Row2;

			// Token: 0x020000F7 RID: 247
			private sealed class ValueFetcher<TValue>
			{
				// Token: 0x06000508 RID: 1288 RVA: 0x0001BF1E File Offset: 0x0001A11E
				public ValueFetcher(IRow row, int col)
				{
					this.Getter = row.GetGetter<TValue>(col);
					this.Row = row;
					this.Pos = -1L;
				}

				// Token: 0x06000509 RID: 1289 RVA: 0x0001BF42 File Offset: 0x0001A142
				public void Ensure()
				{
					if (this.Pos != this.Row.Position)
					{
						this.Getter.Invoke(ref this.Value);
						this.Pos = this.Row.Position;
					}
				}

				// Token: 0x0600050A RID: 1290 RVA: 0x0001BF79 File Offset: 0x0001A179
				public void GetValue(ref TValue value)
				{
					this.Ensure();
					value = this.Value;
				}

				// Token: 0x04000272 RID: 626
				private readonly IRow Row;

				// Token: 0x04000273 RID: 627
				private long Pos;

				// Token: 0x04000274 RID: 628
				private readonly ValueGetter<TValue> Getter;

				// Token: 0x04000275 RID: 629
				public TValue Value;
			}

			// Token: 0x020000F8 RID: 248
			private abstract class OneKeyComparer : LeftJoinDataViewBase.KeyComparer
			{
				// Token: 0x0600050B RID: 1291 RVA: 0x0001BF8D File Offset: 0x0001A18D
				protected OneKeyComparer(IRow row1, int col1, IRow row2, int col2)
					: base(row1, row2)
				{
					this.Column1 = col1;
				}

				// Token: 0x0600050C RID: 1292 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
				public static LeftJoinDataViewBase.KeyComparer.OneKeyComparer Create(IRow row1, int col1, IRow row2, int col2)
				{
					ColumnType columnType = row1.Schema.GetColumnType(col1);
					if (columnType.IsKey && columnType.AsKey.Count > 0)
					{
						int count = columnType.AsKey.Count;
						if (columnType.RawType == typeof(byte))
						{
							return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.U1KnownCountKeyTypeKeyComparer(count, row1, col1, row2, col2);
						}
						if (columnType.RawType == typeof(ushort))
						{
							return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.U2KnownCountKeyTypeKeyComparer(count, row1, col1, row2, col2);
						}
						if (columnType.RawType == typeof(uint))
						{
							return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.U4KnownCountKeyTypeKeyComparer(count, row1, col1, row2, col2);
						}
						if (columnType.RawType == typeof(ulong))
						{
							return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.U8KnownCountKeyTypeKeyComparer(count, row1, col1, row2, col2);
						}
					}
					if (columnType.IsBool)
					{
						return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.DvBoolKeyComparer(row1, col1, row2, col2);
					}
					if (columnType.RawType == typeof(float))
					{
						return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.SingleKeyComparer(row1, col1, row2, col2);
					}
					if (columnType.RawType == typeof(double))
					{
						return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.DoubleKeyComparer(row1, col1, row2, col2);
					}
					Func<IRow, int, IRow, int, LeftJoinDataViewBase.KeyComparer.OneKeyComparer.EquatableKeyComparer<int>> func = new Func<IRow, int, IRow, int, LeftJoinDataViewBase.KeyComparer.OneKeyComparer.EquatableKeyComparer<int>>(LeftJoinDataViewBase.KeyComparer.OneKeyComparer.CreateEqu<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.RawType });
					return (LeftJoinDataViewBase.KeyComparer.OneKeyComparer)methodInfo.Invoke(null, new object[] { row1, col1, row2, col2 });
				}

				// Token: 0x0600050D RID: 1293 RVA: 0x0001C124 File Offset: 0x0001A324
				private static LeftJoinDataViewBase.KeyComparer.OneKeyComparer.EquatableKeyComparer<TValue> CreateEqu<TValue>(IRow row1, int col1, IRow row2, int col2) where TValue : struct, IEquatable<TValue>
				{
					return new LeftJoinDataViewBase.KeyComparer.OneKeyComparer.EquatableKeyComparer<TValue>(row1, col1, row2, col2);
				}

				// Token: 0x04000276 RID: 630
				public readonly int Column1;

				// Token: 0x020000F9 RID: 249
				private abstract class TypedKeyComparer<TValue> : LeftJoinDataViewBase.KeyComparer.OneKeyComparer
				{
					// Token: 0x0600050E RID: 1294 RVA: 0x0001C12F File Offset: 0x0001A32F
					protected TypedKeyComparer(IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this.Fetcher1 = new LeftJoinDataViewBase.KeyComparer.ValueFetcher<TValue>(row1, col1);
						this.Fetcher2 = new LeftJoinDataViewBase.KeyComparer.ValueFetcher<TValue>(row2, col2);
					}

					// Token: 0x0600050F RID: 1295 RVA: 0x0001C157 File Offset: 0x0001A357
					public override uint GetHash1()
					{
						this.Fetcher1.Ensure();
						return this.GetHashCore(this.Fetcher1.Value);
					}

					// Token: 0x06000510 RID: 1296 RVA: 0x0001C175 File Offset: 0x0001A375
					public override uint GetHash2()
					{
						this.Fetcher2.Ensure();
						return this.GetHashCore(this.Fetcher2.Value);
					}

					// Token: 0x06000511 RID: 1297 RVA: 0x0001C193 File Offset: 0x0001A393
					public override bool Same()
					{
						this.Fetcher1.Ensure();
						this.Fetcher2.Ensure();
						return this.SameCore(this.Fetcher1.Value, this.Fetcher2.Value);
					}

					// Token: 0x06000512 RID: 1298 RVA: 0x0001C1C7 File Offset: 0x0001A3C7
					public override ValueGetter<T> GetValueGetter1<T>(int col)
					{
						return (ValueGetter<T>)new ValueGetter<TValue>(this.Fetcher1.GetValue);
					}

					// Token: 0x06000513 RID: 1299
					protected abstract uint GetHashCore(TValue value);

					// Token: 0x06000514 RID: 1300
					protected abstract bool SameCore(TValue value1, TValue value2);

					// Token: 0x04000277 RID: 631
					protected readonly LeftJoinDataViewBase.KeyComparer.ValueFetcher<TValue> Fetcher1;

					// Token: 0x04000278 RID: 632
					protected readonly LeftJoinDataViewBase.KeyComparer.ValueFetcher<TValue> Fetcher2;
				}

				// Token: 0x020000FA RID: 250
				private sealed class EquatableKeyComparer<TValue> : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<TValue> where TValue : struct, IEquatable<TValue>
				{
					// Token: 0x06000515 RID: 1301 RVA: 0x0001C1DF File Offset: 0x0001A3DF
					public EquatableKeyComparer(IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
					}

					// Token: 0x06000516 RID: 1302 RVA: 0x0001C1EC File Offset: 0x0001A3EC
					protected override uint GetHashCore(TValue value)
					{
						return (uint)value.GetHashCode();
					}

					// Token: 0x06000517 RID: 1303 RVA: 0x0001C1FB File Offset: 0x0001A3FB
					protected override bool SameCore(TValue value1, TValue value2)
					{
						return value1.Equals(value2);
					}
				}

				// Token: 0x020000FB RID: 251
				private sealed class SingleKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<float>
				{
					// Token: 0x06000518 RID: 1304 RVA: 0x0001C20C File Offset: 0x0001A40C
					public SingleKeyComparer(IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this.NaHashValue = (uint)float.NaN.GetHashCode();
					}

					// Token: 0x06000519 RID: 1305 RVA: 0x0001C237 File Offset: 0x0001A437
					protected override uint GetHashCore(float value)
					{
						if (TypeUtils.IsNA(value))
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x0600051A RID: 1306 RVA: 0x0001C24F File Offset: 0x0001A44F
					protected override bool SameCore(float value1, float value2)
					{
						if (TypeUtils.IsNA(value1))
						{
							return TypeUtils.IsNA(value2);
						}
						return this.Fetcher1.Value.Equals(this.Fetcher2.Value);
					}

					// Token: 0x04000279 RID: 633
					private readonly uint NaHashValue;
				}

				// Token: 0x020000FC RID: 252
				private sealed class DoubleKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<double>
				{
					// Token: 0x0600051B RID: 1307 RVA: 0x0001C27C File Offset: 0x0001A47C
					public DoubleKeyComparer(IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this.NaHashValue = (uint)double.NaN.GetHashCode();
					}

					// Token: 0x0600051C RID: 1308 RVA: 0x0001C2AB File Offset: 0x0001A4AB
					protected override uint GetHashCore(double value)
					{
						if (TypeUtils.IsNA(value))
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x0600051D RID: 1309 RVA: 0x0001C2C3 File Offset: 0x0001A4C3
					protected override bool SameCore(double value1, double value2)
					{
						if (TypeUtils.IsNA(value1))
						{
							return TypeUtils.IsNA(value2);
						}
						return this.Fetcher1.Value.Equals(this.Fetcher2.Value);
					}

					// Token: 0x0400027A RID: 634
					private readonly uint NaHashValue;
				}

				// Token: 0x020000FD RID: 253
				private sealed class DvBoolKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<DvBool>
				{
					// Token: 0x0600051E RID: 1310 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
					public DvBoolKeyComparer(IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this.NaHashValue = (uint)DvBool.NA.GetHashCode();
					}

					// Token: 0x0600051F RID: 1311 RVA: 0x0001C321 File Offset: 0x0001A521
					protected override uint GetHashCore(DvBool value)
					{
						if (value.IsNA)
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x06000520 RID: 1312 RVA: 0x0001C340 File Offset: 0x0001A540
					protected override bool SameCore(DvBool value1, DvBool value2)
					{
						if (value1.IsNA)
						{
							return value2.IsNA;
						}
						return value1.Equals(value2);
					}

					// Token: 0x0400027B RID: 635
					private readonly uint NaHashValue;
				}

				// Token: 0x020000FE RID: 254
				private sealed class U1KnownCountKeyTypeKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<byte>
				{
					// Token: 0x06000521 RID: 1313 RVA: 0x0001C35C File Offset: 0x0001A55C
					public U1KnownCountKeyTypeKeyComparer(int count, IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this._count = count;
						this.NaHashValue = (uint)0.GetHashCode();
					}

					// Token: 0x06000522 RID: 1314 RVA: 0x0001C38B File Offset: 0x0001A58B
					protected override uint GetHashCore(byte value)
					{
						if (value == 0 || (int)value > this._count)
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x06000523 RID: 1315 RVA: 0x0001C3A7 File Offset: 0x0001A5A7
					protected override bool SameCore(byte value1, byte value2)
					{
						if (value1 == 0 || (int)value1 > this._count)
						{
							return value2 == 0 || (int)value2 > this._count;
						}
						return value1.Equals(value2);
					}

					// Token: 0x0400027C RID: 636
					private readonly uint NaHashValue;

					// Token: 0x0400027D RID: 637
					private readonly int _count;
				}

				// Token: 0x020000FF RID: 255
				private sealed class U2KnownCountKeyTypeKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<ushort>
				{
					// Token: 0x06000524 RID: 1316 RVA: 0x0001C3CC File Offset: 0x0001A5CC
					public U2KnownCountKeyTypeKeyComparer(int count, IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this._count = count;
						this.NaHashValue = (uint)0.GetHashCode();
					}

					// Token: 0x06000525 RID: 1317 RVA: 0x0001C3FB File Offset: 0x0001A5FB
					protected override uint GetHashCore(ushort value)
					{
						if (value == 0 || (int)value > this._count)
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x06000526 RID: 1318 RVA: 0x0001C417 File Offset: 0x0001A617
					protected override bool SameCore(ushort value1, ushort value2)
					{
						if (value1 == 0 || (int)value1 > this._count)
						{
							return value2 == 0 || (int)value2 > this._count;
						}
						return value1.Equals(value2);
					}

					// Token: 0x0400027E RID: 638
					private readonly uint NaHashValue;

					// Token: 0x0400027F RID: 639
					private readonly int _count;
				}

				// Token: 0x02000100 RID: 256
				private sealed class U4KnownCountKeyTypeKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<uint>
				{
					// Token: 0x06000527 RID: 1319 RVA: 0x0001C43C File Offset: 0x0001A63C
					public U4KnownCountKeyTypeKeyComparer(int count, IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this._count = count;
						this.NaHashValue = (uint)0U.GetHashCode();
					}

					// Token: 0x06000528 RID: 1320 RVA: 0x0001C46B File Offset: 0x0001A66B
					protected override uint GetHashCore(uint value)
					{
						if (value == 0U || (ulong)value > (ulong)((long)this._count))
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x06000529 RID: 1321 RVA: 0x0001C489 File Offset: 0x0001A689
					protected override bool SameCore(uint value1, uint value2)
					{
						if (value1 == 0U || (ulong)value1 > (ulong)((long)this._count))
						{
							return value2 == 0U || (ulong)value2 > (ulong)((long)this._count);
						}
						return value1.Equals(value2);
					}

					// Token: 0x04000280 RID: 640
					private readonly uint NaHashValue;

					// Token: 0x04000281 RID: 641
					private readonly int _count;
				}

				// Token: 0x02000101 RID: 257
				private sealed class U8KnownCountKeyTypeKeyComparer : LeftJoinDataViewBase.KeyComparer.OneKeyComparer.TypedKeyComparer<ulong>
				{
					// Token: 0x0600052A RID: 1322 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
					public U8KnownCountKeyTypeKeyComparer(int count, IRow row1, int col1, IRow row2, int col2)
						: base(row1, col1, row2, col2)
					{
						this._count = (ulong)((long)count);
						this.NaHashValue = (uint)0UL.GetHashCode();
					}

					// Token: 0x0600052B RID: 1323 RVA: 0x0001C4E5 File Offset: 0x0001A6E5
					protected override uint GetHashCore(ulong value)
					{
						if (value == 0UL || value > this._count)
						{
							return this.NaHashValue;
						}
						return (uint)value.GetHashCode();
					}

					// Token: 0x0600052C RID: 1324 RVA: 0x0001C503 File Offset: 0x0001A703
					protected override bool SameCore(ulong value1, ulong value2)
					{
						if (value1 == 0UL || value1 > this._count)
						{
							return value2 == 0UL || value2 > this._count;
						}
						return value1.Equals(value2);
					}

					// Token: 0x04000282 RID: 642
					private readonly uint NaHashValue;

					// Token: 0x04000283 RID: 643
					private readonly ulong _count;
				}
			}

			// Token: 0x02000102 RID: 258
			private sealed class CompositeKeyComparer : LeftJoinDataViewBase.KeyComparer
			{
				// Token: 0x0600052D RID: 1325 RVA: 0x0001C52C File Offset: 0x0001A72C
				public CompositeKeyComparer(IRow row1, IRow row2, LeftJoinDataViewBase.KeyComparer.OneKeyComparer[] items)
					: base(row1, row2)
				{
					this._items = items;
				}

				// Token: 0x0600052E RID: 1326 RVA: 0x0001C540 File Offset: 0x0001A740
				public override uint GetHash1()
				{
					uint num = 973955451U;
					foreach (LeftJoinDataViewBase.KeyComparer.OneKeyComparer oneKeyComparer in this._items)
					{
						num = Hashing.CombineHash(num, oneKeyComparer.GetHash1());
					}
					return num;
				}

				// Token: 0x0600052F RID: 1327 RVA: 0x0001C57C File Offset: 0x0001A77C
				public override uint GetHash2()
				{
					uint num = 973955451U;
					foreach (LeftJoinDataViewBase.KeyComparer.OneKeyComparer oneKeyComparer in this._items)
					{
						num = Hashing.CombineHash(num, oneKeyComparer.GetHash2());
					}
					return num;
				}

				// Token: 0x06000530 RID: 1328 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
				public override bool Same()
				{
					foreach (LeftJoinDataViewBase.KeyComparer.OneKeyComparer oneKeyComparer in this._items)
					{
						if (!oneKeyComparer.Same())
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x06000531 RID: 1329 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
				public override ValueGetter<TValue> GetValueGetter1<TValue>(int col)
				{
					foreach (LeftJoinDataViewBase.KeyComparer.OneKeyComparer oneKeyComparer in this._items)
					{
						if (col == oneKeyComparer.Column1)
						{
							return oneKeyComparer.GetValueGetter1<TValue>(col);
						}
					}
					throw Contracts.Except("Bad col value in GetValueGetter1");
				}

				// Token: 0x04000284 RID: 644
				private const uint HashSeed = 973955451U;

				// Token: 0x04000285 RID: 645
				private readonly LeftJoinDataViewBase.KeyComparer.OneKeyComparer[] _items;
			}
		}
	}
}
