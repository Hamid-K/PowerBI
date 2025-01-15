using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000117 RID: 279
	public sealed class MetadataDispatcher : MetadataDispatcherBase
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0001E7E5 File Offset: 0x0001C9E5
		public MetadataDispatcher(int colCount)
			: base(colCount)
		{
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001E7EE File Offset: 0x0001C9EE
		public MetadataDispatcher.Builder BuildMetadata(int index)
		{
			return new MetadataDispatcher.Builder(this, index, null, -1, null);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001E7FA File Offset: 0x0001C9FA
		public MetadataDispatcher.Builder BuildMetadata(int index, ISchema schemaSrc, int indexSrc)
		{
			Contracts.CheckValue<ISchema>(schemaSrc, "schemaSrc");
			return new MetadataDispatcher.Builder(this, index, schemaSrc, indexSrc, null);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001E811 File Offset: 0x0001CA11
		public MetadataDispatcher.Builder BuildMetadata(int index, ISchema schemaSrc, int indexSrc, Func<string, int, bool> filterSrc)
		{
			Contracts.CheckValue<ISchema>(schemaSrc, "schemaSrc");
			return new MetadataDispatcher.Builder(this, index, schemaSrc, indexSrc, filterSrc);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001E840 File Offset: 0x0001CA40
		public MetadataDispatcher.Builder BuildMetadata(int index, ISchema schemaSrc, int indexSrc, string kindSrc)
		{
			Contracts.CheckValue<ISchema>(schemaSrc, "schemaSrc");
			Contracts.CheckNonWhiteSpace(kindSrc, "kindSrc");
			return new MetadataDispatcher.Builder(this, index, schemaSrc, indexSrc, (string k, int i) => k == kindSrc);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		public MetadataDispatcher.Builder BuildMetadata(int index, ISchema schemaSrc, int indexSrc, params string[] kindsSrc)
		{
			Contracts.CheckValue<ISchema>(schemaSrc, "schemaSrc");
			Contracts.CheckParam(Utils.Size<string>(kindsSrc) >= 2, "kindsSrc");
			Contracts.CheckParam(!kindsSrc.Any((string k) => string.IsNullOrWhiteSpace(k)), "kindsSrc");
			HashSet<string> set = new HashSet<string>(kindsSrc);
			return new MetadataDispatcher.Builder(this, index, schemaSrc, indexSrc, (string k, int i) => set.Contains(k));
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001E934 File Offset: 0x0001CB34
		public new void Seal()
		{
			base.Seal();
		}

		// Token: 0x02000118 RID: 280
		public sealed class Builder : IDisposable
		{
			// Token: 0x060005A4 RID: 1444 RVA: 0x0001E93C File Offset: 0x0001CB3C
			internal Builder(MetadataDispatcher md, int index, ISchema schemaSrc = null, int indexSrc = -1, Func<string, int, bool> filterSrc = null)
			{
				Contracts.CheckValue<MetadataDispatcher>(md, "md");
				Contracts.CheckParam(0 <= index && index < md.ColCount, "index");
				this._index = index;
				this._md = md;
				this._info = this._md.CreateInfo(schemaSrc, indexSrc, filterSrc);
				MetadataDispatcherBase.ColInfo colInfoOrNull = this._md.GetColInfoOrNull(this._index);
				Contracts.Check(colInfoOrNull == null, "Duplicate building of metadata");
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
			public void AddGetter<TValue>(string kind, ColumnType type, MetadataUtils.MetadataGetter<TValue> getter)
			{
				Contracts.Check(this._md != null, "Builder disposed");
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckValue<ColumnType>(type, "type");
				Contracts.CheckValue<MetadataUtils.MetadataGetter<TValue>>(getter, "getter");
				Contracts.CheckParam(type.RawType == typeof(TValue), "type", "Given type doesn't match type parameter");
				if (this._getters != null && this._getters.Any((MetadataDispatcherBase.GetterInfo g) => g.Kind == kind))
				{
					throw Contracts.Except("Duplicate specification of metadata");
				}
				Utils.Add<MetadataDispatcherBase.GetterInfo>(ref this._getters, new MetadataDispatcherBase.GetterInfoDelegate<TValue>(kind, type, getter));
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
			public void AddPrimitive<TValue>(string kind, ColumnType type, TValue value)
			{
				Contracts.Check(this._md != null, "Builder disposed");
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckValue<ColumnType>(type, "type");
				Contracts.CheckParam(type.RawType == typeof(TValue), "type", "Given type doesn't match type parameter");
				Contracts.CheckParam(type.IsPrimitive, "type", "Must be a primitive type");
				if (this._getters != null && this._getters.Any((MetadataDispatcherBase.GetterInfo g) => g.Kind == kind))
				{
					throw Contracts.Except("Duplicate specification of metadata");
				}
				Utils.Add<MetadataDispatcherBase.GetterInfo>(ref this._getters, new MetadataDispatcherBase.GetterInfoPrimitive<TValue>(kind, type, value));
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x0001EB7C File Offset: 0x0001CD7C
			public void Dispose()
			{
				if (this._md == null)
				{
					return;
				}
				MetadataDispatcher md = this._md;
				this._md = null;
				MetadataDispatcherBase.ColInfo colInfo = this._info;
				this._info = null;
				List<MetadataDispatcherBase.GetterInfo> getters = this._getters;
				this._getters = null;
				if (Utils.Size<MetadataDispatcherBase.GetterInfo>(getters) > 0)
				{
					colInfo = colInfo.UpdateGetters(getters);
				}
				md.RegisterColumn(this._index, colInfo);
			}

			// Token: 0x040002D0 RID: 720
			private readonly int _index;

			// Token: 0x040002D1 RID: 721
			private MetadataDispatcher _md;

			// Token: 0x040002D2 RID: 722
			private MetadataDispatcherBase.ColInfo _info;

			// Token: 0x040002D3 RID: 723
			private List<MetadataDispatcherBase.GetterInfo> _getters;
		}
	}
}
