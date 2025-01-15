using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001FD RID: 509
	internal static class ObjectChangeTracker
	{
		// Token: 0x06001CFF RID: 7423 RVA: 0x000C6BA8 File Offset: 0x000C4DA8
		public static void RegisterPropertyChanging(MetadataObject obj, string propName, Type type, object oldValue, object newValue)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.IsRemoved)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectRemovedCannotBeModifiedAttachedToModel);
			}
			if (!(propName == "Name"))
			{
				if (!(propName == "Id"))
				{
					if (propName == "LineageTag")
					{
						ObjectChangeTracker.HandleLineageTagChanging((IMetadataObjectWithLineage)obj, (string)newValue);
					}
				}
				else
				{
					ObjectChangeTracker.HandleIdChanging(obj, (ObjectId)newValue);
				}
			}
			else
			{
				ObjectChangeTracker.HandleNameChanging((NamedMetadataObject)obj, (string)newValue);
			}
			Model model = obj.Model;
			if (model != null && model.TxManager != null)
			{
				ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
				ObjectChangeTracker.EnsureNoMergePartitionsInSavepoint(model);
				ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
				ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(obj);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000C6C58 File Offset: 0x000C4E58
		public static void RegisterPropertyChanged(MetadataObject obj, string propName, Type type, object oldValue, object newValue)
		{
			if (obj == null)
			{
				return;
			}
			if (propName == "Name")
			{
				ObjectChangeTracker.HandleNameChanged((NamedMetadataObject)obj, (string)oldValue);
				return;
			}
			if (propName == "Id")
			{
				ObjectChangeTracker.HandleIdChanged(obj, (ObjectId)oldValue);
				return;
			}
			if (!(propName == "LineageTag"))
			{
				return;
			}
			ObjectChangeTracker.HandleLineageTagChanged((IMetadataObjectWithLineage)obj, (string)oldValue);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000C6CC4 File Offset: 0x000C4EC4
		public static void RegisterUpcomingPropertyChange(MetadataObject obj)
		{
			Model model = obj.Model;
			if (model != null && model.TxManager != null)
			{
				ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
				ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(obj);
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000C6CF0 File Offset: 0x000C4EF0
		public static void RegisterObjectAdding(MetadataObject obj, MetadataObject parentObject)
		{
			ObjectChangeTracker.RegisterObjectAddingImpl(obj, parentObject, null);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000C6CFA File Offset: 0x000C4EFA
		public static void RegisterObjectAdded(MetadataObject obj, MetadataObject parentObject)
		{
			ObjectChangeTracker.RegisterObjectAddedImpl(obj, parentObject.Model);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000C6D08 File Offset: 0x000C4F08
		public static void RegisterObjectAdding(MetadataObject obj, IMetadataObjectCollection parentCollection)
		{
			ObjectChangeTracker.RegisterObjectAddingImpl(obj, parentCollection.Owner, parentCollection);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000C6D17 File Offset: 0x000C4F17
		public static void RegisterObjectAdded(MetadataObject obj, IMetadataObjectCollection parentCollection)
		{
			ObjectChangeTracker.RegisterObjectAddedImpl(obj, parentCollection.Owner.Model);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000C6D2C File Offset: 0x000C4F2C
		public static void RegisterObjectRenaming(NamedMetadataObject obj)
		{
			if (obj.RenameRequestedThroughAPI)
			{
				throw new InvalidOperationException(TomSR.Exception_RenameAlreadyRequested);
			}
			if (obj.IsRemoved)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectRemovedCannotBeModifiedAttachedToModel);
			}
			Model model = obj.Model;
			if (model != null && model.TxManager != null)
			{
				ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
				ObjectChangeTracker.EnsureNoMergePartitionsInSavepoint(model);
				ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
				ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(obj);
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000C6D8C File Offset: 0x000C4F8C
		public static void RegisterObjectRenamed(NamedMetadataObject obj)
		{
			Model model = obj.Model;
			if (model != null && model.TxManager != null)
			{
				model.TxManager.CurrentSavepoint.AnyRenameRequestedThroughAPI = true;
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000C6DBC File Offset: 0x000C4FBC
		public static void RegisterPartitionsMerging(Partition target, IEnumerable<Partition> sources)
		{
			Model model = target.Model;
			if (model == null || model.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_DisconnectedPartitionCannotBeMerged);
			}
			if (model.TxManager.CurrentSavepoint.AllMergePartitionsRequestedTables.Contains(target.Table))
			{
				throw new InvalidOperationException(TomSR.Exception_MergePartitionsForTableAlreadyRequested);
			}
			using (IEnumerator<Partition> enumerator = sources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Table != target.Table)
					{
						throw new InvalidOperationException(TomSR.Exception_PartitionsFromDifferentTablesCannotBeMerged);
					}
				}
			}
			if (target.IsRemoved)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectRemovedCannotBeModifiedAttachedToModel);
			}
			ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
			ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
			ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(target);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000C6E80 File Offset: 0x000C5080
		public static void RegisterPartitionsMerged(Partition target)
		{
			target.Model.TxManager.CurrentSavepoint.AllMergePartitionsRequestedTables.Add(target.Table);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000C6EA4 File Offset: 0x000C50A4
		public static void RegisterPartitionForAnalyzeRefreshPolicyImpact(Partition partition)
		{
			Model model = partition.Model;
			if (model == null || model.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_DisconnectedObjectCannotBeAnalyzeRefreshPolicyImpact);
			}
			ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
			ObjectChangeTracker.EnsureNoMergePartitionsInSavepoint(model);
			ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
			ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(partition);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000C6EE8 File Offset: 0x000C50E8
		public static void RegisterObjectForRefresh(MetadataObject obj, RefreshType type, bool hasOverrides)
		{
			Model model = obj.Model;
			if (model == null || model.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_DisconnectedObjectCannotBeRefreshed);
			}
			if (hasOverrides && !Utils.CanRefreshWithOverrides(type))
			{
				throw new ArgumentException(TomSR.Exception_OverridesIncompatibleWithRefreshType, "overrides");
			}
			ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
			ObjectChangeTracker.EnsureNoMergePartitionsInSavepoint(model);
			ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
			ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(obj);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000C6F46 File Offset: 0x000C5146
		public static void RegisterObjectRemoving(MetadataObject obj, IMetadataObjectCollection parentCollection)
		{
			ObjectChangeTracker.RegisterObjectRemovingImpl(obj, parentCollection.Owner, parentCollection);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000C6F55 File Offset: 0x000C5155
		public static void RegisterObjectRemoved(MetadataObject obj, IMetadataObjectCollection parentCollection)
		{
			ObjectChangeTracker.RegisterObjectRemovedImpl(obj, parentCollection.Owner.Model);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000C6F68 File Offset: 0x000C5168
		public static void RegisterObjectRemoving(MetadataObject obj, MetadataObject parentObject)
		{
			ObjectChangeTracker.RegisterObjectRemovingImpl(obj, parentObject, null);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000C6F72 File Offset: 0x000C5172
		public static void RegisterObjectRemoved(MetadataObject obj, MetadataObject parentObject)
		{
			ObjectChangeTracker.RegisterObjectRemovedImpl(obj, parentObject.Model);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000C6F80 File Offset: 0x000C5180
		public static void RegisterCollectionChanging(IMetadataObjectCollection collection)
		{
			Model model = collection.Owner.Model;
			if (model != null && model.TxManager != null)
			{
				ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
				ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(collection);
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000C6FB4 File Offset: 0x000C51B4
		internal static void RegisterAddedSubtreeWithSavepoint(TxSavepoint savepoint, MetadataObject subtreeRoot)
		{
			foreach (MetadataObject metadataObject in subtreeRoot.GetSelfAndAllDescendants())
			{
				savepoint.RegisterBody(metadataObject.Body);
				foreach (IMetadataObjectCollection metadataObjectCollection in metadataObject.GetChildrenCollections(false))
				{
					savepoint.RegisterBody(metadataObjectCollection.Body);
				}
			}
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000C7048 File Offset: 0x000C5248
		private static void RegisterRevertedSubtreeWithSavepoint(TxSavepoint savepoint, MetadataObject subtreeRoot)
		{
			foreach (MetadataObject metadataObject in subtreeRoot.GetSelfAndAllDescendants())
			{
				savepoint.UnregisterBody(metadataObject.Body);
				foreach (IMetadataObjectCollection metadataObjectCollection in metadataObject.GetChildrenCollections(false))
				{
					savepoint.UnregisterBody(metadataObjectCollection.Body);
				}
			}
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000C70DC File Offset: 0x000C52DC
		private static void RegisterObjectAddingImpl(MetadataObject obj, MetadataObject parent, ITxObject owner = null)
		{
			if (obj.IsRemoved)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectRemovedCannotBeModifiedAttachedToModel);
			}
			if (parent.IsRemoved)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectRemovedCannotBeModifiedAttachedToModel);
			}
			Model model = parent.Model;
			if (model == null || model.TxManager == null)
			{
				return;
			}
			IEqualityComparer<string> namesComparer = model.GetNamesComparer();
			bool flag = ObjectChangeTracker.HasDifferentCultureInfo(obj, namesComparer);
			if (flag)
			{
				ObjectChangeTracker.EnsureCultureInfoCanBeUpdated(obj, namesComparer);
			}
			ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
			ObjectChangeTracker.EnsureNoMergePartitionsInSavepoint(model);
			ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
			ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(owner ?? parent);
			foreach (MetadataObject metadataObject in obj.GetSelfAndAllDescendants())
			{
				metadataObject.CloneBody(null);
				if (flag)
				{
					ObjectChangeTracker.UpdateMetadataCulture(metadataObject, namesComparer);
				}
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000C71A8 File Offset: 0x000C53A8
		private static bool HasDifferentCultureInfo(MetadataObject obj, IEqualityComparer<string> comparer)
		{
			return comparer != obj.GetNamesComparer();
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000C71B8 File Offset: 0x000C53B8
		private static void EnsureCultureInfoCanBeUpdated(MetadataObject obj, IEqualityComparer<string> comparer)
		{
			using (IEnumerator<INamedMetadataObjectCollection> enumerator = (from c in obj.GetSelfAndAllDescendants().SelectMany((MetadataObject o) => o.GetChildrenCollections(false))
				where c is INamedMetadataObjectCollection
				select (INamedMetadataObjectCollection)c).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.CanUpdateCultureInfo(comparer))
					{
						throw new ArgumentException(TomSR.Exception_CannotUpdateCultureInfoOfChildCollection);
					}
				}
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000C727C File Offset: 0x000C547C
		private static void UpdateMetadataCulture(MetadataObject obj, IEqualityComparer<string> comparer)
		{
			foreach (INamedMetadataObjectCollection namedMetadataObjectCollection in from c in obj.GetChildrenCollections(false)
				where c is INamedMetadataObjectCollection
				select (INamedMetadataObjectCollection)c)
			{
				namedMetadataObjectCollection.CloneBody(null);
				namedMetadataObjectCollection.UpdateCultureInfo(comparer);
			}
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000C7318 File Offset: 0x000C5518
		private static void RegisterObjectAddedImpl(MetadataObject obj, Model model)
		{
			if (model != null)
			{
				if (model.TxManager != null)
				{
					ObjectChangeTracker.RegisterAddedSubtreeWithSavepoint(model.TxManager.CurrentSavepoint, obj);
				}
				model.NotifySubtreeAdded(obj);
			}
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000C7340 File Offset: 0x000C5540
		private static void RegisterObjectRemovingImpl(MetadataObject obj, MetadataObject parent, ITxObject owner = null)
		{
			if (parent.IsRemoved)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectRemovedCannotBeModifiedAttachedToModel);
			}
			Model model = parent.Model;
			if (model == null || model.TxManager == null)
			{
				return;
			}
			ObjectChangeTracker.EnsureNoRenameInSavepoint(model);
			ObjectChangeTracker.EnsureNoMergePartitionsInSavepoint(model);
			ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(model, true);
			ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(owner ?? parent);
			foreach (MetadataObject metadataObject in obj.GetSelfAndAllDescendants())
			{
				ObjectChangeTracker.EnsureObjectIsInCurrentSavepoint(metadataObject);
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000C73D0 File Offset: 0x000C55D0
		private static void RegisterObjectRemovedImpl(MetadataObject obj, Model model)
		{
			foreach (MetadataObject metadataObject in obj.GetSelfAndAllDescendants())
			{
				metadataObject.MarkAsRemoved();
			}
			if (model == null)
			{
				return;
			}
			if (model.TxManager != null)
			{
				if (obj.Id.IsNull && obj.Body.CreatedFrom == null)
				{
					ObjectChangeTracker.RegisterRevertedSubtreeWithSavepoint(model.TxManager.CurrentSavepoint, obj);
				}
				else
				{
					Queue<MetadataObject> queue = new Queue<MetadataObject>();
					queue.Enqueue(obj);
					while (queue.Count > 0)
					{
						foreach (MetadataObject metadataObject2 in queue.Dequeue().GetChildren(false))
						{
							if (metadataObject2.Id.IsNull && metadataObject2.Body.CreatedFrom == null)
							{
								ObjectChangeTracker.RegisterRevertedSubtreeWithSavepoint(model.TxManager.CurrentSavepoint, metadataObject2);
							}
							else
							{
								queue.Enqueue(metadataObject2);
							}
						}
					}
				}
			}
			model.NotifySubtreeRemoved(obj);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000C74EC File Offset: 0x000C56EC
		private static void HandleIdChanging(MetadataObject obj, ObjectId newId)
		{
			if (obj.Id != newId)
			{
				IMetadataObjectCollection parentCollection = obj.ParentCollection;
				if (parentCollection != null)
				{
					parentCollection.NotifyIdChanging(obj, newId);
				}
				if (obj.Model != null)
				{
					((INotifyObjectIdChange)obj.Model).NotifyIdChanging(obj, newId);
				}
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000C7530 File Offset: 0x000C5730
		private static void HandleIdChanged(MetadataObject obj, ObjectId oldId)
		{
			if (obj.Id != oldId)
			{
				IMetadataObjectCollection parentCollection = obj.ParentCollection;
				if (parentCollection != null)
				{
					parentCollection.NotifyIdChanged(obj, oldId);
				}
				if (obj.Model != null)
				{
					((INotifyObjectIdChange)obj.Model).NotifyIdChanged(obj, oldId);
				}
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000C7574 File Offset: 0x000C5774
		private static void HandleNameChanging(NamedMetadataObject obj, string newName)
		{
			if (string.Compare(obj.Name, newName, StringComparison.Ordinal) != 0)
			{
				IMetadataObjectCollection parentCollection = obj.ParentCollection;
				if (parentCollection != null)
				{
					((INamedMetadataObjectCollection)parentCollection).NotifyNameChanging(obj, newName);
				}
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000C75A8 File Offset: 0x000C57A8
		private static void HandleNameChanged(NamedMetadataObject obj, string oldName)
		{
			if (string.Compare(obj.Name, oldName, StringComparison.Ordinal) != 0)
			{
				IMetadataObjectCollection parentCollection = obj.ParentCollection;
				if (parentCollection != null)
				{
					((INamedMetadataObjectCollection)parentCollection).NotifyNameChanged(obj, oldName);
				}
				foreach (NamedMetadataObject namedMetadataObject in from o in obj.GetNameLinkedObjects(oldName)
					select (NamedMetadataObject)o)
				{
					((INamedMetadataObjectCollection)namedMetadataObject.ParentCollection).NotifyNameChanged(namedMetadataObject, oldName);
				}
			}
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000C764C File Offset: 0x000C584C
		private static void HandleLineageTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			if (string.Compare(obj.LineageTag, newTag, StringComparison.Ordinal) != 0)
			{
				IMetadataObjectCollection parentCollection = ((MetadataObject)obj).ParentCollection;
				if (parentCollection != null)
				{
					((INotifyObjectLineageTagChange)parentCollection).NotifyTagChanging(obj, newTag);
				}
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000C7684 File Offset: 0x000C5884
		private static void HandleLineageTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			if (string.Compare(obj.LineageTag, oldTag, StringComparison.Ordinal) != 0)
			{
				IMetadataObjectCollection parentCollection = ((MetadataObject)obj).ParentCollection;
				if (parentCollection != null)
				{
					((INotifyObjectLineageTagChange)parentCollection).NotifyTagChanged(obj, oldTag);
				}
			}
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000C76BC File Offset: 0x000C58BC
		private static void EnsureNoRenameInSavepoint(Model model)
		{
			if (model.TxManager.CurrentSavepoint.AnyRenameRequestedThroughAPI)
			{
				throw new InvalidOperationException(TomSR.Exception_ModelCannotBeMotified_RenameRequested);
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000C76DB File Offset: 0x000C58DB
		private static void EnsureNoMergePartitionsInSavepoint(Model model)
		{
			if (model.TxManager.CurrentSavepoint.AllMergePartitionsRequestedTables.Count > 0)
			{
				throw new InvalidOperationException(TomSR.Exception_ModelCannotBeMotified_MergePartitionsRequested);
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000C7700 File Offset: 0x000C5900
		internal static void EnsureInCorrectSavepointForLocalChange(Model model, bool isModelChanging)
		{
			if (model.Database.HasObsoleteTransaction())
			{
				throw new InvalidOperationException(TomSR.Exception_ModifyDirtyDatabase(model.Database.Name));
			}
			TxSavepoint currentSavepoint = model.TxManager.CurrentSavepoint;
			if (currentSavepoint.Name == "SyncedMostRecent")
			{
				return;
			}
			ITransaction transaction = ((model.Database.Parent != null) ? model.Database.Parent.CurrentTransaction : null);
			if (transaction != null)
			{
				if (transaction.ModifiedDatabase == null)
				{
					transaction.ModifiedDatabase = model.Database;
					currentSavepoint.Name = "BeginTransaction";
					model.TxManager.AddSavepoint("Synced");
					if (isModelChanging)
					{
						model.TxManager.AddSavepoint("Modified");
						return;
					}
				}
				else
				{
					if (transaction.ModifiedDatabase != model.Database)
					{
						throw new InvalidOperationException(TomSR.Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction);
					}
					if (isModelChanging && currentSavepoint.Name == "Synced")
					{
						model.TxManager.AddSavepoint("Modified");
						return;
					}
				}
			}
			else if (isModelChanging && (currentSavepoint == null || currentSavepoint.Name == "Synced"))
			{
				model.TxManager.AddSavepoint("Modified");
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000C7824 File Offset: 0x000C5A24
		private static void EnsureObjectIsInCurrentSavepoint(ITxObject obj)
		{
			if (obj.Body.Savepoint != null && obj.Body.Savepoint != obj.Body.Savepoint.TxManager.CurrentSavepoint)
			{
				obj.CloneBody(obj.Body.Savepoint.TxManager.CurrentSavepoint);
			}
		}

		// Token: 0x0400069F RID: 1695
		internal static readonly CopyContext BodyCloneContext = new CopyContext(CopyFlags.IncludeObjectIds | CopyFlags.CloningBody | CopyFlags.DontTrackObjectChanges | CopyFlags.IncludeOperationalFlags, null);
	}
}
