using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200057D RID: 1405
	internal class ForeignConstraint : InternalBase
	{
		// Token: 0x060043FC RID: 17404 RVA: 0x000EE3C8 File Offset: 0x000EC5C8
		internal ForeignConstraint(AssociationSet i_fkeySet, EntitySet i_parentTable, EntitySet i_childTable, ReadOnlyMetadataCollection<EdmProperty> i_parentColumns, ReadOnlyMetadataCollection<EdmProperty> i_childColumns)
		{
			this.m_fKeySet = i_fkeySet;
			this.m_parentTable = i_parentTable;
			this.m_childTable = i_childTable;
			this.m_childColumns = new List<MemberPath>();
			foreach (EdmProperty edmProperty in i_childColumns)
			{
				MemberPath memberPath = new MemberPath(this.m_childTable, edmProperty);
				this.m_childColumns.Add(memberPath);
			}
			this.m_parentColumns = new List<MemberPath>();
			foreach (EdmProperty edmProperty2 in i_parentColumns)
			{
				MemberPath memberPath2 = new MemberPath(this.m_parentTable, edmProperty2);
				this.m_parentColumns.Add(memberPath2);
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x000EE4AC File Offset: 0x000EC6AC
		internal EntitySet ParentTable
		{
			get
			{
				return this.m_parentTable;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x000EE4B4 File Offset: 0x000EC6B4
		internal EntitySet ChildTable
		{
			get
			{
				return this.m_childTable;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x000EE4BC File Offset: 0x000EC6BC
		internal IEnumerable<MemberPath> ChildColumns
		{
			get
			{
				return this.m_childColumns;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x000EE4C4 File Offset: 0x000EC6C4
		internal IEnumerable<MemberPath> ParentColumns
		{
			get
			{
				return this.m_parentColumns;
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000EE4CC File Offset: 0x000EC6CC
		internal static List<ForeignConstraint> GetForeignConstraints(EntityContainer container)
		{
			List<ForeignConstraint> list = new List<ForeignConstraint>();
			foreach (EntitySetBase entitySetBase in container.BaseEntitySets)
			{
				AssociationSet associationSet = entitySetBase as AssociationSet;
				if (associationSet != null)
				{
					Dictionary<string, EntitySet> dictionary = new Dictionary<string, EntitySet>();
					foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
					{
						dictionary.Add(associationSetEnd.Name, associationSetEnd.EntitySet);
					}
					foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
					{
						EntitySet entitySet = dictionary[referentialConstraint.FromRole.Name];
						EntitySet entitySet2 = dictionary[referentialConstraint.ToRole.Name];
						ForeignConstraint foreignConstraint = new ForeignConstraint(associationSet, entitySet, entitySet2, referentialConstraint.FromProperties, referentialConstraint.ToProperties);
						list.Add(foreignConstraint);
					}
				}
			}
			return list;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x000EE614 File Offset: 0x000EC814
		internal void CheckConstraint(Set<Cell> cells, QueryRewriter childRewriter, QueryRewriter parentRewriter, ErrorLog errorLog, ConfigViewGenerator config)
		{
			if (!this.IsConstraintRelevantForCells(cells))
			{
				return;
			}
			if (config.IsNormalTracing)
			{
				Trace.WriteLine(string.Empty);
				Trace.WriteLine(string.Empty);
				Trace.Write("Checking: ");
				Trace.WriteLine(this);
			}
			if (childRewriter == null && parentRewriter == null)
			{
				return;
			}
			if (childRewriter == null)
			{
				string text = Strings.ViewGen_Foreign_Key_Missing_Table_Mapping(this.ToUserString(), this.ChildTable.Name);
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyMissingTableMapping, text, parentRewriter.UsedCells, string.Empty);
				errorLog.AddEntry(record);
				return;
			}
			if (parentRewriter == null)
			{
				string text2 = Strings.ViewGen_Foreign_Key_Missing_Table_Mapping(this.ToUserString(), this.ParentTable.Name);
				ErrorLog.Record record2 = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyMissingTableMapping, text2, childRewriter.UsedCells, string.Empty);
				errorLog.AddEntry(record2);
				return;
			}
			if (this.CheckIfConstraintMappedToForeignKeyAssociation(childRewriter, cells))
			{
				return;
			}
			int count = errorLog.Count;
			if (this.IsForeignKeySuperSetOfPrimaryKeyInChildTable())
			{
				this.GuaranteeForeignKeyConstraintInCSpace(childRewriter, parentRewriter, errorLog);
			}
			else
			{
				this.GuaranteeMappedRelationshipForForeignKey(childRewriter, parentRewriter, cells, errorLog, config);
			}
			if (count == errorLog.Count)
			{
				this.CheckForeignKeyColumnOrder(cells, errorLog);
			}
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x000EE71C File Offset: 0x000EC91C
		private void GuaranteeForeignKeyConstraintInCSpace(QueryRewriter childRewriter, QueryRewriter parentRewriter, ErrorLog errorLog)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			CellTreeNode basicView = childRewriter.BasicView;
			CellTreeNode basicView2 = parentRewriter.BasicView;
			if (!FragmentQueryProcessor.Merge(viewgenContext.RightFragmentQP, viewgenContext2.RightFragmentQP).IsContainedIn(basicView.RightFragmentQuery, basicView2.RightFragmentQuery))
			{
				string text = Strings.ViewGen_Foreign_Key_Not_Guaranteed_InCSpace(this.ToUserString());
				Set<LeftCellWrapper> set = new Set<LeftCellWrapper>(basicView2.GetLeaves());
				set.AddRange(basicView.GetLeaves());
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyNotGuaranteedInCSpace, text, set, string.Empty);
				errorLog.AddEntry(record);
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000EE7AC File Offset: 0x000EC9AC
		private void GuaranteeMappedRelationshipForForeignKey(QueryRewriter childRewriter, QueryRewriter parentRewriter, IEnumerable<Cell> cells, ErrorLog errorLog, ConfigViewGenerator config)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			IEnumerable<MemberPath> keyFields = ExtentKey.GetPrimaryKeyForEntityType(new MemberPath(this.ChildTable), this.ChildTable.ElementType).KeyFields;
			bool flag = false;
			bool flag2 = false;
			List<ErrorLog.Record> list = null;
			foreach (Cell cell in cells)
			{
				if (cell.SQuery.Extent.Equals(this.ChildTable))
				{
					AssociationEndMember relationEndForColumns = ForeignConstraint.GetRelationEndForColumns(cell, this.ChildColumns);
					if (relationEndForColumns == null || this.CheckParentColumnsForForeignKey(cell, cells, relationEndForColumns, ref list))
					{
						flag2 = true;
						if (ForeignConstraint.GetRelationEndForColumns(cell, keyFields) != null && relationEndForColumns != null && ForeignConstraint.FindEntitySetForColumnsMappedToEntityKeys(cells, keyFields).Count > 0)
						{
							flag = true;
							this.CheckConstraintWhenParentChildMapped(cell, errorLog, relationEndForColumns, config);
							break;
						}
						if (relationEndForColumns != null)
						{
							flag = ForeignConstraint.CheckConstraintWhenOnlyParentMapped((AssociationSet)cell.CQuery.Extent, relationEndForColumns, childRewriter, parentRewriter);
							if (flag)
							{
								break;
							}
						}
					}
				}
			}
			if (!flag2)
			{
				foreach (ErrorLog.Record record in list)
				{
					errorLog.AddEntry(record);
				}
				return;
			}
			if (!flag)
			{
				string text = Strings.ViewGen_Foreign_Key_Missing_Relationship_Mapping(this.ToUserString());
				IEnumerable<LeftCellWrapper> wrappersFromContext = ForeignConstraint.GetWrappersFromContext(viewgenContext2, this.ParentTable);
				IEnumerable<LeftCellWrapper> wrappersFromContext2 = ForeignConstraint.GetWrappersFromContext(viewgenContext, this.ChildTable);
				Set<LeftCellWrapper> set = new Set<LeftCellWrapper>(wrappersFromContext);
				set.AddRange(wrappersFromContext2);
				ErrorLog.Record record2 = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyMissingRelationshipMapping, text, set, string.Empty);
				errorLog.AddEntry(record2);
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000EE964 File Offset: 0x000ECB64
		private bool CheckIfConstraintMappedToForeignKeyAssociation(QueryRewriter childRewriter, Set<Cell> cells)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			List<Set<EdmProperty>> list = new List<Set<EdmProperty>>();
			List<Set<EdmProperty>> list2 = new List<Set<EdmProperty>>();
			foreach (Cell cell in cells)
			{
				if (cell.CQuery.Extent.BuiltInTypeKind != BuiltInTypeKind.AssociationSet)
				{
					Set<EdmProperty> cslotsForTableColumns = cell.GetCSlotsForTableColumns(this.ChildColumns);
					if (cslotsForTableColumns != null && cslotsForTableColumns.Count != 0)
					{
						list.Add(cslotsForTableColumns);
					}
					Set<EdmProperty> cslotsForTableColumns2 = cell.GetCSlotsForTableColumns(this.ParentColumns);
					if (cslotsForTableColumns2 != null && cslotsForTableColumns2.Count != 0)
					{
						list2.Add(cslotsForTableColumns2);
					}
				}
			}
			if (list.Count != 0 && list2.Count != 0)
			{
				foreach (AssociationType associationType in from it in viewgenContext.EntityContainerMapping.EdmEntityContainer.BaseEntitySets.OfType<AssociationSet>()
					where it.ElementType.IsForeignKey
					select it.ElementType)
				{
					ReferentialConstraint refConstraint = associationType.ReferentialConstraints.FirstOrDefault<ReferentialConstraint>();
					IEnumerable<Set<EdmProperty>> enumerable = list.Where((Set<EdmProperty> it) => it.SetEquals(new Set<EdmProperty>(refConstraint.ToProperties)));
					IEnumerable<Set<EdmProperty>> enumerable2 = list2.Where((Set<EdmProperty> it) => it.SetEquals(new Set<EdmProperty>(refConstraint.FromProperties)));
					if (enumerable.Count<Set<EdmProperty>>() != 0 && enumerable2.Count<Set<EdmProperty>>() != 0)
					{
						foreach (Set<EdmProperty> set in enumerable2)
						{
							Set<int> propertyIndexes = ForeignConstraint.GetPropertyIndexes(set, refConstraint.FromProperties);
							using (IEnumerator<Set<EdmProperty>> enumerator4 = enumerable.GetEnumerator())
							{
								while (enumerator4.MoveNext())
								{
									if (ForeignConstraint.GetPropertyIndexes(enumerator4.Current, refConstraint.ToProperties).SequenceEqual(propertyIndexes))
									{
										return true;
									}
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000EEBC8 File Offset: 0x000ECDC8
		private static Set<int> GetPropertyIndexes(IEnumerable<EdmProperty> properties1, ReadOnlyMetadataCollection<EdmProperty> properties2)
		{
			Set<int> set = new Set<int>();
			foreach (EdmProperty edmProperty in properties1)
			{
				set.Add(properties2.IndexOf(edmProperty));
			}
			return set;
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000EEC20 File Offset: 0x000ECE20
		private static bool CheckConstraintWhenOnlyParentMapped(AssociationSet assocSet, AssociationEndMember endMember, QueryRewriter childRewriter, QueryRewriter parentRewriter)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			CellTreeNode basicView = parentRewriter.BasicView;
			RoleBoolean roleBoolean = new RoleBoolean(assocSet.AssociationSetEnds[endMember.Name]);
			BoolExpression boolExpression = basicView.RightFragmentQuery.Condition.Create(roleBoolean);
			FragmentQuery fragmentQuery = FragmentQuery.Create(basicView.RightFragmentQuery.Attributes, boolExpression);
			return FragmentQueryProcessor.Merge(viewgenContext.RightFragmentQP, viewgenContext2.RightFragmentQP).IsContainedIn(fragmentQuery, basicView.RightFragmentQuery);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000EEC9C File Offset: 0x000ECE9C
		private bool CheckConstraintWhenParentChildMapped(Cell cell, ErrorLog errorLog, AssociationEndMember parentEnd, ConfigViewGenerator config)
		{
			bool flag = true;
			if (parentEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				string text = Strings.ViewGen_Foreign_Key_UpperBound_MustBeOne(this.ToUserString(), cell.CQuery.Extent.Name, parentEnd.Name);
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyUpperBoundMustBeOne, text, cell, string.Empty);
				errorLog.AddEntry(record);
				flag = false;
			}
			if (!MemberPath.AreAllMembersNullable(this.ChildColumns) && parentEnd.RelationshipMultiplicity != RelationshipMultiplicity.One)
			{
				string text2 = Strings.ViewGen_Foreign_Key_LowerBound_MustBeOne(this.ToUserString(), cell.CQuery.Extent.Name, parentEnd.Name);
				ErrorLog.Record record2 = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyLowerBoundMustBeOne, text2, cell, string.Empty);
				errorLog.AddEntry(record2);
				flag = false;
			}
			if (config.IsNormalTracing && flag)
			{
				Trace.WriteLine("Foreign key mapped to relationship " + cell.CQuery.Extent.Name);
			}
			return flag;
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000EED74 File Offset: 0x000ECF74
		private bool CheckParentColumnsForForeignKey(Cell cell, IEnumerable<Cell> cells, AssociationEndMember parentEnd, ref List<ErrorLog.Record> errorList)
		{
			EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd((AssociationSet)cell.CQuery.Extent, parentEnd);
			if (!ForeignConstraint.FindEntitySetForColumnsMappedToEntityKeys(cells, this.ParentColumns).Contains(entitySetAtEnd))
			{
				if (errorList == null)
				{
					errorList = new List<ErrorLog.Record>();
				}
				string text = Strings.ViewGen_Foreign_Key_ParentTable_NotMappedToEnd(this.ToUserString(), this.ChildTable.Name, cell.CQuery.Extent.Name, parentEnd.Name, this.ParentTable.Name, entitySetAtEnd.Name);
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyParentTableNotMappedToEnd, text, cell, string.Empty);
				errorList.Add(record);
				return false;
			}
			return true;
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000EEE18 File Offset: 0x000ED018
		private static IList<EntitySet> FindEntitySetForColumnsMappedToEntityKeys(IEnumerable<Cell> cells, IEnumerable<MemberPath> tableColumns)
		{
			List<EntitySet> list = new List<EntitySet>();
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				if (!(cquery.Extent is AssociationSet))
				{
					Set<EdmProperty> cslotsForTableColumns = cell.GetCSlotsForTableColumns(tableColumns);
					if (cslotsForTableColumns != null)
					{
						EntitySet entitySet = (EntitySet)cquery.Extent;
						List<EdmProperty> list2 = new List<EdmProperty>();
						foreach (EdmMember edmMember in entitySet.ElementType.KeyMembers)
						{
							EdmProperty edmProperty = (EdmProperty)edmMember;
							list2.Add(edmProperty);
						}
						if (new Set<EdmProperty>(list2).MakeReadOnly().SetEquals(cslotsForTableColumns))
						{
							list.Add(entitySet);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000EEF0C File Offset: 0x000ED10C
		private static AssociationEndMember GetRelationEndForColumns(Cell cell, IEnumerable<MemberPath> columns)
		{
			if (cell.CQuery.Extent is EntitySet)
			{
				return null;
			}
			AssociationSet associationSet = (AssociationSet)cell.CQuery.Extent;
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
				ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(new MemberPath(associationSet, correspondingAssociationEndMember), associationSetEnd.EntitySet.ElementType);
				List<int> projectedPositions = cell.CQuery.GetProjectedPositions(primaryKeyForEntityType.KeyFields);
				if (projectedPositions != null)
				{
					List<int> projectedPositions2 = cell.SQuery.GetProjectedPositions(columns, projectedPositions);
					if (projectedPositions2 != null && Helpers.IsSetEqual<int>(projectedPositions2, projectedPositions, EqualityComparer<int>.Default))
					{
						return correspondingAssociationEndMember;
					}
				}
			}
			return null;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000EEFE4 File Offset: 0x000ED1E4
		private static List<LeftCellWrapper> GetWrappersFromContext(ViewgenContext context, EntitySetBase extent)
		{
			List<LeftCellWrapper> list;
			if (context == null)
			{
				list = new List<LeftCellWrapper>();
			}
			else
			{
				list = context.AllWrappersForExtent;
			}
			return list;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000EF004 File Offset: 0x000ED204
		private bool CheckForeignKeyColumnOrder(Set<Cell> cells, ErrorLog errorLog)
		{
			List<Cell> list = new List<Cell>();
			List<Cell> list2 = new List<Cell>();
			foreach (Cell cell in cells)
			{
				if (cell.SQuery.Extent.Equals(this.ChildTable))
				{
					list2.Add(cell);
				}
				if (cell.SQuery.Extent.Equals(this.ParentTable))
				{
					list.Add(cell);
				}
			}
			foreach (Cell cell2 in list2)
			{
				List<List<int>> slotNumsForColumns = ForeignConstraint.GetSlotNumsForColumns(cell2, this.ChildColumns);
				if (slotNumsForColumns.Count != 0)
				{
					List<MemberPath> list3 = null;
					List<MemberPath> list4 = null;
					Cell cell3 = null;
					foreach (List<int> list5 in slotNumsForColumns)
					{
						list3 = new List<MemberPath>(list5.Count);
						foreach (int num in list5)
						{
							MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)cell2.CQuery.ProjectedSlotAt(num);
							list3.Add(memberProjectedSlot.MemberPath);
						}
						foreach (Cell cell4 in list)
						{
							List<List<int>> slotNumsForColumns2 = ForeignConstraint.GetSlotNumsForColumns(cell4, this.ParentColumns);
							if (slotNumsForColumns2.Count != 0)
							{
								foreach (List<int> list6 in slotNumsForColumns2)
								{
									list4 = new List<MemberPath>(list6.Count);
									foreach (int num2 in list6)
									{
										MemberProjectedSlot memberProjectedSlot2 = (MemberProjectedSlot)cell4.CQuery.ProjectedSlotAt(num2);
										list4.Add(memberProjectedSlot2.MemberPath);
									}
									if (list3.Count == list4.Count)
									{
										bool flag = false;
										int num3 = 0;
										while (num3 < list3.Count && !flag)
										{
											MemberPath memberPath = list4[num3];
											MemberPath memberPath2 = list3[num3];
											if (!memberPath.LeafEdmMember.Equals(memberPath2.LeafEdmMember))
											{
												if (memberPath.IsEquivalentViaRefConstraint(memberPath2))
												{
													return true;
												}
												flag = true;
											}
											num3++;
										}
										if (!flag)
										{
											return true;
										}
										cell3 = cell4;
									}
								}
							}
						}
					}
					string text = Strings.ViewGen_Foreign_Key_ColumnOrder_Incorrect(this.ToUserString(), MemberPath.PropertiesToUserString(this.ChildColumns, false), this.ChildTable.Name, MemberPath.PropertiesToUserString(list3, false), cell2.CQuery.Extent.Name, MemberPath.PropertiesToUserString(this.ParentColumns, false), this.ParentTable.Name, MemberPath.PropertiesToUserString(list4, false), cell3.CQuery.Extent.Name);
					ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyColumnOrderIncorrect, text, new Cell[] { cell3, cell2 }, string.Empty);
					errorLog.AddEntry(record);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000EF40C File Offset: 0x000ED60C
		private static List<List<int>> GetSlotNumsForColumns(Cell cell, IEnumerable<MemberPath> columns)
		{
			List<List<int>> list = new List<List<int>>();
			AssociationSet associationSet = cell.CQuery.Extent as AssociationSet;
			if (associationSet != null)
			{
				using (ReadOnlyMetadataCollection<AssociationSetEnd>.Enumerator enumerator = associationSet.AssociationSetEnds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AssociationSetEnd associationSetEnd = enumerator.Current;
						List<int> associationEndSlots = cell.CQuery.GetAssociationEndSlots(associationSetEnd.CorrespondingAssociationEndMember);
						List<int> projectedPositions = cell.SQuery.GetProjectedPositions(columns, associationEndSlots);
						if (projectedPositions != null)
						{
							list.Add(projectedPositions);
						}
					}
					return list;
				}
			}
			List<int> projectedPositions2 = cell.SQuery.GetProjectedPositions(columns);
			if (projectedPositions2 != null)
			{
				list.Add(projectedPositions2);
			}
			return list;
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000EF4BC File Offset: 0x000ED6BC
		private bool IsForeignKeySuperSetOfPrimaryKeyInChildTable()
		{
			bool flag = true;
			foreach (EdmMember edmMember in this.m_childTable.ElementType.KeyMembers)
			{
				EdmProperty edmProperty = (EdmProperty)edmMember;
				bool flag2 = false;
				using (List<MemberPath>.Enumerator enumerator2 = this.m_childColumns.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.LeafEdmMember.Equals(edmProperty))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x000EF570 File Offset: 0x000ED770
		private bool IsConstraintRelevantForCells(IEnumerable<Cell> cells)
		{
			bool flag = false;
			foreach (Cell cell in cells)
			{
				EntitySetBase extent = cell.SQuery.Extent;
				if (extent.Equals(this.m_parentTable) || extent.Equals(this.m_childTable))
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000EF5E0 File Offset: 0x000ED7E0
		internal string ToUserString()
		{
			string text = MemberPath.PropertiesToUserString(this.m_childColumns, false);
			string text2 = MemberPath.PropertiesToUserString(this.m_parentColumns, false);
			return Strings.ViewGen_Foreign_Key(this.m_fKeySet.Name, this.m_childTable.Name, text, this.m_parentTable.Name, text2);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000EF62F File Offset: 0x000ED82F
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_fKeySet.Name + ": ");
			builder.Append(this.ToUserString());
		}

		// Token: 0x04001887 RID: 6279
		private readonly AssociationSet m_fKeySet;

		// Token: 0x04001888 RID: 6280
		private readonly EntitySet m_parentTable;

		// Token: 0x04001889 RID: 6281
		private readonly EntitySet m_childTable;

		// Token: 0x0400188A RID: 6282
		private readonly List<MemberPath> m_parentColumns;

		// Token: 0x0400188B RID: 6283
		private readonly List<MemberPath> m_childColumns;
	}
}
