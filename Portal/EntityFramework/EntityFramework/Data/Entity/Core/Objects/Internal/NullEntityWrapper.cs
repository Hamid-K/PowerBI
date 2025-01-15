using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200044D RID: 1101
	internal class NullEntityWrapper : IEntityWrapper
	{
		// Token: 0x06003597 RID: 13719 RVA: 0x000ACA33 File Offset: 0x000AAC33
		private NullEntityWrapper()
		{
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000ACA3B File Offset: 0x000AAC3B
		internal static IEntityWrapper NullWrapper
		{
			get
			{
				return NullEntityWrapper._nullWrapper;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000ACA42 File Offset: 0x000AAC42
		public RelationshipManager RelationshipManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000ACA45 File Offset: 0x000AAC45
		public bool OwnsRelationshipManager
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000ACA48 File Offset: 0x000AAC48
		public object Entity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000ACA4B File Offset: 0x000AAC4B
		// (set) Token: 0x0600359D RID: 13725 RVA: 0x000ACA4E File Offset: 0x000AAC4E
		public EntityEntry ObjectStateEntry
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000ACA50 File Offset: 0x000AAC50
		public void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000ACA52 File Offset: 0x000AAC52
		public bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return false;
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000ACA55 File Offset: 0x000AAC55
		// (set) Token: 0x060035A1 RID: 13729 RVA: 0x000ACA58 File Offset: 0x000AAC58
		public EntityKey EntityKey
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000ACA5A File Offset: 0x000AAC5A
		public EntityKey GetEntityKeyFromEntity()
		{
			return null;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000ACA5D File Offset: 0x000AAC5D
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x000ACA60 File Offset: 0x000AAC60
		public ObjectContext Context
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000ACA62 File Offset: 0x000AAC62
		public MergeOption MergeOption
		{
			get
			{
				return MergeOption.NoTracking;
			}
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000ACA65 File Offset: 0x000AAC65
		public void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000ACA67 File Offset: 0x000AAC67
		public void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000ACA69 File Offset: 0x000AAC69
		public void DetachContext()
		{
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000ACA6B File Offset: 0x000AAC6B
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000ACA6D File Offset: 0x000AAC6D
		public void TakeSnapshot(EntityEntry entry)
		{
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000ACA6F File Offset: 0x000AAC6F
		public void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000ACA71 File Offset: 0x000AAC71
		public Type IdentityType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000ACA74 File Offset: 0x000AAC74
		public void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000ACA76 File Offset: 0x000AAC76
		public object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			return null;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000ACA79 File Offset: 0x000AAC79
		public void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000ACA7B File Offset: 0x000AAC7B
		public void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000ACA7D File Offset: 0x000AAC7D
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000ACA7F File Offset: 0x000AAC7F
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000ACA82 File Offset: 0x000AAC82
		public bool InitializingProxyRelatedEnds
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000ACA84 File Offset: 0x000AAC84
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000ACA86 File Offset: 0x000AAC86
		public bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000ACA89 File Offset: 0x000AAC89
		public bool OverridesEqualsOrGetHashCode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001157 RID: 4439
		private static readonly IEntityWrapper _nullWrapper = new NullEntityWrapper();
	}
}
