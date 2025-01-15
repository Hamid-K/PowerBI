using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003FD RID: 1021
	internal sealed class NamedNodeMap : INamedNodeMap, IEnumerable<IAttr>, IEnumerable
	{
		// Token: 0x06002067 RID: 8295 RVA: 0x00056405 File Offset: 0x00054605
		public NamedNodeMap(Element owner)
		{
			this._items = new List<Attr>();
			this._owner = new WeakReference<Element>(owner);
		}

		// Token: 0x17000A33 RID: 2611
		public IAttr this[string name]
		{
			get
			{
				return this.GetNamedItem(name);
			}
		}

		// Token: 0x17000A34 RID: 2612
		public IAttr this[int index]
		{
			get
			{
				if (index < 0 || index >= this._items.Count)
				{
					return null;
				}
				return this._items[index];
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0005644F File Offset: 0x0005464F
		public int Length
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0005645C File Offset: 0x0005465C
		internal void FastAddItem(Attr attr)
		{
			this._items.Add(attr);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0005646C File Offset: 0x0005466C
		internal void RaiseChangedEvent(Attr attr, string newValue, string oldValue)
		{
			Element element = null;
			if (this._owner.TryGetTarget(out element))
			{
				element.AttributeChanged(attr.LocalName, attr.NamespaceUri, oldValue, newValue);
			}
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000564A0 File Offset: 0x000546A0
		internal IAttr RemoveNamedItemOrDefault(string name, bool suppressMutationObservers)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (name.Is(this._items[i].Name))
				{
					Attr attr = this._items[i];
					this._items.RemoveAt(i);
					attr.Container = null;
					if (!suppressMutationObservers)
					{
						this.RaiseChangedEvent(attr, null, attr.Value);
					}
					return attr;
				}
			}
			return null;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00056510 File Offset: 0x00054710
		internal IAttr RemoveNamedItemOrDefault(string name)
		{
			return this.RemoveNamedItemOrDefault(name, false);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0005651C File Offset: 0x0005471C
		internal IAttr RemoveNamedItemOrDefault(string namespaceUri, string localName, bool suppressMutationObservers)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (localName.Is(this._items[i].LocalName) && namespaceUri.Is(this._items[i].NamespaceUri))
				{
					Attr attr = this._items[i];
					this._items.RemoveAt(i);
					attr.Container = null;
					if (!suppressMutationObservers)
					{
						this.RaiseChangedEvent(attr, null, attr.Value);
					}
					return attr;
				}
			}
			return null;
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000565A5 File Offset: 0x000547A5
		internal IAttr RemoveNamedItemOrDefault(string namespaceUri, string localName)
		{
			return this.RemoveNamedItemOrDefault(namespaceUri, localName, false);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000565B0 File Offset: 0x000547B0
		public IAttr GetNamedItem(string name)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (name.Is(this._items[i].Name))
				{
					return this._items[i];
				}
			}
			return null;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000565FC File Offset: 0x000547FC
		public IAttr GetNamedItem(string namespaceUri, string localName)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (localName.Is(this._items[i].LocalName) && namespaceUri.Is(this._items[i].NamespaceUri))
				{
					return this._items[i];
				}
			}
			return null;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00056660 File Offset: 0x00054860
		public IAttr SetNamedItem(IAttr item)
		{
			Attr attr = this.Prepare(item);
			if (attr != null)
			{
				string name = item.Name;
				for (int i = 0; i < this._items.Count; i++)
				{
					if (name.Is(this._items[i].Name))
					{
						Attr attr2 = this._items[i];
						this._items[i] = attr;
						this.RaiseChangedEvent(attr, attr.Value, attr2.Value);
						return attr2;
					}
				}
				this._items.Add(attr);
				this.RaiseChangedEvent(attr, attr.Value, null);
			}
			return null;
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000566F8 File Offset: 0x000548F8
		public IAttr SetNamedItemWithNamespaceUri(IAttr item, bool suppressMutationObservers)
		{
			Attr attr = this.Prepare(item);
			if (attr != null)
			{
				string localName = item.LocalName;
				string namespaceUri = item.NamespaceUri;
				for (int i = 0; i < this._items.Count; i++)
				{
					if (localName.Is(this._items[i].LocalName) && namespaceUri.Is(this._items[i].NamespaceUri))
					{
						Attr attr2 = this._items[i];
						this._items[i] = attr;
						if (!suppressMutationObservers)
						{
							this.RaiseChangedEvent(attr, attr.Value, attr2.Value);
						}
						return attr2;
					}
				}
				this._items.Add(attr);
				if (!suppressMutationObservers)
				{
					this.RaiseChangedEvent(attr, attr.Value, null);
				}
			}
			return null;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000567BC File Offset: 0x000549BC
		public IAttr SetNamedItemWithNamespaceUri(IAttr item)
		{
			return this.SetNamedItemWithNamespaceUri(item, false);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000567C6 File Offset: 0x000549C6
		public IAttr RemoveNamedItem(string name)
		{
			IAttr attr = this.RemoveNamedItemOrDefault(name);
			if (attr == null)
			{
				throw new DomException(DomError.NotFound);
			}
			return attr;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000567D9 File Offset: 0x000549D9
		public IAttr RemoveNamedItem(string namespaceUri, string localName)
		{
			IAttr attr = this.RemoveNamedItemOrDefault(namespaceUri, localName);
			if (attr == null)
			{
				throw new DomException(DomError.NotFound);
			}
			return attr;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000567ED File Offset: 0x000549ED
		public IEnumerator<IAttr> GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000567ED File Offset: 0x000549ED
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00056800 File Offset: 0x00054A00
		private Attr Prepare(IAttr item)
		{
			Attr attr = item as Attr;
			if (attr != null)
			{
				if (attr.Container == this)
				{
					return null;
				}
				if (attr.Container != null)
				{
					throw new DomException(DomError.InUse);
				}
				attr.Container = this;
			}
			return attr;
		}

		// Token: 0x04000D12 RID: 3346
		private readonly List<Attr> _items;

		// Token: 0x04000D13 RID: 3347
		private readonly WeakReference<Element> _owner;
	}
}
