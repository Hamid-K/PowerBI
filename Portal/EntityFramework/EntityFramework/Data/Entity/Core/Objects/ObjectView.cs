using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000425 RID: 1061
	internal class ObjectView<TElement> : IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IObjectView
	{
		// Token: 0x0600337D RID: 13181 RVA: 0x000A64E9 File Offset: 0x000A46E9
		internal ObjectView(IObjectViewData<TElement> viewData, object eventDataSource)
		{
			this._viewData = viewData;
			this._listener = new ObjectViewListener(this, (IList)this._viewData.List, eventDataSource);
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000A651C File Offset: 0x000A471C
		private void EnsureWritableList()
		{
			if (((IList)this).IsReadOnly)
			{
				throw new InvalidOperationException(Strings.ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000A6531 File Offset: 0x000A4731
		private static bool IsElementTypeAbstract
		{
			get
			{
				return typeof(TElement).IsAbstract();
			}
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000A6544 File Offset: 0x000A4744
		void ICancelAddNew.CancelNew(int itemIndex)
		{
			if (this._addNewIndex >= 0 && itemIndex == this._addNewIndex)
			{
				TElement telement = this._viewData.List[this._addNewIndex];
				this._listener.UnregisterEntityEvents(telement);
				int addNewIndex = this._addNewIndex;
				this._addNewIndex = -1;
				try
				{
					this._suspendEvent = true;
					this._viewData.Remove(telement, true);
				}
				finally
				{
					this._suspendEvent = false;
				}
				this.OnListChanged(ListChangedType.ItemDeleted, addNewIndex, -1);
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000A65D4 File Offset: 0x000A47D4
		void ICancelAddNew.EndNew(int itemIndex)
		{
			if (this._addNewIndex >= 0 && itemIndex == this._addNewIndex)
			{
				this._viewData.CommitItemAt(this._addNewIndex);
				this._addNewIndex = -1;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000A6600 File Offset: 0x000A4800
		bool IBindingList.AllowNew
		{
			get
			{
				return this._viewData.AllowNew && !ObjectView<TElement>.IsElementTypeAbstract;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000A6619 File Offset: 0x000A4819
		bool IBindingList.AllowEdit
		{
			get
			{
				return this._viewData.AllowEdit;
			}
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000A6628 File Offset: 0x000A4828
		object IBindingList.AddNew()
		{
			this.EnsureWritableList();
			if (ObjectView<TElement>.IsElementTypeAbstract)
			{
				throw new InvalidOperationException(Strings.ObjectView_AddNewOperationNotAllowedOnAbstractBindingList);
			}
			this._viewData.EnsureCanAddNew();
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			TElement telement = (TElement)((object)Activator.CreateInstance(typeof(TElement)));
			this._addNewIndex = this._viewData.Add(telement, true);
			this._listener.RegisterEntityEvents(telement);
			this.OnListChanged(ListChangedType.ItemAdded, this._addNewIndex, -1);
			return telement;
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000A66B1 File Offset: 0x000A48B1
		bool IBindingList.AllowRemove
		{
			get
			{
				return this._viewData.AllowRemove;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000A66BE File Offset: 0x000A48BE
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000A66C1 File Offset: 0x000A48C1
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000A66C4 File Offset: 0x000A48C4
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000A66C7 File Offset: 0x000A48C7
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000A66CA File Offset: 0x000A48CA
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000A66D1 File Offset: 0x000A48D1
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600338C RID: 13196 RVA: 0x000A66D8 File Offset: 0x000A48D8
		// (remove) Token: 0x0600338D RID: 13197 RVA: 0x000A66F1 File Offset: 0x000A48F1
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Combine(this.onListChanged, value);
			}
			remove
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Remove(this.onListChanged, value);
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000A670A File Offset: 0x000A490A
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000A6711 File Offset: 0x000A4911
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000A6718 File Offset: 0x000A4918
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000A671F File Offset: 0x000A491F
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000A6726 File Offset: 0x000A4926
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170009EC RID: 2540
		public TElement this[int index]
		{
			get
			{
				return this._viewData.List[index];
			}
			set
			{
				throw new InvalidOperationException(Strings.ObjectView_CannotReplacetheEntityorRow);
			}
		}

		// Token: 0x170009ED RID: 2541
		object IList.this[int index]
		{
			get
			{
				return this._viewData.List[index];
			}
			set
			{
				throw new InvalidOperationException(Strings.ObjectView_CannotReplacetheEntityorRow);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000A6770 File Offset: 0x000A4970
		bool IList.IsReadOnly
		{
			get
			{
				return !this._viewData.AllowNew && !this._viewData.AllowRemove;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000A678F File Offset: 0x000A498F
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000A6794 File Offset: 0x000A4994
		int IList.Add(object value)
		{
			Check.NotNull<object>(value, "value");
			this.EnsureWritableList();
			if (!(value is TElement))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
			}
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			int num = ((IList)this).IndexOf(value);
			if (num == -1)
			{
				num = this._viewData.Add((TElement)((object)value), false);
				if (!this._viewData.FiresEventOnAdd)
				{
					this._listener.RegisterEntityEvents(value);
					this.OnListChanged(ListChangedType.ItemAdded, num, -1);
				}
			}
			return num;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000A6814 File Offset: 0x000A4A14
		void IList.Clear()
		{
			this.EnsureWritableList();
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			if (this._viewData.FiresEventOnClear)
			{
				this._viewData.Clear();
				return;
			}
			try
			{
				this._suspendEvent = true;
				this._viewData.Clear();
			}
			finally
			{
				this._suspendEvent = false;
			}
			this.OnListChanged(ListChangedType.Reset, -1, -1);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000A6884 File Offset: 0x000A4A84
		bool IList.Contains(object value)
		{
			return value is TElement && this._viewData.List.Contains((TElement)((object)value));
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000A68B8 File Offset: 0x000A4AB8
		int IList.IndexOf(object value)
		{
			int num;
			if (value is TElement)
			{
				num = this._viewData.List.IndexOf((TElement)((object)value));
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000A68E9 File Offset: 0x000A4AE9
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(Strings.ObjectView_IndexBasedInsertIsNotSupported);
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000A68F8 File Offset: 0x000A4AF8
		void IList.Remove(object value)
		{
			Check.NotNull<object>(value, "value");
			this.EnsureWritableList();
			if (!(value is TElement))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
			}
			((ICancelAddNew)this).EndNew(this._addNewIndex);
			TElement telement = (TElement)((object)value);
			int num = this._viewData.List.IndexOf(telement);
			if (this._viewData.Remove(telement, false) && !this._viewData.FiresEventOnRemove)
			{
				this._listener.UnregisterEntityEvents(telement);
				this.OnListChanged(ListChangedType.ItemDeleted, num, -1);
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000A6985 File Offset: 0x000A4B85
		void IList.RemoveAt(int index)
		{
			((IList)this).Remove(((IList)this)[index]);
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000A6994 File Offset: 0x000A4B94
		public int Count
		{
			get
			{
				return this._viewData.List.Count;
			}
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000A69A6 File Offset: 0x000A4BA6
		public void CopyTo(Array array, int index)
		{
			((IList)this._viewData.List).CopyTo(array, index);
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000A69BF File Offset: 0x000A4BBF
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000A69C2 File Offset: 0x000A4BC2
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000A69C5 File Offset: 0x000A4BC5
		public IEnumerator GetEnumerator()
		{
			return this._viewData.List.GetEnumerator();
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000A69D8 File Offset: 0x000A4BD8
		private void OnListChanged(ListChangedType listchangedType, int newIndex, int oldIndex)
		{
			ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(listchangedType, newIndex, oldIndex);
			this.OnListChanged(listChangedEventArgs);
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000A69F5 File Offset: 0x000A4BF5
		private void OnListChanged(ListChangedEventArgs changeArgs)
		{
			if (this.onListChanged != null && !this._suspendEvent)
			{
				this.onListChanged(this, changeArgs);
			}
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000A6A14 File Offset: 0x000A4C14
		void IObjectView.EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			int num = ((IList)this).IndexOf((TElement)((object)sender));
			this.OnListChanged(ListChangedType.ItemChanged, num, num);
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000A6A3C File Offset: 0x000A4C3C
		void IObjectView.CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			TElement telement = default(TElement);
			if (this._addNewIndex >= 0)
			{
				telement = this[this._addNewIndex];
			}
			ListChangedEventArgs listChangedEventArgs = this._viewData.OnCollectionChanged(sender, e, this._listener);
			if (this._addNewIndex >= 0)
			{
				if (this._addNewIndex >= this.Count)
				{
					this._addNewIndex = ((IList)this).IndexOf(telement);
				}
				else
				{
					TElement telement2 = this[this._addNewIndex];
					if (!telement2.Equals(telement))
					{
						this._addNewIndex = ((IList)this).IndexOf(telement);
					}
				}
			}
			if (listChangedEventArgs != null)
			{
				this.OnListChanged(listChangedEventArgs);
			}
		}

		// Token: 0x040010B0 RID: 4272
		private bool _suspendEvent;

		// Token: 0x040010B1 RID: 4273
		private ListChangedEventHandler onListChanged;

		// Token: 0x040010B2 RID: 4274
		private readonly ObjectViewListener _listener;

		// Token: 0x040010B3 RID: 4275
		private int _addNewIndex = -1;

		// Token: 0x040010B4 RID: 4276
		private readonly IObjectViewData<TElement> _viewData;
	}
}
