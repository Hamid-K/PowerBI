using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200058F RID: 1423
	internal class TileNamed<T_Query> : Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060044D9 RID: 17625 RVA: 0x000F3B14 File Offset: 0x000F1D14
		public TileNamed(T_Query namedQuery)
			: base(TileOpKind.Named, namedQuery)
		{
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x000F3B1E File Offset: 0x000F1D1E
		public T_Query NamedQuery
		{
			get
			{
				return base.Query;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x000F3B26 File Offset: 0x000F1D26
		public override Tile<T_Query> Arg1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x000F3B29 File Offset: 0x000F1D29
		public override Tile<T_Query> Arg2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000F3B2C File Offset: 0x000F1D2C
		public override string Description
		{
			get
			{
				T_Query query = base.Query;
				return query.Description;
			}
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x000F3B50 File Offset: 0x000F1D50
		public override string ToString()
		{
			T_Query query = base.Query;
			return query.ToString();
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x000F3B71 File Offset: 0x000F1D71
		internal override Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile)
		{
			if (this != oldTile)
			{
				return this;
			}
			return newTile;
		}
	}
}
