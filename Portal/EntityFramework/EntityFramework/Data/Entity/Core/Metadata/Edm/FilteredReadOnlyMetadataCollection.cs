using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C5 RID: 1221
	internal class FilteredReadOnlyMetadataCollection<TDerived, TBase> : ReadOnlyMetadataCollection<TDerived>, IBaseList<TBase>, IList, ICollection, IEnumerable where TDerived : TBase where TBase : MetadataItem
	{
		// Token: 0x06003C4D RID: 15437 RVA: 0x000C8168 File Offset: 0x000C6368
		internal FilteredReadOnlyMetadataCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate)
			: base(FilteredReadOnlyMetadataCollection<TDerived, TBase>.FilterCollection(collection, predicate))
		{
			this._source = collection;
			this._predicate = predicate;
		}

		// Token: 0x17000BDE RID: 3038
		public override TDerived this[string identity]
		{
			get
			{
				TBase tbase = this._source[identity];
				if (this._predicate(tbase))
				{
					return (TDerived)((object)tbase);
				}
				throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
			}
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x000C81CC File Offset: 0x000C63CC
		public override TDerived GetValue(string identity, bool ignoreCase)
		{
			TBase value = this._source.GetValue(identity, ignoreCase);
			if (this._predicate(value))
			{
				return (TDerived)((object)value);
			}
			throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x000C8214 File Offset: 0x000C6414
		public override bool Contains(string identity)
		{
			TBase tbase;
			return this._source.TryGetValue(identity, false, out tbase) && this._predicate(tbase);
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x000C8240 File Offset: 0x000C6440
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

		// Token: 0x06003C52 RID: 15442 RVA: 0x000C8288 File Offset: 0x000C6488
		internal static List<TDerived> FilterCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate)
		{
			List<TDerived> list = new List<TDerived>(collection.Count);
			for (int i = 0; i < collection.Count; i++)
			{
				TBase tbase = collection[i];
				if (predicate(tbase))
				{
					list.Add((TDerived)((object)tbase));
				}
			}
			return list;
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000C82D8 File Offset: 0x000C64D8
		public override int IndexOf(TDerived value)
		{
			TBase tbase;
			if (this._source.TryGetValue(value.Identity, false, out tbase) && this._predicate(tbase))
			{
				return base.IndexOf((TDerived)((object)tbase));
			}
			return -1;
		}

		// Token: 0x17000BDF RID: 3039
		TBase IBaseList<TBase>.this[string identity]
		{
			get
			{
				return (TBase)((object)this[identity]);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		TBase IBaseList<TBase>.this[int index]
		{
			get
			{
				return (TBase)((object)base[index]);
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x000C8347 File Offset: 0x000C6547
		int IBaseList<TBase>.IndexOf(TBase item)
		{
			if (this._predicate(item))
			{
				return this.IndexOf((TDerived)((object)item));
			}
			return -1;
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x000C836A File Offset: 0x000C656A
		bool IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x040014C8 RID: 5320
		private readonly ReadOnlyMetadataCollection<TBase> _source;

		// Token: 0x040014C9 RID: 5321
		private readonly Predicate<TBase> _predicate;
	}
}
