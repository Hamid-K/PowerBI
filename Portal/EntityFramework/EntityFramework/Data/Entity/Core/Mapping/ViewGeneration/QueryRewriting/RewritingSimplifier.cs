using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200058C RID: 1420
	internal class RewritingSimplifier<T_Tile> where T_Tile : class
	{
		// Token: 0x060044C1 RID: 17601 RVA: 0x000F35E8 File Offset: 0x000F17E8
		private RewritingSimplifier(T_Tile originalRewriting, T_Tile toAvoid, Dictionary<T_Tile, TileOpKind> usedViews, RewritingProcessor<T_Tile> qp)
		{
			this.m_originalRewriting = originalRewriting;
			this.m_toAvoid = toAvoid;
			this.m_qp = qp;
			this.m_usedViews = usedViews;
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x000F3618 File Offset: 0x000F1818
		private RewritingSimplifier(T_Tile rewriting, T_Tile toFill, T_Tile toAvoid, RewritingProcessor<T_Tile> qp)
		{
			this.m_originalRewriting = toFill;
			this.m_toAvoid = toAvoid;
			this.m_qp = qp;
			this.m_usedViews = new Dictionary<T_Tile, TileOpKind>();
			this.GatherUnionedSubqueriesInUsedViews(rewriting);
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000F3654 File Offset: 0x000F1854
		internal static bool TrySimplifyUnionRewriting(ref T_Tile rewriting, T_Tile toFill, T_Tile toAvoid, RewritingProcessor<T_Tile> qp)
		{
			T_Tile t_Tile;
			if (new RewritingSimplifier<T_Tile>(rewriting, toFill, toAvoid, qp).SimplifyRewriting(out t_Tile))
			{
				rewriting = t_Tile;
				return true;
			}
			return false;
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000F3684 File Offset: 0x000F1884
		internal static bool TrySimplifyJoinRewriting(ref T_Tile rewriting, T_Tile toAvoid, Dictionary<T_Tile, TileOpKind> usedViews, RewritingProcessor<T_Tile> qp)
		{
			T_Tile t_Tile;
			if (new RewritingSimplifier<T_Tile>(rewriting, toAvoid, usedViews, qp).SimplifyRewriting(out t_Tile))
			{
				rewriting = t_Tile;
				return true;
			}
			return false;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000F36B4 File Offset: 0x000F18B4
		private void GatherUnionedSubqueriesInUsedViews(T_Tile query)
		{
			if (query != null)
			{
				if (this.m_qp.GetOpKind(query) != TileOpKind.Union)
				{
					this.m_usedViews[query] = TileOpKind.Union;
					return;
				}
				this.GatherUnionedSubqueriesInUsedViews(this.m_qp.GetArg1(query));
				this.GatherUnionedSubqueriesInUsedViews(this.m_qp.GetArg2(query));
			}
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x000F370C File Offset: 0x000F190C
		private bool SimplifyRewriting(out T_Tile simplifiedRewriting)
		{
			bool flag = false;
			simplifiedRewriting = default(T_Tile);
			T_Tile t_Tile;
			while (this.SimplifyRewritingOnce(out t_Tile))
			{
				flag = true;
				simplifiedRewriting = t_Tile;
			}
			return flag;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000F3738 File Offset: 0x000F1938
		private bool SimplifyRewritingOnce(out T_Tile simplifiedRewriting)
		{
			HashSet<T_Tile> hashSet = new HashSet<T_Tile>(this.m_usedViews.Keys);
			foreach (T_Tile t_Tile in this.m_usedViews.Keys)
			{
				TileOpKind tileOpKind = this.m_usedViews[t_Tile];
				if (tileOpKind <= TileOpKind.Join)
				{
					hashSet.Remove(t_Tile);
					if (this.SimplifyRewritingOnce(t_Tile, hashSet, out simplifiedRewriting))
					{
						return true;
					}
					hashSet.Add(t_Tile);
				}
			}
			simplifiedRewriting = default(T_Tile);
			return false;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000F37D8 File Offset: 0x000F19D8
		private bool SimplifyRewritingOnce(T_Tile newRewriting, HashSet<T_Tile> remainingViews, out T_Tile simplifiedRewriting)
		{
			simplifiedRewriting = default(T_Tile);
			if (remainingViews.Count == 0)
			{
				return false;
			}
			if (remainingViews.Count != 1)
			{
				int num = remainingViews.Count / 2;
				int num2 = 0;
				T_Tile t_Tile = newRewriting;
				T_Tile t_Tile2 = newRewriting;
				HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
				HashSet<T_Tile> hashSet2 = new HashSet<T_Tile>();
				foreach (T_Tile t_Tile3 in remainingViews)
				{
					TileOpKind tileOpKind = this.m_usedViews[t_Tile3];
					if (num2++ < num)
					{
						hashSet.Add(t_Tile3);
						t_Tile = this.GetRewritingHalf(t_Tile, t_Tile3, tileOpKind);
					}
					else
					{
						hashSet2.Add(t_Tile3);
						t_Tile2 = this.GetRewritingHalf(t_Tile2, t_Tile3, tileOpKind);
					}
				}
				return this.SimplifyRewritingOnce(t_Tile, hashSet2, out simplifiedRewriting) || this.SimplifyRewritingOnce(t_Tile2, hashSet, out simplifiedRewriting);
			}
			T_Tile t_Tile4 = remainingViews.First<T_Tile>();
			bool flag;
			if (this.m_usedViews[t_Tile4] == TileOpKind.Union)
			{
				flag = this.m_qp.IsContainedIn(this.m_originalRewriting, newRewriting);
			}
			else
			{
				flag = this.m_qp.IsContainedIn(this.m_originalRewriting, newRewriting) && this.m_qp.IsDisjointFrom(this.m_toAvoid, newRewriting);
			}
			if (flag)
			{
				simplifiedRewriting = newRewriting;
				this.m_usedViews.Remove(t_Tile4);
				return true;
			}
			return false;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x000F3930 File Offset: 0x000F1B30
		private T_Tile GetRewritingHalf(T_Tile halfRewriting, T_Tile remainingView, TileOpKind viewKind)
		{
			switch (viewKind)
			{
			case TileOpKind.Union:
				halfRewriting = this.m_qp.Union(halfRewriting, remainingView);
				break;
			case TileOpKind.Join:
				halfRewriting = this.m_qp.Join(halfRewriting, remainingView);
				break;
			case TileOpKind.AntiSemiJoin:
				halfRewriting = this.m_qp.AntiSemiJoin(halfRewriting, remainingView);
				break;
			}
			return halfRewriting;
		}

		// Token: 0x040018C2 RID: 6338
		private readonly T_Tile m_originalRewriting;

		// Token: 0x040018C3 RID: 6339
		private readonly T_Tile m_toAvoid;

		// Token: 0x040018C4 RID: 6340
		private readonly RewritingProcessor<T_Tile> m_qp;

		// Token: 0x040018C5 RID: 6341
		private readonly Dictionary<T_Tile, TileOpKind> m_usedViews = new Dictionary<T_Tile, TileOpKind>();
	}
}
