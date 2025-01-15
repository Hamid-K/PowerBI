using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000586 RID: 1414
	internal class FragmentQueryKBChaseSupport : FragmentQueryKB
	{
		// Token: 0x06004450 RID: 17488 RVA: 0x000F06BC File Offset: 0x000EE8BC
		internal FragmentQueryKBChaseSupport()
		{
			this._chase = new FragmentQueryKBChaseSupport.AtomicConditionRuleChase(this);
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x000F06E4 File Offset: 0x000EE8E4
		internal Dictionary<TermExpr<DomainConstraint<BoolLiteral, Constant>>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>> Implications
		{
			get
			{
				if (this._implications == null)
				{
					this._implications = new Dictionary<TermExpr<DomainConstraint<BoolLiteral, Constant>>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in base.Facts)
					{
						this.CacheFact(boolExpr);
					}
				}
				return this._implications;
			}
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x000F074C File Offset: 0x000EE94C
		internal override void AddFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			base.AddFact(fact);
			this._kbSize += fact.CountTerms();
			if (this._implications != null)
			{
				this.CacheFact(fact);
			}
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x000F0778 File Offset: 0x000EE978
		private void CacheFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Implication implication = fact as KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Implication;
			KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Equivalence equivalence = fact as KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Equivalence;
			if (implication != null)
			{
				this.CacheImplication(implication.Condition, implication.Implies);
				return;
			}
			if (equivalence != null)
			{
				this.CacheImplication(equivalence.Left, equivalence.Right);
				this.CacheImplication(equivalence.Right, equivalence.Left);
				return;
			}
			this.CacheResidualFact(fact);
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x000F07D8 File Offset: 0x000EE9D8
		private IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> ResidueInternal
		{
			get
			{
				if (this._residueSize < 0 && this._residualFacts.Count > 0)
				{
					this.PrepareResidue();
				}
				return this._residualFacts;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x000F07FD File Offset: 0x000EE9FD
		private int ResidueSize
		{
			get
			{
				if (this._residueSize < 0)
				{
					this.PrepareResidue();
				}
				return this._residueSize;
			}
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x000F0814 File Offset: 0x000EEA14
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Chase(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr;
			this.Implications.TryGetValue(expression, out boolExpr);
			return new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
			{
				expression,
				boolExpr ?? TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value
			});
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000F084C File Offset: 0x000EEA4C
		internal bool IsSatisfiable(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			ConversionContext<DomainConstraint<BoolLiteral, Constant>> conversionContext = IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext();
			Converter<DomainConstraint<BoolLiteral, Constant>> converter = new Converter<DomainConstraint<BoolLiteral, Constant>>(expression, conversionContext);
			if (converter.Vertex.IsZero())
			{
				return false;
			}
			if (base.KbExpression.ExprType == ExprType.True)
			{
				return true;
			}
			int num = expression.CountTerms() + this._kbSize;
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = converter.Dnf.Expr;
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = ((FragmentQueryKBChaseSupport.Normalizer.EstimateNnfAndSplitTermCount(expr) > FragmentQueryKBChaseSupport.Normalizer.EstimateNnfAndSplitTermCount(expression)) ? expression : expr);
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr2 = this._chase.Chase(FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(boolExpr));
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr3;
			if (boolExpr2.CountTerms() + this.ResidueSize > num)
			{
				boolExpr3 = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[] { base.KbExpression, expression });
			}
			else
			{
				boolExpr3 = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this.ResidueInternal) { boolExpr2 });
				conversionContext = IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext();
			}
			return !new Converter<DomainConstraint<BoolLiteral, Constant>>(boolExpr3, conversionContext).Vertex.IsZero();
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x000F0934 File Offset: 0x000EEB34
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Chase(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			if (this.Implications.Count != 0)
			{
				return this._chase.Chase(FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(expression));
			}
			return expression;
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x000F0958 File Offset: 0x000EEB58
		private void CacheImplication(BoolExpr<DomainConstraint<BoolLiteral, Constant>> condition, BoolExpr<DomainConstraint<BoolLiteral, Constant>> implies)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = FragmentQueryKBChaseSupport.Normalizer.ToDnf(condition, false);
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr2 = FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(implies);
			ExprType exprType = boolExpr.ExprType;
			if (exprType != ExprType.Or)
			{
				if (exprType != ExprType.Term)
				{
					this.CacheResidualFact(new OrExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						new NotExpr<DomainConstraint<BoolLiteral, Constant>>(condition),
						implies
					}));
					return;
				}
			}
			else
			{
				using (HashSet<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>.Enumerator enumerator = ((OrExpr<DomainConstraint<BoolLiteral, Constant>>)boolExpr).Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr3 = enumerator.Current;
						if (boolExpr3.ExprType != ExprType.Term)
						{
							this.CacheResidualFact(new OrExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
							{
								new NotExpr<DomainConstraint<BoolLiteral, Constant>>(boolExpr3),
								implies
							}));
						}
						else
						{
							this.CacheNormalizedImplication((TermExpr<DomainConstraint<BoolLiteral, Constant>>)boolExpr3, boolExpr2);
						}
					}
					return;
				}
			}
			this.CacheNormalizedImplication((TermExpr<DomainConstraint<BoolLiteral, Constant>>)boolExpr, boolExpr2);
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x000F0A30 File Offset: 0x000EEC30
		private void CacheNormalizedImplication(TermExpr<DomainConstraint<BoolLiteral, Constant>> condition, BoolExpr<DomainConstraint<BoolLiteral, Constant>> implies)
		{
			foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr in this.Implications.Keys)
			{
				if (termExpr.Identifier.Variable.Equals(condition.Identifier.Variable) && !termExpr.Identifier.Range.SetEquals(condition.Identifier.Range))
				{
					this.CacheResidualFact(new OrExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						new NotExpr<DomainConstraint<BoolLiteral, Constant>>(condition),
						implies
					}));
					return;
				}
			}
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.Chase(implies), IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext()).Dnf.Expr;
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport = new FragmentQueryKBChaseSupport();
			fragmentQueryKBChaseSupport.Implications[condition] = expr;
			bool flag = true;
			foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr2 in new Set<TermExpr<DomainConstraint<BoolLiteral, Constant>>>(this.Implications.Keys))
			{
				BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = fragmentQueryKBChaseSupport.Chase(this.Implications[termExpr2]);
				if (termExpr2.Equals(condition))
				{
					flag = false;
					boolExpr = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[] { boolExpr, expr });
				}
				this.Implications[termExpr2] = new Converter<DomainConstraint<BoolLiteral, Constant>>(boolExpr, IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext()).Dnf.Expr;
			}
			if (flag)
			{
				this.Implications[condition] = expr;
			}
			this._residueSize = -1;
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x000F0BD4 File Offset: 0x000EEDD4
		private void CacheResidualFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			this._residualFacts.Add(fact);
			this._residueSize = -1;
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x000F0BEC File Offset: 0x000EEDEC
		private void PrepareResidue()
		{
			int num = 0;
			if (this.Implications.Count > 0 && this._residualFacts.Count > 0)
			{
				Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> set = new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in this._residualFacts)
				{
					BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.Chase(boolExpr), IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext()).Dnf.Expr;
					set.Add(expr);
					num += expr.CountTerms();
					this._residueSize = num;
				}
				this._residualFacts = set;
			}
			this._residueSize = num;
		}

		// Token: 0x0400189A RID: 6298
		private Dictionary<TermExpr<DomainConstraint<BoolLiteral, Constant>>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>> _implications;

		// Token: 0x0400189B RID: 6299
		private readonly FragmentQueryKBChaseSupport.AtomicConditionRuleChase _chase;

		// Token: 0x0400189C RID: 6300
		private Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> _residualFacts = new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();

		// Token: 0x0400189D RID: 6301
		private int _kbSize;

		// Token: 0x0400189E RID: 6302
		private int _residueSize = -1;

		// Token: 0x02000B8F RID: 2959
		private static class Normalizer
		{
			// Token: 0x06006696 RID: 26262 RVA: 0x0015FD8F File Offset: 0x0015DF8F
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> ToNnfAndSplitRange(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr)
			{
				return expr.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor.Instance);
			}

			// Token: 0x06006697 RID: 26263 RVA: 0x0015FD9C File Offset: 0x0015DF9C
			internal static int EstimateNnfAndSplitTermCount(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr)
			{
				return expr.Accept<int>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter.Instance);
			}

			// Token: 0x06006698 RID: 26264 RVA: 0x0015FDA9 File Offset: 0x0015DFA9
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> ToDnf(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr, bool isNnf)
			{
				if (!isNnf)
				{
					expr = FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(expr);
				}
				return expr.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.DnfTreeVisitor.Instance);
			}

			// Token: 0x02000D78 RID: 3448
			private class NonNegatedTreeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06006F01 RID: 28417 RVA: 0x0017C9EB File Offset: 0x0017ABEB
				private NonNegatedTreeVisitor()
				{
				}

				// Token: 0x06006F02 RID: 28418 RVA: 0x0017C9F3 File Offset: 0x0017ABF3
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expr)
				{
					return expr.Child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.NegatedTreeVisitor.Instance);
				}

				// Token: 0x06006F03 RID: 28419 RVA: 0x0017CA08 File Offset: 0x0017AC08
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					int count = expression.Identifier.Range.Count;
					if (count == 0)
					{
						return FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
					}
					if (count != 1)
					{
						List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> list = new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
						DomainVariable<BoolLiteral, Constant> variable = expression.Identifier.Variable;
						foreach (Constant constant in expression.Identifier.Range)
						{
							list.Add(new DomainConstraint<BoolLiteral, Constant>(variable, new Set<Constant>(new Constant[] { constant }, Constant.EqualityComparer)));
						}
						return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(list);
					}
					return expression;
				}

				// Token: 0x04003327 RID: 13095
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor Instance = new FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor();
			}

			// Token: 0x02000D79 RID: 3449
			private class NegatedTreeVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>>
			{
				// Token: 0x06006F05 RID: 28421 RVA: 0x0017CAC8 File Offset: 0x0017ACC8
				private NegatedTreeVisitor()
				{
				}

				// Token: 0x06006F06 RID: 28422 RVA: 0x0017CAD0 File Offset: 0x0017ACD0
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				}

				// Token: 0x06006F07 RID: 28423 RVA: 0x0017CAD7 File Offset: 0x0017ACD7
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				}

				// Token: 0x06006F08 RID: 28424 RVA: 0x0017CADE File Offset: 0x0017ACDE
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor.Instance);
				}

				// Token: 0x06006F09 RID: 28425 RVA: 0x0017CAF0 File Offset: 0x0017ACF0
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(expression.Children.Select((BoolExpr<DomainConstraint<BoolLiteral, Constant>> child) => child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this)));
				}

				// Token: 0x06006F0A RID: 28426 RVA: 0x0017CB0E File Offset: 0x0017AD0E
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return new AndExpr<DomainConstraint<BoolLiteral, Constant>>(expression.Children.Select((BoolExpr<DomainConstraint<BoolLiteral, Constant>> child) => child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this)));
				}

				// Token: 0x06006F0B RID: 28427 RVA: 0x0017CB2C File Offset: 0x0017AD2C
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					DomainConstraint<BoolLiteral, Constant> domainConstraint = expression.Identifier.InvertDomainConstraint();
					if (domainConstraint.Range.Count == 0)
					{
						return FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
					}
					List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> list = new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					DomainVariable<BoolLiteral, Constant> variable = domainConstraint.Variable;
					foreach (Constant constant in domainConstraint.Range)
					{
						list.Add(new DomainConstraint<BoolLiteral, Constant>(variable, new Set<Constant>(new Constant[] { constant }, Constant.EqualityComparer)));
					}
					return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(list);
				}

				// Token: 0x04003328 RID: 13096
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NegatedTreeVisitor Instance = new FragmentQueryKBChaseSupport.Normalizer.NegatedTreeVisitor();
			}

			// Token: 0x02000D7A RID: 3450
			private class NonNegatedNnfSplitCounter : TermCounter<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06006F0F RID: 28431 RVA: 0x0017CBF2 File Offset: 0x0017ADF2
				private NonNegatedNnfSplitCounter()
				{
				}

				// Token: 0x06006F10 RID: 28432 RVA: 0x0017CBFA File Offset: 0x0017ADFA
				internal override int VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expr)
				{
					return expr.Child.Accept<int>(FragmentQueryKBChaseSupport.Normalizer.NegatedNnfSplitCountEstimator.Instance);
				}

				// Token: 0x06006F11 RID: 28433 RVA: 0x0017CC0C File Offset: 0x0017AE0C
				internal override int VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Identifier.Range.Count;
				}

				// Token: 0x04003329 RID: 13097
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter Instance = new FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter();
			}

			// Token: 0x02000D7B RID: 3451
			private class NegatedNnfSplitCountEstimator : TermCounter<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06006F13 RID: 28435 RVA: 0x0017CC2A File Offset: 0x0017AE2A
				private NegatedNnfSplitCountEstimator()
				{
				}

				// Token: 0x06006F14 RID: 28436 RVA: 0x0017CC32 File Offset: 0x0017AE32
				internal override int VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Child.Accept<int>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter.Instance);
				}

				// Token: 0x06006F15 RID: 28437 RVA: 0x0017CC44 File Offset: 0x0017AE44
				internal override int VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Identifier.Variable.Domain.Count - expression.Identifier.Range.Count;
				}

				// Token: 0x0400332A RID: 13098
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NegatedNnfSplitCountEstimator Instance = new FragmentQueryKBChaseSupport.Normalizer.NegatedNnfSplitCountEstimator();
			}

			// Token: 0x02000D7C RID: 3452
			private class DnfTreeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06006F17 RID: 28439 RVA: 0x0017CC78 File Offset: 0x0017AE78
				private DnfTreeVisitor()
				{
				}

				// Token: 0x06006F18 RID: 28440 RVA: 0x0017CC80 File Offset: 0x0017AE80
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression;
				}

				// Token: 0x06006F19 RID: 28441 RVA: 0x0017CC84 File Offset: 0x0017AE84
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = base.VisitAnd(expression);
					TreeExpr<DomainConstraint<BoolLiteral, Constant>> treeExpr = boolExpr as TreeExpr<DomainConstraint<BoolLiteral, Constant>>;
					if (treeExpr == null)
					{
						return boolExpr;
					}
					Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> set = new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					Set<Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> set2 = new Set<Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>>();
					foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr2 in treeExpr.Children)
					{
						OrExpr<DomainConstraint<BoolLiteral, Constant>> orExpr = boolExpr2 as OrExpr<DomainConstraint<BoolLiteral, Constant>>;
						if (orExpr != null)
						{
							set2.Add(new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(orExpr.Children));
						}
						else
						{
							set.Add(boolExpr2);
						}
					}
					set2.Add(new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						new AndExpr<DomainConstraint<BoolLiteral, Constant>>(set)
					}));
					IEnumerable<IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> enumerable = new IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>[] { Enumerable.Empty<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>() };
					IEnumerable<IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> enumerable2 = set2.Aggregate(enumerable, (IEnumerable<IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> accumulator, Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> bucket) => from accseq in accumulator
						from item in bucket
						select accseq.Concat(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[] { item }));
					List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> list = new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					foreach (IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> enumerable3 in enumerable2)
					{
						list.Add(new AndExpr<DomainConstraint<BoolLiteral, Constant>>(enumerable3));
					}
					return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(list);
				}

				// Token: 0x0400332B RID: 13099
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.DnfTreeVisitor Instance = new FragmentQueryKBChaseSupport.Normalizer.DnfTreeVisitor();
			}
		}

		// Token: 0x02000B90 RID: 2960
		private class AtomicConditionRuleChase
		{
			// Token: 0x06006699 RID: 26265 RVA: 0x0015FDC1 File Offset: 0x0015DFC1
			internal AtomicConditionRuleChase(FragmentQueryKBChaseSupport kb)
			{
				this._visitor = new FragmentQueryKBChaseSupport.AtomicConditionRuleChase.NonNegatedDomainConstraintTreeVisitor(kb);
			}

			// Token: 0x0600669A RID: 26266 RVA: 0x0015FDD5 File Offset: 0x0015DFD5
			internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Chase(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this._visitor);
			}

			// Token: 0x04002E15 RID: 11797
			private readonly FragmentQueryKBChaseSupport.AtomicConditionRuleChase.NonNegatedDomainConstraintTreeVisitor _visitor;

			// Token: 0x02000D7D RID: 3453
			private class NonNegatedDomainConstraintTreeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06006F1B RID: 28443 RVA: 0x0017CDCC File Offset: 0x0017AFCC
				internal NonNegatedDomainConstraintTreeVisitor(FragmentQueryKBChaseSupport kb)
				{
					this._kb = kb;
				}

				// Token: 0x06006F1C RID: 28444 RVA: 0x0017CDDB File Offset: 0x0017AFDB
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return this._kb.Chase(expression);
				}

				// Token: 0x06006F1D RID: 28445 RVA: 0x0017CDE9 File Offset: 0x0017AFE9
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return base.VisitNot(expression);
				}

				// Token: 0x0400332C RID: 13100
				private readonly FragmentQueryKBChaseSupport _kb;
			}
		}
	}
}
