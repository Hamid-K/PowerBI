using System;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200058E RID: 1422
	internal class TileBinaryOperator<T_Query> : Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060044D4 RID: 17620 RVA: 0x000F3A29 File Offset: 0x000F1C29
		public TileBinaryOperator(Tile<T_Query> arg1, Tile<T_Query> arg2, TileOpKind opKind, T_Query query)
			: base(opKind, query)
		{
			this.m_arg1 = arg1;
			this.m_arg2 = arg2;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060044D5 RID: 17621 RVA: 0x000F3A42 File Offset: 0x000F1C42
		public override Tile<T_Query> Arg1
		{
			get
			{
				return this.m_arg1;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x000F3A4A File Offset: 0x000F1C4A
		public override Tile<T_Query> Arg2
		{
			get
			{
				return this.m_arg2;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x000F3A54 File Offset: 0x000F1C54
		public override string Description
		{
			get
			{
				string text = null;
				switch (base.OpKind)
				{
				case TileOpKind.Union:
					text = "({0} | {1})";
					break;
				case TileOpKind.Join:
					text = "({0} & {1})";
					break;
				case TileOpKind.AntiSemiJoin:
					text = "({0} - {1})";
					break;
				}
				return string.Format(CultureInfo.InvariantCulture, text, new object[]
				{
					this.Arg1.Description,
					this.Arg2.Description
				});
			}
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x000F3AC4 File Offset: 0x000F1CC4
		internal override Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile)
		{
			Tile<T_Query> tile = this.Arg1.Replace(oldTile, newTile);
			Tile<T_Query> tile2 = this.Arg2.Replace(oldTile, newTile);
			if (tile != this.Arg1 || tile2 != this.Arg2)
			{
				return new TileBinaryOperator<T_Query>(tile, tile2, base.OpKind, base.Query);
			}
			return this;
		}

		// Token: 0x040018C8 RID: 6344
		private readonly Tile<T_Query> m_arg1;

		// Token: 0x040018C9 RID: 6345
		private readonly Tile<T_Query> m_arg2;
	}
}
