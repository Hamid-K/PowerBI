using System;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000040 RID: 64
	internal class TopClause : ISqlFragment
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001A133 File Offset: 0x00018333
		internal bool WithTies
		{
			get
			{
				return this.withTies;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001A13B File Offset: 0x0001833B
		internal ISqlFragment TopCount
		{
			get
			{
				return this.topCount;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001A143 File Offset: 0x00018343
		internal TopClause(ISqlFragment topCount, bool withTies)
		{
			this.topCount = topCount;
			this.withTies = withTies;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001A15C File Offset: 0x0001835C
		internal TopClause(int topCount, bool withTies)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));
			this.topCount = sqlBuilder;
			this.withTies = withTies;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001A198 File Offset: 0x00018398
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("TOP ");
			if (sqlGenerator.SqlVersion != SqlVersion.Sql8)
			{
				writer.Write("(");
			}
			this.TopCount.WriteSql(writer, sqlGenerator);
			if (sqlGenerator.SqlVersion != SqlVersion.Sql8)
			{
				writer.Write(")");
			}
			writer.Write(" ");
			if (this.WithTies)
			{
				writer.Write("WITH TIES ");
			}
		}

		// Token: 0x04000128 RID: 296
		private readonly ISqlFragment topCount;

		// Token: 0x04000129 RID: 297
		private readonly bool withTies;
	}
}
