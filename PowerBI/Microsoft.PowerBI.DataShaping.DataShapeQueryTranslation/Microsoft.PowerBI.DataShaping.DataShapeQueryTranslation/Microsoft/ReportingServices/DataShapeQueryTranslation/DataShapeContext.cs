using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200004F RID: 79
	internal sealed class DataShapeContext
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000092C0 File Offset: 0x000074C0
		private DataShapeContext(DataShape dataShape, DataShapeAnnotations annotations, ScopeTree scopeTree, bool hasComplexSlicer)
		{
			this.m_dataShape = dataShape;
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_hasComplexSlicer = hasComplexSlicer;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000092E8 File Offset: 0x000074E8
		public static DataShapeContext Create(DataShape dataShape, DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			bool flag = annotations.HasComplexSlicer(dataShape);
			return new DataShapeContext(dataShape, annotations, scopeTree, flag);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00009306 File Offset: 0x00007506
		public DataShape DataShape
		{
			get
			{
				return this.m_dataShape;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000930E File Offset: 0x0000750E
		public Identifier Id
		{
			get
			{
				return this.m_dataShape.Id;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000931B File Offset: 0x0000751B
		internal ScopeTree ScopeTree
		{
			get
			{
				return this.m_scopeTree;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00009323 File Offset: 0x00007523
		internal DataShapeAnnotations Annotations
		{
			get
			{
				return this.m_annotations;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000932B File Offset: 0x0000752B
		public bool HasAnyPrimaryDynamic
		{
			get
			{
				return this.PrimaryDynamics.Count > 0;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000933B File Offset: 0x0000753B
		public bool HasAnySecondaryDynamic
		{
			get
			{
				return this.SecondaryDynamics.Count > 0;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000934B File Offset: 0x0000754B
		public DataMember FirstPrimaryMember
		{
			get
			{
				return this.m_dataShape.PrimaryHierarchy.DataMembers[0];
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00009363 File Offset: 0x00007563
		public DataMember FirstSecondaryMember
		{
			get
			{
				return this.m_dataShape.SecondaryHierarchy.DataMembers[0];
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000937B File Offset: 0x0000757B
		public DataMember FirstPrimaryDynamic
		{
			get
			{
				return this.PrimaryDynamics.FirstOrDefault<DataMember>();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00009388 File Offset: 0x00007588
		public DataMember LastPrimaryDynamic
		{
			get
			{
				return this.PrimaryDynamics.LastOrDefault<DataMember>();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00009395 File Offset: 0x00007595
		public DataMember FirstSecondaryDynamic
		{
			get
			{
				return this.SecondaryDynamics.FirstOrDefault<DataMember>();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000093A2 File Offset: 0x000075A2
		public DataMember LastPrimaryDynamicExcludingContextOnly
		{
			get
			{
				return this.PrimaryDynamicsExcludingContextOnly.LastOrDefault<DataMember>();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000093AF File Offset: 0x000075AF
		public DataMember LastSecondaryDynamic
		{
			get
			{
				return this.SecondaryDynamics.LastOrDefault<DataMember>();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000333 RID: 819 RVA: 0x000093BC File Offset: 0x000075BC
		public IReadOnlyList<DataMember> PrimaryDynamics
		{
			get
			{
				if (this.m_primaryDynamics == null)
				{
					this.m_primaryDynamics = this.m_dataShape.PrimaryHierarchy.GetAllDynamicMembers().ToList<DataMember>();
				}
				return this.m_primaryDynamics;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000093E7 File Offset: 0x000075E7
		public IReadOnlyList<DataMember> PrimaryDynamicsExcludingContextOnly
		{
			get
			{
				if (this.m_primaryDynamicsExcludingContextOnly == null)
				{
					this.m_primaryDynamicsExcludingContextOnly = this.ExcludeContextOnlyDataMembers(this.PrimaryDynamics);
				}
				return this.m_primaryDynamicsExcludingContextOnly;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00009409 File Offset: 0x00007609
		public IReadOnlyList<DataMember> SecondaryDynamics
		{
			get
			{
				if (this.m_secondaryDynamics == null)
				{
					this.m_secondaryDynamics = this.m_dataShape.SecondaryHierarchy.GetAllDynamicMembers().ToList<DataMember>();
				}
				return this.m_secondaryDynamics;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00009434 File Offset: 0x00007634
		public IReadOnlyList<DataMember> SecondaryDynamicsExcludingContextOnly
		{
			get
			{
				if (this.m_secondaryDynamicsExcludingContextOnly == null)
				{
					this.m_secondaryDynamicsExcludingContextOnly = this.ExcludeContextOnlyDataMembers(this.SecondaryDynamics);
				}
				return this.m_secondaryDynamicsExcludingContextOnly;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00009456 File Offset: 0x00007656
		public IReadOnlyList<DataMember> PrimaryMembers
		{
			get
			{
				if (this.m_primaryMembers == null)
				{
					this.m_primaryMembers = this.m_dataShape.PrimaryHierarchy.GetAllMembersDepthFirst().ToList<DataMember>();
				}
				return this.m_primaryMembers;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00009481 File Offset: 0x00007681
		public IReadOnlyList<DataMember> PrimaryMembersExcludingContextOnly
		{
			get
			{
				if (this.m_primaryMembersExcludingContextOnly == null)
				{
					this.m_primaryMembersExcludingContextOnly = this.ExcludeContextOnlyDataMembers(this.PrimaryMembers);
				}
				return this.m_primaryMembersExcludingContextOnly;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000094A3 File Offset: 0x000076A3
		public IReadOnlyList<DataMember> SecondaryMembers
		{
			get
			{
				if (this.m_secondaryMembers == null)
				{
					this.m_secondaryMembers = this.m_dataShape.SecondaryHierarchy.GetAllMembersDepthFirst().ToList<DataMember>();
				}
				return this.m_secondaryMembers;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000094CE File Offset: 0x000076CE
		public IReadOnlyList<DataMember> SecondaryMembersExcludingContextOnly
		{
			get
			{
				if (this.m_secondaryMembersExcludingContextOnly == null)
				{
					this.m_secondaryMembersExcludingContextOnly = this.ExcludeContextOnlyDataMembers(this.SecondaryMembers);
				}
				return this.m_secondaryMembersExcludingContextOnly;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000094F0 File Offset: 0x000076F0
		public IReadOnlyList<DataMember> AllDynamics
		{
			get
			{
				if (this.m_allDynamics == null)
				{
					this.m_allDynamics = Util.CreateList<DataMember>(new IReadOnlyList<DataMember>[] { this.PrimaryDynamics, this.SecondaryDynamics });
				}
				return this.m_allDynamics;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009523 File Offset: 0x00007723
		public IReadOnlyList<DataMember> AllDynamicsExcludingContextOnly
		{
			get
			{
				if (this.m_allDynamicsExcludingContextOnly == null)
				{
					this.m_allDynamicsExcludingContextOnly = Util.CreateList<DataMember>(new IReadOnlyList<DataMember>[] { this.PrimaryDynamicsExcludingContextOnly, this.SecondaryDynamicsExcludingContextOnly });
				}
				return this.m_allDynamicsExcludingContextOnly;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00009556 File Offset: 0x00007756
		public Limit PrimaryHierarchyLimit
		{
			get
			{
				return this.PrimaryHierarchyLimits.SingleOrDefault<Limit>();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00009563 File Offset: 0x00007763
		public IReadOnlyList<Limit> PrimaryHierarchyLimits
		{
			get
			{
				if (this.m_primaryHierarchyLimits == null)
				{
					this.m_primaryHierarchyLimits = this.GetHierarchyLimits(this.PrimaryDynamicsExcludingContextOnly);
				}
				return this.m_primaryHierarchyLimits;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00009585 File Offset: 0x00007785
		public bool HasPrimaryHierarchyLimit
		{
			get
			{
				return !this.PrimaryHierarchyLimits.IsNullOrEmpty<Limit>();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00009595 File Offset: 0x00007795
		public bool HasPrimaryScopedLimits
		{
			get
			{
				return this.PrimaryHierarchyLimits.Count > 1;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000095A5 File Offset: 0x000077A5
		public Limit SecondaryHierarchyLimit
		{
			get
			{
				return this.SecondaryHierarchyLimits.SingleOrDefault<Limit>();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000095B2 File Offset: 0x000077B2
		public IReadOnlyList<Limit> SecondaryHierarchyLimits
		{
			get
			{
				if (this.m_secondaryHierarchyLimits == null)
				{
					this.m_secondaryHierarchyLimits = this.GetHierarchyLimits(this.SecondaryDynamicsExcludingContextOnly);
				}
				return this.m_secondaryHierarchyLimits;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000095D4 File Offset: 0x000077D4
		public bool HasSecondaryHierarchyLimit
		{
			get
			{
				return !this.SecondaryHierarchyLimits.IsNullOrEmpty<Limit>();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000095E4 File Offset: 0x000077E4
		public bool HasSecondaryScopedLimits
		{
			get
			{
				return this.SecondaryHierarchyLimits.Count > 1;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000095F4 File Offset: 0x000077F4
		public bool HasAnyScopedLimits
		{
			get
			{
				return this.HasPrimaryScopedLimits || this.HasSecondaryScopedLimits;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00009608 File Offset: 0x00007808
		private IReadOnlyList<Limit> GetHierarchyLimits(IReadOnlyList<DataMember> dynamicMembers)
		{
			if (dynamicMembers.Count == 0)
			{
				return new Limit[0];
			}
			List<Limit> list = new List<Limit>();
			foreach (DataMember dataMember in dynamicMembers)
			{
				Limit limitWithInnermostTarget = this.m_annotations.GetLimitWithInnermostTarget(dataMember);
				if (limitWithInnermostTarget != null)
				{
					list.Add(limitWithInnermostTarget);
				}
			}
			return list;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00009678 File Offset: 0x00007878
		public Limit IntersectionLimit
		{
			get
			{
				if (this.m_intersectionLimit == null)
				{
					DataIntersection dataIntersection = this.InnermostScopeExcludingContextOnly as DataIntersection;
					if (dataIntersection != null)
					{
						this.m_intersectionLimit = this.m_annotations.GetLimit(dataIntersection);
					}
				}
				return this.m_intersectionLimit;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000096B4 File Offset: 0x000078B4
		public bool HasIntersectionLimit
		{
			get
			{
				return this.IntersectionLimit != null;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000096C0 File Offset: 0x000078C0
		public bool HasBinnedLineSampleLimit
		{
			get
			{
				return (this.IntersectionLimit != null && this.IntersectionLimit.Operator.ObjectType == ObjectType.BinnedLineSampleLimitOperator) || (this.PrimaryHierarchyLimits.Count == 1 && this.PrimaryHierarchyLimit.Operator.ObjectType == ObjectType.BinnedLineSampleLimitOperator);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00009710 File Offset: 0x00007910
		public bool HasOverlappingPointsSampleLimit
		{
			get
			{
				return (this.IntersectionLimit != null && this.IntersectionLimit.Operator.ObjectType == ObjectType.OverlappingPointsSampleLimitOperator) || (this.PrimaryHierarchyLimits.Count == 1 && this.PrimaryHierarchyLimit.Operator.ObjectType == ObjectType.OverlappingPointsSampleLimitOperator);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000975F File Offset: 0x0000795F
		public bool HasTopNPerLevelSampleLimit
		{
			get
			{
				return this.PrimaryHierarchyLimits.Count == 1 && this.PrimaryHierarchyLimit.Operator.ObjectType == ObjectType.TopNPerLevelLimitOperator;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00009785 File Offset: 0x00007985
		public IScope InnermostScope
		{
			get
			{
				if (this.m_innermostScope == null)
				{
					this.m_innermostScope = this.m_scopeTree.GetInnermostScopeInDataShape(this.m_dataShape);
				}
				return this.m_innermostScope;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000097AC File Offset: 0x000079AC
		public IScope InnermostScopeExcludingContextOnly
		{
			get
			{
				if (this.m_innermostScopeExcludingContextOnly == null)
				{
					this.m_innermostScopeExcludingContextOnly = this.m_scopeTree.GetInnermostScopeExcludingContextOnlyInDataShape(this.m_dataShape);
				}
				return this.m_innermostScopeExcludingContextOnly;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000097D3 File Offset: 0x000079D3
		public IReadOnlyList<IScope> RowScopes
		{
			get
			{
				if (this.m_rowScopes == null)
				{
					this.m_rowScopes = this.InnermostScope.GetAllParentScopesWithoutTopDataShapes(this.m_scopeTree).ToList<IScope>();
				}
				return this.m_rowScopes;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000097FF File Offset: 0x000079FF
		public bool HasComplexSlicer
		{
			get
			{
				return this.m_hasComplexSlicer;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009807 File Offset: 0x00007A07
		public IReadOnlyList<DataShape> InputSubqueryDataShapes
		{
			get
			{
				return this.m_annotations.InputSubqueryDataShapes(this.DataShape);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000981A File Offset: 0x00007A1A
		public bool HasDataShapeAggregatesAndProjections
		{
			get
			{
				return this.DataShapeAggregatesAndProjections.Count > 0;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000982A File Offset: 0x00007A2A
		public bool HasGroupingStructureAggregates
		{
			get
			{
				return this.m_annotations.HasGroupingStructureAggregates(this.DataShape);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000983D File Offset: 0x00007A3D
		public bool HasDataShapeProjections
		{
			get
			{
				return this.DataShapeProjections.Count > 0;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000984D File Offset: 0x00007A4D
		public bool HasDataTransforms
		{
			get
			{
				return !this.m_dataShape.Transforms.IsNullOrEmpty<DataTransform>();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009862 File Offset: 0x00007A62
		public bool HasDetailGroups
		{
			get
			{
				return this.DetailIdentities.Count >= 1;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009878 File Offset: 0x00007A78
		public IReadOnlyList<DetailGroupIdentity> DetailIdentities
		{
			get
			{
				if (this.m_detailGroups == null)
				{
					this.m_detailGroups = this.AllDynamics.Select((DataMember d) => d.Group.DetailGroupIdentity).WhereNonNull<DetailGroupIdentity>().EvaluateReadOnly<DetailGroupIdentity>();
				}
				return this.m_detailGroups;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000098CD File Offset: 0x00007ACD
		private Dictionary<Identifier, DataShapeContext> NestedDataShapeContexts
		{
			get
			{
				if (this.m_nestedDataShapeContexts == null)
				{
					this.m_nestedDataShapeContexts = new Dictionary<Identifier, DataShapeContext>();
				}
				return this.m_nestedDataShapeContexts;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000098E8 File Offset: 0x00007AE8
		public DataShapeContext GetNestedDataShapeContext(DataShape dataShape)
		{
			DataShapeContext dataShapeContext;
			if (!this.NestedDataShapeContexts.TryGetValue(dataShape.Id, out dataShapeContext))
			{
				dataShapeContext = DataShapeContext.Create(dataShape, this.m_annotations, this.m_scopeTree);
				this.NestedDataShapeContexts.Add(dataShape.Id, dataShapeContext);
			}
			return dataShapeContext;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009930 File Offset: 0x00007B30
		public DataShapeContext GetNestedDataShapeContextOrSelf(DataShape dataShape)
		{
			if (dataShape == this.DataShape)
			{
				return this;
			}
			return this.GetNestedDataShapeContext(dataShape);
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00009944 File Offset: 0x00007B44
		public IReadOnlyList<Calculation> DataShapeAggregatesAndProjections
		{
			get
			{
				if (this.m_dataShapeAggregatesAndProjections == null)
				{
					this.m_dataShapeAggregatesAndProjections = (from a in this.m_annotations.GetDataShapeAggregatesAndProjections(this.DataShape)
						where !this.m_annotations.IsSubtotal(a) && !this.m_annotations.CanBeHandledByProcessing(a) && !this.m_annotations.IsVisualCalculation(a)
						select a).ToList<Calculation>();
				}
				return this.m_dataShapeAggregatesAndProjections;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00009981 File Offset: 0x00007B81
		public IReadOnlyList<Calculation> DataShapeAggregatesOverScopes
		{
			get
			{
				if (this.m_dataShapeAggregatesOverScopes == null)
				{
					this.m_dataShapeAggregatesOverScopes = this.m_annotations.GetDataShapeAggregatesOverScopes(this.DataShape);
				}
				return this.m_dataShapeAggregatesOverScopes;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000099A8 File Offset: 0x00007BA8
		public IReadOnlyList<Calculation> DataShapeProjections
		{
			get
			{
				if (this.m_dataShapeProjections == null)
				{
					this.m_dataShapeProjections = this.DataShapeAggregatesAndProjections.Except(this.DataShapeAggregatesOverScopes).ToList<Calculation>();
				}
				return this.m_dataShapeProjections;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000099D4 File Offset: 0x00007BD4
		public bool HasAnyNestedDataShape
		{
			get
			{
				return !this.DataShape.DataShapes.IsNullOrEmpty<DataShape>();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000099EC File Offset: 0x00007BEC
		public bool HasSortByMeasure
		{
			get
			{
				if (this.m_hasSortByMeasure == null)
				{
					this.m_hasSortByMeasure = new bool?(false);
					foreach (DataMember dataMember in this.AllDynamics)
					{
						if (this.m_annotations.DataMemberAnnotations.HasSortByMeasureKeys(dataMember))
						{
							this.m_hasSortByMeasure = new bool?(true);
							break;
						}
					}
				}
				return this.m_hasSortByMeasure.Value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00009A78 File Offset: 0x00007C78
		public bool HasShowItemsWithNoData
		{
			get
			{
				if (this.m_hasShowItemsWithNoData == null)
				{
					this.m_hasShowItemsWithNoData = new bool?(this.AllDynamics.Any((DataMember d) => d.UsesShowItemsWithNoData()));
				}
				return this.m_hasShowItemsWithNoData.Value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public bool HasTotals
		{
			get
			{
				if (this.m_hasTotals == null)
				{
					IEnumerable<DataMember> allMembersDepthFirst = this.DataShape.PrimaryHierarchy.GetAllMembersDepthFirst();
					IEnumerable<DataMember> allMembersDepthFirst2 = this.DataShape.SecondaryHierarchy.GetAllMembersDepthFirst();
					BatchSubtotalAnnotation subtotalAnnotation;
					this.m_hasTotals = new bool?(allMembersDepthFirst.Concat(allMembersDepthFirst2).Any((DataMember m) => this.Annotations.TryGetBatchSubtotalSourceAnnotation(m, out subtotalAnnotation)));
				}
				return this.m_hasTotals.Value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00009B4B File Offset: 0x00007D4B
		public bool HasAllPrimaryOutputTotals
		{
			get
			{
				if (this.m_allPrimariesHaveTotals == null)
				{
					this.m_allPrimariesHaveTotals = new bool?(this.PrimaryDynamics.All((DataMember m) => m.HasOutputTotal(this.Annotations)));
				}
				return this.m_allPrimariesHaveTotals.Value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00009B87 File Offset: 0x00007D87
		public bool HasAllSecondaryOutputTotals
		{
			get
			{
				if (this.m_allSecondariesHaveTotals == null)
				{
					this.m_allSecondariesHaveTotals = new bool?(this.SecondaryDynamics.All((DataMember m) => m.HasOutputTotal(this.Annotations)));
				}
				return this.m_allSecondariesHaveTotals.Value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00009BC4 File Offset: 0x00007DC4
		public bool HasPrimaryInstanceFilters
		{
			get
			{
				if (this.m_hasPrimaryInstanceFilters == null)
				{
					this.m_hasPrimaryInstanceFilters = new bool?(this.PrimaryDynamics.Any((DataMember d) => !d.InstanceFilters.IsNullOrEmpty<FilterCondition>()));
				}
				return this.m_hasPrimaryInstanceFilters.Value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00009C20 File Offset: 0x00007E20
		public bool HasSecondaryInstanceFilters
		{
			get
			{
				if (this.m_hasSecondaryInstanceFilters == null)
				{
					this.m_hasSecondaryInstanceFilters = new bool?(this.SecondaryDynamics.Any((DataMember d) => !d.InstanceFilters.IsNullOrEmpty<FilterCondition>()));
				}
				return this.m_hasSecondaryInstanceFilters.Value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00009C7A File Offset: 0x00007E7A
		public bool HasInstanceFilters
		{
			get
			{
				return this.HasPrimaryInstanceFilters || this.HasSecondaryInstanceFilters;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00009C8C File Offset: 0x00007E8C
		public bool HasSynchronizationDataShapes
		{
			get
			{
				return !this.SynchronizationDataShapes.IsNullOrEmpty<DataShape>();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00009C9C File Offset: 0x00007E9C
		public IReadOnlyList<DataShape> SynchronizationDataShapes
		{
			get
			{
				if (this.m_synchronizationDataShapes == null && !this.DataShape.DataShapes.IsNullOrEmpty<DataShape>())
				{
					this.m_synchronizationDataShapes = this.DataShape.DataShapes.Where((DataShape d) => d.Usage == DataShapeUsage.Synchronization).EvaluateReadOnly<DataShape>();
				}
				return this.m_synchronizationDataShapes;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00009D03 File Offset: 0x00007F03
		public bool HasAnyVisualCalculations
		{
			get
			{
				return this.m_annotations.GetVisualCalculations(this.DataShape).Any<Calculation>();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00009D1B File Offset: 0x00007F1B
		public bool HasAnyContextOnlyCalculations
		{
			get
			{
				return this.m_annotations.GetDataShapeAnnotation(this.DataShape).HasContextOnlyCalculations;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00009D33 File Offset: 0x00007F33
		public bool HasAnyContextOnlyDataMembers
		{
			get
			{
				return this.m_annotations.GetDataShapeAnnotation(this.DataShape).HasContextOnlyDataMemebers;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00009D4C File Offset: 0x00007F4C
		public DataShapeContext GetSynchronizationDataShapeContext(Calculation syncIndexCalculation)
		{
			DataShape dataShape = this.Annotations.GetReferencedScopes(syncIndexCalculation).Single("SynchronizedIndex calculations can only and must reference a single scope.", Array.Empty<string>()) as DataShape;
			Contract.RetailAssert(dataShape != null, "SynchronizedIndex calculations only reference a DataShape.");
			return this.GetNestedDataShapeContext(dataShape);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009D90 File Offset: 0x00007F90
		private IReadOnlyList<DataMember> ExcludeContextOnlyDataMembers(IReadOnlyList<DataMember> singleHierarchyDataMembers)
		{
			if (this.HasAnyContextOnlyDataMembers)
			{
				if (!singleHierarchyDataMembers.All((DataMember member) => !member.ContextOnly))
				{
					return singleHierarchyDataMembers.Where((DataMember member) => !member.ContextOnly).ToList<DataMember>();
				}
			}
			return singleHierarchyDataMembers;
		}

		// Token: 0x0400016C RID: 364
		private readonly DataShape m_dataShape;

		// Token: 0x0400016D RID: 365
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400016E RID: 366
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400016F RID: 367
		private readonly bool m_hasComplexSlicer;

		// Token: 0x04000170 RID: 368
		private bool? m_hasSortByMeasure;

		// Token: 0x04000171 RID: 369
		private bool? m_hasShowItemsWithNoData;

		// Token: 0x04000172 RID: 370
		private bool? m_hasTotals;

		// Token: 0x04000173 RID: 371
		private bool? m_allPrimariesHaveTotals;

		// Token: 0x04000174 RID: 372
		private bool? m_allSecondariesHaveTotals;

		// Token: 0x04000175 RID: 373
		private bool? m_hasPrimaryInstanceFilters;

		// Token: 0x04000176 RID: 374
		private bool? m_hasSecondaryInstanceFilters;

		// Token: 0x04000177 RID: 375
		private IReadOnlyList<DataMember> m_primaryDynamics;

		// Token: 0x04000178 RID: 376
		private IReadOnlyList<DataMember> m_primaryMembers;

		// Token: 0x04000179 RID: 377
		private IReadOnlyList<DataMember> m_secondaryDynamics;

		// Token: 0x0400017A RID: 378
		private IReadOnlyList<DataMember> m_secondaryMembers;

		// Token: 0x0400017B RID: 379
		private IReadOnlyList<DataMember> m_allDynamics;

		// Token: 0x0400017C RID: 380
		private IReadOnlyList<DataMember> m_primaryDynamicsExcludingContextOnly;

		// Token: 0x0400017D RID: 381
		private IReadOnlyList<DataMember> m_primaryMembersExcludingContextOnly;

		// Token: 0x0400017E RID: 382
		private IReadOnlyList<DataMember> m_secondaryDynamicsExcludingContextOnly;

		// Token: 0x0400017F RID: 383
		private IReadOnlyList<DataMember> m_secondaryMembersExcludingContextOnly;

		// Token: 0x04000180 RID: 384
		private IReadOnlyList<DataMember> m_allDynamicsExcludingContextOnly;

		// Token: 0x04000181 RID: 385
		private IReadOnlyList<Limit> m_primaryHierarchyLimits;

		// Token: 0x04000182 RID: 386
		private IReadOnlyList<Limit> m_secondaryHierarchyLimits;

		// Token: 0x04000183 RID: 387
		private Limit m_intersectionLimit;

		// Token: 0x04000184 RID: 388
		private IScope m_innermostScope;

		// Token: 0x04000185 RID: 389
		private IScope m_innermostScopeExcludingContextOnly;

		// Token: 0x04000186 RID: 390
		private IReadOnlyList<IScope> m_rowScopes;

		// Token: 0x04000187 RID: 391
		private IReadOnlyList<Calculation> m_dataShapeAggregatesAndProjections;

		// Token: 0x04000188 RID: 392
		private IReadOnlyList<Calculation> m_dataShapeAggregatesOverScopes;

		// Token: 0x04000189 RID: 393
		private IReadOnlyList<Calculation> m_dataShapeProjections;

		// Token: 0x0400018A RID: 394
		private Dictionary<Identifier, DataShapeContext> m_nestedDataShapeContexts;

		// Token: 0x0400018B RID: 395
		private IReadOnlyList<DetailGroupIdentity> m_detailGroups;

		// Token: 0x0400018C RID: 396
		private IReadOnlyList<DataShape> m_synchronizationDataShapes;
	}
}
