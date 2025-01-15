using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E0 RID: 224
	internal class SimplePropertyBag<TKey> : IEnumerable<KeyValuePair<TKey, object>>, IEnumerable
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x000265CF File Offset: 0x000255CF
		private static void InternalAddItemToChangeList(TKey key, List<TKey> changeList)
		{
			if (!changeList.Contains(key))
			{
				changeList.Add(key);
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000265E1 File Offset: 0x000255E1
		private void Changed()
		{
			if (this.OnChange != null)
			{
				this.OnChange();
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000265F8 File Offset: 0x000255F8
		private void InternalRemoveItem(TKey key)
		{
			object obj;
			if (this.TryGetValue(key, out obj))
			{
				this.items.Remove(key);
				this.removedItems.Add(key);
				this.Changed();
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002662F File Offset: 0x0002562F
		internal IEnumerable<TKey> AddedItems
		{
			get
			{
				return this.addedItems;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00026637 File Offset: 0x00025637
		internal IEnumerable<TKey> RemovedItems
		{
			get
			{
				return this.removedItems;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002663F File Offset: 0x0002563F
		internal IEnumerable<TKey> ModifiedItems
		{
			get
			{
				return this.modifiedItems;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002667B File Offset: 0x0002567B
		public void ClearChangeLog()
		{
			this.removedItems.Clear();
			this.addedItems.Clear();
			this.modifiedItems.Clear();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002669E File Offset: 0x0002569E
		public bool ContainsKey(TKey key)
		{
			return this.items.ContainsKey(key);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000266AC File Offset: 0x000256AC
		public bool TryGetValue(TKey key, out object value)
		{
			return this.items.TryGetValue(key, ref value);
		}

		// Token: 0x17000288 RID: 648
		public object this[TKey key]
		{
			get
			{
				object obj;
				if (this.TryGetValue(key, out obj))
				{
					return obj;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.InternalRemoveItem(key);
					return;
				}
				if (this.removedItems.Remove(key))
				{
					SimplePropertyBag<TKey>.InternalAddItemToChangeList(key, this.modifiedItems);
				}
				else if (!this.ContainsKey(key))
				{
					SimplePropertyBag<TKey>.InternalAddItemToChangeList(key, this.addedItems);
				}
				else if (!this.modifiedItems.Contains(key))
				{
					SimplePropertyBag<TKey>.InternalAddItemToChangeList(key, this.modifiedItems);
				}
				this.items[key] = value;
				this.Changed();
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000B86 RID: 2950 RVA: 0x00026750 File Offset: 0x00025750
		// (remove) Token: 0x06000B87 RID: 2951 RVA: 0x00026769 File Offset: 0x00025769
		public event PropertyBagChangedDelegate OnChange;

		// Token: 0x06000B88 RID: 2952 RVA: 0x00026782 File Offset: 0x00025782
		public IEnumerator<KeyValuePair<TKey, object>> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00026794 File Offset: 0x00025794
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400036F RID: 879
		private Dictionary<TKey, object> items = new Dictionary<TKey, object>();

		// Token: 0x04000370 RID: 880
		private List<TKey> removedItems = new List<TKey>();

		// Token: 0x04000371 RID: 881
		private List<TKey> addedItems = new List<TKey>();

		// Token: 0x04000372 RID: 882
		private List<TKey> modifiedItems = new List<TKey>();
	}
}
