using System;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000FA RID: 250
	internal class EntityTrackingAdapter
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x000279E8 File Offset: 0x00025BE8
		internal EntityTrackingAdapter(EntityTrackerBase entityTracker, MergeOption mergeOption, ClientEdmModel model, DataServiceContext context)
		{
			this.MaterializationLog = new AtomMaterializerLog(mergeOption, model, entityTracker);
			this.MergeOption = mergeOption;
			this.EntityTracker = entityTracker;
			this.Model = model;
			this.Context = context;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00027A1B File Offset: 0x00025C1B
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00027A23 File Offset: 0x00025C23
		internal MergeOption MergeOption { get; private set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00027A2C File Offset: 0x00025C2C
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00027A34 File Offset: 0x00025C34
		internal DataServiceContext Context { get; private set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00027A3D File Offset: 0x00025C3D
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00027A45 File Offset: 0x00025C45
		internal AtomMaterializerLog MaterializationLog { get; private set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00027A4E File Offset: 0x00025C4E
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x00027A56 File Offset: 0x00025C56
		internal EntityTrackerBase EntityTracker { get; private set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00027A5F File Offset: 0x00025C5F
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00027A67 File Offset: 0x00025C67
		internal ClientEdmModel Model { get; private set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00027A70 File Offset: 0x00025C70
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00027A78 File Offset: 0x00025C78
		internal object TargetInstance
		{
			get
			{
				return this.targetInstance;
			}
			set
			{
				this.targetInstance = value;
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00027A81 File Offset: 0x00025C81
		internal virtual bool TryResolveExistingEntity(MaterializerEntry entry, Type expectedEntryType)
		{
			return this.TryResolveAsTarget(entry) || this.TryResolveAsExistingEntry(entry, expectedEntryType);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00027A9C File Offset: 0x00025C9C
		internal bool TryResolveAsExistingEntry(MaterializerEntry entry, Type expectedEntryType)
		{
			if (entry.Entry.IsTransient)
			{
				return false;
			}
			if (!entry.IsTracking)
			{
				return false;
			}
			if (entry.Id == null)
			{
				throw Error.InvalidOperation(Strings.Deserialize_MissingIdElement);
			}
			return this.TryResolveAsCreated(entry) || this.TryResolveFromContext(entry, expectedEntryType);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00027AF0 File Offset: 0x00025CF0
		private bool TryResolveAsTarget(MaterializerEntry entry)
		{
			if (entry.ResolvedObject == null)
			{
				return false;
			}
			ClientEdmModel model = this.Model;
			entry.ActualType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entry.ResolvedObject.GetType()));
			this.MaterializationLog.FoundTargetInstance(entry);
			entry.ShouldUpdateFromPayload = this.MergeOption != MergeOption.PreserveChanges;
			entry.EntityHasBeenResolved = true;
			return true;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00027B54 File Offset: 0x00025D54
		private bool TryResolveFromContext(MaterializerEntry entry, Type expectedEntryType)
		{
			if (this.MergeOption != MergeOption.NoTracking)
			{
				EntityStates entityStates;
				entry.ResolvedObject = this.EntityTracker.TryGetEntity(entry.Id, out entityStates);
				if (entry.ResolvedObject != null)
				{
					if (!expectedEntryType.IsInstanceOfType(entry.ResolvedObject))
					{
						throw Error.InvalidOperation(Strings.Deserialize_Current(expectedEntryType, entry.ResolvedObject.GetType()));
					}
					ClientEdmModel model = this.Model;
					entry.ActualType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entry.ResolvedObject.GetType()));
					entry.EntityHasBeenResolved = true;
					entry.ShouldUpdateFromPayload = this.MergeOption == MergeOption.OverwriteChanges || (this.MergeOption == MergeOption.PreserveChanges && entityStates == EntityStates.Unchanged) || (this.MergeOption == MergeOption.PreserveChanges && entityStates == EntityStates.Deleted);
					this.MaterializationLog.FoundExistingInstance(entry);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00027C20 File Offset: 0x00025E20
		private bool TryResolveAsCreated(MaterializerEntry entry)
		{
			MaterializerEntry materializerEntry;
			if (!this.MaterializationLog.TryResolve(entry, out materializerEntry))
			{
				return false;
			}
			entry.ActualType = materializerEntry.ActualType;
			entry.ResolvedObject = materializerEntry.ResolvedObject;
			entry.CreatedByMaterializer = materializerEntry.CreatedByMaterializer;
			entry.ShouldUpdateFromPayload = materializerEntry.ShouldUpdateFromPayload;
			entry.EntityHasBeenResolved = true;
			return true;
		}

		// Token: 0x0400060F RID: 1551
		private object targetInstance;
	}
}
