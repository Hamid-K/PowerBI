using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Client.Materialization;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000AF RID: 175
	internal class AtomMaterializerLog
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00018894 File Offset: 0x00016A94
		internal AtomMaterializerLog(MergeOption mergeOption, ClientEdmModel model, EntityTrackerBase entityTracker)
		{
			this.appendOnlyEntries = new Dictionary<Uri, ODataResource>(EqualityComparer<Uri>.Default);
			this.mergeOption = mergeOption;
			this.clientEdmModel = model;
			this.entityTracker = entityTracker;
			this.identityStack = new Dictionary<Uri, ODataResource>(EqualityComparer<Uri>.Default);
			this.links = new List<LinkDescriptor>();
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000188E7 File Offset: 0x00016AE7
		internal bool Tracking
		{
			get
			{
				return this.mergeOption != MergeOption.NoTracking;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000188F8 File Offset: 0x00016AF8
		internal static void MergeEntityDescriptorInfo(EntityDescriptor trackedEntityDescriptor, EntityDescriptor entityDescriptorFromMaterializer, bool mergeInfo, MergeOption mergeOption)
		{
			if (trackedEntityDescriptor != entityDescriptorFromMaterializer)
			{
				if (entityDescriptorFromMaterializer.ETag != null && mergeOption != MergeOption.AppendOnly)
				{
					trackedEntityDescriptor.ETag = entityDescriptorFromMaterializer.ETag;
				}
				if (mergeInfo)
				{
					if (entityDescriptorFromMaterializer.SelfLink != null)
					{
						trackedEntityDescriptor.SelfLink = entityDescriptorFromMaterializer.SelfLink;
					}
					if (entityDescriptorFromMaterializer.EditLink != null)
					{
						trackedEntityDescriptor.EditLink = entityDescriptorFromMaterializer.EditLink;
					}
					foreach (LinkInfo linkInfo in entityDescriptorFromMaterializer.LinkInfos)
					{
						trackedEntityDescriptor.MergeLinkInfo(linkInfo);
					}
					foreach (StreamDescriptor streamDescriptor in entityDescriptorFromMaterializer.StreamDescriptors)
					{
						trackedEntityDescriptor.MergeStreamDescriptor(streamDescriptor);
					}
					trackedEntityDescriptor.ServerTypeName = entityDescriptorFromMaterializer.ServerTypeName;
				}
				if (entityDescriptorFromMaterializer.OperationDescriptors != null)
				{
					trackedEntityDescriptor.ClearOperationDescriptors();
					trackedEntityDescriptor.AppendOperationalDescriptors(entityDescriptorFromMaterializer.OperationDescriptors);
				}
				if (entityDescriptorFromMaterializer.ReadStreamUri != null)
				{
					trackedEntityDescriptor.ReadStreamUri = entityDescriptorFromMaterializer.ReadStreamUri;
				}
				if (entityDescriptorFromMaterializer.EditStreamUri != null)
				{
					trackedEntityDescriptor.EditStreamUri = entityDescriptorFromMaterializer.EditStreamUri;
				}
				if (entityDescriptorFromMaterializer.ReadStreamUri != null || entityDescriptorFromMaterializer.EditStreamUri != null)
				{
					trackedEntityDescriptor.StreamETag = entityDescriptorFromMaterializer.StreamETag;
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018A5C File Offset: 0x00016C5C
		internal void ApplyToContext()
		{
			if (!this.Tracking)
			{
				return;
			}
			foreach (KeyValuePair<Uri, ODataResource> keyValuePair in this.identityStack)
			{
				MaterializerEntry entry = MaterializerEntry.GetEntry(keyValuePair.Value);
				bool flag = entry.CreatedByMaterializer || entry.ResolvedObject == this.insertRefreshObject || entry.ShouldUpdateFromPayload;
				EntityDescriptor entityDescriptor = this.entityTracker.InternalAttachEntityDescriptor(entry.EntityDescriptor, false);
				AtomMaterializerLog.MergeEntityDescriptorInfo(entityDescriptor, entry.EntityDescriptor, flag, this.mergeOption);
				if (flag && (this.mergeOption != MergeOption.PreserveChanges || entityDescriptor.State != EntityStates.Deleted))
				{
					entityDescriptor.State = EntityStates.Unchanged;
					entityDescriptor.PropertiesToSerialize.Clear();
				}
			}
			foreach (LinkDescriptor linkDescriptor in this.links)
			{
				if (EntityStates.Added == linkDescriptor.State)
				{
					this.entityTracker.AttachLink(linkDescriptor.Source, linkDescriptor.SourceProperty, linkDescriptor.Target, this.mergeOption);
				}
				else if (EntityStates.Modified == linkDescriptor.State)
				{
					object obj = linkDescriptor.Target;
					if (MergeOption.PreserveChanges == this.mergeOption)
					{
						LinkDescriptor linkDescriptor2 = this.entityTracker.GetLinks(linkDescriptor.Source, linkDescriptor.SourceProperty).SingleOrDefault<LinkDescriptor>();
						if (linkDescriptor2 != null && linkDescriptor2.Target == null)
						{
							continue;
						}
						if ((obj != null && EntityStates.Deleted == this.entityTracker.GetEntityDescriptor(obj).State) || EntityStates.Deleted == this.entityTracker.GetEntityDescriptor(linkDescriptor.Source).State)
						{
							obj = null;
						}
					}
					this.entityTracker.AttachLink(linkDescriptor.Source, linkDescriptor.SourceProperty, obj, this.mergeOption);
				}
				else
				{
					this.entityTracker.DetachExistingLink(linkDescriptor, false);
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00018C7C File Offset: 0x00016E7C
		internal void Clear()
		{
			this.identityStack.Clear();
			this.links.Clear();
			this.insertRefreshObject = null;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00018C9B File Offset: 0x00016E9B
		internal void FoundExistingInstance(MaterializerEntry entry)
		{
			this.identityStack[entry.Id] = entry.Entry;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018CB4 File Offset: 0x00016EB4
		internal void FoundTargetInstance(MaterializerEntry entry)
		{
			if (AtomMaterializerLog.IsEntity(entry))
			{
				this.entityTracker.AttachIdentity(entry.EntityDescriptor, this.mergeOption);
				this.identityStack.Add(entry.Id, entry.Entry);
				this.insertRefreshObject = entry.ResolvedObject;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018D04 File Offset: 0x00016F04
		internal bool TryResolve(MaterializerEntry entry, out MaterializerEntry existingEntry)
		{
			ODataResource odataResource;
			if (this.identityStack.TryGetValue(entry.Id, out odataResource))
			{
				existingEntry = MaterializerEntry.GetEntry(odataResource);
				return true;
			}
			if (this.appendOnlyEntries.TryGetValue(entry.Id, out odataResource))
			{
				EntityStates entityStates;
				this.entityTracker.TryGetEntity(entry.Id, out entityStates);
				if (entityStates == EntityStates.Unchanged)
				{
					existingEntry = MaterializerEntry.GetEntry(odataResource);
					return true;
				}
				this.appendOnlyEntries.Remove(entry.Id);
			}
			existingEntry = null;
			return false;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018D80 File Offset: 0x00016F80
		internal void AddedLink(MaterializerEntry source, string propertyName, object target)
		{
			if (!this.Tracking)
			{
				return;
			}
			if (AtomMaterializerLog.IsEntity(source) && AtomMaterializerLog.IsEntity(target, this.clientEdmModel))
			{
				LinkDescriptor linkDescriptor = new LinkDescriptor(source.ResolvedObject, propertyName, target, EntityStates.Added);
				this.links.Add(linkDescriptor);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018DC8 File Offset: 0x00016FC8
		internal void CreatedInstance(MaterializerEntry entry)
		{
			if (AtomMaterializerLog.IsEntity(entry) && entry.IsTracking && !entry.Entry.IsTransient)
			{
				this.identityStack.Add(entry.Id, entry.Entry);
				if (this.mergeOption == MergeOption.AppendOnly)
				{
					this.appendOnlyEntries.Add(entry.Id, entry.Entry);
				}
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00018E28 File Offset: 0x00017028
		internal void RemovedLink(MaterializerEntry source, string propertyName, object target)
		{
			if (AtomMaterializerLog.IsEntity(source) && AtomMaterializerLog.IsEntity(target, this.clientEdmModel))
			{
				LinkDescriptor linkDescriptor = new LinkDescriptor(source.ResolvedObject, propertyName, target, EntityStates.Detached);
				this.links.Add(linkDescriptor);
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00018E68 File Offset: 0x00017068
		internal void SetLink(MaterializerEntry source, string propertyName, object target)
		{
			if (!this.Tracking)
			{
				return;
			}
			if (AtomMaterializerLog.IsEntity(source) && AtomMaterializerLog.IsEntity(target, this.clientEdmModel))
			{
				LinkDescriptor linkDescriptor = new LinkDescriptor(source.ResolvedObject, propertyName, target, EntityStates.Modified);
				this.links.Add(linkDescriptor);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00018EB0 File Offset: 0x000170B0
		private static bool IsEntity(MaterializerEntry entry)
		{
			return entry.ActualType.IsEntityType;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018EBD File Offset: 0x000170BD
		private static bool IsEntity(object entity, ClientEdmModel model)
		{
			return entity == null || ClientTypeUtil.TypeIsEntity(entity.GetType(), model);
		}

		// Token: 0x04000284 RID: 644
		private readonly MergeOption mergeOption;

		// Token: 0x04000285 RID: 645
		private readonly ClientEdmModel clientEdmModel;

		// Token: 0x04000286 RID: 646
		private readonly EntityTrackerBase entityTracker;

		// Token: 0x04000287 RID: 647
		private readonly Dictionary<Uri, ODataResource> appendOnlyEntries;

		// Token: 0x04000288 RID: 648
		private readonly Dictionary<Uri, ODataResource> identityStack;

		// Token: 0x04000289 RID: 649
		private readonly List<LinkDescriptor> links;

		// Token: 0x0400028A RID: 650
		private object insertRefreshObject;
	}
}
