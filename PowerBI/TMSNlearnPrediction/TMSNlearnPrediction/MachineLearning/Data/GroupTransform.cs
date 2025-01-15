using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000B8 RID: 184
	public sealed class GroupTransform : TransformBase
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00015E2A File Offset: 0x0001402A
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("GRP TRNS", 65537U, 65537U, 65537U, "GroupTransform", null);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00015E4C File Offset: 0x0001404C
		public GroupTransform(GroupTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "GroupTransform", input)
		{
			Contracts.CheckValue<GroupTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<string>(args.groupKey) > 0, "groupKey", "There must be at least one group key");
			this._schema = new GroupTransform.GroupSchema(this._host, this._input.Schema, args.groupKey, args.column ?? new string[0]);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00015EF0 File Offset: 0x000140F0
		public static GroupTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(GroupTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			IHost h = env.Register("GroupTransform");
			return HostExtensions.Apply<GroupTransform>(h, "Loading Model", (IChannel ch) => new GroupTransform(ctx, h, input));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015F7B File Offset: 0x0001417B
		private GroupTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			this._schema = new GroupTransform.GroupSchema(input.Schema, host, ctx);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00015F98 File Offset: 0x00014198
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(GroupTransform.GetVersionInfo());
			this._schema.Save(ctx);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00015FC8 File Offset: 0x000141C8
		public override long? GetRowCount(bool lazy = true)
		{
			return null;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00015FDE File Offset: 0x000141DE
		public override ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015FE6 File Offset: 0x000141E6
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			return new GroupTransform.Cursor(this, predicate);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00016000 File Offset: 0x00014200
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return new bool?(false);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00016008 File Offset: 0x00014208
		public override bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001600C File Offset: 0x0001420C
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursorCore(predicate, null) };
		}

		// Token: 0x04000193 RID: 403
		public const string Summary = "Groups values of a scalar column into a vector, by a contiguous group ID";

		// Token: 0x04000194 RID: 404
		private const string RegistrationName = "GroupTransform";

		// Token: 0x04000195 RID: 405
		public const string LoaderSignature = "GroupTransform";

		// Token: 0x04000196 RID: 406
		private readonly GroupTransform.GroupSchema _schema;

		// Token: 0x020000B9 RID: 185
		public sealed class Arguments
		{
			// Token: 0x04000197 RID: 407
			[Argument(4, HelpText = "Columns to group by", ShortName = "g", SortOrder = 1, Purpose = "ColumnSelector")]
			public string[] groupKey;

			// Token: 0x04000198 RID: 408
			[Argument(4, HelpText = "Columns to group together", ShortName = "col", SortOrder = 2)]
			public string[] column;
		}

		// Token: 0x020000BA RID: 186
		private sealed class GroupSchema : ISchema
		{
			// Token: 0x060003B6 RID: 950 RVA: 0x00016070 File Offset: 0x00014270
			public GroupSchema(IExceptionContext ectx, ISchema inputSchema, string[] groupColumns, string[] keepColumns)
			{
				this._ectx = ectx;
				this._input = inputSchema;
				this._groupColumns = groupColumns;
				this.GroupIds = this.GetColumnIds(inputSchema, groupColumns, (string x) => Contracts.ExceptUserArg(this._ectx, "groupKey", x));
				this._groupCount = this.GroupIds.Length;
				this._keepColumns = keepColumns;
				this.KeepIds = this.GetColumnIds(inputSchema, keepColumns, (string x) => Contracts.ExceptUserArg(this._ectx, "column", x));
				this._columnTypes = GroupTransform.GroupSchema.BuildColumnTypes(this._input, this.KeepIds);
				this._columnNameMap = this.BuildColumnNameMap();
			}

			// Token: 0x060003B7 RID: 951 RVA: 0x00016114 File Offset: 0x00014314
			public GroupSchema(ISchema inputSchema, IHostEnvironment env, ModelLoadContext ctx)
			{
				this._ectx = env.Register("GroupTransform");
				this._input = inputSchema;
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._ectx, num > 0);
				this._groupColumns = new string[num];
				for (int i = 0; i < num; i++)
				{
					this._groupColumns[i] = ctx.LoadNonEmptyString();
				}
				int num2 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._ectx, num2 >= 0);
				this._keepColumns = new string[num2];
				for (int j = 0; j < num2; j++)
				{
					this._keepColumns[j] = ctx.LoadNonEmptyString();
				}
				this.GroupIds = this.GetColumnIds(inputSchema, this._groupColumns, new Func<string, Exception>(this._ectx.Except));
				this._groupCount = this.GroupIds.Length;
				this.KeepIds = this.GetColumnIds(inputSchema, this._keepColumns, new Func<string, Exception>(this._ectx.Except));
				this._columnTypes = GroupTransform.GroupSchema.BuildColumnTypes(this._input, this.KeepIds);
				this._columnNameMap = this.BuildColumnNameMap();
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x00016240 File Offset: 0x00014440
			private Dictionary<string, int> BuildColumnNameMap()
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (int i = 0; i < this._groupCount; i++)
				{
					dictionary[this._groupColumns[i]] = i;
				}
				for (int j = 0; j < this._keepColumns.Length; j++)
				{
					dictionary[this._keepColumns[j]] = j + this._groupCount;
				}
				return dictionary;
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x000162A0 File Offset: 0x000144A0
			private static ColumnType[] BuildColumnTypes(ISchema input, int[] ids)
			{
				ColumnType[] array = new ColumnType[ids.Length];
				for (int i = 0; i < ids.Length; i++)
				{
					ColumnType columnType = input.GetColumnType(ids[i]);
					array[i] = new VectorType(columnType.AsPrimitive, 0);
				}
				return array;
			}

			// Token: 0x060003BA RID: 954 RVA: 0x000162E0 File Offset: 0x000144E0
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write(this._groupColumns.Length);
				foreach (string text in this._groupColumns)
				{
					ctx.SaveString(text);
				}
				ctx.Writer.Write(this._keepColumns.Length);
				foreach (string text2 in this._keepColumns)
				{
					ctx.SaveString(text2);
				}
			}

			// Token: 0x060003BB RID: 955 RVA: 0x0001635C File Offset: 0x0001455C
			private int[] GetColumnIds(ISchema schema, string[] names, Func<string, Exception> except)
			{
				int[] array = new int[names.Length];
				for (int i = 0; i < names.Length; i++)
				{
					int num;
					if (!schema.TryGetColumnIndex(names[i], ref num))
					{
						throw except(string.Format("Could not find column '{0}'", names[i]));
					}
					ColumnType columnType = schema.GetColumnType(num);
					if (!columnType.IsPrimitive)
					{
						throw except(string.Format("Column '{0}' has type '{1}', but must have a primitive type", names[i], columnType));
					}
					array[i] = num;
				}
				return array;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060003BC RID: 956 RVA: 0x000163CB File Offset: 0x000145CB
			public int ColumnCount
			{
				get
				{
					return this._groupCount + this.KeepIds.Length;
				}
			}

			// Token: 0x060003BD RID: 957 RVA: 0x000163DC File Offset: 0x000145DC
			public void CheckColumnInRange(int col)
			{
				Contracts.Check(this._ectx, 0 <= col && col < this._groupCount + this.KeepIds.Length);
			}

			// Token: 0x060003BE RID: 958 RVA: 0x00016402 File Offset: 0x00014602
			public bool TryGetColumnIndex(string name, out int col)
			{
				return this._columnNameMap.TryGetValue(name, out col);
			}

			// Token: 0x060003BF RID: 959 RVA: 0x00016411 File Offset: 0x00014611
			public string GetColumnName(int col)
			{
				this.CheckColumnInRange(col);
				if (col < this._groupCount)
				{
					return this._groupColumns[col];
				}
				return this._keepColumns[col - this._groupCount];
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x0001643B File Offset: 0x0001463B
			public ColumnType GetColumnType(int col)
			{
				this.CheckColumnInRange(col);
				if (col < this._groupCount)
				{
					return this._input.GetColumnType(this.GroupIds[col]);
				}
				return this._columnTypes[col - this._groupCount];
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x00016470 File Offset: 0x00014670
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				this.CheckColumnInRange(col);
				if (col < this._groupCount)
				{
					return this._input.GetMetadataTypes(this.GroupIds[col]);
				}
				col -= this._groupCount;
				List<KeyValuePair<string, ColumnType>> list = new List<KeyValuePair<string, ColumnType>>();
				foreach (string text in GroupTransform.GroupSchema.PreservedMetadata)
				{
					ColumnType metadataTypeOrNull = this._input.GetMetadataTypeOrNull(text, this.KeepIds[col]);
					if (metadataTypeOrNull != null)
					{
						list.Add(MetadataUtils.GetPair(metadataTypeOrNull, text));
					}
				}
				return list;
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x000164F4 File Offset: 0x000146F4
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				this.CheckColumnInRange(col);
				if (col < this._groupCount)
				{
					return this._input.GetMetadataTypeOrNull(kind, this.GroupIds[col]);
				}
				col -= this._groupCount;
				if (GroupTransform.GroupSchema.PreservedMetadata.Contains(kind))
				{
					return this._input.GetMetadataTypeOrNull(kind, this.KeepIds[col]);
				}
				return null;
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x00016554 File Offset: 0x00014754
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				this.CheckColumnInRange(col);
				if (col < this._groupCount)
				{
					this._input.GetMetadata<TValue>(kind, this.GroupIds[col], ref value);
					return;
				}
				col -= this._groupCount;
				if (GroupTransform.GroupSchema.PreservedMetadata.Contains(kind))
				{
					this._input.GetMetadata<TValue>(kind, this.KeepIds[col], ref value);
				}
				throw MetadataUtils.ExceptGetMetadata(this._ectx);
			}

			// Token: 0x04000199 RID: 409
			private static readonly string[] PreservedMetadata = new string[] { "IsNormalized", "KeyValues" };

			// Token: 0x0400019A RID: 410
			private readonly IExceptionContext _ectx;

			// Token: 0x0400019B RID: 411
			private readonly ISchema _input;

			// Token: 0x0400019C RID: 412
			private readonly string[] _groupColumns;

			// Token: 0x0400019D RID: 413
			private readonly string[] _keepColumns;

			// Token: 0x0400019E RID: 414
			public readonly int[] GroupIds;

			// Token: 0x0400019F RID: 415
			public readonly int[] KeepIds;

			// Token: 0x040001A0 RID: 416
			private readonly int _groupCount;

			// Token: 0x040001A1 RID: 417
			private readonly ColumnType[] _columnTypes;

			// Token: 0x040001A2 RID: 418
			private readonly Dictionary<string, int> _columnNameMap;
		}

		// Token: 0x020000BB RID: 187
		public sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060003C7 RID: 967 RVA: 0x000165EA File Offset: 0x000147EA
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x000165EE File Offset: 0x000147EE
			public ISchema Schema
			{
				get
				{
					return this._parent.Schema;
				}
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x00016618 File Offset: 0x00014818
			public Cursor(GroupTransform parent, Func<int, bool> predicate)
				: base(parent._host)
			{
				this._parent = parent;
				GroupTransform.GroupSchema schema = this._parent._schema;
				this._active = Utils.BuildArray<bool>(schema.ColumnCount, predicate);
				this._groupCount = schema.GroupIds.Length;
				bool[] srcActiveLeading = new bool[this._parent._input.Schema.ColumnCount];
				foreach (int num in schema.GroupIds)
				{
					srcActiveLeading[num] = true;
				}
				this._leadingCursor = parent._input.GetRowCursor((int x) => srcActiveLeading[x], null);
				bool[] srcActiveTrailing = new bool[this._parent._input.Schema.ColumnCount];
				for (int j = 0; j < this._groupCount; j++)
				{
					if (this._active[j])
					{
						srcActiveTrailing[schema.GroupIds[j]] = true;
					}
				}
				for (int k = 0; k < schema.KeepIds.Length; k++)
				{
					if (this._active[k + this._groupCount])
					{
						srcActiveTrailing[schema.KeepIds[k]] = true;
					}
				}
				this._trailingCursor = parent._input.GetRowCursor((int x) => srcActiveTrailing[x], null);
				this._groupCheckers = new GroupTransform.Cursor.GroupKeyColumnChecker[this._groupCount];
				for (int l = 0; l < this._groupCount; l++)
				{
					this._groupCheckers[l] = new GroupTransform.Cursor.GroupKeyColumnChecker(this._leadingCursor, this._parent._schema.GroupIds[l]);
				}
				this._aggregators = new GroupTransform.Cursor.KeepColumnAggregator[this._parent._schema.KeepIds.Length];
				for (int m = 0; m < this._aggregators.Length; m++)
				{
					if (this._active[m + this._groupCount])
					{
						this._aggregators[m] = GroupTransform.Cursor.KeepColumnAggregator.Create(this._trailingCursor, this._parent._schema.KeepIds[m]);
					}
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00016831 File Offset: 0x00014A31
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return this._trailingCursor.GetIdGetter();
			}

			// Token: 0x060003CB RID: 971 RVA: 0x0001683E File Offset: 0x00014A3E
			public bool IsColumnActive(int col)
			{
				this._parent._schema.CheckColumnInRange(col);
				return this._active[col];
			}

			// Token: 0x060003CC RID: 972 RVA: 0x0001686C File Offset: 0x00014A6C
			protected override bool MoveNextCore()
			{
				if (this._leadingCursor.State == null)
				{
					this._leadingCursor.MoveNext();
				}
				if (this._leadingCursor.State == 2)
				{
					return false;
				}
				int num = 0;
				while (this._leadingCursor.State == 1 && this.IsSameGroup())
				{
					num++;
					this._leadingCursor.MoveNext();
				}
				foreach (GroupTransform.Cursor.KeepColumnAggregator keepColumnAggregator in this._aggregators.Where((GroupTransform.Cursor.KeepColumnAggregator x) => x != null))
				{
					keepColumnAggregator.SetSize(num);
				}
				for (int i = 0; i < num; i++)
				{
					this._trailingCursor.MoveNext();
					foreach (GroupTransform.Cursor.KeepColumnAggregator keepColumnAggregator2 in this._aggregators.Where((GroupTransform.Cursor.KeepColumnAggregator x) => x != null))
					{
						keepColumnAggregator2.ReadValue(i);
					}
				}
				return true;
			}

			// Token: 0x060003CD RID: 973 RVA: 0x000169B0 File Offset: 0x00014BB0
			private bool IsSameGroup()
			{
				bool flag = true;
				foreach (GroupTransform.Cursor.GroupKeyColumnChecker groupKeyColumnChecker in this._groupCheckers)
				{
					flag = groupKeyColumnChecker.IsSameKey() && flag;
				}
				return flag;
			}

			// Token: 0x060003CE RID: 974 RVA: 0x000169E7 File Offset: 0x00014BE7
			public override void Dispose()
			{
				this._leadingCursor.Dispose();
				this._trailingCursor.Dispose();
				base.Dispose();
			}

			// Token: 0x060003CF RID: 975 RVA: 0x00016A08 File Offset: 0x00014C08
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				this._parent._schema.CheckColumnInRange(col);
				if (!this._active[col])
				{
					throw Contracts.ExceptParam(this._ch, "col", "Column #{0} is not active", new object[] { col });
				}
				if (col < this._groupCount)
				{
					return this._trailingCursor.GetGetter<TValue>(this._parent._schema.GroupIds[col]);
				}
				return this._aggregators[col - this._groupCount].GetGetter<TValue>(this._ch);
			}

			// Token: 0x040001A3 RID: 419
			private readonly GroupTransform _parent;

			// Token: 0x040001A4 RID: 420
			private readonly bool[] _active;

			// Token: 0x040001A5 RID: 421
			private readonly int _groupCount;

			// Token: 0x040001A6 RID: 422
			private readonly IRowCursor _leadingCursor;

			// Token: 0x040001A7 RID: 423
			private readonly IRowCursor _trailingCursor;

			// Token: 0x040001A8 RID: 424
			private readonly GroupTransform.Cursor.GroupKeyColumnChecker[] _groupCheckers;

			// Token: 0x040001A9 RID: 425
			private readonly GroupTransform.Cursor.KeepColumnAggregator[] _aggregators;

			// Token: 0x020000BC RID: 188
			private sealed class GroupKeyColumnChecker
			{
				// Token: 0x060003D2 RID: 978 RVA: 0x00016AF8 File Offset: 0x00014CF8
				private static Func<bool> MakeSameChecker<T>(IRow row, int col) where T : IEquatable<T>
				{
					T oldValue = default(T);
					T newValue = default(T);
					bool first = true;
					ValueGetter<T> getter = row.GetGetter<T>(col);
					return delegate
					{
						getter.Invoke(ref newValue);
						bool flag = first || oldValue.Equals(newValue);
						oldValue = newValue;
						first = false;
						return flag;
					};
				}

				// Token: 0x060003D3 RID: 979 RVA: 0x00016B44 File Offset: 0x00014D44
				public GroupKeyColumnChecker(IRow row, int col)
				{
					ColumnType columnType = row.Schema.GetColumnType(col);
					Func<IRow, int, Func<bool>> func = new Func<IRow, int, Func<bool>>(GroupTransform.Cursor.GroupKeyColumnChecker.MakeSameChecker<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.RawType });
					this.IsSameKey = (Func<bool>)methodInfo.Invoke(null, new object[] { row, col });
				}

				// Token: 0x040001AC RID: 428
				public readonly Func<bool> IsSameKey;
			}

			// Token: 0x020000BD RID: 189
			private abstract class KeepColumnAggregator
			{
				// Token: 0x060003D4 RID: 980
				public abstract ValueGetter<T> GetGetter<T>(IExceptionContext ctx);

				// Token: 0x060003D5 RID: 981
				public abstract void SetSize(int size);

				// Token: 0x060003D6 RID: 982
				public abstract void ReadValue(int position);

				// Token: 0x060003D7 RID: 983 RVA: 0x00016BC0 File Offset: 0x00014DC0
				public static GroupTransform.Cursor.KeepColumnAggregator Create(IRow row, int col)
				{
					ColumnType columnType = row.Schema.GetColumnType(col);
					Type typeFromHandle = typeof(GroupTransform.Cursor.KeepColumnAggregator.ListAggregator<>);
					ConstructorInfo constructor = typeFromHandle.MakeGenericType(new Type[] { columnType.RawType }).GetConstructor(new Type[]
					{
						typeof(IRow),
						typeof(int)
					});
					return constructor.Invoke(new object[] { row, col }) as GroupTransform.Cursor.KeepColumnAggregator;
				}

				// Token: 0x020000BE RID: 190
				private sealed class ListAggregator<TValue> : GroupTransform.Cursor.KeepColumnAggregator
				{
					// Token: 0x060003D9 RID: 985 RVA: 0x00016C54 File Offset: 0x00014E54
					public ListAggregator(IRow row, int col)
					{
						this._srcGetter = row.GetGetter<TValue>(col);
						this._getter = new ValueGetter<VBuffer<TValue>>(this.Getter);
					}

					// Token: 0x060003DA RID: 986 RVA: 0x00016C7C File Offset: 0x00014E7C
					private void Getter(ref VBuffer<TValue> dst)
					{
						TValue[] array = ((Utils.Size<TValue>(dst.Values) < this._size) ? new TValue[this._size] : dst.Values);
						Array.Copy(this._buffer, array, this._size);
						dst = new VBuffer<TValue>(this._size, array, dst.Indices);
					}

					// Token: 0x060003DB RID: 987 RVA: 0x00016CDA File Offset: 0x00014EDA
					public override ValueGetter<T> GetGetter<T>(IExceptionContext ctx)
					{
						Contracts.Check(ctx, typeof(T) == typeof(VBuffer<TValue>));
						return (ValueGetter<T>)this._getter;
					}

					// Token: 0x060003DC RID: 988 RVA: 0x00016D06 File Offset: 0x00014F06
					public override void SetSize(int size)
					{
						Array.Resize<TValue>(ref this._buffer, size);
						this._size = size;
					}

					// Token: 0x060003DD RID: 989 RVA: 0x00016D1B File Offset: 0x00014F1B
					public override void ReadValue(int position)
					{
						this._srcGetter.Invoke(ref this._buffer[position]);
					}

					// Token: 0x040001AD RID: 429
					private readonly ValueGetter<TValue> _srcGetter;

					// Token: 0x040001AE RID: 430
					private readonly Delegate _getter;

					// Token: 0x040001AF RID: 431
					private TValue[] _buffer;

					// Token: 0x040001B0 RID: 432
					private int _size;
				}
			}
		}
	}
}
