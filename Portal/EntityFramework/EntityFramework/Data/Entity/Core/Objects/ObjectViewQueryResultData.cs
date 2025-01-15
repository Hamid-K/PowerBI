using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000429 RID: 1065
	internal sealed class ObjectViewQueryResultData<TElement> : IObjectViewData<TElement>
	{
		// Token: 0x060033C7 RID: 13255 RVA: 0x000A74E4 File Offset: 0x000A56E4
		internal ObjectViewQueryResultData(IEnumerable queryResults, ObjectContext objectContext, bool forceReadOnlyList, EntitySet entitySet)
		{
			bool flag = ObjectViewQueryResultData<TElement>.IsEditable(typeof(TElement));
			this._objectContext = objectContext;
			this._entitySet = entitySet;
			this._canEditItems = flag;
			this._canModifyList = !forceReadOnlyList && flag && this._objectContext != null;
			this._bindingList = new List<TElement>();
			foreach (object obj in queryResults)
			{
				TElement telement = (TElement)((object)obj);
				this._bindingList.Add(telement);
			}
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000A7590 File Offset: 0x000A5790
		private static bool IsEditable(Type elementType)
		{
			return !(elementType == typeof(DbDataRecord)) && (!(elementType != typeof(DbDataRecord)) || !elementType.IsSubclassOf(typeof(DbDataRecord)));
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000A75CD File Offset: 0x000A57CD
		private void EnsureEntitySet()
		{
			if (this._entitySet == null)
			{
				throw new InvalidOperationException(Strings.ObjectView_CannotResolveTheEntitySet(typeof(TElement).FullName));
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000A75F1 File Offset: 0x000A57F1
		public IList<TElement> List
		{
			get
			{
				return this._bindingList;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000A75F9 File Offset: 0x000A57F9
		public bool AllowNew
		{
			get
			{
				return this._canModifyList && this._entitySet != null;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000A760E File Offset: 0x000A580E
		public bool AllowEdit
		{
			get
			{
				return this._canEditItems;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000A7616 File Offset: 0x000A5816
		public bool AllowRemove
		{
			get
			{
				return this._canModifyList;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000A761E File Offset: 0x000A581E
		public bool FiresEventOnAdd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000A7621 File Offset: 0x000A5821
		public bool FiresEventOnRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000A7624 File Offset: 0x000A5824
		public bool FiresEventOnClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000A7627 File Offset: 0x000A5827
		public void EnsureCanAddNew()
		{
			this.EnsureEntitySet();
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000A7630 File Offset: 0x000A5830
		public int Add(TElement item, bool isAddNew)
		{
			this.EnsureEntitySet();
			if (!isAddNew)
			{
				this._objectContext.AddObject(TypeHelpers.GetFullName(this._entitySet.EntityContainer.Name, this._entitySet.Name), item);
			}
			this._bindingList.Add(item);
			return this._bindingList.Count - 1;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000A7690 File Offset: 0x000A5890
		public void CommitItemAt(int index)
		{
			this.EnsureEntitySet();
			TElement telement = this._bindingList[index];
			this._objectContext.AddObject(TypeHelpers.GetFullName(this._entitySet.EntityContainer.Name, this._entitySet.Name), telement);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000A76E4 File Offset: 0x000A58E4
		public void Clear()
		{
			while (0 < this._bindingList.Count)
			{
				TElement telement = this._bindingList[this._bindingList.Count - 1];
				this.Remove(telement, false);
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000A7724 File Offset: 0x000A5924
		public bool Remove(TElement item, bool isCancelNew)
		{
			bool flag;
			if (isCancelNew)
			{
				flag = this._bindingList.Remove(item);
			}
			else
			{
				EntityEntry entityEntry = this._objectContext.ObjectStateManager.FindEntityEntry(item);
				if (entityEntry != null)
				{
					entityEntry.Delete();
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000A776C File Offset: 0x000A596C
		public ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener)
		{
			ListChangedEventArgs listChangedEventArgs = null;
			if (e.Element.GetType().IsAssignableFrom(typeof(TElement)) && this._bindingList.Contains((TElement)((object)e.Element)))
			{
				TElement telement = (TElement)((object)e.Element);
				int num = this._bindingList.IndexOf(telement);
				if (num >= 0 && e.Action == CollectionChangeAction.Remove)
				{
					this._bindingList.Remove(telement);
					listener.UnregisterEntityEvents(telement);
					listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemDeleted, num, -1);
				}
			}
			return listChangedEventArgs;
		}

		// Token: 0x040010C0 RID: 4288
		private readonly List<TElement> _bindingList;

		// Token: 0x040010C1 RID: 4289
		private readonly ObjectContext _objectContext;

		// Token: 0x040010C2 RID: 4290
		private readonly EntitySet _entitySet;

		// Token: 0x040010C3 RID: 4291
		private readonly bool _canEditItems;

		// Token: 0x040010C4 RID: 4292
		private readonly bool _canModifyList;
	}
}
