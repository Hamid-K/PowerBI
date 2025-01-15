using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000459 RID: 1113
	internal class TransactionManager
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000B061A File Offset: 0x000AE81A
		// (set) Token: 0x0600366C RID: 13932 RVA: 0x000B0622 File Offset: 0x000AE822
		internal Dictionary<RelatedEnd, IList<IEntityWrapper>> PromotedRelationships { get; private set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x000B062B File Offset: 0x000AE82B
		// (set) Token: 0x0600366E RID: 13934 RVA: 0x000B0633 File Offset: 0x000AE833
		internal Dictionary<object, EntityEntry> PromotedKeyEntries { get; private set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000B063C File Offset: 0x000AE83C
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x000B0644 File Offset: 0x000AE844
		internal HashSet<EntityReference> PopulatedEntityReferences { get; private set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000B064D File Offset: 0x000AE84D
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x000B0655 File Offset: 0x000AE855
		internal HashSet<EntityReference> AlignedEntityReferences { get; private set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000B065E File Offset: 0x000AE85E
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x000B0666 File Offset: 0x000AE866
		internal MergeOption? OriginalMergeOption
		{
			get
			{
				return this._originalMergeOption;
			}
			set
			{
				this._originalMergeOption = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000B066F File Offset: 0x000AE86F
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x000B0677 File Offset: 0x000AE877
		internal HashSet<IEntityWrapper> ProcessedEntities { get; private set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000B0680 File Offset: 0x000AE880
		// (set) Token: 0x06003678 RID: 13944 RVA: 0x000B0688 File Offset: 0x000AE888
		internal Dictionary<object, IEntityWrapper> WrappedEntities { get; private set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000B0691 File Offset: 0x000AE891
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x000B0699 File Offset: 0x000AE899
		internal bool TrackProcessedEntities { get; private set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000B06A2 File Offset: 0x000AE8A2
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x000B06AA File Offset: 0x000AE8AA
		internal bool IsAddTracking { get; private set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000B06B3 File Offset: 0x000AE8B3
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000B06BB File Offset: 0x000AE8BB
		internal bool IsAttachTracking { get; private set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000B06C4 File Offset: 0x000AE8C4
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000B06CC File Offset: 0x000AE8CC
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> AddedRelationshipsByGraph { get; private set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000B06D5 File Offset: 0x000AE8D5
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x000B06DD File Offset: 0x000AE8DD
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> DeletedRelationshipsByGraph { get; private set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000B06E6 File Offset: 0x000AE8E6
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x000B06EE File Offset: 0x000AE8EE
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> AddedRelationshipsByForeignKey { get; private set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000B06F7 File Offset: 0x000AE8F7
		// (set) Token: 0x06003686 RID: 13958 RVA: 0x000B06FF File Offset: 0x000AE8FF
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> AddedRelationshipsByPrincipalKey { get; private set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x000B0708 File Offset: 0x000AE908
		// (set) Token: 0x06003688 RID: 13960 RVA: 0x000B0710 File Offset: 0x000AE910
		internal Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> DeletedRelationshipsByForeignKey { get; private set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x000B0719 File Offset: 0x000AE919
		// (set) Token: 0x0600368A RID: 13962 RVA: 0x000B0721 File Offset: 0x000AE921
		internal Dictionary<IEntityWrapper, HashSet<RelatedEnd>> ChangedForeignKeys { get; private set; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x000B072A File Offset: 0x000AE92A
		// (set) Token: 0x0600368C RID: 13964 RVA: 0x000B0732 File Offset: 0x000AE932
		internal bool IsDetectChanges { get; private set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000B073B File Offset: 0x000AE93B
		// (set) Token: 0x0600368E RID: 13966 RVA: 0x000B0743 File Offset: 0x000AE943
		internal bool IsAlignChanges { get; private set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000B074C File Offset: 0x000AE94C
		// (set) Token: 0x06003690 RID: 13968 RVA: 0x000B0754 File Offset: 0x000AE954
		internal bool IsLocalPublicAPI { get; private set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x000B075D File Offset: 0x000AE95D
		// (set) Token: 0x06003692 RID: 13970 RVA: 0x000B0765 File Offset: 0x000AE965
		internal bool IsOriginalValuesGetter { get; private set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000B076E File Offset: 0x000AE96E
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x000B0776 File Offset: 0x000AE976
		internal bool IsForeignKeyUpdate { get; private set; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x000B077F File Offset: 0x000AE97F
		// (set) Token: 0x06003696 RID: 13974 RVA: 0x000B0787 File Offset: 0x000AE987
		internal bool IsRelatedEndAdd { get; private set; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000B0790 File Offset: 0x000AE990
		internal bool IsGraphUpdate
		{
			get
			{
				return this._graphUpdateCount != 0;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000B079B File Offset: 0x000AE99B
		// (set) Token: 0x06003699 RID: 13977 RVA: 0x000B07A3 File Offset: 0x000AE9A3
		internal object EntityBeingReparented { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000B07AC File Offset: 0x000AE9AC
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x000B07B4 File Offset: 0x000AE9B4
		internal bool IsDetaching { get; private set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000B07BD File Offset: 0x000AE9BD
		// (set) Token: 0x0600369D RID: 13981 RVA: 0x000B07C5 File Offset: 0x000AE9C5
		internal EntityReference RelationshipBeingUpdated { get; private set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000B07CE File Offset: 0x000AE9CE
		// (set) Token: 0x0600369F RID: 13983 RVA: 0x000B07D6 File Offset: 0x000AE9D6
		internal bool IsFixupByReference { get; private set; }

		// Token: 0x060036A0 RID: 13984 RVA: 0x000B07E0 File Offset: 0x000AE9E0
		internal void BeginAddTracking()
		{
			this.IsAddTracking = true;
			this.PopulatedEntityReferences = new HashSet<EntityReference>();
			this.AlignedEntityReferences = new HashSet<EntityReference>();
			this.PromotedRelationships = new Dictionary<RelatedEnd, IList<IEntityWrapper>>();
			if (!this.IsDetectChanges)
			{
				this.TrackProcessedEntities = true;
				this.ProcessedEntities = new HashSet<IEntityWrapper>();
				this.WrappedEntities = new Dictionary<object, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
			}
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000B083F File Offset: 0x000AEA3F
		internal void EndAddTracking()
		{
			this.IsAddTracking = false;
			this.PopulatedEntityReferences = null;
			this.AlignedEntityReferences = null;
			this.PromotedRelationships = null;
			if (!this.IsDetectChanges)
			{
				this.TrackProcessedEntities = false;
				this.ProcessedEntities = null;
				this.WrappedEntities = null;
			}
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000B087C File Offset: 0x000AEA7C
		internal void BeginAttachTracking()
		{
			this.IsAttachTracking = true;
			this.PromotedRelationships = new Dictionary<RelatedEnd, IList<IEntityWrapper>>();
			this.PromotedKeyEntries = new Dictionary<object, EntityEntry>(ObjectReferenceEqualityComparer.Default);
			this.PopulatedEntityReferences = new HashSet<EntityReference>();
			this.AlignedEntityReferences = new HashSet<EntityReference>();
			this.TrackProcessedEntities = true;
			this.ProcessedEntities = new HashSet<IEntityWrapper>();
			this.WrappedEntities = new Dictionary<object, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
			this.OriginalMergeOption = null;
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000B08F4 File Offset: 0x000AEAF4
		internal void EndAttachTracking()
		{
			this.IsAttachTracking = false;
			this.PromotedRelationships = null;
			this.PromotedKeyEntries = null;
			this.PopulatedEntityReferences = null;
			this.AlignedEntityReferences = null;
			this.TrackProcessedEntities = false;
			this.ProcessedEntities = null;
			this.WrappedEntities = null;
			this.OriginalMergeOption = null;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000B0948 File Offset: 0x000AEB48
		internal bool BeginDetectChanges()
		{
			if (this.IsDetectChanges)
			{
				return false;
			}
			this.IsDetectChanges = true;
			this.TrackProcessedEntities = true;
			this.ProcessedEntities = new HashSet<IEntityWrapper>();
			this.WrappedEntities = new Dictionary<object, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
			this.DeletedRelationshipsByGraph = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>>();
			this.AddedRelationshipsByGraph = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>>();
			this.DeletedRelationshipsByForeignKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.AddedRelationshipsByForeignKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.AddedRelationshipsByPrincipalKey = new Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>>();
			this.ChangedForeignKeys = new Dictionary<IEntityWrapper, HashSet<RelatedEnd>>();
			return true;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000B09CC File Offset: 0x000AEBCC
		internal void EndDetectChanges()
		{
			this.IsDetectChanges = false;
			this.TrackProcessedEntities = false;
			this.ProcessedEntities = null;
			this.WrappedEntities = null;
			this.DeletedRelationshipsByGraph = null;
			this.AddedRelationshipsByGraph = null;
			this.DeletedRelationshipsByForeignKey = null;
			this.AddedRelationshipsByForeignKey = null;
			this.AddedRelationshipsByPrincipalKey = null;
			this.ChangedForeignKeys = null;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000B0A1F File Offset: 0x000AEC1F
		internal void BeginAlignChanges()
		{
			this.IsAlignChanges = true;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000B0A28 File Offset: 0x000AEC28
		internal void EndAlignChanges()
		{
			this.IsAlignChanges = false;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000B0A31 File Offset: 0x000AEC31
		internal void ResetProcessedEntities()
		{
			this.ProcessedEntities.Clear();
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000B0A3E File Offset: 0x000AEC3E
		internal void BeginLocalPublicAPI()
		{
			this.IsLocalPublicAPI = true;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000B0A47 File Offset: 0x000AEC47
		internal void EndLocalPublicAPI()
		{
			this.IsLocalPublicAPI = false;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000B0A50 File Offset: 0x000AEC50
		internal void BeginOriginalValuesGetter()
		{
			this.IsOriginalValuesGetter = true;
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000B0A59 File Offset: 0x000AEC59
		internal void EndOriginalValuesGetter()
		{
			this.IsOriginalValuesGetter = false;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000B0A62 File Offset: 0x000AEC62
		internal void BeginForeignKeyUpdate(EntityReference relationship)
		{
			this.RelationshipBeingUpdated = relationship;
			this.IsForeignKeyUpdate = true;
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000B0A72 File Offset: 0x000AEC72
		internal void EndForeignKeyUpdate()
		{
			this.RelationshipBeingUpdated = null;
			this.IsForeignKeyUpdate = false;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000B0A82 File Offset: 0x000AEC82
		internal void BeginRelatedEndAdd()
		{
			this.IsRelatedEndAdd = true;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000B0A8B File Offset: 0x000AEC8B
		internal void EndRelatedEndAdd()
		{
			this.IsRelatedEndAdd = false;
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000B0A94 File Offset: 0x000AEC94
		internal void BeginGraphUpdate()
		{
			this._graphUpdateCount++;
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000B0AA4 File Offset: 0x000AECA4
		internal void EndGraphUpdate()
		{
			this._graphUpdateCount--;
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000B0AB4 File Offset: 0x000AECB4
		internal void BeginDetaching()
		{
			this.IsDetaching = true;
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000B0ABD File Offset: 0x000AECBD
		internal void EndDetaching()
		{
			this.IsDetaching = false;
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000B0AC6 File Offset: 0x000AECC6
		internal void BeginFixupKeysByReference()
		{
			this.IsFixupByReference = true;
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000B0ACF File Offset: 0x000AECCF
		internal void EndFixupKeysByReference()
		{
			this.IsFixupByReference = false;
		}

		// Token: 0x040011A9 RID: 4521
		private MergeOption? _originalMergeOption;

		// Token: 0x040011BB RID: 4539
		private int _graphUpdateCount;
	}
}
