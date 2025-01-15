using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Microsoft.OData.Client.Materialization;

namespace Microsoft.OData.Client
{
	// Token: 0x020000BB RID: 187
	public class DataServiceCollection<T> : ObservableCollection<T>
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0001B458 File Offset: 0x00019658
		public DataServiceCollection()
			: this(null, null, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B466 File Offset: 0x00019666
		public DataServiceCollection(DataServiceQuerySingle<T> item)
			: this(null, item.Query, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001B479 File Offset: 0x00019679
		public DataServiceCollection(IEnumerable<T> items)
			: this(null, items, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B487 File Offset: 0x00019687
		public DataServiceCollection(TrackingMode trackingMode, DataServiceQuerySingle<T> item)
			: this(null, item.Query, trackingMode, null, null, null)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B49A File Offset: 0x0001969A
		public DataServiceCollection(IEnumerable<T> items, TrackingMode trackingMode)
			: this(null, items, trackingMode, null, null, null)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B4A8 File Offset: 0x000196A8
		public DataServiceCollection(DataServiceContext context)
			: this(context, null, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001B4B6 File Offset: 0x000196B6
		public DataServiceCollection(DataServiceContext context, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
			: this(context, null, TrackingMode.AutoChangeTracking, entitySetName, entityChangedCallback, collectionChangedCallback)
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001B4C5 File Offset: 0x000196C5
		public DataServiceCollection(IEnumerable<T> items, TrackingMode trackingMode, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
			: this(null, items, trackingMode, entitySetName, entityChangedCallback, collectionChangedCallback)
		{
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001B4D8 File Offset: 0x000196D8
		public DataServiceCollection(DataServiceContext context, IEnumerable<T> items, TrackingMode trackingMode, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
		{
			if (trackingMode == TrackingMode.AutoChangeTracking)
			{
				if (context == null)
				{
					if (items == null)
					{
						this.trackingOnLoad = true;
						this.entitySetName = entitySetName;
						this.entityChangedCallback = entityChangedCallback;
						this.collectionChangedCallback = collectionChangedCallback;
					}
					else
					{
						context = DataServiceCollection<T>.GetContextFromItems(items);
					}
				}
				if (!this.trackingOnLoad)
				{
					if (items != null)
					{
						DataServiceCollection<T>.ValidateIteratorParameter(items);
					}
					this.StartTracking(context, items, entitySetName, entityChangedCallback, collectionChangedCallback);
					return;
				}
			}
			else if (items != null)
			{
				this.Load(items);
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001B548 File Offset: 0x00019748
		[SuppressMessage("Microsoft.Performance", "CA1800", Justification = "Constructor and debug-only code can't reuse cast.")]
		internal DataServiceCollection(object entityMaterializer, DataServiceContext context, IEnumerable<T> items, TrackingMode trackingMode, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
			: this((context != null) ? context : ((ODataEntityMaterializer)entityMaterializer).EntityTrackingAdapter.Context, items, trackingMode, entitySetName, entityChangedCallback, collectionChangedCallback)
		{
			if (items != null)
			{
				((ODataEntityMaterializer)entityMaterializer).PropagateContinuation<T>(items, this);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000630 RID: 1584 RVA: 0x0001B580 File Offset: 0x00019780
		// (remove) Token: 0x06000631 RID: 1585 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public event EventHandler<LoadCompletedEventArgs> LoadCompleted;

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001B5ED File Offset: 0x000197ED
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001B5F5 File Offset: 0x000197F5
		public DataServiceQueryContinuation<T> Continuation
		{
			get
			{
				return this.continuation;
			}
			set
			{
				this.continuation = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001B5FE File Offset: 0x000197FE
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001B606 File Offset: 0x00019806
		internal BindingObserver Observer
		{
			get
			{
				return this.observer;
			}
			set
			{
				this.observer = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001B60F File Offset: 0x0001980F
		internal bool IsTracking
		{
			get
			{
				return this.observer != null;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001B61C File Offset: 0x0001981C
		public void Load(IEnumerable<T> items)
		{
			DataServiceCollection<T>.ValidateIteratorParameter(items);
			if (this.trackingOnLoad)
			{
				DataServiceContext contextFromItems = DataServiceCollection<T>.GetContextFromItems(items);
				this.trackingOnLoad = false;
				this.StartTracking(contextFromItems, items, this.entitySetName, this.entityChangedCallback, this.collectionChangedCallback);
				return;
			}
			this.StartLoading();
			try
			{
				this.InternalLoadCollection(items);
			}
			finally
			{
				this.FinishLoading();
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001B688 File Offset: 0x00019888
		public void LoadAsync(IQueryable<T> query)
		{
			Util.CheckArgumentNull<IQueryable<T>>(query, "query");
			DataServiceQuery<T> dsq = query as DataServiceQuery<T>;
			if (dsq == null)
			{
				throw new ArgumentException(Strings.DataServiceCollection_LoadAsyncRequiresDataServiceQuery, "query");
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			if (this.trackingOnLoad)
			{
				this.StartTracking(((DataServiceQueryProvider)dsq.Provider).Context, null, this.entitySetName, this.entityChangedCallback, this.collectionChangedCallback);
				this.trackingOnLoad = false;
			}
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => dsq.BeginExecute(asyncCallback, null), delegate(IAsyncResult asyncResult)
			{
				QueryOperationResponse<T> queryOperationResponse = (QueryOperationResponse<T>)dsq.EndExecute(asyncResult);
				this.Load(queryOperationResponse);
				return queryOperationResponse;
			});
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001B740 File Offset: 0x00019940
		public void LoadAsync(Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			DataServiceContext context = this.observer.Context;
			requestUri = UriUtil.CreateUri(context.BaseUri, requestUri);
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => context.BeginExecute<T>(requestUri, asyncCallback, null), delegate(IAsyncResult asyncResult)
			{
				QueryOperationResponse<T> queryOperationResponse = (QueryOperationResponse<T>)context.EndExecute<T>(asyncResult);
				this.Load(queryOperationResponse);
				return queryOperationResponse;
			});
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public void LoadAsync()
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			object parent;
			string property;
			if (!this.observer.LookupParent<T>(this, out parent, out property))
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity);
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => this.observer.Context.BeginLoadProperty(parent, property, asyncCallback, null), (IAsyncResult asyncResult) => this.observer.Context.EndLoadProperty(asyncResult));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001B868 File Offset: 0x00019A68
		public bool LoadNextPartialSetAsync()
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			if (this.Continuation == null)
			{
				if (this.LoadCompleted != null)
				{
					this.LoadCompleted(this, new LoadCompletedEventArgs(null, null));
				}
				return false;
			}
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => this.observer.Context.BeginExecute<T>(this.Continuation, asyncCallback, null), delegate(IAsyncResult asyncResult)
			{
				QueryOperationResponse<T> queryOperationResponse = (QueryOperationResponse<T>)this.observer.Context.EndExecute<T>(asyncResult);
				this.Load(queryOperationResponse);
				return queryOperationResponse;
			});
			return true;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001B8DF File Offset: 0x00019ADF
		public void CancelAsyncLoad()
		{
			if (this.ongoingAsyncOperation != null)
			{
				this.observer.Context.CancelRequest(this.ongoingAsyncOperation);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001B900 File Offset: 0x00019B00
		public void Load(T item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			this.StartLoading();
			try
			{
				if (!base.Contains(item))
				{
					base.Add(item);
				}
			}
			finally
			{
				this.FinishLoading();
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001B950 File Offset: 0x00019B50
		public void Clear(bool stopTracking)
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (!stopTracking)
			{
				base.Clear();
				return;
			}
			try
			{
				this.observer.DetachBehavior = true;
				base.Clear();
			}
			finally
			{
				this.observer.DetachBehavior = false;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001B9AC File Offset: 0x00019BAC
		public void Detach()
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (!this.rootCollection)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_CannotStopTrackingChildCollection);
			}
			this.observer.StopTracking();
			this.observer = null;
			this.rootCollection = false;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001B9F8 File Offset: 0x00019BF8
		protected override void InsertItem(int index, T item)
		{
			if (this.trackingOnLoad)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_InsertIntoTrackedButNotLoadedCollection);
			}
			if (this.IsTracking && item != null && !(item is INotifyPropertyChanged))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(item.GetType()));
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001BA57 File Offset: 0x00019C57
		private static void ValidateIteratorParameter(IEnumerable<T> items)
		{
			Util.CheckArgumentNull<IEnumerable<T>>(items, "items");
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001BA68 File Offset: 0x00019C68
		private static DataServiceContext GetContextFromItems(IEnumerable<T> items)
		{
			DataServiceQuery<T> dataServiceQuery = items as DataServiceQuery<T>;
			if (dataServiceQuery != null)
			{
				DataServiceQueryProvider dataServiceQueryProvider = dataServiceQuery.Provider as DataServiceQueryProvider;
				return dataServiceQueryProvider.Context;
			}
			QueryOperationResponse queryOperationResponse = items as QueryOperationResponse;
			if (queryOperationResponse != null)
			{
				return queryOperationResponse.Results.Context;
			}
			throw new ArgumentException(Strings.DataServiceCollection_CannotDetermineContextFromItems);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		private void InternalLoadCollection(IEnumerable<T> items)
		{
			DataServiceQuery<T> dataServiceQuery = items as DataServiceQuery<T>;
			if (dataServiceQuery != null)
			{
				items = dataServiceQuery.Execute() as QueryOperationResponse<T>;
			}
			foreach (T t in items)
			{
				if (!base.Contains(t))
				{
					base.Add(t);
				}
			}
			QueryOperationResponse<T> queryOperationResponse = items as QueryOperationResponse<T>;
			if (queryOperationResponse != null)
			{
				this.continuation = queryOperationResponse.GetContinuation();
				return;
			}
			this.continuation = null;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001BB40 File Offset: 0x00019D40
		private void StartLoading()
		{
			if (this.IsTracking)
			{
				if (this.observer.Context == null)
				{
					throw new InvalidOperationException(Strings.DataServiceCollection_LoadRequiresTargetCollectionObserved);
				}
				this.observer.AttachBehavior = true;
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001BB6E File Offset: 0x00019D6E
		private void FinishLoading()
		{
			if (this.IsTracking)
			{
				this.observer.AttachBehavior = false;
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001BB84 File Offset: 0x00019D84
		private void StartTracking(DataServiceContext context, IEnumerable<T> items, string entitySet, Func<EntityChangedParams, bool> entityChanged, Func<EntityCollectionChangedParams, bool> collectionChanged)
		{
			context.UsingDataServiceCollection = true;
			if (!BindingEntityInfo.IsEntityType(typeof(T), context.Model))
			{
				throw new ArgumentException(Strings.DataBinding_DataServiceCollectionArgumentMustHaveEntityType(typeof(T)));
			}
			this.observer = new BindingObserver(context, entityChanged, collectionChanged);
			if (items != null)
			{
				try
				{
					this.InternalLoadCollection(items);
				}
				catch
				{
					this.observer = null;
					throw;
				}
			}
			this.observer.StartTracking<T>(this, entitySet);
			this.rootCollection = true;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001BC10 File Offset: 0x00019E10
		private void BeginLoadAsyncOperation(Func<AsyncCallback, IAsyncResult> beginCall, Func<IAsyncResult, QueryOperationResponse> endCall)
		{
			this.ongoingAsyncOperation = null;
			try
			{
				SynchronizationContext syncContext = SynchronizationContext.Current;
				AsyncCallback asyncCallback;
				if (syncContext == null)
				{
					asyncCallback = delegate(IAsyncResult ar)
					{
						this.EndLoadAsyncOperation(endCall, ar);
					};
				}
				else
				{
					asyncCallback = delegate(IAsyncResult ar)
					{
						syncContext.Post(delegate(object unused)
						{
							this.EndLoadAsyncOperation(endCall, ar);
						}, null);
					};
				}
				this.ongoingAsyncOperation = beginCall(asyncCallback);
			}
			catch (Exception)
			{
				this.ongoingAsyncOperation = null;
				throw;
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		private void EndLoadAsyncOperation(Func<IAsyncResult, QueryOperationResponse> endCall, IAsyncResult asyncResult)
		{
			try
			{
				QueryOperationResponse queryOperationResponse = endCall(asyncResult);
				this.ongoingAsyncOperation = null;
				if (this.LoadCompleted != null)
				{
					this.LoadCompleted(this, new LoadCompletedEventArgs(queryOperationResponse, null));
				}
			}
			catch (Exception ex)
			{
				if (!CommonUtil.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.ongoingAsyncOperation = null;
				if (this.LoadCompleted != null)
				{
					this.LoadCompleted(this, new LoadCompletedEventArgs(null, ex));
				}
			}
		}

		// Token: 0x040002C5 RID: 709
		private BindingObserver observer;

		// Token: 0x040002C6 RID: 710
		private bool rootCollection;

		// Token: 0x040002C7 RID: 711
		private DataServiceQueryContinuation<T> continuation;

		// Token: 0x040002C8 RID: 712
		private bool trackingOnLoad;

		// Token: 0x040002C9 RID: 713
		private Func<EntityChangedParams, bool> entityChangedCallback;

		// Token: 0x040002CA RID: 714
		private Func<EntityCollectionChangedParams, bool> collectionChangedCallback;

		// Token: 0x040002CB RID: 715
		private string entitySetName;

		// Token: 0x040002CC RID: 716
		private IAsyncResult ongoingAsyncOperation;
	}
}
