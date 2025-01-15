using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000587 RID: 1415
	internal class FragmentQueryProcessor : TileQueryProcessor<FragmentQuery>
	{
		// Token: 0x0600445D RID: 17501 RVA: 0x000F0CA8 File Offset: 0x000EEEA8
		public FragmentQueryProcessor(FragmentQueryKBChaseSupport kb)
		{
			this._kb = kb;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x000F0CB7 File Offset: 0x000EEEB7
		internal static FragmentQueryProcessor Merge(FragmentQueryProcessor qp1, FragmentQueryProcessor qp2)
		{
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport = new FragmentQueryKBChaseSupport();
			fragmentQueryKBChaseSupport.AddKnowledgeBase(qp1.KnowledgeBase);
			fragmentQueryKBChaseSupport.AddKnowledgeBase(qp2.KnowledgeBase);
			return new FragmentQueryProcessor(fragmentQueryKBChaseSupport);
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x000F0CDB File Offset: 0x000EEEDB
		internal FragmentQueryKB KnowledgeBase
		{
			get
			{
				return this._kb;
			}
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000F0CE4 File Offset: 0x000EEEE4
		internal override FragmentQuery Union(FragmentQuery q1, FragmentQuery q2)
		{
			HashSet<MemberPath> hashSet = new HashSet<MemberPath>(q1.Attributes);
			hashSet.IntersectWith(q2.Attributes);
			BoolExpression boolExpression = BoolExpression.CreateOr(new BoolExpression[] { q1.Condition, q2.Condition });
			return FragmentQuery.Create(hashSet, boolExpression);
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x000F0D2E File Offset: 0x000EEF2E
		internal bool IsDisjointFrom(FragmentQuery q1, FragmentQuery q2)
		{
			return !this.IsSatisfiable(this.Intersect(q1, q2));
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x000F0D41 File Offset: 0x000EEF41
		internal bool IsContainedIn(FragmentQuery q1, FragmentQuery q2)
		{
			return !this.IsSatisfiable(this.Difference(q1, q2));
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x000F0D54 File Offset: 0x000EEF54
		internal bool IsEquivalentTo(FragmentQuery q1, FragmentQuery q2)
		{
			return this.IsContainedIn(q1, q2) && this.IsContainedIn(q2, q1);
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x000F0D6C File Offset: 0x000EEF6C
		internal override FragmentQuery Intersect(FragmentQuery q1, FragmentQuery q2)
		{
			HashSet<MemberPath> hashSet = new HashSet<MemberPath>(q1.Attributes);
			hashSet.IntersectWith(q2.Attributes);
			BoolExpression boolExpression = BoolExpression.CreateAnd(new BoolExpression[] { q1.Condition, q2.Condition });
			return FragmentQuery.Create(hashSet, boolExpression);
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000F0DB6 File Offset: 0x000EEFB6
		internal override FragmentQuery Difference(FragmentQuery qA, FragmentQuery qB)
		{
			return FragmentQuery.Create(qA.Attributes, BoolExpression.CreateAndNot(qA.Condition, qB.Condition));
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x000F0DD4 File Offset: 0x000EEFD4
		internal override bool IsSatisfiable(FragmentQuery query)
		{
			return this.IsSatisfiable(query.Condition);
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x000F0DE2 File Offset: 0x000EEFE2
		private bool IsSatisfiable(BoolExpression condition)
		{
			return this._kb.IsSatisfiable(condition.Tree);
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x000F0DF8 File Offset: 0x000EEFF8
		internal override FragmentQuery CreateDerivedViewBySelectingConstantAttributes(FragmentQuery view)
		{
			HashSet<MemberPath> hashSet = new HashSet<MemberPath>();
			foreach (DomainVariable<BoolLiteral, Constant> domainVariable in view.Condition.Variables)
			{
				MemberRestriction memberRestriction = domainVariable.Identifier as MemberRestriction;
				if (memberRestriction != null)
				{
					MemberPath memberPath = memberRestriction.RestrictedMemberSlot.MemberPath;
					Domain domain = memberRestriction.Domain;
					if (!view.Attributes.Contains(memberPath))
					{
						if (!domain.AllPossibleValues.Any((Constant it) => it.HasNotNull()))
						{
							foreach (Constant constant in domain.Values)
							{
								DomainConstraint<BoolLiteral, Constant> domainConstraint = new DomainConstraint<BoolLiteral, Constant>(domainVariable, new Set<Constant>(new Constant[] { constant }, Constant.EqualityComparer));
								BoolExpression boolExpression = view.Condition.Create(new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
								{
									view.Condition.Tree,
									new NotExpr<DomainConstraint<BoolLiteral, Constant>>(new TermExpr<DomainConstraint<BoolLiteral, Constant>>(domainConstraint))
								}));
								if (!this.IsSatisfiable(boolExpression))
								{
									hashSet.Add(memberPath);
								}
							}
						}
					}
				}
			}
			if (hashSet.Count > 0)
			{
				hashSet.UnionWith(view.Attributes);
				return new FragmentQuery(string.Format(CultureInfo.InvariantCulture, "project({0})", new object[] { view.Description }), view.FromVariable, hashSet, view.Condition);
			}
			return null;
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x000F0FBC File Offset: 0x000EF1BC
		public override string ToString()
		{
			return this._kb.ToString();
		}

		// Token: 0x0400189F RID: 6303
		private readonly FragmentQueryKBChaseSupport _kb;

		// Token: 0x02000B91 RID: 2961
		private class AttributeSetComparator : IEqualityComparer<HashSet<MemberPath>>
		{
			// Token: 0x0600669B RID: 26267 RVA: 0x0015FDE3 File Offset: 0x0015DFE3
			public bool Equals(HashSet<MemberPath> x, HashSet<MemberPath> y)
			{
				return x.SetEquals(y);
			}

			// Token: 0x0600669C RID: 26268 RVA: 0x0015FDEC File Offset: 0x0015DFEC
			public int GetHashCode(HashSet<MemberPath> attrs)
			{
				int num = 123;
				foreach (MemberPath memberPath in attrs)
				{
					num += MemberPath.EqualityComparer.GetHashCode(memberPath) * 7;
				}
				return num;
			}
		}
	}
}
