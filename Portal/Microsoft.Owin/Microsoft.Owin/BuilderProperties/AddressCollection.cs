using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin.BuilderProperties
{
	// Token: 0x02000047 RID: 71
	public struct AddressCollection : IEnumerable<Address>, IEnumerable
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000727E File Offset: 0x0000547E
		public AddressCollection(IList<IDictionary<string, object>> list)
		{
			this._list = list;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007287 File Offset: 0x00005487
		public IList<IDictionary<string, object>> List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000728F File Offset: 0x0000548F
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170000BC RID: 188
		public Address this[int index]
		{
			get
			{
				return new Address(this._list[index]);
			}
			set
			{
				this._list[index] = value.Dictionary;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000072C4 File Offset: 0x000054C4
		public void Add(Address address)
		{
			this._list.Add(address.Dictionary);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000072D8 File Offset: 0x000054D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Address>)this).GetEnumerator();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000072EA File Offset: 0x000054EA
		public IEnumerator<Address> GetEnumerator()
		{
			foreach (IDictionary<string, object> entry in this.List)
			{
				yield return new Address(entry);
			}
			IEnumerator<IDictionary<string, object>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000072FE File Offset: 0x000054FE
		public static AddressCollection Create()
		{
			return new AddressCollection(new List<IDictionary<string, object>>());
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000730A File Offset: 0x0000550A
		public bool Equals(AddressCollection other)
		{
			return object.Equals(this._list, other._list);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000731D File Offset: 0x0000551D
		public override bool Equals(object obj)
		{
			return obj is AddressCollection && this.Equals((AddressCollection)obj);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007335 File Offset: 0x00005535
		public override int GetHashCode()
		{
			if (this._list == null)
			{
				return 0;
			}
			return this._list.GetHashCode();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000734C File Offset: 0x0000554C
		public static bool operator ==(AddressCollection left, AddressCollection right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007356 File Offset: 0x00005556
		public static bool operator !=(AddressCollection left, AddressCollection right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400007F RID: 127
		private readonly IList<IDictionary<string, object>> _list;
	}
}
