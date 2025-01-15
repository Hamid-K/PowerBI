using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005AC RID: 1452
	internal class OpCellTreeNode : CellTreeNode
	{
		// Token: 0x0600469B RID: 18075 RVA: 0x000F9357 File Offset: 0x000F7557
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType)
			: base(context)
		{
			this.m_opType = opType;
			this.m_attrs = new Set<MemberPath>(MemberPath.EqualityComparer);
			this.m_children = new List<CellTreeNode>();
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x000F9382 File Offset: 0x000F7582
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType, params CellTreeNode[] children)
			: this(context, opType, children)
		{
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000F9390 File Offset: 0x000F7590
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType, IEnumerable<CellTreeNode> children)
			: this(context, opType)
		{
			foreach (CellTreeNode cellTreeNode in children)
			{
				this.Add(cellTreeNode);
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x000F93E0 File Offset: 0x000F75E0
		internal override CellTreeOpType OpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x000F93E8 File Offset: 0x000F75E8
		internal override FragmentQuery LeftFragmentQuery
		{
			get
			{
				if (this.m_leftFragmentQuery == null)
				{
					this.m_leftFragmentQuery = OpCellTreeNode.GenerateFragmentQuery(this.Children, true, base.ViewgenContext, this.OpType);
				}
				return this.m_leftFragmentQuery;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x000F9416 File Offset: 0x000F7616
		internal override FragmentQuery RightFragmentQuery
		{
			get
			{
				if (this.m_rightFragmentQuery == null)
				{
					this.m_rightFragmentQuery = OpCellTreeNode.GenerateFragmentQuery(this.Children, false, base.ViewgenContext, this.OpType);
				}
				return this.m_rightFragmentQuery;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x000F9444 File Offset: 0x000F7644
		internal override MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_children[0].RightDomainMap;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x000F9457 File Offset: 0x000F7657
		internal override Set<MemberPath> Attributes
		{
			get
			{
				return this.m_attrs;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x000F945F File Offset: 0x000F765F
		internal override List<CellTreeNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x000F9467 File Offset: 0x000F7667
		internal override int NumProjectedSlots
		{
			get
			{
				return this.m_children[0].NumProjectedSlots;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x000F947A File Offset: 0x000F767A
		internal override int NumBoolSlots
		{
			get
			{
				return this.m_children[0].NumBoolSlots;
			}
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x000F948D File Offset: 0x000F768D
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitOpNode(this, param);
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000F9498 File Offset: 0x000F7698
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			switch (this.OpType)
			{
			case CellTreeOpType.Union:
				return visitor.VisitUnion(this, param);
			case CellTreeOpType.FOJ:
				return visitor.VisitFullOuterJoin(this, param);
			case CellTreeOpType.LOJ:
				return visitor.VisitLeftOuterJoin(this, param);
			case CellTreeOpType.IJ:
				return visitor.VisitInnerJoin(this, param);
			case CellTreeOpType.LASJ:
				return visitor.VisitLeftAntiSemiJoin(this, param);
			default:
				return visitor.VisitInnerJoin(this, param);
			}
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000F94FF File Offset: 0x000F76FF
		internal void Add(CellTreeNode child)
		{
			this.Insert(this.m_children.Count, child);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000F9513 File Offset: 0x000F7713
		internal void AddFirst(CellTreeNode child)
		{
			this.Insert(0, child);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x000F951D File Offset: 0x000F771D
		private void Insert(int index, CellTreeNode child)
		{
			this.m_attrs.Unite(child.Attributes);
			this.m_children.Insert(index, child);
			this.m_leftFragmentQuery = null;
			this.m_rightFragmentQuery = null;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000F954C File Offset: 0x000F774C
		internal override CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			CqlBlock cqlBlock;
			if (this.OpType == CellTreeOpType.Union)
			{
				cqlBlock = this.UnionToCqlBlock(requiredSlots, identifiers, ref blockAliasNum, ref withRelationships);
			}
			else
			{
				cqlBlock = this.JoinToCqlBlock(requiredSlots, identifiers, ref blockAliasNum, ref withRelationships);
			}
			return cqlBlock;
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000F9580 File Offset: 0x000F7780
		internal override bool IsProjectedSlot(int slot)
		{
			using (List<CellTreeNode>.Enumerator enumerator = this.Children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsProjectedSlot(slot))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000F95DC File Offset: 0x000F77DC
		private CqlBlock UnionToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			List<CqlBlock> list = new List<CqlBlock>();
			List<Tuple<CqlBlock, SlotInfo>> list2 = new List<Tuple<CqlBlock, SlotInfo>>();
			int num = requiredSlots.Length;
			foreach (CellTreeNode cellTreeNode in this.Children)
			{
				bool[] projectedSlots = cellTreeNode.GetProjectedSlots();
				OpCellTreeNode.AndWith(projectedSlots, requiredSlots);
				CqlBlock cqlBlock = cellTreeNode.ToCqlBlock(projectedSlots, identifiers, ref blockAliasNum, ref withRelationships);
				for (int i = projectedSlots.Length; i < cqlBlock.Slots.Count; i++)
				{
					list2.Add(Tuple.Create<CqlBlock, SlotInfo>(cqlBlock, cqlBlock.Slots[i]));
				}
				SlotInfo[] array = new SlotInfo[cqlBlock.Slots.Count];
				for (int j = 0; j < num; j++)
				{
					if (requiredSlots[j] && !projectedSlots[j])
					{
						if (base.IsBoolSlot(j))
						{
							array[j] = new SlotInfo(true, true, new BooleanProjectedSlot(BoolExpression.False, identifiers, base.SlotToBoolIndex(j)), null);
						}
						else
						{
							MemberPath memberPath = cqlBlock.MemberPath(j);
							array[j] = new SlotInfo(true, true, new ConstantProjectedSlot(Constant.Null), memberPath);
						}
					}
					else
					{
						array[j] = cqlBlock.Slots[j];
					}
				}
				cqlBlock.Slots = new ReadOnlyCollection<SlotInfo>(array);
				list.Add(cqlBlock);
			}
			if (list2.Count != 0)
			{
				foreach (CqlBlock cqlBlock2 in list)
				{
					SlotInfo[] array2 = new SlotInfo[num + list2.Count];
					cqlBlock2.Slots.CopyTo(array2, 0);
					int num2 = num;
					foreach (Tuple<CqlBlock, SlotInfo> tuple in list2)
					{
						SlotInfo item = tuple.Item2;
						if (tuple.Item1.Equals(cqlBlock2))
						{
							array2[num2] = new SlotInfo(true, true, item.SlotValue, item.OutputMember);
						}
						else
						{
							array2[num2] = new SlotInfo(true, true, new ConstantProjectedSlot(Constant.Null), item.OutputMember);
						}
						num2++;
					}
					cqlBlock2.Slots = new ReadOnlyCollection<SlotInfo>(array2);
				}
			}
			SlotInfo[] array3 = new SlotInfo[num + list2.Count];
			CqlBlock cqlBlock3 = list[0];
			for (int k = 0; k < num; k++)
			{
				SlotInfo slotInfo = cqlBlock3.Slots[k];
				bool flag = requiredSlots[k];
				array3[k] = new SlotInfo(flag, flag, slotInfo.SlotValue, slotInfo.OutputMember);
			}
			for (int l = num; l < num + list2.Count; l++)
			{
				SlotInfo slotInfo2 = cqlBlock3.Slots[l];
				array3[l] = new SlotInfo(true, true, slotInfo2.SlotValue, slotInfo2.OutputMember);
			}
			SlotInfo[] array4 = array3;
			List<CqlBlock> list3 = list;
			int num3 = blockAliasNum + 1;
			blockAliasNum = num3;
			return new UnionCqlBlock(array4, list3, identifiers, num3);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000F9914 File Offset: 0x000F7B14
		private static void AndWith(bool[] boolArray, bool[] another)
		{
			for (int i = 0; i < boolArray.Length; i++)
			{
				boolArray[i] &= another[i];
			}
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000F9940 File Offset: 0x000F7B40
		private CqlBlock JoinToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			int num = requiredSlots.Length;
			List<CqlBlock> list = new List<CqlBlock>();
			List<Tuple<QualifiedSlot, MemberPath>> list2 = new List<Tuple<QualifiedSlot, MemberPath>>();
			foreach (CellTreeNode cellTreeNode in this.Children)
			{
				bool[] projectedSlots = cellTreeNode.GetProjectedSlots();
				OpCellTreeNode.AndWith(projectedSlots, requiredSlots);
				CqlBlock cqlBlock = cellTreeNode.ToCqlBlock(projectedSlots, identifiers, ref blockAliasNum, ref withRelationships);
				list.Add(cqlBlock);
				for (int i = projectedSlots.Length; i < cqlBlock.Slots.Count; i++)
				{
					list2.Add(Tuple.Create<QualifiedSlot, MemberPath>(cqlBlock.QualifySlotWithBlockAlias(i), cqlBlock.MemberPath(i)));
				}
			}
			SlotInfo[] array = new SlotInfo[num + list2.Count];
			for (int j = 0; j < num; j++)
			{
				SlotInfo joinSlotInfo = this.GetJoinSlotInfo(this.OpType, requiredSlots[j], list, j, identifiers);
				array[j] = joinSlotInfo;
			}
			int num2 = 0;
			int k = num;
			while (k < num + list2.Count)
			{
				array[k] = new SlotInfo(true, true, list2[num2].Item1, list2[num2].Item2);
				k++;
				num2++;
			}
			List<JoinCqlBlock.OnClause> list3 = new List<JoinCqlBlock.OnClause>();
			for (int l = 1; l < list.Count; l++)
			{
				CqlBlock cqlBlock2 = list[l];
				JoinCqlBlock.OnClause onClause = new JoinCqlBlock.OnClause();
				foreach (int num3 in base.KeySlots)
				{
					if (!base.ViewgenContext.Config.IsValidationEnabled && (!cqlBlock2.IsProjected(num3) || !list[0].IsProjected(num3)))
					{
						ErrorLog errorLog = new ErrorLog();
						errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.NoJoinKeyOrFKProvidedInMapping, Strings.Viewgen_NoJoinKeyOrFK, base.ViewgenContext.AllWrappersForExtent, string.Empty));
						ExceptionHelpers.ThrowMappingException(errorLog, base.ViewgenContext.Config);
					}
					QualifiedSlot qualifiedSlot = list[0].QualifySlotWithBlockAlias(num3);
					QualifiedSlot qualifiedSlot2 = cqlBlock2.QualifySlotWithBlockAlias(num3);
					MemberPath outputMember = array[num3].OutputMember;
					onClause.Add(qualifiedSlot, outputMember, qualifiedSlot2, outputMember);
				}
				list3.Add(onClause);
			}
			CellTreeOpType opType = this.OpType;
			SlotInfo[] array2 = array;
			List<CqlBlock> list4 = list;
			List<JoinCqlBlock.OnClause> list5 = list3;
			int num4 = blockAliasNum + 1;
			blockAliasNum = num4;
			return new JoinCqlBlock(opType, array2, list4, list5, identifiers, num4);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000F9BB0 File Offset: 0x000F7DB0
		private SlotInfo GetJoinSlotInfo(CellTreeOpType opType, bool isRequiredSlot, List<CqlBlock> children, int slotNum, CqlIdentifiers identifiers)
		{
			if (!isRequiredSlot)
			{
				return new SlotInfo(false, false, null, base.GetMemberPath(slotNum));
			}
			int num = -1;
			CaseStatement caseStatement = null;
			for (int i = 0; i < children.Count; i++)
			{
				CqlBlock cqlBlock = children[i];
				if (cqlBlock.IsProjected(slotNum))
				{
					if (base.IsKeySlot(slotNum))
					{
						num = i;
						break;
					}
					if (opType == CellTreeOpType.IJ)
					{
						num = OpCellTreeNode.GetInnerJoinChildForSlot(children, slotNum);
						break;
					}
					if (num != -1)
					{
						if (caseStatement == null)
						{
							caseStatement = new CaseStatement(base.GetMemberPath(slotNum));
							this.AddCaseForOuterJoins(caseStatement, children[num], slotNum, identifiers);
						}
						this.AddCaseForOuterJoins(caseStatement, cqlBlock, slotNum, identifiers);
					}
					num = i;
				}
			}
			MemberPath memberPath = base.GetMemberPath(slotNum);
			ProjectedSlot projectedSlot;
			if (caseStatement != null && (caseStatement.Clauses.Count > 0 || caseStatement.ElseValue != null))
			{
				caseStatement.Simplify();
				projectedSlot = new CaseStatementProjectedSlot(caseStatement, null);
			}
			else if (num >= 0)
			{
				projectedSlot = children[num].QualifySlotWithBlockAlias(slotNum);
			}
			else if (base.IsBoolSlot(slotNum))
			{
				projectedSlot = new BooleanProjectedSlot(BoolExpression.False, identifiers, base.SlotToBoolIndex(slotNum));
			}
			else
			{
				projectedSlot = new ConstantProjectedSlot(Domain.GetDefaultValueForMemberPath(memberPath, base.GetLeaves(), base.ViewgenContext.Config));
			}
			bool flag = base.IsBoolSlot(slotNum) && ((opType == CellTreeOpType.LOJ && num > 0) || opType == CellTreeOpType.FOJ);
			return new SlotInfo(true, true, projectedSlot, memberPath, flag);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000F9D04 File Offset: 0x000F7F04
		private static int GetInnerJoinChildForSlot(List<CqlBlock> children, int slotNum)
		{
			int num = -1;
			for (int i = 0; i < children.Count; i++)
			{
				CqlBlock cqlBlock = children[i];
				if (cqlBlock.IsProjected(slotNum))
				{
					ProjectedSlot projectedSlot = cqlBlock.SlotValue(slotNum);
					ConstantProjectedSlot constantProjectedSlot = projectedSlot as ConstantProjectedSlot;
					if (projectedSlot is MemberProjectedSlot)
					{
						num = i;
					}
					else if (constantProjectedSlot != null && constantProjectedSlot.CellConstant.IsNull())
					{
						if (num == -1)
						{
							num = i;
						}
					}
					else
					{
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000F9D6C File Offset: 0x000F7F6C
		private void AddCaseForOuterJoins(CaseStatement caseForOuterJoins, CqlBlock child, int slotNum, CqlIdentifiers identifiers)
		{
			ConstantProjectedSlot constantProjectedSlot = child.SlotValue(slotNum) as ConstantProjectedSlot;
			if (constantProjectedSlot != null && constantProjectedSlot.CellConstant.IsNull())
			{
				return;
			}
			BoolExpression boolExpression = BoolExpression.False;
			for (int i = 0; i < this.NumBoolSlots; i++)
			{
				int num = base.BoolIndexToSlot(i);
				if (child.IsProjected(num))
				{
					QualifiedCellIdBoolean qualifiedCellIdBoolean = new QualifiedCellIdBoolean(child, identifiers, i);
					boolExpression = BoolExpression.CreateOr(new BoolExpression[]
					{
						boolExpression,
						BoolExpression.CreateLiteral(qualifiedCellIdBoolean, this.RightDomainMap)
					});
				}
			}
			QualifiedSlot qualifiedSlot = child.QualifySlotWithBlockAlias(slotNum);
			caseForOuterJoins.AddWhenThen(boolExpression, qualifiedSlot);
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000F9DFC File Offset: 0x000F7FFC
		private static FragmentQuery GenerateFragmentQuery(IEnumerable<CellTreeNode> children, bool isLeft, ViewgenContext context, CellTreeOpType OpType)
		{
			FragmentQuery fragmentQuery = (isLeft ? children.First<CellTreeNode>().LeftFragmentQuery : children.First<CellTreeNode>().RightFragmentQuery);
			FragmentQueryProcessor fragmentQueryProcessor = (isLeft ? context.LeftFragmentQP : context.RightFragmentQP);
			foreach (CellTreeNode cellTreeNode in children.Skip(1))
			{
				FragmentQuery fragmentQuery2 = (isLeft ? cellTreeNode.LeftFragmentQuery : cellTreeNode.RightFragmentQuery);
				switch (OpType)
				{
				case CellTreeOpType.LOJ:
					break;
				case CellTreeOpType.IJ:
					fragmentQuery = fragmentQueryProcessor.Intersect(fragmentQuery, fragmentQuery2);
					break;
				case CellTreeOpType.LASJ:
					fragmentQuery = fragmentQueryProcessor.Difference(fragmentQuery, fragmentQuery2);
					break;
				default:
					fragmentQuery = fragmentQueryProcessor.Union(fragmentQuery, fragmentQuery2);
					break;
				}
			}
			return fragmentQuery;
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000F9EC0 File Offset: 0x000F80C0
		internal static string OpToEsql(CellTreeOpType opType)
		{
			switch (opType)
			{
			case CellTreeOpType.Union:
				return "UNION ALL";
			case CellTreeOpType.FOJ:
				return "FULL OUTER JOIN";
			case CellTreeOpType.LOJ:
				return "LEFT OUTER JOIN";
			case CellTreeOpType.IJ:
				return "INNER JOIN";
			default:
				return null;
			}
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000F9EF8 File Offset: 0x000F80F8
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			stringBuilder.Append("(");
			for (int i = 0; i < this.m_children.Count; i++)
			{
				this.m_children[i].ToCompactString(stringBuilder);
				if (i != this.m_children.Count - 1)
				{
					StringUtil.FormatStringBuilder(stringBuilder, " {0} ", new object[] { this.OpType });
				}
			}
			stringBuilder.Append(")");
		}

		// Token: 0x04001921 RID: 6433
		private readonly Set<MemberPath> m_attrs;

		// Token: 0x04001922 RID: 6434
		private readonly List<CellTreeNode> m_children;

		// Token: 0x04001923 RID: 6435
		private readonly CellTreeOpType m_opType;

		// Token: 0x04001924 RID: 6436
		private FragmentQuery m_leftFragmentQuery;

		// Token: 0x04001925 RID: 6437
		private FragmentQuery m_rightFragmentQuery;
	}
}
