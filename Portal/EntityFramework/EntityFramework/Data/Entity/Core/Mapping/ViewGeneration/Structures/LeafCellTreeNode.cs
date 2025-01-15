using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A3 RID: 1443
	internal class LeafCellTreeNode : CellTreeNode
	{
		// Token: 0x060045F1 RID: 17905 RVA: 0x000F6AB0 File Offset: 0x000F4CB0
		internal LeafCellTreeNode(ViewgenContext context, LeftCellWrapper cellWrapper)
			: base(context)
		{
			this.m_cellWrapper = cellWrapper;
			this.m_rightFragmentQuery = FragmentQuery.Create(cellWrapper.OriginalCellNumberString, cellWrapper.CreateRoleBoolean(), cellWrapper.RightCellQuery);
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x000F6ADD File Offset: 0x000F4CDD
		internal LeafCellTreeNode(ViewgenContext context, LeftCellWrapper cellWrapper, FragmentQuery rightFragmentQuery)
			: base(context)
		{
			this.m_cellWrapper = cellWrapper;
			this.m_rightFragmentQuery = rightFragmentQuery;
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x000F6AF4 File Offset: 0x000F4CF4
		internal LeftCellWrapper LeftCellWrapper
		{
			get
			{
				return this.m_cellWrapper;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x000F6AFC File Offset: 0x000F4CFC
		internal override MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_cellWrapper.RightDomainMap;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x000F6B09 File Offset: 0x000F4D09
		internal override FragmentQuery LeftFragmentQuery
		{
			get
			{
				return this.m_cellWrapper.FragmentQuery;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x000F6B16 File Offset: 0x000F4D16
		internal override FragmentQuery RightFragmentQuery
		{
			get
			{
				return this.m_rightFragmentQuery;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000F6B1E File Offset: 0x000F4D1E
		internal override Set<MemberPath> Attributes
		{
			get
			{
				return this.m_cellWrapper.Attributes;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000F6B2B File Offset: 0x000F4D2B
		internal override List<CellTreeNode> Children
		{
			get
			{
				return new List<CellTreeNode>();
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x000F6B32 File Offset: 0x000F4D32
		internal override CellTreeOpType OpType
		{
			get
			{
				return CellTreeOpType.Leaf;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x000F6B35 File Offset: 0x000F4D35
		internal override int NumProjectedSlots
		{
			get
			{
				return this.LeftCellWrapper.RightCellQuery.NumProjectedSlots;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000F6B47 File Offset: 0x000F4D47
		internal override int NumBoolSlots
		{
			get
			{
				return this.LeftCellWrapper.RightCellQuery.NumBoolVars;
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000F6B59 File Offset: 0x000F4D59
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitLeaf(this, param);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000F6B63 File Offset: 0x000F4D63
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitLeaf(this, param);
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000F6B70 File Offset: 0x000F4D70
		internal override bool IsProjectedSlot(int slot)
		{
			CellQuery rightCellQuery = this.LeftCellWrapper.RightCellQuery;
			if (base.IsBoolSlot(slot))
			{
				return rightCellQuery.GetBoolVar(base.SlotToBoolIndex(slot)) != null;
			}
			return rightCellQuery.ProjectedSlotAt(slot) != null;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000F6BB0 File Offset: 0x000F4DB0
		internal override CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			int num = requiredSlots.Length;
			CellQuery rightCellQuery = this.LeftCellWrapper.RightCellQuery;
			SlotInfo[] array = new SlotInfo[num];
			for (int i = 0; i < rightCellQuery.NumProjectedSlots; i++)
			{
				ProjectedSlot projectedSlot = rightCellQuery.ProjectedSlotAt(i);
				if (requiredSlots[i] && projectedSlot == null)
				{
					ConstantProjectedSlot constantProjectedSlot = new ConstantProjectedSlot(Domain.GetDefaultValueForMemberPath(base.ProjectedSlotMap[i], base.GetLeaves(), base.ViewgenContext.Config));
					rightCellQuery.FixMissingSlotAsDefaultConstant(i, constantProjectedSlot);
					projectedSlot = constantProjectedSlot;
				}
				SlotInfo slotInfo = new SlotInfo(requiredSlots[i], projectedSlot != null, projectedSlot, base.ProjectedSlotMap[i]);
				array[i] = slotInfo;
			}
			for (int j = 0; j < rightCellQuery.NumBoolVars; j++)
			{
				BoolExpression boolVar = rightCellQuery.GetBoolVar(j);
				BooleanProjectedSlot booleanProjectedSlot;
				if (boolVar != null)
				{
					booleanProjectedSlot = new BooleanProjectedSlot(boolVar, identifiers, j);
				}
				else
				{
					booleanProjectedSlot = new BooleanProjectedSlot(BoolExpression.False, identifiers, j);
				}
				int num2 = base.BoolIndexToSlot(j);
				SlotInfo slotInfo2 = new SlotInfo(requiredSlots[num2], boolVar != null, booleanProjectedSlot, null);
				array[num2] = slotInfo2;
			}
			IEnumerable<SlotInfo> enumerable = array;
			if (rightCellQuery.Extent.EntityContainer.DataSpace == DataSpace.SSpace && this.m_cellWrapper.LeftExtent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
			{
				IEnumerable<AssociationSetMapping> relationshipSetMappingsFor = base.ViewgenContext.EntityContainerMapping.GetRelationshipSetMappingsFor(this.m_cellWrapper.LeftExtent, rightCellQuery.Extent);
				List<SlotInfo> list = new List<SlotInfo>();
				using (IEnumerator<AssociationSetMapping> enumerator = relationshipSetMappingsFor.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WithRelationship withRelationship;
						if (LeafCellTreeNode.TryGetWithRelationship(enumerator.Current, this.m_cellWrapper.LeftExtent, rightCellQuery.SourceExtentMemberPath, ref list, out withRelationship))
						{
							withRelationships.Add(withRelationship);
							enumerable = array.Concat(list);
						}
					}
				}
			}
			EntitySetBase extent = rightCellQuery.Extent;
			CellQuery.SelectDistinct selectDistinctFlag = rightCellQuery.SelectDistinctFlag;
			SlotInfo[] array2 = enumerable.ToArray<SlotInfo>();
			BoolExpression whereClause = rightCellQuery.WhereClause;
			int num3 = blockAliasNum + 1;
			blockAliasNum = num3;
			return new ExtentCqlBlock(extent, selectDistinctFlag, array2, whereClause, identifiers, num3);
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000F6D94 File Offset: 0x000F4F94
		private static bool TryGetWithRelationship(AssociationSetMapping collocatedAssociationSetMap, EntitySetBase thisExtent, MemberPath sRootNode, ref List<SlotInfo> foreignKeySlots, out WithRelationship withRelationship)
		{
			withRelationship = null;
			EndPropertyMapping foreignKeyEndMapFromAssociationMap = LeafCellTreeNode.GetForeignKeyEndMapFromAssociationMap(collocatedAssociationSetMap);
			if (foreignKeyEndMapFromAssociationMap == null || foreignKeyEndMapFromAssociationMap.AssociationEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				return false;
			}
			AssociationEndMember associationEnd = foreignKeyEndMapFromAssociationMap.AssociationEnd;
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(associationEnd);
			EntityType entityType = (EntityType)((RefType)associationEnd.TypeUsage.EdmType).ElementType;
			EntityType entityType2 = (EntityType)((RefType)otherAssociationEnd.TypeUsage.EdmType).ElementType;
			AssociationSet associationSet = (AssociationSet)collocatedAssociationSetMap.Set;
			MemberPath memberPath = new MemberPath(associationSet, associationEnd);
			IEnumerable<ScalarPropertyMapping> enumerable = foreignKeyEndMapFromAssociationMap.PropertyMappings.Cast<ScalarPropertyMapping>();
			List<MemberPath> list = new List<MemberPath>();
			using (ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = entityType.KeyMembers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty edmProperty = (EdmProperty)enumerator.Current;
					ScalarPropertyMapping scalarPropertyMapping = enumerable.Where((ScalarPropertyMapping propMap) => propMap.Property.Equals(edmProperty)).First<ScalarPropertyMapping>();
					MemberProjectedSlot memberProjectedSlot = new MemberProjectedSlot(new MemberPath(sRootNode, scalarPropertyMapping.Column));
					MemberPath memberPath2 = new MemberPath(memberPath, edmProperty);
					list.Add(memberPath2);
					foreignKeySlots.Add(new SlotInfo(true, true, memberProjectedSlot, memberPath2));
				}
			}
			if (thisExtent.ElementType.IsAssignableFrom(entityType2))
			{
				withRelationship = new WithRelationship(associationSet, otherAssociationEnd, entityType2, associationEnd, entityType, list);
				return true;
			}
			return false;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000F6F00 File Offset: 0x000F5100
		private static EndPropertyMapping GetForeignKeyEndMapFromAssociationMap(AssociationSetMapping collocatedAssociationSetMap)
		{
			MappingFragment mappingFragment = collocatedAssociationSetMap.TypeMappings.First<TypeMapping>().MappingFragments.First<MappingFragment>();
			IEnumerable<EdmMember> keyMembers = collocatedAssociationSetMap.StoreEntitySet.ElementType.KeyMembers;
			using (IEnumerator<PropertyMapping> enumerator = mappingFragment.PropertyMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EndPropertyMapping endMap = (EndPropertyMapping)enumerator.Current;
					if (endMap.StoreProperties.SequenceEqual(keyMembers, EqualityComparer<EdmMember>.Default))
					{
						return (from eMap in mappingFragment.PropertyMappings.OfType<EndPropertyMapping>()
							where !eMap.Equals(endMap)
							select eMap).First<EndPropertyMapping>();
					}
				}
			}
			return null;
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000F6FC0 File Offset: 0x000F51C0
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			this.m_cellWrapper.ToCompactString(stringBuilder);
		}

		// Token: 0x04001902 RID: 6402
		internal static readonly IEqualityComparer<LeafCellTreeNode> EqualityComparer = new LeafCellTreeNode.LeafCellTreeNodeComparer();

		// Token: 0x04001903 RID: 6403
		private readonly LeftCellWrapper m_cellWrapper;

		// Token: 0x04001904 RID: 6404
		private readonly FragmentQuery m_rightFragmentQuery;

		// Token: 0x02000BCC RID: 3020
		private class LeafCellTreeNodeComparer : IEqualityComparer<LeafCellTreeNode>
		{
			// Token: 0x060067FD RID: 26621 RVA: 0x00162B6B File Offset: 0x00160D6B
			public bool Equals(LeafCellTreeNode left, LeafCellTreeNode right)
			{
				return left == right || (left != null && right != null && left.m_cellWrapper.Equals(right.m_cellWrapper));
			}

			// Token: 0x060067FE RID: 26622 RVA: 0x00162B8C File Offset: 0x00160D8C
			public int GetHashCode(LeafCellTreeNode node)
			{
				return node.m_cellWrapper.GetHashCode();
			}
		}
	}
}
