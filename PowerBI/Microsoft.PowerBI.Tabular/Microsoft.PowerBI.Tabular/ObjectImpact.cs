using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000106 RID: 262
	public class ObjectImpact
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x0007BFBC File Offset: 0x0007A1BC
		internal ObjectImpact()
		{
			this.addedObjects = new List<MetadataObject>();
			this.addedSubtreeRoots = new List<MetadataObject>();
			this.removedObjects = new List<MetadataObject>();
			this.removedSubtreeRoots = new List<RemovedSubtreeEntry>();
			this.propChanges = new List<PropertyChangeEntry>();
			this.addedObjectsReadOnly = this.addedObjects.AsReadOnly();
			this.addedSubtreeRootsReadOnly = this.addedSubtreeRoots.AsReadOnly();
			this.removedObjectsReadOnly = this.removedObjects.AsReadOnly();
			this.removedSubtreeRootsReadOnly = this.removedSubtreeRoots.AsReadOnly();
			this.propChangesReadOnly = this.propChanges.AsReadOnly();
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0007C05B File Offset: 0x0007A25B
		public bool IsEmpty
		{
			get
			{
				return this.addedObjects.Count == 0 && this.removedObjects.Count == 0 && this.propChanges.Count == 0;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0007C087 File Offset: 0x0007A287
		public ReadOnlyCollection<MetadataObject> AddedObjects
		{
			get
			{
				return this.addedObjectsReadOnly;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0007C08F File Offset: 0x0007A28F
		public ReadOnlyCollection<MetadataObject> AddedSubtreeRoots
		{
			get
			{
				return this.addedSubtreeRootsReadOnly;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0007C097 File Offset: 0x0007A297
		public ReadOnlyCollection<MetadataObject> RemovedObjects
		{
			get
			{
				return this.removedObjectsReadOnly;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0007C09F File Offset: 0x0007A29F
		public ReadOnlyCollection<RemovedSubtreeEntry> RemovedSubtreeRoots
		{
			get
			{
				return this.removedSubtreeRootsReadOnly;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0007C0A7 File Offset: 0x0007A2A7
		public ReadOnlyCollection<PropertyChangeEntry> PropertyChanges
		{
			get
			{
				return this.propChangesReadOnly;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0007C0AF File Offset: 0x0007A2AF
		internal static ObjectImpact Empty
		{
			get
			{
				return ObjectImpact.emptyImpact;
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0007C0B6 File Offset: 0x0007A2B6
		internal void RegisterAddedObject(MetadataObject obj)
		{
			this.addedObjects.Add(obj);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0007C0C4 File Offset: 0x0007A2C4
		internal void RegisterAddedSubtree(MetadataObject subtreeRoot)
		{
			this.addedSubtreeRoots.Add(subtreeRoot);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0007C0D2 File Offset: 0x0007A2D2
		internal void RegisterRemovedObject(MetadataObject obj)
		{
			this.removedObjects.Add(obj);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0007C0E0 File Offset: 0x0007A2E0
		internal void RegisterRemovedSubtree(MetadataObject subtreeRoot)
		{
			this.removedSubtreeRoots.Add(new RemovedSubtreeEntry
			{
				RemovedObject = subtreeRoot,
				LastParent = subtreeRoot.LastParent
			});
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0007C105 File Offset: 0x0007A305
		internal void RegisterPropertyChange(PropertyChangeEntry propertyChangeEntry)
		{
			this.propChanges.Add(propertyChangeEntry);
		}

		// Token: 0x0400025C RID: 604
		private static readonly ObjectImpact emptyImpact = new ObjectImpact();

		// Token: 0x0400025D RID: 605
		private List<MetadataObject> addedObjects;

		// Token: 0x0400025E RID: 606
		private List<MetadataObject> addedSubtreeRoots;

		// Token: 0x0400025F RID: 607
		private List<MetadataObject> removedObjects;

		// Token: 0x04000260 RID: 608
		private List<RemovedSubtreeEntry> removedSubtreeRoots;

		// Token: 0x04000261 RID: 609
		private List<PropertyChangeEntry> propChanges;

		// Token: 0x04000262 RID: 610
		private ReadOnlyCollection<MetadataObject> addedObjectsReadOnly;

		// Token: 0x04000263 RID: 611
		private ReadOnlyCollection<MetadataObject> addedSubtreeRootsReadOnly;

		// Token: 0x04000264 RID: 612
		private ReadOnlyCollection<MetadataObject> removedObjectsReadOnly;

		// Token: 0x04000265 RID: 613
		private ReadOnlyCollection<RemovedSubtreeEntry> removedSubtreeRootsReadOnly;

		// Token: 0x04000266 RID: 614
		private ReadOnlyCollection<PropertyChangeEntry> propChangesReadOnly;
	}
}
