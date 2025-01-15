using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003D RID: 61
	internal class SymbolPair : ISqlFragment
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00019FAF File Offset: 0x000181AF
		public SymbolPair(Symbol source, Symbol column)
		{
			this.Source = source;
			this.Column = column;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00019FC5 File Offset: 0x000181C5
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
		}

		// Token: 0x04000124 RID: 292
		public Symbol Source;

		// Token: 0x04000125 RID: 293
		public Symbol Column;
	}
}
