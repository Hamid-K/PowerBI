using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200058D RID: 1421
	internal abstract class Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060044CA RID: 17610 RVA: 0x000F3983 File Offset: 0x000F1B83
		protected Tile(TileOpKind opKind, T_Query query)
		{
			this.m_opKind = opKind;
			this.m_query = query;
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x000F3999 File Offset: 0x000F1B99
		public T_Query Query
		{
			get
			{
				return this.m_query;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060044CC RID: 17612
		public abstract string Description { get; }

		// Token: 0x060044CD RID: 17613 RVA: 0x000F39A1 File Offset: 0x000F1BA1
		public IEnumerable<T_Query> GetNamedQueries()
		{
			return Tile<T_Query>.GetNamedQueries(this);
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000F39A9 File Offset: 0x000F1BA9
		private static IEnumerable<T_Query> GetNamedQueries(Tile<T_Query> rewriting)
		{
			if (rewriting != null)
			{
				if (rewriting.OpKind == TileOpKind.Named)
				{
					yield return ((TileNamed<T_Query>)rewriting).NamedQuery;
				}
				else
				{
					foreach (T_Query t_Query in Tile<T_Query>.GetNamedQueries(rewriting.Arg1))
					{
						yield return t_Query;
					}
					IEnumerator<T_Query> enumerator = null;
					foreach (T_Query t_Query2 in Tile<T_Query>.GetNamedQueries(rewriting.Arg2))
					{
						yield return t_Query2;
					}
					enumerator = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000F39BC File Offset: 0x000F1BBC
		public override string ToString()
		{
			if (this.Description != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}: [{1}]", new object[] { this.Description, this.Query });
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[] { this.Query });
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060044D0 RID: 17616
		public abstract Tile<T_Query> Arg1 { get; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060044D1 RID: 17617
		public abstract Tile<T_Query> Arg2 { get; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x000F3A21 File Offset: 0x000F1C21
		public TileOpKind OpKind
		{
			get
			{
				return this.m_opKind;
			}
		}

		// Token: 0x060044D3 RID: 17619
		internal abstract Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile);

		// Token: 0x040018C6 RID: 6342
		private readonly T_Query m_query;

		// Token: 0x040018C7 RID: 6343
		private readonly TileOpKind m_opKind;
	}
}
