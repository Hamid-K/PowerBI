using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200059D RID: 1437
	internal abstract class CellTreeNode : InternalBase
	{
		// Token: 0x06004598 RID: 17816 RVA: 0x000F5D3C File Offset: 0x000F3F3C
		protected CellTreeNode(ViewgenContext context)
		{
			this.m_viewgenContext = context;
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x000F5D4C File Offset: 0x000F3F4C
		internal CellTreeNode MakeCopy()
		{
			CellTreeNode.DefaultCellTreeVisitor<bool> defaultCellTreeVisitor = new CellTreeNode.DefaultCellTreeVisitor<bool>();
			return this.Accept<bool, CellTreeNode>(defaultCellTreeVisitor, true);
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600459A RID: 17818
		internal abstract CellTreeOpType OpType { get; }

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600459B RID: 17819
		internal abstract MemberDomainMap RightDomainMap { get; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600459C RID: 17820
		internal abstract FragmentQuery LeftFragmentQuery { get; }

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600459D RID: 17821
		internal abstract FragmentQuery RightFragmentQuery { get; }

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x000F5D67 File Offset: 0x000F3F67
		internal bool IsEmptyRightFragmentQuery
		{
			get
			{
				return !this.m_viewgenContext.RightFragmentQP.IsSatisfiable(this.RightFragmentQuery);
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600459F RID: 17823
		internal abstract Set<MemberPath> Attributes { get; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060045A0 RID: 17824
		internal abstract List<CellTreeNode> Children { get; }

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060045A1 RID: 17825
		internal abstract int NumProjectedSlots { get; }

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060045A2 RID: 17826
		internal abstract int NumBoolSlots { get; }

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x000F5D82 File Offset: 0x000F3F82
		internal MemberProjectionIndex ProjectedSlotMap
		{
			get
			{
				return this.m_viewgenContext.MemberMaps.ProjectedSlotMap;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060045A4 RID: 17828 RVA: 0x000F5D94 File Offset: 0x000F3F94
		internal ViewgenContext ViewgenContext
		{
			get
			{
				return this.m_viewgenContext;
			}
		}

		// Token: 0x060045A5 RID: 17829
		internal abstract CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships);

		// Token: 0x060045A6 RID: 17830
		internal abstract bool IsProjectedSlot(int slot);

		// Token: 0x060045A7 RID: 17831
		internal abstract TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param);

		// Token: 0x060045A8 RID: 17832
		internal abstract TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param);

		// Token: 0x060045A9 RID: 17833 RVA: 0x000F5D9C File Offset: 0x000F3F9C
		internal CellTreeNode Flatten()
		{
			return CellTreeNode.FlatteningVisitor.Flatten(this);
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x000F5DA4 File Offset: 0x000F3FA4
		internal List<LeftCellWrapper> GetLeaves()
		{
			return (from leafNode in this.GetLeafNodes()
				select leafNode.LeftCellWrapper).ToList<LeftCellWrapper>();
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x000F5DD5 File Offset: 0x000F3FD5
		internal IEnumerable<LeafCellTreeNode> GetLeafNodes()
		{
			return CellTreeNode.LeafVisitor.GetLeaves(this);
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x000F5DDD File Offset: 0x000F3FDD
		internal CellTreeNode AssociativeFlatten()
		{
			return CellTreeNode.AssociativeOpFlatteningVisitor.Flatten(this);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x000F5DE5 File Offset: 0x000F3FE5
		internal static bool IsAssociativeOp(CellTreeOpType opType)
		{
			return opType == CellTreeOpType.IJ || opType == CellTreeOpType.Union || opType == CellTreeOpType.FOJ;
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x000F5DF8 File Offset: 0x000F3FF8
		internal bool[] GetProjectedSlots()
		{
			int num = this.ProjectedSlotMap.Count + this.NumBoolSlots;
			bool[] array = new bool[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.IsProjectedSlot(i);
			}
			return array;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x000F5E36 File Offset: 0x000F4036
		protected MemberPath GetMemberPath(int slotNum)
		{
			return this.ProjectedSlotMap.GetMemberPath(slotNum, this.NumBoolSlots);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x000F5E4A File Offset: 0x000F404A
		protected int BoolIndexToSlot(int boolIndex)
		{
			return this.ProjectedSlotMap.BoolIndexToSlot(boolIndex, this.NumBoolSlots);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x000F5E5E File Offset: 0x000F405E
		protected int SlotToBoolIndex(int slotNum)
		{
			return this.ProjectedSlotMap.SlotToBoolIndex(slotNum, this.NumBoolSlots);
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x000F5E72 File Offset: 0x000F4072
		protected bool IsKeySlot(int slotNum)
		{
			return this.ProjectedSlotMap.IsKeySlot(slotNum, this.NumBoolSlots);
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x000F5E86 File Offset: 0x000F4086
		protected bool IsBoolSlot(int slotNum)
		{
			return this.ProjectedSlotMap.IsBoolSlot(slotNum, this.NumBoolSlots);
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x000F5E9A File Offset: 0x000F409A
		protected IEnumerable<int> KeySlots
		{
			get
			{
				int numMembers = this.ProjectedSlotMap.Count;
				int num;
				for (int slotNum = 0; slotNum < numMembers; slotNum = num + 1)
				{
					if (this.IsKeySlot(slotNum))
					{
						yield return slotNum;
					}
					num = slotNum;
				}
				yield break;
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x000F5EAC File Offset: 0x000F40AC
		internal override void ToFullString(StringBuilder builder)
		{
			int num = 0;
			bool[] projectedSlots = this.GetProjectedSlots();
			CqlIdentifiers cqlIdentifiers = new CqlIdentifiers();
			List<WithRelationship> list = new List<WithRelationship>();
			this.ToCqlBlock(projectedSlots, cqlIdentifiers, ref num, ref list).AsEsql(builder, false, 1);
		}

		// Token: 0x040018F1 RID: 6385
		private readonly ViewgenContext m_viewgenContext;

		// Token: 0x02000BBB RID: 3003
		internal abstract class CellTreeVisitor<TInput, TOutput>
		{
			// Token: 0x06006789 RID: 26505
			internal abstract TOutput VisitLeaf(LeafCellTreeNode node, TInput param);

			// Token: 0x0600678A RID: 26506
			internal abstract TOutput VisitUnion(OpCellTreeNode node, TInput param);

			// Token: 0x0600678B RID: 26507
			internal abstract TOutput VisitInnerJoin(OpCellTreeNode node, TInput param);

			// Token: 0x0600678C RID: 26508
			internal abstract TOutput VisitLeftOuterJoin(OpCellTreeNode node, TInput param);

			// Token: 0x0600678D RID: 26509
			internal abstract TOutput VisitFullOuterJoin(OpCellTreeNode node, TInput param);

			// Token: 0x0600678E RID: 26510
			internal abstract TOutput VisitLeftAntiSemiJoin(OpCellTreeNode node, TInput param);
		}

		// Token: 0x02000BBC RID: 3004
		internal abstract class SimpleCellTreeVisitor<TInput, TOutput>
		{
			// Token: 0x06006790 RID: 26512
			internal abstract TOutput VisitLeaf(LeafCellTreeNode node, TInput param);

			// Token: 0x06006791 RID: 26513
			internal abstract TOutput VisitOpNode(OpCellTreeNode node, TInput param);
		}

		// Token: 0x02000BBD RID: 3005
		private class DefaultCellTreeVisitor<TInput> : CellTreeNode.CellTreeVisitor<TInput, CellTreeNode>
		{
			// Token: 0x06006793 RID: 26515 RVA: 0x00162053 File Offset: 0x00160253
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, TInput param)
			{
				return node;
			}

			// Token: 0x06006794 RID: 26516 RVA: 0x00162056 File Offset: 0x00160256
			internal override CellTreeNode VisitUnion(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06006795 RID: 26517 RVA: 0x00162060 File Offset: 0x00160260
			internal override CellTreeNode VisitInnerJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06006796 RID: 26518 RVA: 0x0016206A File Offset: 0x0016026A
			internal override CellTreeNode VisitLeftOuterJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06006797 RID: 26519 RVA: 0x00162074 File Offset: 0x00160274
			internal override CellTreeNode VisitFullOuterJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06006798 RID: 26520 RVA: 0x0016207E File Offset: 0x0016027E
			internal override CellTreeNode VisitLeftAntiSemiJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06006799 RID: 26521 RVA: 0x00162088 File Offset: 0x00160288
			private OpCellTreeNode AcceptChildren(OpCellTreeNode node, TInput param)
			{
				List<CellTreeNode> list = new List<CellTreeNode>();
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					list.Add(cellTreeNode.Accept<TInput, CellTreeNode>(this, param));
				}
				return new OpCellTreeNode(node.ViewgenContext, node.OpType, list);
			}
		}

		// Token: 0x02000BBE RID: 3006
		private class FlatteningVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, CellTreeNode>
		{
			// Token: 0x0600679B RID: 26523 RVA: 0x00162104 File Offset: 0x00160304
			protected FlatteningVisitor()
			{
			}

			// Token: 0x0600679C RID: 26524 RVA: 0x0016210C File Offset: 0x0016030C
			internal static CellTreeNode Flatten(CellTreeNode node)
			{
				CellTreeNode.FlatteningVisitor flatteningVisitor = new CellTreeNode.FlatteningVisitor();
				return node.Accept<bool, CellTreeNode>(flatteningVisitor, true);
			}

			// Token: 0x0600679D RID: 26525 RVA: 0x00162127 File Offset: 0x00160327
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				return node;
			}

			// Token: 0x0600679E RID: 26526 RVA: 0x0016212C File Offset: 0x0016032C
			internal override CellTreeNode VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				List<CellTreeNode> list = new List<CellTreeNode>();
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					CellTreeNode cellTreeNode2 = cellTreeNode.Accept<bool, CellTreeNode>(this, dummy);
					list.Add(cellTreeNode2);
				}
				if (list.Count == 1)
				{
					return list[0];
				}
				return new OpCellTreeNode(node.ViewgenContext, node.OpType, list);
			}
		}

		// Token: 0x02000BBF RID: 3007
		private class AssociativeOpFlatteningVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, CellTreeNode>
		{
			// Token: 0x0600679F RID: 26527 RVA: 0x001621B0 File Offset: 0x001603B0
			private AssociativeOpFlatteningVisitor()
			{
			}

			// Token: 0x060067A0 RID: 26528 RVA: 0x001621B8 File Offset: 0x001603B8
			internal static CellTreeNode Flatten(CellTreeNode node)
			{
				CellTreeNode cellTreeNode = CellTreeNode.FlatteningVisitor.Flatten(node);
				CellTreeNode.AssociativeOpFlatteningVisitor associativeOpFlatteningVisitor = new CellTreeNode.AssociativeOpFlatteningVisitor();
				return cellTreeNode.Accept<bool, CellTreeNode>(associativeOpFlatteningVisitor, true);
			}

			// Token: 0x060067A1 RID: 26529 RVA: 0x001621D8 File Offset: 0x001603D8
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				return node;
			}

			// Token: 0x060067A2 RID: 26530 RVA: 0x001621DC File Offset: 0x001603DC
			internal override CellTreeNode VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				List<CellTreeNode> list = new List<CellTreeNode>();
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					CellTreeNode cellTreeNode2 = cellTreeNode.Accept<bool, CellTreeNode>(this, dummy);
					list.Add(cellTreeNode2);
				}
				List<CellTreeNode> list2 = list;
				if (CellTreeNode.IsAssociativeOp(node.OpType))
				{
					list2 = new List<CellTreeNode>();
					foreach (CellTreeNode cellTreeNode3 in list)
					{
						if (cellTreeNode3.OpType == node.OpType)
						{
							list2.AddRange(cellTreeNode3.Children);
						}
						else
						{
							list2.Add(cellTreeNode3);
						}
					}
				}
				return new OpCellTreeNode(node.ViewgenContext, node.OpType, list2);
			}
		}

		// Token: 0x02000BC0 RID: 3008
		private class LeafVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, IEnumerable<LeafCellTreeNode>>
		{
			// Token: 0x060067A3 RID: 26531 RVA: 0x001622C0 File Offset: 0x001604C0
			private LeafVisitor()
			{
			}

			// Token: 0x060067A4 RID: 26532 RVA: 0x001622C8 File Offset: 0x001604C8
			internal static IEnumerable<LeafCellTreeNode> GetLeaves(CellTreeNode node)
			{
				CellTreeNode.LeafVisitor leafVisitor = new CellTreeNode.LeafVisitor();
				return node.Accept<bool, IEnumerable<LeafCellTreeNode>>(leafVisitor, true);
			}

			// Token: 0x060067A5 RID: 26533 RVA: 0x001622E3 File Offset: 0x001604E3
			internal override IEnumerable<LeafCellTreeNode> VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				yield return node;
				yield break;
			}

			// Token: 0x060067A6 RID: 26534 RVA: 0x001622F3 File Offset: 0x001604F3
			internal override IEnumerable<LeafCellTreeNode> VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					IEnumerable<LeafCellTreeNode> enumerable = cellTreeNode.Accept<bool, IEnumerable<LeafCellTreeNode>>(this, dummy);
					foreach (LeafCellTreeNode leafCellTreeNode in enumerable)
					{
						yield return leafCellTreeNode;
					}
					IEnumerator<LeafCellTreeNode> enumerator2 = null;
				}
				List<CellTreeNode>.Enumerator enumerator = default(List<CellTreeNode>.Enumerator);
				yield break;
				yield break;
			}
		}
	}
}
