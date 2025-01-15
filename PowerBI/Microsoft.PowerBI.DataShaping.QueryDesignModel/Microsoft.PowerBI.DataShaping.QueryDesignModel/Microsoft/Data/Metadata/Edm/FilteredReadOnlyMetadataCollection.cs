using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000094 RID: 148
	internal class FilteredReadOnlyMetadataCollection<TDerived, TBase> : ReadOnlyMetadataCollection<TDerived>, IBaseList<TBase>, IList, ICollection, IEnumerable where TDerived : TBase where TBase : MetadataItem
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x00018DAA File Offset: 0x00016FAA
		internal FilteredReadOnlyMetadataCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate)
			: base(FilteredReadOnlyMetadataCollection<TDerived, TBase>.FilterCollection(collection, predicate))
		{
			this._source = collection;
			this._predicate = predicate;
		}

		// Token: 0x170003CD RID: 973
		public override TDerived this[string identity]
		{
			get
			{
				TBase tbase = this._source[identity];
				if (this._predicate(tbase))
				{
					return (TDerived)((object)tbase);
				}
				throw EntityUtil.ItemInvalidIdentity(identity, "identity");
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00018E08 File Offset: 0x00017008
		public override TDerived GetValue(string identity, bool ignoreCase)
		{
			TBase value = this._source.GetValue(identity, ignoreCase);
			if (this._predicate(value))
			{
				return (TDerived)((object)value);
			}
			throw EntityUtil.ItemInvalidIdentity(identity, "identity");
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00018E48 File Offset: 0x00017048
		public override bool Contains(string identity)
		{
			TBase tbase;
			return this._source.TryGetValue(identity, false, out tbase) && this._predicate(tbase);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00018E74 File Offset: 0x00017074
		public override bool TryGetValue(string identity, bool ignoreCase, out TDerived item)
		{
			item = default(TDerived);
			TBase tbase;
			if (this._source.TryGetValue(identity, ignoreCase, out tbase) && this._predicate(tbase))
			{
				item = (TDerived)((object)tbase);
				return true;
			}
			return false;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00018EBC File Offset: 0x000170BC
		internal static List<TDerived> FilterCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate)
		{
			List<TDerived> list = new List<TDerived>(collection.Count);
			foreach (TBase tbase in collection)
			{
				if (predicate(tbase))
				{
					list.Add((TDerived)((object)tbase));
				}
			}
			return list;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00018F2C File Offset: 0x0001712C
		public override int IndexOf(TDerived value)
		{
			TBase tbase;
			if (this._source.TryGetValue(value.Identity, false, out tbase) && this._predicate(tbase))
			{
				return base.IndexOf((TDerived)((object)tbase));
			}
			return -1;
		}

		// Token: 0x170003CE RID: 974
		TBase IBaseList<TBase>.this[string identity]
		{
			get
			{
				return (TBase)((object)this[identity]);
			}
		}

		// Token: 0x170003CF RID: 975
		TBase IBaseList<TBase>.this[int index]
		{
			get
			{
				return (TBase)((object)base[index]);
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00018F9B File Offset: 0x0001719B
		int IBaseList<TBase>.IndexOf(TBase item)
		{
			if (this._predicate(item))
			{
				return this.IndexOf((TDerived)((object)item));
			}
			return -1;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00018FBE File Offset: 0x000171BE
		bool IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x04000849 RID: 2121
		private readonly ReadOnlyMetadataCollection<TBase> _source;

		// Token: 0x0400084A RID: 2122
		private readonly Predicate<TBase> _predicate;
	}
}
