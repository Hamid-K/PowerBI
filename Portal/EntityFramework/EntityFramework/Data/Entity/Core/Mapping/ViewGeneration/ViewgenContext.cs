using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200056C RID: 1388
	internal class ViewgenContext : InternalBase
	{
		// Token: 0x06004391 RID: 17297 RVA: 0x000EADD0 File Offset: 0x000E8FD0
		internal ViewgenContext(ViewTarget viewTarget, EntitySetBase extent, IList<Cell> extentCells, CqlIdentifiers identifiers, ConfigViewGenerator config, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap, EntityContainerMapping entityContainerMapping)
		{
			foreach (Cell cell in extentCells)
			{
			}
			this.m_extent = extent;
			this.m_viewTarget = viewTarget;
			this.m_config = config;
			this.m_edmItemCollection = entityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
			this.m_entityContainerMapping = entityContainerMapping;
			this.m_identifiers = identifiers;
			updateDomainMap = updateDomainMap.MakeCopy();
			MemberDomainMap memberDomainMap = ((viewTarget == ViewTarget.QueryView) ? queryDomainMap : updateDomainMap);
			this.m_memberMaps = new MemberMaps(viewTarget, MemberProjectionIndex.Create(extent, this.m_edmItemCollection), queryDomainMap, updateDomainMap);
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport = new FragmentQueryKBChaseSupport();
			fragmentQueryKBChaseSupport.CreateVariableConstraints(extent, memberDomainMap, this.m_edmItemCollection);
			this.m_leftFragmentQP = new FragmentQueryProcessor(fragmentQueryKBChaseSupport);
			this.m_rewritingCache = new Dictionary<FragmentQuery, Tile<FragmentQuery>>(FragmentQuery.GetEqualityComparer(this.m_leftFragmentQP));
			if (!this.CreateLeftCellWrappers(extentCells, viewTarget))
			{
				return;
			}
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport2 = new FragmentQueryKBChaseSupport();
			MemberDomainMap memberDomainMap2 = ((viewTarget == ViewTarget.QueryView) ? updateDomainMap : queryDomainMap);
			foreach (LeftCellWrapper leftCellWrapper in this.m_cellWrappers)
			{
				EntitySetBase rightExtent = leftCellWrapper.RightExtent;
				fragmentQueryKBChaseSupport2.CreateVariableConstraints(rightExtent, memberDomainMap2, this.m_edmItemCollection);
				fragmentQueryKBChaseSupport2.CreateAssociationConstraints(rightExtent, memberDomainMap2, this.m_edmItemCollection);
			}
			if (this.m_viewTarget == ViewTarget.UpdateView)
			{
				this.CreateConstraintsForForeignKeyAssociationsAffectingThisWrapper(fragmentQueryKBChaseSupport2, memberDomainMap2);
			}
			this.m_rightFragmentQP = new FragmentQueryProcessor(fragmentQueryKBChaseSupport2);
			if (this.m_viewTarget == ViewTarget.QueryView)
			{
				this.CheckConcurrencyControlTokens();
			}
			this.m_cellWrappers.Sort(LeftCellWrapper.Comparer);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000EAF70 File Offset: 0x000E9170
		private void CreateConstraintsForForeignKeyAssociationsAffectingThisWrapper(FragmentQueryKB rightKB, MemberDomainMap rightDomainMap)
		{
			foreach (AssociationSet associationSet in new ViewgenContext.OneToOneFkAssociationsForEntitiesFilter().Filter((from it in this.m_cellWrappers.Select((LeftCellWrapper it) => it.RightExtent).OfType<EntitySet>()
				select it.ElementType).ToList<EntityType>(), this.m_entityContainerMapping.EdmEntityContainer.BaseEntitySets.OfType<AssociationSet>()))
			{
				rightKB.CreateEquivalenceConstraintForOneToOneForeignKeyAssociation(associationSet, rightDomainMap);
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004393 RID: 17299 RVA: 0x000EB030 File Offset: 0x000E9230
		internal ViewTarget ViewTarget
		{
			get
			{
				return this.m_viewTarget;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000EB038 File Offset: 0x000E9238
		internal MemberMaps MemberMaps
		{
			get
			{
				return this.m_memberMaps;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06004395 RID: 17301 RVA: 0x000EB040 File Offset: 0x000E9240
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000EB048 File Offset: 0x000E9248
		internal ConfigViewGenerator Config
		{
			get
			{
				return this.m_config;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x000EB050 File Offset: 0x000E9250
		internal CqlIdentifiers CqlIdentifiers
		{
			get
			{
				return this.m_identifiers;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x000EB058 File Offset: 0x000E9258
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this.m_edmItemCollection;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x000EB060 File Offset: 0x000E9260
		internal FragmentQueryProcessor LeftFragmentQP
		{
			get
			{
				return this.m_leftFragmentQP;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600439A RID: 17306 RVA: 0x000EB068 File Offset: 0x000E9268
		internal FragmentQueryProcessor RightFragmentQP
		{
			get
			{
				return this.m_rightFragmentQP;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600439B RID: 17307 RVA: 0x000EB070 File Offset: 0x000E9270
		internal List<LeftCellWrapper> AllWrappersForExtent
		{
			get
			{
				return this.m_cellWrappers;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x000EB078 File Offset: 0x000E9278
		internal EntityContainerMapping EntityContainerMapping
		{
			get
			{
				return this.m_entityContainerMapping;
			}
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000EB080 File Offset: 0x000E9280
		internal bool TryGetCachedRewriting(FragmentQuery query, out Tile<FragmentQuery> rewriting)
		{
			return this.m_rewritingCache.TryGetValue(query, out rewriting);
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000EB08F File Offset: 0x000E928F
		internal void SetCachedRewriting(FragmentQuery query, Tile<FragmentQuery> rewriting)
		{
			this.m_rewritingCache[query] = rewriting;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000EB0A0 File Offset: 0x000E92A0
		private void CheckConcurrencyControlTokens()
		{
			EntityTypeBase elementType = this.m_extent.ElementType;
			Set<EdmMember> concurrencyMembersForTypeHierarchy = MetadataHelper.GetConcurrencyMembersForTypeHierarchy(elementType, this.m_edmItemCollection);
			Set<MemberPath> set = new Set<MemberPath>(MemberPath.EqualityComparer);
			foreach (EdmMember edmMember in concurrencyMembersForTypeHierarchy)
			{
				if (!edmMember.DeclaringType.IsAssignableFrom(elementType))
				{
					string text = Strings.ViewGen_Concurrency_Derived_Class(edmMember.Name, edmMember.DeclaringType.Name, this.m_extent);
					ExceptionHelpers.ThrowMappingException(new ErrorLog.Record(ViewGenErrorCode.ConcurrencyDerivedClass, text, this.m_cellWrappers, string.Empty), this.m_config);
				}
				set.Add(new MemberPath(this.m_extent, edmMember));
			}
			if (concurrencyMembersForTypeHierarchy.Count > 0)
			{
				foreach (LeftCellWrapper leftCellWrapper in this.m_cellWrappers)
				{
					Set<MemberPath> set2 = new Set<MemberPath>(leftCellWrapper.OnlyInputCell.CQuery.WhereClause.MemberRestrictions.Select((MemberRestriction oneOf) => oneOf.RestrictedMemberSlot.MemberPath), MemberPath.EqualityComparer);
					set2.Intersect(set);
					if (set2.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine(Strings.ViewGen_Concurrency_Invalid_Condition(MemberPath.PropertiesToUserString(set2, false), this.m_extent.Name));
						ExceptionHelpers.ThrowMappingException(new ErrorLog.Record(ViewGenErrorCode.ConcurrencyTokenHasCondition, stringBuilder.ToString(), new LeftCellWrapper[] { leftCellWrapper }, string.Empty), this.m_config);
					}
				}
			}
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000EB26C File Offset: 0x000E946C
		private bool CreateLeftCellWrappers(IList<Cell> extentCells, ViewTarget viewTarget)
		{
			List<Cell> list = ViewgenContext.AlignFields(extentCells, this.m_memberMaps.ProjectedSlotMap, viewTarget);
			this.m_cellWrappers = new List<LeftCellWrapper>();
			for (int i = 0; i < list.Count; i++)
			{
				Cell cell = list[i];
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				CellQuery rightQuery = cell.GetRightQuery(viewTarget);
				Set<MemberPath> nonNullSlots = leftQuery.GetNonNullSlots();
				FragmentQuery fragmentQuery = FragmentQuery.Create(BoolExpression.CreateLiteral(new CellIdBoolean(this.m_identifiers, extentCells[i].CellNumber), this.m_memberMaps.LeftDomainMap), leftQuery);
				if (viewTarget == ViewTarget.UpdateView)
				{
					fragmentQuery = this.m_leftFragmentQP.CreateDerivedViewBySelectingConstantAttributes(fragmentQuery) ?? fragmentQuery;
				}
				LeftCellWrapper leftCellWrapper = new LeftCellWrapper(this.m_viewTarget, nonNullSlots, fragmentQuery, leftQuery, rightQuery, this.m_memberMaps, extentCells[i]);
				this.m_cellWrappers.Add(leftCellWrapper);
			}
			return true;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000EB344 File Offset: 0x000E9544
		private static List<Cell> AlignFields(IEnumerable<Cell> cells, MemberProjectionIndex projectedSlotMap, ViewTarget viewTarget)
		{
			List<Cell> list = new List<Cell>();
			foreach (Cell cell in cells)
			{
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				CellQuery rightQuery = cell.GetRightQuery(viewTarget);
				CellQuery cellQuery;
				CellQuery cellQuery2;
				leftQuery.CreateFieldAlignedCellQueries(rightQuery, projectedSlotMap, out cellQuery, out cellQuery2);
				Cell cell2 = ((viewTarget == ViewTarget.QueryView) ? Cell.CreateCS(cellQuery, cellQuery2, cell.CellLabel, cell.CellNumber) : Cell.CreateCS(cellQuery2, cellQuery, cell.CellLabel, cell.CellNumber));
				list.Add(cell2);
			}
			return list;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000EB3E0 File Offset: 0x000E95E0
		internal override void ToCompactString(StringBuilder builder)
		{
			LeftCellWrapper.WrappersToStringBuilder(builder, this.m_cellWrappers, "Left Cell Wrappers");
		}

		// Token: 0x0400183A RID: 6202
		private readonly ConfigViewGenerator m_config;

		// Token: 0x0400183B RID: 6203
		private readonly ViewTarget m_viewTarget;

		// Token: 0x0400183C RID: 6204
		private readonly EntitySetBase m_extent;

		// Token: 0x0400183D RID: 6205
		private readonly MemberMaps m_memberMaps;

		// Token: 0x0400183E RID: 6206
		private readonly EdmItemCollection m_edmItemCollection;

		// Token: 0x0400183F RID: 6207
		private readonly EntityContainerMapping m_entityContainerMapping;

		// Token: 0x04001840 RID: 6208
		private List<LeftCellWrapper> m_cellWrappers;

		// Token: 0x04001841 RID: 6209
		private readonly FragmentQueryProcessor m_leftFragmentQP;

		// Token: 0x04001842 RID: 6210
		private readonly FragmentQueryProcessor m_rightFragmentQP;

		// Token: 0x04001843 RID: 6211
		private readonly CqlIdentifiers m_identifiers;

		// Token: 0x04001844 RID: 6212
		private readonly Dictionary<FragmentQuery, Tile<FragmentQuery>> m_rewritingCache;

		// Token: 0x02000B7D RID: 2941
		internal class OneToOneFkAssociationsForEntitiesFilter
		{
			// Token: 0x06006656 RID: 26198 RVA: 0x0015F628 File Offset: 0x0015D828
			public virtual IEnumerable<AssociationSet> Filter(IList<EntityType> entityTypes, IEnumerable<AssociationSet> associationSets)
			{
				Func<AssociationEndMember, bool> <>9__1;
				return associationSets.Where(delegate(AssociationSet a)
				{
					if (a.ElementType.IsForeignKey)
					{
						IEnumerable<AssociationEndMember> associationEndMembers = a.ElementType.AssociationEndMembers;
						Func<AssociationEndMember, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (AssociationEndMember aem) => aem.RelationshipMultiplicity == RelationshipMultiplicity.One && entityTypes.Contains(aem.GetEntityType()));
						}
						return associationEndMembers.All(func);
					}
					return false;
				});
			}
		}
	}
}
