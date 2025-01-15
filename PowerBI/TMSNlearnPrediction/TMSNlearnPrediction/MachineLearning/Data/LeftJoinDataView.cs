using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Data.Conversion;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000103 RID: 259
	public sealed class LeftJoinDataView : LeftJoinDataViewBase
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001C635 File Offset: 0x0001A835
		public override ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001C63D File Offset: 0x0001A83D
		public string RelativeIndexColumnName
		{
			get
			{
				return this._schema.RelativeIndexColumnName;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001C64A File Offset: 0x0001A84A
		public bool HasRelativeIndex
		{
			get
			{
				return this._schema.RelativeIndexColumnName != null;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001C660 File Offset: 0x0001A860
		public LeftJoinDataView(IHostEnvironment env, IDataView left, IDataView right, LeftJoinDataViewBase.JoinKeyColumn[] jointKeyColumns, bool inner, bool? addRelativeIndex = null, string relativeIndexColumnName = null)
			: base(env, "LeftJoinDataView", left, right, jointKeyColumns, true)
		{
			this._inner = inner;
			if (string.IsNullOrWhiteSpace(relativeIndexColumnName))
			{
				relativeIndexColumnName = null;
			}
			bool flag = addRelativeIndex ?? (relativeIndexColumnName != null || !this._hasUniqueKeys);
			if (flag && relativeIndexColumnName == null)
			{
				relativeIndexColumnName = "RelativeIndex";
			}
			this._schema = new LeftJoinDataView.JointSchema(this, relativeIndexColumnName);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		protected override IRowCursor GetRowCursorFromLeftCursor(IRowCursor leftCursor, Func<int, bool> predicate)
		{
			if (this._inner)
			{
				return new LeftJoinDataView.InnerJoinRowCursor(this, leftCursor, predicate);
			}
			return new LeftJoinDataView.OuterJoinRowCursor(this, leftCursor, predicate);
		}

		// Token: 0x04000286 RID: 646
		public const string RegistrationName = "LeftJoinDataView";

		// Token: 0x04000287 RID: 647
		internal const string RelativeIndexColumnDefaultName = "RelativeIndex";

		// Token: 0x04000288 RID: 648
		private readonly bool _inner;

		// Token: 0x04000289 RID: 649
		private readonly LeftJoinDataView.JointSchema _schema;

		// Token: 0x02000104 RID: 260
		private class JointSchema : ISchema
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001C6EF File Offset: 0x0001A8EF
			public int ColumnCount
			{
				get
				{
					return this._columnCount;
				}
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
			public JointSchema(LeftJoinDataView parent, string relativeIndexColumnName)
			{
				this._host = parent._host;
				this._leftSchema = parent._leftDv.Schema;
				this._rightSchema = parent._rightDv.Schema;
				this.JcolLeftLim = this._leftSchema.ColumnCount;
				if (relativeIndexColumnName != null)
				{
					this.RelativeIndexColumnName = relativeIndexColumnName;
					this.JcolRelIndex = this.JcolLeftLim;
					this._jcolRightMin = this.JcolLeftLim + 1;
				}
				else
				{
					this.JcolRelIndex = -1;
					this._jcolRightMin = this.JcolLeftLim;
				}
				this._jointToRightColIndexMap = new int[this._rightSchema.ColumnCount];
				this._rightToJointColIndexMap = new int[this._rightSchema.ColumnCount];
				int num = this._jcolRightMin;
				for (int i = 0; i < this._rightSchema.ColumnCount; i++)
				{
					this._rightToJointColIndexMap[i] = -1;
					if (!MetadataUtils.IsHidden(this._rightSchema, i) && !parent._isRightColumnKey[i])
					{
						this._jointToRightColIndexMap[num - this._jcolRightMin] = i;
						this._rightToJointColIndexMap[i] = num;
						num++;
					}
				}
				Array.Resize<int>(ref this._jointToRightColIndexMap, num - this._jcolRightMin);
				this._columnCount = num;
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x0001C824 File Offset: 0x0001AA24
			public void CheckColumnInRange(int col)
			{
				Contracts.CheckParam(this._host, 0 <= col && col < this._columnCount, "col", "Column index out of range");
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x0001C84B File Offset: 0x0001AA4B
			public int MapJointColToRightCol(int jcol)
			{
				return this._jointToRightColIndexMap[jcol - this._jcolRightMin];
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x0001C85C File Offset: 0x0001AA5C
			public bool TryMapRightColToJointCol(int rcol, out int jcol)
			{
				jcol = this._rightToJointColIndexMap[rcol];
				return jcol >= 0;
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x0001C870 File Offset: 0x0001AA70
			public bool TryGetColumnIndex(string name, out int col)
			{
				int num;
				if (this._rightSchema.TryGetColumnIndex(name, ref num) && this.TryMapRightColToJointCol(num, out col))
				{
					return true;
				}
				if (this.JcolRelIndex >= 0 && name == this.RelativeIndexColumnName)
				{
					col = this.JcolRelIndex;
					return true;
				}
				return this._leftSchema.TryGetColumnIndex(name, ref col);
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x0001C8CC File Offset: 0x0001AACC
			public string GetColumnName(int col)
			{
				this.CheckColumnInRange(col);
				if (col >= this._jcolRightMin)
				{
					int num = this._jointToRightColIndexMap[col - this._jcolRightMin];
					return this._rightSchema.GetColumnName(num);
				}
				if (col == this.JcolRelIndex)
				{
					return this.RelativeIndexColumnName;
				}
				return this._leftSchema.GetColumnName(col);
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x0001C92B File Offset: 0x0001AB2B
			public ColumnType GetColumnType(int col)
			{
				this.CheckColumnInRange(col);
				if (col == this.JcolRelIndex)
				{
					return NumberType.I8;
				}
				return this.JointApplyFunc<ColumnType>((ISchema schema, int x) => schema.GetColumnType(x), col);
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x0001C970 File Offset: 0x0001AB70
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				this.CheckColumnInRange(col);
				if (col == this.JcolRelIndex)
				{
					return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
				}
				return this.JointApplyFunc<IEnumerable<KeyValuePair<string, ColumnType>>>((ISchema schema, int x) => schema.GetMetadataTypes(x), col);
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				if (col == this.JcolRelIndex)
				{
					return null;
				}
				return this.JointApplyFunc<ColumnType>((ISchema schema, int x) => schema.GetMetadataTypeOrNull(kind, x), col);
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x0001C9FC File Offset: 0x0001ABFC
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				this.CheckColumnInRange(col);
				Contracts.Check(this._host, col != this.JcolRelIndex, "Invalid call to GetMetadata");
				if (col >= this._jcolRightMin)
				{
					int num = this._jointToRightColIndexMap[col - this._jcolRightMin];
					this._rightSchema.GetMetadata<TValue>(kind, num, ref value);
					return;
				}
				this._leftSchema.GetMetadata<TValue>(kind, col, ref value);
			}

			// Token: 0x06000542 RID: 1346 RVA: 0x0001CA64 File Offset: 0x0001AC64
			private T JointApplyFunc<T>(Func<ISchema, int, T> func, int jcol)
			{
				if (jcol >= this._jcolRightMin)
				{
					int num = this._jointToRightColIndexMap[jcol - this._jcolRightMin];
					return func(this._rightSchema, num);
				}
				return func(this._leftSchema, jcol);
			}

			// Token: 0x0400028A RID: 650
			private readonly IHost _host;

			// Token: 0x0400028B RID: 651
			private readonly ISchema _leftSchema;

			// Token: 0x0400028C RID: 652
			private readonly ISchema _rightSchema;

			// Token: 0x0400028D RID: 653
			private readonly int _columnCount;

			// Token: 0x0400028E RID: 654
			public readonly int JcolLeftLim;

			// Token: 0x0400028F RID: 655
			public readonly int JcolRelIndex;

			// Token: 0x04000290 RID: 656
			private readonly int _jcolRightMin;

			// Token: 0x04000291 RID: 657
			public readonly string RelativeIndexColumnName;

			// Token: 0x04000292 RID: 658
			private readonly int[] _jointToRightColIndexMap;

			// Token: 0x04000293 RID: 659
			private readonly int[] _rightToJointColIndexMap;
		}

		// Token: 0x02000105 RID: 261
		private abstract class RowCursorBase : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001CAA5 File Offset: 0x0001ACA5
			public override long Batch
			{
				get
				{
					return this._leftCursor.Batch;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001CAB2 File Offset: 0x0001ACB2
			public ISchema Schema
			{
				get
				{
					return this._parent.Schema;
				}
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
			protected RowCursorBase(LeftJoinDataView parent, IRowCursor leftCursor, Func<int, bool> predicate)
				: base(parent._host)
			{
				LeftJoinDataView.RowCursorBase <>4__this = this;
				this._parent = parent;
				this._schema = this._parent._schema;
				this._leftCursor = leftCursor;
				this._rightRow = this._parent._rightDv.GetSeeker((int x) => LeftJoinDataView.RowCursorBase.IsRightColumnNeededForCursor(<>4__this._parent, predicate, x));
				this._kc = LeftJoinDataViewBase.KeyComparer.Create(this._leftCursor, this._rightRow, this._parent._leftKeyIndices, this._parent._rightKeyIndices);
				this._getters = new Delegate[this._schema.ColumnCount];
				for (int i = 0; i < this._schema.ColumnCount; i++)
				{
					if (predicate(i))
					{
						this._getters[i] = this.CreateGetterDelegate(i);
					}
				}
				this._irowRightLim = this._parent._nextSameKeys.Length;
				this._irowRight = -1;
				this._relativeIndex = DvInt8.NA;
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x0001CC28 File Offset: 0x0001AE28
			public override ValueGetter<UInt128> GetIdGetter()
			{
				ValueGetter<UInt128> leftGetter = this._leftCursor.GetIdGetter();
				return delegate(ref UInt128 val)
				{
					leftGetter.Invoke(ref val);
					val = val.Combine(new UInt128((ulong)((long)(this._irowRight + 1)), 0UL));
				};
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x0001CC60 File Offset: 0x0001AE60
			private void MoveRight(int irow)
			{
				this._irowRight = irow;
				this._rightRow.MoveTo((long)this._irowRight);
				if (this._relativeIndex.IsNA)
				{
					this._relativeIndex = 0L;
					return;
				}
				this._relativeIndex += 1L;
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x0001CCBA File Offset: 0x0001AEBA
			private void NotFound()
			{
				this._irowRight = -1;
				this._relativeIndex = DvInt8.NA;
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x0001CCD0 File Offset: 0x0001AED0
			protected bool TryMoveRightSameKey()
			{
				int num = this._parent._nextSameKeys[this._irowRight];
				if (num >= 0)
				{
					this.MoveRight(num);
					return true;
				}
				this.NotFound();
				return false;
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x0001CD04 File Offset: 0x0001AF04
			protected void LookupKeys()
			{
				uint hash = this._kc.GetHash1();
				int num;
				if (!this._parent._hashToHead.TryGetValue(hash, out num))
				{
					this.NotFound();
					return;
				}
				for (;;)
				{
					this.MoveRight(num);
					if (this._kc.Same())
					{
						break;
					}
					this.NotFound();
					num = this._parent._nextSameHash[num];
					if (num < 0)
					{
						return;
					}
				}
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x0001CD66 File Offset: 0x0001AF66
			[Conditional("DEBUG")]
			protected void AssertConsistency()
			{
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x0001CD68 File Offset: 0x0001AF68
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				ValueGetter<TValue> valueGetter = this._getters[col] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x0001CDBF File Offset: 0x0001AFBF
			public bool IsColumnActive(int col)
			{
				this._schema.CheckColumnInRange(col);
				return this._getters[col] != null;
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x0001CDDC File Offset: 0x0001AFDC
			public override void Dispose()
			{
				if (base.State != 2)
				{
					this._irowRight = -1;
					this._relativeIndex = DvInt8.NA;
					this._leftCursor.Dispose();
					this._rightRow.Dispose();
					this._ch.Done();
					this._ch.Dispose();
					base.Dispose();
				}
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x0001CE38 File Offset: 0x0001B038
			private Delegate CreateGetterDelegate(int col)
			{
				if (col == this._schema.JcolRelIndex)
				{
					return new ValueGetter<DvInt8>(this.RelativeIndexGetter);
				}
				Func<int, ValueGetter<int>> func = new Func<int, ValueGetter<int>>(this.CreateGetterDelegate<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Schema.GetColumnType(col).RawType });
				return (Delegate)methodInfo.Invoke(this, new object[] { col });
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
			private ValueGetter<TValue> CreateGetterDelegate<TValue>(int col)
			{
				if (col >= this._schema.JcolLeftLim)
				{
					return this.CreateRightGetterDelegate<TValue>(this._schema.MapJointColToRightCol(col));
				}
				if (this._parent._isLeftColumnKey[col])
				{
					return this._kc.GetValueGetter1<TValue>(col);
				}
				return this._leftCursor.GetGetter<TValue>(col);
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x0001CF0E File Offset: 0x0001B10E
			protected virtual ValueGetter<TValue> CreateRightGetterDelegate<TValue>(int rcol)
			{
				return this._rightRow.GetGetter<TValue>(rcol);
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x0001CF1C File Offset: 0x0001B11C
			private void RelativeIndexGetter(ref DvInt8 value)
			{
				Contracts.Check(this._ch, base.State == 1, "The cursor is not in valid state.");
				value = this._relativeIndex;
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x0001CF44 File Offset: 0x0001B144
			private static bool IsRightColumnNeededForCursor(LeftJoinDataView parent, Func<int, bool> predicate, int rcol)
			{
				int num;
				return parent._isRightColumnKey[rcol] || (parent._schema.TryMapRightColToJointCol(rcol, out num) && predicate(num));
			}

			// Token: 0x04000296 RID: 662
			private readonly LeftJoinDataView _parent;

			// Token: 0x04000297 RID: 663
			private readonly LeftJoinDataView.JointSchema _schema;

			// Token: 0x04000298 RID: 664
			protected readonly IRowSeeker _rightRow;

			// Token: 0x04000299 RID: 665
			protected readonly IRowCursor _leftCursor;

			// Token: 0x0400029A RID: 666
			private readonly Delegate[] _getters;

			// Token: 0x0400029B RID: 667
			private readonly LeftJoinDataViewBase.KeyComparer _kc;

			// Token: 0x0400029C RID: 668
			protected readonly int _irowRightLim;

			// Token: 0x0400029D RID: 669
			protected int _irowRight;

			// Token: 0x0400029E RID: 670
			private DvInt8 _relativeIndex;
		}

		// Token: 0x02000106 RID: 262
		private sealed class OuterJoinRowCursor : LeftJoinDataView.RowCursorBase
		{
			// Token: 0x06000556 RID: 1366 RVA: 0x0001CF76 File Offset: 0x0001B176
			public OuterJoinRowCursor(LeftJoinDataView parent, IRowCursor leftCursor, Func<int, bool> predicate)
				: base(parent, leftCursor, predicate)
			{
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x0001CF81 File Offset: 0x0001B181
			protected override bool MoveNextCore()
			{
				if (this._irowRight >= 0 && base.TryMoveRightSameKey())
				{
					return true;
				}
				if (!this._leftCursor.MoveNext())
				{
					return false;
				}
				base.LookupKeys();
				return true;
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
			protected override ValueGetter<TValue> CreateRightGetterDelegate<TValue>(int colRight)
			{
				ValueGetter<TValue> rightRowGetter = base.CreateRightGetterDelegate<TValue>(colRight);
				ColumnType columnType = this._rightRow.Schema.GetColumnType(colRight);
				ValueGetter<TValue> missingValueGetter = LeftJoinDataView.OuterJoinRowCursor.CreateMissingValueGetter<TValue>(columnType);
				return delegate(ref TValue value)
				{
					if (this._irowRight < 0)
					{
						missingValueGetter.Invoke(ref value);
						return;
					}
					rightRowGetter.Invoke(ref value);
				};
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x0001D034 File Offset: 0x0001B234
			private static ValueGetter<TValue> CreateMissingValueGetter<TValue>(ColumnType type)
			{
				if (!type.IsVector)
				{
					return Conversions.Instance.GetNAOrDefaultGetter<TValue>(type);
				}
				Func<int, ValueGetter<VBuffer<int>>> func = new Func<int, ValueGetter<VBuffer<int>>>(LeftJoinDataView.OuterJoinRowCursor.CreateMissingValueGetterVecItem<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.ItemType.RawType });
				object obj = methodInfo.Invoke(null, new object[] { type.VectorSize });
				return (ValueGetter<TValue>)obj;
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
			private static ValueGetter<VBuffer<TItem>> CreateMissingValueGetterVecItem<TItem>(int length)
			{
				return delegate(ref VBuffer<TItem> value)
				{
					value = new VBuffer<TItem>(length, 0, value.Values, value.Indices);
				};
			}
		}

		// Token: 0x02000107 RID: 263
		private sealed class InnerJoinRowCursor : LeftJoinDataView.RowCursorBase
		{
			// Token: 0x0600055B RID: 1371 RVA: 0x0001D0FE File Offset: 0x0001B2FE
			public InnerJoinRowCursor(LeftJoinDataView parent, IRowCursor leftCursor, Func<int, bool> predicate)
				: base(parent, leftCursor, predicate)
			{
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x0001D109 File Offset: 0x0001B309
			protected override bool MoveNextCore()
			{
				if (this._irowRight >= 0 && base.TryMoveRightSameKey())
				{
					return true;
				}
				while (this._leftCursor.MoveNext())
				{
					base.LookupKeys();
					if (this._irowRight >= 0)
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}
