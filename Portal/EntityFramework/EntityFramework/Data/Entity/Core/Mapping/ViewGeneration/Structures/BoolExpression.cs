using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000595 RID: 1429
	internal class BoolExpression : InternalBase
	{
		// Token: 0x06004500 RID: 17664 RVA: 0x000F3E26 File Offset: 0x000F2026
		internal static BoolExpression CreateLiteral(BoolLiteral literal, MemberDomainMap memberDomainMap)
		{
			return new BoolExpression(literal.GetDomainBoolExpression(memberDomainMap), memberDomainMap);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x000F3E35 File Offset: 0x000F2035
		internal BoolExpression Create(BoolLiteral literal)
		{
			return new BoolExpression(literal.GetDomainBoolExpression(this.m_memberDomainMap), this.m_memberDomainMap);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000F3E4E File Offset: 0x000F204E
		internal static BoolExpression CreateNot(BoolExpression expression)
		{
			return new BoolExpression(ExprType.Not, new BoolExpression[] { expression });
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000F3E60 File Offset: 0x000F2060
		internal static BoolExpression CreateAnd(params BoolExpression[] children)
		{
			return new BoolExpression(ExprType.And, children);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000F3E69 File Offset: 0x000F2069
		internal static BoolExpression CreateOr(params BoolExpression[] children)
		{
			return new BoolExpression(ExprType.Or, children);
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000F3E72 File Offset: 0x000F2072
		internal static BoolExpression CreateAndNot(BoolExpression e1, BoolExpression e2)
		{
			return BoolExpression.CreateAnd(new BoolExpression[]
			{
				e1,
				BoolExpression.CreateNot(e2)
			});
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x000F3E8C File Offset: 0x000F208C
		internal BoolExpression Create(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			return new BoolExpression(expression, this.m_memberDomainMap);
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x000F3E9A File Offset: 0x000F209A
		private BoolExpression(bool isTrue)
		{
			if (isTrue)
			{
				this.m_tree = TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				return;
			}
			this.m_tree = FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x000F3EBC File Offset: 0x000F20BC
		private BoolExpression(ExprType opType, IEnumerable<BoolExpression> children)
		{
			List<BoolExpression> list = new List<BoolExpression>(children);
			foreach (BoolExpression boolExpression in children)
			{
				if (boolExpression.m_memberDomainMap != null)
				{
					this.m_memberDomainMap = boolExpression.m_memberDomainMap;
					break;
				}
			}
			switch (opType)
			{
			case ExprType.And:
				this.m_tree = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(BoolExpression.ToBoolExprList(list));
				return;
			case ExprType.Not:
				this.m_tree = new NotExpr<DomainConstraint<BoolLiteral, Constant>>(list[0].m_tree);
				return;
			case ExprType.Or:
				this.m_tree = new OrExpr<DomainConstraint<BoolLiteral, Constant>>(BoolExpression.ToBoolExprList(list));
				return;
			default:
				return;
			}
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x000F3F70 File Offset: 0x000F2170
		internal BoolExpression(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr, MemberDomainMap memberDomainMap)
		{
			this.m_tree = expr;
			this.m_memberDomainMap = memberDomainMap;
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x000F3F86 File Offset: 0x000F2186
		internal IEnumerable<BoolExpression> Atoms
		{
			get
			{
				IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> terms = BoolExpression.TermVisitor.GetTerms(this.m_tree, false);
				foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr in terms)
				{
					yield return new BoolExpression(termExpr, this.m_memberDomainMap);
				}
				IEnumerator<TermExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x000F3F98 File Offset: 0x000F2198
		internal BoolLiteral AsLiteral
		{
			get
			{
				TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr = this.m_tree as TermExpr<DomainConstraint<BoolLiteral, Constant>>;
				if (termExpr == null)
				{
					return null;
				}
				return BoolExpression.GetBoolLiteral(termExpr);
			}
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x000F3FBC File Offset: 0x000F21BC
		internal static BoolLiteral GetBoolLiteral(TermExpr<DomainConstraint<BoolLiteral, Constant>> term)
		{
			return term.Identifier.Variable.Identifier;
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x000F3FCE File Offset: 0x000F21CE
		internal bool IsTrue
		{
			get
			{
				return this.m_tree.ExprType == ExprType.True;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x000F3FDE File Offset: 0x000F21DE
		internal bool IsFalse
		{
			get
			{
				return this.m_tree.ExprType == ExprType.False;
			}
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x000F3FEE File Offset: 0x000F21EE
		internal bool IsAlwaysTrue()
		{
			this.InitializeConverter();
			return this.m_converter.Vertex.IsOne();
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000F4006 File Offset: 0x000F2206
		internal bool IsSatisfiable()
		{
			return !this.IsUnsatisfiable();
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000F4011 File Offset: 0x000F2211
		internal bool IsUnsatisfiable()
		{
			this.InitializeConverter();
			return this.m_converter.Vertex.IsZero();
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x000F4029 File Offset: 0x000F2229
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Tree
		{
			get
			{
				return this.m_tree;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x000F4031 File Offset: 0x000F2231
		internal IEnumerable<DomainConstraint<BoolLiteral, Constant>> VariableConstraints
		{
			get
			{
				return LeafVisitor<DomainConstraint<BoolLiteral, Constant>>.GetLeaves(this.m_tree);
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x000F403E File Offset: 0x000F223E
		internal IEnumerable<DomainVariable<BoolLiteral, Constant>> Variables
		{
			get
			{
				return this.VariableConstraints.Select((DomainConstraint<BoolLiteral, Constant> domainConstraint) => domainConstraint.Variable);
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x000F406A File Offset: 0x000F226A
		internal IEnumerable<MemberRestriction> MemberRestrictions
		{
			get
			{
				foreach (DomainVariable<BoolLiteral, Constant> domainVariable in this.Variables)
				{
					MemberRestriction memberRestriction = domainVariable.Identifier as MemberRestriction;
					if (memberRestriction != null)
					{
						yield return memberRestriction;
					}
				}
				IEnumerator<DomainVariable<BoolLiteral, Constant>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000F407A File Offset: 0x000F227A
		private static IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> ToBoolExprList(IEnumerable<BoolExpression> nodes)
		{
			foreach (BoolExpression boolExpression in nodes)
			{
				yield return boolExpression.m_tree;
			}
			IEnumerator<BoolExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x000F408A File Offset: 0x000F228A
		internal bool RepresentsAllTypeConditions
		{
			get
			{
				return this.MemberRestrictions.All((MemberRestriction var) => var is TypeRestriction);
			}
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000F40B8 File Offset: 0x000F22B8
		internal BoolExpression RemapLiterals(Dictionary<BoolLiteral, BoolLiteral> remap)
		{
			BooleanExpressionTermRewriter<DomainConstraint<BoolLiteral, Constant>, DomainConstraint<BoolLiteral, Constant>> booleanExpressionTermRewriter = new BooleanExpressionTermRewriter<DomainConstraint<BoolLiteral, Constant>, DomainConstraint<BoolLiteral, Constant>>(delegate(TermExpr<DomainConstraint<BoolLiteral, Constant>> term)
			{
				BoolLiteral boolLiteral;
				if (!remap.TryGetValue(BoolExpression.GetBoolLiteral(term), out boolLiteral))
				{
					return term;
				}
				return boolLiteral.GetDomainBoolExpression(this.m_memberDomainMap);
			});
			return new BoolExpression(this.m_tree.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(booleanExpressionTermRewriter), this.m_memberDomainMap);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x000F4100 File Offset: 0x000F2300
		internal virtual void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			BoolExpression.RequiredSlotsVisitor.GetRequiredSlots(this.m_tree, projectedSlotMap, requiredSlots);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000F410F File Offset: 0x000F230F
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias)
		{
			return BoolExpression.AsEsqlVisitor.AsEsql(this.m_tree, builder, blockAlias);
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x000F411E File Offset: 0x000F231E
		internal DbExpression AsCqt(DbExpression row)
		{
			return BoolExpression.AsCqtVisitor.AsCqt(this.m_tree, row);
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000F412C File Offset: 0x000F232C
		internal StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool writeRoundtrippingMessage)
		{
			if (writeRoundtrippingMessage)
			{
				builder.AppendLine(Strings.Viewgen_ConfigurationErrorMsg(blockAlias));
				builder.Append("  ");
			}
			return BoolExpression.AsUserStringVisitor.AsUserString(this.m_tree, builder, blockAlias);
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000F4157 File Offset: 0x000F2357
		internal override void ToCompactString(StringBuilder builder)
		{
			BoolExpression.CompactStringVisitor.ToBuilder(this.m_tree, builder);
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000F4166 File Offset: 0x000F2366
		internal BoolExpression RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return new BoolExpression(BoolExpression.RemapBoolVisitor.RemapExtentTreeNodes(this.m_tree, this.m_memberDomainMap, remap), this.m_memberDomainMap);
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x000F4188 File Offset: 0x000F2388
		internal static List<BoolExpression> AddConjunctionToBools(List<BoolExpression> bools, BoolExpression conjunct)
		{
			List<BoolExpression> list = new List<BoolExpression>();
			foreach (BoolExpression boolExpression in bools)
			{
				if (boolExpression == null)
				{
					list.Add(null);
				}
				else
				{
					list.Add(BoolExpression.CreateAnd(new BoolExpression[] { boolExpression, conjunct }));
				}
			}
			return list;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x000F41FC File Offset: 0x000F23FC
		private void InitializeConverter()
		{
			if (this.m_converter != null)
			{
				return;
			}
			this.m_converter = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.m_tree, IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext());
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x000F4222 File Offset: 0x000F2422
		internal BoolExpression MakeCopy()
		{
			return this.Create(this.m_tree.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(BoolExpression._copyVisitorInstance));
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000F423C File Offset: 0x000F243C
		internal void ExpensiveSimplify()
		{
			if (!this.IsFinal())
			{
				this.m_tree = this.m_tree.Simplify();
				return;
			}
			this.InitializeConverter();
			this.m_tree = this.m_tree.ExpensiveSimplify(out this.m_converter);
			this.FixDomainMap(this.m_memberDomainMap);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x000F428C File Offset: 0x000F248C
		internal void FixDomainMap(MemberDomainMap domainMap)
		{
			this.m_tree = BoolExpression.FixRangeVisitor.FixRange(this.m_tree, domainMap);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x000F42A0 File Offset: 0x000F24A0
		private bool IsFinal()
		{
			return this.m_memberDomainMap != null && BoolExpression.IsFinalVisitor.IsFinal(this.m_tree);
		}

		// Token: 0x040018D1 RID: 6353
		private BoolExpr<DomainConstraint<BoolLiteral, Constant>> m_tree;

		// Token: 0x040018D2 RID: 6354
		private readonly MemberDomainMap m_memberDomainMap;

		// Token: 0x040018D3 RID: 6355
		private Converter<DomainConstraint<BoolLiteral, Constant>> m_converter;

		// Token: 0x040018D4 RID: 6356
		internal static readonly IEqualityComparer<BoolExpression> EqualityComparer = new BoolExpression.BoolComparer();

		// Token: 0x040018D5 RID: 6357
		internal static readonly BoolExpression True = new BoolExpression(true);

		// Token: 0x040018D6 RID: 6358
		internal static readonly BoolExpression False = new BoolExpression(false);

		// Token: 0x040018D7 RID: 6359
		private static readonly BoolExpression.CopyVisitor _copyVisitorInstance = new BoolExpression.CopyVisitor();

		// Token: 0x02000B9E RID: 2974
		private class CopyVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
		}

		// Token: 0x02000B9F RID: 2975
		private class BoolComparer : IEqualityComparer<BoolExpression>
		{
			// Token: 0x060066EA RID: 26346 RVA: 0x00160B17 File Offset: 0x0015ED17
			public bool Equals(BoolExpression left, BoolExpression right)
			{
				return left == right || (left != null && right != null && left.m_tree.Equals(right.m_tree));
			}

			// Token: 0x060066EB RID: 26347 RVA: 0x00160B38 File Offset: 0x0015ED38
			public int GetHashCode(BoolExpression expression)
			{
				return expression.m_tree.GetHashCode();
			}
		}

		// Token: 0x02000BA0 RID: 2976
		private class FixRangeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060066ED RID: 26349 RVA: 0x00160B4D File Offset: 0x0015ED4D
			private FixRangeVisitor(MemberDomainMap memberDomainMap)
			{
				this.m_memberDomainMap = memberDomainMap;
			}

			// Token: 0x060066EE RID: 26350 RVA: 0x00160B5C File Offset: 0x0015ED5C
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberDomainMap memberDomainMap)
			{
				BoolExpression.FixRangeVisitor fixRangeVisitor = new BoolExpression.FixRangeVisitor(memberDomainMap);
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(fixRangeVisitor);
			}

			// Token: 0x060066EF RID: 26351 RVA: 0x00160B77 File Offset: 0x0015ED77
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return BoolExpression.GetBoolLiteral(expression).FixRange(expression.Identifier.Range, this.m_memberDomainMap);
			}

			// Token: 0x04002E52 RID: 11858
			private readonly MemberDomainMap m_memberDomainMap;
		}

		// Token: 0x02000BA1 RID: 2977
		private class IsFinalVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, bool>
		{
			// Token: 0x060066F0 RID: 26352 RVA: 0x00160B98 File Offset: 0x0015ED98
			internal static bool IsFinal(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolExpression.IsFinalVisitor isFinalVisitor = new BoolExpression.IsFinalVisitor();
				return expression.Accept<bool>(isFinalVisitor);
			}

			// Token: 0x060066F1 RID: 26353 RVA: 0x00160BB2 File Offset: 0x0015EDB2
			internal override bool VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return true;
			}

			// Token: 0x060066F2 RID: 26354 RVA: 0x00160BB5 File Offset: 0x0015EDB5
			internal override bool VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return true;
			}

			// Token: 0x060066F3 RID: 26355 RVA: 0x00160BB8 File Offset: 0x0015EDB8
			internal override bool VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				MemberRestriction memberRestriction = BoolExpression.GetBoolLiteral(expression) as MemberRestriction;
				return memberRestriction == null || memberRestriction.IsComplete;
			}

			// Token: 0x060066F4 RID: 26356 RVA: 0x00160BDC File Offset: 0x0015EDDC
			internal override bool VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return expression.Child.Accept<bool>(this);
			}

			// Token: 0x060066F5 RID: 26357 RVA: 0x00160BEA File Offset: 0x0015EDEA
			internal override bool VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression);
			}

			// Token: 0x060066F6 RID: 26358 RVA: 0x00160BF3 File Offset: 0x0015EDF3
			internal override bool VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression);
			}

			// Token: 0x060066F7 RID: 26359 RVA: 0x00160BFC File Offset: 0x0015EDFC
			private bool VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				bool flag = true;
				bool flag2 = true;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (!(boolExpr is FalseExpr<DomainConstraint<BoolLiteral, Constant>>) && !(boolExpr is TrueExpr<DomainConstraint<BoolLiteral, Constant>>))
					{
						bool flag3 = boolExpr.Accept<bool>(this);
						if (flag)
						{
							flag2 = flag3;
						}
						flag = false;
					}
				}
				return flag2;
			}
		}

		// Token: 0x02000BA2 RID: 2978
		private class RemapBoolVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060066F9 RID: 26361 RVA: 0x00160C78 File Offset: 0x0015EE78
			private RemapBoolVisitor(MemberDomainMap memberDomainMap, Dictionary<MemberPath, MemberPath> remap)
			{
				this.m_remap = remap;
				this.m_memberDomainMap = memberDomainMap;
			}

			// Token: 0x060066FA RID: 26362 RVA: 0x00160C90 File Offset: 0x0015EE90
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> RemapExtentTreeNodes(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberDomainMap memberDomainMap, Dictionary<MemberPath, MemberPath> remap)
			{
				BoolExpression.RemapBoolVisitor remapBoolVisitor = new BoolExpression.RemapBoolVisitor(memberDomainMap, remap);
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(remapBoolVisitor);
			}

			// Token: 0x060066FB RID: 26363 RVA: 0x00160CAC File Offset: 0x0015EEAC
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return BoolExpression.GetBoolLiteral(expression).RemapBool(this.m_remap).GetDomainBoolExpression(this.m_memberDomainMap);
			}

			// Token: 0x04002E53 RID: 11859
			private readonly Dictionary<MemberPath, MemberPath> m_remap;

			// Token: 0x04002E54 RID: 11860
			private readonly MemberDomainMap m_memberDomainMap;
		}

		// Token: 0x02000BA3 RID: 2979
		private class RequiredSlotsVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060066FC RID: 26364 RVA: 0x00160CCA File Offset: 0x0015EECA
			private RequiredSlotsVisitor(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
			{
				this.m_projectedSlotMap = projectedSlotMap;
				this.m_requiredSlots = requiredSlots;
			}

			// Token: 0x060066FD RID: 26365 RVA: 0x00160CE0 File Offset: 0x0015EEE0
			internal static void GetRequiredSlots(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
			{
				BoolExpression.RequiredSlotsVisitor requiredSlotsVisitor = new BoolExpression.RequiredSlotsVisitor(projectedSlotMap, requiredSlots);
				expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(requiredSlotsVisitor);
			}

			// Token: 0x060066FE RID: 26366 RVA: 0x00160CFD File Offset: 0x0015EEFD
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolExpression.GetBoolLiteral(expression).GetRequiredSlots(this.m_projectedSlotMap, this.m_requiredSlots);
				return expression;
			}

			// Token: 0x04002E55 RID: 11861
			private readonly MemberProjectionIndex m_projectedSlotMap;

			// Token: 0x04002E56 RID: 11862
			private readonly bool[] m_requiredSlots;
		}

		// Token: 0x02000BA4 RID: 2980
		private sealed class AsEsqlVisitor : BoolExpression.AsCqlVisitor<StringBuilder>
		{
			// Token: 0x060066FF RID: 26367 RVA: 0x00160D18 File Offset: 0x0015EF18
			internal static StringBuilder AsEsql(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder, string blockAlias)
			{
				BoolExpression.AsEsqlVisitor asEsqlVisitor = new BoolExpression.AsEsqlVisitor(builder, blockAlias);
				return expression.Accept<StringBuilder>(asEsqlVisitor);
			}

			// Token: 0x06006700 RID: 26368 RVA: 0x00160D34 File Offset: 0x0015EF34
			private AsEsqlVisitor(StringBuilder builder, string blockAlias)
			{
				this.m_builder = builder;
				this.m_blockAlias = blockAlias;
			}

			// Token: 0x06006701 RID: 26369 RVA: 0x00160D4A File Offset: 0x0015EF4A
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x06006702 RID: 26370 RVA: 0x00160D63 File Offset: 0x0015EF63
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x06006703 RID: 26371 RVA: 0x00160D7C File Offset: 0x0015EF7C
			protected override StringBuilder BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull)
			{
				return literal.AsEsql(this.m_builder, this.m_blockAlias, skipIsNotNull);
			}

			// Token: 0x06006704 RID: 26372 RVA: 0x00160D91 File Offset: 0x0015EF91
			protected override StringBuilder NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x06006705 RID: 26373 RVA: 0x00160DC8 File Offset: 0x0015EFC8
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.And);
			}

			// Token: 0x06006706 RID: 26374 RVA: 0x00160DD2 File Offset: 0x0015EFD2
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.Or);
			}

			// Token: 0x06006707 RID: 26375 RVA: 0x00160DDC File Offset: 0x0015EFDC
			private StringBuilder VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, ExprType kind)
			{
				this.m_builder.Append('(');
				bool flag = true;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (!flag)
					{
						if (kind == ExprType.And)
						{
							this.m_builder.Append(" AND ");
						}
						else
						{
							this.m_builder.Append(" OR ");
						}
					}
					flag = false;
					boolExpr.Accept<StringBuilder>(this);
				}
				this.m_builder.Append(')');
				return this.m_builder;
			}

			// Token: 0x04002E57 RID: 11863
			private readonly StringBuilder m_builder;

			// Token: 0x04002E58 RID: 11864
			private readonly string m_blockAlias;
		}

		// Token: 0x02000BA5 RID: 2981
		private sealed class AsCqtVisitor : BoolExpression.AsCqlVisitor<DbExpression>
		{
			// Token: 0x06006708 RID: 26376 RVA: 0x00160E80 File Offset: 0x0015F080
			internal static DbExpression AsCqt(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, DbExpression row)
			{
				BoolExpression.AsCqtVisitor asCqtVisitor = new BoolExpression.AsCqtVisitor(row);
				return expression.Accept<DbExpression>(asCqtVisitor);
			}

			// Token: 0x06006709 RID: 26377 RVA: 0x00160E9B File Offset: 0x0015F09B
			private AsCqtVisitor(DbExpression row)
			{
				this.m_row = row;
			}

			// Token: 0x0600670A RID: 26378 RVA: 0x00160EAA File Offset: 0x0015F0AA
			internal override DbExpression VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return DbExpressionBuilder.True;
			}

			// Token: 0x0600670B RID: 26379 RVA: 0x00160EB1 File Offset: 0x0015F0B1
			internal override DbExpression VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return DbExpressionBuilder.False;
			}

			// Token: 0x0600670C RID: 26380 RVA: 0x00160EB8 File Offset: 0x0015F0B8
			protected override DbExpression BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull)
			{
				return literal.AsCqt(this.m_row, skipIsNotNull);
			}

			// Token: 0x0600670D RID: 26381 RVA: 0x00160EC7 File Offset: 0x0015F0C7
			protected override DbExpression NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return expression.Child.Accept<DbExpression>(this).Not();
			}

			// Token: 0x0600670E RID: 26382 RVA: 0x00160EDA File Offset: 0x0015F0DA
			internal override DbExpression VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.And));
			}

			// Token: 0x0600670F RID: 26383 RVA: 0x00160EEF File Offset: 0x0015F0EF
			internal override DbExpression VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
			}

			// Token: 0x06006710 RID: 26384 RVA: 0x00160F04 File Offset: 0x0015F104
			private DbExpression VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, Func<DbExpression, DbExpression, DbExpression> op)
			{
				DbExpression dbExpression = null;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (dbExpression == null)
					{
						dbExpression = boolExpr.Accept<DbExpression>(this);
					}
					else
					{
						dbExpression = op(dbExpression, boolExpr.Accept<DbExpression>(this));
					}
				}
				return dbExpression;
			}

			// Token: 0x04002E59 RID: 11865
			private readonly DbExpression m_row;
		}

		// Token: 0x02000BA6 RID: 2982
		private abstract class AsCqlVisitor<T_Return> : Visitor<DomainConstraint<BoolLiteral, Constant>, T_Return>
		{
			// Token: 0x06006711 RID: 26385 RVA: 0x00160F70 File Offset: 0x0015F170
			protected AsCqlVisitor()
			{
				this.m_skipIsNotNull = true;
			}

			// Token: 0x06006712 RID: 26386 RVA: 0x00160F80 File Offset: 0x0015F180
			internal override T_Return VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				return this.BooleanLiteralAsCql(boolLiteral, this.m_skipIsNotNull);
			}

			// Token: 0x06006713 RID: 26387
			protected abstract T_Return BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull);

			// Token: 0x06006714 RID: 26388 RVA: 0x00160FA1 File Offset: 0x0015F1A1
			internal override T_Return VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_skipIsNotNull = false;
				return this.NotExprAsCql(expression);
			}

			// Token: 0x06006715 RID: 26389
			protected abstract T_Return NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression);

			// Token: 0x04002E5A RID: 11866
			private bool m_skipIsNotNull;
		}

		// Token: 0x02000BA7 RID: 2983
		private class AsUserStringVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, StringBuilder>
		{
			// Token: 0x06006716 RID: 26390 RVA: 0x00160FB1 File Offset: 0x0015F1B1
			private AsUserStringVisitor(StringBuilder builder, string blockAlias)
			{
				this.m_builder = builder;
				this.m_blockAlias = blockAlias;
				this.m_skipIsNotNull = true;
			}

			// Token: 0x06006717 RID: 26391 RVA: 0x00160FD0 File Offset: 0x0015F1D0
			internal static StringBuilder AsUserString(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder, string blockAlias)
			{
				BoolExpression.AsUserStringVisitor asUserStringVisitor = new BoolExpression.AsUserStringVisitor(builder, blockAlias);
				return expression.Accept<StringBuilder>(asUserStringVisitor);
			}

			// Token: 0x06006718 RID: 26392 RVA: 0x00160FEC File Offset: 0x0015F1EC
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x06006719 RID: 26393 RVA: 0x00161005 File Offset: 0x0015F205
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x0600671A RID: 26394 RVA: 0x00161020 File Offset: 0x0015F220
			internal override StringBuilder VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				if (boolLiteral is ScalarRestriction || boolLiteral is TypeRestriction)
				{
					return boolLiteral.AsUserString(this.m_builder, Strings.ViewGen_EntityInstanceToken, this.m_skipIsNotNull);
				}
				return boolLiteral.AsUserString(this.m_builder, this.m_blockAlias, this.m_skipIsNotNull);
			}

			// Token: 0x0600671B RID: 26395 RVA: 0x00161074 File Offset: 0x0015F274
			internal override StringBuilder VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_skipIsNotNull = false;
				TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr = expression.Child as TermExpr<DomainConstraint<BoolLiteral, Constant>>;
				if (termExpr != null)
				{
					return BoolExpression.GetBoolLiteral(termExpr).AsNegatedUserString(this.m_builder, this.m_blockAlias, this.m_skipIsNotNull);
				}
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x0600671C RID: 26396 RVA: 0x001610EA File Offset: 0x0015F2EA
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.And);
			}

			// Token: 0x0600671D RID: 26397 RVA: 0x001610F4 File Offset: 0x0015F2F4
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.Or);
			}

			// Token: 0x0600671E RID: 26398 RVA: 0x00161100 File Offset: 0x0015F300
			private StringBuilder VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, ExprType kind)
			{
				this.m_builder.Append('(');
				bool flag = true;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (!flag)
					{
						if (kind == ExprType.And)
						{
							this.m_builder.Append(" AND ");
						}
						else
						{
							this.m_builder.Append(" OR ");
						}
					}
					flag = false;
					boolExpr.Accept<StringBuilder>(this);
				}
				this.m_builder.Append(')');
				return this.m_builder;
			}

			// Token: 0x04002E5B RID: 11867
			private readonly StringBuilder m_builder;

			// Token: 0x04002E5C RID: 11868
			private readonly string m_blockAlias;

			// Token: 0x04002E5D RID: 11869
			private bool m_skipIsNotNull;
		}

		// Token: 0x02000BA8 RID: 2984
		private class TermVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>
		{
			// Token: 0x0600671F RID: 26399 RVA: 0x001611A4 File Offset: 0x0015F3A4
			private TermVisitor(bool allowAllOperators)
			{
			}

			// Token: 0x06006720 RID: 26400 RVA: 0x001611AC File Offset: 0x0015F3AC
			internal static IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> GetTerms(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, bool allowAllOperators)
			{
				BoolExpression.TermVisitor termVisitor = new BoolExpression.TermVisitor(allowAllOperators);
				return expression.Accept<IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>(termVisitor);
			}

			// Token: 0x06006721 RID: 26401 RVA: 0x001611C7 File Offset: 0x0015F3C7
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield break;
			}

			// Token: 0x06006722 RID: 26402 RVA: 0x001611D0 File Offset: 0x0015F3D0
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield break;
			}

			// Token: 0x06006723 RID: 26403 RVA: 0x001611D9 File Offset: 0x0015F3D9
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield return expression;
				yield break;
			}

			// Token: 0x06006724 RID: 26404 RVA: 0x001611E9 File Offset: 0x0015F3E9
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x06006725 RID: 26405 RVA: 0x001611F2 File Offset: 0x0015F3F2
			private IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTreeNode(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr in boolExpr.Accept<IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>(this))
					{
						yield return termExpr;
					}
					IEnumerator<TermExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator2 = null;
				}
				HashSet<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>.Enumerator enumerator = default(HashSet<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06006726 RID: 26406 RVA: 0x00161209 File Offset: 0x0015F409
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x06006727 RID: 26407 RVA: 0x00161212 File Offset: 0x0015F412
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}
		}

		// Token: 0x02000BA9 RID: 2985
		private class CompactStringVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, StringBuilder>
		{
			// Token: 0x06006728 RID: 26408 RVA: 0x0016121B File Offset: 0x0015F41B
			private CompactStringVisitor(StringBuilder builder)
			{
				this.m_builder = builder;
			}

			// Token: 0x06006729 RID: 26409 RVA: 0x0016122C File Offset: 0x0015F42C
			internal static StringBuilder ToBuilder(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder)
			{
				BoolExpression.CompactStringVisitor compactStringVisitor = new BoolExpression.CompactStringVisitor(builder);
				return expression.Accept<StringBuilder>(compactStringVisitor);
			}

			// Token: 0x0600672A RID: 26410 RVA: 0x00161247 File Offset: 0x0015F447
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x0600672B RID: 26411 RVA: 0x00161260 File Offset: 0x0015F460
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x0600672C RID: 26412 RVA: 0x00161279 File Offset: 0x0015F479
			internal override StringBuilder VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolExpression.GetBoolLiteral(expression).ToCompactString(this.m_builder);
				return this.m_builder;
			}

			// Token: 0x0600672D RID: 26413 RVA: 0x00161292 File Offset: 0x0015F492
			internal override StringBuilder VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x0600672E RID: 26414 RVA: 0x001612C9 File Offset: 0x0015F4C9
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, "AND");
			}

			// Token: 0x0600672F RID: 26415 RVA: 0x001612D7 File Offset: 0x0015F4D7
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, "OR");
			}

			// Token: 0x06006730 RID: 26416 RVA: 0x001612E8 File Offset: 0x0015F4E8
			private StringBuilder VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, string opAsString)
			{
				List<string> list = new List<string>();
				StringBuilder builder = this.m_builder;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					this.m_builder = new StringBuilder();
					boolExpr.Accept<StringBuilder>(this);
					list.Add(this.m_builder.ToString());
				}
				this.m_builder = builder;
				this.m_builder.Append('(');
				StringUtil.ToSeparatedStringSorted(this.m_builder, list, " " + opAsString + " ");
				this.m_builder.Append(')');
				return this.m_builder;
			}

			// Token: 0x04002E5E RID: 11870
			private StringBuilder m_builder;
		}
	}
}
