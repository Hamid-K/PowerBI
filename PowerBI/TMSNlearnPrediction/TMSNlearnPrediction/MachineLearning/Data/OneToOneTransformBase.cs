using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000027 RID: 39
	public abstract class OneToOneTransformBase : RowToRowTransformBase, ITransposeDataView, IDataView, ISchematized
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000070EF File Offset: 0x000052EF
		protected ITransposeSchema InputTransposeSchema
		{
			get
			{
				if (this.InputTranspose == null)
				{
					return null;
				}
				return this.InputTranspose.TransposeSchema;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007108 File Offset: 0x00005308
		protected OneToOneTransformBase(IHostEnvironment env, string name, OneToOneColumn[] column, IDataView input, Func<ColumnType, string> testType)
			: base(env, name, input)
		{
			Contracts.CheckUserArg(this._host, Utils.Size<OneToOneColumn>(column) > 0, "column");
			this.InputTranspose = this._input as ITransposeDataView;
			this._bindings = OneToOneTransformBase.Bindings.Create(this, column, this._input.Schema, this.InputTransposeSchema, testType);
			this.Infos = this._bindings.Infos;
			this._md = new MetadataDispatcher(this.Infos.Length);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007190 File Offset: 0x00005390
		protected OneToOneTransformBase(IHost host, OneToOneColumn[] column, IDataView input, Func<ColumnType, string> testType)
			: base(host, input)
		{
			Contracts.CheckUserArg(this._host, Utils.Size<OneToOneColumn>(column) > 0, "column");
			this.InputTranspose = this._input as ITransposeDataView;
			this._bindings = OneToOneTransformBase.Bindings.Create(this, column, this._input.Schema, this.InputTransposeSchema, testType);
			this.Infos = this._bindings.Infos;
			this._md = new MetadataDispatcher(this.Infos.Length);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007214 File Offset: 0x00005414
		protected OneToOneTransformBase(ModelLoadContext ctx, IHost host, IDataView input, Func<ColumnType, string> testType)
			: base(host, input)
		{
			Contracts.CheckValue<ModelLoadContext>(this._host, ctx, "ctx");
			this.InputTranspose = this._input as ITransposeDataView;
			this._bindings = OneToOneTransformBase.Bindings.Create(this, ctx, this._input.Schema, this.InputTransposeSchema, testType);
			this.Infos = this._bindings.Infos;
			this._md = new MetadataDispatcher(this.Infos.Length);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000072DC File Offset: 0x000054DC
		protected OneToOneTransformBase(IHostEnvironment env, string name, OneToOneTransformBase transform, IDataView newInput, Func<ColumnType, string> checkType)
			: base(env, name, newInput)
		{
			this.InputTranspose = this._input as ITransposeDataView;
			OneToOneColumn[] array = transform.Infos.Select((OneToOneTransformBase.ColInfo x) => new OneToOneTransformBase.ColumnTmp
			{
				name = x.Name,
				source = transform.Source.Schema.GetColumnName(x.Source)
			}).ToArray<OneToOneTransformBase.ColumnTmp>();
			this._bindings = OneToOneTransformBase.Bindings.Create(this, array, newInput.Schema, this.InputTransposeSchema, checkType);
			this.Infos = this._bindings.Infos;
			this._md = new MetadataDispatcher(this.Infos.Length);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000737A File Offset: 0x0000557A
		protected MetadataDispatcher Metadata
		{
			get
			{
				return this._md;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007382 File Offset: 0x00005582
		protected void SaveBase(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			this._bindings.Save(ctx);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000073A1 File Offset: 0x000055A1
		public sealed override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000073A9 File Offset: 0x000055A9
		public ITransposeSchema TransposeSchema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000073B1 File Offset: 0x000055B1
		protected int ColumnIndex(int iinfo)
		{
			return this._bindings.MapIinfoToCol(iinfo);
		}

		// Token: 0x060000D0 RID: 208
		protected abstract ColumnType GetColumnTypeCore(int iinfo);

		// Token: 0x060000D1 RID: 209 RVA: 0x000073BF File Offset: 0x000055BF
		protected virtual VectorType GetSlotTypeCore(int iinfo)
		{
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000073C2 File Offset: 0x000055C2
		protected virtual void ActivateSourceColumns(int iinfo, bool[] active)
		{
			active[this.Infos[iinfo].Source] = true;
		}

		// Token: 0x060000D3 RID: 211
		protected abstract Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer);

		// Token: 0x060000D4 RID: 212 RVA: 0x000073D4 File Offset: 0x000055D4
		protected ValueGetter<T> GetSrcGetter<T>(IRow input, int iinfo)
		{
			int source = this.Infos[iinfo].Source;
			return input.GetGetter<T>(source);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000073F8 File Offset: 0x000055F8
		protected Delegate GetSrcGetter(ColumnType typeDst, IRow row, int iinfo)
		{
			Contracts.CheckValue<ColumnType>(this._host, typeDst, "typeDst");
			Contracts.CheckValue<IRow>(this._host, row, "row");
			Func<IRow, int, ValueGetter<int>> func = new Func<IRow, int, ValueGetter<int>>(this.GetSrcGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { typeDst.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { row, iinfo });
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007478 File Offset: 0x00005678
		protected sealed override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this.WantParallelCursors(predicate))
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000749E File Offset: 0x0000569E
		protected virtual bool WantParallelCursors(Func<int, bool> predicate)
		{
			return this._bindings.AnyNewColumnsActive(predicate);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000074AC File Offset: 0x000056AC
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new OneToOneTransformBase.RowCursor(this._host, this, rowCursor, active);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000074F0 File Offset: 0x000056F0
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] array = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			if (array.Length == 1 && n > 1 && this.WantParallelCursors(predicate))
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new OneToOneTransformBase.RowCursor(this._host, this, array[i], active);
			}
			return array2;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007590 File Offset: 0x00005790
		protected Exception ExceptGetSlotCursor(int col)
		{
			return Contracts.ExceptParam(this._host, "col", "Bad call to GetSlotCursor on untransposable column '{0}'", new object[] { this.Schema.GetColumnName(col) });
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000075CC File Offset: 0x000057CC
		public ISlotCursor GetSlotCursor(int col)
		{
			Contracts.CheckParam(this._host, 0 <= col && col < this._bindings.ColumnCount, "col");
			bool flag;
			int num = this._bindings.MapColumnIndex(out flag, col);
			if (flag)
			{
				if (this.InputTranspose != null)
				{
					return this.InputTranspose.GetSlotCursor(num);
				}
				throw this.ExceptGetSlotCursor(col);
			}
			else
			{
				if (this.GetSlotTypeCore(num) == null)
				{
					throw this.ExceptGetSlotCursor(col);
				}
				return this.GetSlotCursorCore(num);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007645 File Offset: 0x00005845
		protected virtual ISlotCursor GetSlotCursorCore(int iinfo)
		{
			throw Contracts.ExceptNotImpl(this._host, "Data view indicated it could transpose a column, but apparently it could not");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007657 File Offset: 0x00005857
		protected static string TestIsText(ColumnType type)
		{
			if (type.IsText)
			{
				return null;
			}
			return "Expected Text type";
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007668 File Offset: 0x00005868
		protected static string TestIsTextItem(ColumnType type)
		{
			if (type.ItemType.IsText)
			{
				return null;
			}
			return "Expected Text type";
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000767E File Offset: 0x0000587E
		protected static string TestIsTextVector(ColumnType type)
		{
			if (type.ItemType.IsText && type.IsVector)
			{
				return null;
			}
			return "Expected vector of Text type";
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000769C File Offset: 0x0000589C
		protected static string TestIsFloatItem(ColumnType type)
		{
			if (type.ItemType == NumberType.Float)
			{
				return null;
			}
			return "Expected R4 or a vector of R4";
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000076B2 File Offset: 0x000058B2
		protected static string TestIsFloatVector(ColumnType type)
		{
			if (!type.IsVector || type.ItemType != NumberType.Float)
			{
				return "Expected Float vector";
			}
			return null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000076D0 File Offset: 0x000058D0
		protected static string TestIsKnownSizeFloatVector(ColumnType type)
		{
			if (!type.IsKnownSizeVector || type.ItemType != NumberType.Float)
			{
				return "Expected Float vector of known size";
			}
			return null;
		}

		// Token: 0x04000062 RID: 98
		private readonly OneToOneTransformBase.Bindings _bindings;

		// Token: 0x04000063 RID: 99
		private readonly MetadataDispatcher _md;

		// Token: 0x04000064 RID: 100
		protected readonly OneToOneTransformBase.ColInfo[] Infos;

		// Token: 0x04000065 RID: 101
		protected readonly ITransposeDataView InputTranspose;

		// Token: 0x02000028 RID: 40
		public sealed class ColInfo
		{
			// Token: 0x060000E3 RID: 227 RVA: 0x000076EE File Offset: 0x000058EE
			public ColInfo(string name, int colSrc, ColumnType typeSrc, VectorType slotTypeSrc)
			{
				this.Name = name;
				this.Source = colSrc;
				this.TypeSrc = typeSrc;
				this.SlotTypeSrc = slotTypeSrc;
			}

			// Token: 0x04000066 RID: 102
			public readonly string Name;

			// Token: 0x04000067 RID: 103
			public readonly int Source;

			// Token: 0x04000068 RID: 104
			public readonly ColumnType TypeSrc;

			// Token: 0x04000069 RID: 105
			public readonly VectorType SlotTypeSrc;
		}

		// Token: 0x0200002B RID: 43
		private sealed class Bindings : ColumnBindingsBase, ITransposeSchema, ISchema
		{
			// Token: 0x060000FB RID: 251 RVA: 0x00007C3C File Offset: 0x00005E3C
			private Bindings(OneToOneTransformBase parent, OneToOneTransformBase.ColInfo[] infos, ISchema input, bool user, string[] names)
				: base(input, user, names)
			{
				this._parent = parent;
				this._inputTransposed = ((this._parent.InputTranspose == null) ? null : this._parent.InputTranspose.TransposeSchema);
				this.Infos = infos;
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00007C88 File Offset: 0x00005E88
			public static OneToOneTransformBase.Bindings Create(OneToOneTransformBase parent, OneToOneColumn[] column, ISchema input, ITransposeSchema transInput, Func<ColumnType, string> testType)
			{
				IHost host = parent._host;
				Contracts.CheckUserArg(host, Utils.Size<OneToOneColumn>(column) > 0, "column");
				string[] array = new string[column.Length];
				OneToOneTransformBase.ColInfo[] array2 = new OneToOneTransformBase.ColInfo[column.Length];
				for (int i = 0; i < array.Length; i++)
				{
					OneToOneColumn oneToOneColumn = column[i];
					Contracts.CheckUserArg(host, oneToOneColumn.TrySanitize(), "name", "Invalid new column name");
					array[i] = oneToOneColumn.name;
					int num;
					if (!input.TryGetColumnIndex(oneToOneColumn.source, ref num))
					{
						throw Contracts.ExceptUserArg(host, "column", "Source column '{0}' not found", new object[] { oneToOneColumn.source });
					}
					ColumnType columnType = input.GetColumnType(num);
					if (testType != null)
					{
						string text = testType(columnType);
						if (text != null)
						{
							throw Contracts.ExceptUserArg(host, "column", "Source column '{0}' has invalid type ('{1}'): {2}.", new object[] { oneToOneColumn.source, columnType, text });
						}
					}
					VectorType vectorType = ((transInput == null) ? null : transInput.GetSlotType(num));
					array2[i] = new OneToOneTransformBase.ColInfo(array[i], num, columnType, vectorType);
				}
				return new OneToOneTransformBase.Bindings(parent, array2, input, true, array);
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00007DB0 File Offset: 0x00005FB0
			public static OneToOneTransformBase.Bindings Create(OneToOneTransformBase parent, ModelLoadContext ctx, ISchema input, ITransposeSchema transInput, Func<ColumnType, string> testType)
			{
				IHost host = parent._host;
				Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(host, num > 0);
				string[] array = new string[num];
				OneToOneTransformBase.ColInfo[] array2 = new OneToOneTransformBase.ColInfo[num];
				for (int i = 0; i < num; i++)
				{
					string text = ctx.LoadNonEmptyString();
					array[i] = text;
					string text2 = ctx.LoadStringOrNull();
					string text3 = text2 ?? text;
					Contracts.CheckDecode(host, !string.IsNullOrEmpty(text3));
					int num2;
					if (!input.TryGetColumnIndex(text3, ref num2))
					{
						throw Contracts.ExceptDecode(host, "Source column '{0}' is required but not found", new object[] { text3 });
					}
					ColumnType columnType = input.GetColumnType(num2);
					if (testType != null)
					{
						string text4 = testType(columnType);
						if (text4 != null)
						{
							throw Contracts.ExceptDecode(host, "Source column '{0}' has invalid type ('{1}'): {2}.", new object[] { text3, columnType, text4 });
						}
					}
					VectorType vectorType = ((transInput == null) ? null : transInput.GetSlotType(num2));
					array2[i] = new OneToOneTransformBase.ColInfo(text, num2, columnType, vectorType);
				}
				return new OneToOneTransformBase.Bindings(parent, array2, input, false, array);
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00007ED4 File Offset: 0x000060D4
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write(this.Infos.Length);
				foreach (OneToOneTransformBase.ColInfo colInfo in this.Infos)
				{
					ctx.SaveNonEmptyString(colInfo.Name);
					ctx.SaveNonEmptyString(this.Input.GetColumnName(colInfo.Source));
				}
			}

			// Token: 0x060000FF RID: 255 RVA: 0x00007F54 File Offset: 0x00006154
			public Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = new bool[this.Input.ColumnCount];
				for (int i = 0; i < base.ColumnCount; i++)
				{
					if (predicate(i))
					{
						bool flag;
						int num = base.MapColumnIndex(out flag, i);
						if (flag)
						{
							active[num] = true;
						}
						else
						{
							this._parent.ActivateSourceColumns(num, active);
						}
					}
				}
				return (int col) => 0 <= col && col < active.Length && active[col];
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00007FCD File Offset: 0x000061CD
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return this._parent.GetColumnTypeCore(iinfo);
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00007FDC File Offset: 0x000061DC
			public VectorType GetSlotType(int col)
			{
				Contracts.CheckParam(this._parent._host, 0 <= col && col < base.ColumnCount, "col");
				bool flag;
				int num = base.MapColumnIndex(out flag, col);
				if (!flag)
				{
					return this._parent.GetSlotTypeCore(num);
				}
				if (this._inputTransposed != null)
				{
					return this._inputTransposed.GetSlotType(num);
				}
				return null;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x0000803E File Offset: 0x0000623E
			protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
			{
				return this._parent._md.GetMetadataTypes(iinfo);
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00008051 File Offset: 0x00006251
			protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
			{
				return this._parent._md.GetMetadataTypeOrNull(kind, iinfo);
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00008065 File Offset: 0x00006265
			protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
			{
				this._parent._md.GetMetadata<TValue>(this._parent._host, kind, iinfo, ref value);
			}

			// Token: 0x0400006F RID: 111
			private const string InvalidTypeErrorFormat = "Source column '{0}' has invalid type ('{1}'): {2}.";

			// Token: 0x04000070 RID: 112
			private readonly OneToOneTransformBase _parent;

			// Token: 0x04000071 RID: 113
			private readonly ITransposeSchema _inputTransposed;

			// Token: 0x04000072 RID: 114
			public readonly OneToOneTransformBase.ColInfo[] Infos;
		}

		// Token: 0x0200002D RID: 45
		private sealed class ColumnTmp : OneToOneColumn
		{
		}

		// Token: 0x0200002E RID: 46
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x0600010C RID: 268 RVA: 0x000081D8 File Offset: 0x000063D8
			public RowCursor(IChannelProvider provider, OneToOneTransformBase parent, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				this._bindings = parent._bindings;
				this._active = active;
				this._getters = new Delegate[parent.Infos.Length];
				List<Action> list = null;
				for (int i = 0; i < this._getters.Length; i++)
				{
					if (this.IsColumnActive(parent._bindings.MapIinfoToCol(i)))
					{
						Action action;
						this._getters[i] = parent.GetGetterCore(this._ch, base.Input, i, out action);
						if (action != null)
						{
							Utils.Add<Action>(ref list, action);
						}
					}
				}
				if (Utils.Size<Action>(list) > 0)
				{
					this._disposers = list.ToArray();
				}
			}

			// Token: 0x0600010D RID: 269 RVA: 0x0000827C File Offset: 0x0000647C
			public override void Dispose()
			{
				if (this._disposers != null)
				{
					foreach (Action action in this._disposers)
					{
						action();
					}
				}
				base.Dispose();
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600010E RID: 270 RVA: 0x000082B6 File Offset: 0x000064B6
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x0600010F RID: 271 RVA: 0x000082C0 File Offset: 0x000064C0
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				ValueGetter<TValue> valueGetter = this._getters[num] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00008336 File Offset: 0x00006536
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x04000075 RID: 117
			private readonly OneToOneTransformBase.Bindings _bindings;

			// Token: 0x04000076 RID: 118
			private readonly bool[] _active;

			// Token: 0x04000077 RID: 119
			private readonly Delegate[] _getters;

			// Token: 0x04000078 RID: 120
			private readonly Action[] _disposers;
		}
	}
}
