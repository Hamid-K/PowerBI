using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000563 RID: 1379
	internal class CellCreator : InternalBase
	{
		// Token: 0x0600433C RID: 17212 RVA: 0x000E7B65 File Offset: 0x000E5D65
		internal CellCreator(EntityContainerMapping containerMapping)
		{
			this.m_containerMapping = containerMapping;
			this.m_identifiers = new CqlIdentifiers();
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000E7B7F File Offset: 0x000E5D7F
		internal CqlIdentifiers Identifiers
		{
			get
			{
				return this.m_identifiers;
			}
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000E7B88 File Offset: 0x000E5D88
		internal List<Cell> GenerateCells()
		{
			List<Cell> list = new List<Cell>();
			this.ExtractCells(list);
			this.ExpandCells(list);
			this.m_identifiers.AddIdentifier(this.m_containerMapping.EdmEntityContainer.Name);
			this.m_identifiers.AddIdentifier(this.m_containerMapping.StorageEntityContainer.Name);
			foreach (Cell cell in list)
			{
				cell.GetIdentifiers(this.m_identifiers);
			}
			return list;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000E7C24 File Offset: 0x000E5E24
		private void ExpandCells(List<Cell> cells)
		{
			Set<MemberPath> set = new Set<MemberPath>();
			using (List<Cell>.Enumerator enumerator = cells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell5 = enumerator.Current;
					IEnumerable<MemberPath> enumerable = from member in cell5.SQuery.GetProjectedMembers()
						where CellCreator.IsBooleanMember(member)
						select member;
					Func<MemberPath, bool> func;
					Func<MemberPath, bool> <>9__1;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (MemberPath boolMember) => (from restriction in cell5.SQuery.GetConjunctsFromWhereClause()
							where restriction.Domain.Values.Contains(Constant.NotNull)
							select restriction.RestrictedMemberSlot.MemberPath).Contains(boolMember));
					}
					foreach (MemberPath memberPath in enumerable.Where(func))
					{
						set.Add(memberPath);
					}
				}
			}
			Dictionary<MemberPath, Set<MemberPath>> dictionary = new Dictionary<MemberPath, Set<MemberPath>>();
			using (List<Cell>.Enumerator enumerator = cells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					Func<int, MemberPath> <>9__4;
					foreach (MemberPath memberPath2 in set)
					{
						IEnumerable<int> projectedPositions = cell.SQuery.GetProjectedPositions(memberPath2);
						Func<int, MemberPath> func2;
						if ((func2 = <>9__4) == null)
						{
							func2 = (<>9__4 = (int pos) => ((MemberProjectedSlot)cell.CQuery.ProjectedSlotAt(pos)).MemberPath);
						}
						IEnumerable<MemberPath> enumerable2 = projectedPositions.Select(func2);
						Set<MemberPath> set2 = null;
						if (!dictionary.TryGetValue(memberPath2, out set2))
						{
							set2 = new Set<MemberPath>();
							dictionary[memberPath2] = set2;
						}
						set2.AddRange(enumerable2);
					}
				}
			}
			foreach (Cell cell4 in cells.ToArray())
			{
				foreach (MemberPath memberPath3 in set)
				{
					Set<MemberPath> set3 = dictionary[memberPath3];
					if (cell4.SQuery.GetProjectedMembers().Contains(memberPath3))
					{
						Cell cell2 = null;
						if (this.TryCreateAdditionalCellWithCondition(cell4, memberPath3, true, ViewTarget.UpdateView, out cell2))
						{
							cells.Add(cell2);
						}
						if (this.TryCreateAdditionalCellWithCondition(cell4, memberPath3, false, ViewTarget.UpdateView, out cell2))
						{
							cells.Add(cell2);
						}
					}
					else
					{
						foreach (MemberPath memberPath4 in cell4.CQuery.GetProjectedMembers().Intersect(set3))
						{
							Cell cell3 = null;
							if (this.TryCreateAdditionalCellWithCondition(cell4, memberPath4, true, ViewTarget.QueryView, out cell3))
							{
								cells.Add(cell3);
							}
							if (this.TryCreateAdditionalCellWithCondition(cell4, memberPath4, false, ViewTarget.QueryView, out cell3))
							{
								cells.Add(cell3);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x000E7F3C File Offset: 0x000E613C
		private bool TryCreateAdditionalCellWithCondition(Cell originalCell, MemberPath memberToExpand, bool conditionValue, ViewTarget viewTarget, out Cell result)
		{
			result = null;
			MemberPath sourceExtentMemberPath = originalCell.GetLeftQuery(viewTarget).SourceExtentMemberPath;
			MemberPath sourceExtentMemberPath2 = originalCell.GetRightQuery(viewTarget).SourceExtentMemberPath;
			int num = originalCell.GetLeftQuery(viewTarget).GetProjectedMembers().TakeWhile((MemberPath path) => !path.Equals(memberToExpand))
				.Count<MemberPath>();
			MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)originalCell.GetRightQuery(viewTarget).ProjectedSlotAt(num);
			MemberPath rightSidePath = memberProjectedSlot.MemberPath;
			List<ProjectedSlot> list = new List<ProjectedSlot>();
			List<ProjectedSlot> list2 = new List<ProjectedSlot>();
			ScalarConstant negatedCondition = new ScalarConstant(!conditionValue);
			if ((from restriction in originalCell.GetLeftQuery(viewTarget).Conditions
				where restriction.RestrictedMemberSlot.MemberPath.Equals(memberToExpand)
				where restriction.Domain.Values.Contains(negatedCondition)
				select restriction).Any<MemberRestriction>() || (from restriction in originalCell.GetRightQuery(viewTarget).Conditions
				where restriction.RestrictedMemberSlot.MemberPath.Equals(rightSidePath)
				where restriction.Domain.Values.Contains(negatedCondition)
				select restriction).Any<MemberRestriction>())
			{
				return false;
			}
			for (int i = 0; i < originalCell.GetLeftQuery(viewTarget).NumProjectedSlots; i++)
			{
				list.Add(originalCell.GetLeftQuery(viewTarget).ProjectedSlotAt(i));
			}
			for (int j = 0; j < originalCell.GetRightQuery(viewTarget).NumProjectedSlots; j++)
			{
				list2.Add(originalCell.GetRightQuery(viewTarget).ProjectedSlotAt(j));
			}
			BoolExpression boolExpression = BoolExpression.CreateLiteral(new ScalarRestriction(memberToExpand, new ScalarConstant(conditionValue)), null);
			boolExpression = BoolExpression.CreateAnd(new BoolExpression[]
			{
				originalCell.GetLeftQuery(viewTarget).WhereClause,
				boolExpression
			});
			BoolExpression boolExpression2 = BoolExpression.CreateLiteral(new ScalarRestriction(rightSidePath, new ScalarConstant(conditionValue)), null);
			boolExpression2 = BoolExpression.CreateAnd(new BoolExpression[]
			{
				originalCell.GetRightQuery(viewTarget).WhereClause,
				boolExpression2
			});
			CellQuery cellQuery = new CellQuery(list2, boolExpression2, sourceExtentMemberPath2, originalCell.GetRightQuery(viewTarget).SelectDistinctFlag);
			CellQuery cellQuery2 = new CellQuery(list, boolExpression, sourceExtentMemberPath, originalCell.GetLeftQuery(viewTarget).SelectDistinctFlag);
			Cell cell;
			if (viewTarget == ViewTarget.UpdateView)
			{
				cell = Cell.CreateCS(cellQuery, cellQuery2, originalCell.CellLabel, this.m_currentCellNumber);
			}
			else
			{
				cell = Cell.CreateCS(cellQuery2, cellQuery, originalCell.CellLabel, this.m_currentCellNumber);
			}
			this.m_currentCellNumber++;
			result = cell;
			return true;
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x000E81AC File Offset: 0x000E63AC
		private void ExtractCells(List<Cell> cells)
		{
			foreach (EntitySetBaseMapping entitySetBaseMapping in this.m_containerMapping.AllSetMaps)
			{
				foreach (TypeMapping typeMapping in entitySetBaseMapping.TypeMappings)
				{
					EntityTypeMapping entityTypeMapping = typeMapping as EntityTypeMapping;
					Set<EdmType> set = new Set<EdmType>();
					if (entityTypeMapping != null)
					{
						set.AddRange(entityTypeMapping.Types);
						foreach (EntityTypeBase entityTypeBase in entityTypeMapping.IsOfTypes)
						{
							IEnumerable<EdmType> typeAndSubtypesOf = MetadataHelper.GetTypeAndSubtypesOf(entityTypeBase, this.m_containerMapping.StorageMappingItemCollection.EdmItemCollection, false);
							set.AddRange(typeAndSubtypesOf);
						}
					}
					EntitySetBase set2 = entitySetBaseMapping.Set;
					foreach (MappingFragment mappingFragment in typeMapping.MappingFragments)
					{
						this.ExtractCellsFromTableFragment(set2, mappingFragment, set, cells);
					}
				}
			}
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000E8304 File Offset: 0x000E6504
		private void ExtractCellsFromTableFragment(EntitySetBase extent, MappingFragment fragmentMap, Set<EdmType> allTypes, List<Cell> cells)
		{
			MemberPath memberPath = new MemberPath(extent);
			BoolExpression boolExpression = BoolExpression.True;
			List<ProjectedSlot> list = new List<ProjectedSlot>();
			if (allTypes.Count > 0)
			{
				boolExpression = BoolExpression.CreateLiteral(new TypeRestriction(memberPath, allTypes), null);
			}
			MemberPath memberPath2 = new MemberPath(fragmentMap.TableSet);
			BoolExpression @true = BoolExpression.True;
			List<ProjectedSlot> list2 = new List<ProjectedSlot>();
			this.ExtractProperties(fragmentMap.AllProperties, memberPath, list, ref boolExpression, memberPath2, list2, ref @true);
			CellQuery cellQuery = new CellQuery(list, boolExpression, memberPath, CellQuery.SelectDistinct.No);
			CellQuery cellQuery2 = new CellQuery(list2, @true, memberPath2, fragmentMap.IsSQueryDistinct ? CellQuery.SelectDistinct.Yes : CellQuery.SelectDistinct.No);
			CellLabel cellLabel = new CellLabel(fragmentMap);
			Cell cell = Cell.CreateCS(cellQuery, cellQuery2, cellLabel, this.m_currentCellNumber);
			this.m_currentCellNumber++;
			cells.Add(cell);
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x000E83BC File Offset: 0x000E65BC
		private void ExtractProperties(IEnumerable<PropertyMapping> properties, MemberPath cNode, List<ProjectedSlot> cSlots, ref BoolExpression cQueryWhereClause, MemberPath sRootExtent, List<ProjectedSlot> sSlots, ref BoolExpression sQueryWhereClause)
		{
			foreach (PropertyMapping propertyMapping in properties)
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				EndPropertyMapping endPropertyMapping = propertyMapping as EndPropertyMapping;
				ConditionPropertyMapping conditionPropertyMapping = propertyMapping as ConditionPropertyMapping;
				if (scalarPropertyMapping != null)
				{
					MemberPath memberPath = new MemberPath(cNode, scalarPropertyMapping.Property);
					MemberPath memberPath2 = new MemberPath(sRootExtent, scalarPropertyMapping.Column);
					cSlots.Add(new MemberProjectedSlot(memberPath));
					sSlots.Add(new MemberProjectedSlot(memberPath2));
				}
				if (complexPropertyMapping != null)
				{
					foreach (ComplexTypeMapping complexTypeMapping in complexPropertyMapping.TypeMappings)
					{
						MemberPath memberPath3 = new MemberPath(cNode, complexPropertyMapping.Property);
						Set<EdmType> set = new Set<EdmType>();
						IEnumerable<EdmType> enumerable = Helpers.AsSuperTypeList<ComplexType, EdmType>(complexTypeMapping.Types);
						set.AddRange(enumerable);
						foreach (EdmType edmType in complexTypeMapping.IsOfTypes)
						{
							set.AddRange(MetadataHelper.GetTypeAndSubtypesOf(edmType, this.m_containerMapping.StorageMappingItemCollection.EdmItemCollection, false));
						}
						BoolExpression boolExpression = BoolExpression.CreateLiteral(new TypeRestriction(memberPath3, set), null);
						cQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[] { cQueryWhereClause, boolExpression });
						this.ExtractProperties(complexTypeMapping.AllProperties, memberPath3, cSlots, ref cQueryWhereClause, sRootExtent, sSlots, ref sQueryWhereClause);
					}
				}
				if (endPropertyMapping != null)
				{
					MemberPath memberPath4 = new MemberPath(cNode, endPropertyMapping.AssociationEnd);
					this.ExtractProperties(endPropertyMapping.PropertyMappings, memberPath4, cSlots, ref cQueryWhereClause, sRootExtent, sSlots, ref sQueryWhereClause);
				}
				if (conditionPropertyMapping != null)
				{
					if (conditionPropertyMapping.Column != null)
					{
						BoolExpression conditionExpression = CellCreator.GetConditionExpression(sRootExtent, conditionPropertyMapping);
						sQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[] { sQueryWhereClause, conditionExpression });
					}
					else
					{
						BoolExpression conditionExpression2 = CellCreator.GetConditionExpression(cNode, conditionPropertyMapping);
						cQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[] { cQueryWhereClause, conditionExpression2 });
					}
				}
			}
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000E860C File Offset: 0x000E680C
		private static BoolExpression GetConditionExpression(MemberPath member, ConditionPropertyMapping conditionMap)
		{
			EdmMember edmMember = ((conditionMap.Column != null) ? conditionMap.Column : conditionMap.Property);
			MemberPath memberPath = new MemberPath(member, edmMember);
			MemberRestriction memberRestriction;
			if (conditionMap.IsNull != null)
			{
				Constant constant = (conditionMap.IsNull.Value ? Constant.Null : Constant.NotNull);
				if (MetadataHelper.IsNonRefSimpleMember(edmMember))
				{
					memberRestriction = new ScalarRestriction(memberPath, constant);
				}
				else
				{
					memberRestriction = new TypeRestriction(memberPath, constant);
				}
			}
			else
			{
				memberRestriction = new ScalarRestriction(memberPath, new ScalarConstant(conditionMap.Value));
			}
			return BoolExpression.CreateLiteral(memberRestriction, null);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x000E86A0 File Offset: 0x000E68A0
		private static bool IsBooleanMember(MemberPath path)
		{
			PrimitiveType primitiveType = path.EdmType as PrimitiveType;
			return primitiveType != null && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Boolean;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000E86C7 File Offset: 0x000E68C7
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("CellCreator");
		}

		// Token: 0x04001803 RID: 6147
		private readonly EntityContainerMapping m_containerMapping;

		// Token: 0x04001804 RID: 6148
		private int m_currentCellNumber;

		// Token: 0x04001805 RID: 6149
		private readonly CqlIdentifiers m_identifiers;
	}
}
