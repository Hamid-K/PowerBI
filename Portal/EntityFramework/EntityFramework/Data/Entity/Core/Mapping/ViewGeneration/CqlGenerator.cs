using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000567 RID: 1383
	internal sealed class CqlGenerator : InternalBase
	{
		// Token: 0x0600436B RID: 17259 RVA: 0x000E94C7 File Offset: 0x000E76C7
		internal CqlGenerator(CellTreeNode view, Dictionary<MemberPath, CaseStatement> caseStatements, CqlIdentifiers identifiers, MemberProjectionIndex projectedSlotMap, int numCellsInView, BoolExpression topLevelWhereClause, StorageMappingItemCollection mappingItemCollection)
		{
			this.m_view = view;
			this.m_caseStatements = caseStatements;
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_numBools = numCellsInView;
			this.m_topLevelWhereClause = topLevelWhereClause;
			this.m_identifiers = identifiers;
			this.m_mappingItemCollection = mappingItemCollection;
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000E9504 File Offset: 0x000E7704
		private int TotalSlots
		{
			get
			{
				return this.m_projectedSlotMap.Count + this.m_numBools;
			}
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000E9518 File Offset: 0x000E7718
		internal string GenerateEsql()
		{
			CqlBlock cqlBlock = this.GenerateCqlBlockTree();
			StringBuilder stringBuilder = new StringBuilder(1024);
			cqlBlock.AsEsql(stringBuilder, true, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000E9548 File Offset: 0x000E7748
		internal DbQueryCommandTree GenerateCqt()
		{
			DbExpression dbExpression = this.GenerateCqlBlockTree().AsCqt(true);
			return DbQueryCommandTree.FromValidExpression(this.m_mappingItemCollection.Workspace, DataSpace.SSpace, dbExpression, true, false);
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000E9578 File Offset: 0x000E7778
		private CqlBlock GenerateCqlBlockTree()
		{
			bool[] requiredSlots = this.GetRequiredSlots();
			List<WithRelationship> list = new List<WithRelationship>();
			CqlBlock cqlBlock = this.m_view.ToCqlBlock(requiredSlots, this.m_identifiers, ref this.m_currentBlockNum, ref list);
			foreach (CaseStatement caseStatement in this.m_caseStatements.Values)
			{
				caseStatement.Simplify();
			}
			return this.ConstructCaseBlocks(cqlBlock, list);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000E9600 File Offset: 0x000E7800
		private bool[] GetRequiredSlots()
		{
			bool[] array = new bool[this.TotalSlots];
			foreach (CaseStatement caseStatement in this.m_caseStatements.Values)
			{
				this.GetRequiredSlotsForCaseMember(caseStatement.MemberPath, array);
			}
			for (int i = this.TotalSlots - this.m_numBools; i < this.TotalSlots; i++)
			{
				array[i] = true;
			}
			foreach (CaseStatement caseStatement2 in this.m_caseStatements.Values)
			{
				if (!caseStatement2.MemberPath.IsPartOfKey && !caseStatement2.DependsOnMemberValue)
				{
					array[this.m_projectedSlotMap.IndexOf(caseStatement2.MemberPath)] = false;
				}
			}
			return array;
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000E9700 File Offset: 0x000E7900
		private CqlBlock ConstructCaseBlocks(CqlBlock viewBlock, IEnumerable<WithRelationship> withRelationships)
		{
			bool[] array = new bool[this.TotalSlots];
			array[0] = true;
			this.m_topLevelWhereClause.GetRequiredSlots(this.m_projectedSlotMap, array);
			return this.ConstructCaseBlocks(viewBlock, 0, array, withRelationships);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000E973C File Offset: 0x000E793C
		private CqlBlock ConstructCaseBlocks(CqlBlock viewBlock, int startSlotNum, bool[] parentRequiredSlots, IEnumerable<WithRelationship> withRelationships)
		{
			int count = this.m_projectedSlotMap.Count;
			int num = this.FindNextCaseStatementSlot(startSlotNum, parentRequiredSlots, count);
			if (num == -1)
			{
				return viewBlock;
			}
			MemberPath memberPath = this.m_projectedSlotMap[num];
			bool[] array = new bool[this.TotalSlots];
			this.GetRequiredSlotsForCaseMember(memberPath, array);
			for (int i = 0; i < this.TotalSlots; i++)
			{
				if (parentRequiredSlots[i])
				{
					array[i] = true;
				}
			}
			CaseStatement caseStatement = this.m_caseStatements[memberPath];
			array[num] = caseStatement.DependsOnMemberValue;
			CqlBlock cqlBlock = this.ConstructCaseBlocks(viewBlock, num + 1, array, null);
			SlotInfo[] array2 = this.CreateSlotInfosForCaseStatement(parentRequiredSlots, num, cqlBlock, caseStatement, withRelationships);
			this.m_currentBlockNum++;
			BoolExpression boolExpression = ((startSlotNum == 0) ? this.m_topLevelWhereClause : BoolExpression.True);
			if (startSlotNum == 0)
			{
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j].ResetIsRequiredByParent();
				}
			}
			return new CaseCqlBlock(array2, num, cqlBlock, boolExpression, this.m_identifiers, this.m_currentBlockNum);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000E9834 File Offset: 0x000E7A34
		private SlotInfo[] CreateSlotInfosForCaseStatement(bool[] parentRequiredSlots, int foundSlot, CqlBlock childBlock, CaseStatement thisCaseStatement, IEnumerable<WithRelationship> withRelationships)
		{
			int num = childBlock.Slots.Count - this.TotalSlots;
			SlotInfo[] array = new SlotInfo[this.TotalSlots + num];
			for (int i = 0; i < this.TotalSlots; i++)
			{
				bool flag = childBlock.IsProjected(i);
				bool flag2 = parentRequiredSlots[i];
				ProjectedSlot projectedSlot = childBlock.SlotValue(i);
				MemberPath outputMemberPath = this.GetOutputMemberPath(i);
				if (i == foundSlot)
				{
					projectedSlot = new CaseStatementProjectedSlot(thisCaseStatement.DeepQualify(childBlock), withRelationships);
					flag = true;
				}
				else if (flag && flag2)
				{
					projectedSlot = childBlock.QualifySlotWithBlockAlias(i);
				}
				SlotInfo slotInfo = new SlotInfo(flag2 && flag, flag, projectedSlot, outputMemberPath);
				array[i] = slotInfo;
			}
			for (int j = this.TotalSlots; j < this.TotalSlots + num; j++)
			{
				QualifiedSlot qualifiedSlot = childBlock.QualifySlotWithBlockAlias(j);
				array[j] = new SlotInfo(true, true, qualifiedSlot, childBlock.MemberPath(j));
			}
			return array;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000E990C File Offset: 0x000E7B0C
		private int FindNextCaseStatementSlot(int startSlotNum, bool[] parentRequiredSlots, int numMembers)
		{
			int num = -1;
			for (int i = startSlotNum; i < numMembers; i++)
			{
				MemberPath memberPath = this.m_projectedSlotMap[i];
				if (parentRequiredSlots[i] && this.m_caseStatements.ContainsKey(memberPath))
				{
					num = i;
					break;
				}
			}
			return num;
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x000E994C File Offset: 0x000E7B4C
		private void GetRequiredSlotsForCaseMember(MemberPath caseMemberPath, bool[] requiredSlots)
		{
			CaseStatement caseStatement = this.m_caseStatements[caseMemberPath];
			bool flag = false;
			foreach (CaseStatement.WhenThen whenThen in caseStatement.Clauses)
			{
				whenThen.Condition.GetRequiredSlots(this.m_projectedSlotMap, requiredSlots);
				if (!(whenThen.Value is ConstantProjectedSlot))
				{
					flag = true;
				}
			}
			EdmType edmType = caseMemberPath.EdmType;
			if (Helper.IsEntityType(edmType) || Helper.IsComplexType(edmType))
			{
				using (IEnumerator<EdmType> enumerator2 = caseStatement.InstantiatedTypes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EdmType edmType2 = enumerator2.Current;
						foreach (object obj in Helper.GetAllStructuralMembers(edmType2))
						{
							EdmMember edmMember = (EdmMember)obj;
							int slotIndex = this.GetSlotIndex(caseMemberPath, edmMember);
							requiredSlots[slotIndex] = true;
						}
					}
					return;
				}
			}
			if (caseMemberPath.IsScalarType())
			{
				if (flag)
				{
					int num = this.m_projectedSlotMap.IndexOf(caseMemberPath);
					requiredSlots[num] = true;
					return;
				}
			}
			else
			{
				if (Helper.IsAssociationType(edmType))
				{
					using (ReadOnlyMetadataCollection<AssociationEndMember>.Enumerator enumerator4 = ((AssociationSet)caseMemberPath.Extent).ElementType.AssociationEndMembers.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							AssociationEndMember associationEndMember = enumerator4.Current;
							int slotIndex2 = this.GetSlotIndex(caseMemberPath, associationEndMember);
							requiredSlots[slotIndex2] = true;
						}
						return;
					}
				}
				foreach (EdmMember edmMember2 in (edmType as RefType).ElementType.KeyMembers)
				{
					int slotIndex3 = this.GetSlotIndex(caseMemberPath, edmMember2);
					requiredSlots[slotIndex3] = true;
				}
			}
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000E9B50 File Offset: 0x000E7D50
		private MemberPath GetOutputMemberPath(int slotNum)
		{
			return this.m_projectedSlotMap.GetMemberPath(slotNum, this.TotalSlots - this.m_projectedSlotMap.Count);
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000E9B70 File Offset: 0x000E7D70
		private int GetSlotIndex(MemberPath member, EdmMember child)
		{
			MemberPath memberPath = new MemberPath(member, child);
			return this.m_projectedSlotMap.IndexOf(memberPath);
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000E9B94 File Offset: 0x000E7D94
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("View: ");
			this.m_view.ToCompactString(builder);
			builder.Append("ProjectedSlotMap: ");
			this.m_projectedSlotMap.ToCompactString(builder);
			builder.Append("Case statements: ");
			foreach (MemberPath memberPath in this.m_caseStatements.Keys)
			{
				this.m_caseStatements[memberPath].ToCompactString(builder);
				builder.AppendLine();
			}
		}

		// Token: 0x04001812 RID: 6162
		private readonly CellTreeNode m_view;

		// Token: 0x04001813 RID: 6163
		private readonly Dictionary<MemberPath, CaseStatement> m_caseStatements;

		// Token: 0x04001814 RID: 6164
		private readonly MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04001815 RID: 6165
		private readonly int m_numBools;

		// Token: 0x04001816 RID: 6166
		private int m_currentBlockNum;

		// Token: 0x04001817 RID: 6167
		private readonly BoolExpression m_topLevelWhereClause;

		// Token: 0x04001818 RID: 6168
		private readonly CqlIdentifiers m_identifiers;

		// Token: 0x04001819 RID: 6169
		private readonly StorageMappingItemCollection m_mappingItemCollection;
	}
}
