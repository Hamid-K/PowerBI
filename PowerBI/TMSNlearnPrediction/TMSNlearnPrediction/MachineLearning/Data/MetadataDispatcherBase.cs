using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000111 RID: 273
	public abstract class MetadataDispatcherBase
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001DE7E File Offset: 0x0001C07E
		protected int ColCount
		{
			get
			{
				return this._infos.Length;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001DE88 File Offset: 0x0001C088
		protected MetadataDispatcherBase(int colCount)
		{
			Contracts.CheckParam(colCount >= 0, "colCount");
			this._infos = new MetadataDispatcherBase.ColInfo[colCount];
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		protected MetadataDispatcherBase.ColInfo CreateInfo(ISchema schemaSrc = null, int indexSrc = -1, Func<string, int, bool> filterSrc = null)
		{
			Contracts.Check(!this._sealed, "MetadataDispatcher sealed");
			Contracts.Check(schemaSrc == null || (0 <= indexSrc && indexSrc < schemaSrc.ColumnCount), "indexSrc out of range");
			Contracts.Check(filterSrc == null || schemaSrc != null, "filterSrc should be null if schemaSrc is null");
			return new MetadataDispatcherBase.ColInfo(schemaSrc, indexSrc, filterSrc, null);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001DF10 File Offset: 0x0001C110
		protected void RegisterColumn(int index, MetadataDispatcherBase.ColInfo info)
		{
			Contracts.Check(!this._sealed, "MetadataDispatcher sealed");
			Contracts.CheckValue<MetadataDispatcherBase.ColInfo>(info, "info");
			Contracts.CheckParam(0 <= index && index < this._infos.Length, "info.Index");
			Contracts.Check(this._infos[index] == null, "Column already registered");
			this._infos[index] = info;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001DF75 File Offset: 0x0001C175
		protected void Seal()
		{
			this._sealed = true;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001DF7E File Offset: 0x0001C17E
		protected MetadataDispatcherBase.ColInfo GetColInfoOrNull(int index)
		{
			Contracts.CheckParam(0 <= index && index < this._infos.Length, "col");
			return this._infos[index];
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int index)
		{
			Contracts.Check(this._sealed, "MetadataDispatcher not sealed");
			MetadataDispatcherBase.ColInfo colInfoOrNull = this.GetColInfoOrNull(index);
			if (colInfoOrNull == null)
			{
				return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
			}
			return this.GetTypesCore(index, colInfoOrNull);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001E324 File Offset: 0x0001C524
		private IEnumerable<KeyValuePair<string, ColumnType>> GetTypesCore(int index, MetadataDispatcherBase.ColInfo info)
		{
			HashSet<string> kinds = null;
			if (info.GetterCount > 0)
			{
				if (info.SchemaSrc != null)
				{
					kinds = new HashSet<string>();
				}
				foreach (MetadataDispatcherBase.GetterInfo g in info.Getters)
				{
					yield return new KeyValuePair<string, ColumnType>(g.Kind, g.Type);
					if (kinds != null)
					{
						kinds.Add(g.Kind);
					}
				}
			}
			if (info.SchemaSrc != null)
			{
				foreach (KeyValuePair<string, ColumnType> kvp in info.SchemaSrc.GetMetadataTypes(info.IndexSrc))
				{
					if (kinds != null)
					{
						HashSet<string> hashSet = kinds;
						KeyValuePair<string, ColumnType> keyValuePair = kvp;
						if (hashSet.Contains(keyValuePair.Key))
						{
							continue;
						}
					}
					if (info.FilterSrc != null)
					{
						Func<string, int, bool> filterSrc = info.FilterSrc;
						KeyValuePair<string, ColumnType> keyValuePair2 = kvp;
						if (!filterSrc(keyValuePair2.Key, index))
						{
							continue;
						}
					}
					yield return kvp;
				}
			}
			yield break;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001E350 File Offset: 0x0001C550
		public ColumnType GetMetadataTypeOrNull(string kind, int index)
		{
			Contracts.Check(this._sealed, "MetadataDispatcher not sealed");
			MetadataDispatcherBase.ColInfo colInfoOrNull = this.GetColInfoOrNull(index);
			if (colInfoOrNull == null)
			{
				return null;
			}
			foreach (MetadataDispatcherBase.GetterInfo getterInfo in colInfoOrNull.Getters)
			{
				if (getterInfo.Kind == kind)
				{
					return getterInfo.Type;
				}
			}
			if (colInfoOrNull.SchemaSrc == null)
			{
				return null;
			}
			if (colInfoOrNull.FilterSrc != null && !colInfoOrNull.FilterSrc(kind, index))
			{
				return null;
			}
			return colInfoOrNull.SchemaSrc.GetMetadataTypeOrNull(kind, colInfoOrNull.IndexSrc);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001E404 File Offset: 0x0001C604
		public void GetMetadata<TValue>(IExceptionContext ectx, string kind, int index, ref TValue value)
		{
			Contracts.Check(ectx, this._sealed, "MetadataDispatcher not sealed");
			Contracts.Check(ectx, 0 <= index && index < this._infos.Length);
			MetadataDispatcherBase.ColInfo colInfo = this._infos[index];
			if (colInfo == null)
			{
				throw MetadataUtils.ExceptGetMetadata(ectx);
			}
			foreach (MetadataDispatcherBase.GetterInfo getterInfo in colInfo.Getters)
			{
				if (getterInfo.Kind == kind)
				{
					MetadataDispatcherBase.GetterInfo<TValue> getterInfo2 = getterInfo as MetadataDispatcherBase.GetterInfo<TValue>;
					if (getterInfo2 == null)
					{
						throw MetadataUtils.ExceptGetMetadata(ectx);
					}
					getterInfo2.Get(index, ref value);
					return;
				}
			}
			if (colInfo.SchemaSrc == null || (colInfo.FilterSrc != null && !colInfo.FilterSrc(kind, index)))
			{
				throw MetadataUtils.ExceptGetMetadata(ectx);
			}
			colInfo.SchemaSrc.GetMetadata<TValue>(kind, colInfo.IndexSrc, ref value);
		}

		// Token: 0x040002C4 RID: 708
		private bool _sealed;

		// Token: 0x040002C5 RID: 709
		private readonly MetadataDispatcherBase.ColInfo[] _infos;

		// Token: 0x02000112 RID: 274
		protected sealed class ColInfo
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001E4EC File Offset: 0x0001C6EC
			public int GetterCount
			{
				get
				{
					return this._getters.Length;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001E670 File Offset: 0x0001C870
			public IEnumerable<MetadataDispatcherBase.GetterInfo> Getters
			{
				get
				{
					foreach (MetadataDispatcherBase.GetterInfo g in this._getters)
					{
						yield return g;
					}
					yield break;
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0001E68D File Offset: 0x0001C88D
			public ColInfo(ISchema schemaSrc, int indexSrc, Func<string, int, bool> filterSrc, IEnumerable<MetadataDispatcherBase.GetterInfo> getters = null)
			{
				this.SchemaSrc = schemaSrc;
				this.IndexSrc = indexSrc;
				this.FilterSrc = filterSrc;
				this._getters = ((getters != null) ? getters.ToArray<MetadataDispatcherBase.GetterInfo>() : new MetadataDispatcherBase.GetterInfo[0]);
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0001E6CC File Offset: 0x0001C8CC
			public MetadataDispatcherBase.ColInfo UpdateGetters(IEnumerable<MetadataDispatcherBase.GetterInfo> getters)
			{
				if (getters == null)
				{
					return this;
				}
				Contracts.Check(!getters.Any((MetadataDispatcherBase.GetterInfo g) => g == null), "Invalid getter info");
				return new MetadataDispatcherBase.ColInfo(this.SchemaSrc, this.IndexSrc, this.FilterSrc, getters);
			}

			// Token: 0x040002C6 RID: 710
			public readonly ISchema SchemaSrc;

			// Token: 0x040002C7 RID: 711
			public readonly int IndexSrc;

			// Token: 0x040002C8 RID: 712
			public readonly Func<string, int, bool> FilterSrc;

			// Token: 0x040002C9 RID: 713
			private readonly MetadataDispatcherBase.GetterInfo[] _getters;
		}

		// Token: 0x02000113 RID: 275
		protected abstract class GetterInfo
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x0001E726 File Offset: 0x0001C926
			protected GetterInfo(string kind, ColumnType type)
			{
				Contracts.CheckNonWhiteSpace(kind, "Invalid metadata kind");
				Contracts.CheckValue<ColumnType>(type, "type");
				this.Kind = kind;
				this.Type = type;
			}

			// Token: 0x040002CB RID: 715
			public readonly string Kind;

			// Token: 0x040002CC RID: 716
			public readonly ColumnType Type;
		}

		// Token: 0x02000114 RID: 276
		protected abstract class GetterInfo<TValue> : MetadataDispatcherBase.GetterInfo
		{
			// Token: 0x06000596 RID: 1430 RVA: 0x0001E753 File Offset: 0x0001C953
			protected GetterInfo(string kind, ColumnType type)
				: base(kind, type)
			{
			}

			// Token: 0x06000597 RID: 1431
			public abstract void Get(int index, ref TValue value);
		}

		// Token: 0x02000115 RID: 277
		protected sealed class GetterInfoDelegate<TValue> : MetadataDispatcherBase.GetterInfo<TValue>
		{
			// Token: 0x06000598 RID: 1432 RVA: 0x0001E75D File Offset: 0x0001C95D
			public GetterInfoDelegate(string kind, ColumnType type, MetadataUtils.MetadataGetter<TValue> getter)
				: base(kind, type)
			{
				Contracts.Check(type.RawType == typeof(TValue), "Incompatible types");
				Contracts.CheckValue<MetadataUtils.MetadataGetter<TValue>>(getter, "getter");
				this.Getter = getter;
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x0001E798 File Offset: 0x0001C998
			public override void Get(int index, ref TValue value)
			{
				this.Getter.Invoke(index, ref value);
			}

			// Token: 0x040002CD RID: 717
			public readonly MetadataUtils.MetadataGetter<TValue> Getter;
		}

		// Token: 0x02000116 RID: 278
		protected sealed class GetterInfoPrimitive<TValue> : MetadataDispatcherBase.GetterInfo<TValue>
		{
			// Token: 0x0600059A RID: 1434 RVA: 0x0001E7A7 File Offset: 0x0001C9A7
			public GetterInfoPrimitive(string kind, ColumnType type, TValue value)
				: base(kind, type)
			{
				Contracts.Check(type.RawType == typeof(TValue), "Incompatible types");
				this.Value = value;
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0001E7D7 File Offset: 0x0001C9D7
			public override void Get(int index, ref TValue value)
			{
				value = this.Value;
			}

			// Token: 0x040002CE RID: 718
			public readonly TValue Value;
		}
	}
}
