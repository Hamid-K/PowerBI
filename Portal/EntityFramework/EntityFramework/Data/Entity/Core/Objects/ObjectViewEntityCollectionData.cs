using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000426 RID: 1062
	internal sealed class ObjectViewEntityCollectionData<TViewElement, TItemElement> : IObjectViewData<TViewElement> where TViewElement : TItemElement where TItemElement : class
	{
		// Token: 0x060033A9 RID: 13225 RVA: 0x000A6AE4 File Offset: 0x000A4CE4
		internal ObjectViewEntityCollectionData(EntityCollection<TItemElement> entityCollection)
		{
			this._entityCollection = entityCollection;
			this._canEditItems = true;
			this._bindingList = new List<TViewElement>(entityCollection.Count);
			foreach (TItemElement titemElement in entityCollection)
			{
				TViewElement tviewElement = (TViewElement)((object)titemElement);
				this._bindingList.Add(tviewElement);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000A6B60 File Offset: 0x000A4D60
		public IList<TViewElement> List
		{
			get
			{
				return this._bindingList;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000A6B68 File Offset: 0x000A4D68
		public bool AllowNew
		{
			get
			{
				return !this._entityCollection.IsReadOnly;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000A6B78 File Offset: 0x000A4D78
		public bool AllowEdit
		{
			get
			{
				return this._canEditItems;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000A6B80 File Offset: 0x000A4D80
		public bool AllowRemove
		{
			get
			{
				return !this._entityCollection.IsReadOnly;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000A6B90 File Offset: 0x000A4D90
		public bool FiresEventOnAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000A6B93 File Offset: 0x000A4D93
		public bool FiresEventOnRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000A6B96 File Offset: 0x000A4D96
		public bool FiresEventOnClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000A6B99 File Offset: 0x000A4D99
		public void EnsureCanAddNew()
		{
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000A6B9B File Offset: 0x000A4D9B
		public int Add(TViewElement item, bool isAddNew)
		{
			if (isAddNew)
			{
				this._bindingList.Add(item);
			}
			else
			{
				this._entityCollection.Add((TItemElement)((object)item));
			}
			return this._bindingList.Count - 1;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000A6BD4 File Offset: 0x000A4DD4
		public void CommitItemAt(int index)
		{
			TViewElement tviewElement = this._bindingList[index];
			try
			{
				this._itemCommitPending = true;
				this._entityCollection.Add((TItemElement)((object)tviewElement));
			}
			finally
			{
				this._itemCommitPending = false;
			}
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000A6C28 File Offset: 0x000A4E28
		public void Clear()
		{
			if (0 < this._bindingList.Count)
			{
				List<object> list = new List<object>();
				foreach (TViewElement tviewElement in this._bindingList)
				{
					object obj = tviewElement;
					list.Add(obj);
				}
				this._entityCollection.BulkDeleteAll(list);
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000A6CA0 File Offset: 0x000A4EA0
		public bool Remove(TViewElement item, bool isCancelNew)
		{
			bool flag;
			if (isCancelNew)
			{
				flag = this._bindingList.Remove(item);
			}
			else
			{
				flag = this._entityCollection.RemoveInternal((TItemElement)((object)item));
			}
			return flag;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000A6CD8 File Offset: 0x000A4ED8
		public ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener)
		{
			ListChangedEventArgs listChangedEventArgs = null;
			switch (e.Action)
			{
			case CollectionChangeAction.Add:
				if (e.Element is TViewElement && !this._itemCommitPending)
				{
					TViewElement tviewElement = (TViewElement)((object)e.Element);
					this._bindingList.Add(tviewElement);
					listener.RegisterEntityEvents(tviewElement);
					listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemAdded, this._bindingList.Count - 1, -1);
				}
				break;
			case CollectionChangeAction.Remove:
				if (e.Element is TViewElement)
				{
					TViewElement tviewElement2 = (TViewElement)((object)e.Element);
					int num = this._bindingList.IndexOf(tviewElement2);
					if (num != -1)
					{
						this._bindingList.Remove(tviewElement2);
						listener.UnregisterEntityEvents(tviewElement2);
						listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemDeleted, num, -1);
					}
				}
				break;
			case CollectionChangeAction.Refresh:
				foreach (TViewElement tviewElement3 in this._bindingList)
				{
					listener.UnregisterEntityEvents(tviewElement3);
				}
				this._bindingList.Clear();
				foreach (object obj in this._entityCollection.GetInternalEnumerable())
				{
					TViewElement tviewElement4 = (TViewElement)((object)obj);
					this._bindingList.Add(tviewElement4);
					listener.RegisterEntityEvents(tviewElement4);
				}
				listChangedEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1, -1);
				break;
			}
			return listChangedEventArgs;
		}

		// Token: 0x040010B5 RID: 4277
		private readonly List<TViewElement> _bindingList;

		// Token: 0x040010B6 RID: 4278
		private readonly EntityCollection<TItemElement> _entityCollection;

		// Token: 0x040010B7 RID: 4279
		private readonly bool _canEditItems;

		// Token: 0x040010B8 RID: 4280
		private bool _itemCommitPending;
	}
}
