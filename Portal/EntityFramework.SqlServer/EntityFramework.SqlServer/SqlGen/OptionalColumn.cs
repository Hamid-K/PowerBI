using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000031 RID: 49
	internal sealed class OptionalColumn
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00011734 File Offset: 0x0000F934
		internal void Append(object s)
		{
			this.m_builder.Append(s);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00011742 File Offset: 0x0000F942
		internal void MarkAsUsed()
		{
			this.m_usageManager.MarkAsUsed(this.m_symbol);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00011755 File Offset: 0x0000F955
		internal OptionalColumn(SymbolUsageManager usageManager, Symbol symbol)
		{
			this.m_usageManager = usageManager;
			this.m_symbol = symbol;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00011776 File Offset: 0x0000F976
		public bool WriteSqlIfUsed(SqlWriter writer, SqlGenerator sqlGenerator, string separator)
		{
			if (this.m_usageManager.IsUsed(this.m_symbol))
			{
				writer.Write(separator);
				this.m_builder.WriteSql(writer, sqlGenerator);
				return true;
			}
			return false;
		}

		// Token: 0x040000E1 RID: 225
		private readonly SymbolUsageManager m_usageManager;

		// Token: 0x040000E2 RID: 226
		private readonly SqlBuilder m_builder = new SqlBuilder();

		// Token: 0x040000E3 RID: 227
		private readonly Symbol m_symbol;
	}
}
