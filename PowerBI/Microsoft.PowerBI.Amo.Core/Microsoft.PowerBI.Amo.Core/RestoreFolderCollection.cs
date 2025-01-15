using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B1 RID: 177
	[Guid("2B6F1632-9B68-4f78-B56E-1E359681ABEF")]
	public sealed class RestoreFolderCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00027F5D File Offset: 0x0002615D
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170001EB RID: 491
		public RestoreFolder this[int index]
		{
			get
			{
				return (RestoreFolder)this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				this.items[index] = value;
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00027F9F File Offset: 0x0002619F
		public int Add(RestoreFolder item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.items.Contains(item))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			return this.items.Add(item);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00027FE0 File Offset: 0x000261E0
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				RestoreFolder restoreFolder = (RestoreFolder)obj;
				this.Add(restoreFolder);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00028044 File Offset: 0x00026244
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00028051 File Offset: 0x00026251
		public bool Contains(RestoreFolder item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00028064 File Offset: 0x00026264
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00028073 File Offset: 0x00026273
		public int IndexOf(RestoreFolder item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00028086 File Offset: 0x00026286
		public void Insert(int index, RestoreFolder item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.items.Contains(item))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			this.items.Insert(index, item);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000280C6 File Offset: 0x000262C6
		public void Remove(RestoreFolder item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000280D8 File Offset: 0x000262D8
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000280E6 File Offset: 0x000262E6
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000280E9 File Offset: 0x000262E9
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001EE RID: 494
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				if (!(value is RestoreFolder))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002814C File Offset: 0x0002634C
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is RestoreFolder))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000281B9 File Offset: 0x000263B9
		bool IList.Contains(object value)
		{
			return value != null && value is RestoreFolder && this.items.Contains(value);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000281D4 File Offset: 0x000263D4
		int IList.IndexOf(object value)
		{
			if (value != null && value is RestoreFolder)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000281F0 File Offset: 0x000263F0
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is RestoreFolder))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002825E File Offset: 0x0002645E
		void IList.Remove(object value)
		{
			if (value == null || !(value is RestoreFolder))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00028278 File Offset: 0x00026478
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0002827B File Offset: 0x0002647B
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002827E File Offset: 0x0002647E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040004D2 RID: 1234
		private ArrayList items = new ArrayList();
	}
}
