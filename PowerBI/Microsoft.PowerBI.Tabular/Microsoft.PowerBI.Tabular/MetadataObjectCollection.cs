using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000101 RID: 257
	public abstract class MetadataObjectCollection<T, P> : ICollection<T>, IEnumerable<T>, IEnumerable, IMetadataObjectCollection<T>, IMetadataObjectCollection, INotifyObjectIdChange, ITxObject where T : MetadataObject where P : MetadataObject
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x0007AA09 File Offset: 0x00078C09
		public MetadataObjectCollection(ObjectType itemType, P parent)
			: this(itemType, parent, true)
		{
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0007AA14 File Offset: 0x00078C14
		internal MetadataObjectCollection(ObjectType itemType, P parent, bool createBody)
		{
			this.ItemType = itemType;
			this.Parent = parent;
			if (createBody)
			{
				this.body = new MetadataObjectCollection<T, P>.ObjectCollectionBody(this);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0007AA39 File Offset: 0x00078C39
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x0007AA41 File Offset: 0x00078C41
		internal ObjectType ItemType { get; private set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0007AA4A File Offset: 0x00078C4A
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x0007AA52 File Offset: 0x00078C52
		internal virtual MetadataObjectCollection<T, P>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0007AA5B File Offset: 0x00078C5B
		internal virtual MetadataObjectCollection<T, P>.ObjectCollectionBody CreateBody()
		{
			return new MetadataObjectCollection<T, P>.ObjectCollectionBody(this);
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0007AA63 File Offset: 0x00078C63
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x0007AA6B File Offset: 0x00078C6B
		public P Parent { get; private set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0007AA74 File Offset: 0x00078C74
		public int Count
		{
			get
			{
				return this.Body.List.Count;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0007AA86 File Offset: 0x00078C86
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0007AA89 File Offset: 0x00078C89
		public IEnumerator<T> GetEnumerator()
		{
			return this.Body.List.GetEnumerator();
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0007AAA0 File Offset: 0x00078CA0
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.Body.List.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700042A RID: 1066
		internal T this[ObjectId id]
		{
			get
			{
				return this.GetById(id);
			}
		}

		// Token: 0x1700042B RID: 1067
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Body.List.Count)
				{
					throw new ArgumentOutOfRangeException("index", index, string.Empty);
				}
				return this.Body.List[index];
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0007AB00 File Offset: 0x00078D00
		internal T FindById(ObjectId id)
		{
			T t;
			if (!this.Body.MapById.TryGetValue(id, out t))
			{
				return default(T);
			}
			return t;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0007AB30 File Offset: 0x00078D30
		internal T GetById(ObjectId id)
		{
			T t;
			if (!this.Body.MapById.TryGetValue(id, out t))
			{
				throw new ArgumentException(TomSR.Exception_CannotFindItemWithId(id.ToString()), "id");
			}
			return t;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0007AB70 File Offset: 0x00078D70
		internal bool Contains(ObjectId id)
		{
			return this.Body.MapById.ContainsKey(id);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0007AB83 File Offset: 0x00078D83
		public bool Contains(T metadataObject)
		{
			return this.Body.Hashset.Contains(metadataObject);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0007AB96 File Offset: 0x00078D96
		internal int IndexOf(ObjectId id)
		{
			return this.Body.List.IndexOf(this.Body.MapById[id]);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0007ABB9 File Offset: 0x00078DB9
		public int IndexOf(T metadataObject)
		{
			return this.Body.List.IndexOf(metadataObject);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0007ABCC File Offset: 0x00078DCC
		public void Add(T metadataObject)
		{
			this.ValidateCanAdd(metadataObject);
			this.OnItemAdding(metadataObject);
			this.Body.List.Add(metadataObject);
			this.Body.Hashset.Add(metadataObject);
			if (metadataObject.Id != ObjectId.Null)
			{
				this.Body.MapById[metadataObject.Id] = metadataObject;
			}
			this.OnItemAdded(metadataObject);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0007AC44 File Offset: 0x00078E44
		public bool Remove(T metadataObject)
		{
			if (!this.IsItemPresent(metadataObject))
			{
				return false;
			}
			this.ValidateCanRemove(metadataObject);
			this.OnItemRemoving(metadataObject);
			this.Body.List.Remove(metadataObject);
			this.Body.Hashset.Remove(metadataObject);
			if (metadataObject.Id != ObjectId.Null)
			{
				this.Body.MapById.Remove(metadataObject.Id);
			}
			this.OnItemRemoved(metadataObject);
			return true;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0007ACCC File Offset: 0x00078ECC
		internal void Remove(ObjectId id)
		{
			if (id == ObjectId.Null)
			{
				throw new ArgumentNullException("id", "Id cannot be null");
			}
			T t;
			this.CheckCanRemove(id, out t);
			this.OnItemRemoving(t);
			this.Body.List.Remove(t);
			this.Body.Hashset.Remove(t);
			this.Body.MapById.Remove(id);
			this.OnItemRemoved(t);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0007AD44 File Offset: 0x00078F44
		public void Clear()
		{
			foreach (T t in new List<T>(this.Body.List))
			{
				this.Remove(t);
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0007ADA4 File Offset: 0x00078FA4
		void INotifyObjectIdChange.NotifyIdChanging(MetadataObject obj, ObjectId newId)
		{
			Utils.Verify(obj is T);
			if (!this.Contains((T)((object)obj)))
			{
				return;
			}
			if (this.Body.MapById.ContainsKey(newId))
			{
				throw new ArgumentException(string.Format("Collection already contains an object with key '{0}'", newId));
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0007ADF8 File Offset: 0x00078FF8
		void INotifyObjectIdChange.NotifyIdChanged(MetadataObject obj, ObjectId oldId)
		{
			Utils.Verify(obj is T);
			if (!this.Contains((T)((object)obj)))
			{
				return;
			}
			this.Body.MapById.Remove(oldId);
			this.Body.MapById[obj.Id] = (T)((object)obj);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0007AE50 File Offset: 0x00079050
		private bool IsItemPresent(T item)
		{
			return this.Body.Hashset.Contains(item) && this.Body.List.Contains(item) && (item.Id.IsNull || this.Body.MapById.ContainsKey(item.Id));
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0007AEB8 File Offset: 0x000790B8
		internal virtual void ValidateCanAdd(T metadataObject)
		{
			if (metadataObject.Parent != null && metadataObject.Parent != this.Parent)
			{
				throw new ArgumentException("Cannot add object to the collection because the object already belongs to another collection.");
			}
			if (this.Body.Hashset.Contains(metadataObject))
			{
				throw new ArgumentException("Object already exists in the collection");
			}
			if (!metadataObject.Id.IsNull && this.Body.MapById.ContainsKey(metadataObject.Id))
			{
				throw new ArgumentException(string.Format("Object with ID '{0}' already exists in the collection", metadataObject.Id.ToString()));
			}
			this.Parent.ValidateCompatibilityRequirement(metadataObject, null, null);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0007AF88 File Offset: 0x00079188
		internal void ValidateCanRemove(T @object)
		{
			if (!this.Body.Hashset.Contains(@object))
			{
				throw new ArgumentException("Object does not exist in the collection", "metadataObject");
			}
			if (!@object.Id.IsNull && (!this.Body.MapById.ContainsKey(@object.Id) || this.Body.MapById[@object.Id] != @object))
			{
				throw new ArgumentException("Object does not exist in the collection", "metadataObject");
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0007B024 File Offset: 0x00079224
		private void CheckCanRemove(ObjectId id, out T @object)
		{
			if (!this.Body.MapById.TryGetValue(id, out @object))
			{
				throw new ArgumentException(string.Format("Object with ID '{0}' does not exist in the collection", id.ToString()), "id");
			}
			this.ValidateCanRemove(@object);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0007B073 File Offset: 0x00079273
		internal virtual void OnItemAdding(T item)
		{
			ObjectChangeTracker.RegisterObjectAdding(item, this);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0007B081 File Offset: 0x00079281
		internal virtual void OnItemAdded(T item)
		{
			item.Parent = this.Parent;
			ObjectChangeTracker.RegisterObjectAdded(item, this);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0007B0A5 File Offset: 0x000792A5
		internal virtual void OnItemRemoving(T item)
		{
			ObjectChangeTracker.RegisterObjectRemoving(item, this);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0007B0B3 File Offset: 0x000792B3
		internal virtual void OnItemRemoved(T item)
		{
			item.Parent = null;
			ObjectChangeTracker.RegisterObjectRemoved(item, this);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0007B0D0 File Offset: 0x000792D0
		internal virtual void CopyFrom(MetadataObjectCollection<T, P> other, CopyContext copyContext)
		{
			try
			{
				this.copyInProgress = true;
				List<T> list = new List<T>();
				List<T> list2 = new List<T>();
				List<KeyValuePair<T, T>> list3 = new List<KeyValuePair<T, T>>();
				this.CompareWith(other, copyContext, list, list2, list3);
				bool flag = (copyContext.Flags & CopyFlags.IgnoreInferredObjects) == CopyFlags.IgnoreInferredObjects;
				for (int i = 0; i < list.Count; i++)
				{
					if (!flag || !ObjectTreeHelper.IsInferredObject(list[i]))
					{
						this.Remove(list[i]);
					}
				}
				for (int j = 0; j < list2.Count; j++)
				{
					if (!flag || !ObjectTreeHelper.IsInferredObject(list2[j]))
					{
						T t = list2[j].CloneInternal<T>(copyContext);
						t.Parent = null;
						this.Add(t);
					}
				}
				for (int k = 0; k < list3.Count; k++)
				{
					list3[k].Key.CopyFrom(list3[k].Value, copyContext);
				}
			}
			finally
			{
				this.copyInProgress = false;
			}
		}

		// Token: 0x060010FC RID: 4348
		private protected abstract void CompareWith(MetadataObjectCollection<T, P> other, CopyContext context, IList<T> removedItems, IList<T> addedItems, IList<KeyValuePair<T, T>> matchedItems);

		// Token: 0x060010FD RID: 4349 RVA: 0x0007B214 File Offset: 0x00079414
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0007B21C File Offset: 0x0007941C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0007B224 File Offset: 0x00079424
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0007B22C File Offset: 0x0007942C
		int ICollection<T>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0007B234 File Offset: 0x00079434
		bool ICollection<T>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0007B23D File Offset: 0x0007943D
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0007B247 File Offset: 0x00079447
		void ICollection<T>.Clear()
		{
			this.Clear();
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0007B24F File Offset: 0x0007944F
		ObjectType IMetadataObjectCollection.ItemType
		{
			get
			{
				return this.ItemType;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0007B257 File Offset: 0x00079457
		MetadataObject IMetadataObjectCollection.Owner
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0007B264 File Offset: 0x00079464
		IEnumerable<MetadataObject> IMetadataObjectCollection.GetObjects()
		{
			foreach (T t in this.Body.List)
			{
				yield return t;
			}
			List<T>.Enumerator enumerator = default(List<T>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0007B274 File Offset: 0x00079474
		void IMetadataObjectCollection.Remove(MetadataObject obj)
		{
			if (!(obj is T))
			{
				throw TomInternalException.Create("Attempt to remove object of type '{0}' from collection of '{1}'", new object[]
				{
					obj.GetType().Name,
					typeof(T).Name
				});
			}
			this.Remove((T)((object)obj));
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0007B2C8 File Offset: 0x000794C8
		void IMetadataObjectCollection.Add(MetadataObject obj)
		{
			if (!(obj is T))
			{
				throw TomInternalException.Create("Attempt to add object of type '{0}' to collection of '{1}'", new object[]
				{
					obj.GetType().Name,
					typeof(T).Name
				});
			}
			this.Add((T)((object)obj));
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0007B31A File Offset: 0x0007951A
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x0007B322 File Offset: 0x00079522
		ITxObjectBody ITxObject.Body
		{
			get
			{
				return this.Body;
			}
			set
			{
				this.Body = (MetadataObjectCollection<T, P>.ObjectCollectionBody)value;
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0007B330 File Offset: 0x00079530
		ITxObjectBody ITxObject.CreateBody()
		{
			return this.CreateBody();
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0007B338 File Offset: 0x00079538
		void ITxObject.NotifyBodyReverted()
		{
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0007B33A File Offset: 0x0007953A
		T IMetadataObjectCollection<T>.FindById(ObjectId id)
		{
			return this.FindById(id);
		}

		// Token: 0x04000252 RID: 594
		internal bool copyInProgress;

		// Token: 0x04000253 RID: 595
		private MetadataObjectCollection<T, P>.ObjectCollectionBody body;

		// Token: 0x02000304 RID: 772
		internal class ObjectCollectionBody : IMetadataObjectCollectionBody, ITxObjectBody
		{
			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06002428 RID: 9256 RVA: 0x000E3A1F File Offset: 0x000E1C1F
			// (set) Token: 0x06002429 RID: 9257 RVA: 0x000E3A27 File Offset: 0x000E1C27
			internal List<T> List { get; private set; }

			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x0600242A RID: 9258 RVA: 0x000E3A30 File Offset: 0x000E1C30
			// (set) Token: 0x0600242B RID: 9259 RVA: 0x000E3A38 File Offset: 0x000E1C38
			internal Dictionary<ObjectId, T> MapById { get; private set; }

			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x0600242C RID: 9260 RVA: 0x000E3A41 File Offset: 0x000E1C41
			// (set) Token: 0x0600242D RID: 9261 RVA: 0x000E3A49 File Offset: 0x000E1C49
			internal HashSet<T> Hashset { get; private set; }

			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x0600242E RID: 9262 RVA: 0x000E3A52 File Offset: 0x000E1C52
			// (set) Token: 0x0600242F RID: 9263 RVA: 0x000E3A5A File Offset: 0x000E1C5A
			internal MetadataObjectCollection<T, P> Owner { get; private set; }

			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x06002430 RID: 9264 RVA: 0x000E3A63 File Offset: 0x000E1C63
			// (set) Token: 0x06002431 RID: 9265 RVA: 0x000E3A6B File Offset: 0x000E1C6B
			internal MetadataObjectCollection<T, P>.ObjectCollectionBody CreatedFrom { get; set; }

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06002432 RID: 9266 RVA: 0x000E3A74 File Offset: 0x000E1C74
			// (set) Token: 0x06002433 RID: 9267 RVA: 0x000E3A7C File Offset: 0x000E1C7C
			internal TxSavepoint Savepoint { get; set; }

			// Token: 0x06002434 RID: 9268 RVA: 0x000E3A85 File Offset: 0x000E1C85
			internal ObjectCollectionBody(MetadataObjectCollection<T, P> owner)
			{
				this.List = new List<T>();
				this.MapById = new Dictionary<ObjectId, T>();
				this.Hashset = new HashSet<T>();
				this.Owner = owner;
			}

			// Token: 0x06002435 RID: 9269 RVA: 0x000E3AB8 File Offset: 0x000E1CB8
			internal virtual void CopyFrom(MetadataObjectCollection<T, P>.ObjectCollectionBody other, CopyContext context)
			{
				this.List.Clear();
				this.List.AddRange(other.List);
				this.Hashset.Clear();
				foreach (T t in other.Hashset)
				{
					this.Hashset.Add(t);
				}
				this.MapById.Clear();
				foreach (ObjectId objectId in other.MapById.Keys)
				{
					this.MapById[objectId] = other.MapById[objectId];
				}
			}

			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x06002436 RID: 9270 RVA: 0x000E3B9C File Offset: 0x000E1D9C
			ITxObject ITxObjectBody.Owner
			{
				get
				{
					return this.Owner;
				}
			}

			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x06002437 RID: 9271 RVA: 0x000E3BA4 File Offset: 0x000E1DA4
			// (set) Token: 0x06002438 RID: 9272 RVA: 0x000E3BAC File Offset: 0x000E1DAC
			TxSavepoint ITxObjectBody.Savepoint
			{
				get
				{
					return this.Savepoint;
				}
				set
				{
					this.Savepoint = value;
				}
			}

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06002439 RID: 9273 RVA: 0x000E3BB5 File Offset: 0x000E1DB5
			// (set) Token: 0x0600243A RID: 9274 RVA: 0x000E3BBD File Offset: 0x000E1DBD
			ITxObjectBody ITxObjectBody.CreatedFrom
			{
				get
				{
					return this.CreatedFrom;
				}
				set
				{
					this.CreatedFrom = (MetadataObjectCollection<T, P>.ObjectCollectionBody)value;
				}
			}

			// Token: 0x0600243B RID: 9275 RVA: 0x000E3BCB File Offset: 0x000E1DCB
			void ITxObjectBody.CopyFrom(ITxObjectBody other, CopyContext context)
			{
				this.CopyFrom((MetadataObjectCollection<T, P>.ObjectCollectionBody)other, context);
			}

			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x0600243C RID: 9276 RVA: 0x000E3BDA File Offset: 0x000E1DDA
			IEnumerable<MetadataObject> IMetadataObjectCollectionBody.AllObjects
			{
				get
				{
					return this.List.Cast<MetadataObject>();
				}
			}
		}
	}
}
