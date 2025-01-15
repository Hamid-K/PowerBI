using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B4 RID: 180
	internal sealed class BindingGraph
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x00019C88 File Offset: 0x00017E88
		public BindingGraph(BindingObserver observer)
		{
			this.observer = observer;
			this.graph = new BindingGraph.Graph();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00019CA4 File Offset: 0x00017EA4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public bool AddDataServiceCollection(object source, string sourceProperty, object collection, string collectionEntitySet)
		{
			if (this.graph.ExistsVertex(collection))
			{
				return false;
			}
			BindingGraph.Vertex vertex = this.graph.AddVertex(collection);
			vertex.IsDataServiceCollection = true;
			vertex.EntitySet = collectionEntitySet;
			ICollection collection2 = collection as ICollection;
			if (source != null)
			{
				vertex.Parent = this.graph.LookupVertex(source);
				vertex.ParentProperty = sourceProperty;
				this.graph.AddEdge(source, collection, sourceProperty);
				Type collectionEntityType = BindingUtils.GetCollectionEntityType(collection.GetType());
				if (!typeof(INotifyPropertyChanged).IsAssignableFrom(collectionEntityType))
				{
					throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(collectionEntityType));
				}
				typeof(BindingGraph).GetMethod("SetObserver", false, false).MakeGenericMethod(new Type[] { collectionEntityType }).Invoke(this, new object[] { collection2 });
			}
			else
			{
				this.graph.Root = vertex;
			}
			this.AttachDataServiceCollectionNotification(collection);
			foreach (object obj in collection2)
			{
				this.AddEntity(source, sourceProperty, obj, collectionEntitySet, collection);
			}
			return true;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00019DD8 File Offset: 0x00017FD8
		public void AddPrimitiveOrComplexCollection(object source, string sourceProperty, object collection, Type collectionItemType)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(source);
			if (this.graph.LookupVertex(collection) != null)
			{
				throw new InvalidOperationException(Strings.DataBinding_CollectionAssociatedWithMultipleEntities(collection.GetType()));
			}
			BindingGraph.Vertex vertex2 = this.graph.AddVertex(collection);
			vertex2.Parent = vertex;
			vertex2.ParentProperty = sourceProperty;
			vertex2.IsPrimitiveOrEnumOrComplexCollection = true;
			vertex2.PrimitiveOrComplexCollectionItemType = collectionItemType;
			this.graph.AddEdge(source, collection, sourceProperty);
			if (!this.AttachPrimitiveOrComplexCollectionNotification(collection))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyCollectionChangedNotImpl(collection.GetType()));
			}
			if (PrimitiveType.IsKnownNullableType(collectionItemType) || collectionItemType.IsEnum())
			{
				return;
			}
			if (!typeof(INotifyPropertyChanged).IsAssignableFrom(collectionItemType))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(collectionItemType));
			}
			this.AddComplexObjectsFromCollection(collection, (IEnumerable)collection);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00019EAC File Offset: 0x000180AC
		public bool AddEntity(object source, string sourceProperty, object target, string targetEntitySet, object edgeSource)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(edgeSource);
			BindingGraph.Vertex vertex2 = null;
			bool flag = false;
			if (target != null)
			{
				vertex2 = this.graph.LookupVertex(target);
				if (vertex2 == null)
				{
					vertex2 = this.graph.AddVertex(target);
					vertex2.EntitySet = BindingEntityInfo.GetEntitySet(target, targetEntitySet, this.observer.Context.Model);
					if (!this.AttachEntityOrComplexObjectNotification(target))
					{
						throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(target.GetType()));
					}
					flag = true;
				}
				if (this.graph.ExistsEdge(edgeSource, target, vertex.IsDataServiceCollection ? null : sourceProperty))
				{
					throw new InvalidOperationException(Strings.DataBinding_EntityAlreadyInCollection(target.GetType()));
				}
				this.graph.AddEdge(edgeSource, target, vertex.IsDataServiceCollection ? null : sourceProperty);
			}
			if (!vertex.IsDataServiceCollection)
			{
				this.observer.HandleUpdateEntityReference(source, sourceProperty, vertex.EntitySet, target, (vertex2 == null) ? null : vertex2.EntitySet);
			}
			else
			{
				this.observer.HandleAddEntity(source, sourceProperty, (vertex.Parent != null) ? vertex.Parent.EntitySet : null, edgeSource as ICollection, target, vertex2.EntitySet);
			}
			if (flag)
			{
				this.AddFromProperties(target);
			}
			return flag;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00019FD8 File Offset: 0x000181D8
		public void RemoveDataServiceCollectionItem(object item, object parent, string parentProperty)
		{
			if (this.graph.LookupVertex(item) == null)
			{
				return;
			}
			if (parentProperty != null)
			{
				BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo = BindingEntityInfo.GetObservableProperties(parent.GetType(), this.observer.Context.Model).Single((BindingEntityInfo.BindingPropertyInfo p) => p.PropertyInfo.PropertyName == parentProperty);
				parent = bindingPropertyInfo.PropertyInfo.GetValue(parent);
			}
			object obj = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			this.GetDataServiceCollectionInfo(parent, out obj, out text, out text2, out text3);
			text3 = BindingEntityInfo.GetEntitySet(item, text3, this.observer.Context.Model);
			this.observer.HandleDeleteEntity(obj, text, text2, parent as ICollection, item, text3);
			this.graph.RemoveEdge(parent, item, null);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001A0A4 File Offset: 0x000182A4
		public void RemoveComplexTypeCollectionItem(object item, object collection)
		{
			if (item == null)
			{
				return;
			}
			if (this.graph.LookupVertex(item) == null)
			{
				return;
			}
			this.graph.RemoveEdge(collection, item, null);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001A0D4 File Offset: 0x000182D4
		public void RemoveCollection(object source)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(source);
			foreach (BindingGraph.Edge edge in vertex.OutgoingEdges.ToList<BindingGraph.Edge>())
			{
				this.graph.RemoveEdge(source, edge.Target.Item, null);
			}
			this.RemoveUnreachableVertices();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001A150 File Offset: 0x00018350
		public void RemoveRelation(object source, string relation)
		{
			BindingGraph.Edge edge = this.graph.LookupVertex(source).OutgoingEdges.SingleOrDefault((BindingGraph.Edge e) => e.Source.Item == source && e.Label == relation);
			if (edge != null)
			{
				this.graph.RemoveEdge(edge.Source.Item, edge.Target.Item, edge.Label);
			}
			this.RemoveUnreachableVertices();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001A1CC File Offset: 0x000183CC
		public void RemoveNonTrackedEntities()
		{
			foreach (object obj in this.graph.Select((object o) => !this.observer.IsContextTrackingEntity(o) && BindingEntityInfo.IsEntityType(o.GetType(), this.observer.Context.Model)))
			{
				this.graph.ClearEdgesForVertex(this.graph.LookupVertex(obj));
			}
			this.RemoveUnreachableVertices();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001A240 File Offset: 0x00018440
		public IEnumerable<object> GetDataServiceCollectionItems(object collection)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(collection);
			foreach (BindingGraph.Edge edge in vertex.OutgoingEdges.ToList<BindingGraph.Edge>())
			{
				yield return edge.Target.Item;
			}
			List<BindingGraph.Edge>.Enumerator enumerator = default(List<BindingGraph.Edge>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A257 File Offset: 0x00018457
		public void Reset()
		{
			this.graph.Reset(new Action<object>(this.DetachNotifications));
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A270 File Offset: 0x00018470
		public void RemoveUnreachableVertices()
		{
			this.graph.RemoveUnreachableVertices(new Action<object>(this.DetachNotifications));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A289 File Offset: 0x00018489
		public void GetDataServiceCollectionInfo(object collection, out object source, out string sourceProperty, out string sourceEntitySet, out string targetEntitySet)
		{
			this.graph.LookupVertex(collection).GetDataServiceCollectionInfo(out source, out sourceProperty, out sourceEntitySet, out targetEntitySet);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001A2A2 File Offset: 0x000184A2
		public void GetPrimitiveOrComplexCollectionInfo(object collection, out object source, out string sourceProperty, out Type collectionItemType)
		{
			this.graph.LookupVertex(collection).GetPrimitiveOrComplexCollectionInfo(out source, out sourceProperty, out collectionItemType);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001A2BC File Offset: 0x000184BC
		public void GetAncestorEntityForComplexProperty(ref object entity, ref string propertyName, ref object propertyValue)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(entity);
			while (vertex.IsComplex || vertex.IsPrimitiveOrEnumOrComplexCollection)
			{
				propertyName = vertex.IncomingEdges[0].Label;
				propertyValue = vertex.Item;
				entity = vertex.Parent.Item;
				vertex = vertex.Parent;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A318 File Offset: 0x00018518
		public void AddComplexObject(object source, string sourceProperty, object target)
		{
			if (this.graph.LookupVertex(target) != null)
			{
				throw new InvalidOperationException(Strings.DataBinding_ComplexObjectAssociatedWithMultipleEntities(target.GetType()));
			}
			BindingGraph.Vertex vertex = this.graph.LookupVertex(source);
			BindingGraph.Vertex vertex2 = this.graph.AddVertex(target);
			vertex2.Parent = vertex;
			vertex2.IsComplex = true;
			if (!this.AttachEntityOrComplexObjectNotification(target))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(target.GetType()));
			}
			this.graph.AddEdge(source, target, sourceProperty);
			this.AddFromProperties(target);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001A3A0 File Offset: 0x000185A0
		public void AddComplexObjectsFromCollection(object collection, IEnumerable collectionItems)
		{
			foreach (object obj in collectionItems)
			{
				if (obj != null)
				{
					this.AddComplexObject(collection, null, obj);
				}
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001A3F4 File Offset: 0x000185F4
		private void AddFromProperties(object entity)
		{
			foreach (BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo in BindingEntityInfo.GetObservableProperties(entity.GetType(), this.observer.Context.Model))
			{
				object value = bindingPropertyInfo.PropertyInfo.GetValue(entity);
				if (value != null)
				{
					switch (bindingPropertyInfo.PropertyKind)
					{
					case BindingPropertyKind.BindingPropertyKindEntity:
						this.AddEntity(entity, bindingPropertyInfo.PropertyInfo.PropertyName, value, null, entity);
						break;
					case BindingPropertyKind.BindingPropertyKindDataServiceCollection:
						this.AddDataServiceCollection(entity, bindingPropertyInfo.PropertyInfo.PropertyName, value, null);
						break;
					case BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection:
						this.AddPrimitiveOrComplexCollection(entity, bindingPropertyInfo.PropertyInfo.PropertyName, value, bindingPropertyInfo.PropertyInfo.PrimitiveOrComplexCollectionItemType);
						break;
					default:
						this.AddComplexObject(entity, bindingPropertyInfo.PropertyInfo.PropertyName, value);
						break;
					}
				}
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001A4E4 File Offset: 0x000186E4
		private void AttachDataServiceCollectionNotification(object target)
		{
			INotifyCollectionChanged notifyCollectionChanged = target as INotifyCollectionChanged;
			notifyCollectionChanged.CollectionChanged -= this.observer.OnDataServiceCollectionChanged;
			notifyCollectionChanged.CollectionChanged += this.observer.OnDataServiceCollectionChanged;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A528 File Offset: 0x00018728
		private bool AttachPrimitiveOrComplexCollectionNotification(object collection)
		{
			INotifyCollectionChanged notifyCollectionChanged = collection as INotifyCollectionChanged;
			if (notifyCollectionChanged != null)
			{
				notifyCollectionChanged.CollectionChanged -= this.observer.OnPrimitiveOrComplexCollectionChanged;
				notifyCollectionChanged.CollectionChanged += this.observer.OnPrimitiveOrComplexCollectionChanged;
				return true;
			}
			return false;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001A570 File Offset: 0x00018770
		private bool AttachEntityOrComplexObjectNotification(object target)
		{
			INotifyPropertyChanged notifyPropertyChanged = target as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.observer.OnPropertyChanged;
				notifyPropertyChanged.PropertyChanged += this.observer.OnPropertyChanged;
				return true;
			}
			return false;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001A5B8 File Offset: 0x000187B8
		private void DetachNotifications(object target)
		{
			this.DetachCollectionNotifications(target);
			INotifyPropertyChanged notifyPropertyChanged = target as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.observer.OnPropertyChanged;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001A5F0 File Offset: 0x000187F0
		private void DetachCollectionNotifications(object target)
		{
			INotifyCollectionChanged notifyCollectionChanged = target as INotifyCollectionChanged;
			if (notifyCollectionChanged != null)
			{
				notifyCollectionChanged.CollectionChanged -= this.observer.OnDataServiceCollectionChanged;
				notifyCollectionChanged.CollectionChanged -= this.observer.OnPrimitiveOrComplexCollectionChanged;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001A638 File Offset: 0x00018838
		private void SetObserver<T>(ICollection collection)
		{
			DataServiceCollection<T> dataServiceCollection = collection as DataServiceCollection<T>;
			dataServiceCollection.Observer = this.observer;
		}

		// Token: 0x040002AB RID: 683
		private BindingObserver observer;

		// Token: 0x040002AC RID: 684
		private BindingGraph.Graph graph;

		// Token: 0x02000192 RID: 402
		internal sealed class Graph
		{
			// Token: 0x06000E49 RID: 3657 RVA: 0x00030EFA File Offset: 0x0002F0FA
			public Graph()
			{
				this.vertices = new Dictionary<object, BindingGraph.Vertex>(ReferenceEqualityComparer<object>.Instance);
			}

			// Token: 0x1700036F RID: 879
			// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00030F12 File Offset: 0x0002F112
			// (set) Token: 0x06000E4B RID: 3659 RVA: 0x00030F1A File Offset: 0x0002F11A
			public BindingGraph.Vertex Root
			{
				get
				{
					return this.root;
				}
				set
				{
					this.root = value;
				}
			}

			// Token: 0x06000E4C RID: 3660 RVA: 0x00030F24 File Offset: 0x0002F124
			public BindingGraph.Vertex AddVertex(object item)
			{
				BindingGraph.Vertex vertex = new BindingGraph.Vertex(item);
				this.vertices.Add(item, vertex);
				return vertex;
			}

			// Token: 0x06000E4D RID: 3661 RVA: 0x00030F48 File Offset: 0x0002F148
			public void ClearEdgesForVertex(BindingGraph.Vertex v)
			{
				foreach (BindingGraph.Edge edge in v.OutgoingEdges.Concat(v.IncomingEdges).ToList<BindingGraph.Edge>())
				{
					this.RemoveEdge(edge.Source.Item, edge.Target.Item, edge.Label);
				}
			}

			// Token: 0x06000E4E RID: 3662 RVA: 0x00030FC8 File Offset: 0x0002F1C8
			public bool ExistsVertex(object item)
			{
				BindingGraph.Vertex vertex;
				return this.vertices.TryGetValue(item, out vertex);
			}

			// Token: 0x06000E4F RID: 3663 RVA: 0x00030FE4 File Offset: 0x0002F1E4
			public BindingGraph.Vertex LookupVertex(object item)
			{
				BindingGraph.Vertex vertex;
				this.vertices.TryGetValue(item, out vertex);
				return vertex;
			}

			// Token: 0x06000E50 RID: 3664 RVA: 0x00031004 File Offset: 0x0002F204
			public BindingGraph.Edge AddEdge(object source, object target, string label)
			{
				BindingGraph.Vertex vertex = this.vertices[source];
				BindingGraph.Vertex vertex2 = this.vertices[target];
				BindingGraph.Edge edge = new BindingGraph.Edge
				{
					Source = vertex,
					Target = vertex2,
					Label = label
				};
				vertex.OutgoingEdges.Add(edge);
				vertex2.IncomingEdges.Add(edge);
				return edge;
			}

			// Token: 0x06000E51 RID: 3665 RVA: 0x00031060 File Offset: 0x0002F260
			public void RemoveEdge(object source, object target, string label)
			{
				BindingGraph.Vertex vertex = this.vertices[source];
				BindingGraph.Vertex vertex2 = this.vertices[target];
				BindingGraph.Edge edge = new BindingGraph.Edge
				{
					Source = vertex,
					Target = vertex2,
					Label = label
				};
				vertex.OutgoingEdges.Remove(edge);
				vertex2.IncomingEdges.Remove(edge);
			}

			// Token: 0x06000E52 RID: 3666 RVA: 0x000310BC File Offset: 0x0002F2BC
			public bool ExistsEdge(object source, object target, string label)
			{
				BindingGraph.Edge e = new BindingGraph.Edge
				{
					Source = this.vertices[source],
					Target = this.vertices[target],
					Label = label
				};
				return this.vertices[source].OutgoingEdges.Any((BindingGraph.Edge r) => r.Equals(e));
			}

			// Token: 0x06000E53 RID: 3667 RVA: 0x00031127 File Offset: 0x0002F327
			public IList<object> Select(Func<object, bool> filter)
			{
				return this.vertices.Keys.Where(filter).ToList<object>();
			}

			// Token: 0x06000E54 RID: 3668 RVA: 0x00031140 File Offset: 0x0002F340
			public void Reset(Action<object> action)
			{
				foreach (object obj in this.vertices.Keys)
				{
					action(obj);
				}
				this.vertices.Clear();
			}

			// Token: 0x06000E55 RID: 3669 RVA: 0x000311A4 File Offset: 0x0002F3A4
			public void RemoveUnreachableVertices(Action<object> detachAction)
			{
				try
				{
					foreach (BindingGraph.Vertex vertex in this.UnreachableVertices())
					{
						this.ClearEdgesForVertex(vertex);
						detachAction(vertex.Item);
						this.vertices.Remove(vertex.Item);
					}
				}
				finally
				{
					foreach (BindingGraph.Vertex vertex2 in this.vertices.Values)
					{
						vertex2.Color = VertexColor.White;
					}
				}
			}

			// Token: 0x06000E56 RID: 3670 RVA: 0x00031264 File Offset: 0x0002F464
			private IEnumerable<BindingGraph.Vertex> UnreachableVertices()
			{
				if (this.vertices.Count == 0)
				{
					return Enumerable.Empty<BindingGraph.Vertex>();
				}
				Queue<BindingGraph.Vertex> queue = new Queue<BindingGraph.Vertex>();
				this.Root.Color = VertexColor.Gray;
				queue.Enqueue(this.Root);
				while (queue.Count != 0)
				{
					BindingGraph.Vertex vertex = queue.Dequeue();
					foreach (BindingGraph.Edge edge in vertex.OutgoingEdges)
					{
						if (edge.Target.Color == VertexColor.White)
						{
							edge.Target.Color = VertexColor.Gray;
							queue.Enqueue(edge.Target);
						}
					}
					vertex.Color = VertexColor.Black;
				}
				return this.vertices.Values.Where((BindingGraph.Vertex v) => v.Color == VertexColor.White).ToList<BindingGraph.Vertex>();
			}

			// Token: 0x04000779 RID: 1913
			private Dictionary<object, BindingGraph.Vertex> vertices;

			// Token: 0x0400077A RID: 1914
			private BindingGraph.Vertex root;
		}

		// Token: 0x02000193 RID: 403
		internal sealed class Vertex
		{
			// Token: 0x06000E57 RID: 3671 RVA: 0x0003134C File Offset: 0x0002F54C
			public Vertex(object item)
			{
				this.Item = item;
				this.Color = VertexColor.White;
			}

			// Token: 0x17000370 RID: 880
			// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00031362 File Offset: 0x0002F562
			// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0003136A File Offset: 0x0002F56A
			public object Item { get; private set; }

			// Token: 0x17000371 RID: 881
			// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00031373 File Offset: 0x0002F573
			// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0003137B File Offset: 0x0002F57B
			public string EntitySet { get; set; }

			// Token: 0x17000372 RID: 882
			// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00031384 File Offset: 0x0002F584
			// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0003138C File Offset: 0x0002F58C
			public bool IsDataServiceCollection { get; set; }

			// Token: 0x17000373 RID: 883
			// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00031395 File Offset: 0x0002F595
			// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0003139D File Offset: 0x0002F59D
			public bool IsComplex { get; set; }

			// Token: 0x17000374 RID: 884
			// (get) Token: 0x06000E60 RID: 3680 RVA: 0x000313A6 File Offset: 0x0002F5A6
			// (set) Token: 0x06000E61 RID: 3681 RVA: 0x000313AE File Offset: 0x0002F5AE
			public bool IsPrimitiveOrEnumOrComplexCollection { get; set; }

			// Token: 0x17000375 RID: 885
			// (get) Token: 0x06000E62 RID: 3682 RVA: 0x000313B7 File Offset: 0x0002F5B7
			// (set) Token: 0x06000E63 RID: 3683 RVA: 0x000313BF File Offset: 0x0002F5BF
			public Type PrimitiveOrComplexCollectionItemType { get; set; }

			// Token: 0x17000376 RID: 886
			// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000313C8 File Offset: 0x0002F5C8
			// (set) Token: 0x06000E65 RID: 3685 RVA: 0x000313D0 File Offset: 0x0002F5D0
			public BindingGraph.Vertex Parent { get; set; }

			// Token: 0x17000377 RID: 887
			// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000313D9 File Offset: 0x0002F5D9
			// (set) Token: 0x06000E67 RID: 3687 RVA: 0x000313E1 File Offset: 0x0002F5E1
			public string ParentProperty { get; set; }

			// Token: 0x17000378 RID: 888
			// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000313EA File Offset: 0x0002F5EA
			public bool IsRootDataServiceCollection
			{
				get
				{
					return this.IsDataServiceCollection && this.Parent == null;
				}
			}

			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000313FF File Offset: 0x0002F5FF
			// (set) Token: 0x06000E6A RID: 3690 RVA: 0x00031407 File Offset: 0x0002F607
			public VertexColor Color { get; set; }

			// Token: 0x1700037A RID: 890
			// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00031410 File Offset: 0x0002F610
			public IList<BindingGraph.Edge> IncomingEdges
			{
				get
				{
					if (this.incomingEdges == null)
					{
						this.incomingEdges = new List<BindingGraph.Edge>();
					}
					return this.incomingEdges;
				}
			}

			// Token: 0x1700037B RID: 891
			// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003142B File Offset: 0x0002F62B
			public IList<BindingGraph.Edge> OutgoingEdges
			{
				get
				{
					if (this.outgoingEdges == null)
					{
						this.outgoingEdges = new List<BindingGraph.Edge>();
					}
					return this.outgoingEdges;
				}
			}

			// Token: 0x06000E6D RID: 3693 RVA: 0x00031446 File Offset: 0x0002F646
			public void GetDataServiceCollectionInfo(out object source, out string sourceProperty, out string sourceEntitySet, out string targetEntitySet)
			{
				if (!this.IsRootDataServiceCollection)
				{
					source = this.Parent.Item;
					sourceProperty = this.ParentProperty;
					sourceEntitySet = this.Parent.EntitySet;
				}
				else
				{
					source = null;
					sourceProperty = null;
					sourceEntitySet = null;
				}
				targetEntitySet = this.EntitySet;
			}

			// Token: 0x06000E6E RID: 3694 RVA: 0x00031486 File Offset: 0x0002F686
			public void GetPrimitiveOrComplexCollectionInfo(out object source, out string sourceProperty, out Type collectionItemType)
			{
				source = this.Parent.Item;
				sourceProperty = this.ParentProperty;
				collectionItemType = this.PrimitiveOrComplexCollectionItemType;
			}

			// Token: 0x0400077B RID: 1915
			private List<BindingGraph.Edge> incomingEdges;

			// Token: 0x0400077C RID: 1916
			private List<BindingGraph.Edge> outgoingEdges;
		}

		// Token: 0x02000194 RID: 404
		internal sealed class Edge : IEquatable<BindingGraph.Edge>
		{
			// Token: 0x1700037C RID: 892
			// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000314A5 File Offset: 0x0002F6A5
			// (set) Token: 0x06000E70 RID: 3696 RVA: 0x000314AD File Offset: 0x0002F6AD
			public BindingGraph.Vertex Source { get; set; }

			// Token: 0x1700037D RID: 893
			// (get) Token: 0x06000E71 RID: 3697 RVA: 0x000314B6 File Offset: 0x0002F6B6
			// (set) Token: 0x06000E72 RID: 3698 RVA: 0x000314BE File Offset: 0x0002F6BE
			public BindingGraph.Vertex Target { get; set; }

			// Token: 0x1700037E RID: 894
			// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000314C7 File Offset: 0x0002F6C7
			// (set) Token: 0x06000E74 RID: 3700 RVA: 0x000314CF File Offset: 0x0002F6CF
			public string Label { get; set; }

			// Token: 0x06000E75 RID: 3701 RVA: 0x000314D8 File Offset: 0x0002F6D8
			public bool Equals(BindingGraph.Edge other)
			{
				return other != null && this.Source == other.Source && this.Target == other.Target && this.Label == other.Label;
			}
		}
	}
}
