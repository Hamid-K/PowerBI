using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000591 RID: 1425
	internal abstract class TileProcessor<T_Tile>
	{
		// Token: 0x060044E0 RID: 17632
		internal abstract bool IsEmpty(T_Tile tile);

		// Token: 0x060044E1 RID: 17633
		internal abstract T_Tile Union(T_Tile a, T_Tile b);

		// Token: 0x060044E2 RID: 17634
		internal abstract T_Tile Join(T_Tile a, T_Tile b);

		// Token: 0x060044E3 RID: 17635
		internal abstract T_Tile AntiSemiJoin(T_Tile a, T_Tile b);

		// Token: 0x060044E4 RID: 17636
		internal abstract T_Tile GetArg1(T_Tile tile);

		// Token: 0x060044E5 RID: 17637
		internal abstract T_Tile GetArg2(T_Tile tile);

		// Token: 0x060044E6 RID: 17638
		internal abstract TileOpKind GetOpKind(T_Tile tile);
	}
}
