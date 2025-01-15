using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F7 RID: 247
	internal class DbLocalView<TEntity> : ObservableCollection<TEntity>, ICollection<TEntity>, IEnumerable<TEntity>, IEnumerable, IList, ICollection where TEntity : class
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x0002FA9A File Offset: 0x0002DC9A
		public DbLocalView()
		{
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0002FAA2 File Offset: 0x0002DCA2
		public DbLocalView(IEnumerable<TEntity> collection)
		{
			Check.NotNull<IEnumerable<TEntity>>(collection, "collection");
			collection.Each(new Action<TEntity>(this.Add));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0002FACC File Offset: 0x0002DCCC
		internal DbLocalView(InternalContext internalContext)
		{
			this._internalContext = internalContext;
			try
			{
				this._inStateManagerChanged = true;
				foreach (TEntity tentity in this._internalContext.GetLocalEntities<TEntity>())
				{
					base.Add(tentity);
				}
			}
			finally
			{
				this._inStateManagerChanged = false;
			}
			this._internalContext.RegisterObjectStateManagerChangedEvent(new CollectionChangeEventHandler(this.StateManagerChangedHandler));
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0002FB60 File Offset: 0x0002DD60
		internal ObservableBackedBindingList<TEntity> BindingList
		{
			get
			{
				ObservableBackedBindingList<TEntity> observableBackedBindingList;
				if ((observableBackedBindingList = this._bindingList) == null)
				{
					observableBackedBindingList = (this._bindingList = new ObservableBackedBindingList<TEntity>(this));
				}
				return observableBackedBindingList;
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0002FB88 File Offset: 0x0002DD88
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (!this._inStateManagerChanged && this._internalContext != null)
			{
				if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
				{
					foreach (object obj in e.OldItems)
					{
						TEntity tentity = (TEntity)((object)obj);
						this._internalContext.Set<TEntity>().Remove(tentity);
					}
				}
				if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
				{
					foreach (object obj2 in e.NewItems)
					{
						TEntity tentity2 = (TEntity)((object)obj2);
						if (!this._internalContext.EntityInContextAndNotDeleted(tentity2))
						{
							this._internalContext.Set<TEntity>().Add(tentity2);
						}
					}
				}
			}
			base.OnCollectionChanged(e);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0002FC94 File Offset: 0x0002DE94
		private void StateManagerChangedHandler(object sender, CollectionChangeEventArgs e)
		{
			try
			{
				this._inStateManagerChanged = true;
				TEntity tentity = e.Element as TEntity;
				if (tentity != null)
				{
					if (e.Action == CollectionChangeAction.Remove && this.Contains(tentity))
					{
						this.Remove(tentity);
					}
					else if (e.Action == CollectionChangeAction.Add && !this.Contains(tentity))
					{
						base.Add(tentity);
					}
				}
			}
			finally
			{
				this._inStateManagerChanged = false;
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0002FD10 File Offset: 0x0002DF10
		protected override void ClearItems()
		{
			new List<TEntity>(this).Each((TEntity t) => this.Remove(t));
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0002FD29 File Offset: 0x0002DF29
		protected override void InsertItem(int index, TEntity item)
		{
			if (!this.Contains(item))
			{
				base.InsertItem(index, item);
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0002FD3C File Offset: 0x0002DF3C
		public new virtual bool Contains(TEntity item)
		{
			IEqualityComparer<TEntity> @default = ObjectReferenceEqualityComparer.Default;
			foreach (TEntity tentity in base.Items)
			{
				if (@default.Equals(tentity, item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0002FD9C File Offset: 0x0002DF9C
		public new virtual bool Remove(TEntity item)
		{
			IEqualityComparer<TEntity> @default = ObjectReferenceEqualityComparer.Default;
			int num = 0;
			while (num < base.Count && !@default.Equals(base.Items[num], item))
			{
				num++;
			}
			if (num == base.Count)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
		bool ICollection<TEntity>.Contains(TEntity item)
		{
			return this.Contains(item);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0002FDF1 File Offset: 0x0002DFF1
		bool ICollection<TEntity>.Remove(TEntity item)
		{
			return this.Remove(item);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0002FDFA File Offset: 0x0002DFFA
		bool IList.Contains(object value)
		{
			return DbLocalView<TEntity>.IsCompatibleObject(value) && this.Contains((TEntity)((object)value));
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0002FE12 File Offset: 0x0002E012
		void IList.Remove(object value)
		{
			if (DbLocalView<TEntity>.IsCompatibleObject(value))
			{
				this.Remove((TEntity)((object)value));
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0002FE29 File Offset: 0x0002E029
		private static bool IsCompatibleObject(object value)
		{
			return value is TEntity || value == null;
		}

		// Token: 0x0400090F RID: 2319
		private readonly InternalContext _internalContext;

		// Token: 0x04000910 RID: 2320
		private bool _inStateManagerChanged;

		// Token: 0x04000911 RID: 2321
		private ObservableBackedBindingList<TEntity> _bindingList;
	}
}
