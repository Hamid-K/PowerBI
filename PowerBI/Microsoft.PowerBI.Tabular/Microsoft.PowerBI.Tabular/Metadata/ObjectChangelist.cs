using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001FC RID: 508
	internal sealed class ObjectChangelist
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x000C6740 File Offset: 0x000C4940
		internal ObjectChangelist()
		{
			this.addedObjects = new List<MetadataObject>();
			this.addedSubtreeRoots = new List<MetadataObject>();
			this.removedObjects = new List<MetadataObject>();
			this.removedSubtreeRoots = new List<MetadataObject>();
			this.propChanges = new List<PropertyChangeEntry>();
			this.refreshedObjects = new List<MetadataObject>();
			this.renamedObjects = new List<NamedMetadataObject>();
			this.partitionMergedObjects = new List<Partition>();
			this.analyzeRefreshPolicyImpactObjects = new List<Partition>();
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x000C67B6 File Offset: 0x000C49B6
		public IReadOnlyCollection<MetadataObject> AddedObjects
		{
			get
			{
				return this.addedObjects;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x000C67BE File Offset: 0x000C49BE
		public IReadOnlyCollection<MetadataObject> AddedSubtreeRoots
		{
			get
			{
				return this.addedSubtreeRoots;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000C67C6 File Offset: 0x000C49C6
		public IReadOnlyCollection<MetadataObject> RemovedObjects
		{
			get
			{
				return this.removedObjects;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x000C67CE File Offset: 0x000C49CE
		public IReadOnlyCollection<MetadataObject> RemovedSubtreeRoots
		{
			get
			{
				return this.removedSubtreeRoots;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000C67D6 File Offset: 0x000C49D6
		public IReadOnlyCollection<PropertyChangeEntry> PropChanges
		{
			get
			{
				return this.propChanges;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000C67DE File Offset: 0x000C49DE
		public IReadOnlyCollection<Partition> AnalyzeRefreshPolicyImpactObjects
		{
			get
			{
				return this.analyzeRefreshPolicyImpactObjects;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000C67E6 File Offset: 0x000C49E6
		public IReadOnlyCollection<MetadataObject> RefreshObjects
		{
			get
			{
				return this.refreshedObjects;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x000C67EE File Offset: 0x000C49EE
		public IReadOnlyCollection<NamedMetadataObject> RenamedObjects
		{
			get
			{
				return this.renamedObjects;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000C67F6 File Offset: 0x000C49F6
		public IReadOnlyCollection<Partition> PartitionMergedObjects
		{
			get
			{
				return this.partitionMergedObjects;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x000C6800 File Offset: 0x000C4A00
		public bool IsEmpty
		{
			get
			{
				return this.addedObjects.Count == 0 && this.removedObjects.Count == 0 && this.propChanges.Count == 0 && this.analyzeRefreshPolicyImpactObjects.Count == 0 && this.refreshedObjects.Count == 0 && this.renamedObjects.Count == 0 && this.partitionMergedObjects.Count == 0;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x000C686B File Offset: 0x000C4A6B
		public static ObjectChangelist Empty
		{
			get
			{
				return ObjectChangelist.emptyChangelist;
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000C6872 File Offset: 0x000C4A72
		internal void RegisterAddedObject(MetadataObject obj)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.addedObjects.Add(obj);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000C6893 File Offset: 0x000C4A93
		internal void RegisterAddedSubtree(MetadataObject subtreeRoot)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.addedSubtreeRoots.Add(subtreeRoot);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000C68B4 File Offset: 0x000C4AB4
		internal void RegisterRemovedObject(MetadataObject obj)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.removedObjects.Add(obj);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000C68D5 File Offset: 0x000C4AD5
		internal void RegisterRemovedSubtree(MetadataObject subtreeRoot)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.removedSubtreeRoots.Add(subtreeRoot);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000C68F6 File Offset: 0x000C4AF6
		internal void RegisterPropertyChange(PropertyChangeEntry propertyChangeEntry)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.propChanges.Add(propertyChangeEntry);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000C6917 File Offset: 0x000C4B17
		internal void RegisterObjectForRefresh(MetadataObject obj)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.refreshedObjects.Add(obj);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000C6938 File Offset: 0x000C4B38
		internal void RegisterObjectForMergePartitions(Partition obj)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.partitionMergedObjects.Add(obj);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000C6959 File Offset: 0x000C4B59
		internal void RegisterObjectForAnalyzeRefreshPolicyImpact(Partition obj)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.analyzeRefreshPolicyImpactObjects.Add(obj);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000C697A File Offset: 0x000C4B7A
		internal void RegisterObjectForRename(NamedMetadataObject obj)
		{
			if (this.isReadOnly)
			{
				throw new TomInternalException("Cannot update a read-only changelist!");
			}
			this.renamedObjects.Add(obj);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000C699C File Offset: 0x000C4B9C
		internal ObjectImpact ConvertToImpact()
		{
			ObjectImpact objectImpact = new ObjectImpact();
			foreach (MetadataObject metadataObject in this.addedObjects)
			{
				objectImpact.RegisterAddedObject(metadataObject);
			}
			foreach (MetadataObject metadataObject2 in this.addedSubtreeRoots)
			{
				objectImpact.RegisterAddedSubtree(metadataObject2);
			}
			foreach (MetadataObject metadataObject3 in this.removedObjects)
			{
				objectImpact.RegisterRemovedObject(metadataObject3);
			}
			foreach (MetadataObject metadataObject4 in this.removedSubtreeRoots)
			{
				objectImpact.RegisterRemovedSubtree(metadataObject4);
			}
			foreach (PropertyChangeEntry propertyChangeEntry in this.propChanges)
			{
				if (propertyChangeEntry.IsUserProperty)
				{
					objectImpact.RegisterPropertyChange(propertyChangeEntry);
				}
			}
			return objectImpact;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000C6B10 File Offset: 0x000C4D10
		internal ObjectChangelist MarkAsReadOnly()
		{
			this.isReadOnly = true;
			return this;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000C6B1C File Offset: 0x000C4D1C
		[Conditional("DEBUG")]
		internal void EnsureHasOnlyOperationalChanges(bool canHaveRefresh, bool canHaveMerge, bool canHaveARPImpact)
		{
			if (this.addedObjects.Count <= 0 && this.removedObjects.Count <= 0 && this.propChanges.Count <= 0)
			{
				int count = this.renamedObjects.Count;
			}
			if (!canHaveRefresh)
			{
				int count2 = this.refreshedObjects.Count;
			}
			if (!canHaveMerge)
			{
				int count3 = this.partitionMergedObjects.Count;
			}
			if (!canHaveARPImpact)
			{
				int count4 = this.analyzeRefreshPolicyImpactObjects.Count;
			}
		}

		// Token: 0x04000694 RID: 1684
		private static readonly ObjectChangelist emptyChangelist = new ObjectChangelist().MarkAsReadOnly();

		// Token: 0x04000695 RID: 1685
		private List<MetadataObject> addedObjects;

		// Token: 0x04000696 RID: 1686
		private List<MetadataObject> addedSubtreeRoots;

		// Token: 0x04000697 RID: 1687
		private List<MetadataObject> removedObjects;

		// Token: 0x04000698 RID: 1688
		private List<MetadataObject> removedSubtreeRoots;

		// Token: 0x04000699 RID: 1689
		private List<PropertyChangeEntry> propChanges;

		// Token: 0x0400069A RID: 1690
		private List<MetadataObject> refreshedObjects;

		// Token: 0x0400069B RID: 1691
		private List<NamedMetadataObject> renamedObjects;

		// Token: 0x0400069C RID: 1692
		private List<Partition> partitionMergedObjects;

		// Token: 0x0400069D RID: 1693
		private List<Partition> analyzeRefreshPolicyImpactObjects;

		// Token: 0x0400069E RID: 1694
		private bool isReadOnly;
	}
}
