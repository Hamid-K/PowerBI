using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000105 RID: 261
	public abstract class NamedMetadataObjectCollection<T, P> : MetadataObjectCollection<T, P>, INamedMetadataObjectCollection, IMetadataObjectCollection, INotifyObjectIdChange, ITxObject, INotifyObjectNameChange where T : NamedMetadataObject where P : MetadataObject
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0007B833 File Offset: 0x00079A33
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x0007B83B File Offset: 0x00079A3B
		internal override MetadataObjectCollection<T, P>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (NamedMetadataObjectCollection<T, P>.ObjectCollectionBody)value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0007B849 File Offset: 0x00079A49
		private NamedMetadataObjectCollection<T, P>.ObjectCollectionBody _Body
		{
			get
			{
				return (NamedMetadataObjectCollection<T, P>.ObjectCollectionBody)this.Body;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0007B856 File Offset: 0x00079A56
		internal override MetadataObjectCollection<T, P>.ObjectCollectionBody CreateBody()
		{
			return new NamedMetadataObjectCollection<T, P>.ObjectCollectionBody(this, this._Body.MapByName.Comparer);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0007B870 File Offset: 0x00079A70
		internal override void CopyFrom(MetadataObjectCollection<T, P> other, CopyContext copyContext)
		{
			try
			{
				if (!this._Body.CanUpdateNameComaprison(other.Parent.GetNamesComparer()))
				{
					throw new InvalidOperationException(TomSR.Exception_CannotCopyCollectionCultureConflict);
				}
				this.rebuildMapByNameAfterCopy = false;
				base.CopyFrom(other, copyContext);
			}
			finally
			{
				if (this.rebuildMapByNameAfterCopy)
				{
					this._Body.RebuildMapByName();
				}
				this.rebuildMapByNameAfterCopy = false;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0007B8E4 File Offset: 0x00079AE4
		private protected override void CompareWith(MetadataObjectCollection<T, P> other, CopyContext context, IList<T> removedItems, IList<T> addedItems, IList<KeyValuePair<T, T>> matchedItems)
		{
			NamedMetadataObjectCollection<T, P> namedMetadataObjectCollection = (NamedMetadataObjectCollection<T, P>)other;
			bool flag = (context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds;
			if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
			{
				foreach (T t in this)
				{
					if (flag && !t.Id.IsNull)
					{
						if (!namedMetadataObjectCollection.Contains(t.Id))
						{
							removedItems.Add(t);
						}
					}
					else if (!namedMetadataObjectCollection.Contains(t.Name))
					{
						removedItems.Add(t);
					}
				}
			}
			foreach (T t2 in namedMetadataObjectCollection)
			{
				Utils.Verify(!flag || !t2.Id.IsNull, "If we compare collections using Ids, then objects in 'other' collection must have Ids");
				T t3 = (flag ? base.FindById(t2.Id) : default(T));
				if (t3 == null)
				{
					t3 = this.Find(t2.Name);
					if (t3 != null && flag && !t3.Id.IsNull)
					{
						t3 = default(T);
					}
				}
				if (t3 != null)
				{
					matchedItems.Add(new KeyValuePair<T, T>(t3, t2));
				}
				else
				{
					addedItems.Add(t2);
				}
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0007BA84 File Offset: 0x00079C84
		private protected NamedMetadataObjectCollection(ObjectType itemType, P parent, IEqualityComparer<string> comparer, bool createBody = true)
			: base(itemType, parent, false)
		{
			if (createBody)
			{
				this.body = new NamedMetadataObjectCollection<T, P>.ObjectCollectionBody(this, comparer);
			}
		}

		// Token: 0x1700043A RID: 1082
		public T this[string name]
		{
			get
			{
				T t;
				if (!this._Body.MapByName.TryGetValue(name, out t))
				{
					throw new ArgumentException(TomSR.Exception_ObjectWithNameNotExistInCollection(ClientHostingManager.MarkAsRestrictedInformation(name, InfoRestrictionType.CCON)), "name");
				}
				return t;
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0007BADC File Offset: 0x00079CDC
		public void Remove(string name)
		{
			if (this.copyInProgress)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotRemoveItemByNameDuringCopy(ClientHostingManager.MarkAsRestrictedInformation(name, InfoRestrictionType.CCON)));
			}
			T t;
			this.CheckCanRemove(name, out t);
			this.OnItemRemoving(t);
			this.Body.List.Remove(t);
			this.Body.Hashset.Remove(t);
			this._Body.MapByName.Remove(name);
			if (t.Id != ObjectId.Null)
			{
				this.Body.MapById.Remove(t.Id);
			}
			this.OnItemRemoved(t);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0007BB83 File Offset: 0x00079D83
		private void CheckCanRemove(string name, out T @object)
		{
			if (!this._Body.MapByName.TryGetValue(name, out @object))
			{
				throw new ArgumentException(TomSR.Exception_ObjectWithNameNotExistInCollection(ClientHostingManager.MarkAsRestrictedInformation(name, InfoRestrictionType.CCON)), "name");
			}
			base.ValidateCanRemove(@object);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0007BBBC File Offset: 0x00079DBC
		public T Find(string name)
		{
			T t;
			if (!this._Body.MapByName.TryGetValue(name, out t))
			{
				return default(T);
			}
			return t;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0007BBE9 File Offset: 0x00079DE9
		public bool Contains(string name)
		{
			return this._Body.MapByName.ContainsKey(name);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0007BBFC File Offset: 0x00079DFC
		public string GetNewName()
		{
			return this.GetNewName(Utils.GetDefaultNameForObjectType(base.ItemType));
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0007BC10 File Offset: 0x00079E10
		public string GetNewName(string namePrefix)
		{
			if (string.IsNullOrEmpty(namePrefix))
			{
				throw new ArgumentNullException("namePrefix");
			}
			namePrefix = Utils.GetSyntacticallyValidName(namePrefix, base.ItemType);
			if (!this.Contains(namePrefix))
			{
				return namePrefix;
			}
			StringGenerator stringGenerator = new StringGenerator(namePrefix, Utils.GetMaxNameLengthForObjectType(base.ItemType));
			for (string text = stringGenerator.Next; text != null; text = stringGenerator.Next)
			{
				string text2;
				if (Utils.IsSyntacticallyValidName(text, base.ItemType, out text2) && !this.Contains(text))
				{
					return text;
				}
			}
			throw TomInternalException.CreateWithRestrictedInfo("Could not generate new name for object type '{0}' and prefix '{1}'", new KeyValuePair<InfoRestrictionType, object>[]
			{
				new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.Unrestricted, base.ItemType.ToString()),
				new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.CCON, namePrefix)
			});
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0007BCC8 File Offset: 0x00079EC8
		public bool ContainsName(string name)
		{
			return this._Body.MapByName.ContainsKey(name);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0007BCDC File Offset: 0x00079EDC
		void INotifyObjectNameChange.NotifyNameChanging(NamedMetadataObject obj, string newName)
		{
			Utils.Verify(obj is T);
			if (!base.Contains((T)((object)obj)))
			{
				return;
			}
			if (!this.rebuildMapByNameAfterCopy && !this._Body.MapByName.Comparer.Equals(obj.Name, newName) && this._Body.MapByName.ContainsKey(newName))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithName(ClientHostingManager.MarkAsRestrictedInformation(newName, InfoRestrictionType.CCON)));
				}
				this.rebuildMapByNameAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
			foreach (MetadataObject metadataObject in obj.GetNameLinkedObjects(null))
			{
				Utils.Verify(metadataObject.ParentCollection != null);
				ObjectChangeTracker.RegisterCollectionChanging(metadataObject.ParentCollection);
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0007BDBC File Offset: 0x00079FBC
		void INotifyObjectNameChange.NotifyNameChanged(NamedMetadataObject obj, string oldName)
		{
			Utils.Verify(obj is T);
			if (!base.Contains((T)((object)obj)))
			{
				return;
			}
			if (!this.rebuildMapByNameAfterCopy)
			{
				this._Body.MapByName.Remove(oldName);
				this._Body.MapByName[obj.Name] = (T)((object)obj);
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0007BE1C File Offset: 0x0007A01C
		MetadataObject INamedMetadataObjectCollection.Find(string name)
		{
			return this.Find(name);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0007BE2A File Offset: 0x0007A02A
		IEqualityComparer<string> INamedMetadataObjectCollection.GetNamesComparer()
		{
			return this._Body.MapByName.Comparer;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0007BE3C File Offset: 0x0007A03C
		bool INamedMetadataObjectCollection.CanUpdateCultureInfo(IEqualityComparer<string> comparer)
		{
			return this._Body.CanUpdateNameComaprison(comparer);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0007BE4A File Offset: 0x0007A04A
		void INamedMetadataObjectCollection.UpdateCultureInfo(IEqualityComparer<string> comparer)
		{
			this._Body.UpdateNameComaprison(comparer);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0007BE58 File Offset: 0x0007A058
		internal override void ValidateCanAdd(T item)
		{
			if (!this.rebuildMapByNameAfterCopy && item.Name != null && this._Body.MapByName.ContainsKey(item.Name))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_ItemAlreadyPresentInCollection(ClientHostingManager.MarkAsRestrictedInformation(item.Name, InfoRestrictionType.CCON)));
				}
				this.rebuildMapByNameAfterCopy = true;
			}
			base.ValidateCanAdd(item);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0007BECC File Offset: 0x0007A0CC
		internal override void OnItemAdding(T item)
		{
			if (string.IsNullOrEmpty(item.Name))
			{
				if (item is CalculatedTableColumn)
				{
					((CalculatedTableColumn)((object)item)).InferredName = this.GetNewName();
				}
				else
				{
					item.Name = this.GetNewName();
				}
				if (this._Body.MapByName.ContainsKey(item.Name))
				{
					throw new ArgumentException(TomSR.Exception_ItemAlreadyPresentInCollection(ClientHostingManager.MarkAsRestrictedInformation(item.Name, InfoRestrictionType.CCON)));
				}
			}
			base.OnItemAdding(item);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0007BF61 File Offset: 0x0007A161
		internal override void OnItemAdded(T item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildMapByNameAfterCopy)
			{
				this._Body.MapByName[item.Name] = item;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0007BF8E File Offset: 0x0007A18E
		internal override void OnItemRemoved(T item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildMapByNameAfterCopy)
			{
				this._Body.MapByName.Remove(item.Name);
			}
		}

		// Token: 0x0400025A RID: 602
		private NamedMetadataObjectCollection<T, P>.ObjectCollectionBody body;

		// Token: 0x0400025B RID: 603
		private bool rebuildMapByNameAfterCopy;

		// Token: 0x02000307 RID: 775
		internal new class ObjectCollectionBody : MetadataObjectCollection<T, P>.ObjectCollectionBody
		{
			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x0600244E RID: 9294 RVA: 0x000E3E5F File Offset: 0x000E205F
			// (set) Token: 0x0600244F RID: 9295 RVA: 0x000E3E67 File Offset: 0x000E2067
			internal Dictionary<string, T> MapByName { get; private set; }

			// Token: 0x06002450 RID: 9296 RVA: 0x000E3E70 File Offset: 0x000E2070
			public ObjectCollectionBody(NamedMetadataObjectCollection<T, P> owner, IEqualityComparer<string> comparer)
				: base(owner)
			{
				this.MapByName = new Dictionary<string, T>(comparer);
			}

			// Token: 0x06002451 RID: 9297 RVA: 0x000E3E88 File Offset: 0x000E2088
			internal void CopyFrom(NamedMetadataObjectCollection<T, P>.ObjectCollectionBody other, CopyContext context)
			{
				if (!this.CanUpdateNameComaprison(other.MapByName.Comparer))
				{
					throw new InvalidOperationException(TomSR.Exception_CannotCopyCollectionCultureConflict);
				}
				base.CopyFrom(other, context);
				this.MapByName = new Dictionary<string, T>(other.MapByName, this.MapByName.Comparer);
			}

			// Token: 0x06002452 RID: 9298 RVA: 0x000E3ED8 File Offset: 0x000E20D8
			internal bool CanUpdateNameComaprison(IEqualityComparer<string> comparer)
			{
				if (this.MapByName.Comparer == comparer)
				{
					return true;
				}
				HashSet<string> hashSet = new HashSet<string>(comparer);
				foreach (string text in this.MapByName.Keys)
				{
					if (hashSet.Contains(text))
					{
						return false;
					}
					hashSet.Add(text);
				}
				return true;
			}

			// Token: 0x06002453 RID: 9299 RVA: 0x000E3F58 File Offset: 0x000E2158
			internal void UpdateNameComaprison(IEqualityComparer<string> comparer)
			{
				if (this.MapByName.Comparer != comparer)
				{
					this.MapByName = new Dictionary<string, T>(this.MapByName, comparer);
				}
			}

			// Token: 0x06002454 RID: 9300 RVA: 0x000E3F7A File Offset: 0x000E217A
			internal override void CopyFrom(MetadataObjectCollection<T, P>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((NamedMetadataObjectCollection<T, P>.ObjectCollectionBody)other, context);
			}

			// Token: 0x06002455 RID: 9301 RVA: 0x000E3F8C File Offset: 0x000E218C
			internal void RebuildMapByName()
			{
				this.MapByName.Clear();
				foreach (T t in base.List)
				{
					this.MapByName[t.Name] = t;
				}
			}
		}
	}
}
