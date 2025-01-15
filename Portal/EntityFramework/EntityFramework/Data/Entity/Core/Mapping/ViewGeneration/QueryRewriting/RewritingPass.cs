using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200058A RID: 1418
	internal class RewritingPass<T_Tile> where T_Tile : class
	{
		// Token: 0x0600449E RID: 17566 RVA: 0x000F2D7A File Offset: 0x000F0F7A
		public RewritingPass(T_Tile toFill, T_Tile toAvoid, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			this.m_toFill = toFill;
			this.m_toAvoid = toAvoid;
			this.m_views = views;
			this.m_qp = qp;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000F2DAA File Offset: 0x000F0FAA
		public static bool RewriteQuery(T_Tile toFill, T_Tile toAvoid, out T_Tile rewriting, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			if (new RewritingPass<T_Tile>(toFill, toAvoid, views, qp).RewriteQuery(out rewriting))
			{
				RewritingSimplifier<T_Tile>.TrySimplifyUnionRewriting(ref rewriting, toFill, toAvoid, qp);
				return true;
			}
			return false;
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x000F2DCC File Offset: 0x000F0FCC
		private static bool RewriteQueryInternal(T_Tile toFill, T_Tile toAvoid, out T_Tile rewriting, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			return new RewritingPass<T_Tile>(toFill, toAvoid, views, qp).RewriteQuery(out rewriting);
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000F2DE0 File Offset: 0x000F0FE0
		private bool RewriteQuery(out T_Tile rewriting)
		{
			rewriting = this.m_toFill;
			T_Tile t_Tile;
			if (!this.FindRewritingByIncludedAndDisjoint(out t_Tile) && !this.FindContributingView(out t_Tile))
			{
				return false;
			}
			bool flag = !this.m_qp.IsDisjointFrom(t_Tile, this.m_toAvoid);
			if (flag)
			{
				foreach (T_Tile t_Tile2 in this.AvailableViews)
				{
					if (this.TryJoin(t_Tile2, ref t_Tile))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				foreach (T_Tile t_Tile3 in this.AvailableViews)
				{
					if (this.TryAntiSemiJoin(t_Tile3, ref t_Tile))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				return false;
			}
			RewritingSimplifier<T_Tile>.TrySimplifyJoinRewriting(ref t_Tile, this.m_toAvoid, this.m_usedViews, this.m_qp);
			T_Tile t_Tile4 = this.m_qp.AntiSemiJoin(this.m_toFill, t_Tile);
			if (!this.m_qp.IsEmpty(t_Tile4))
			{
				T_Tile t_Tile5;
				if (!RewritingPass<T_Tile>.RewriteQueryInternal(t_Tile4, this.m_toAvoid, out t_Tile5, this.m_views, this.m_qp))
				{
					rewriting = t_Tile5;
					return false;
				}
				if (this.m_qp.IsContainedIn(t_Tile, t_Tile5))
				{
					t_Tile = t_Tile5;
				}
				else
				{
					t_Tile = this.m_qp.Union(t_Tile, t_Tile5);
				}
			}
			rewriting = t_Tile;
			return true;
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x000F2F50 File Offset: 0x000F1150
		private bool TryJoin(T_Tile view, ref T_Tile rewriting)
		{
			T_Tile t_Tile = this.m_qp.Join(rewriting, view);
			if (!this.m_qp.IsEmpty(t_Tile))
			{
				this.m_usedViews[view] = TileOpKind.Join;
				rewriting = t_Tile;
				return this.m_qp.IsDisjointFrom(rewriting, this.m_toAvoid);
			}
			return false;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000F2FAC File Offset: 0x000F11AC
		private bool TryAntiSemiJoin(T_Tile view, ref T_Tile rewriting)
		{
			T_Tile t_Tile = this.m_qp.AntiSemiJoin(rewriting, view);
			if (!this.m_qp.IsEmpty(t_Tile))
			{
				this.m_usedViews[view] = TileOpKind.AntiSemiJoin;
				rewriting = t_Tile;
				return this.m_qp.IsDisjointFrom(rewriting, this.m_toAvoid);
			}
			return false;
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000F3008 File Offset: 0x000F1208
		private bool FindRewritingByIncludedAndDisjoint(out T_Tile rewritingSoFar)
		{
			rewritingSoFar = default(T_Tile);
			foreach (T_Tile t_Tile in this.AvailableViews)
			{
				if (this.m_qp.IsContainedIn(this.m_toFill, t_Tile))
				{
					if (rewritingSoFar == null)
					{
						rewritingSoFar = t_Tile;
						this.m_usedViews[t_Tile] = TileOpKind.Join;
					}
					else
					{
						T_Tile t_Tile2 = this.m_qp.Join(rewritingSoFar, t_Tile);
						if (this.m_qp.IsContainedIn(rewritingSoFar, t_Tile2))
						{
							continue;
						}
						rewritingSoFar = t_Tile2;
						this.m_usedViews[t_Tile] = TileOpKind.Join;
					}
					if (this.m_qp.IsContainedIn(rewritingSoFar, this.m_toFill))
					{
						return true;
					}
				}
			}
			if (rewritingSoFar != null)
			{
				foreach (T_Tile t_Tile3 in this.AvailableViews)
				{
					if (this.m_qp.IsDisjointFrom(this.m_toFill, t_Tile3) && !this.m_qp.IsDisjointFrom(rewritingSoFar, t_Tile3))
					{
						rewritingSoFar = this.m_qp.AntiSemiJoin(rewritingSoFar, t_Tile3);
						this.m_usedViews[t_Tile3] = TileOpKind.AntiSemiJoin;
						if (this.m_qp.IsContainedIn(rewritingSoFar, this.m_toFill))
						{
							return true;
						}
					}
				}
			}
			return rewritingSoFar != null;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000F31B4 File Offset: 0x000F13B4
		private bool FindContributingView(out T_Tile rewriting)
		{
			foreach (T_Tile t_Tile in this.AvailableViews)
			{
				if (!this.m_qp.IsDisjointFrom(t_Tile, this.m_toFill))
				{
					rewriting = t_Tile;
					this.m_usedViews[t_Tile] = TileOpKind.Join;
					return true;
				}
			}
			rewriting = default(T_Tile);
			return false;
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x000F3230 File Offset: 0x000F1430
		private IEnumerable<T_Tile> AvailableViews
		{
			get
			{
				return this.m_views.Where((T_Tile view) => !this.m_usedViews.ContainsKey(view));
			}
		}

		// Token: 0x040018B3 RID: 6323
		private readonly T_Tile m_toFill;

		// Token: 0x040018B4 RID: 6324
		private readonly T_Tile m_toAvoid;

		// Token: 0x040018B5 RID: 6325
		private readonly List<T_Tile> m_views;

		// Token: 0x040018B6 RID: 6326
		private readonly RewritingProcessor<T_Tile> m_qp;

		// Token: 0x040018B7 RID: 6327
		private readonly Dictionary<T_Tile, TileOpKind> m_usedViews = new Dictionary<T_Tile, TileOpKind>();
	}
}
