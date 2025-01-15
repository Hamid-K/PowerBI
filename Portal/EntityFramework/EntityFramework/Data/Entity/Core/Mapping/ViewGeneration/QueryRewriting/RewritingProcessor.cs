using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200058B RID: 1419
	internal class RewritingProcessor<T_Tile> : TileProcessor<T_Tile> where T_Tile : class
	{
		// Token: 0x060044A8 RID: 17576 RVA: 0x000F325A File Offset: 0x000F145A
		public RewritingProcessor(TileProcessor<T_Tile> tileProcessor)
		{
			this.m_tileProcessor = tileProcessor;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x000F3269 File Offset: 0x000F1469
		internal TileProcessor<T_Tile> TileProcessor
		{
			get
			{
				return this.m_tileProcessor;
			}
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x000F3271 File Offset: 0x000F1471
		public void GetStatistics(out int numSATChecks, out int numIntersection, out int numUnion, out int numDifference, out int numErrors)
		{
			numSATChecks = this.m_numSATChecks;
			numIntersection = this.m_numIntersection;
			numUnion = this.m_numUnion;
			numDifference = this.m_numDifference;
			numErrors = this.m_numErrors;
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x000F329D File Offset: 0x000F149D
		internal override T_Tile GetArg1(T_Tile tile)
		{
			return this.m_tileProcessor.GetArg1(tile);
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x000F32AB File Offset: 0x000F14AB
		internal override T_Tile GetArg2(T_Tile tile)
		{
			return this.m_tileProcessor.GetArg2(tile);
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x000F32B9 File Offset: 0x000F14B9
		internal override TileOpKind GetOpKind(T_Tile tile)
		{
			return this.m_tileProcessor.GetOpKind(tile);
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x000F32C7 File Offset: 0x000F14C7
		internal override bool IsEmpty(T_Tile a)
		{
			this.m_numSATChecks++;
			return this.m_tileProcessor.IsEmpty(a);
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x000F32E3 File Offset: 0x000F14E3
		public bool IsDisjointFrom(T_Tile a, T_Tile b)
		{
			return this.m_tileProcessor.IsEmpty(this.Join(a, b));
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x000F32F8 File Offset: 0x000F14F8
		internal bool IsContainedIn(T_Tile a, T_Tile b)
		{
			T_Tile t_Tile = this.AntiSemiJoin(a, b);
			return this.IsEmpty(t_Tile);
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000F3318 File Offset: 0x000F1518
		internal bool IsEquivalentTo(T_Tile a, T_Tile b)
		{
			bool flag = this.IsContainedIn(a, b);
			bool flag2 = this.IsContainedIn(b, a);
			return flag && flag2;
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000F3338 File Offset: 0x000F1538
		internal override T_Tile Union(T_Tile a, T_Tile b)
		{
			this.m_numUnion++;
			return this.m_tileProcessor.Union(a, b);
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x000F3355 File Offset: 0x000F1555
		internal override T_Tile Join(T_Tile a, T_Tile b)
		{
			if (a == null)
			{
				return b;
			}
			this.m_numIntersection++;
			return this.m_tileProcessor.Join(a, b);
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000F337C File Offset: 0x000F157C
		internal override T_Tile AntiSemiJoin(T_Tile a, T_Tile b)
		{
			this.m_numDifference++;
			return this.m_tileProcessor.AntiSemiJoin(a, b);
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000F3399 File Offset: 0x000F1599
		public void AddError()
		{
			this.m_numErrors++;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x000F33AC File Offset: 0x000F15AC
		public int CountOperators(T_Tile query)
		{
			int num = 0;
			if (query != null && this.GetOpKind(query) != TileOpKind.Named)
			{
				num++;
				num += this.CountOperators(this.GetArg1(query));
				num += this.CountOperators(this.GetArg2(query));
			}
			return num;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x000F33F4 File Offset: 0x000F15F4
		public int CountViews(T_Tile query)
		{
			HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
			this.GatherViews(query, hashSet);
			return hashSet.Count;
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x000F3415 File Offset: 0x000F1615
		public void GatherViews(T_Tile rewriting, HashSet<T_Tile> views)
		{
			if (rewriting != null)
			{
				if (this.GetOpKind(rewriting) == TileOpKind.Named)
				{
					views.Add(rewriting);
					return;
				}
				this.GatherViews(this.GetArg1(rewriting), views);
				this.GatherViews(this.GetArg2(rewriting), views);
			}
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x000F344E File Offset: 0x000F164E
		public static IEnumerable<T> AllButOne<T>(IEnumerable<T> list, int toSkipPosition)
		{
			int valuePosition = 0;
			foreach (T t in list)
			{
				int num = valuePosition;
				valuePosition = num + 1;
				if (num != toSkipPosition)
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000F3465 File Offset: 0x000F1665
		public static IEnumerable<T> Concat<T>(T value, IEnumerable<T> rest)
		{
			yield return value;
			foreach (T t in rest)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x000F347C File Offset: 0x000F167C
		public static IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> list)
		{
			IEnumerable<T> rest = null;
			int valuePosition = 0;
			foreach (T value in list)
			{
				int num = valuePosition;
				valuePosition = num + 1;
				rest = RewritingProcessor<T_Tile>.AllButOne<T>(list, num);
				foreach (IEnumerable<T> enumerable in RewritingProcessor<T_Tile>.Permute<T>(rest))
				{
					yield return RewritingProcessor<T_Tile>.Concat<T>(value, enumerable);
				}
				IEnumerator<IEnumerable<T>> enumerator2 = null;
				value = default(T);
			}
			IEnumerator<T> enumerator = null;
			if (rest == null)
			{
				yield return list;
			}
			yield break;
			yield break;
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000F348C File Offset: 0x000F168C
		public static List<T> RandomPermutation<T>(IEnumerable<T> input)
		{
			List<T> list = new List<T>(input);
			for (int i = 0; i < list.Count; i++)
			{
				int num = RewritingProcessor<T_Tile>.rnd.Next(list.Count);
				T t = list[i];
				list[i] = list[num];
				list[num] = t;
			}
			return list;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000F34E1 File Offset: 0x000F16E1
		public static IEnumerable<T> Reverse<T>(IEnumerable<T> input, HashSet<T> filter)
		{
			List<T> list = new List<T>(input);
			list.Reverse();
			foreach (T t in list)
			{
				if (filter.Contains(t))
				{
					yield return t;
				}
			}
			List<T>.Enumerator enumerator = default(List<T>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000F34F8 File Offset: 0x000F16F8
		public bool RewriteQuery(T_Tile toFill, T_Tile toAvoid, IEnumerable<T_Tile> views, out T_Tile rewriting)
		{
			if (this.RewriteQueryOnce(toFill, toAvoid, views, out rewriting))
			{
				HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
				this.GatherViews(rewriting, hashSet);
				int num = this.CountOperators(rewriting);
				int num2 = 0;
				int num3 = Math.Min(0, Math.Max(0, (int)((double)hashSet.Count * 0.0)));
				while (num2++ < num3)
				{
					IEnumerable<T_Tile> enumerable;
					if (num2 == 1)
					{
						enumerable = RewritingProcessor<T_Tile>.Reverse<T_Tile>(views, hashSet);
					}
					else
					{
						enumerable = RewritingProcessor<T_Tile>.RandomPermutation<T_Tile>(hashSet);
					}
					T_Tile t_Tile;
					this.RewriteQueryOnce(toFill, toAvoid, enumerable, out t_Tile);
					int num4 = this.CountOperators(t_Tile);
					if (num4 < num)
					{
						num = num4;
						rewriting = t_Tile;
					}
					HashSet<T_Tile> hashSet2 = new HashSet<T_Tile>();
					this.GatherViews(t_Tile, hashSet2);
					hashSet = hashSet2;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000F35B8 File Offset: 0x000F17B8
		public bool RewriteQueryOnce(T_Tile toFill, T_Tile toAvoid, IEnumerable<T_Tile> views, out T_Tile rewriting)
		{
			List<T_Tile> list = new List<T_Tile>(views);
			return RewritingPass<T_Tile>.RewriteQuery(toFill, toAvoid, out rewriting, list, this);
		}

		// Token: 0x040018B8 RID: 6328
		public const double PermuteFraction = 0.0;

		// Token: 0x040018B9 RID: 6329
		public const int MinPermutations = 0;

		// Token: 0x040018BA RID: 6330
		public const int MaxPermutations = 0;

		// Token: 0x040018BB RID: 6331
		private int m_numSATChecks;

		// Token: 0x040018BC RID: 6332
		private int m_numIntersection;

		// Token: 0x040018BD RID: 6333
		private int m_numDifference;

		// Token: 0x040018BE RID: 6334
		private int m_numUnion;

		// Token: 0x040018BF RID: 6335
		private int m_numErrors;

		// Token: 0x040018C0 RID: 6336
		private readonly TileProcessor<T_Tile> m_tileProcessor;

		// Token: 0x040018C1 RID: 6337
		private static Random rnd = new Random(1507);
	}
}
