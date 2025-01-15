using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000583 RID: 1411
	internal class DefaultTileProcessor<T_Query> : TileProcessor<Tile<T_Query>> where T_Query : ITileQuery
	{
		// Token: 0x06004431 RID: 17457 RVA: 0x000EFDC0 File Offset: 0x000EDFC0
		internal DefaultTileProcessor(TileQueryProcessor<T_Query> tileQueryProcessor)
		{
			this._tileQueryProcessor = tileQueryProcessor;
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x000EFDCF File Offset: 0x000EDFCF
		internal TileQueryProcessor<T_Query> QueryProcessor
		{
			get
			{
				return this._tileQueryProcessor;
			}
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000EFDD7 File Offset: 0x000EDFD7
		internal override bool IsEmpty(Tile<T_Query> tile)
		{
			return !this._tileQueryProcessor.IsSatisfiable(tile.Query);
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000EFDED File Offset: 0x000EDFED
		internal override Tile<T_Query> Union(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.Union, this._tileQueryProcessor.Union(arg1.Query, arg2.Query));
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x000EFE0E File Offset: 0x000EE00E
		internal override Tile<T_Query> Join(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.Join, this._tileQueryProcessor.Intersect(arg1.Query, arg2.Query));
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x000EFE2F File Offset: 0x000EE02F
		internal override Tile<T_Query> AntiSemiJoin(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.AntiSemiJoin, this._tileQueryProcessor.Difference(arg1.Query, arg2.Query));
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x000EFE50 File Offset: 0x000EE050
		internal override Tile<T_Query> GetArg1(Tile<T_Query> tile)
		{
			return tile.Arg1;
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x000EFE58 File Offset: 0x000EE058
		internal override Tile<T_Query> GetArg2(Tile<T_Query> tile)
		{
			return tile.Arg2;
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x000EFE60 File Offset: 0x000EE060
		internal override TileOpKind GetOpKind(Tile<T_Query> tile)
		{
			return tile.OpKind;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x000EFE68 File Offset: 0x000EE068
		internal bool IsContainedIn(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return this.IsEmpty(this.AntiSemiJoin(arg1, arg2));
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x000EFE78 File Offset: 0x000EE078
		internal bool IsEquivalentTo(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return this.IsContainedIn(arg1, arg2) && this.IsContainedIn(arg2, arg1);
		}

		// Token: 0x04001894 RID: 6292
		private readonly TileQueryProcessor<T_Query> _tileQueryProcessor;
	}
}
