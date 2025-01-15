using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000BD RID: 189
	[Guid("52478F9D-620A-4fc4-98F4-E594BEA9E676")]
	public sealed class ServerPropertyCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000290EC File Offset: 0x000272EC
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x1700021D RID: 541
		public ServerProperty this[int index]
		{
			get
			{
				return (ServerProperty)this.items[index];
			}
		}

		// Token: 0x1700021E RID: 542
		public ServerProperty this[string name]
		{
			get
			{
				int num = this.IndexOf(name);
				if (num == -1)
				{
					throw Utils.CreateItemNotFoundException(name, "Name", typeof(ServerProperty).Name);
				}
				return (ServerProperty)this.items[num];
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00029154 File Offset: 0x00027354
		public int Add(ServerProperty item)
		{
			int count = this.Count;
			this.Insert(count, item);
			return count;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00029174 File Offset: 0x00027374
		public ServerProperty Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ServerProperty serverProperty = new ServerProperty(name, value);
			this.Add(serverProperty);
			return serverProperty;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000291A0 File Offset: 0x000273A0
		public void Clear()
		{
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				this[i].owningCollection = null;
				i++;
			}
			this.items.Clear();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000291DD File Offset: 0x000273DD
		public bool Contains(ServerProperty item)
		{
			return item != null && item.owningCollection == this;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000291ED File Offset: 0x000273ED
		public bool Contains(string name)
		{
			return this.IndexOf(name) != -1;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000291FC File Offset: 0x000273FC
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002920C File Offset: 0x0002740C
		public void CopyTo(ServerPropertyCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			if (col == this)
			{
				return;
			}
			col.Clear();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				col.Insert(i, this[i].Clone());
				i++;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00029260 File Offset: 0x00027460
		public ServerProperty Find(string name)
		{
			int num = this.IndexOf(name);
			if (num != -1)
			{
				return (ServerProperty)this.items[num];
			}
			return null;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002928C File Offset: 0x0002748C
		public int IndexOf(ServerProperty item)
		{
			if (item != null && item.owningCollection == this)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000292A8 File Offset: 0x000274A8
		public int IndexOf(string name)
		{
			name = Utils.Trim(name);
			if (name == null)
			{
				return -1;
			}
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				if (string.Compare(Utils.Trim(((ServerProperty)this.items[i]).Name), name, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00029308 File Offset: 0x00027508
		public void Insert(int index, ServerProperty item)
		{
			if (index < 0 || index > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (item.owningCollection == this)
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(item.Name, typeof(ServerProperty).Name, typeof(ServerPropertyCollection).Name));
			}
			if (item.owningCollection != null)
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInAnotherCollection(typeof(ServerProperty).Name, typeof(ServerPropertyCollection).Name));
			}
			if (item.Name == null)
			{
				throw new ArgumentException(SR.ValueIsRequired("Name"), "item");
			}
			if (this.Contains(item.Name))
			{
				throw new InvalidOperationException(SR.Collections_NameIsNotUnique(item.Name, typeof(ServerProperty).Name));
			}
			this.items.Insert(index, item);
			item.owningCollection = this;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00029418 File Offset: 0x00027618
		public ServerProperty Insert(int index, string name, string value)
		{
			if (index < 0 || index > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ServerProperty serverProperty = new ServerProperty(name, value);
			this.Insert(index, serverProperty);
			return serverProperty;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002946C File Offset: 0x0002766C
		public void Remove(ServerProperty item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_RemoveNullObject(typeof(ServerPropertyCollection).Name));
			}
			int num = this.items.IndexOf(item);
			if (num == -1)
			{
				throw new ArgumentException(SR.Collections_RemoveInexistentObject(typeof(ServerPropertyCollection).Name), "item");
			}
			this.items.RemoveAt(num);
			item.owningCollection = null;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000294E0 File Offset: 0x000276E0
		public void Remove(string name)
		{
			int num = this.IndexOf(name);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00029501 File Offset: 0x00027701
		public void RemoveAt(int index)
		{
			((ServerProperty)this.items[index]).owningCollection = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00029526 File Offset: 0x00027726
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00029529 File Offset: 0x00027729
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000221 RID: 545
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new InvalidOperationException(SR.Collections_CannotSetItems(typeof(ServerPropertyCollection).Name));
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00029558 File Offset: 0x00027758
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			ServerProperty serverProperty = value as ServerProperty;
			if (serverProperty == null)
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			return this.Add(serverProperty);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000295A4 File Offset: 0x000277A4
		bool IList.Contains(object value)
		{
			ServerProperty serverProperty = value as ServerProperty;
			return serverProperty != null && this.Contains(serverProperty);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000295C4 File Offset: 0x000277C4
		int IList.IndexOf(object value)
		{
			ServerProperty serverProperty = value as ServerProperty;
			if (serverProperty != null)
			{
				return this.IndexOf(serverProperty);
			}
			return -1;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000295E4 File Offset: 0x000277E4
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			ServerProperty serverProperty = value as ServerProperty;
			if (serverProperty == null)
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			this.Insert(index, serverProperty);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00029634 File Offset: 0x00027834
		void IList.Remove(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_RemoveNullObject(typeof(ServerPropertyCollection).Name));
			}
			ServerProperty serverProperty = value as ServerProperty;
			if (serverProperty == null)
			{
				throw new ArgumentException(SR.Collections_RemoveObjectOfInvalidType(typeof(ServerPropertyCollection).Name, value.GetType().Name));
			}
			this.Remove(serverProperty);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00029699 File Offset: 0x00027899
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002969C File Offset: 0x0002789C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002969F File Offset: 0x0002789F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400050F RID: 1295
		private ArrayList items = new ArrayList();
	}
}
