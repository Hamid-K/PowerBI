using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B5 RID: 181
	internal sealed class BindingObserver
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001A685 File Offset: 0x00018885
		internal BindingObserver(DataServiceContext context, Func<EntityChangedParams, bool> entityChanged, Func<EntityCollectionChangedParams, bool> collectionChanged)
		{
			this.Context = context;
			this.Context.ChangesSaved += this.OnChangesSaved;
			this.EntityChanged = entityChanged;
			this.CollectionChanged = collectionChanged;
			this.bindingGraph = new BindingGraph(this);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001A6C5 File Offset: 0x000188C5
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0001A6CD File Offset: 0x000188CD
		internal DataServiceContext Context { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001A6D6 File Offset: 0x000188D6
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0001A6DE File Offset: 0x000188DE
		internal bool AttachBehavior { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001A6E7 File Offset: 0x000188E7
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001A6EF File Offset: 0x000188EF
		internal bool DetachBehavior { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001A6F8 File Offset: 0x000188F8
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0001A700 File Offset: 0x00018900
		internal Func<EntityChangedParams, bool> EntityChanged { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001A709 File Offset: 0x00018909
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0001A711 File Offset: 0x00018911
		internal Func<EntityCollectionChangedParams, bool> CollectionChanged { get; private set; }

		// Token: 0x060005FC RID: 1532 RVA: 0x0001A71C File Offset: 0x0001891C
		internal void StartTracking<T>(DataServiceCollection<T> collection, string collectionEntitySet)
		{
			try
			{
				this.AttachBehavior = true;
				this.bindingGraph.AddDataServiceCollection(null, null, collection, collectionEntitySet);
			}
			finally
			{
				this.AttachBehavior = false;
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001A75C File Offset: 0x0001895C
		internal void StopTracking()
		{
			this.bindingGraph.Reset();
			this.Context.ChangesSaved -= this.OnChangesSaved;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001A780 File Offset: 0x00018980
		internal bool LookupParent<T>(DataServiceCollection<T> collection, out object parentEntity, out string parentProperty)
		{
			string text;
			string text2;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out parentEntity, out parentProperty, out text, out text2);
			return parentEntity != null;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001A7A4 File Offset: 0x000189A4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal void OnPropertyChanged(object source, PropertyChangedEventArgs eventArgs)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<PropertyChangedEventArgs>(eventArgs, "eventArgs");
			string propertyName = eventArgs.PropertyName;
			if (string.IsNullOrEmpty(propertyName))
			{
				this.HandleUpdateEntity(source, null, null);
				return;
			}
			BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo;
			ClientPropertyAnnotation clientPropertyAnnotation;
			object obj;
			if (BindingEntityInfo.TryGetPropertyValue(source, propertyName, this.Context.Model, out bindingPropertyInfo, out clientPropertyAnnotation, out obj))
			{
				if (bindingPropertyInfo != null)
				{
					this.bindingGraph.RemoveRelation(source, propertyName);
					switch (bindingPropertyInfo.PropertyKind)
					{
					case BindingPropertyKind.BindingPropertyKindEntity:
						this.bindingGraph.AddEntity(source, propertyName, obj, null, source);
						return;
					case BindingPropertyKind.BindingPropertyKindDataServiceCollection:
						if (obj == null)
						{
							return;
						}
						try
						{
							typeof(BindingUtils).GetMethod("VerifyObserverNotPresent", false, true).MakeGenericMethod(new Type[] { bindingPropertyInfo.PropertyInfo.EntityCollectionItemType }).Invoke(null, new object[]
							{
								obj,
								propertyName,
								source.GetType()
							});
						}
						catch (TargetInvocationException ex)
						{
							throw ex.InnerException;
						}
						try
						{
							this.AttachBehavior = true;
							this.bindingGraph.AddDataServiceCollection(source, propertyName, obj, null);
							return;
						}
						finally
						{
							this.AttachBehavior = false;
						}
						break;
					case BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection:
						break;
					default:
						if (obj != null)
						{
							this.bindingGraph.AddComplexObject(source, propertyName, obj);
						}
						this.HandleUpdateEntity(source, propertyName, obj);
						return;
					}
					if (obj != null)
					{
						this.bindingGraph.AddPrimitiveOrComplexCollection(source, propertyName, obj, bindingPropertyInfo.PropertyInfo.PrimitiveOrComplexCollectionItemType);
					}
					this.HandleUpdateEntity(source, propertyName, obj);
					return;
				}
				if (!clientPropertyAnnotation.IsStreamLinkProperty)
				{
					this.HandleUpdateEntity(source, propertyName, obj);
				}
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001A938 File Offset: 0x00018B38
		internal void OnDataServiceCollectionChanged(object collection, NotifyCollectionChangedEventArgs eventArgs)
		{
			Util.CheckArgumentNull<object>(collection, "collection");
			Util.CheckArgumentNull<NotifyCollectionChangedEventArgs>(eventArgs, "eventArgs");
			object obj;
			string text;
			string text2;
			string text3;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out obj, out text, out text2, out text3);
			switch (eventArgs.Action)
			{
			case NotifyCollectionChangedAction.Add:
				this.OnAddToDataServiceCollection(eventArgs, obj, text, text3, collection);
				return;
			case NotifyCollectionChangedAction.Remove:
				this.OnRemoveFromDataServiceCollection(eventArgs, obj, text, collection);
				return;
			case NotifyCollectionChangedAction.Replace:
				this.OnRemoveFromDataServiceCollection(eventArgs, obj, text, collection);
				this.OnAddToDataServiceCollection(eventArgs, obj, text, text3, collection);
				return;
			case NotifyCollectionChangedAction.Move:
				return;
			case NotifyCollectionChangedAction.Reset:
				if (this.DetachBehavior)
				{
					this.RemoveWithDetachDataServiceCollection(collection);
					return;
				}
				this.bindingGraph.RemoveCollection(collection);
				return;
			default:
				throw new InvalidOperationException(Strings.DataBinding_DataServiceCollectionChangedUnknownActionCollection(eventArgs.Action));
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		internal void OnPrimitiveOrComplexCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Util.CheckArgumentNull<object>(sender, "sender");
			Util.CheckArgumentNull<NotifyCollectionChangedEventArgs>(e, "e");
			object obj;
			string text;
			Type type;
			this.bindingGraph.GetPrimitiveOrComplexCollectionInfo(sender, out obj, out text, out type);
			if (!PrimitiveType.IsKnownNullableType(type) && !type.IsEnum())
			{
				switch (e.Action)
				{
				case NotifyCollectionChangedAction.Add:
					this.OnAddToComplexTypeCollection(sender, e.NewItems);
					break;
				case NotifyCollectionChangedAction.Remove:
					this.OnRemoveFromComplexTypeCollection(sender, e.OldItems);
					break;
				case NotifyCollectionChangedAction.Replace:
					this.OnRemoveFromComplexTypeCollection(sender, e.OldItems);
					this.OnAddToComplexTypeCollection(sender, e.NewItems);
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					this.bindingGraph.RemoveCollection(sender);
					break;
				default:
					throw new InvalidOperationException(Strings.DataBinding_CollectionChangedUnknownActionCollection(e.Action, sender.GetType()));
				}
			}
			this.HandleUpdateEntity(obj, text, sender);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Pending")]
		internal void HandleAddEntity(object source, string sourceProperty, string sourceEntitySet, ICollection collection, object target, string targetEntitySet)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (source != null && this.IsDetachedOrDeletedFromContext(source))
			{
				return;
			}
			EntityDescriptor entityDescriptor = this.Context.GetEntityDescriptor(target);
			bool flag = !this.AttachBehavior && (entityDescriptor == null || (source != null && !this.IsContextTrackingLink(source, sourceProperty, target) && entityDescriptor.State != EntityStates.Deleted));
			if (flag && this.CollectionChanged != null)
			{
				EntityCollectionChangedParams entityCollectionChangedParams = new EntityCollectionChangedParams(this.Context, source, sourceProperty, sourceEntitySet, collection, target, targetEntitySet, NotifyCollectionChangedAction.Add);
				if (this.CollectionChanged(entityCollectionChangedParams))
				{
					return;
				}
			}
			if (source != null && this.IsDetachedOrDeletedFromContext(source))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_DetachedSource);
			}
			entityDescriptor = this.Context.GetEntityDescriptor(target);
			if (source != null)
			{
				if (this.AttachBehavior)
				{
					if (entityDescriptor == null)
					{
						BindingUtils.ValidateEntitySetName(targetEntitySet, target);
						this.Context.AttachTo(targetEntitySet, target);
						this.Context.AttachLink(source, sourceProperty, target);
						return;
					}
					if (entityDescriptor.State != EntityStates.Deleted && !this.IsContextTrackingLink(source, sourceProperty, target))
					{
						this.Context.AttachLink(source, sourceProperty, target);
						return;
					}
				}
				else
				{
					if (entityDescriptor == null)
					{
						this.Context.AddRelatedObject(source, sourceProperty, target);
						return;
					}
					if (entityDescriptor.State != EntityStates.Deleted && !this.IsContextTrackingLink(source, sourceProperty, target))
					{
						this.Context.AddLink(source, sourceProperty, target);
						return;
					}
				}
			}
			else if (entityDescriptor == null)
			{
				BindingUtils.ValidateEntitySetName(targetEntitySet, target);
				if (this.AttachBehavior)
				{
					this.Context.AttachTo(targetEntitySet, target);
					return;
				}
				this.Context.AddObject(targetEntitySet, target);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001AC60 File Offset: 0x00018E60
		internal void HandleDeleteEntity(object source, string sourceProperty, string sourceEntitySet, ICollection collection, object target, string targetEntitySet)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (source != null && this.IsDetachedOrDeletedFromContext(source))
			{
				return;
			}
			bool flag = this.IsContextTrackingEntity(target) && !this.DetachBehavior;
			if (flag && this.CollectionChanged != null)
			{
				EntityCollectionChangedParams entityCollectionChangedParams = new EntityCollectionChangedParams(this.Context, source, sourceProperty, sourceEntitySet, collection, target, targetEntitySet, NotifyCollectionChangedAction.Remove);
				if (this.CollectionChanged(entityCollectionChangedParams))
				{
					return;
				}
			}
			if (source != null && !this.IsContextTrackingEntity(source))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_DetachedSource);
			}
			if (this.IsContextTrackingEntity(target))
			{
				if (this.DetachBehavior)
				{
					this.Context.Detach(target);
					return;
				}
				this.Context.DeleteObject(target);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001AD14 File Offset: 0x00018F14
		internal void HandleUpdateEntityReference(object source, string sourceProperty, string sourceEntitySet, object target, string targetEntitySet)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (this.IsDetachedOrDeletedFromContext(source))
			{
				return;
			}
			EntityDescriptor entityDescriptor = ((target != null) ? this.Context.GetEntityDescriptor(target) : null);
			bool flag = !this.AttachBehavior && (entityDescriptor == null || !this.IsContextTrackingLink(source, sourceProperty, target));
			if (flag && this.EntityChanged != null)
			{
				EntityChangedParams entityChangedParams = new EntityChangedParams(this.Context, source, sourceProperty, target, sourceEntitySet, targetEntitySet);
				if (this.EntityChanged(entityChangedParams))
				{
					return;
				}
			}
			if (this.IsDetachedOrDeletedFromContext(source))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_DetachedSource);
			}
			entityDescriptor = ((target != null) ? this.Context.GetEntityDescriptor(target) : null);
			if (target != null)
			{
				bool flag2 = true;
				if (entityDescriptor == null)
				{
					if (targetEntitySet == null && !this.AttachBehavior)
					{
						this.Context.UpdateRelatedObject(source, sourceProperty, target);
						flag2 = false;
					}
					else
					{
						BindingUtils.ValidateEntitySetName(targetEntitySet, target);
						if (this.AttachBehavior)
						{
							this.Context.AttachTo(targetEntitySet, target);
						}
						else
						{
							this.Context.AddObject(targetEntitySet, target);
						}
					}
					entityDescriptor = this.Context.GetEntityDescriptor(target);
				}
				if (!this.IsContextTrackingLink(source, sourceProperty, target))
				{
					if (this.AttachBehavior)
					{
						if (entityDescriptor.State != EntityStates.Deleted)
						{
							this.Context.AttachLink(source, sourceProperty, target);
							return;
						}
					}
					else if (flag2)
					{
						this.Context.SetLink(source, sourceProperty, target);
						return;
					}
				}
			}
			else
			{
				this.Context.SetLink(source, sourceProperty, null);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001AE7B File Offset: 0x0001907B
		internal bool IsContextTrackingEntity(object entity)
		{
			return this.Context.GetEntityDescriptor(entity) != null;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001AE8C File Offset: 0x0001908C
		private void HandleUpdateEntity(object entity, string propertyName, object propertyValue)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (!BindingEntityInfo.IsEntityType(entity.GetType(), this.Context.Model))
			{
				this.bindingGraph.GetAncestorEntityForComplexProperty(ref entity, ref propertyName, ref propertyValue);
			}
			if (this.IsDetachedOrDeletedFromContext(entity))
			{
				return;
			}
			HashSet<string> propertiesToSerialize = this.Context.GetEntityDescriptor(entity).PropertiesToSerialize;
			if (propertyName != null)
			{
				propertiesToSerialize.Add(propertyName);
			}
			if (this.EntityChanged != null)
			{
				EntityChangedParams entityChangedParams = new EntityChangedParams(this.Context, entity, propertyName, propertyValue, null, null);
				if (this.EntityChanged(entityChangedParams))
				{
					return;
				}
			}
			if (this.IsContextTrackingEntity(entity))
			{
				this.Context.UpdateObject(entity);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001AF34 File Offset: 0x00019134
		private void OnAddToDataServiceCollection(NotifyCollectionChangedEventArgs eventArgs, object source, string sourceProperty, string targetEntitySet, object collection)
		{
			if (eventArgs.NewItems != null)
			{
				foreach (object obj in eventArgs.NewItems)
				{
					if (obj == null)
					{
						throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNull("Add"));
					}
					if (!BindingEntityInfo.IsEntityType(obj.GetType(), this.Context.Model))
					{
						throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNotEntity("Add"));
					}
					this.bindingGraph.AddEntity(source, sourceProperty, obj, targetEntitySet, collection);
				}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001AFD8 File Offset: 0x000191D8
		private void OnRemoveFromDataServiceCollection(NotifyCollectionChangedEventArgs eventArgs, object source, string sourceProperty, object collection)
		{
			if (eventArgs.OldItems != null)
			{
				this.DeepRemoveDataServiceCollection(eventArgs.OldItems, source ?? collection, sourceProperty, new Action<object>(this.ValidateDataServiceCollectionItem));
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001B004 File Offset: 0x00019204
		private void RemoveWithDetachDataServiceCollection(object collection)
		{
			object obj = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out obj, out text, out text2, out text3);
			this.DeepRemoveDataServiceCollection(this.bindingGraph.GetDataServiceCollectionItems(collection), obj ?? collection, text, null);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001B048 File Offset: 0x00019248
		private void DeepRemoveDataServiceCollection(IEnumerable collection, object source, string sourceProperty, Action<object> itemValidator)
		{
			foreach (object obj in collection)
			{
				if (itemValidator != null)
				{
					itemValidator(obj);
				}
				List<BindingObserver.UnTrackingInfo> list = new List<BindingObserver.UnTrackingInfo>();
				this.CollectUnTrackingInfo(obj, source, sourceProperty, list);
				foreach (BindingObserver.UnTrackingInfo unTrackingInfo in list)
				{
					this.bindingGraph.RemoveDataServiceCollectionItem(unTrackingInfo.Entity, unTrackingInfo.Parent, unTrackingInfo.ParentProperty);
				}
			}
			this.bindingGraph.RemoveUnreachableVertices();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001B114 File Offset: 0x00019314
		private void OnAddToComplexTypeCollection(object collection, IList newItems)
		{
			if (newItems != null)
			{
				this.bindingGraph.AddComplexObjectsFromCollection(collection, newItems);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001B128 File Offset: 0x00019328
		private void OnRemoveFromComplexTypeCollection(object collection, IList items)
		{
			if (items != null)
			{
				foreach (object obj in items)
				{
					this.bindingGraph.RemoveComplexTypeCollectionItem(obj, collection);
				}
				this.bindingGraph.RemoveUnreachableVertices();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001B18C File Offset: 0x0001938C
		private void OnChangesSaved(object sender, SaveChangesEventArgs eventArgs)
		{
			this.bindingGraph.RemoveNonTrackedEntities();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001B19C File Offset: 0x0001939C
		private void CollectUnTrackingInfo(object currentEntity, object parentEntity, string parentProperty, IList<BindingObserver.UnTrackingInfo> entitiesToUnTrack)
		{
			IEnumerable<EntityDescriptor> entities = this.Context.Entities;
			Func<EntityDescriptor, bool> <>9__0;
			Func<EntityDescriptor, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (EntityDescriptor x) => x.ParentEntity == currentEntity && x.State == EntityStates.Added);
			}
			foreach (EntityDescriptor entityDescriptor in entities.Where(func))
			{
				this.CollectUnTrackingInfo(entityDescriptor.Entity, entityDescriptor.ParentEntity, entityDescriptor.ParentPropertyForInsert, entitiesToUnTrack);
			}
			entitiesToUnTrack.Add(new BindingObserver.UnTrackingInfo
			{
				Entity = currentEntity,
				Parent = parentEntity,
				ParentProperty = parentProperty
			});
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001B258 File Offset: 0x00019458
		private bool IsContextTrackingLink(object source, string sourceProperty, object target)
		{
			return this.Context.GetLinkDescriptor(source, sourceProperty, target) != null;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001B26C File Offset: 0x0001946C
		private bool IsDetachedOrDeletedFromContext(object entity)
		{
			EntityDescriptor entityDescriptor = this.Context.GetEntityDescriptor(entity);
			return entityDescriptor == null || entityDescriptor.State == EntityStates.Deleted;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001B294 File Offset: 0x00019494
		private void ValidateDataServiceCollectionItem(object target)
		{
			if (target == null)
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNull("Remove"));
			}
			if (!BindingEntityInfo.IsEntityType(target.GetType(), this.Context.Model))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNotEntity("Remove"));
			}
		}

		// Token: 0x040002AD RID: 685
		private BindingGraph bindingGraph;

		// Token: 0x02000198 RID: 408
		private class UnTrackingInfo
		{
			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06000E84 RID: 3716 RVA: 0x000316E7 File Offset: 0x0002F8E7
			// (set) Token: 0x06000E85 RID: 3717 RVA: 0x000316EF File Offset: 0x0002F8EF
			public object Entity { get; set; }

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x06000E86 RID: 3718 RVA: 0x000316F8 File Offset: 0x0002F8F8
			// (set) Token: 0x06000E87 RID: 3719 RVA: 0x00031700 File Offset: 0x0002F900
			public object Parent { get; set; }

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00031709 File Offset: 0x0002F909
			// (set) Token: 0x06000E89 RID: 3721 RVA: 0x00031711 File Offset: 0x0002F911
			public string ParentProperty { get; set; }
		}
	}
}
