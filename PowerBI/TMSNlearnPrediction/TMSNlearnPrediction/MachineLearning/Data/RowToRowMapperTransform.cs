using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200013A RID: 314
	public sealed class RowToRowMapperTransform : RowToRowTransformBase
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x00021B63 File Offset: 0x0001FD63
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("ROW MPPR", 65537U, 65537U, 65537U, "RowToRowMapper", null);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00021B84 File Offset: 0x0001FD84
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00021B94 File Offset: 0x0001FD94
		public RowToRowMapperTransform(IHostEnvironment env, IDataView input, IRowMapper mapper)
			: base(env, "RowToRowMapperTransform", input)
		{
			Contracts.CheckValue<IRowMapper>(mapper, "mapper");
			this._mapper = mapper;
			this._bindings = new RowToRowMapperTransform.Bindings(input.Schema, this);
			RowToRowMapperTransform.CreateMetadata(this._bindings.OutputColInfos.Select((RowMapperColumnInfo info) => info.Metadata), this._bindings, out this._md);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00021C18 File Offset: 0x0001FE18
		private RowToRowMapperTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			ctx.LoadModel<IRowMapper, SignatureLoadRowMapper>(out this._mapper, "Mapper", new object[] { host, input.Schema });
			this._bindings = new RowToRowMapperTransform.Bindings(input.Schema, this);
			RowToRowMapperTransform.CreateMetadata(from info in this._mapper.GetOutputColumns()
				select info.Metadata, this._bindings, out this._md);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00021CC4 File Offset: 0x0001FEC4
		public static RowToRowMapperTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("RowToRowMapperTransform");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(RowToRowMapperTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<RowToRowMapperTransform>(h, "Loading Model", (IChannel ch) => new RowToRowMapperTransform(ctx, h, input));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00021D59 File Offset: 0x0001FF59
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(RowToRowMapperTransform.GetVersionInfo());
			ctx.SaveModel<IRowMapper>(this._mapper, "Mapper");
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00021D90 File Offset: 0x0001FF90
		private static void CreateMetadata(IEnumerable<ColumnMetadataInfo> infos, RowToRowMapperTransform.Bindings bindings, out MetadataDispatcher md)
		{
			md = new MetadataDispatcher(bindings.InfoCount);
			foreach (ColumnMetadataInfo columnMetadataInfo in infos)
			{
				if (columnMetadataInfo != null)
				{
					int num;
					bindings.TryGetInfoIndex(columnMetadataInfo.Name, out num);
					using (MetadataDispatcher.Builder builder = md.BuildMetadata(num))
					{
						foreach (KeyValuePair<string, MetadataInfo> keyValuePair in columnMetadataInfo.Infos())
						{
							Action<MetadataDispatcher.Builder, string, MetadataInfo<int>> action = new Action<MetadataDispatcher.Builder, string, MetadataInfo<int>>(RowToRowMapperTransform.AddGetter<int>);
							MethodInfo methodInfo = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { keyValuePair.Value.Type.RawType });
							methodInfo.Invoke(null, new object[] { builder, keyValuePair.Key, keyValuePair.Value });
						}
					}
				}
			}
			md.Seal();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00021ED0 File Offset: 0x000200D0
		private static void AddGetter<T>(MetadataDispatcher.Builder bldr, string kind, MetadataInfo<T> info)
		{
			bldr.AddGetter<T>(kind, info.Type, info.Getter);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00021EE8 File Offset: 0x000200E8
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this._bindings.AnyNewColumnsActive(predicate))
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00021F14 File Offset: 0x00020114
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> func;
			bool[] active = this._bindings.GetActive(predicate, out func);
			return new RowToRowMapperTransform.RowCursor(this._host, this._input.GetRowCursor(func, rand), this, active);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00021F4C File Offset: 0x0002014C
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> func;
			bool[] active = this._bindings.GetActive(predicate, out func);
			IRowCursor[] array = this._input.GetRowCursorSet(ref consolidator, func, n, rand);
			if (array.Length == 1 && n > 1 && this._bindings.AnyNewColumnsActive(predicate))
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new RowToRowMapperTransform.RowCursor(this._host, array[i], this, active);
			}
			return array2;
		}

		// Token: 0x04000336 RID: 822
		public const string RegistrationName = "RowToRowMapperTransform";

		// Token: 0x04000337 RID: 823
		public const string LoaderSignature = "RowToRowMapper";

		// Token: 0x04000338 RID: 824
		private readonly IRowMapper _mapper;

		// Token: 0x04000339 RID: 825
		private readonly RowToRowMapperTransform.Bindings _bindings;

		// Token: 0x0400033A RID: 826
		private readonly MetadataDispatcher _md;

		// Token: 0x0200013B RID: 315
		private sealed class Bindings : ColumnBindingsBase
		{
			// Token: 0x06000659 RID: 1625 RVA: 0x00021FEC File Offset: 0x000201EC
			public Bindings(ISchema inputSchema, RowToRowMapperTransform parent)
				: base(inputSchema, true, (from info in Contracts.CheckRef<RowToRowMapperTransform>(parent, "parent")._mapper.GetOutputColumns()
					select info.Name).ToArray<string>())
			{
				this._parent = parent;
				this.OutputColInfos = this._parent._mapper.GetOutputColumns().ToArray<RowMapperColumnInfo>();
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x0002205F File Offset: 0x0002025F
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return this.OutputColInfos[iinfo].ColType;
			}

			// Token: 0x0600065B RID: 1627 RVA: 0x000220A4 File Offset: 0x000202A4
			public bool[] GetActive(Func<int, bool> predicate, out Func<int, bool> predicateInput)
			{
				bool[] active = base.GetActive(predicate);
				bool[] activeInput = base.GetActiveInput(predicate);
				Func<int, bool> activeOutputColumns = this.GetActiveOutputColumns(active);
				Func<int, bool> predicateIn = this._parent._mapper.GetDependencies(activeOutputColumns);
				predicateInput = (int col) => 0 <= col && col < activeInput.Length && (activeInput[col] || predicateIn(col));
				return active;
			}

			// Token: 0x0600065C RID: 1628 RVA: 0x00022130 File Offset: 0x00020330
			public Func<int, bool> GetActiveOutputColumns(bool[] active)
			{
				return (int col) => 0 <= col && col < this.OutputColInfos.Length && active[this.MapIinfoToCol(col)];
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x0002215D File Offset: 0x0002035D
			protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
			{
				return this._parent._md.GetMetadataTypes(iinfo);
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x00022170 File Offset: 0x00020370
			protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
			{
				return this._parent._md.GetMetadataTypeOrNull(kind, iinfo);
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x00022184 File Offset: 0x00020384
			protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
			{
				this._parent._md.GetMetadata<TValue>(this._parent._host, kind, iinfo, ref value);
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x000221A4 File Offset: 0x000203A4
			public bool TryGetInfoIndex(string name, out int iinfo)
			{
				return base.TryGetColumnIndexCore(name, out iinfo);
			}

			// Token: 0x0400033D RID: 829
			private readonly RowToRowMapperTransform _parent;

			// Token: 0x0400033E RID: 830
			public readonly RowMapperColumnInfo[] OutputColInfos;
		}

		// Token: 0x0200013C RID: 316
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000662 RID: 1634 RVA: 0x000221AE File Offset: 0x000203AE
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x000221B8 File Offset: 0x000203B8
			public RowCursor(IChannelProvider provider, IRowCursor input, RowToRowMapperTransform parent, bool[] active)
				: base(provider, input)
			{
				Func<int, bool> activeOutputColumns = parent._bindings.GetActiveOutputColumns(active);
				this._getters = parent._mapper.CreateGetters(input, activeOutputColumns);
				this._active = active;
				this._bindings = parent._bindings;
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00022202 File Offset: 0x00020402
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active[col];
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x0002222C File Offset: 0x0002042C
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				Delegate @delegate = this._getters[num];
				ValueGetter<TValue> valueGetter = @delegate as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x04000340 RID: 832
			private readonly Delegate[] _getters;

			// Token: 0x04000341 RID: 833
			private readonly bool[] _active;

			// Token: 0x04000342 RID: 834
			private readonly RowToRowMapperTransform.Bindings _bindings;
		}
	}
}
