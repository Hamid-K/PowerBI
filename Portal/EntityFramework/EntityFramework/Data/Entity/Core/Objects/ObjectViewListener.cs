using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000428 RID: 1064
	internal sealed class ObjectViewListener
	{
		// Token: 0x060033BD RID: 13245 RVA: 0x000A7265 File Offset: 0x000A5465
		internal ObjectViewListener(IObjectView view, IList list, object dataSource)
		{
			this._viewWeak = new WeakReference(view);
			this._dataSource = dataSource;
			this._list = list;
			this.RegisterCollectionEvents();
			this.RegisterEntityEvents();
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000A7293 File Offset: 0x000A5493
		private void CleanUpListener()
		{
			this.UnregisterCollectionEvents();
			this.UnregisterEntityEvents();
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000A72A4 File Offset: 0x000A54A4
		private void RegisterCollectionEvents()
		{
			ObjectStateManager objectStateManager = this._dataSource as ObjectStateManager;
			if (objectStateManager != null)
			{
				objectStateManager.EntityDeleted += this.CollectionChanged;
				return;
			}
			if (this._dataSource != null)
			{
				((RelatedEnd)this._dataSource).AssociationChangedForObjectView += this.CollectionChanged;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000A72F8 File Offset: 0x000A54F8
		private void UnregisterCollectionEvents()
		{
			ObjectStateManager objectStateManager = this._dataSource as ObjectStateManager;
			if (objectStateManager != null)
			{
				objectStateManager.EntityDeleted -= this.CollectionChanged;
				return;
			}
			if (this._dataSource != null)
			{
				((RelatedEnd)this._dataSource).AssociationChangedForObjectView -= this.CollectionChanged;
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000A734C File Offset: 0x000A554C
		internal void RegisterEntityEvents(object entity)
		{
			INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged += this.EntityPropertyChanged;
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000A7378 File Offset: 0x000A5578
		private void RegisterEntityEvents()
		{
			if (this._list != null)
			{
				foreach (object obj in this._list)
				{
					INotifyPropertyChanged notifyPropertyChanged = obj as INotifyPropertyChanged;
					if (notifyPropertyChanged != null)
					{
						notifyPropertyChanged.PropertyChanged += this.EntityPropertyChanged;
					}
				}
			}
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000A73E8 File Offset: 0x000A55E8
		internal void UnregisterEntityEvents(object entity)
		{
			INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.EntityPropertyChanged;
			}
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000A7414 File Offset: 0x000A5614
		private void UnregisterEntityEvents()
		{
			if (this._list != null)
			{
				foreach (object obj in this._list)
				{
					INotifyPropertyChanged notifyPropertyChanged = obj as INotifyPropertyChanged;
					if (notifyPropertyChanged != null)
					{
						notifyPropertyChanged.PropertyChanged -= this.EntityPropertyChanged;
					}
				}
			}
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000A7484 File Offset: 0x000A5684
		private void EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IObjectView objectView = (IObjectView)this._viewWeak.Target;
			if (objectView != null)
			{
				objectView.EntityPropertyChanged(sender, e);
				return;
			}
			this.CleanUpListener();
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000A74B4 File Offset: 0x000A56B4
		private void CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			IObjectView objectView = (IObjectView)this._viewWeak.Target;
			if (objectView != null)
			{
				objectView.CollectionChanged(sender, e);
				return;
			}
			this.CleanUpListener();
		}

		// Token: 0x040010BD RID: 4285
		private readonly WeakReference _viewWeak;

		// Token: 0x040010BE RID: 4286
		private readonly object _dataSource;

		// Token: 0x040010BF RID: 4287
		private readonly IList _list;
	}
}
