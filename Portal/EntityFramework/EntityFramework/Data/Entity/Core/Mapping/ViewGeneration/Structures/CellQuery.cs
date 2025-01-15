using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200059C RID: 1436
	internal class CellQuery : InternalBase
	{
		// Token: 0x0600456F RID: 17775 RVA: 0x000F4F5C File Offset: 0x000F315C
		internal CellQuery(List<ProjectedSlot> slots, BoolExpression whereClause, MemberPath rootMember, CellQuery.SelectDistinct eliminateDuplicates)
			: this(slots.ToArray(), whereClause, new List<BoolExpression>(), eliminateDuplicates, rootMember)
		{
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000F4F73 File Offset: 0x000F3173
		internal CellQuery(ProjectedSlot[] projectedSlots, BoolExpression whereClause, List<BoolExpression> boolExprs, CellQuery.SelectDistinct elimDupl, MemberPath rootMember)
		{
			this.m_boolExprs = boolExprs;
			this.m_projectedSlots = projectedSlots;
			this.m_whereClause = whereClause;
			this.m_originalWhereClause = whereClause;
			this.m_selectDistinct = elimDupl;
			this.m_extentMemberPath = rootMember;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000F4FA8 File Offset: 0x000F31A8
		internal CellQuery(CellQuery source)
		{
			this.m_basicCellRelation = source.m_basicCellRelation;
			this.m_boolExprs = source.m_boolExprs;
			this.m_selectDistinct = source.m_selectDistinct;
			this.m_extentMemberPath = source.m_extentMemberPath;
			this.m_originalWhereClause = source.m_originalWhereClause;
			this.m_projectedSlots = source.m_projectedSlots;
			this.m_whereClause = source.m_whereClause;
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000F500F File Offset: 0x000F320F
		private CellQuery(CellQuery existing, ProjectedSlot[] newSlots)
			: this(newSlots, existing.m_whereClause, existing.m_boolExprs, existing.m_selectDistinct, existing.m_extentMemberPath)
		{
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000F5030 File Offset: 0x000F3230
		internal CellQuery.SelectDistinct SelectDistinctFlag
		{
			get
			{
				return this.m_selectDistinct;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x000F5038 File Offset: 0x000F3238
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extentMemberPath.Extent;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x000F5045 File Offset: 0x000F3245
		internal int NumProjectedSlots
		{
			get
			{
				return this.m_projectedSlots.Length;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x000F504F File Offset: 0x000F324F
		internal ProjectedSlot[] ProjectedSlots
		{
			get
			{
				return this.m_projectedSlots;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x000F5057 File Offset: 0x000F3257
		internal List<BoolExpression> BoolVars
		{
			get
			{
				return this.m_boolExprs;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x000F505F File Offset: 0x000F325F
		internal int NumBoolVars
		{
			get
			{
				return this.m_boolExprs.Count;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x000F506C File Offset: 0x000F326C
		internal BoolExpression WhereClause
		{
			get
			{
				return this.m_whereClause;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x000F5074 File Offset: 0x000F3274
		internal MemberPath SourceExtentMemberPath
		{
			get
			{
				return this.m_extentMemberPath;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x000F507C File Offset: 0x000F327C
		internal BasicCellRelation BasicCellRelation
		{
			get
			{
				return this.m_basicCellRelation;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x000F5084 File Offset: 0x000F3284
		internal IEnumerable<MemberRestriction> Conditions
		{
			get
			{
				return this.GetConjunctsFromOriginalWhereClause();
			}
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x000F508C File Offset: 0x000F328C
		internal ProjectedSlot ProjectedSlotAt(int slotNum)
		{
			return this.m_projectedSlots[slotNum];
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x000F5098 File Offset: 0x000F3298
		internal ErrorLog.Record CheckForDuplicateFields(CellQuery cQuery, Cell sourceCell)
		{
			KeyToListMap<MemberProjectedSlot, int> keyToListMap = new KeyToListMap<MemberProjectedSlot, int>(ProjectedSlot.EqualityComparer);
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				keyToListMap.Add(memberProjectedSlot, i);
			}
			StringBuilder stringBuilder = null;
			bool flag = false;
			foreach (MemberProjectedSlot memberProjectedSlot2 in keyToListMap.Keys)
			{
				ReadOnlyCollection<int> readOnlyCollection = keyToListMap.ListForKey(memberProjectedSlot2);
				if (readOnlyCollection.Count > 1 && !cQuery.AreSlotsEquivalentViaRefConstraints(readOnlyCollection))
				{
					flag = true;
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(Strings.ViewGen_Duplicate_CProperties(this.Extent.Name));
						stringBuilder.AppendLine();
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					for (int j = 0; j < readOnlyCollection.Count; j++)
					{
						int num = readOnlyCollection[j];
						if (j != 0)
						{
							stringBuilder2.Append(", ");
						}
						MemberProjectedSlot memberProjectedSlot3 = (MemberProjectedSlot)cQuery.m_projectedSlots[num];
						stringBuilder2.Append(memberProjectedSlot3.ToUserString());
					}
					stringBuilder.AppendLine(Strings.ViewGen_Duplicate_CProperties_IsMapped(memberProjectedSlot2.ToUserString(), stringBuilder2.ToString()));
				}
			}
			if (!flag)
			{
				return null;
			}
			return new ErrorLog.Record(ViewGenErrorCode.DuplicateCPropertiesMapped, stringBuilder.ToString(), sourceCell, string.Empty);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000F51F8 File Offset: 0x000F33F8
		private bool AreSlotsEquivalentViaRefConstraints(ReadOnlyCollection<int> cSideSlotIndexes)
		{
			if (!(this.Extent is AssociationSet))
			{
				return false;
			}
			if (cSideSlotIndexes.Count > 2)
			{
				return false;
			}
			MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)this.m_projectedSlots[cSideSlotIndexes[0]];
			MemberProjectedSlot memberProjectedSlot2 = (MemberProjectedSlot)this.m_projectedSlots[cSideSlotIndexes[1]];
			return memberProjectedSlot.MemberPath.IsEquivalentViaRefConstraint(memberProjectedSlot2.MemberPath);
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000F5258 File Offset: 0x000F3458
		internal ErrorLog.Record CheckForProjectedNotNullSlots(Cell sourceCell, IEnumerable<Cell> associationSets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (MemberRestriction memberRestriction in this.Conditions)
			{
				if (memberRestriction.Domain.ContainsNotNull() && MemberProjectedSlot.GetSlotForMember(this.m_projectedSlots, memberRestriction.RestrictedMemberSlot.MemberPath) == null)
				{
					bool flag2 = true;
					if (this.Extent is EntitySet)
					{
						bool flag3 = sourceCell.CQuery == this;
						ViewTarget target = (flag3 ? ViewTarget.QueryView : ViewTarget.UpdateView);
						CellQuery cellQuery = (flag3 ? sourceCell.SQuery : sourceCell.CQuery);
						EntitySet rightExtent = cellQuery.Extent as EntitySet;
						if (rightExtent != null)
						{
							IEnumerable<AssociationSet> associationSets2 = (cellQuery.Extent as EntitySet).AssociationSets;
							Func<AssociationSet, bool> func;
							Func<AssociationSet, bool> <>9__0;
							if ((func = <>9__0) == null)
							{
								Func<AssociationSetEnd, bool> <>9__1;
								func = (<>9__0 = delegate(AssociationSet association)
								{
									IEnumerable<AssociationSetEnd> associationSetEnds = association.AssociationSetEnds;
									Func<AssociationSetEnd, bool> func3;
									if ((func3 = <>9__1) == null)
									{
										func3 = (<>9__1 = (AssociationSetEnd end) => end.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One && MetadataHelper.GetOppositeEnd(end).EntitySet.EdmEquals(rightExtent));
									}
									return associationSetEnds.Any(func3);
								});
							}
							using (IEnumerator<AssociationSet> enumerator2 = associationSets2.Where(func).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									AssociationSet association = enumerator2.Current;
									Func<Cell, bool> func2;
									Func<Cell, bool> <>9__2;
									if ((func2 = <>9__2) == null)
									{
										func2 = (<>9__2 = (Cell c) => c.GetRightQuery(target).Extent.EdmEquals(association));
									}
									using (IEnumerator<Cell> enumerator3 = associationSets.Where(func2).GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											if (MemberProjectedSlot.GetSlotForMember(enumerator3.Current.GetLeftQuery(target).ProjectedSlots, memberRestriction.RestrictedMemberSlot.MemberPath) != null)
											{
												flag2 = false;
											}
										}
									}
								}
							}
						}
					}
					if (flag2)
					{
						stringBuilder.AppendLine(Strings.ViewGen_NotNull_No_Projected_Slot(memberRestriction.RestrictedMemberSlot.MemberPath.PathToString(new bool?(false))));
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return new ErrorLog.Record(ViewGenErrorCode.NotNullNoProjectedSlot, stringBuilder.ToString(), sourceCell, string.Empty);
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000F54B8 File Offset: 0x000F36B8
		internal void FixMissingSlotAsDefaultConstant(int slotNumber, ConstantProjectedSlot slot)
		{
			this.m_projectedSlots[slotNumber] = slot;
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000F54C4 File Offset: 0x000F36C4
		internal void CreateFieldAlignedCellQueries(CellQuery otherQuery, MemberProjectionIndex projectedSlotMap, out CellQuery newMainQuery, out CellQuery newOtherQuery)
		{
			int count = projectedSlotMap.Count;
			ProjectedSlot[] array = new ProjectedSlot[count];
			ProjectedSlot[] array2 = new ProjectedSlot[count];
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				int num = projectedSlotMap.IndexOf(memberProjectedSlot.MemberPath);
				array[num] = this.m_projectedSlots[i];
				array2[num] = otherQuery.m_projectedSlots[i];
			}
			newMainQuery = new CellQuery(this, array);
			newOtherQuery = new CellQuery(otherQuery, array2);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x000F5540 File Offset: 0x000F3740
		internal Set<MemberPath> GetNonNullSlots()
		{
			Set<MemberPath> set = new Set<MemberPath>(MemberPath.EqualityComparer);
			foreach (ProjectedSlot projectedSlot in this.m_projectedSlots)
			{
				if (projectedSlot != null)
				{
					MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
					set.Add(memberProjectedSlot.MemberPath);
				}
			}
			return set;
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x000F558C File Offset: 0x000F378C
		internal ErrorLog.Record VerifyKeysPresent(Cell ownerCell, Func<object, object, string> formatEntitySetMessage, Func<object, object, object, string> formatAssociationSetMessage, ViewGenErrorCode errorCode)
		{
			List<MemberPath> list = new List<MemberPath>(1);
			List<ExtentKey> list2 = new List<ExtentKey>(1);
			if (this.Extent is EntitySet)
			{
				MemberPath memberPath = new MemberPath(this.Extent);
				list.Add(memberPath);
				EntityType entityType = (EntityType)this.Extent.ElementType;
				List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(memberPath, entityType);
				list2.Add(keysForEntityType[0]);
			}
			else
			{
				AssociationSet associationSet = (AssociationSet)this.Extent;
				foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
				{
					AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
					MemberPath memberPath2 = new MemberPath(associationSet, correspondingAssociationEndMember);
					list.Add(memberPath2);
					List<ExtentKey> keysForEntityType2 = ExtentKey.GetKeysForEntityType(memberPath2, MetadataHelper.GetEntityTypeForEnd(correspondingAssociationEndMember));
					list2.Add(keysForEntityType2[0]);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				MemberPath memberPath3 = list[i];
				if (MemberProjectedSlot.GetKeySlots(this.GetMemberProjectedSlots(), memberPath3) == null)
				{
					ExtentKey extentKey = list2[i];
					string text2;
					if (this.Extent is EntitySet)
					{
						string text = MemberPath.PropertiesToUserString(extentKey.KeyFields, true);
						text2 = formatEntitySetMessage(text, this.Extent.Name);
					}
					else
					{
						string name = memberPath3.RootEdmMember.Name;
						string text3 = MemberPath.PropertiesToUserString(extentKey.KeyFields, false);
						text2 = formatAssociationSetMessage(text3, name, this.Extent.Name);
					}
					return new ErrorLog.Record(errorCode, text2, ownerCell, string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x000F5730 File Offset: 0x000F3930
		internal IEnumerable<MemberPath> GetProjectedMembers()
		{
			foreach (MemberProjectedSlot memberProjectedSlot in this.GetMemberProjectedSlots())
			{
				yield return memberProjectedSlot.MemberPath;
			}
			IEnumerator<MemberProjectedSlot> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x000F5740 File Offset: 0x000F3940
		private IEnumerable<MemberProjectedSlot> GetMemberProjectedSlots()
		{
			ProjectedSlot[] array = this.m_projectedSlots;
			for (int i = 0; i < array.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = array[i] as MemberProjectedSlot;
				if (memberProjectedSlot != null)
				{
					yield return memberProjectedSlot;
				}
			}
			array = null;
			yield break;
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x000F5750 File Offset: 0x000F3950
		internal List<MemberProjectedSlot> GetAllQuerySlots()
		{
			HashSet<MemberProjectedSlot> hashSet = new HashSet<MemberProjectedSlot>(this.GetMemberProjectedSlots());
			hashSet.Add(new MemberProjectedSlot(this.SourceExtentMemberPath));
			foreach (MemberRestriction memberRestriction in this.Conditions)
			{
				hashSet.Add(memberRestriction.RestrictedMemberSlot);
			}
			return new List<MemberProjectedSlot>(hashSet);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x000F57C8 File Offset: 0x000F39C8
		internal int GetProjectedPosition(MemberProjectedSlot slot)
		{
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				if (ProjectedSlot.EqualityComparer.Equals(slot, this.m_projectedSlots[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x000F5800 File Offset: 0x000F3A00
		internal List<int> GetProjectedPositions(MemberPath member)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				if (memberProjectedSlot != null && MemberPath.EqualityComparer.Equals(member, memberProjectedSlot.MemberPath))
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x000F5854 File Offset: 0x000F3A54
		internal List<int> GetProjectedPositions(IEnumerable<MemberPath> paths)
		{
			List<int> list = new List<int>();
			foreach (MemberPath memberPath in paths)
			{
				List<int> projectedPositions = this.GetProjectedPositions(memberPath);
				if (projectedPositions.Count == 0)
				{
					return null;
				}
				list.Add(projectedPositions[0]);
			}
			return list;
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x000F58C4 File Offset: 0x000F3AC4
		internal List<int> GetAssociationEndSlots(AssociationEndMember endMember)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				if (memberProjectedSlot != null && memberProjectedSlot.MemberPath.RootEdmMember.Equals(endMember))
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x000F5918 File Offset: 0x000F3B18
		internal List<int> GetProjectedPositions(IEnumerable<MemberPath> paths, List<int> slotsToSearchFrom)
		{
			List<int> list = new List<int>();
			foreach (MemberPath memberPath in paths)
			{
				List<int> projectedPositions = this.GetProjectedPositions(memberPath);
				if (projectedPositions.Count == 0)
				{
					return null;
				}
				int num = -1;
				if (projectedPositions.Count > 1)
				{
					for (int i = 0; i < projectedPositions.Count; i++)
					{
						if (slotsToSearchFrom.Contains(projectedPositions[i]))
						{
							num = projectedPositions[i];
						}
					}
					if (num == -1)
					{
						return null;
					}
				}
				else
				{
					num = projectedPositions[0];
				}
				list.Add(num);
			}
			return list;
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000F59D0 File Offset: 0x000F3BD0
		internal void UpdateWhereClause(MemberDomainMap domainMap)
		{
			List<BoolExpression> list = new List<BoolExpression>();
			foreach (BoolExpression boolExpression in this.WhereClause.Atoms)
			{
				MemberRestriction memberRestriction = boolExpression.AsLiteral as MemberRestriction;
				IEnumerable<Constant> domain = domainMap.GetDomain(memberRestriction.RestrictedMemberSlot.MemberPath);
				MemberRestriction memberRestriction2 = memberRestriction.CreateCompleteMemberRestriction(domain);
				ScalarRestriction scalarRestriction = memberRestriction as ScalarRestriction;
				bool flag = scalarRestriction != null && !scalarRestriction.Domain.Contains(Constant.Null) && !scalarRestriction.Domain.Contains(Constant.NotNull) && !scalarRestriction.Domain.Contains(Constant.Undefined);
				if (flag)
				{
					domainMap.AddSentinel(memberRestriction2.RestrictedMemberSlot.MemberPath);
				}
				list.Add(BoolExpression.CreateLiteral(memberRestriction2, domainMap));
				if (flag)
				{
					domainMap.RemoveSentinel(memberRestriction2.RestrictedMemberSlot.MemberPath);
				}
			}
			if (list.Count > 0)
			{
				this.m_whereClause = BoolExpression.CreateAnd(list.ToArray());
			}
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x000F5AE8 File Offset: 0x000F3CE8
		internal BoolExpression GetBoolVar(int varNum)
		{
			return this.m_boolExprs[varNum];
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000F5AF8 File Offset: 0x000F3CF8
		internal void InitializeBoolExpressions(int numBoolVars, int cellNum)
		{
			this.m_boolExprs = new List<BoolExpression>(numBoolVars);
			for (int i = 0; i < numBoolVars; i++)
			{
				this.m_boolExprs.Add(null);
			}
			this.m_boolExprs[cellNum] = BoolExpression.True;
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x000F5B3A File Offset: 0x000F3D3A
		internal IEnumerable<MemberRestriction> GetConjunctsFromWhereClause()
		{
			return CellQuery.GetConjunctsFromWhereClause(this.m_whereClause);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000F5B47 File Offset: 0x000F3D47
		internal IEnumerable<MemberRestriction> GetConjunctsFromOriginalWhereClause()
		{
			return CellQuery.GetConjunctsFromWhereClause(this.m_originalWhereClause);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x000F5B54 File Offset: 0x000F3D54
		private static IEnumerable<MemberRestriction> GetConjunctsFromWhereClause(BoolExpression whereClause)
		{
			foreach (BoolExpression boolExpression in whereClause.Atoms)
			{
				if (!boolExpression.IsTrue)
				{
					MemberRestriction memberRestriction = boolExpression.AsLiteral as MemberRestriction;
					yield return memberRestriction;
				}
			}
			IEnumerator<BoolExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x000F5B64 File Offset: 0x000F3D64
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			ProjectedSlot[] projectedSlots = this.m_projectedSlots;
			for (int i = 0; i < projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = projectedSlots[i] as MemberProjectedSlot;
				if (memberProjectedSlot != null)
				{
					memberProjectedSlot.MemberPath.GetIdentifiers(identifiers);
				}
			}
			this.m_extentMemberPath.GetIdentifiers(identifiers);
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x000F5BAC File Offset: 0x000F3DAC
		internal void CreateBasicCellRelation(ViewCellRelation viewCellRelation)
		{
			List<MemberProjectedSlot> allQuerySlots = this.GetAllQuerySlots();
			this.m_basicCellRelation = new BasicCellRelation(this, viewCellRelation, allQuerySlots);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x000F5BD0 File Offset: 0x000F3DD0
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			List<BoolExpression> boolExprs = this.m_boolExprs;
			int num = 0;
			bool flag = true;
			using (List<BoolExpression>.Enumerator enumerator = boolExprs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						if (!flag)
						{
							stringBuilder.Append(",");
						}
						else
						{
							stringBuilder.Append("[");
						}
						StringUtil.FormatStringBuilder(stringBuilder, "C{0}", new object[] { num });
						flag = false;
					}
					num++;
				}
			}
			if (flag)
			{
				this.ToFullString(stringBuilder);
				return;
			}
			stringBuilder.Append("]");
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x000F5C78 File Offset: 0x000F3E78
		internal override void ToFullString(StringBuilder builder)
		{
			builder.Append("SELECT ");
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				builder.Append("DISTINCT ");
			}
			StringUtil.ToSeparatedString(builder, this.m_projectedSlots, ", ", "_");
			if (this.m_boolExprs.Count > 0)
			{
				builder.Append(", Bool[");
				StringUtil.ToSeparatedString(builder, this.m_boolExprs, ", ", "_");
				builder.Append("]");
			}
			builder.Append(" FROM ");
			this.m_extentMemberPath.ToFullString(builder);
			if (!this.m_whereClause.IsTrue)
			{
				builder.Append(" WHERE ");
				this.m_whereClause.ToFullString(builder);
			}
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x000F5D34 File Offset: 0x000F3F34
		public override string ToString()
		{
			return this.ToFullString();
		}

		// Token: 0x040018EA RID: 6378
		private List<BoolExpression> m_boolExprs;

		// Token: 0x040018EB RID: 6379
		private readonly ProjectedSlot[] m_projectedSlots;

		// Token: 0x040018EC RID: 6380
		private BoolExpression m_whereClause;

		// Token: 0x040018ED RID: 6381
		private readonly BoolExpression m_originalWhereClause;

		// Token: 0x040018EE RID: 6382
		private readonly CellQuery.SelectDistinct m_selectDistinct;

		// Token: 0x040018EF RID: 6383
		private readonly MemberPath m_extentMemberPath;

		// Token: 0x040018F0 RID: 6384
		private BasicCellRelation m_basicCellRelation;

		// Token: 0x02000BB5 RID: 2997
		internal enum SelectDistinct
		{
			// Token: 0x04002E82 RID: 11906
			Yes,
			// Token: 0x04002E83 RID: 11907
			No
		}
	}
}
