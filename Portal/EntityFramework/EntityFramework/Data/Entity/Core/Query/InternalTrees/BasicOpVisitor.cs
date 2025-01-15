using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200037F RID: 895
	internal abstract class BasicOpVisitor
	{
		// Token: 0x06002B15 RID: 11029 RVA: 0x0008D63C File Offset: 0x0008B83C
		protected virtual void VisitChildren(Node n)
		{
			foreach (Node node in n.Children)
			{
				this.VisitNode(node);
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0008D690 File Offset: 0x0008B890
		protected virtual void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0008D6C7 File Offset: 0x0008B8C7
		internal virtual void VisitNode(Node n)
		{
			n.Op.Accept(this, n);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0008D6D6 File Offset: 0x0008B8D6
		protected virtual void VisitDefault(Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0008D6DF File Offset: 0x0008B8DF
		protected virtual void VisitConstantOp(ConstantBaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0008D6E9 File Offset: 0x0008B8E9
		protected virtual void VisitTableOp(ScanTableBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0008D6F3 File Offset: 0x0008B8F3
		protected virtual void VisitJoinOp(JoinBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0008D6FD File Offset: 0x0008B8FD
		protected virtual void VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0008D707 File Offset: 0x0008B907
		protected virtual void VisitSetOp(SetOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0008D711 File Offset: 0x0008B911
		protected virtual void VisitSortOp(SortBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0008D71B File Offset: 0x0008B91B
		protected virtual void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0008D725 File Offset: 0x0008B925
		public virtual void Visit(Op op, Node n)
		{
			throw new NotSupportedException(Strings.Iqt_General_UnsupportedOp(op.GetType().FullName));
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0008D73C File Offset: 0x0008B93C
		protected virtual void VisitScalarOpDefault(ScalarOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0008D745 File Offset: 0x0008B945
		public virtual void Visit(ConstantOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0008D74F File Offset: 0x0008B94F
		public virtual void Visit(NullOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x0008D759 File Offset: 0x0008B959
		public virtual void Visit(NullSentinelOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x0008D763 File Offset: 0x0008B963
		public virtual void Visit(InternalConstantOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x0008D76D File Offset: 0x0008B96D
		public virtual void Visit(ConstantPredicateOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x0008D777 File Offset: 0x0008B977
		public virtual void Visit(FunctionOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0008D781 File Offset: 0x0008B981
		public virtual void Visit(PropertyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0008D78B File Offset: 0x0008B98B
		public virtual void Visit(RelPropertyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0008D795 File Offset: 0x0008B995
		public virtual void Visit(CaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0008D79F File Offset: 0x0008B99F
		public virtual void Visit(ComparisonOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0008D7A9 File Offset: 0x0008B9A9
		public virtual void Visit(LikeOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0008D7B3 File Offset: 0x0008B9B3
		public virtual void Visit(AggregateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0008D7BD File Offset: 0x0008B9BD
		public virtual void Visit(NewInstanceOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x0008D7C7 File Offset: 0x0008B9C7
		public virtual void Visit(NewEntityOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x0008D7D1 File Offset: 0x0008B9D1
		public virtual void Visit(DiscriminatedNewEntityOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x0008D7DB File Offset: 0x0008B9DB
		public virtual void Visit(NewMultisetOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0008D7E5 File Offset: 0x0008B9E5
		public virtual void Visit(NewRecordOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x0008D7EF File Offset: 0x0008B9EF
		public virtual void Visit(RefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x0008D7F9 File Offset: 0x0008B9F9
		public virtual void Visit(VarRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0008D803 File Offset: 0x0008BA03
		public virtual void Visit(ConditionalOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0008D80D File Offset: 0x0008BA0D
		public virtual void Visit(ArithmeticOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0008D817 File Offset: 0x0008BA17
		public virtual void Visit(TreatOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0008D821 File Offset: 0x0008BA21
		public virtual void Visit(CastOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0008D82B File Offset: 0x0008BA2B
		public virtual void Visit(SoftCastOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0008D835 File Offset: 0x0008BA35
		public virtual void Visit(IsOfOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0008D83F File Offset: 0x0008BA3F
		public virtual void Visit(ExistsOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0008D849 File Offset: 0x0008BA49
		public virtual void Visit(ElementOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0008D853 File Offset: 0x0008BA53
		public virtual void Visit(GetEntityRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0008D85D File Offset: 0x0008BA5D
		public virtual void Visit(GetRefKeyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0008D867 File Offset: 0x0008BA67
		public virtual void Visit(CollectOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0008D871 File Offset: 0x0008BA71
		public virtual void Visit(DerefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0008D87B File Offset: 0x0008BA7B
		public virtual void Visit(NavigateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0008D885 File Offset: 0x0008BA85
		protected virtual void VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0008D88E File Offset: 0x0008BA8E
		public virtual void Visit(VarDefOp op, Node n)
		{
			this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0008D898 File Offset: 0x0008BA98
		public virtual void Visit(VarDefListOp op, Node n)
		{
			this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0008D8A2 File Offset: 0x0008BAA2
		protected virtual void VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0008D8AB File Offset: 0x0008BAAB
		public virtual void Visit(ScanTableOp op, Node n)
		{
			this.VisitTableOp(op, n);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0008D8B5 File Offset: 0x0008BAB5
		public virtual void Visit(ScanViewOp op, Node n)
		{
			this.VisitTableOp(op, n);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0008D8BF File Offset: 0x0008BABF
		public virtual void Visit(UnnestOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0008D8C9 File Offset: 0x0008BAC9
		public virtual void Visit(ProjectOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0008D8D3 File Offset: 0x0008BAD3
		public virtual void Visit(FilterOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0008D8DD File Offset: 0x0008BADD
		public virtual void Visit(SortOp op, Node n)
		{
			this.VisitSortOp(op, n);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0008D8E7 File Offset: 0x0008BAE7
		public virtual void Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitSortOp(op, n);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0008D8F1 File Offset: 0x0008BAF1
		public virtual void Visit(GroupByOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0008D8FB File Offset: 0x0008BAFB
		public virtual void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0008D905 File Offset: 0x0008BB05
		public virtual void Visit(CrossJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0008D90F File Offset: 0x0008BB0F
		public virtual void Visit(InnerJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0008D919 File Offset: 0x0008BB19
		public virtual void Visit(LeftOuterJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0008D923 File Offset: 0x0008BB23
		public virtual void Visit(FullOuterJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0008D92D File Offset: 0x0008BB2D
		public virtual void Visit(CrossApplyOp op, Node n)
		{
			this.VisitApplyOp(op, n);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0008D937 File Offset: 0x0008BB37
		public virtual void Visit(OuterApplyOp op, Node n)
		{
			this.VisitApplyOp(op, n);
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0008D941 File Offset: 0x0008BB41
		public virtual void Visit(UnionAllOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x0008D94B File Offset: 0x0008BB4B
		public virtual void Visit(IntersectOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0008D955 File Offset: 0x0008BB55
		public virtual void Visit(ExceptOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0008D95F File Offset: 0x0008BB5F
		public virtual void Visit(DistinctOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0008D969 File Offset: 0x0008BB69
		public virtual void Visit(SingleRowOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x0008D973 File Offset: 0x0008BB73
		public virtual void Visit(SingleRowTableOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0008D97D File Offset: 0x0008BB7D
		protected virtual void VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0008D986 File Offset: 0x0008BB86
		public virtual void Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0008D990 File Offset: 0x0008BB90
		protected virtual void VisitNestOp(NestBaseOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0008D99A File Offset: 0x0008BB9A
		public virtual void Visit(SingleStreamNestOp op, Node n)
		{
			this.VisitNestOp(op, n);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0008D9A4 File Offset: 0x0008BBA4
		public virtual void Visit(MultiStreamNestOp op, Node n)
		{
			this.VisitNestOp(op, n);
		}
	}
}
