using System;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000032 RID: 50
	internal class SkipClause : ISqlFragment
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000117A2 File Offset: 0x0000F9A2
		internal ISqlFragment SkipCount
		{
			get
			{
				return this.skipCount;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000117AA File Offset: 0x0000F9AA
		internal SkipClause(ISqlFragment skipCount)
		{
			this.skipCount = skipCount;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000117BC File Offset: 0x0000F9BC
		internal SkipClause(int skipCount)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(skipCount.ToString(CultureInfo.InvariantCulture));
			this.skipCount = sqlBuilder;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000117EE File Offset: 0x0000F9EE
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("OFFSET ");
			this.SkipCount.WriteSql(writer, sqlGenerator);
			writer.Write(" ROWS ");
		}

		// Token: 0x040000E4 RID: 228
		private readonly ISqlFragment skipCount;
	}
}
