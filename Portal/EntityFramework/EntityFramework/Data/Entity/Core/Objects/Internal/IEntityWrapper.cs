using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000447 RID: 1095
	internal interface IEntityWrapper
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003547 RID: 13639
		RelationshipManager RelationshipManager { get; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003548 RID: 13640
		bool OwnsRelationshipManager { get; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003549 RID: 13641
		object Entity { get; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600354A RID: 13642
		// (set) Token: 0x0600354B RID: 13643
		EntityEntry ObjectStateEntry { get; set; }

		// Token: 0x0600354C RID: 13644
		void EnsureCollectionNotNull(RelatedEnd relatedEnd);

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600354D RID: 13645
		// (set) Token: 0x0600354E RID: 13646
		EntityKey EntityKey { get; set; }

		// Token: 0x0600354F RID: 13647
		EntityKey GetEntityKeyFromEntity();

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003550 RID: 13648
		// (set) Token: 0x06003551 RID: 13649
		ObjectContext Context { get; set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003552 RID: 13650
		MergeOption MergeOption { get; }

		// Token: 0x06003553 RID: 13651
		void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption);

		// Token: 0x06003554 RID: 13652
		void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption);

		// Token: 0x06003555 RID: 13653
		void DetachContext();

		// Token: 0x06003556 RID: 13654
		void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06003557 RID: 13655
		void TakeSnapshot(EntityEntry entry);

		// Token: 0x06003558 RID: 13656
		void TakeSnapshotOfRelationships(EntityEntry entry);

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06003559 RID: 13657
		Type IdentityType { get; }

		// Token: 0x0600355A RID: 13658
		void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x0600355B RID: 13659
		bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x0600355C RID: 13660
		object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x0600355D RID: 13661
		void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x0600355E RID: 13662
		void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x0600355F RID: 13663
		void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003560 RID: 13664
		// (set) Token: 0x06003561 RID: 13665
		bool InitializingProxyRelatedEnds { get; set; }

		// Token: 0x06003562 RID: 13666
		void UpdateCurrentValueRecord(object value, EntityEntry entry);

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003563 RID: 13667
		bool RequiresRelationshipChangeTracking { get; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003564 RID: 13668
		bool OverridesEqualsOrGetHashCode { get; }
	}
}
