using System;
using System.Data.Entity.Core.Query.PlanCompiler;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000381 RID: 897
	internal abstract class BasicOpVisitorOfT<TResultType>
	{
		// Token: 0x06002B69 RID: 11113 RVA: 0x0008DA78 File Offset: 0x0008BC78
		protected virtual void VisitChildren(Node n)
		{
			for (int i = 0; i < n.Children.Count; i++)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0008DAB0 File Offset: 0x0008BCB0
		protected virtual void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0008DAE8 File Offset: 0x0008BCE8
		internal TResultType VisitNode(Node n)
		{
			return n.Op.Accept<TResultType>(this, n);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0008DAF8 File Offset: 0x0008BCF8
		protected virtual TResultType VisitDefault(Node n)
		{
			this.VisitChildren(n);
			return default(TResultType);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0008DB18 File Offset: 0x0008BD18
		internal virtual TResultType Unimplemented(Node n)
		{
			PlanCompiler.Assert(false, "Not implemented op type");
			return default(TResultType);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0008DB39 File Offset: 0x0008BD39
		public virtual TResultType Visit(Op op, Node n)
		{
			return this.Unimplemented(n);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0008DB42 File Offset: 0x0008BD42
		protected virtual TResultType VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0008DB4B File Offset: 0x0008BD4B
		public virtual TResultType Visit(VarDefOp op, Node n)
		{
			return this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0008DB55 File Offset: 0x0008BD55
		public virtual TResultType Visit(VarDefListOp op, Node n)
		{
			return this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0008DB5F File Offset: 0x0008BD5F
		protected virtual TResultType VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0008DB68 File Offset: 0x0008BD68
		public virtual TResultType Visit(PhysicalProjectOp op, Node n)
		{
			return this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0008DB72 File Offset: 0x0008BD72
		protected virtual TResultType VisitNestOp(NestBaseOp op, Node n)
		{
			return this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0008DB7C File Offset: 0x0008BD7C
		public virtual TResultType Visit(SingleStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0008DB86 File Offset: 0x0008BD86
		public virtual TResultType Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0008DB90 File Offset: 0x0008BD90
		protected virtual TResultType VisitRelOpDefault(RelOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0008DB99 File Offset: 0x0008BD99
		protected virtual TResultType VisitApplyOp(ApplyBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0008DBA3 File Offset: 0x0008BDA3
		public virtual TResultType Visit(CrossApplyOp op, Node n)
		{
			return this.VisitApplyOp(op, n);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0008DBAD File Offset: 0x0008BDAD
		public virtual TResultType Visit(OuterApplyOp op, Node n)
		{
			return this.VisitApplyOp(op, n);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0008DBB7 File Offset: 0x0008BDB7
		protected virtual TResultType VisitJoinOp(JoinBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0008DBC1 File Offset: 0x0008BDC1
		public virtual TResultType Visit(CrossJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0008DBCB File Offset: 0x0008BDCB
		public virtual TResultType Visit(FullOuterJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0008DBD5 File Offset: 0x0008BDD5
		public virtual TResultType Visit(LeftOuterJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0008DBDF File Offset: 0x0008BDDF
		public virtual TResultType Visit(InnerJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0008DBE9 File Offset: 0x0008BDE9
		protected virtual TResultType VisitSetOp(SetOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0008DBF3 File Offset: 0x0008BDF3
		public virtual TResultType Visit(ExceptOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0008DBFD File Offset: 0x0008BDFD
		public virtual TResultType Visit(IntersectOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0008DC07 File Offset: 0x0008BE07
		public virtual TResultType Visit(UnionAllOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0008DC11 File Offset: 0x0008BE11
		public virtual TResultType Visit(DistinctOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0008DC1B File Offset: 0x0008BE1B
		public virtual TResultType Visit(FilterOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0008DC25 File Offset: 0x0008BE25
		protected virtual TResultType VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x0008DC2F File Offset: 0x0008BE2F
		public virtual TResultType Visit(GroupByOp op, Node n)
		{
			return this.VisitGroupByOp(op, n);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0008DC39 File Offset: 0x0008BE39
		public virtual TResultType Visit(GroupByIntoOp op, Node n)
		{
			return this.VisitGroupByOp(op, n);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0008DC43 File Offset: 0x0008BE43
		public virtual TResultType Visit(ProjectOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0008DC4D File Offset: 0x0008BE4D
		protected virtual TResultType VisitTableOp(ScanTableBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0008DC57 File Offset: 0x0008BE57
		public virtual TResultType Visit(ScanTableOp op, Node n)
		{
			return this.VisitTableOp(op, n);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0008DC61 File Offset: 0x0008BE61
		public virtual TResultType Visit(ScanViewOp op, Node n)
		{
			return this.VisitTableOp(op, n);
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0008DC6B File Offset: 0x0008BE6B
		public virtual TResultType Visit(SingleRowOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x0008DC75 File Offset: 0x0008BE75
		public virtual TResultType Visit(SingleRowTableOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x0008DC7F File Offset: 0x0008BE7F
		protected virtual TResultType VisitSortOp(SortBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x0008DC89 File Offset: 0x0008BE89
		public virtual TResultType Visit(SortOp op, Node n)
		{
			return this.VisitSortOp(op, n);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x0008DC93 File Offset: 0x0008BE93
		public virtual TResultType Visit(ConstrainedSortOp op, Node n)
		{
			return this.VisitSortOp(op, n);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x0008DC9D File Offset: 0x0008BE9D
		public virtual TResultType Visit(UnnestOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x0008DCA7 File Offset: 0x0008BEA7
		protected virtual TResultType VisitScalarOpDefault(ScalarOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x0008DCB0 File Offset: 0x0008BEB0
		protected virtual TResultType VisitConstantOp(ConstantBaseOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0008DCBA File Offset: 0x0008BEBA
		public virtual TResultType Visit(AggregateOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0008DCC4 File Offset: 0x0008BEC4
		public virtual TResultType Visit(ArithmeticOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0008DCCE File Offset: 0x0008BECE
		public virtual TResultType Visit(CaseOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x0008DCD8 File Offset: 0x0008BED8
		public virtual TResultType Visit(CastOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0008DCE2 File Offset: 0x0008BEE2
		public virtual TResultType Visit(SoftCastOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0008DCEC File Offset: 0x0008BEEC
		public virtual TResultType Visit(CollectOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0008DCF6 File Offset: 0x0008BEF6
		public virtual TResultType Visit(ComparisonOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0008DD00 File Offset: 0x0008BF00
		public virtual TResultType Visit(ConditionalOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0008DD0A File Offset: 0x0008BF0A
		public virtual TResultType Visit(ConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0008DD14 File Offset: 0x0008BF14
		public virtual TResultType Visit(ConstantPredicateOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0008DD1E File Offset: 0x0008BF1E
		public virtual TResultType Visit(ElementOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0008DD28 File Offset: 0x0008BF28
		public virtual TResultType Visit(ExistsOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0008DD32 File Offset: 0x0008BF32
		public virtual TResultType Visit(FunctionOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0008DD3C File Offset: 0x0008BF3C
		public virtual TResultType Visit(GetEntityRefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0008DD46 File Offset: 0x0008BF46
		public virtual TResultType Visit(GetRefKeyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0008DD50 File Offset: 0x0008BF50
		public virtual TResultType Visit(InternalConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0008DD5A File Offset: 0x0008BF5A
		public virtual TResultType Visit(IsOfOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0008DD64 File Offset: 0x0008BF64
		public virtual TResultType Visit(LikeOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0008DD6E File Offset: 0x0008BF6E
		public virtual TResultType Visit(NewEntityOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0008DD78 File Offset: 0x0008BF78
		public virtual TResultType Visit(NewInstanceOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0008DD82 File Offset: 0x0008BF82
		public virtual TResultType Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0008DD8C File Offset: 0x0008BF8C
		public virtual TResultType Visit(NewMultisetOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0008DD96 File Offset: 0x0008BF96
		public virtual TResultType Visit(NewRecordOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0008DDA0 File Offset: 0x0008BFA0
		public virtual TResultType Visit(NullOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0008DDAA File Offset: 0x0008BFAA
		public virtual TResultType Visit(NullSentinelOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0008DDB4 File Offset: 0x0008BFB4
		public virtual TResultType Visit(PropertyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0008DDBE File Offset: 0x0008BFBE
		public virtual TResultType Visit(RelPropertyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x0008DDC8 File Offset: 0x0008BFC8
		public virtual TResultType Visit(RefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0008DDD2 File Offset: 0x0008BFD2
		public virtual TResultType Visit(TreatOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x0008DDDC File Offset: 0x0008BFDC
		public virtual TResultType Visit(VarRefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x0008DDE6 File Offset: 0x0008BFE6
		public virtual TResultType Visit(DerefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x0008DDF0 File Offset: 0x0008BFF0
		public virtual TResultType Visit(NavigateOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}
	}
}
